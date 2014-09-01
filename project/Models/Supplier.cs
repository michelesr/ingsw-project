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

        public static int getUserIdBySupplierId(int supplier_id) {
            return getById<Supplier>(supplier_id).user_id;
        }

        // check if user_id provided match with the supplier id provided
        public static bool checkUserId(int uid, int sid) {
            return getUserIdBySupplierId(sid) == uid;
        }
	}
}

