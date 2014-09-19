using System;
using project.Utils;
using Newtonsoft.Json;

namespace project.Models {

    /// Tipi di utenza
	public enum userType {supplier, admin, undefined};

    /// Utente generico
	public class User : Model {

        /// Indirizzo di posta elettronica dell'utente
		public String email {get; set;}
        /// Password dell'utente
		public String password {get; set;}
        /// Nome dell'utente
		public String first_name {get; set;}
        /// Cognome dell'utente
		public String last_name {get; set;}
        /** Id dell'user, viene ereditato nelle sottoclassi e serve
            per tenere tracca dell'id dell'user nella tabella User, che
            differisce dall'id (ereditato da Model) nelle tabelle Admin|Supplier */
		public int user_id {get; set;}
        /// Tipo d'utenza (viene ereditato e settato correttamente nelle sottoclassi)
		public userType type = userType.undefined;

        /// Costruttore
		public User(String email, String password, String first_name, String last_name) : this() {
			this.email = email;
			this.password = password;
			this.first_name = first_name;
			this.last_name = last_name;
		}

        /// Costruttore vuoto (utile al serializzatore/deserializzatore JSON)
        public User() : base () {}

        /** Costruttore protetto, utile per cancellazioni dell'utente dal db
            (per cancellare l'utente serve solo il suo id),
            usato nelle operazioni interne, ie. delete() */
		protected User(int id) : this() {
			this.id = id;
		}

        /// Aggiorna nel db gli attributi modificati
		public override void update () {
		    base.update();

			if (this is Supplier || this is Admin) {
				User u = new User(email, password, first_name, last_name);
				u.id = user_id;
				_setUserType(u);
				u.update();
			}
		}

        /// Elimina l'utente dal db
		public override void delete() {
			base.delete();

			if (this is Supplier || this is Admin)
				new User(user_id).delete();
		}

        /// Inserisce l'utente nel db
		public override void insert() {

			if ((this is Supplier || this is Admin)) {
				User u = new User(email, password, first_name, last_name);
				_setUserType(u);
				u.insert();
				user_id = u.id;
			} 

			base.insert();
		}

        /// Ritorna l'istanza dell'User relativo all'indirizzo email fornito
		public static User getUserByEmail(String email) {
			return getUserHashtableByEmail(email).toObject<User>();
		}

        /// Ritorna la ConvertibleHashtable dell'utente relativo all'indirizzo email fornito
		public static ConvertibleHashtable getUserHashtableByEmail(String email) {
			return _db.getData("User", "email", email)[0];
		}

        /// Restituisce true <=> l'email e la password corrispondono a un utente esistente 
		public static bool checkPassword(String email, String password) {
			bool x = false;
			ConvertibleHashtable h = getUserHashtableByEmail(email); 
			if (h.ContainsKey("password")) 
				x = h["password"].ToString() == password;

			return x;
		}
            
        /** Questo metodo verrà eriditato da Admin e Supplier, qui è stato inserito solo per
            poterlo utilizzare in maniera virtuale nei metodi insert e update */
		protected virtual void _setUserType(User u) {
			u.type = userType.undefined;
		}

        /// Ritorna l'istanza dell'Admin|Supplier avente l'user_id specificato
        /*  È stato inserito qui per non ripeterlo 2 volte in Admin e User, che lo richiamano
            passandogli il tipo esatto di utenza */
        protected static T _getAdminOrSupplierByUserId<T>(int user_id) {
            return _getAdminOrSupplierHashtableByUserId<T>(user_id).toObject<T>();
        }

        /// Ritorna la ConvertibleHashtable dell'Admin|Supplier avente l'user_id specificato
        protected static ConvertibleHashtable _getAdminOrSupplierHashtableByUserId<T>(int user_id) {
            ConvertibleHashtable userData = getHashtableById<User>(user_id);
            ConvertibleHashtable extraData = _db.getData(_getTableName<T>(), "user_id", user_id.ToString())[0];

            extraData.merge(userData);
            return extraData;
        }
	}
}
