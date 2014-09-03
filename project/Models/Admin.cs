using System;
using project.Utils;

namespace project.Models {
    // classe rappresentante gli amministratori
    // ATTENZIONE: questa classe, sebbene non abbia caratteristiche aggiuntive rispetto
    // all'utenza di base, viene comunque inserita in caso si vogliano 
    // aggiungere altre feature in seguito, ed ha la sua tabella nel db
	public class Admin : User {

        // ATTENZIONE: vengono ereditati id da Model, e user_id da User
        // id fa riferimento all'id nella tabella admin, user_id all'id nella tabella User

        // costruttore base
		public Admin(String email, String password, String first_name, String last_name) : base(email, password, first_name, last_name) {}

        // registra il tipo di utenza
		protected override void _setUserType(User u) {
			u.type = userType.admin;
		}

        // ritorna l'hashtable dell'Admin a partire dall'user_id
        public static ConvertibleHashtable getHashtableByUserId(int user_id)  {
            return _getAdminOrSupplierHashtableByUserId<Admin>(user_id);
        }

        // ritorna l'Admin a partire dall'user_id
        public static Admin getByUserId(int user_id) {
            return _getAdminOrSupplierByUserId<Admin>(user_id);
        }
	}
}
