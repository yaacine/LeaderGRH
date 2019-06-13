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

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Prochaine_Entretien.xaml
    /// </summary>
    /// 
    public partial class Prochaine_Entretien : Page
    {
        public static List<Evaluation> liste = null;
        public static List<Evaluation_Info> list  = null;
        public static int index;
        public Prochaine_Entretien()
        {
            InitializeComponent();
        }

        private void Searche_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void searche_info_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Modifier_Date_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Modifier_Date();
            win.ShowDialog();
        }

        private void Visualiser_Click(object sender, RoutedEventArgs e)
        {
            byte[] fichier = (byte[])liste.ElementAt(Datagridgraph.SelectedIndex).FichierEval.ToArray();
            if(File.Exists(@"C:\Windows\Temp\Visualiserr.xlsx"))
            {
                File.Delete(@"C:\Windows\Temp\Visualiserr.xlsx");
            }
            File.WriteAllBytes(@"C:\Windows\Temp\Visualiserr.xlsx", fichier);
            System.Diagnostics.Process.Start(@"C:\Windows\Temp\Visualiserr.xlsx");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    futuresEntretiensEvaluation.Width = (win as MainWindow).Main.Width;
                    futuresEntretiensEvaluation.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    barreDeRecherche.Width = (win as MainWindow).Main.Width + 20;
                    Datagridgraph.Width = (win as MainWindow).Main.Width - 180;
                    Datagridgraph.Height = (win as MainWindow).Main.Height - 210;
                }
            }
            liste = GEvaluation.EvalFuture();//retourne une liste des Evaluations futur
            list = GEvaluation.Evalfutureinfo(liste);
            Datagridgraph.ItemsSource = list;


        }

        private void Datagridgraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Visualiser.IsEnabled = true;
            Modifier_Date.IsEnabled = true;
            index = Datagridgraph.SelectedIndex;
            
        }

        private void futuresEntretiensEvaluation_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Filtre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
