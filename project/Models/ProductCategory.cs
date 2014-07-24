using System;
using System.Collections;

namespace project.Models {
	public class ProductCategory : Model {
	    private static readonly String _tableName = "ProductCategory";
		public static void add(Hashtable data) {
			_add(_tableName, data);
		}
		public static void initTable() {
			_initTable(_tableName, _model);
		}
	}
}
