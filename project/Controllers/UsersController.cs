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
        // richiede una api_key negli headers http
        // ritorna tutti gli utenti
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Index(int id) {
			ApiKey k = ApiKey.getApiKey();
            if (id == -1 && k.isAdmin())
                return Json(ConvertibleHashtable.filterPassword(Model.getAll<User>()), JsonRequestBehavior.AllowGet);
			else 
				return Detail (id);
		}

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult IndexSupplier(int id) {
            ApiKey k = ApiKey.getApiKey();
            if (id == -1 && k.isAdmin())
                return Json(Supplier.getAll(), JsonRequestBehavior.AllowGet);
            else 
                return Detail (id);
        }

        // GET /api/users/detail/<id>/
        // richiede una api_key negli headers http
        // ritorna i dettagli di un utente
        // nota: se l'utente è un supplier ritorna anche le
        // informazioni del supplier
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Detail(int id) {
			ApiKey k = ApiKey.getApiKey();
            if (k.isAdmin() || k.checkUser(id)) {
                ConvertibleHashtable h = Model.getHashtableById<User>(id);
                if (h.toObject<User>().type == userType.supplier)
                    h = Supplier.getHashtableByUserId(id);
                return Json(h.filterPassword(), JsonRequestBehavior.AllowGet);
            }
			else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
		}

        // GET /api/users/delete/<id>/
        // richiede una api_key negli headers http
        // elimina un utente
        public JsonResult Delete(int id) {
            ApiKey k = ApiKey.getApiKey();
            if(k.isAdmin() || k.checkUser(id)) {
                ApiKey.fromUserId(id).delete();
                Admin a = Admin.getByUserId(id);
                if (a.user_id == id)
                    a.delete();
                else {
                    Supplier s = Supplier.getByUserId(id);
                    if (s.user_id == id)
                        s.delete();
                    else
                        return Json(Costants.USER_NOT_FOUND, JsonRequestBehavior.AllowGet);
                }
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }

        // POST /api/users/add/
        // richiede una admin api_key negli haeders http
        // aggiunge un utente
        // data: {type: "admin|supplier", user_data:{first_name:"fn", last_name:"ln", email:"email", password:"pw"},
        //        supplier_data:{vat:"vat", supplier_name:"name", city:<city_id>}}
		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Index() {
            ConvertibleHashtable h = ConvertibleHashtable.fromRequest();
            ConvertibleHashtable ud = ConvertibleHashtable.fromJObject((JObject) h["user_data"]);
			ApiKey k = ApiKey.getApiKey();
			if (k.isAdmin ()) {
                if (h["type"].ToString() == "supplier") {
                    ud.merge(((JObject) h["supplier_data"]).ToObject<ConvertibleHashtable>());
                    if (ud.ContainsKey("user_id"))
                        ud.Remove("user_id");
                    ud.toObject<Supplier>().insert();
                }
                else if (h["type"].ToString() == "admin")
					ud.toObject<Admin>().insert();
				else
      				return Json(Costants.WRONG_USER_TYPE, JsonRequestBehavior.AllowGet);
				return Json(Costants.OK, JsonRequestBehavior.AllowGet);
			}
			else 
				return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
		}

        // POST /api/users/update/<id>
        // richiede una api_key negli headers http
        // aggiorna le informazioni su un utente
        // data: {user_data:{first_name:"fn", last_name:"ln", email:"email", password:"pw"},
        //        supplier_data:{vat:"vat", supplier_name:"name", city:<city_id>}}
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update(int id) {
            ConvertibleHashtable h = ConvertibleHashtable.fromRequest();
            ApiKey k = ApiKey.getApiKey();
            if (k.isAdmin() || k.checkUser(id)) {
                // controlla se si deve modificare le informazioni specifiche di un supplier
                ConvertibleHashtable supplierCurrentData = Supplier.getHashtableByUserId(id);
                if (h.ContainsKey("supplier_data") && supplierCurrentData["user_id"].ToString() == id.ToString()) {
                    ConvertibleHashtable newData = ConvertibleHashtable.fromJObject((JObject) h["supplier_data"]);
                    if (h.ContainsKey("user_data"))
                        newData.merge(ConvertibleHashtable.fromJObject((JObject) h["user_data"]));
                    supplierCurrentData.update(newData);
                    if (supplierCurrentData.ContainsKey("user_id"))
                        supplierCurrentData.Remove("user_id");
                    supplierCurrentData.toObject<Supplier>().update();
                }
                // modifica le info di base dell'utente, che sia admin o supplier
                else {
                    ConvertibleHashtable currentData = Model.getHashtableById<User>(id);
                    ConvertibleHashtable newData = ConvertibleHashtable.fromJObject((JObject) h["user_data"]);
                    currentData.update(newData);
                    currentData.toObject<User>().update();
                }
                // aggiorna le api key per riflettere evenutali modifiche alla password o alla mail
                ApiKey.fromUserId(id).update();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }
	}
}
