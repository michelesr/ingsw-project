using System;

namespace project.Models {
	public abstract class Model {
		private static readonly Database _db = Database.Istance;
		protected static readonly String[][] _model = {
			new String[] {"name", "VARCHAR"}
		};
		protected static void _add(String tableName, String[][] data) {
			_db.insertData(tableName, data);
		}
		protected static void _initTable(String tableName, String[][] model) {
			_db.createTable(tableName, model);
		}
	}
}

