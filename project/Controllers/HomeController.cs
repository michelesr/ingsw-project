using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace project.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			ViewData ["Message"] = "Welcome to ASP.NET MVC on Mono!";
			Database db = Database.Istance;
			db.printTable("Supplier"); 
			return View ();
		}
	}
}

