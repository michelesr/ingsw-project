using System;

namespace project.Utils
{
	public class Costants
	{
		public static ConvertibleHashtable UNAUTHORIZED { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable();
				h.Add ("error", "user_unauthorized");
				return h;
			} 
		}

		public static ConvertibleHashtable OK { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable ();
				h.Add ("server", "success");
				return h;
			} 
		}

		public static ConvertibleHashtable WRONG_USER_TYPE { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable ();
				h.Add ("error", "wrong user type");
				return h;
			} 
		}
        public static ConvertibleHashtable USER_NOT_FOUND {
            get { 
                ConvertibleHashtable h = new ConvertibleHashtable ();
                h.Add("error", "user not found");
                return h;
            } 
        }
	}
}

