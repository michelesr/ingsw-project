using System;

namespace project.Models
{
	public abstract class User
	{
		private int userId;

		public User (String email, String password, String nome, String cognome)
		{
			String[][] values = {
					new String[] { email, password, nome, cognome },
				};
			Database db = Database.Istance;
			db.insertData("User", values);
			userId = int.Parse(db.getData("User", "email", email, new String[] {"id"})[0]["id"].ToString());
		}

		public static String[][] model() {
			String[][] model = {
				new String[] {"email", "VARCHAR"},
				new String[] {"password", "VARCHAR"},
				new String[] {"nome", "VARCHAR"},
				new String[] {"cognome", "VARCHAR"},
				//new String[] {"birth_date", "DATE"},
				//new String[] {"city", "VARCHAR"},
			};
			return model;
		}

		public static String[][] example_data() {
			String[][] data = {
				new String[] { "mike@fender.it", "pw", "Mike", "Fender" },
				new String[] { "kobe@bryant.io", "pass", "Kobe", "Bryant" },
				//new String[] { "mike@fender.it", "pw", "Mike", "Fender", "1999-01-01", "Mondolfo" },
				//new String[] { "mike@fender.it", "pw", "Mike", "Fender", "1999-01-01", "Mondolfo" },
			};
			return data;
		}

		// e.g.: method edit_user()
	}
}

