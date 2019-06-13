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
    /// Logique d'interaction pour Theme.xaml
    /// </summary>
    public partial class Theme : Page
    {
        public Theme()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    Theme_page.Width = (win as MainWindow).Main.Width;
                    Theme_page.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    formulaire.Width = (win as MainWindow).Main.Width - 100;
                    formulaire.Width = (win as MainWindow).Main.Width - 100;
                }
            }
        }

        private void Bleu_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).ApplyBleu();
                    (win as MainWindow).Bleu.IsChecked = true;
                    Parametres_Genereaux.ModifyUserTheme((win as MainWindow).NomUser.Content.ToString(), "Bleu");
                }
            }
        }

        private void Vert_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).ApplyVert();
                    (win as MainWindow).Vert.IsChecked = true;
                    Parametres_Genereaux.ModifyUserTheme((win as MainWindow).NomUser.Content.ToString(), "Vert");
                }
            }
        }

        private void Marron_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).ApplyMarron();
                    (win as MainWindow).Marron.IsChecked = true;
                    Parametres_Genereaux.ModifyUserTheme((win as MainWindow).NomUser.Content.ToString(), "Marron");
                }
            }
        }
    }
}
