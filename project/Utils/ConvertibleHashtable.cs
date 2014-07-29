using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project.Utils {
	public class ConvertibleHashtable : Hashtable {
		// converte l'hashtable nell'oggetto .NET desiderato
		public T toObject<T>() {
			return JObject.Parse(JsonConvert.SerializeObject(this)).ToObject<T>();
		}

		public static ConvertibleHashtable fromString(String s) {
			return JObject.Parse(s).ToObject<ConvertibleHashtable>();
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
	}
}

