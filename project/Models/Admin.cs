using System;
using project.Utils;

namespace project.Models {
	public class Admin : User {
		public Admin(String email, String password, String first_name, String last_name) : base(email, password, first_name, last_name) {}

		protected override void _setUserType(User u) {
			u.type = userType.admin;
		}

        public static Admin getByUserId(int user_id) {
            return _getAdminOrSupplierByUserId<Admin>(user_id);
        }
	}
}
