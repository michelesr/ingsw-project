using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;

namespace project.Controllers {

	// /api/user
	public class UserController : Controller {

		// GET: /api/user/
		// GET: /api/user/list/
		public ActionResult List () {
			return Json(Models.User.getAll(), JsonRequestBehavior.AllowGet);
		}

		// GET: /api/user/1/detail/
		//public ActionResult Detail (int id) {
		//	return Json(Models.User.getOne(id), JsonRequestBehavior.AllowGet);
		//}
	}
}
