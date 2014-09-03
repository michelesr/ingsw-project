using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace project.Controllers {
    // controller della homepage
    public class HomeController : Controller {
        // GET / 
        // ritorna la homepage
        public ActionResult Index() {
            return File("Content/base.html", "text/html");
		}
	}
}
