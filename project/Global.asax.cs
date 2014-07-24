using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using project.Models;

namespace project
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				"DefaultAPI",
				"api/{controller}/{action}/{id}",
				new { id = "", action = "Index" }
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

		public static void initTables() {
			Admin.initTable();
			City.initTable();
			ProductCategory.initTable();
			Supplier.initTable();
			Models.User.initTable();
		}

		protected void Application_Start ()
		{
			initTables();
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}
	}
}
