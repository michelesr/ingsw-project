using System;
using System.Collections;

namespace project.Models {
	public class User : Model {
		public String email {get; set;}
		public String password {get; set;}
		public String first_name {get; set;}
		public String last_name {get; set;}

		new protected static readonly String[][] _model = {
			new String[] {"email", "VARCHAR", "UNIQUE NOT NULL"},
			new String[] {"password", "VARCHAR", "NOT NULL"},
			new String[] {"first_name", "VARCHAR", "NOT NULL"},
			new String[] {"last_name", "VARCHAR", "NOT NULL"}
		};

		public User(String email, String password, String first_name, String last_name) : base() {
			this._tableName = "User";
			this.email = email;
			this.password = password;
			this.first_name = first_name;
			this.last_name = last_name;
		}

		public static void initTable() {
			_initTable("User", _model);
		}
	}
}
