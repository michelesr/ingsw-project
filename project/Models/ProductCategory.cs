using System;
using System.Collections;

namespace project.Models {
	public class ProductCategory : Model {
		new private static readonly String _tableName = "ProductCategory";
		public static void initTable() {
			_initTable(_tableName, _model);
		}
	}
}
