﻿using System;
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

        [AcceptVerbs(HttpVerbs.Get)]

        // delete product
        // GET /api/products/delete/<id>
        public JsonResult Delete(int id) {
            if (!ApiKey.isRegistered()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                ConvertibleHashtable.fromRequest().toObject<Product>().delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Update(int id) {
            if (!ApiKey.isRegistered()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                ConvertibleHashtable newData = ConvertibleHashtable.fromRequest();
                ConvertibleHashtable currentData = Model.getHashtableById<Product>(id);
                currentData.update(newData);
                currentData.toObject<Product>().update();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

        // add product
		// POST /api/products/
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Index() {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else {
                ConvertibleHashtable.fromRequest().toObject<Product>().insert();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

	}
}
