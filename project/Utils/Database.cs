using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace project.Utils
{
	// api di alto livello di astrazione per database sqlite
	public class Database
	{
		private const String _fileName = "db.sqlite"; // path del database
		private SqliteConnection _con; // connessione al database 
		private static volatile Database _istance = null; // istanza del database
		private static object _lock = new object(); // lock per la creazione dell'istanza

		// ritorna l'istanza del database, creandola qualora non esistesse
		public static Database Istance {
			get {
				lock (_lock) {
					if (_istance == null) {
						_istance = new Database();
					}
				}
				return _istance;
			}
		}

		// crea il Database 
		private Database()
		{
			if (File.Exists(_fileName) == false) {
				SqliteConnection.CreateFile(_fileName);
			}
			_con = new SqliteConnection("Data Source=" + _fileName);
			_con.Open();
		}

		// lancia una query e ritorna la tabella risultante
		private ConvertibleHashtable[] _executeQuery(String sql) {
			Console.WriteLine(sql);
			DataTable table = new DataTable();
			table.Load(new SqliteCommand(sql, _con).ExecuteReader());
			return _parseTable(table);
		}

		// converte i risultati di una query in un array di ConvertibleHashtable 
		private ConvertibleHashtable[] _parseTable(DataTable table) {
			ConvertibleHashtable[] outputTable;
			if (table.Rows.Count > 0) {
				outputTable = new ConvertibleHashtable[table.Rows.Count];
				foreach (DataRow row in table.Rows) { outputTable [table.Rows.IndexOf(row)] = new ConvertibleHashtable ();
					foreach (DataColumn column in table.Columns) {
						outputTable [table.Rows.IndexOf(row)].Add (column.ColumnName, row[column.ColumnName]);
					}
				}
			}
			else
				outputTable = new ConvertibleHashtable[] { new ConvertibleHashtable() };
			return outputTable;
		}

		// crea una tabella col nome scelto e con le combinazioni dati/tipi scelte 
		public void createTable(String tableName, String[][] fields) {
			String sql = "CREATE TABLE IF NOT EXISTS `" + tableName + "` (";
			foreach(String[] field in fields) {
				String options = (field.Length < 3? "": " " + field[2]);
				sql += "`" + field[0] + "` " + field[1] + options + ", ";
			}
			sql += "`id` INTEGER PRIMARY KEY NOT NULL";
			foreach(String[] field in fields) 
				if (field.Length >= 4) 
					sql += field[3];
			sql += ");";
			_executeQuery(sql);
		}

        // crea un trigger per controllare che non venga inserito un valore relativo a un valore esterno inesistente
        public void createInsertTrigger(String localTable, String localField, String foreignTable) {
            String sql = "CREATE TRIGGER IF NOT EXISTS it__" + localTable + "__" + localField +
                         " BEFORE INSERT ON `" + localTable + "` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value \"" + localField + "\" references null value') " +
                         " WHERE NEW.`" + localField + "` IS NOT NULL AND ((SELECT `id` FROM `" + foreignTable + "` WHERE `id`=NEW.`" + localField + "`) IS NULL); END;";
            _executeQuery(sql);
        }

        public void createUpdateTrigger(String localTable, String localField, String foreignTable) {
            String sql = "CREATE TRIGGER IF NOT EXISTS ut__" + localTable + "__" + localField +
                         " BEFORE UPDATE ON `" + localTable + "` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value \"" + localField + "\" references null value') " +
                         " WHERE NEW.`" + localField + "` IS NOT NULL AND ((SELECT `id` FROM `" + foreignTable + "` WHERE `id`=NEW.`" + localField + "`) IS NULL); END;";
            _executeQuery(sql);
        }

		// inserisce un valore dentro una tabella (campi espliciti)
		public int insertData(String tableName, ConvertibleHashtable data) {
			// interrogo il db per ottenere informazioni sulla tabella 
			ConvertibleHashtable[] h = _executeQuery("pragma table_info(`" + tableName + "`);");
			List<String> keys = new List<String>();
			List<String> invalidKeys = new List<String>();

			// aggiungo i nomi delle colonne alle chiavi
			foreach(ConvertibleHashtable x in h) 
				keys.Add(x["name"].ToString());

			// aggiungo le chiavi invalide a una lista
			foreach(var k in data.Keys) {
				if(!(keys.Contains(k.ToString()))) 
					invalidKeys.Add(k.ToString());
			}

			// rimuovo i campi invalidi e l'id che viene autogenerato
			foreach(String k in invalidKeys)
				data.Remove(k);
			data.Remove("id");

			String sql = "INSERT INTO `" + tableName + "` (";
			foreach(var d in data.Keys) {
				sql += "`" + d.ToString() + "`, ";
			}
			sql += "`id`) VALUES (";
			foreach(var d in data.Keys) {
				sql += "'" + data[d.ToString()] + "', ";
			}
			sql += "NULL);" ;
			lock (_lock) {
				_executeQuery(sql);
				return int.Parse(_executeQuery("SELECT last_insert_rowid() FROM `" + 
				                 tableName + "`;")[0]["last_insert_rowid()"].ToString()); 
			}
		}

		// ritorna tutti i record della tabella scelta
		public ConvertibleHashtable[] getData(String tableName) {
			return _executeQuery("SELECT * FROM " + tableName);

		}

		// ritorna i record per i quali inputField = inputValue
		public ConvertibleHashtable[] getData(String tableName, String inputField, String inputValue) {
			return getData(tableName, inputField, inputValue, new String[] {"*"});
		} 

		// ritorna i campi scelti per i record dove inputField = inputValue 
		public ConvertibleHashtable[] getData(String tableName, String inputField, String inputValue, String[] outputFields) {
			String sql = "SELECT ";
			for(int i=0; i < outputFields.Length; i++) {
				sql += outputFields[i];
				if (i < outputFields.Length - 1) {
					sql += ",";
				}
			}
			sql += " FROM `" + tableName + "` WHERE `" + inputField + "`='" + inputValue + "'";
			return _executeQuery(sql);
		} 
		
		// modifica un record per i quali vale oldField = oldValue assegnando al campo fieldToEdit il valore newValue
		public void updateData(String tableName, String fieldToEdit, String newValue, String oldField, String oldValue) {
		    _executeQuery("UPDATE `" + tableName + "` SET `" + fieldToEdit + "`='" + newValue + "' WHERE `" + oldField + "`='" + oldValue + "';");
		}
		
		// rimuove i record per i quali vale field = value dalla tabella scelta
		public void deleteData(String tableName, String field, String value) {
			_executeQuery("DELETE FROM `" + tableName + "` WHERE " + field + "='" + value + "';");
		}

		// ritorna una stringa che descrive il contenuto della tabella
		public String tableToString(String tableName) {
			String sql = "SELECT * FROM " + tableName;
			SqliteCommand cmd = new SqliteCommand(sql, _con);
			DataTable table = new DataTable();
			table.Load(cmd.ExecuteReader());

			String columns = string.Empty;
			foreach (DataColumn column in table.Columns) {
				columns += column.ColumnName + " | ";
			}
			String tableString = columns + "\n";

			foreach (DataRow row in table.Rows) {
				String rowText = string.Empty;
				foreach (DataColumn column in table.Columns) {
					rowText += row[column.ColumnName] + " | ";
				}
				tableString += rowText + "\n";
			}
			return tableString;
		}
	}
}
