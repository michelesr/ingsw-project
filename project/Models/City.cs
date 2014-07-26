using System;
using System.Collections;

namespace project.Models
{
	public class City : Model
	{
		new private static readonly String _tableName = "City";
		public static void initTable() {
			_initTable(_tableName, _model);
		}
	}
}

