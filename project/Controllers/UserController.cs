using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;
using Newtonsoft.Json;

namespace project.Controllers {

	public class UserController : Controller {

		// /#/user/list
		// GET /api/user/
		// GET /api/user/list/
		[AcceptVerbs(HttpVerbs.Get)]
		public String Index() {
			return JsonConvert.SerializeObject(Models.User.getAll());
		}

		// /#/user/1/detail
		// GET /api/user/detail/<id>/
		public String Detail(int id) {
			return JsonConvert.SerializeObject(Models.User.getById(id));
		}

		// /#/user/1/create
		// POST /api/user/add/
		[AcceptVerbs(HttpVerbs.Post)]
		public String Index(String data, String type, String supplierData) {
			String returnMsg = "OK\n";
			Hashtable d = JsonConvert.DeserializeObject<Hashtable>(data);
			try {
				if (type == "admin")
				    Models.Admin.add(d, new Hashtable());
				else if (type == "supplier") {
					if (supplierData != null) { 
						Hashtable sd = JsonConvert.DeserializeObject<Hashtable>(supplierData);
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
		}
	}
}
