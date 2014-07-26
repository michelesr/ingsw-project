using System;
using project.Tools;

namespace project.Models {
	public class Supplier : User {
		new private static readonly String[][] _model = new string[][] {
			new String[] {"vat", "VARCHAR", "NOT NULL"},
			new String[] {"supplier_name", "VARCHAR", "NOT NULL"},
			new String[] {"city", "VARCHAR"},
			new String[] {"user_id", "INTEGER", "NOT NULL", Database.getForeignKeyOption("user_id", "User", "id")},
		};
		public String vat {get; set;}
		public String supplier_name {get; set;}
		public String city {get; set;}

		public Supplier(String email, String password, String first_name, String last_name, String vat, String supplier_name, String city) :
		base (email, password, first_name, last_name) { 
			_tableName = "Supplier";
			this.vat = vat;
			this.supplier_name = supplier_name;
			this.city = city;
		}
	}
}

