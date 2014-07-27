using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using project.Utils;

namespace project.Models {
	public abstract class Model {
		protected static readonly Database _db = Database.Istance;
		protected String _tableName {
			get { return this.GetType().ToString().Split('.').Last(); }
		}
		public int id { get; set; }

		protected ConvertibleHashtable _toConvertibleHashtable() {
			return JObject.FromObject(this).ToObject<ConvertibleHashtable>();
		}

		public virtual void insert() {
			id = _db.insertData(_tableName, this._toConvertibleHashtable());
			Console.WriteLine(id);
		}

		public virtual void update() {
			ConvertibleHashtable old = _getById(id);
			ConvertibleHashtable current = this._toConvertibleHashtable();
			foreach(var k in old.Keys) 
				if (old[k].ToString() != current[k].ToString())
					_db.updateData(_tableName, k.ToString(), current[k].ToString(), "id", id.ToString());
		}

		public virtual void delete() {
			_db.deleteData(_tableName, "id", id.ToString());
		}

		private ConvertibleHashtable _getById(int id) {
			return _db.getData(_tableName, "id", id.ToString())[0];
		}

		public static T getById<T> (int id) {
			return _db.getData(_getTableName<T>(), "id", id.ToString())[0].toObject<T>();
		}

		public static Hashtable[] getAll<T>() {
			return _db.getData(_getTableName<T>());
		}

		protected static readonly String[][] _model = {
			new String[] {"name", "VARCHAR"}
		};

		public static void createSchema() {
			String[] tables = new String[] {"Admin", "User", "Supplier"};
			String[][][] models = new String[][][] { Admin._model, User._model, Supplier._model };
			for(int i = 0; i < tables.Length; i++) 
				_initTable(tables[i], models[i]);
		}

		private static void _initTable(String tableName, String[][] model) {
			_db.createTable(tableName, model);
		}

		private static String _getTableName<T>() {
			return typeof(T).ToString().Split('.').Last();
		}
	}
}

