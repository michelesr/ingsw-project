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

	public class ProductsController : Controller {

        // get all products
		// GET /api/products/
		[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else {
                if (id == -1)
                    return Json(Model.getAll<Product>(), JsonRequestBehavior.AllowGet);
                else
                    return Detail(id);
            }

		}

        // GET /api/products/detail/<id>
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<Product>(id), JsonRequestBehavior.AllowGet);
        }

        // delete product
        // GET /api/products/delete/<id>
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Delete(int id) {
            ApiKey k = ApiKey.getApiKey();
            Product p = Model.getById<Product>(id);
            if(k.isAdmin() || Supplier.checkUserId(k.user_id, p.supplier_id))  {
                ConvertibleHashtable.fromRequest().toObject<Product>().delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update(int id) {
            ApiKey k = ApiKey.getApiKey();
            ConvertibleHashtable newData = ConvertibleHashtable.fromRequest();
            ConvertibleHashtable currentData = Model.getHashtableById<Product>(id);
            if(k.isAdmin() || Supplier.checkUserId(k.user_id, int.Parse(currentData["supplier_id"].ToString())))  {
                currentData.update(newData);
                currentData.toObject<Product>().update();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }

        // add product
		// POST /api/products/
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Index() {
            ApiKey k = ApiKey.getApiKey();
            ConvertibleHashtable data = ConvertibleHashtable.fromRequest();
            if (k.isAdmin() || (data.ContainsKey("supplier_id") && 
                Supplier.checkUserId(k.user_id, int.Parse(data["supplier_id"].ToString())))) {
                ConvertibleHashtable.fromRequest().toObject<Product>().insert();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
        }

	}
}
