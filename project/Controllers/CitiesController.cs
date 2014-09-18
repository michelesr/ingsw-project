using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;
using project.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project.Controllers {

    /// Controller delle Città.

    public class CitiesController : Controller {

		[AcceptVerbs(HttpVerbs.Get)]

        /** Ritorna tutte le città

            API Reference: GET /api/cities/

            Requisiti: api_key negli header http */

        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else if (id == -1)
               return Json(Model.getAll<City>(), JsonRequestBehavior.AllowGet);
            else
                return Detail(id);
		}
        
        [AcceptVerbs(HttpVerbs.Get)]

        /** Ritorna una città

            API Reference: GET /api/cities/detail/<id>/

            Requisiti: api_key negli header http */

        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<City>(id), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]

        /** Elimina una città

            Api Reference: GET /api/cities/delete/<id>/

            Requisiti: admin api_key negli header http */

        public JsonResult Delete(int id) {
            if (!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                Model.getById<City>(id).delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]

        /** Aggiorna una città

            Api Reference: POST /api/cities/update/<id>
	    
            Requisiti: admin api_key negli header http 

            JSON Data: {name: "name"} */

        public JsonResult Update(int id) {
            if (!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                ConvertibleHashtable newData = ConvertibleHashtable.fromRequest();
                ConvertibleHashtable currentData = Model.getHashtableById<City>(id);
                currentData.update(newData);
                currentData.toObject<City>().update();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]

        /** Aggiunge una città

            Api Reference: POST /api/cities/

            Requisiti: admin api_key negli header http 

            JSON Data: {name: "name"} */ 

        public JsonResult Index() {
            if (!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else {
                ConvertibleHashtable.fromRequest().toObject<City>().insert();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }
	}
}
