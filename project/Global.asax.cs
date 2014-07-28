using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using project.Models;
using project.Utils;

namespace project
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				"DefaultApi",
				"api/{controller}/{id}/{action}",
				new { id = "", action = "List" }
			);

			routes.MapRoute (
				"Default",
				"{controller}/{action}",
				new { controller = "Home", action = "Index" }
			);

		}

		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			Schema.createSchema();
			Admin a = new Admin("michele@gmail.com", "jigrweijeriogjeio", "Michele", "Verdi");
			Supplier s = new Supplier("pincopallino@gmail.com", "4ijijrojiiojioo3r90", "Pinco","Pallino", "498959jijgir", "PincoPallino&co", "Marotta");
			a.insert();
			s.insert();
			a.email = "michele@libero.it";
			a.last_name = "Verdino";
			s.first_name = "Tonio";
			s.last_name = "Sempronio";
			s.supplier_name = "boiamond";
			a.update();
			s.update();
			s.delete();
			a.delete();
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}
	}
}
