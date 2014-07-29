using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Utils;
using Newtonsoft.Json.Linq;

namespace project.Controllers {
	public class AuthController : Controller {

		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Index(int id, String data) {
			ConvertibleHashtable d = JObject.Parse(data).ToObject<ConvertibleHashtable>(); 
			ConvertibleHashtable h = new ConvertibleHashtable();
			h.Add("auth", Models.User.checkPassword(d["email"].ToString(), d["password"].ToString()));
			if((bool) h["auth"]) {
				h.Add("user", Models.User.getUserHashtableByEmail(d["email"].ToString()).filter("password"));
			}
			return Json(h, JsonRequestBehavior.AllowGet);
		}
	}
}

