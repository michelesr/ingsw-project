using System;

namespace project.Models
{
    public class ProductStock : Model
	{
        public int product_id { get; set; }
        public double price { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public int aviability { get; set; }

        public bool checkUserId(int uid) {
            return Model.getById<Product>(product_id).checkUserId(uid);
        }
	}
}

