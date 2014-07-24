using System;
using System.Collections;

namespace project.Models {
	public abstract class Model {
		private static readonly Database _db = Database.Istance;
		protected static readonly String[][] _model = {
			new String[] {"name", "VARCHAR"}
		};
		protected static void _add(String tableName, Hashtable data) {
			_db.insertData(tableName, data);
		}
		protected static void _initTable(String tableName, String[][] model) {
			_db.createTable(tableName, model);
		}
		protected static Hashtable[] _getAll(String tableName) {
			return _db.getData(tableName);
		}
		protected static Hashtable _getById(String tableName, int id) {
			return _db.getData(tableName, "id", id.ToString())[0];
		}
	}
}

