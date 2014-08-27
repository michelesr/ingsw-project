using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Newtonsoft.Json.Linq;
using project.Models;
using project.Utils;

namespace project.Controllers {

	public class UsersController : Controller {

        // GET /api/users/
        // GET /api/users/index/
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Index(int id) {
			ApiKey k = ApiKey.getApiKey();
            if (id == -1 && k.isAdmin())
                return Json(ConvertibleHashtable.filterPassword(Model.getAll<Models.User>()), JsonRequestBehavior.AllowGet);
			else 
				return Detail (id);
		}

        // GET /api/users/detail/<id>/
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Detail(int id) {
			ApiKey k = ApiKey.getApiKey();
			if (k.isAdmin() || k.checkUser(id))
			    return Json(Model.getHashtableById<Models.User>(id).filterPassword(), JsonRequestBehavior.AllowGet);
			else
				return Json(Utils.Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
		}

        // GET /api/users/delete/<id>/
        public JsonResult Delete(int id) {
            ApiKey k = ApiKey.getApiKey();
            if(k.isAdmin() || k.checkUser(id)) {
                Admin a = Admin.getByUserId(id);
                if (a.user_id == id)
                    a.delete();
                else {
                    Supplier s = Supplier.getByUserId(id);
                    if (s.user_id == id)
                        s.delete();
                    else
                        return Json(Utils.Costants.USER_NOT_FOUND, JsonRequestBehavior.AllowGet);
                }
                ApiKey.fromUserId(id).delete();
                Model.getById<User>(id).delete();
                return Json(Utils.Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Utils.Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }

        // POST /api/users/add/
		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Index() {
            ConvertibleHashtable h = ConvertibleHashtable.fromRequest();
			ConvertibleHashtable ud = ((JObject)h ["user_data"]).ToObject<ConvertibleHashtable>();
			ApiKey k = ApiKey.getApiKey();
			if (k.isAdmin ()) {
                if (h["type"].ToString() == "supplier") {
                    ud.merge(((JObject) h["supplier_data"]).ToObject<ConvertibleHashtable>());
                    ud.toObject<Supplier>().insert();
                }
                else if (h["type"].ToString() == "admin")
					ud.toObject<Admin>().insert();
				else
      				return Json(Utils.Costants.WRONG_USER_TYPE, JsonRequestBehavior.AllowGet);
				return Json(Utils.Costants.OK, JsonRequestBehavior.AllowGet);
			}
			else 
				return Json(Utils.Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
		}

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update(int id) {
            ConvertibleHashtable h = ConvertibleHashtable.fromRequest();
            ApiKey k = ApiKey.getApiKey();
            if (k.isAdmin() || k.checkUser(id)) {
                ConvertibleHashtable cd = Model.getHashtableById<User>(id);
                ConvertibleHashtable ud = ((JObject) h["user_data"]).ToObject<ConvertibleHashtable>();
                foreach (var key in ud.Keys) 
                    if (cd.ContainsKey(key))
                        cd[key] = ud[key];
                cd.toObject<User>().update();
                ApiKey.fromUserId(id).update();
                return Json(Utils.Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else 
                return Json(Utils.Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }
	}
}
