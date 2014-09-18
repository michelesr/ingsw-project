using System;
using System.Collections;

namespace project.Models {
    /* Questa classe non ha metodi ma viene utilizzata
       dal serializzatore JSON per gestire le città */
    /// Città 
    public class City : Model {
        /// Nome della città
        public String name { get; set;}
	}
}
