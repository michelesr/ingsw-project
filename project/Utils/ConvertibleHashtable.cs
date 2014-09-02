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

        public static ConvertibleHashtable fromJObject(JObject j) {
            return j.ToObject<ConvertibleHashtable>();
        }

		public static ConvertibleHashtable fromString(String s) {
            return fromJObject(JObject.Parse(s));
		}

        public static ConvertibleHashtable fromRequest() {
            Stream input = HttpContext.Current.Request.InputStream;
            input.Position = 0;
            return fromString(new StreamReader(input).ReadToEnd());
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

        public void update(ConvertibleHashtable h) {
            foreach (var key in h.Keys) 
                if (this.ContainsKey(key))
                    this[key] = h[key];
        }

		public void merge(ConvertibleHashtable h) {
			foreach (var k in h.Keys) {
				if (!this.ContainsKey(k))
					this.Add(k, h[k]);
			}
		}

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
	}
}

