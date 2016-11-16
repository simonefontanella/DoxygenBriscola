using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fontanella.Simone._5i.Briscola
{
    class GameMaster
    {
        public Deck deck { get; }
        public Player player;
        public Player cpu;
        public Card briscola;
        public Card cardCentroPlayer;
        public Card cardCentroCpu;
        public GameMaster()
        {
            deck = new Deck();
            player = new Player(deck.GetThreeCard(), true);
            cpu = new Player(deck.GetThreeCard(), false);
            briscola = deck.GetFirstCard();
            cardCentroPlayer = new Card();
            cardCentroCpu = new Card();
        }

        public int startTurn()
        {
            Random rand = new Random();
            int turn = rand.Next(0, 10);
            return turn;
        }
        public void ConfrontoCarte(Card card1, Card card2)
        {
            int punti = 0;
            int chiVince = CalcoloVincente(card1, card2);
            //sommo i punti delle due carte  e li do a chi vince
            punti = card1.valore + card2.valore;
            if (chiVince == 0)
            {
                player.isTurn = true;
                cpu.isTurn = false;
                player.punti += punti;
            }
            else
            {
                cpu.isTurn = true;
                player.isTurn = false;
                cpu.punti += punti;
            }

            player.handCards.RemoveAll(x => x == card1);
            cpu.handCards.RemoveAll(x => x == card2);
            if (deck.carteRimanenti != 0)
                AggiungiCarta();
        }
        public void AggiungiCarta()
        {
            if (player.isTurn)
            {
                //se il mazzo è finito allora dai la briscola al giocatore
                if (deck.carteRimanenti == 1)
                {
                    player.handCards.Add(deck.GetFirstCard());
                    cpu.handCards.Add(briscola);
                    return;
                }
                player.handCards.Add(deck.GetFirstCard());
                cpu.handCards.Add(deck.GetFirstCard());
            }
            else
            {
                //se il mazzo è finito allora dai la briscola al giocatore
                if (deck.carteRimanenti == 1)
                {
                    cpu.handCards.Add(deck.GetFirstCard());
                    player.handCards.Add(briscola);
                    return;
                }
                player.handCards.Add(deck.GetFirstCard());
                cpu.handCards.Add(deck.GetFirstCard());
            }
        }
        private int CalcoloVincente(Card card1, Card card2)  // calcolo dell carta vincente
        {
            // se il seme della prima carta e della seconda carta sono briscole
            if (card1.seme == briscola.seme && card2.seme == briscola.seme)
            {
                if (card1.valore == card2.valore)
                {
                    if (card1.cardIndex > card2.cardIndex)
                        return 0;
                    else
                        return 1;
                }
                if (card1.valore > card2.valore)
                    return 0;
                else
                    return 1;
            }

            //se la prima e la seconda carta non sono una briscola
            if (card1.seme != briscola.seme && card2.seme != briscola.seme)
            {
                //se il seme della prima carta è uguale a quello della seconda
                if (card1.seme == card2.seme)
                {
                    if (card1.valore == card2.valore)
                    {
                        if (card1.cardIndex > card2.cardIndex)
                            return 0;
                        else
                            return 1;
                    }
                    if (card1.valore > card2.valore)
                        return 0;
                    else
                        return 1;
                }

                //bisogna considerare però che se i due giocatori tirano due lisci vince chi ha iniziato il turno
                if (card1.seme != card2.seme)
                {
                    if (player.isTurn)
                    {
                        return 0;
                    }
                    else
                        return 1;
                }
            }

            //se la prima carta è una briscola e la seconda no
            if (card1.seme == briscola.seme)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int CpuBrain()
        {

            //Se P gioca una briscola il bot gioca liscio o carico minore o se costretto briscola minore
            //Se P gioca liscio il bot controlla se ha un carico per prendere con lo stesso seme del liscio o liscio
            //Se P gioca un carico il bot cerca di prendere con la briscola più bassa o se seupera con un carico dello stesso seme più grande

            int puntiCartaPlayer = cardCentroPlayer.valore;
            string semeBriscola = briscola.seme;
            Card giocaQuesta = new Card();

            if (cpu.handCards.Count == 1)
            {
                return 0;
            }

            if (cardCentroPlayer.seme == semeBriscola)
            {
                return GetCartaMinore(cpu.handCards);
            }

            if (cardCentroPlayer.seme != semeBriscola && cardCentroPlayer.valore == 0)
            {
                return GetCartaMaggiore(cpu.handCards);
            }

            if (cardCentroPlayer.valore >= 4)
            {

                return GetCartaMaggiore(cardCentroPlayer, cpu.handCards);
            }

            #region Backup
            /*if (player.isTurn)
            {
                puntiCartaPlayer = cardCentroPlayer.valore;
                for (int i = 0; i < cpu.handCards.Count; i++)
                {
                    //se la cpu ha una briscola e il player ha tirato una carta che ha come valore > 0
                    if (cpu.handCards[i].seme == semeBriscola && puntiCartaPlayer > 0)
                    {
                        return i;
                    }
                    //se la cpu non ha la briscola o la carta del player ha valore 0
                }
                int cartaMinore = cpu.handCards[0].valore;
                int count = 0;
                for (int j = 1; j < cpu.handCards.Count; j++)
                {
                    if (cartaMinore > cpu.handCards[j].valore)
                    {
                        cartaMinore = cpu.handCards[j].valore;
                        count++;
                    }
                    
                }
                return count;
            }
            else
            {
                for (int i = 0; i < cpu.handCards.Count; i++)
                {
                    //se la cpu ha una briscola
                    if (cpu.handCards[i].seme == briscola.seme)
                    {
                        return i;
                    }
                }
            }*/
            #endregion

            return 0;
        }

        int GetCartaMaggiore(Card cartaGiocatore, List<Card> cards)
        {
            Card cartaMaggiore = new Card();
            foreach (var item in cards)
            {
                if(item.seme == cartaGiocatore.seme && item.valore > cartaGiocatore.valore)
                {
                    if(cartaMaggiore.valore < item.valore)
                        cartaMaggiore = item;
                }
                
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (cartaMaggiore == cards[i])
                    return i;
            }
            return 0;
            
        }
        int GetCartaMinore(List<Card> cards)
        {
            Card cartaMaggiore = new Card();
            foreach (var item in cards)
            {
                if (item.valore < cartaMaggiore.valore)
                {
                    cartaMaggiore = item;
                }
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (cartaMaggiore == cards[i])
                    return i;
            }
            return 0;
        }
        int GetCartaMaggiore(List<Card> cards)
        {
            Card cartaMaggiore = new Card();
            foreach (var item in cards)
            {
                if (item.valore > cartaMaggiore.valore)
                {
                    cartaMaggiore = item;
                }
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (cartaMaggiore == cards[i])
                    return i;
            }
            return 0;
        }
    }
}


