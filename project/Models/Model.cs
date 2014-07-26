using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using project.Tools;

namespace project.Models {
	public abstract class Model {
		protected static readonly Database _db = Database.Istance;
		protected String _tableName;
		public int id { get; set; }

		protected ConvertibleHashtable _toConvertibleHashtable() {
			return JObject.FromObject(this).ToObject<ConvertibleHashtable>();
		}

		public virtual void insert() {
			_db.insertData(_tableName, this._toConvertibleHashtable());
		}

		public static T getById<T> (int id) {
			String tableName = typeof(T).ToString().Split('.').Last();
			return _db.getData(tableName, "id", id.ToString())[0].toObject<T>();
		}

		protected static readonly String[][] _model = {
			new String[] {"name", "VARCHAR"}
		};

		protected static void _initTable(String tableName, String[][] model) {
			_db.createTable(tableName, model);
		}
	}
}

