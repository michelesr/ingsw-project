using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;

namespace project.Controllers {

	// GET: /api/product
	public class UserController : Controller {

		// GET: /api/result/
		// GET: /api/result/list/
		public ActionResult List () {
			return Json(Models.User.getAll(), JsonRequestBehavior.AllowGet);
		}

	}
}
