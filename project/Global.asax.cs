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
    // classe di partenza del programma
	public class MvcApplication : System.Web.HttpApplication
	{
        // registrazione del routing per le richieste http
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

        // aggiunto dall'ide
		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

        // punto di partenza del programma
		protected void Application_Start ()
		{
            // crea lo schema e aggiunge un admin se non è già presente
			Schema.createSchema();
            if (Admin.getUserByEmail("admin@example.org").email != "admin@example.org")
              new Admin("admin@example.org", "admin", "admin", "admin").insert();

            // aggiunti dall'ide
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}
	}
}
