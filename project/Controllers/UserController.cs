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

	// /api/user
	public class UserController : Controller {

		// GET: /api/user/
		// GET: /api/user/list/
		public ActionResult List() {
			return Json(Models.User.getAll(), JsonRequestBehavior.AllowGet);
		}

		// GET: /api/user/1/detail/
		public ActionResult Detail(int id) {
			return Json(Models.User.getById(id), JsonRequestBehavior.AllowGet);
		}

		// POST: /api/user/add/
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Add(String s) {
			Console.WriteLine(s);
			//Hashtable h = JsonConvert.DeserializeObject<Hashtable>(s);
			return Json(s, JsonRequestBehavior.AllowGet);
		}
	}
}
