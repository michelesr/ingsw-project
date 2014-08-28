using System;
using project.Utils;

namespace project.Models {
	public class Supplier : User {
		public String vat {get; set;}
		public String supplier_name {get; set;}
		public String city {get; set;}

		public Supplier(String email, String password, String first_name, String last_name, String vat, String supplier_name, String city) :
		base (email, password, first_name, last_name) { 
			this.vat = vat;
			this.supplier_name = supplier_name;
			this.city = city;
		}
		protected override void _setUserType(User u) {
			u.type = userType.supplier;
		}

        public static ConvertibleHashtable getHashtableByUserId(int user_id)  {
            return _getAdminOrSupplierHashtableByUserId<Supplier>(user_id);
        }

        public static Supplier getByUserId(int user_id) {
            return _getAdminOrSupplierByUserId<Supplier>(user_id);
        }
	}
}

