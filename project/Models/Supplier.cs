using System;
using System.Collections;
using project.Utils;

namespace project.Models {

    /// Produttore (sottoclasse dell'utente generico)
	public class Supplier : User {

        /* ATTENZIONE: vengono ereditati id da Model, e user_id da User
           id fa riferimento all'id nella tabella supplier, 
           user_id all'id nella tabella User */

        /// Partita IVA
		public String vat {get; set;}

        /// Nome del produttore o dell'azienda produttrice
		public String supplier_name {get; set;}

        /// Città del produttore
        public int city {get; set;}

        /// Costruttore
        public Supplier(String email, String password, String first_name, String last_name, String vat, String supplier_name, int city) :
		base (email, password, first_name, last_name) { 
			this.vat = vat;
			this.supplier_name = supplier_name;
			this.city = city;
		}

        /// Registra il tipo di utenza
		protected override void _setUserType(User u) {
			u.type = userType.supplier;
		}

        /// Ritorna la ConvertibleHashtable del Supplier relativo all'user_id fornito
        public static ConvertibleHashtable getHashtableByUserId(int user_id)  {
            return _getAdminOrSupplierHashtableByUserId<Supplier>(user_id);
        }

        /// Ritorna l'istanza del Supplier relativo all'user_id fornito
        public static Supplier getByUserId(int user_id) {
            return _getAdminOrSupplierByUserId<Supplier>(user_id);
        }

        /// Ritorna l'id nella tabella User del Supplier relativo al supplier_id fornito
        public static int getUserIdBySupplierId(int supplier_id) {
            return getById<Supplier>(supplier_id).user_id;
        }

        /// Restituisce true <=> user_id e supplier_id forniti sono relativi alla stessa utenza
        public static bool checkUserId(int uid, int sid) {
            return getUserIdBySupplierId(sid) == uid;
        }

        /// Ritorna una ArrayList di tutte le ConvertibleHashtable dei Supplier
        public static ArrayList getAll() {
            ConvertibleHashtable[] suppliers = Model.getAll<Supplier>();
            ArrayList result = new ArrayList();

            foreach (ConvertibleHashtable s in suppliers) {
                s.merge(User.getHashtableById<User>(int.Parse(s["user_id"].ToString())));
                result.Add(s);
            }

            return result;
        }
	}
}

