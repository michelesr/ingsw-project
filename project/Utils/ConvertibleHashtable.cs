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
	}
}

