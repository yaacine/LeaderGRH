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
using WindowWPf;
namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {  public List<Entretient> list { get; set; }
        public static int index { get; set; }
        
        public Page2()
        {
            InitializeComponent();
        }

        private void DataGrid(object sender, EventArgs e)
        {
            list = GestionEntretien.TousLesEntretienFuture();
            Datagridgraph1.ItemsSource = list;    
        }

        
        private void Searche_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(searche_info.Text=="")  // cas ou la barre de recherche est vide ( charger routes les lignes
            {
                list = GestionEntretien.TousLesEntretienFuture();
                Datagridgraph1.ItemsSource = list;
            }
            else
            {
                Mouse.OverrideCursor = Cursors.AppStarting;
                if (nom.IsSelected == true)
                {
                    list = GestionEntretien.RechercheCandidat(nom: searche_info.Text);
                    Datagridgraph1.ItemsSource = list;
                }
                else
                {
                    if (prenom.IsSelected == true)
                    {
                        list = GestionEntretien.RechercheCandidat(prenom: searche_info.Text);
                        Datagridgraph1.ItemsSource = list;
                    }

                    if (Date.IsSelected == true)
                    {
                        try
                        {
                            list = GestionEntretien.RechercheCandidat(date: searche_info.Text);
                            Datagridgraph1.ItemsSource = list;
                        }
                        catch
                        {
                            MessageBox.Show("la format de la date est faux");
                        }

                    }
                }
                Mouse.OverrideCursor = null;
            }
            
        }

        private void searche_info_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            searche_info.Text = "";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window3 wind = new Window3();
            wind.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window4 wind = new Window4();
            wind.ShowDialog();

        }

        private void Datagridgraph1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            modif.IsEnabled = true;
            CV.IsEnabled = true;
            index = Datagridgraph1.SelectedIndex;
           
            if (DateTime.Today == list.ElementAt(index).DateEntretien ) 
            {
                Entretien.IsEnabled = true;
            }
            else
            {
                Entretien.IsEnabled = false;
            }
        }

        private void CV_Click(object sender, RoutedEventArgs e)
        {
            
          
            if (list.ElementAt(index).Candidat.cvCabdidat == null) { MessageBox.Show("le candidat n'a pas entrer un CV"); }
            else
            {
                var cv = (byte[])list.ElementAt(index).Candidat.cvCabdidat.ToArray();
                File.WriteAllBytes(@"C:\Windows\Temp\cv.docx", cv);
                System.Diagnostics.Process.Start(@"C:\Windows\Temp\cv.docx");
            }
        }

        private void listeEmployesPourEntretiens_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    listeEmployesPourEntretiens.Width = (win as MainWindow).Main.Width;
                    listeEmployesPourEntretiens.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    barreDeRecherche.Width = (win as MainWindow).Main.Width +20;
                    Datagridgraph1.Width = (win as MainWindow).Main.Width - 180;
                    Datagridgraph1.Height = (win as MainWindow).Main.Height - 210;
                }
            }
        }
    }
}
