using System;
using System.Collections;

namespace project.Models {
    // questa classe di per se sembra inutile ma viene utilizzata
    // dal serializzatore e dal db per gestire le città
    // classe rappresentante le città dei produttori
    public class City : Model {
        // attributi
        public String name { get; set;}
	}
}
