using System;

namespace project.Models {
	public class ProductCategory : Model {
		protected static readonly String tableName = "ProductCategory";
		public static void add(String[][] data) {
			add(tableName, data);
		}
		public static void initTable() {
			initTable(tableName, model);
		}
	}
}
