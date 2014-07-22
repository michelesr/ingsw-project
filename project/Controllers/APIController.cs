using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;

namespace project.Controllers
{
	public class APIController : Controller
	{
		public ActionResult Index () {

			var movies = new List<object>();
			movies.Add(new { Title = "Ghostbusters", Genre = "Comedy", Year = 1984 });
			movies.Add(new { Title = "Gone with Wind", Genre = "Drama", Year = 1939 });
			movies.Add(new { Title = "Star Wars", Genre = "Science Fiction", Year = 1977 });

			return Json(movies, JsonRequestBehavior.AllowGet); }

		public ActionResult Product_List () {

			var movies = new List<object>();
			movies.Add(new { id = "1", name = "Ghostbusters", Genre = "Comedy", Year = 1984 });
			movies.Add(new { id = "2", name = "Gone with Wind", Genre = "Drama", Year = 1939 });
			movies.Add(new { id = "3", name = "Star Wars", Genre = "Science Fiction", Year = 1977 });

			return Json(movies, JsonRequestBehavior.AllowGet); }

		public string Users () {
			return "users examples"; }

		//public ActionResult ProdCat () {
			//return Json(Models.ProductCategory.getAll(), JsonRequestBehavior.AllowGet); }

	}
}
