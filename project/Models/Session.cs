using System;
using project.Utils;

namespace project.Models
{
    // classe per la registrazione delle sessioni degli utenti
    public class Session : Model
	{
        // id dell'utente, tempo d'inizio e fine
        public int user_id { get; set; }
        public String start { get; set; }
        public String end { get; set; }

        // ritorna l'ora corrente
        public static String getCurrentTime() {
            return DateTime.Now.ToString();
        }

        // ritorna una sessione che inizia al tempo della chiamata del metodo
        public static Session startingNow() {
            Session s = new Session();
            s.start = Session.getCurrentTime();
            return s;
        }

        // ritorna l'ultima sessione dell'utente
        public static Session getLastByUserId(int user_id) {
            ConvertibleHashtable[] h = _db.getData("Session", "user_id", user_id.ToString());
            return h[h.Length - 1].toObject<Session>();
        }

        // apre una sessione
        public static void OpenSession(int user_id) {
            Session s = Session.startingNow();
            s.user_id = user_id;
            s.insert();
        }

        // chiude una sessione
        public static void CloseSession(int user_id) {
            // chiude la sessione precedentemente aperta
            Session s = Session.getLastByUserId(user_id);
            s.end = Session.getCurrentTime();
            s.update();
        }
	}
}

