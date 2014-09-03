using System;
using System.Collections;

namespace project.Models {
    // questa classe di per se sembra inutile ma viene utilizzata
    // dal serializzatore e dal db per gestire le categorie
    // classe rappresentante le categorie di prodotti
	public class ProductCategory : Model {
        // attributi
        public String name { get; set; }
	}
}
