using System;

namespace project.Models {
	public class ProductCategory : Model {
	    private static readonly String _tableName = "ProductCategory";
		public static void add(String[][] data) {
			_add(_tableName, data);
		}
		public static void initTable() {
			_initTable(_tableName, _model);
		}
	}
}
