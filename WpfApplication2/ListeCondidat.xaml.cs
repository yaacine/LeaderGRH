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
    /// Logique d'interaction pour ListeCondidat.xaml
    /// </summary>
    public partial class ListeCondidat : Page
    {
        public ListeCondidat()
        {
            InitializeComponent();
        }
        private void Searche_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Nom_filtre.IsSelected == true)
            {
                if (IsChar(searche_info.Text))
                {
                    var liste = GCondidat.RechercheCondidat(searche_info.Text, 1);
                    Datagridgraph.ItemsSource = liste;
                }
                else
                {
                    MessageBox.Show("Veuillez entrez une chaine de caractères  !");
                }
            }
            else if (Prenom_filtre.IsSelected == true)
            {
                if (IsChar(searche_info.Text))
                {
                    var liste = GCondidat.RechercheCondidat(searche_info.Text, 2);
                    Datagridgraph.ItemsSource = liste;
                }
                else
                {
                    MessageBox.Show("Veuillez entrez une chaine de caractères !");
                }
            }
            else if (Poste_filtre.IsSelected == true)
            {
                if (IsChar(searche_info.Text))
                {
                    var liste = GCondidat.RechercheCondidat(searche_info.Text, 3);
                    Datagridgraph.ItemsSource = liste;
                }
                else
                {
                    MessageBox.Show("Veuillez entrez une chaine de caractères !");
                }
            }
        }




        private void Datagridgraph_Initialized(object sender, EventArgs e)
        {

            var liste = GCondidat.tousLesCandidats();
            Datagridgraph.ItemsSource = liste;
        }


        private bool IsNumeric(string Nombre)
        {
            bool k = true;
            for (int i = 0; i < Nombre.Length; i++)
            {
                if (Nombre[i] < 48 || Nombre[i] > 57)
                {
                    k = false;
                }
            }
            return k;
        }

        private bool IsChar(string Chaine)
        {
            bool k = true;
            for (int i = 0; i < Chaine.Length; i++)
            {
                if (Chaine[i] >= 48 && Chaine[i] <= 57)
                {
                    k = false;
                }
            }
            return k;
        }

        private void Datagridgraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlanifierButton.IsEnabled = true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AjouterCondidat ee = new AjouterCondidat();
            ee.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            PlanifierEntretien aa = new PlanifierEntretien((Candidat)Datagridgraph.SelectedItem);
            aa.ShowDialog();
        }

        private void listeCandidat_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    listeCandidat.Width = (win as MainWindow).Main.Width;
                    listeCandidat.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    barreDeRecherche.Width = (win as MainWindow).Main.Width + 20;
                    Datagridgraph.Width = (win as MainWindow).Main.Width - 180;
                    Datagridgraph.Height = (win as MainWindow).Main.Height - 210;
                }
            }
        }
    }
}
