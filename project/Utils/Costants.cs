using System;

namespace project.Utils {

    // classe contenente le costanti dell'applicazione
    public class Costants {

        // json per utente non autorizzato
		public static ConvertibleHashtable UNAUTHORIZED { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable();
				h.Add ("error", "user_unauthorized");
				return h;
			} 
		}

        // json per messaggio di successo dell'operazione
		public static ConvertibleHashtable OK { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable ();
				h.Add ("server", "success");
				return h;
			} 
		}

        // json per il messaggio di errore relativo al tipo di utente specificato non valido
		public static ConvertibleHashtable WRONG_USER_TYPE { 
            get { 
				ConvertibleHashtable h = new ConvertibleHashtable ();
				h.Add ("error", "wrong user type");
				return h;
			} 
		}

        // json per utente non trovato
        public static ConvertibleHashtable USER_NOT_FOUND {
            get { 
                ConvertibleHashtable h = new ConvertibleHashtable ();
                h.Add("error", "user not found");
                return h;
            } 
        }
	}
}
