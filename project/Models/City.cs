using System;

namespace project.Models
{
	public class City : Model
	{
		private static readonly String tableName = "City";
		public static void add(String[][] data) {
			add(tableName, data);
		}
		public static void initTable() {
			initTable(tableName, model);
		}
	}
}

