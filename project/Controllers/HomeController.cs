using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace project.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return File("Content/base.html", "text/html");
		}
	}
}
