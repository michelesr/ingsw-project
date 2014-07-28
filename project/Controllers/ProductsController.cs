using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project.Controllers {

	public class ProductsController : Controller {

		// GET /api/products/list
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult List () {
			var movies = new List<object>();
			movies.Add(new { id = 1, name = "Saponette profumate" });
			movies.Add(new { id = 2, name = "Pizza Margherita" });
			movies.Add(new { id = 3, name = "Pasta buona" });

			return Json(movies, JsonRequestBehavior.AllowGet);
		}

		// POST /api/products/add
		[HttpPost]
		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Add (String data) {

			return Json(data);
        }

		// GET /api/products/1/detail
		[AcceptVerbs(HttpVerbs.Get)]
		public JsonResult Detail (int id) {
			var myProduct = new { id = id, name = "Saponette profumate", cat = "Saponi" };

			return Json(myProduct, JsonRequestBehavior.AllowGet);
		}

	}
}
