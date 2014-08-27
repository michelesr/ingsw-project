using System;
using System.Web;

namespace project.Models {
	public class ApiKey : Model {
		public String key {get; set;}
		public int user_id {get; set;}
        public userType utype { 
			get {
                if (Model.getById<Admin>(user_id).user_id == user_id)
                    return userType.admin;
                else if (Model.getById<Supplier>(user_id).user_id == user_id)
                    return userType.supplier;
                else
                    return userType.undefined;
			}
		}

        public override void update() {
            User u = Model.getById<User>(user_id);
            key = Utils.Hashing.CalculateMD5Hash(u.email + u.password);
            base.update();
        }

        public ApiKey(int uid, string email, string password) {
            user_id = uid;
            key = Utils.Hashing.CalculateMD5Hash(email + password);
		}

		public static ApiKey getApiKey(String k) {
			return _db.getData("ApiKey", "key", k)[0].toObject<ApiKey>();
		}

        public static ApiKey fromUserId(int uid) {
            return _db.getData("ApiKey", "user_id", uid.ToString())[0].toObject<ApiKey>();
        }

		public static ApiKey getApiKey() {
			return getApiKey(HttpContext.Current.Request.Headers["api_key"]);
		}

		public bool isAdmin() {
			return this.user_id != 0 && this.utype == userType.admin;
		}

		public bool checkUser(int id) {
			return this.user_id == id;
		}
	}
}
