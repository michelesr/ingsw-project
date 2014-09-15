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

        // costruttore che controlla i campi
        public ProductStock(int product_id, double price, int min, int max, int availability) : base() {
            Console.WriteLine("p_id " + product_id +   " price " + price + " min" + min + " max" + max +  "avai " + availability);
            if (min > 0 && min <= max && price > 0 && availability >= 0) {
                this.min = min;
                this.max = max;
                this.price = price;
                this.availability = availability;
                this.product_id = product_id;
            }
            else 
                throw new FormatException();
        }

        public override void update() {
            if (min < 0 || min > max || price <= 0 || availability < 0)
                throw new FormatException();
            base.update();
        }

        // controlla che l'user_id sia relativo al produttore del prodotto in stock
        public bool checkUserId(int uid) {
            return Model.getById<Product>(product_id).checkUserId(uid);
        }
	}
}

