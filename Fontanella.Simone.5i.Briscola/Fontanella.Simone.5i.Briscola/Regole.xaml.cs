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
using System.Windows.Shapes;

namespace Fontanella.Simone._5i.Briscola
{
    /// <summary>
    /// Logica di interazione per Regole.xaml
    /// </summary>
    public partial class Regole : Window
    {
        public Regole()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader("Regole.txt");
                string a = sr.ReadToEnd();
                txtregole.Text = a;
            }
            catch(Exception err)
            { MessageBox.Show(err.Message); }
        }
    }
}
