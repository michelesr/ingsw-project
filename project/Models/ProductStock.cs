using System;

namespace project.Models
{
    // classe rappresentate gli stock di prodotti
    public class ProductStock : Model
	{
        // prodotto in vendita e prezzo
        public int product_id { get; set; }
        public double price { get; set; }
        // quantità minima e massima acquistabile
        public int min { get; set; }
        public int max { get; set; }
        // disponibilità
        public int availability { get; set; }

        // controlla che l'user_id sia relativo al produttore del prodotto in stock
        public bool checkUserId(int uid) {
            return Model.getById<Product>(product_id).checkUserId(uid);
        }
	}
}

