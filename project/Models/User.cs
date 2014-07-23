using System;

namespace project.Models {
	public abstract class User : Model {
		protected static readonly String _tableName = "User";
		new protected static readonly String[][] _model = {
			new String[] {"email", "VARCHAR"},
			new String[] {"password", "VARCHAR", "NOT NULL"},
			new String[] {"first_name", "VARCHAR", "NOT NULL"},
			new String[] {"last_name", "VARCHAR", "NOT NULL"}
		};
		protected static void _add(String[][] data) {
			_add(_tableName, data);
		}
		public static void initTable() {
			_initTable(_tableName, _model);
		}
	}
}
