using System;
using System.Web;

namespace project.Models {
    /// Chiave di accesso all'api dell'applicazione server
	public class ApiKey : Model {

        /// Valore della chiave (hash md5)
        public String key {get; set;}  
        /// L'user_id del proprietario della chiave
		public int user_id {get; set;}

        /// Tipo di utenza del proprietario
        private userType _utype { 
			get {
                if (Model.getById<Admin>(user_id).user_id == user_id)
                    return userType.admin;
                else if (Model.getById<Supplier>(user_id).user_id == user_id)
                    return userType.supplier;
                else
                    return userType.undefined;
			}
		}

        /// Costruttore, calcola l'hash md5 e genera la chiave
        public ApiKey(int uid, string email, string password) {
            user_id = uid;
            key = Utils.Hashing.CalculateMD5Hash(email + password);
        }

        /// Aggiorna la chiave, utile quando si cambia la mail o la password
        public override void update() {
            User u = Model.getById<User>(user_id);
            key = Utils.Hashing.CalculateMD5Hash(u.email + u.password);
            base.update();
        }

        /// Ritorna l'istanza della chiave relativa all'hash fornito
		public static ApiKey getApiKey(String k) {
			return _db.getData("ApiKey", "key", k)[0].toObject<ApiKey>();
		}

        /// Ritorna l'istanza della chiave relativa all'user_id fornito
        public static ApiKey fromUserId(int uid) {
            return _db.getData("ApiKey", "user_id", uid.ToString())[0].toObject<ApiKey>();
        }

        /// Ritorna l'istanza della chiave relativa all'hash fornito nell'header "api_key" della richiesta http
		public static ApiKey getApiKey() {
			return getApiKey(HttpContext.Current.Request.Headers["api_key"]);
		}

        /// Restituisce true <=> la chiave è di un utente amministratore
		public bool isAdmin() {
            return this.user_id != 0 && this._utype == userType.admin;
		}

        /// Restituisce true <=> la chiave appartiene all'User relativo all'id fornito
		public bool checkUser(int id) {
			return this.user_id == id;
		}

        /// Restituisce vero <=> la chiave appartiene a un'utenza registrata nell'applicazione
        public static bool isRegistered() {
            /* Per la logica del serializzatore JSON e di sqlite, il valore di default
               per la serializzazione è sempre 0, ma gli id iniziano sempre da 1
               per cui l'id == 0 è relativo a un utente non registrato */
            return getApiKey().user_id != 0;
        }

	}
}
