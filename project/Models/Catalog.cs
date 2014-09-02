using System;
using project.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Collections;


namespace project.Models {
    public class Catalog {
        public Supplier supplier { get; set; }
        public ArrayList stocks = new ArrayList();
        public ArrayList categories = new ArrayList();
        public String city;
        public ConvertibleHashtable[] products;
        private Database _db = Database.Istance;

        public Catalog(int supplier_id) {
            supplier = Supplier.getByUserId(Supplier.getUserIdBySupplierId(supplier_id));
            products = _db.getData("Product", "supplier_id", supplier_id.ToString());
            foreach (ConvertibleHashtable product in products) {
                foreach (ConvertibleHashtable h in _db.getData("ProductStock", "product_id", product["id"].ToString())) 
                    stocks.Add(h);
                foreach (ConvertibleHashtable h in _db.getData("ProductCategory", "id", product["product_category"].ToString())) {
                    bool isAlreadyIn = false;
                    foreach (ConvertibleHashtable c in categories)
                        if (c["id"].ToString() == h["id"].ToString())
                            isAlreadyIn = true;
                    if(!isAlreadyIn)
                        categories.Add(h);
                }
            }
            city = Model.getById<City>(supplier.city).name;
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }

    }
}
