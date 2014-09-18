using System;

namespace project.Models {
    /// Prodotto
    public class Product : Model {
        /// L'id del supplier nella tabella Supplier
        public int supplier_id { get; set; }
        /// L'id della categoria del prodotto
        public int product_category { get; set; }
        /// Nome del prodotto
        public String name { get; set; }

        /// Restituisce true <=> l'user_id fornito è relativo al produttore del prodotto
        public bool checkUserId(int uid) {
            return uid == Supplier.getUserIdBySupplierId(this.supplier_id);
        }
	}
}
