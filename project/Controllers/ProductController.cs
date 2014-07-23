﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;

namespace project.Controllers {

	// GET: /api/product
	public class ProductController : Controller {

        // GET: /api/product/
        // GET: /api/product/list/
		public ActionResult List () {
			var movies = new List<object>();
			movies.Add(new { id = 1, name = "Saponette profumate" });
			movies.Add(new { id = 2, name = "Pizza Margherita" });
			movies.Add(new { id = 3, name = "Pasta buona" });

			return Json(movies, JsonRequestBehavior.AllowGet); }

        // GET: /api/product/1/detail/
		public ActionResult Detail (int id) {
			var myProduct = new { id = id, name = "Saponette profumate", cat = "Saponi" };

			return Json(myProduct, JsonRequestBehavior.AllowGet); }

		// GET: /api/product/1/create/
		/*
		public ActionResult Create (ProductInputModel inputModel) {
            if (ModelState.IsValid) {
                return Json(new ProductViewModel { Message = "Product successfully created"} );
            }
            else {
                return Json(new ProductViewModel { Message = "Error" } );
            }
        }
		*/

        /*
		public ActionResult ProdCat () {
			return Json(Models.ProductCategory.getAll(), JsonRequestBehavior.AllowGet); }
        */
	}
}