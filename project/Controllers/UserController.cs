using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using project.Models;
using Newtonsoft.Json;

namespace project.Controllers {

	// /api/user
	public class UserController : Controller {

		// GET: /api/user/
		// GET: /api/user/list/
		public String List() {
			return JsonConvert.SerializeObject(Models.User.getAll());
		}

		// GET: /api/user/1/detail/
		public String Detail(int id) {
			return JsonConvert.SerializeObject(Models.User.getById(id));
		}

		// POST: /api/user/add/
		//[AcceptVerbs(HttpVerbs.Post)]
		public String Add(String data) {
			Hashtable h = JsonConvert.DeserializeObject<Hashtable>(data);
			return JsonConvert.SerializeObject(h);
		}
	}
}
