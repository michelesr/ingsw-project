using System;
using project.Utils;

namespace project.Models
{
    public class Session : Model
	{
        public int user_id { get; set; }
        public String start { get; set; }
        public String end { get; set; }
        public static String getCurrentTime() {
            return DateTime.Now.ToString();
        }
        public static Session startingNow() {
            Session s = new Session();
            s.start = Session.getCurrentTime();
            return s;
        }
        public static Session getLastByUserId(int user_id) {
            ConvertibleHashtable[] h = _db.getData("Session", "user_id", user_id.ToString());
            return h[h.Length - 1].toObject<Session>();
        }

        public static void OpenSession(int user_id) {
            Session s = Session.startingNow();
            s.user_id = user_id;
            s.insert();
        }

        public static void CloseSession(int user_id) {
            // chiude la sessione precedentemente aperta
            Session s = Session.getLastByUserId(user_id);
            s.end = Session.getCurrentTime();
            s.update();
        }
	}
}

