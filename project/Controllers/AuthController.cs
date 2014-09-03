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
    // controller per l'autorizzazione degli utenti
	public class AuthController : Controller {

        // POST /api/auth/
        // {email: "email", password: "password"}
        // autentica l'utente e ritorna l'api_key
		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Index() {
            ConvertibleHashtable d = ConvertibleHashtable.fromRequest(); 
			ConvertibleHashtable h = new ConvertibleHashtable();

            // autentica l'utente, tramite il metodo checkPassword di User
			h.Add("auth", Models.User.checkPassword(d["email"].ToString(), d["password"].ToString()));

            // se l'autenticazione va a buon fine
			if((bool) h["auth"]) {
                // trova l'user_id e l'aggiunge ai dati da ritornare
                int user_id = int.Parse(Models.User.getUserHashtableByEmail(d["email"].ToString())["id"].ToString()); 
                h.Add("user_id", user_id);

                // calcola la chiave, l'inserisce nel db se non esiste già e l'aggiunge ai dati da ritornare
				ApiKey ak = new ApiKey (int.Parse(h["user_id"].ToString()), d["email"].ToString(), d["password"].ToString());

                if (!(ApiKey.getApiKey(ak.key).key == ak.key)) 
                    ak.insert();
                else 
                    Models.Session.CloseSession(user_id); // chiude la sessione se è aperta

                Models.Session.OpenSession(user_id); // apre una nuova sessione
                h.Add("api_key", ak.key);  
			}

			return Json(h, JsonRequestBehavior.AllowGet);
		}

        // GET /api/auth/logout/
        // necessita dell'api_key nell'header della richiesta http
        // chiude la sessione dell'utente
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Logout() {
            ApiKey k = ApiKey.getApiKey();
            Models.Session.CloseSession(k.user_id);
            k.delete();
            return Json(Utils.Costants.OK, JsonRequestBehavior.AllowGet);
        }
	}
}