using System;
using System.Collections;

namespace project.Models {
	public class Supplier : User {
		new private static readonly String _tableName = "Supplier";
		new private static readonly String[][] _model = new string[][] {
			new String[] {"vat", "VARCHAR", "NOT NULL"},
			new String[] {"supplier_name", "VARCHAR", "NOT NULL"},
			new String[] {"city", "VARCHAR"},
			new String[] {"user_id", "INTEGER", "NOT NULL", Database.getForeignKeyOption("user_id", User._tableName, "id")},
		};
		new public static void initTable() {
			_initTable(_tableName, _model);
		}
		public static void add(Hashtable userData, Hashtable data) {
			_add(userData, data, _tableName);
		}
	}
}

