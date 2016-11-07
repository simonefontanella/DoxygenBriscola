using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fontanella.Simone._5i.Briscola { 
    class Deck
    {
      
        public List<Card> _deck = new List<Card>();
        public List<Card> _discardPile = new List<Card>();
        public int carteRimanenti { get; set; }


        // Inizializzo il Deck
        public Deck()
        {
            int count = 1;
            for (int i = 0; i < 40; i++)
            {
                if (count == 11)
                { count = 1; }
                _deck.Add(new Card(semeCarta(i), valoreCarta(i),count++,false));                            
            }
            carteRimanenti = 40;
            Shuffle();
            //_deck = _deck.OrderBy(x => Guid.NewGuid()).ToList();      //quando lo mischio non me le fa vedere
        }

        #region Valori e Semi
        public enum Suit { Bastoni = 0, Denari = 1, Coppe = 2, Spade = 3, Null = 4 }

        string semeCarta(int cardIndex)
        {
            if (cardIndex >= 0 && cardIndex <= 9)
                return Enum.GetName(typeof(Suit), 0);
            else if (cardIndex >= 10 && cardIndex <= 19)
                return Enum.GetName(typeof(Suit), 1);
            else if (cardIndex >= 20 && cardIndex <= 29)
                return Enum.GetName(typeof(Suit), 2);
            else if (cardIndex >= 30 && cardIndex <= 39)
                return Enum.GetName(typeof(Suit), 3);

            return Enum.GetName(typeof(Suit), 4);
        }

        int valoreCarta(int cardIndex)
        {
            cardIndex = cardIndex % 10;  // individuare la carta giusta in base alla posizione

            switch (cardIndex)
            {
                case 0:            // asso
                    return 11;
                    break;
                case 1:             //due
                    return 0;
                    break;
                case 2:             //tre
                    return 10;
                    break;
                case 3:             //quattro
                    return 0;
                    break;
                case 4:             //cinque
                    return 0;
                    break;
                case 5:             //sei
                    return 0;
                    break;
                case 6:             //sette
                    return 0;
                    break;
                case 7:             //fante
                    return 2;
                    break;
                case 8:             //cavallo   
                    return 3;
                    break;
                case 9:             //re
                    return 4;
                    break;
                default:
                    break;
            }
            return 0;
        }
        #endregion


        public Card GetFirstCard()
        {
            if (_deck.Count == 0)
                return null; // il deck è vuoto

            // prendo la prima carta dal deck e la aggiungo alla lista delle scartate
            Card card = _deck[0];
            _deck.RemoveAt(0);
            _discardPile.Add(card);
            carteRimanenti--;
            return card;
        }


        public List<Card> GetThreeCard()
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(this.GetFirstCard());
            }
            return cards;
        }


        //metodo per mischiare il deck
        void Shuffle()
        {
            Random rand = new Random(); 

            int n = _deck.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(0, n + 1);
                var temp = _deck[k];
                _deck[k] = _deck[n];
                _deck[n] = temp;
            }
        }
        
    }
}
