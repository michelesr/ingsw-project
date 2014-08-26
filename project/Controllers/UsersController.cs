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
				return Json(Utils.Costants.unauthorized, JsonRequestBehavior.AllowGet);
		}

		// /#/user/1/create
		// POST /api/user/add/
		[AcceptVerbs(HttpVerbs.Post)]
		public String Index(int id, String data) {
			return "add user " +  "id " + id;
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
