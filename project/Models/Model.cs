using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using project.Utils;

namespace project.Models {
    /** Modello generico di rappresentazione dei dati, 
        contiene la logica di interazione tra gli oggetti
        e il database */
    /// Modello generico del db
	public abstract class Model {
        /// Istanza del db
		protected static readonly Database _db = Database.Istance;
        /// Id del record relativo all'oggetto
        public int id { get; set; }

        /// Tabella relativa all'oggetto in questione
		protected String _tableName {
			get { return _getTableNameByType(this.GetType()); }
		}

        /** Trasforma l'oggetto in una ConvertibleHashtable contenente 
            i suoi attributi nella forma attributo:valore */
		private ConvertibleHashtable _toConvertibleHashtable() {
			return JObject.FromObject(this).ToObject<ConvertibleHashtable>();
		}

        /// Inserisce il record nel db
		public virtual void insert() {
			id = _db.insertData(_tableName, this._toConvertibleHashtable());
			Console.WriteLine(id);
		}

        /// Aggiorna il record nel db
		public virtual void update() {
			ConvertibleHashtable old = _getHashtableById(id, _tableName);
			ConvertibleHashtable current = this._toConvertibleHashtable();

			foreach(var k in old.Keys) 
				if (old[k].ToString() != current[k].ToString())
					_db.updateData(_tableName, k.ToString(), current[k].ToString(), "id", id.ToString());
		}

        /// Cancella il record dal db
		public virtual void delete() {
			_db.deleteData(_tableName, "id", id.ToString());
		}

        /** Ritorna la ConvertibleHashtable dell'oggetto relativo al 
            record del db con l'id fornito (la tabella viene dedotta 
            automaticamente in base al tipo fornito) */
		public static ConvertibleHashtable getHashtableById<T>(int id) {
			return _getHashtableById(id, _getTableNameByType(typeof(T)));
		}

        /** Ritorna la ConvertibleHashtable dell'oggetto relativo alla tabella
            con il nome fornito e al record con l'id fornito */
		private static ConvertibleHashtable _getHashtableById(int id, String tableName) {
			return _db.getData(tableName, "id", id.ToString())[0];
		}

        /** Ritorna l'oggetto relativo all'id fornito (la tabella viene
            dedotta automaticamente in base al tipo fornito) */
		public static T getById<T> (int id) {
			return _getHashtableById(id, _getTableNameByType(typeof(T))).toObject<T>();
		}

        /// Ritorna tutti gli oggetti del tipo fornito
		public static ConvertibleHashtable[] getAll<T>() {
			return _db.getData(_getTableName<T>());
		}

        /** Calcola il nome della tabella a partire dal tipo dell'oggetto
            ATTENZIONE: affinché funzioni nello schema le tabelle devono essere chiamate
            con lo stesso nome delle classi degli oggetti */
        protected static String _getTableName<T>() {
			return _getTableNameByType(typeof(T));
		}

        /** Calcola il nome della tabella a partire dal tipo dell'oggetto
            ATTENZIONE: affinché funzioni nello schema le tabelle devono essere chiamate
            con lo stesso nome delle classi degli oggetti */
		private static String _getTableNameByType(Type type) {
			return type.ToString().Split('.').Last();
		}
	}
}
