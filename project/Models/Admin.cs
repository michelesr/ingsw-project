using System;
using System.Collections;

namespace project.Models {
	public class Admin : User {
		new private static readonly String _tableName = "Admin";
		new private static readonly String[][] _model = {
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

