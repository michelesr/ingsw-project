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

    /// Controller degli stocks
    public class StocksController : Controller {

		[AcceptVerbs(HttpVerbs.Get)]
        // Ritorna tutti gli stocks
        // Api Reference: GET /api/stocks/
        // Requisiti: api_key negli header http
        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else if (id == -1)
                return Json(Model.getAll<ProductStock>(), JsonRequestBehavior.AllowGet);
            else
                return Detail(id);
		}

        [AcceptVerbs(HttpVerbs.Get)]
        // Ritorna uno stock
        // Api Reference: GET /api/stocks/detail/<id>/
        // Requisiti: api_key negli header http
        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<ProductStock>(id), JsonRequestBehavior.AllowGet);
        }

                
        [AcceptVerbs(HttpVerbs.Get)]
        // Elimina uno stock
        // Api Reference: GET /api/stocks/delete/<id>/
        // Requisiti: admin api_key negli header http
        public JsonResult Delete(int id) {
            ApiKey k = ApiKey.getApiKey();
            ProductStock ps = Model.getById<ProductStock>(id);
            if(k.isAdmin() || ps.checkUserId(k.user_id))  {
                ps.delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        // Aggiorna uno stock
        // Api Reference: POST /api/stocks/update/<id>/
        // Requisiti: admin api_key negli header http
        // JSON Data: {product_id:<id>, price:<pr>, min:<min>, max:<max>, availability:<av>}
        public JsonResult Update(int id) {
            ApiKey k = ApiKey.getApiKey();
            ConvertibleHashtable newData = ConvertibleHashtable.fromRequest();
            ConvertibleHashtable currentData = Model.getHashtableById<ProductStock>(id);
            currentData.update(newData);
            ProductStock s = currentData.toObject<ProductStock>();
            if (k.isAdmin() || s.checkUserId(k.user_id)) {
                s.update();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }
                
        [AcceptVerbs(HttpVerbs.Post)]
        // Aggiunge uno stock
        // Api Reference: POST /api/stocks/
        // Requisiti: admin api_key negli header http
        // JSON Data: {product_id:<id>, price:<pr>, min:<min>, max:<max>, availability:<av>}
        public JsonResult Index() {
            ApiKey k = ApiKey.getApiKey();
            ProductStock s = ConvertibleHashtable.fromRequest().toObject<ProductStock>();
            if(k.isAdmin() || s.checkUserId(k.user_id)) {
                s.insert();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }

	}
}
