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

    public class CitiesController : Controller {

        // GET /api/cities/
        // richiede api_key negli header http
        // ritorna tutte le città
		[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else if (id == -1)
               return Json(Model.getAll<City>(), JsonRequestBehavior.AllowGet);
            else
                return Detail(id);
		}
        
        // GET /api/cities/detail/<id>
        // richiede api_key negli header http
        // ritorna una città
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<City>(id), JsonRequestBehavior.AllowGet);
        }

        // GET /api/cities/delete/<id>
        // richiede una admin api_key negli header http
        // elimina una città
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Delete(int id) {
            if (!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                Model.getById<City>(id).delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        // POST /api/cities/update/<id>
        // richiede una admin api_key negli header http
        // aggiorna una città
        // data: {name: "name"}
        [AcceptVerbs(HttpVerbs.Post)]
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

        // POST /api/cities/
        // richiede una admin api_key negli header http
        // aggiunge una città
        // data: {name: "name"}
        [AcceptVerbs(HttpVerbs.Post)]
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
