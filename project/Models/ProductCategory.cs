using System;

namespace project.Models {
	public class ProductCategory {
		public static readonly String[][] model = {
			new String[] {"nome", "VARCHAR"}
		};
		public static void add(String name) {
			String[][] data = {
				new String[] {name}
			};
			Database.Istance.insertData("ProductCategory", data);
		}
	}
}
