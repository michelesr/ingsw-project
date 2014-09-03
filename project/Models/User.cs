using System;
using project.Utils;
using Newtonsoft.Json;

namespace project.Models {

    // enumerazione rappresentante i tipi di utenza
	public enum userType {supplier, admin, undefined};

    // classe che rappresenta l'utenza generica
	public class User : Model {

        // informazioni personali
		public String email {get; set;}
		public String password {get; set;}
		public String first_name {get; set;}
		public String last_name {get; set;}
        // id dell'user (identico al campo id per questo modello)
		public int user_id {get; set;}
        // tipo d'utenza
		public userType type = userType.undefined;

        // costruttore
		public User(String email, String password, String first_name, String last_name) : this() {
			this.email = email;
			this.password = password;
			this.first_name = first_name;
			this.last_name = last_name;
		}

        // costruttore vuoto (utile al serializzatore/deserializzatore JSON)
        public User() : base () {}

        // costruttore (solo id), utile per cancellazioni dell'utente dal db
        // (per cancellare l'utente serve solo il suo id)
        // usato nelle operazioni interne, ie. delete()
		protected User(int id) : this() {
			this.id = id;
		}

        // scrive nel db gli attributi modificati
        // viene richiamato anche il metodo base perché vengono aggiornati 2 record
        // da 2 tabelle (User, (Admin | Supplier))
		public override void update () {
		    base.update();

			if (this is Supplier || this is Admin) {
				User u = new User(email, password, first_name, last_name);
				u.id = user_id;
				_setUserType(u);
				u.update();
			}
		}

        // cancella l'utente dal db
        // viene richiamato anche il metodo base perché vengono cancellati 2 record
        // da 2 tabelle (User, (Admin | Supplier))
		public override void delete() {
			base.delete();

			if (this is Supplier || this is Admin)
				new User(user_id).delete();
		}

        // inserisce l'utente nel db
        // viene richiamato anche il metodo base perché vengono inseriti 2 record
        // in 2 tabelle (User, (Admin | Supplier))
		public override void insert() {

			if ((this is Supplier || this is Admin)) {
				User u = new User(email, password, first_name, last_name);
				_setUserType(u);
				u.insert();
				user_id = u.id;
			} 

			base.insert();
		}

        // ritorna un User a partire dalla sua email
		public static User getUserByEmail(String email) {
			return getUserHashtableByEmail(email).toObject<User>();
		}

        // ritorna l'hashtable di un User a partire dalla sua email
		public static ConvertibleHashtable getUserHashtableByEmail(String email) {
			return _db.getData("User", "email", email)[0];
		}

        // controlla se l'email e la password corrispondono a un utente esistente
		public static bool checkPassword(String email, String password) {
			bool x = false;
			ConvertibleHashtable h = getUserHashtableByEmail(email); 

			if (h.ContainsKey("password")) 
				x = h["password"].ToString() == password;

			return x;
		}
            
        // questo metodo verrà eriditato da Admin e Supplier, qui è stato inserito solo per
        // poterlo utilizzare in maniera astratta nei metodi insert e update
		protected virtual void _setUserType(User u) {
			u.type = userType.undefined;
		}

        // come sopra ma ritorna l'hashtable invece
        protected static T _getAdminOrSupplierByUserId<T>(int user_id) {
            return _getAdminOrSupplierHashtableByUserId<T>(user_id).toObject<T>();
        }

        // questo metodo ritorna un Admin o un Supplier avente l'user_id specificato
        // è stato inserito qui per non ripeterlo 2 volte in Admin e User, che lo richiamano
        // passandogli il tipo esatto di utenza
        protected static ConvertibleHashtable _getAdminOrSupplierHashtableByUserId<T>(int user_id) {
            ConvertibleHashtable userData = getHashtableById<User>(user_id);
            ConvertibleHashtable extraData = _db.getData(_getTableName<T>(), "user_id", user_id.ToString())[0];

            extraData.merge(userData);
            return extraData;
        }
	}
}
