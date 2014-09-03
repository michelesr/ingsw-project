using System;
using project.Utils;

namespace project.Models {
    // classe rappresentante il produttore
	public class Supplier : User {

        // ATTENZIONE: vengono ereditati id da Model, e user_id da User
        // id fa riferimento all'id nella tabella supplier, user_id all'id nella tabella User

        // informazioni personali
		public String vat {get; set;}
		public String supplier_name {get; set;}
        public int city {get; set;}

        // costruttore di base
        public Supplier(String email, String password, String first_name, String last_name, String vat, String supplier_name, int city) :
		base (email, password, first_name, last_name) { 
			this.vat = vat;
			this.supplier_name = supplier_name;
			this.city = city;
		}

        // metodo per registrare il tipo di utenza
		protected override void _setUserType(User u) {
			u.type = userType.supplier;
		}

        // ritorna l'hashtable in base all'user_id
        public static ConvertibleHashtable getHashtableByUserId(int user_id)  {
            return _getAdminOrSupplierHashtableByUserId<Supplier>(user_id);
        }

        // ritorna il supplier relativo all'user_id
        public static Supplier getByUserId(int user_id) {
            return _getAdminOrSupplierByUserId<Supplier>(user_id);
        }

        // ritorna l'user_id a partire dal supplier_id
        public static int getUserIdBySupplierId(int supplier_id) {
            return getById<Supplier>(supplier_id).user_id;
        }

        // controlla se esiste la relazione tra user_id e supplier_id forniti
        public static bool checkUserId(int uid, int sid) {
            return getUserIdBySupplierId(sid) == uid;
        }
	}
}

