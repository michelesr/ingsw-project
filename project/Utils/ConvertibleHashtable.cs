using System;
using System.Collections;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project.Utils {

    // classe che estende le hashtable aggiungendo funzionalità di conversione
	public class ConvertibleHashtable : Hashtable {

		// converte l'hashtable nell'oggetto .NET desiderato
		public T toObject<T>() {
            //Console.WriteLine(JsonConvert.SerializeObject(this));
			return JObject.Parse(JsonConvert.SerializeObject(this)).ToObject<T>();
		}

        // genera un hashtable a partire da un JObject
        public static ConvertibleHashtable fromJObject(JObject j) {
            return j.ToObject<ConvertibleHashtable>();
        }

        // genera un hashtable a partire da una stringa JSON
		public static ConvertibleHashtable fromString(String s) {
            return fromJObject(JObject.Parse(s));
		}

        // genera un hashtable a partire dai dati contenuti nella richiesta http di tipo POST
        public static ConvertibleHashtable fromRequest() {
            Stream input = HttpContext.Current.Request.InputStream;
            input.Position = 0;
            return fromString(new StreamReader(input).ReadToEnd());
        }

        // elimina le chiavi dell'hashtable
		public ConvertibleHashtable filter(String[] keys) {
			foreach (String k in keys)
				this.filter(k);
			return this;
		}

        // elimina una chiave dell'hashtable
		public ConvertibleHashtable filter(String key) {
			this.Remove(key);
			return this;
		}

        // elimina la password
		public ConvertibleHashtable filterPassword() {
			return this.filter("password");
		}

        // elimina le password da un array di hashtable
		public static ConvertibleHashtable[] filterPassword(ConvertibleHashtable[] h) {
			foreach(ConvertibleHashtable x in h) 
				x.filterPassword();
			return h;
		}

        // aggiorna i dati contenuti nell'hashtable sostituendoli con i dati dell'hashtable passata per parametro
        public void update(ConvertibleHashtable h) {
            foreach (var key in h.Keys) 
                if (this.ContainsKey(key))
                    this[key] = h[key];
        }

        // fonde due hashtable aggiungendo alla prima le chiavi della seconda che non son già presenti
		public void merge(ConvertibleHashtable h) {
			foreach (var k in h.Keys) {
				if (!this.ContainsKey(k))
					this.Add(k, h[k]);
			}
		}

        // ritorna una rappresentazione JSON dell'hashtable
        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
	}
}

