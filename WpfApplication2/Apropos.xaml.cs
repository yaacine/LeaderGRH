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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Apropos.xaml
    /// </summary>
    public partial class Apropos : Page
    {
        public Apropos()
        {
            InitializeComponent();
        }

        private void Apropos_Loaded1(object sender, RoutedEventArgs e)
        {
            foreach (System.Windows.Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    Apropos2.Width = (win as MainWindow).Main.Width;
                    Apropos2.Height = (win as MainWindow).Main.Height;
                    groupe.Width = (win as MainWindow).Main.Width - 150;
                    groupe.Height = (win as MainWindow).Main.Height - 220;
                    titre.Width = (win as MainWindow).Main.Width - 140;


                }
            }
        }
    }
}
