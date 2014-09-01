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

    public class CategoriesController : Controller {

		[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else {
                if (id == -1)
                    return Json(Model.getAll<ProductCategory>(), JsonRequestBehavior.AllowGet);
                else
                    return Detail(id);
            }

		}

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<ProductCategory>(id), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Delete(int id) {
            if(!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                ConvertibleHashtable.fromRequest().toObject<ProductCategory>().delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

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
