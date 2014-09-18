using System;
using project.Utils;

namespace project.Models
{
    /// Dati di sessione dell'User
    public class Session : Model
	{
        /// L'id dell'utente
        public int user_id { get; set; }
        /// Data e ora di inizio sessione
        public String start { get; set; }
        /// Data e ora di fine sessione
        public String end { get; set; }

        /// Ritorna l'ora corrente
        public static String getCurrentTime() {
            return DateTime.Now.ToString();
        }

        /// Ritorna una sessione con start settato al tempo della chiamata del metodo
        public static Session startingNow() {
            Session s = new Session();
            s.start = Session.getCurrentTime();
            return s;
        }

        /// Ritorna la sessione più recente dell'utente
        public static Session getLastByUserId(int user_id) {
            ConvertibleHashtable[] h = _db.getData("Session", "user_id", user_id.ToString());
            return h[h.Length - 1].toObject<Session>();
        }

        // Apre una sessione, registrandone il tempo di inizio
        public static void OpenSession(int user_id) {
            Session s = Session.startingNow();
            s.user_id = user_id;
            s.insert();
        }

        // Chiude una sessione, registrandone il tempo di chiusura
        public static void CloseSession(int user_id) {
            // chiude la sessione precedentemente aperta
            Session s = Session.getLastByUserId(user_id);
            s.end = Session.getCurrentTime();
            s.update();
        }
	}
}

