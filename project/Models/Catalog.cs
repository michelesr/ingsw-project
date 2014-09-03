using System;
using project.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Collections;


namespace project.Models {
    // classe per l'esportazione dei listini dei produttori
    public class Catalog {
        // produttore, stock, categorie dei prodotti, città 
        public Supplier supplier { get; set; }
        public ArrayList stocks = new ArrayList();
        public ArrayList categories = new ArrayList();
        public String city;
        public ConvertibleHashtable[] products;
        // istanza del db
        private Database _db = Database.Istance;

        // costruttore, genera il listino
        public Catalog(int supplier_id) {
            // interroga il db per ottenere il supplier e i suoi prodotti
            supplier = Supplier.getByUserId(Supplier.getUserIdBySupplierId(supplier_id));
            products = _db.getData("Product", "supplier_id", supplier_id.ToString());

            // per ogni prodotto del produttore
            foreach (ConvertibleHashtable product in products) {
                // aggiunge gli stock relativi
                foreach (ConvertibleHashtable h in _db.getData("ProductStock", "product_id", product["id"].ToString())) 
                    stocks.Add(h);
                // aggiunge la categoria del prodotto se non è già presente
                foreach (ConvertibleHashtable h in _db.getData("ProductCategory", "id", product["product_category"].ToString())) {
                    bool isAlreadyIn = false;
                    foreach (ConvertibleHashtable c in categories)
                        if (c["id"].ToString() == h["id"].ToString())
                            isAlreadyIn = true;
                    if(!isAlreadyIn)
                        categories.Add(h);
                }
            }
            // ottiene la città del supplier
            city = Model.getById<City>(supplier.city).name;
        }

        // ritorna la stringa JSON del listino (i dati grezzi da esportare)
        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }

    }
}
