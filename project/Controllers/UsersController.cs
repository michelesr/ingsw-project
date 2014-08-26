using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Newtonsoft.Json.Linq;
using project.Models;
using project.Utils;

namespace project.Controllers {

	public class UsersController : Controller {

		// /#/user/list
		// GET /api/user/
		// GET /api/user/list/
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Index(int id) {
			ApiKey k = ApiKey.getApiKey();
			if (id == -1 &&  k.isAdmin())
				return Json (ConvertibleHashtable.filterPassword (Model.getAll<Models.User> ()), JsonRequestBehavior.AllowGet);
			else 
				return Detail (id);
		}

		// /#/user/1/detail
		// GET /api/user/detail/<id>/
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Detail(int id) {
			ApiKey k = ApiKey.getApiKey();
			if (k.isAdmin() || k.checkUser(id))
			    return Json(Model.getHashtableById<Models.User>(id).filterPassword(), JsonRequestBehavior.AllowGet);
			else
				return Json(Utils.Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
		}

		// /#/user/1/create
		// POST /api/user/add/
		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Index() {
			String data = new StreamReader(Request.InputStream).ReadLine();
			ConvertibleHashtable h = ConvertibleHashtable.fromString(data);
			ConvertibleHashtable ud = ((JObject)h ["user_data"]).ToObject<ConvertibleHashtable>();
			ApiKey k = ApiKey.getApiKey();
			if (k.isAdmin ()) {
				if (h ["type"].ToString () == "supplier") {
					ud.merge(((JObject)h["supplier_data"]).ToObject<ConvertibleHashtable>());
					ud.toObject<Supplier>().insert();
				} 
				else if (h ["type"].ToString () == "admin")
					ud.toObject<Admin>().insert();
				else
      				return Json(Utils.Costants.WRONG_USER_TYPE, JsonRequestBehavior.AllowGet);
				return Json(Utils.Costants.OK, JsonRequestBehavior.AllowGet);
			}
			else 
				return Json(Utils.Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
		}

		/*public String Test(String data) {
			ConvertibleHashtable d = JsonConvert.DeserializeObject<ConvertibleHashtable>(data);
			Console.WriteLine(d["data"].GetType());
			Console.WriteLine(d["type"].GetType());
			Console.WriteLine(((JObject)d["data"])["name"]);
			return "test api\n";
		}*/
	}
}
