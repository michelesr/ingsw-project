using System;
using System.Collections;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project.Utils {
	public class ConvertibleHashtable : Hashtable {
		// converte l'hashtable nell'oggetto .NET desiderato
		public T toObject<T>() {
            //Console.WriteLine(JsonConvert.SerializeObject(this));
			return JObject.Parse(JsonConvert.SerializeObject(this)).ToObject<T>();
		}

		public static ConvertibleHashtable fromString(String s) {
			return JObject.Parse(s).ToObject<ConvertibleHashtable>();
		}

        public static ConvertibleHashtable fromRequest() {
            return fromString(new StreamReader(HttpContext.Current.Request.InputStream).ReadLine());
        }

		public ConvertibleHashtable filter(String[] keys) {
			foreach (String k in keys)
				this.filter(k);
			return this;
		}

		public ConvertibleHashtable filter(String key) {
			this.Remove(key);
			return this;
		}

		public ConvertibleHashtable filterPassword() {
			return this.filter("password");
		}

		public static ConvertibleHashtable[] filterPassword(ConvertibleHashtable[] h) {
			foreach(ConvertibleHashtable x in h) 
				x.filterPassword();
			return h;
		}

		public void merge(ConvertibleHashtable h) {
			foreach (var k in h.Keys) {
				if (!this.ContainsKey(k))
					this.Add(k, h[k]);
			}
		}
	}
}

