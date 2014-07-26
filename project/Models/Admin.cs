using System;
using project.Tools;

namespace project.Models {
	public class Admin : User {
		new private static readonly String[][] _model = {
			new String[] {"user_id", "INTEGER", "NOT NULL", Database.getForeignKeyOption("user_id", "User", "id")},
		};
		public Admin(String email, String password, String first_name, String last_name) : base(email, password, first_name, last_name) {
			_tableName = "Admin";
		}
	}
}
