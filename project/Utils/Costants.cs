using System;

namespace project.Utils {

    /// Classe contenente le costanti dell'applicazione
    public class Costants {

        /// JSON per utente non autorizzato
		public static ConvertibleHashtable UNAUTHORIZED { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable();
				h.Add ("error", "user_unauthorized");
				return h;
			} 
		}

        /// JSON per messaggio di successo dell'operazione
		public static ConvertibleHashtable OK { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable ();
				h.Add ("server", "success");
				return h;
			} 
		}

        /// JSON per il messaggio di errore relativo al tipo di utente specificato non valido
		public static ConvertibleHashtable WRONG_USER_TYPE { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable ();
				h.Add ("error", "wrong user type");
				return h;
			} 
		}

        /// JSON per utente non trovato
        public static ConvertibleHashtable USER_NOT_FOUND {
            get { 
                ConvertibleHashtable h = new ConvertibleHashtable ();
                h.Add("error", "user not found");
                return h;
            } 
        }
	}
}
