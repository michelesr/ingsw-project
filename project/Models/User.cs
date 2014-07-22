using System;

namespace project.Models {
	public abstract class User {
		public static readonly String[][] model = {
			new String[] {"email", "VARCHAR"},
			new String[] {"password", "VARCHAR"},
		    new String[] {"first_name", "VARCHAR"},
			new String[] {"last_name", "VARCHAR"}
		};
	}
}
