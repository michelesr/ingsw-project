using System;

namespace project.Models {
    public class Product : Model {
        public int supplier_id { get; set; }
        public int product_category { get; set; }
        public String name { get; set; }

        public bool checkUserId(int uid) {
            return uid == Supplier.getUserIdBySupplierId(this.supplier_id);
        }

	}
}
