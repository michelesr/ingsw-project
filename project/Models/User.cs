using System;
using System.Collections;

namespace project.Models {
	public abstract class User : Model {
		protected static readonly String _tableName = "User";
		new protected static readonly String[][] _model = {
			new String[] {"email", "VARCHAR", "UNIQUE NOT NULL"},
			new String[] {"password", "VARCHAR", "NOT NULL"},
			new String[] {"first_name", "VARCHAR", "NOT NULL"},
			new String[] {"last_name", "VARCHAR", "NOT NULL"}
		};
		protected static void _add(String[][] userData, String[][] data, String tableName) {
			_add(_tableName, userData);
			// trova la mail dell'user
			int i; for (i = 0; userData[i][0] != "email" ; i++);
			// trova l'id dell'user
			String id = Database.Istance.getData(_tableName, "email" , userData[i][1], new String[] {"id"})[0]["id"].ToString();
			// aggiunge l'id nei dati 
			String[][] newData = new String [data.Length+1][];
			data.CopyTo(newData, 0);
			new String[][] {
				new String[] {"user_id", id}
			}.CopyTo(newData, data.Length);
			// chiama add della classe Model per aggiungere l'user nella sua tabella di appartenenza 
			_add(tableName, newData);
		}
		public static void initTable() {
			_initTable(_tableName, _model);
		}

		public static Hashtable[] getAll() {
			return _getAll(_tableName);
		}

		public static Hashtable getById(int id) {
			return _getById(_tableName, id);
		}
	}
}
