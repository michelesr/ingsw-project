using System;

namespace project.Models
{
	public class Supplier : User
	{
		public Supplier (String email, String password, String nome, String cognome) : base (email, password, nome, cognome)
		{
			// TODO
			// - il fornitore ha una lista di prodotti
			// - il fornitore contiene degli stock (prezzo, quantita'..)
			//	- i venditori devono poter aggiungere/cambiare/togliere prodotti dai listini
			//	- i venditori devono poter cambiare il prezzo dei prodotti
			//	- I venditori devono poter cambiare le proprie informazioni anagrafiche
			//	- cambiare la disponibilità
			//	- Pubblicare il listino
			//	- Verificare Lo stato di aggiornamento del loro listino locale con quello pubblico
		}
	}
}

