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

		[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Index(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else {
                if (id == -1)
                    return Json(Model.getAll<City>(), JsonRequestBehavior.AllowGet);
                else
                    return Detail(id);
            }

		}

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Detail(int id) {
            if (!ApiKey.isRegistered())
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else
                return Json(Model.getHashtableById<City>(id), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Delete(int id) {
            if (!ApiKey.getApiKey().isAdmin()) 
                return Json(Costants.UNAUTHORIZED, JsonRequestBehavior.AllowGet);
            else  {
                ConvertibleHashtable.fromRequest().toObject<City>().delete();
                return Json(Costants.OK, JsonRequestBehavior.AllowGet);
            }
        }

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
