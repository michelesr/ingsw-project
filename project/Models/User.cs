using System;

namespace project.Models {
	public class User : Model {
		public String email {get; set;}
		public String password {get; set;}
		public String first_name {get; set;}
		public String last_name {get; set;}
		public int user_id {get; set;}

		protected int getUserId() {
			return int.Parse(_db.getData("User", "email", email, new string[] {"id"})[0]["id"].ToString());
		}

		public User(String email, String password, String first_name, String last_name) : this() {
			this.email = email;
			this.password = password;
			this.first_name = first_name;
			this.last_name = last_name;
		}

		protected User(int id) : this() {
			this.id = id;
		}

		protected User() : base () {}

		public override void update () {
		    base.update();
			if (this is Supplier || this is Admin) {
				User u = new User(email, password, first_name, last_name);
				u.id = user_id;
				u.update();
			}
		}

		public override void delete() {
			base.delete();
			if (this is Supplier || this is Admin)
				new User(user_id).delete();
		}

		public override void insert() {
			if ((this is Supplier || this is Admin)) {
				User u = new User(email, password, first_name, last_name);
				u.insert();
				user_id = u.id;
			} 
			base.insert();
		}
	}
}
