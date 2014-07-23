using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Mono.Data.Sqlite;

namespace project
{
	// api di alto livello di astrazione per database sqlite
	public class Database
	{
		private const String _fileName = "db.sqlite"; // path del database
		private SqliteConnection _con; // connessione al database 
		private static volatile Database _istance = null; // istanza del database
		private static object _myLock = new object(); // lock per la creazione dell'istanza

		// ritorna l'istanza del database, creandola qualora non esistesse
		public static Database Istance {
			get {
				lock (_myLock) {
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

		// lancia una query e ritorna la tabella risultante (null se vuota)
		private Hashtable[] _executeQuery(String sql) {
			DataTable table = new DataTable();
			table.Load(new SqliteCommand(sql, _con).ExecuteReader());
			return _parseTable(table);
		}

		// converte i risultati di una query in un array di hashtable 
		private Hashtable[] _parseTable(DataTable table) {
			Hashtable[] outputTable = null;
			if (table.Rows.Count > 0) {
				outputTable = new Hashtable[table.Rows.Count];
				foreach (DataRow row in table.Rows) { outputTable [table.Rows.IndexOf(row)] = new Hashtable ();
					foreach (DataColumn column in table.Columns) {
						outputTable [table.Rows.IndexOf(row)].Add (column.ColumnName, row[column.ColumnName]);
					}
				}
			}
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
			Console.WriteLine(sql);
			_executeQuery(sql);
		}

		// inserisce un valore dentro una tabella (campi espliciti)
		public void insertData(String tableName, String[][] data) {
			String sql = "INSERT INTO `" + tableName + "` (";
			foreach(String[] d in data) {
				sql += "`" + d[0] + "`, ";
			}
			sql += "`id`) VALUES (";
			foreach(String[] d in data) {
				sql += "'" + d[1] + "', ";
			}
			sql += "NULL);" ;
			_executeQuery(sql);
		}

		// ritorna tutti i record della tabella scelta
		public Hashtable[] getData(String tableName) {
			return _executeQuery("SELECT * FROM " + tableName);

		}

		// ritorna i record per i quali inputField = inputValue
		public Hashtable[] getData(String tableName, String inputField, String inputValue) {
			return getData(tableName, inputField, inputValue, new String[] {"*"});
		} 

		// ritorna i campi scelti per i record dove inputField = inputValue 
		public Hashtable[] getData(String tableName, String inputField, String inputValue, String[] outputFields) {
			String sql = "SELECT ";
			for(int i=0; i < outputFields.Length; i++) {
				sql += outputFields[i];
				if (i < outputFields.Length - 1) {
					sql += ",";
				}
			}
			sql += " FROM " + tableName + " WHERE " + inputField + "='" + inputValue + "'";
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

		// stampa di debug di una tabella
		public void printTable(String tableName) {

			String sql = "SELECT * FROM " + tableName;
			SqliteCommand cmd = new SqliteCommand(sql, _con);
			DataTable table = new DataTable();
			table.Load(cmd.ExecuteReader());

			String columns = string.Empty;
			foreach (DataColumn column in table.Columns) {
				columns += column.ColumnName + " | ";
			}
			Console.WriteLine(columns);

			foreach (DataRow row in table.Rows) {
				String rowText = string.Empty;
				foreach (DataColumn column in table.Columns) {
					rowText += row[column.ColumnName] + " | ";
				}
				Console.WriteLine(rowText);
			}
		}

		public static String getForeignKeyOption(String localField, String foreignTable, String foreignField) {
			return ", FOREIGN KEY(`" + localField + "`) REFERENCES `" + foreignTable + "`(`" + foreignField + "`)"; 
		}

		// chiude la connessione al database
		public void closeDatabase() {
			_con.Close();
		}
	}
}
