using System;

namespace project.Utils
{
	public static class Schema {
		private static Database _db = Database.Istance;
		private static String[] _tables = new String[] {"ApiKey", "User", "Admin", "Supplier"};

		private static readonly String[][] _apiKey = {
			new String[] { "user_id", "INTEGER", "NOT NULL", _getFK ("user_id", "User", "id") },
			new String[] { "key", "VARCHAR", "UNIQUE NOT NULL" }
		};

		private static readonly String[][] _user = {
			new String[] {"email", "VARCHAR", "UNIQUE NOT NULL"},
			new String[] {"password", "VARCHAR", "NOT NULL"},
			new String[] {"first_name", "VARCHAR", "NOT NULL"},
			new String[] {"last_name", "VARCHAR", "NOT NULL"}
		};

		private static readonly String[][] _supplier = new string[][] {
			new String[] {"vat", "VARCHAR", "NOT NULL"},
			new String[] {"supplier_name", "VARCHAR", "NOT NULL"},
			new String[] {"city", "VARCHAR"},
			new String[] {"user_id", "INTEGER", "NOT NULL", _getFK("user_id", "User", "id")},
		};

		private static readonly String[][] _admin = {
			new String[] {"user_id", "INTEGER", "NOT NULL", _getFK("user_id", "User", "id")},
		};

		private static String _getFK(String localField, String foreignTable, String foreignField) {
			return ", FOREIGN KEY(`" + localField + "`) REFERENCES `" + foreignTable + "`(`" + foreignField + "`)"; 
		}

		private static String[][][] _models = new String[][][] { _apiKey, _user, _admin, _supplier };

		public static void createSchema() {
			for(int i = 0; i < _tables.Length; i++) 
				_db.createTable(_tables[i], _models[i]);
		}
	}
}

