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

    /// Controller per la gestione delle categorie: /api/categories/

    public class CategoriesController : Controller {

		[AcceptVerbs(HttpVerbs.Get)]

        /** Ritorna tutte le categorie

            API Reference: GET /api/categories/

            Requisiti: api_key negli header http */

        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else if (id == -1)
                return Json(Model.getAll<ProductCategory>(), JsonRequestBehavior.AllowGet);
            else
                return Detail(id);
		}

        [AcceptVerbs(HttpVerbs.Get)]

        /** Ritorna una categoria

            API Reference: GET /api/categories/detail/<id>/

            Requisiti: api_key negli header http */

        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<ProductCategory>(id), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]

        /** Elimina una categoria

           API Reference: GET /api/categories/delete/<id>/

           Requisiti: admin api_key negli header http */

        public JsonResult Delete(int id) {
            if(!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                Model.getById<ProductCategory>(id).delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]

        /** Aggiorna una categoria

            API Reference: POST /api/categories/update/<id>/

            Requisiti: admin api_key negli header http

            JSON Data: {name: "name"} */

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

        [AcceptVerbs(HttpVerbs.Post)]

        /** Aggiunge una categoria

            API Reference: POST /api/categories/

            Requisiti: admin api_key negli header http

            JSON Data: {name: "name"} */

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
