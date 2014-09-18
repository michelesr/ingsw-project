using System;

namespace project.Models
{
    /// Stock (fornitura) 
    public class ProductStock : Model
	{
        /// L'id del prodotto in stock
        public int product_id { get; set; }
        /// Prezzo
        public double price { get; set; }
        /// Quantità minima acquistabile
        public int min { get; set; }
        /// Quantità massima acquistabile
        public int max { get; set; }
        /// Disponibilità del prodotto nello stock
        public int availability { get; set; }

        /** Costruttore dello Stock, restituisce una FormatException se i
            campi non sono validi (valori validi: price > 0, min < 0, 
            max > min, availability >= 0) */
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

        /** Aggiorna se i campi forniti sono validi
           (valori validi: price > 0, min < 0, 
            max > min, availability >= 0) */ 
        public override void update() {
            if (min < 0 || min > max || price <= 0 || availability < 0)
                throw new FormatException();
            base.update();
        }

        /// Ritorna true se l'uid (user_id) fornito è relativo al produttore dello stock
        public bool checkUserId(int uid) {
            return Model.getById<Product>(product_id).checkUserId(uid);
        }
	}
}

