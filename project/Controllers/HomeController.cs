using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace project.Controllers {
    /// Controller del base template: /
    public class HomeController : Controller {
        /* Ritorna il template base
           Reference: GET / */
        public ActionResult Index() {
            return File("Content/base.html", "text/html");
		}
	}
}
