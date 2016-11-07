using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fontanella.Simone._5i.Briscola
{
    class Card
    {
        public string seme { get; set; }
        public int valore { get; set; }
        public bool isGiocata { get; set; }
        public bool isFaceUp { get; set; }
        public BitmapImage img { get; set; }
        public int cardIndex { get; set; }    
        
        public BitmapImage imgRetroCarta { get; }

        public string path { get; set; }
        public Card() { }
        public Card(string _seme, int cardValore, int _cardIndex, bool isFace)
        {
            seme = _seme;
            valore = cardValore;
            cardIndex = _cardIndex;
            isFaceUp = isFace;
            img= new BitmapImage(new Uri("/Immagini/" + seme + _cardIndex.ToString() + ".png", UriKind.Relative));
            path = "/Immagini/" + seme + _cardIndex.ToString() + ".png";
            imgRetroCarta = new BitmapImage(new Uri("/Immagini/retroCarta.png", UriKind.Relative));
        }
    }
}
