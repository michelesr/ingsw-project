using System;

namespace project
{
	public class User
	{
		public User (String email, String password, String nome, String cognome)
		{
			//String[][] values = {
				//	new String[] { email, password, nome, cognome },
				//};
			//Database.insertData("User", values);
			
		}

		public static String[][] model() {
			String[][] model = {
				new String[] {"email", "VARCHAR"},
				new String[] {"password", "VARCHAR"},
				new String[] {"nome", "VARCHAR"},
				new String[] {"cognome", "VARCHAR"},
				new String[] {"birth_date", "DATE"},
				new String[] {"city", "VARCHAR"},
			};
			return model;
		}

		public static String[][] example_data() {
			String[][] data = {
				new String[] { "mike@fender.it", "pw", "Mike", "Fender", "1999-01-01", "Mondolfo" },
				new String[] { "kobe@bryant.io", "pass", "Kobe", "Bryant", "1995-05-05", "Ancona" },
			};
			return data;
		}

		// e.g.: method edit_user()
	}
}

