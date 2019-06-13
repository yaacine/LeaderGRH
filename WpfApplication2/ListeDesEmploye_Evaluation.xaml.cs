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
    /// Logique d'interaction pour ListeDesEmploye_Evaluation.xaml
    /// </summary>
    public partial class ListeDesEmploye_Evaluation : Page
    {
        public static int  index;
        public static List<Employe> liste = new List<Employe>();
        public ListeDesEmploye_Evaluation()
        {
            InitializeComponent();
        }
        private void Searche_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.AppStarting;
            if (Nom_filtre.IsSelected == true)
            {
                liste = GAdministrative.Recherche(nom: searche_info.Text);
                Datagridgraph.ItemsSource = liste;
            }
            else
            {
                if (prenom_filtre.IsSelected == true)
                {
                    liste = GAdministrative.Recherche(prenom: searche_info.Text);
                    Datagridgraph.ItemsSource = liste;
                }
            }
            Mouse.OverrideCursor = null;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    listeEmployesPourEvaluer.Width = (win as MainWindow).Main.Width;
                    listeEmployesPourEvaluer.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    barreDeRecherche.Width = (win as MainWindow).Main.Width +20;
                    Datagridgraph.Width = (win as MainWindow).Main.Width - 180;
                    Datagridgraph.Height = (win as MainWindow).Main.Height - 210;

                }
            }
            Mouse.OverrideCursor = Cursors.AppStarting;
            liste = GAdministrative.toutlesemploye();
            Datagridgraph.ItemsSource = liste;
            Mouse.OverrideCursor = null;
        }

        private void Datagridgraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListedesEvaluations.IsEnabled = true;
        }
        private void searche_info_KeyDown(object sender, KeyEventArgs e)
        {
            if (Nom_filtre.IsSelected == true)
            {
                liste = GAdministrative.Recherche(nom: searche_info.Text);
                Datagridgraph.ItemsSource = liste;
            }
            else
            {
                if (prenom_filtre.IsSelected == true)
                {
                    liste = GAdministrative.Recherche(prenom: searche_info.Text);
                    Datagridgraph.ItemsSource = liste;
                }
            }
        }

        private void ListedesEvaluations_Click(object sender, RoutedEventArgs e)
        {
            index = Datagridgraph.SelectedIndex;
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new HistoriqueDesEntretiens();
                    break;
                }
            }
        }

        private void Searche_Click(object sender, RoutedEventArgs e)
        {
            if (Nom_filtre.IsSelected == true)
            {
                liste = GAdministrative.Recherche(nom: searche_info.Text);
                Datagridgraph.ItemsSource = liste;
            }
            else
            {
                if (prenom_filtre.IsSelected == true)
                {
                    liste = GAdministrative.Recherche(prenom: searche_info.Text);
                    Datagridgraph.ItemsSource = liste;
                }
            }
        }
    }
}
