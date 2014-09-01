using System;

namespace project.Models
{
    public class ProductStock : Model
	{
        int id_product { get; set; }
        double price { get; set; }
        int min { get; set; }
        int max { get; set; }
        int aviability { get; set; }
	}
}

