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
			get { return _getTableNameByType(this.GetType()); }
		}
		public int id { get; set; }

		private ConvertibleHashtable _toConvertibleHashtable() {
			return JObject.FromObject(this).ToObject<ConvertibleHashtable>();
		}

		public virtual void insert() {
			id = _db.insertData(_tableName, this._toConvertibleHashtable());
			Console.WriteLine(id);
		}

		public virtual void update() {
			ConvertibleHashtable old = _getHashtableById(id, _tableName);
			ConvertibleHashtable current = this._toConvertibleHashtable();
			foreach(var k in old.Keys) 
				if (old[k].ToString() != current[k].ToString())
					_db.updateData(_tableName, k.ToString(), current[k].ToString(), "id", id.ToString());
		}

		public virtual void delete() {
			_db.deleteData(_tableName, "id", id.ToString());
		}

		public static ConvertibleHashtable getHashtableById<T>(int id) {
			return _getHashtableById(id, _getTableNameByType(typeof(T)));
		}

		private static ConvertibleHashtable _getHashtableById(int id, String tableName) {
			return _db.getData(tableName, "id", id.ToString())[0];
		}

		public static T getById<T> (int id) {
			return _getHashtableById(id, _getTableNameByType(typeof(T))).toObject<T>();
		}

		public static ConvertibleHashtable[] getAll<T>() {
			return _db.getData(_getTableName<T>());
		}

        protected static String _getTableName<T>() {
			return _getTableNameByType(typeof(T));
		}

		private static String _getTableNameByType(Type type) {
			return type.ToString().Split('.').Last();
		}
	}
}

