using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;
using project.Utils;

namespace project.Controllers {

    public class CatalogController : Controller {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Export(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED);
            else {
                StreamWriter s = new StreamWriter("Content/export.json");
                Catalog c = new Catalog(id);
                Console.WriteLine(c);
                s.Write(c);
                s.Close();
                return File("Content/export.json", "application/json");
            }
        }
    }
}

