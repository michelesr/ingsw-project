using System;
using project.Utils;

namespace project.Models {
	public class Admin : User {
		public Admin(String email, String password, String first_name, String last_name) : base(email, password, first_name, last_name) {}
	}
}
