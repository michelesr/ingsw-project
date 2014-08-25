using System;
using System.Web;

namespace project.Models {
	public class ApiKey : Model {
		public String key {get; set;}
		public int user_id {get; set;}
		public userType utype{ 
			get {
				if (Model.getById<Admin> (user_id).user_id == user_id)
					return userType.admin;
				else if (Model.getById<Supplier> (user_id).user_id == user_id)
					return userType.supplier;
				else
					return userType.undefined;
			}
		}
		public ApiKey(int id, string mail, string password) {
			user_id = id;
			key = Utils.Hashing.CalculateMD5Hash(mail + password);
		}
		public static ApiKey getApiKey(String k) {
			return _db.getData("ApiKey", "key", k)[0].toObject<ApiKey>();
		}
		public static ApiKey getApiKey() {
			return getApiKey(HttpContext.Current.Request.Headers["api_key"]);
		}
	}
}
