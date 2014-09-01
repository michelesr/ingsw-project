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
				"api/{controller}/{action}/{id}",
				new { action = "Index", id = -1 }
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
            if (Admin.getByUserId(1).email != "admin@example.org")
              new Admin("admin@example.org", "admin", "admin", "admin").insert();
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}
	}
}
