using System;

namespace project.Models
{
	public class Admin : User
	{
		public Admin (String email, String password, String nome, String cognome) : base (email, password, nome, cognome)
		{
			// TODO
			// add/remove suppliers
		}
	}
}

