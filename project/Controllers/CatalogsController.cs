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

    // controller per l'esportazione dei listini
    public class CatalogsController : Controller {

        // GET /api/catalogs/detail/<id_supplier>
        // necessita api_key negli header della richiesta http
        // ritorna un JSON contenente tutte le informazioni su un produttore e i suoi stocks
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id) {
            // controllo dei permessi
            ApiKey k = ApiKey.getApiKey();
            if (!k.isAdmin() && !k.checkUser(id))
                return Json(Costants.UNAUTHORIZED);
            else {
                // crea un file per l'esportazione e lo restituisce
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

