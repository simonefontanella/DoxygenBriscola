using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fontanella.Simone._5i.Briscola
{
    class Player
    { 
        public List<Card> handCards = new List<Card>();
        public bool isTurn = false;
        public int punti = 0;
        public Player()
        { }

        public Player(List<Card> listCard,bool turn)
        {
            handCards = listCard;
            foreach (var item in listCard)
            {
                item.isFaceUp = true;
            }
            isTurn = turn;
            punti = 0;
        }

      /*  public int CpuBrain()
        {
            foreach (var item in handCards)
            {

            }
        }*/

        public void addCarta(Card card)
        {
            handCards.Add(card);
        }
    }
}
