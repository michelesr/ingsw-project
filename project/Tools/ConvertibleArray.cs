using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project.Tools {
	public class ConvertibleArray : Array {
		// converte l'array nell'oggetto .NET desiderato
		public T toObject<T>() {
			return JObject.Parse(JsonConvert.SerializeObject(this)).ToObject<T>();
		}
	}
}

