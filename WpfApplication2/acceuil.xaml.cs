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
    /// Logique d'interaction pour acceuil.xaml
    /// </summary>
    public partial class acceuil : Page
    {
        public acceuil()
        {
            InitializeComponent();
        }

      

        private void Annuaire_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                   Annuaire.Width = (win as MainWindow).Main.Width;
                    Annuaire.Height = (win as MainWindow).Main.Height;
                    racourcis.Width= (win as MainWindow).Main.Width-450;
                    racourcis.Height= (win as MainWindow).Main.Height-100;
                    sitation.Height= (win as MainWindow).Main.Height-100;


                }
            }
        }

        private void listeemployes_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if(win.Title.Equals("GRH"))
                {
                    
                    (win as MainWindow).Main.Content = new Modifier_Employe();
                }
            }
        }

        private void listedesconges_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {

                    (win as MainWindow).Main.Content = new ListeCongés();
                }
            }
        }

        private void evaluation_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new ListeDesEmploye_Evaluation();
                }
            }
        }

        private void embauche_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new Page2();
                }
            }
           
        }

        private void annuaire_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new Annuaire();
                }
            }
        }

        private void aProposButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new Apropos();
                }
            }
        }
    }
}
