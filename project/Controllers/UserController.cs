using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;
using project.Utils;

namespace project.Controllers {

	public class UserController : Controller {

		// /#/user/list
		// GET /api/user/
		// GET /api/user/list/
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Index(int id) {
			if (id == -1)
				return Json(ConvertibleHashtable.filterPassword(Model.getAll<Models.User>()), JsonRequestBehavior.AllowGet);
			else 
				return Detail(id);
		}

		// /#/user/1/detail
		// GET /api/user/detail/<id>/
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Detail(int id) {
			return Json(Model.getHashtableById<Models.User>(id).filterPassword(), JsonRequestBehavior.AllowGet);
		}


		// /#/user/1/create
		// POST /api/user/add/
		/*[AcceptVerbs(HttpVerbs.Post)]
		public String Index(String data, String type, String supplierData) {
			String returnMsg = "OK\n";
			ConvertibleHashtable d = JsonConvert.DeserializeObject<ConvertibleHashtable>(data);
			try {
				if (type == "admin")
				    Models.Admin.add(d, new ConvertibleHashtable());
				else if (type == "supplier") {
					if (supplierData != null) { 
						ConvertibleHashtable sd = JsonConvert.DeserializeObject<ConvertibleHashtable>(supplierData);
						Models.Supplier.add(d, sd); 
					}
					else
						returnMsg = "Invalid supplier data\n";
				}
				else
					returnMsg = "Invalid user type\n";
			}
			catch(Mono.Data.Sqlite.SqliteException e) {
				returnMsg = "Error in sqlite db: " + e.Message + "\n";
			}
			return returnMsg;
		}*/

		/*public String Test(String data) {
			ConvertibleHashtable d = JsonConvert.DeserializeObject<ConvertibleHashtable>(data);
			Console.WriteLine(d["data"].GetType());
			Console.WriteLine(d["type"].GetType());
			Console.WriteLine(((JObject)d["data"])["name"]);
			return "test api\n";
		}*/
	}
}
