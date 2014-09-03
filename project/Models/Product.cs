using System;

namespace project.Models {
    // classe che rappresenta i prodotti
    public class Product : Model {
        // id del supplier e della categoria del prodotto
        public int supplier_id { get; set; }
        public int product_category { get; set; }
        // nome
        public String name { get; set; }

        // controlla che l'user_id fornito sia relativo al produttore del prodotto
        public bool checkUserId(int uid) {
            return uid == Supplier.getUserIdBySupplierId(this.supplier_id);
        }

	}
}
