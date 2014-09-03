using System;
using System.Web;

namespace project.Models {
    // classe per la gestione delle chiavi per l'accesso alle api
	public class ApiKey : Model {
        // chiave e utente relativo
		public String key {get; set;}
		public int user_id {get; set;}
        // ritorna il tipo di utenza
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

        // costruttore, calcola l'hash md5 e genera la chiave
        public ApiKey(int uid, string email, string password) {
            user_id = uid;
            key = Utils.Hashing.CalculateMD5Hash(email + password);
        }

        // aggiorna la chiave, utile quando si cambia la mail o la password
        public override void update() {
            User u = Model.getById<User>(user_id);
            key = Utils.Hashing.CalculateMD5Hash(u.email + u.password);
            base.update();
        }

        // ritorna il reference a una chiave a partire dal hash della chiave
		public static ApiKey getApiKey(String k) {
			return _db.getData("ApiKey", "key", k)[0].toObject<ApiKey>();
		}

        // ritorna la chiave a partire dall'user_id
        public static ApiKey fromUserId(int uid) {
            return _db.getData("ApiKey", "user_id", uid.ToString())[0].toObject<ApiKey>();
        }

        // ritorna la chiave a partire dall'hash dell'api_key fornita negli header della richiesta http
		public static ApiKey getApiKey() {
			return getApiKey(HttpContext.Current.Request.Headers["api_key"]);
		}

        // controlla se la chiave è di un amministratore
		public bool isAdmin() {
            return this.user_id != 0 && this._utype == userType.admin;
		}

        // controlla che la chiave sia relativa all'user con l'id fornito
		public bool checkUser(int id) {
			return this.user_id == id;
		}

        // controlla che la chiave appartenga a un utente registrato
        public static bool isRegistered() {
            // logica del serializzatore e del db, il valore di default
            // per la serializzazione è sempre 0, ma gli id iniziano sempre da 1
            // per cui l'id == 0 è relativo a un utente non registrato
            return getApiKey().user_id != 0;
        }

	}
}
