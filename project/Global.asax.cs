﻿using System;
using System.Collections.Generic;
using System.Collections;
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
				"API",
				"api/{controller}/{id}/{action}",
				new { controller = "", action = "List", id = "" }
			);

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


		private static void initDb(Database db) {

		}

		protected void Application_Start ()
		{
			Database db = Database.Istance;
			initDb(db);
			Models.City.initTable();
			Models.ProductCategory.initTable();
			Models.User.initTable();
			Models.Supplier.initTable();
			Models.Admin.initTable();
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
		}
	}
}
