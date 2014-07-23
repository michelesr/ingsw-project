using System;

namespace project.Models
{
	public class City : Model
	{
		private static readonly String _tableName = "City";
		public static void add(String[][] data) {
			_add(_tableName, data);
		}
		public static void initTable() {
			_initTable(_tableName, _model);
		}
	}
}

