using System;

namespace project.Utils
{
	public class Costants
	{
		public static ConvertibleHashtable unauthorized { get { 
				ConvertibleHashtable h = new ConvertibleHashtable ();
				h.Add ("error", "user_unauthorized");
				return h;
			} 
		}
	}
}

