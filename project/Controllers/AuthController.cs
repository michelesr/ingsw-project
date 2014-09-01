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
		public JsonResult Index() {
            ConvertibleHashtable d = ConvertibleHashtable.fromRequest(); 
			ConvertibleHashtable h = new ConvertibleHashtable();
			h.Add("auth", Models.User.checkPassword(d["email"].ToString(), d["password"].ToString()));
			if((bool) h["auth"]) {
                int user_id = int.Parse(Models.User.getUserHashtableByEmail(d["email"].ToString())["id"].ToString()); 
                h.Add("user_id", user_id);
                // calcolare la chiave, inserirla nel db se non esiste già e aggiungerla ai dati da ritornare
				ApiKey ak = new ApiKey (int.Parse(h["user_id"].ToString()), d["email"].ToString(), d["password"].ToString());
                if (!(ApiKey.getApiKey(ak.key).key == ak.key)) {
                    ak.insert();
                }
                else {
                    // chiude la sessione precedentemente aperta
                    Models.Session.CloseSession(user_id);
                }
                // apre una nuova sessione
                Models.Session.OpenSession(user_id);
				h.Add("api_key", ak.key);
			}
			return Json(h, JsonRequestBehavior.AllowGet);
		}

        public JsonResult Logout() {
            ApiKey k = ApiKey.getApiKey();
            Models.Session.CloseSession(k.user_id);
            k.delete();
            return Json(Utils.Costants.OK, JsonRequestBehavior.AllowGet);
        }
	}
}