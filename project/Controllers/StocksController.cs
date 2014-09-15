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

    public class StocksController : Controller {

        // GET /api/stocks/
        // richiede api_key negli header http
        // ritorna tutti gli stocks
		[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else if (id == -1)
                return Json(Model.getAll<ProductStock>(), JsonRequestBehavior.AllowGet);
            else
                return Detail(id);
		}

        // GET /api/stocks/detail/<id>
        // richiede api_key negli header http
        // ritorna uno stock
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<ProductStock>(id), JsonRequestBehavior.AllowGet);
        }

                
        // GET /api/stocks/delete/<id>
        // richiede una admin api_key negli header http
        // elimina uno stock
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Delete(int id) {
            ApiKey k = ApiKey.getApiKey();
            ProductStock ps = Model.getById<ProductStock>(id);
            if(k.isAdmin() || ps.checkUserId(id))  {
                ps.delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }


        // POST /api/stocks/update/<id>
        // richiede una admin api_key negli header http
        // aggiorna uno stock
        // data: {product_id:<id>, price:<pr>, min:<min>, max:<max>, availability:<av>}
        [AcceptVerbs(HttpVerbs.Post)]
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
                
        // POST /api/stocks/
        // richiede una admin api_key negli header http
        // aggiunge uno stock
        // data: {product_id:<id>, price:<pr>, min:<min>, max:<max>, availability:<av>}
        [AcceptVerbs(HttpVerbs.Post)]
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
