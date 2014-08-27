using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Utils;
using project.Models;
using Newtonsoft.Json.Linq;

namespace project.Controllers {
	public class AuthController : Controller {

		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Index(int id, String data) {
            ConvertibleHashtable d = JObject.Parse(new StreamReader(Request.InputStream).ReadLine()).ToObject<ConvertibleHashtable>();
			ConvertibleHashtable h = new ConvertibleHashtable();
			h.Add("auth", Models.User.checkPassword(d["email"].ToString(), d["password"].ToString()));
			if((bool) h["auth"]) {
				h.Add("user_id", Models.User.getUserHashtableByEmail(d["email"].ToString())["id"]);
                // generare la chiave, inserirla nel db e aggiungerla ai dati
				ApiKey ak = new ApiKey (int.Parse(h["user_id"].ToString()), d["email"].ToString(), d["password"].ToString());
                if (!(ApiKey.getApiKey(ak.key).key == ak.key))
				    ak.insert();
				h.Add("api_key", ak.key);
			}
			return Json(h, JsonRequestBehavior.AllowGet);
		}
	}
}

