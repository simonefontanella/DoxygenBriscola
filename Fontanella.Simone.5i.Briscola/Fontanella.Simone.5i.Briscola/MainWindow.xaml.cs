using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Threading;

namespace Fontanella.Simone._5i.Briscola
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private GameMaster game;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        private void StartGame()
        {
            game = new GameMaster();
            SettaImmagini();
            txtVittoria.Visibility = Visibility.Hidden;
            txtPlayerPunti.Text = "Punti: " + game.player.punti.ToString();
            txtCpuPunti.Text = "Punti: " + game.cpu.punti.ToString();
            btnPlayerCard1.IsEnabled = true;
            btnPlayerCard2.IsEnabled = true;
            btnPlayerCard3.IsEnabled = true;
            txtcard.Text = game.deck.carteRimanenti.ToString();
        }
        // tavolo[0] carta Player , tavolo [1] carta Cpu
        public void MossaCpu()
        {
            //intelligenzza Cpu che determina quale carta giocare
            int indexCardCpu = 0;
            indexCardCpu = game.CpuBrain();
            if (indexCardCpu == 3)
                MessageBox.Show("error");

            if (game.cpu.isTurn)
            {              
               game.cardCentroCpu = game.cpu.handCards[indexCardCpu];
               return;
            }
            game.cardCentroCpu = game.cpu.handCards[indexCardCpu];
            ConfrontoCarteAlCentro();
        }
        private void MostraCarteAlCentro()
        {
            VisualizzaCartaCentro1();
            VisualizzaCartaCentro2();           
        }
        void MossaSuccessiva()
        {
            //se il gioco è terminato la partita finisce
            if (gameEnded())
                return;

            if (game.cpu.isTurn)
            {
               // MessageBox.Show("Turno della Cpu");
                MossaCpu();
            }
            else
            {
                // MessageBox.Show("Turno mio");
                return;
            }
        }
        async private void ConfrontoCarteAlCentro()
        {
            btnPlayerCard1.IsEnabled = false;
            btnPlayerCard2.IsEnabled = false;
            btnPlayerCard3.IsEnabled = false;
            VisualizzaCartaCentro1();
            if (game.player.isTurn)
            {
                await Task.Delay(800);
                VisualizzaCartaCentro2();
            }
            game.ConfrontoCarte(game.cardCentroPlayer, game.cardCentroCpu);
            txtcard.Text = game.deck.carteRimanenti.ToString();
            AggiornaImmagini();
            MossaSuccessiva();
        }      
        void MostraPunti()
        {
            //metodo per mostrare i punti dei due giocatori
            txtPlayerPunti.Text = "Punti: " + game.player.punti.ToString();
            txtCpuPunti.Text = "Punti: " + game.cpu.punti.ToString();
            //txtcard.Text = game.deck.carteRimanenti.ToString();
        }
        public void EndGame()
        {
            if (gameEnded())
            {
                if (game.player.punti == game.cpu.punti)
                {
                    txtVittoria.Text = "Pareggio!";
                    txtVittoria.Visibility = Visibility.Visible;
                    //MessageBox.Show("Pareggio");
                }
                else if (game.player.punti > game.cpu.punti)
                {
                    txtVittoria.Text = "Hai vinto!";
                    txtVittoria.Visibility = Visibility.Visible;
                    //MessageBox.Show("Vince Player");
                }
                else
                {
                    txtVittoria.Text = "Hai Perso!";
                    txtVittoria.Visibility = Visibility.Visible;
                }

            }
            return;
        }
        public async void AggiornaImmagini()
        {
            //dopo ogni turno aggiorno le immagini con le nuove carte         
            await Task.Delay(1000);
            RimuoviCarteCentro();
            MostraPunti();
            if (game.cpu.handCards.Count >= 3 && game.player.handCards.Count >= 3)
            {
                btnPlayerCard1.Source = game.player.handCards[0].img;
                btnPlayerCard2.Source = game.player.handCards[1].img;
                btnPlayerCard3.Source = game.player.handCards[2].img;
                btnCpuCard1.Source = game.cpu.handCards[0].imgRetroCarta;
                btnCpuCard2.Source = game.cpu.handCards[1].imgRetroCarta;
                btnCpuCard3.Source = game.cpu.handCards[2].imgRetroCarta;             
            }
            if (game.cpu.isTurn)
            {
                await Task.Delay(800);
                VisualizzaCartaCentro2();
                btnPlayerCard1.IsEnabled = true;
                btnPlayerCard2.IsEnabled = true;
                btnPlayerCard3.IsEnabled = true;
            }

            if (game.deck.carteRimanenti == 0)
                UltimoTurno();
            

            // decreta il vincitore
            if (gameEnded())
                EndGame();
        }
        private void RimuoviCarteCentro()
        {        
            //metodo per rimuovere le immagini dal centro del tavolo
            btnCardCentro1.Source = null;
            btnCardCentro2.Source = null;
            if (game.player.isTurn)
            {
                btnPlayerCard1.IsEnabled = true;
                btnPlayerCard2.IsEnabled = true;
                btnPlayerCard3.IsEnabled = true;
            }
        }
        private void VisualizzaCartaCentro1()
        {
            if (btnCardCentro2.Source == null)
            {
                btnCardCentro2.Source = game.cardCentroPlayer.img;

                if (game.cardCentroPlayer.img == btnPlayerCard1.Source)
                    btnPlayerCard1.Source = null;
                if (game.cardCentroPlayer.img == btnPlayerCard2.Source)
                    btnPlayerCard2.Source = null;
                if (game.cardCentroPlayer.img == btnPlayerCard3.Source)
                    btnPlayerCard3.Source = null;
            }
        }
        private void VisualizzaCartaCentro2()
        {          
            if (btnCardCentro1.Source == null)
            {
                if (game.cpu.handCards.Count == 0)
                    return;
                btnCardCentro1.Source = game.cardCentroCpu.img;

                #region UltimoTurno
                if (game.cpu.handCards.Count == 1)
                {
                    btnCpuCard1.Source = null;
                    btnCpuCard2.Source = null;
                    btnCpuCard3.Source = null;
                    return;
                }

                if (game.cpu.handCards.Count == 2)
                {
                    if(btnCpuCard1.Source != null)
                    {
                        btnCpuCard2.Source = null;
                        btnCpuCard3.Source = null;
                    }
                    else if (btnCpuCard2.Source != null)
                    {
                        btnCpuCard1.Source = null;
                        btnCpuCard3.Source = null;
                    }
                    if (btnCpuCard3.Source != null)
                    {
                        btnCpuCard1.Source = null;
                        btnCpuCard2.Source = null;
                    }
                    return;
                }
                #endregion

                if (game.cardCentroCpu.img == game.cpu.handCards[0].img)
                    btnCpuCard1.Source = null;
                if (game.cardCentroCpu.img == game.cpu.handCards[1].img)
                    btnCpuCard2.Source = null;
                if (game.cardCentroCpu.img == game.cpu.handCards[2].img)
                    btnCpuCard3.Source = null;
            }
        }
        void UltimoTurno()
        {
            btnDeck.Source = null;
            btnBriscola.Source = null;
        }    
        public void SettaImmagini()
        {
            //setto tutte le immagini delle carte all' inizio della partita
            btnBriscola.Source = game.briscola.img;
            btnPlayerCard1.Source = game.player.handCards[0].img;
            btnPlayerCard2.Source = game.player.handCards[1].img;
            btnPlayerCard3.Source = game.player.handCards[2].img;
            btnCpuCard1.Source = game.cpu.handCards[0].imgRetroCarta;
            btnCpuCard2.Source = game.cpu.handCards[1].imgRetroCarta;
            btnCpuCard3.Source = game.cpu.handCards[2].imgRetroCarta;
            btnDeck.Source = game.briscola.imgRetroCarta;
            btnCardCentro1.Source = null;
            btnCardCentro2.Source = null;
        }      
        private void btnPlayerCard1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            game.cardCentroPlayer = game.player.handCards[0];
            if (game.player.isTurn)
            {    
                MossaCpu();
            }
            else
            {
                ConfrontoCarteAlCentro();
            }
        }
        private void btnPlayerCard2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game.player.handCards.Count < 3)
            {
                int index = MetodoSupporto();
                game.cardCentroPlayer = game.player.handCards[1 - index];
            }
            else
            {
                game.cardCentroPlayer = game.player.handCards[1];
            }
            if (game.player.isTurn)
            {                        
                MossaCpu();
            }
            else
            {
                ConfrontoCarteAlCentro();
            }
        }
        private void btnPlayerCard3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game.player.handCards.Count < 3)
            {
                int index = MetodoSupporto();
                game.cardCentroPlayer = game.player.handCards[2 - index];
            }
            else
            {
                game.cardCentroPlayer = game.player.handCards[2];
            }

            if (game.player.isTurn)
            {
                MossaCpu();
            }
            else
            { 
                ConfrontoCarteAlCentro();
            }
        }
        int MetodoSupporto() // mi ritorna l' indice della carta che devo andare a prendere visto che essendo una lista le carte si spostano tutte di un posto quando ne vado a togliere una ( questo succede solo nell' ultima giocata)
        {
            if (btnPlayerCard3.Source == null && btnPlayerCard1.Source == null)
                return 1;
            if (btnPlayerCard3.Source == null)
                return 0;
            if(btnPlayerCard1.Source == null && btnPlayerCard2.Source == null)
                return 2;
            if (btnPlayerCard1.Source == null || btnPlayerCard2.Source == null || btnPlayerCard3.Source == null)
                return 1;
            return 0;
        }
        bool gameEnded()
        {
            if (game.cpu.handCards.Count == 0 && game.player.handCards.Count == 0)
                return true;
            else
                return false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vuoi iniziare una nuova partita?", "Inizia partita", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                game = new GameMaster();
                StartGame();
            }
        }
    }
}




















































































































































































































































































































































































//code by Fontanella Simone
