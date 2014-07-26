using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using project.Tools;

namespace project.Models {
	public class Admin : User {
		public int user_id { get; set; }
		new private static readonly String[][] _model = {
			new String[] {"user_id", "INTEGER", "NOT NULL", Database.getForeignKeyOption("user_id", "User", "id")},
		};

		public Admin(String email, String password, String first_name, String last_name) : base(email, password, first_name, last_name) {
			_tableName = "Admin";
		}

		public override void insert() {
			new User(email, password, first_name, last_name).insert();
			// acquisisce l'id nella tabella user 
			user_id = int.Parse(_db.getData("User", "email", email, new String[] {"id"})[0]["id"].ToString());
			ConvertibleHashtable h = this._toConvertibleHashtable();
			// rimuove i campi relativi alla tabella user
			for(int i = 0; i < User._model.Length; i++)
				h.Remove(User._model[i][0]);
			_db.insertData(_tableName, h);
		}

		new public static void initTable() {
			_initTable("Admin", _model);
		}
	}
}

