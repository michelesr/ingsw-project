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


		private static void initDb(Database db) {
			String[][] x = Models.User.model;
			String[][] y = Models.Supplier.supplierModel;
			String[][] model = new String[x.Length + y.Length][];
			x.CopyTo(model, 0);
			y.CopyTo(model, x.Length);
			db.createTable("Supplier", model);
			db.createTable("Admin", x); 
		}

		protected void Application_Start ()
		{
			Database db = Database.Istance;
			initDb(db);
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}
	}
}
