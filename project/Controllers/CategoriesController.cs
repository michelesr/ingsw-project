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

    // controller per la gestione delle categorie
    public class CategoriesController : Controller {

        // GET /api/categories/
        // richiede api_key negli header http
        // ritorna tutte le categorie
		[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else if (id == -1)
                return Json(Model.getAll<ProductCategory>(), JsonRequestBehavior.AllowGet);
            else
                return Detail(id);
		}

        // GET /api/categories/detail/<id>
        // richiede api_key negli header http
        // ritorna una categoria
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<ProductCategory>(id), JsonRequestBehavior.AllowGet);
        }


        // GET /api/categories/delete/<id>
        // richiede una admin api_key negli header http
        // elimina una categoria
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Delete(int id) {
            if(!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                Model.getById<ProductCategory>(id).delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        // POST /api/categories/update/<id>
        // richiede una admin api_key negli header http
        // aggiorna una categoria
        // data: {name: "name"}
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update(int id) {
            if(!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                ConvertibleHashtable newData = ConvertibleHashtable.fromRequest();
                ConvertibleHashtable currentData = Model.getHashtableById<ProductCategory>(id);
                currentData.update(newData);
                currentData.toObject<ProductCategory>().update();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        // POST /api/categories/
        // richiede una admin api_key negli header http
        // aggiunge una categoria
        // data: {name: "name"}
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Index() {
            if(!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else {
                ConvertibleHashtable.fromRequest().toObject<ProductCategory>().insert();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }
	}
}
