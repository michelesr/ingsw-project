using System;

namespace project.Models {
	public class ApiKey : Model {
		public String key {get; set;}
		public int user_id {get; set;}
		public ApiKey(int id, string mail, string password) {
			user_id = id;
			key = Utils.Hashing.CalculateMD5Hash(mail + password);
		}
	}
}
