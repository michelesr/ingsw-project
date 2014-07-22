using System;
using System.Reflection;
using System.Collections;

namespace project.Models {
	public abstract class Model {
		private static readonly Database db = Database.Istance;
		protected static readonly String[][] model = {
			new String[] {"name", "VARCHAR"}
		};
		protected static void add(String tableName, String[][] data) {
			db.insertData(tableName, data);
		}
		protected static void initTable(String tableName, String[][] model) {
			db.createTable(tableName, model);
		}
	}
}

