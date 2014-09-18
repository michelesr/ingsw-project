using System;
using project.Utils;

namespace project.Models {
    /// Utente con privilegi di amministratore
    // ATTENZIONE: questa classe, sebbene non abbia caratteristiche aggiuntive rispetto
    // all'utenza di base, viene comunque inserita in caso si vogliano 
    // aggiungere altre feature in seguito, ed ha la sua tabella nel db
	public class Admin : User {

        // ATTENZIONE: vengono ereditati id da Model, e user_id da User
        // id fa riferimento all'id nella tabella admin, user_id all'id nella tabella User

        /// Inizializza una nuova istanza della classe Admin
		public Admin(String email, String password, String first_name, String last_name) : base(email, password, first_name, last_name) {}

        /// Registra il tipo di utenza
		protected override void _setUserType(User u) {
			u.type = userType.admin;
		}

        /// Ritorna la ConvertibleHashtable dell'Admin relativo all'user_id fornito
        public static ConvertibleHashtable getHashtableByUserId(int user_id)  {
            return _getAdminOrSupplierHashtableByUserId<Admin>(user_id);
        }

        /// Ritorna l'istanza dell'Admin relativo all'user_id fornito
        public static Admin getByUserId(int user_id) {
            return _getAdminOrSupplierByUserId<Admin>(user_id);
        }
	}
}
