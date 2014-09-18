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

    /// Controller per l'esportazione dei listini: /api/catalogs/
    public class CatalogsController : Controller {

        [AcceptVerbs(HttpVerbs.Get)]
        /** Ritorna un JSON contenente il listino del produttore
            API Reference: GET /api/catalogs/detail/<supplier_id>/
            Requisiti: api_key negli header della richiesta http */
        public ActionResult Detail(int id) {
            // controllo dei permessi
            ApiKey k = ApiKey.getApiKey();
            if (!k.isAdmin() && !k.checkUser(Supplier.getUserIdBySupplierId(id)))
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else {
                // crea un file per l'esportazione e lo restituisce
                StreamWriter s = new StreamWriter("Content/export.json");
                Catalog c = new Catalog(id);
                Console.WriteLine(c);
                s.Write(c);
                s.Close();
                return File("Content/export.json", "text/plain");
            }
        }
    }
}

