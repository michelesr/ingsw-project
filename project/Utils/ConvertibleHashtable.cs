using System;
using System.Collections;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project.Utils {

    /// Classe che estende le hashtable aggiungendo funzionalità di conversione
	public class ConvertibleHashtable : Hashtable {

        /// Converte l'hashtable nell'oggetto .NET desiderato
		public T toObject<T>() {
            //Console.WriteLine(JsonConvert.SerializeObject(this));
			return JObject.Parse(JsonConvert.SerializeObject(this)).ToObject<T>();
		}

        /// Genera un hashtable a partire da un JObject
        public static ConvertibleHashtable fromJObject(JObject j) {
            return j.ToObject<ConvertibleHashtable>();
        }

        /// Genera un hashtable a partire da una stringa JSON
		public static ConvertibleHashtable fromString(String s) {
            return fromJObject(JObject.Parse(s));
		}

        /// Genera un hashtable a partire dai dati contenuti nella richiesta http di tipo POST
        public static ConvertibleHashtable fromRequest() {
            Stream input = HttpContext.Current.Request.InputStream;
            input.Position = 0;
            return fromString(new StreamReader(input).ReadToEnd());
        }

        /// Elimina le chiavi dell'hashtable
		public ConvertibleHashtable filter(String[] keys) {
			foreach (String k in keys)
				this.filter(k);
			return this;
		}

        /// Elimina una chiave dell'hashtable
		public ConvertibleHashtable filter(String key) {
			this.Remove(key);
			return this;
		}

        /// Elimina la password
		public ConvertibleHashtable filterPassword() {
			return this.filter("password");
		}

        /// Elimina le password da un array di hashtable
		public static ConvertibleHashtable[] filterPassword(ConvertibleHashtable[] h) {
			foreach(ConvertibleHashtable x in h) 
				x.filterPassword();
			return h;
		}

        /// Elimina le password da un ArrayList di hashtable
        public static ArrayList filterPassword(ArrayList a) {
            foreach(ConvertibleHashtable x in a) 
                x.filterPassword();
            return a;
        }

        /** Aggiorna i dati contenuti nell'hashtable sostituendoli 
            con i dati dell'hashtable passata per parametro */
        public void update(ConvertibleHashtable h) {
            foreach (var key in h.Keys) 
                if (this.ContainsKey(key))
                    this[key] = h[key];
        }

        /** Fonde due hashtable aggiungendo alla prima 
            le chiavi della seconda che non son già presenti */
		public void merge(ConvertibleHashtable h) {
			foreach (var k in h.Keys) {
				if (!this.ContainsKey(k))
					this.Add(k, h[k]);
			}
		}

        // Ritorna una rappresentazione JSON dell'hashtable
        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
	}
}

