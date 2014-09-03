using System;

namespace project.Utils
{
    // classe contenente lo schema del database
	public static class Schema {

        // riferimento al db
		private static Database _db = Database.Istance;
        // tabelle
        private static String[] _tables = new String[] {"ApiKey", "User", "Admin", "Supplier", "ProductCategory", "ProductStock", "Product", "City", "Session"};

        // schemi delle tabelle

		private static readonly String[][] _apiKey = {
            // nome campo, tipo di dato, opzioni, chiave esterna(campo interno, tabella esterna, campo esterno)
			new String[] {"user_id", "INTEGER", "NOT NULL", _getFK ("user_id", "User", "id")},
			new String[] {"key", "VARCHAR", "UNIQUE NOT NULL"}
		};

		private static readonly String[][] _user = {
            new String[] {"email", "VARCHAR", "UNIQUE NOT NULL"},
            new String[] {"password", "VARCHAR", "NOT NULL"},
            new String[] {"first_name", "VARCHAR", "NOT NULL"},
            new String[] {"last_name", "VARCHAR", "NOT NULL"},
            new String[] {"type", "INTEGER"}
        };

		private static readonly String[][] _supplier = {
            new String[] {"vat", "VARCHAR", "NOT NULL"},
            new String[] {"supplier_name", "VARCHAR", "NOT NULL"},
            new String[] {"city", "INTEGER", "NOT NULL", _getFK("city", "City", "id")},
            new String[] {"user_id", "INTEGER", "NOT NULL", _getFK("user_id", "User", "id")},
        };

		private static readonly String[][] _admin = {
            new String[] {"user_id", "INTEGER", "NOT NULL", _getFK("user_id", "User", "id")},
        };

        private static readonly String[][] _productCategory = {
            new String[] {"name", "VARCHAR", "UNIQUE NOT NULL"}
        };

        private static readonly String[][] _productStock = {
            new String[] {"product_id", "INTEGER", "NOT NULL", _getFK("product_id", "Product", "id")},
            new String[] {"price", "FLOAT", "NOT NULL"},
            new String[] {"min", "INTEGER"},
            new String[] {"max", "INTEGER"},
            new String[] {"aviability", "INTEGER", "NOT NULL"}
        };

        private static readonly String[][] _city = {
            new String[] {"name", "VARCHAR", "UNIQUE NOT NULL"}
        };

        private static readonly String[][] _product = {
            new String[] {"name", "VARCHAR", "UNIQUE NOT NULL"},
            new String[] {"supplier_id", "INTEGER", "NOT NULL", _getFK("supplier_id", "Supplier", "id") },
            new String[] {"product_category", "INTEGER", "NOT NULL", _getFK("product_category", "ProductCategory", "id")}
        };

        private static readonly String[][] _session = {
            new String[] {"user_id", "INTEGER", "NOT NULL", _getFK("user_id", "User", "id")},
            new String[] { "start", "VARCHAR" },
            new String[] { "end", "VARCHAR" }
        };

        private static String[][][] _models = new String[][][] { _apiKey, _user, _admin, _supplier, _productCategory, _productStock, _product, _city, _session };


        // triggers per l'inserimento/aggiornamento
        private static readonly String[][] _insertTriggers = {
            // tabella interna, campo interno, tabella esterna
            new String[] {"ApiKey", "user_id", "User"},
            new String[] {"Supplier", "user_id", "User"},
            new String[] {"Supplier", "city", "City"},
            new String[] {"Admin", "user_id", "User"},
            new String[] {"Product", "supplier_id", "Supplier"},
            new String[] {"Product", "product_category", "ProductCategory"},
            new String[] {"Session", "user_id", "User"},
            new String[] {"ProductStock", "product_id", "Product"}
        };

        // metodo che restituisce la stringa sql per la generazione delle chiavi esterne
		private static String _getFK(String localField, String foreignTable, String foreignField) {
            return ", FOREIGN KEY(`" + localField + "`) REFERENCES `" + foreignTable + "`(`" + foreignField + "`) ON DELETE RESTRICT ON UPDATE CASCADE"; 
		}

        // metodo che inizializza il database
		public static void createSchema() {
            for (int i = 0; i < _tables.Length; i++) 
				_db.createTable(_tables[i], _models[i]);

            foreach (String[] trigger in _insertTriggers) {
                _db.createInsertTrigger(trigger[0], trigger[1], trigger[2]);
                _db.createUpdateTrigger(trigger[0], trigger[1], trigger[2]);
            }
		}
	}
}

