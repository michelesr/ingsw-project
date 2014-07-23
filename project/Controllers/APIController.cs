using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;

namespace project.Controllers {

	public class APIController : Controller {

		public ActionResult Index () {
			var movies = new List<object>();
			movies.Add(new { Title = "Ghostbusters", Genre = "Comedy", Year = 1984 });
			movies.Add(new { Title = "Gone with Wind", Genre = "Drama", Year = 1939 });
			movies.Add(new { Title = "Star Wars", Genre = "Science Fiction", Year = 1977 });

			return Json(movies, JsonRequestBehavior.AllowGet); }

		public ActionResult Product_List () {
			var movies = new List<object>();
			movies.Add(new { id = 1, name = "Saponette profumate" });
			movies.Add(new { id = 2, name = "Pizza Margherita" });
			movies.Add(new { id = 3, name = "Pasta buona" });

			return Json(movies, JsonRequestBehavior.AllowGet); }

		public ActionResult Product_Detail () {
			var product = new { id = 1, name = "Saponette profumate", cat = "Saponi" };

			return Json(product, JsonRequestBehavior.AllowGet); }

		//public ActionResult ProdCat () {
			//return Json(Models.ProductCategory.getAll(), JsonRequestBehavior.AllowGet); }

	}
}
