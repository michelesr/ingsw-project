using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using project.Utils;

namespace project.Models {
    // classe padre di tutti i modelli del database, contiene la logica dell'interazione con il db
	public abstract class Model {
        // istanza del db
		protected static readonly Database _db = Database.Istance;
        // id del record
        public int id { get; set; }
        // ritorna il nome della tabella a partire dal tipo di dato dell'oggetto

		protected String _tableName {
			get { return _getTableNameByType(this.GetType()); }
		}

        // trasforma l'oggetto in una hashtable contenente i suoi attributi 
        // in forma chiave:valore -> attributo:valore
		private ConvertibleHashtable _toConvertibleHashtable() {
			return JObject.FromObject(this).ToObject<ConvertibleHashtable>();
		}

        // inserisce il record nel db
		public virtual void insert() {
			id = _db.insertData(_tableName, this._toConvertibleHashtable());
			Console.WriteLine(id);
		}

        // aggiorna il record nel db
		public virtual void update() {
			ConvertibleHashtable old = _getHashtableById(id, _tableName);
			ConvertibleHashtable current = this._toConvertibleHashtable();

			foreach(var k in old.Keys) 
				if (old[k].ToString() != current[k].ToString())
					_db.updateData(_tableName, k.ToString(), current[k].ToString(), "id", id.ToString());
		}

        // cancella il record dal db
		public virtual void delete() {
			_db.deleteData(_tableName, "id", id.ToString());
		}

        // ritorna l'hashtable di un oggetto a partire dal suo id
		public static ConvertibleHashtable getHashtableById<T>(int id) {
			return _getHashtableById(id, _getTableNameByType(typeof(T)));
		}

        // come sopra ma necessita del nome della tabella invece del tipo di oggetto
		private static ConvertibleHashtable _getHashtableById(int id, String tableName) {
			return _db.getData(tableName, "id", id.ToString())[0];
		}

        // ritorna l'oggetto a partire dall'id
		public static T getById<T> (int id) {
			return _getHashtableById(id, _getTableNameByType(typeof(T))).toObject<T>();
		}

        // ritorna tutti gli oggetti di un certo tipo
		public static ConvertibleHashtable[] getAll<T>() {
			return _db.getData(_getTableName<T>());
		}

        // vedere sotto
        protected static String _getTableName<T>() {
			return _getTableNameByType(typeof(T));
		}

        // calcola il nome della tabella a partire dal tipo dell'oggetto
        // ATTENZIONE: affinché funzioni nello schema le tabelle devono essere chiamate
        // con lo stesso nome delle classi degli oggetti
		private static String _getTableNameByType(Type type) {
			return type.ToString().Split('.').Last();
		}
	}
}
