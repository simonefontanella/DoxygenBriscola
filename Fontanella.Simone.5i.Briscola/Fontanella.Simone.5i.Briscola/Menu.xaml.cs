using System;
using System.Collections.Generic;
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
    /// Logica di interazione per Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow giocoWindow = new MainWindow();
            this.Hide();
            giocoWindow.ShowDialog();
            Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Regole regoleWindow = new Regole();
            regoleWindow.ShowDialog();
            Show();
        }
    }
}
