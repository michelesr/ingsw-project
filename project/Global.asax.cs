using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace project
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = "" }
			);

		}

		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		private Database db;

		protected void Application_Start ()
		{
			db = Database.Istance;
			db.createTable("highscores", Models.User.model());
			db.insertData("highscores", Models.User.example_data());
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}
	}
}
