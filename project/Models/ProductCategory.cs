using System;
using System.Collections;

namespace project.Models {
    /* Questa classe non ha metodi ma viene utilizzata
       dal serializzatore JSON per gestire le città */
    /// Categoria di prodotti
	public class ProductCategory : Model {
        /// Nome della categoria
        public String name { get; set; }
	}
}
