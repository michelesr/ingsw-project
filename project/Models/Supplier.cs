using System;

namespace project.Models {
	public class Supplier : User {
		public static readonly String[][] supplierModel = {
			new String[] {"vat", "VARCHAR"},
			new String[] {"supplier_name", "VARCHAR"},
			new String[] {"city", "VARCHAR"},
		};
	}
}

