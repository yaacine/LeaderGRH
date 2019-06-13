using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Modifier_Employe.xaml
    /// </summary>
    public partial class Modifier_Employe : Page
    {
        public static List<Employe> liste = new List<Employe>();
        public static string numim;
        public static List<Employe> liste_detaille = new List<Employe>();
        public static int index_detaille { get; set; }
        public static int index_modifier { get; set; }


        public Modifier_Employe()
        {
            InitializeComponent();
        }
        public static byte[] cv;
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
        private void Searche_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(searche_info.Text=="")  // cas ou  la barre de recherche est vide (recharger tous les emlpoyes
            {
                liste = GAdministrative.toutlesemploye();
                Datagridgraph.ItemsSource = liste;
            }
            else
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
            
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            liste_detaille = GAdministrative.toutlesemploye();
            Datagridgraph.ItemsSource = liste_detaille;
            foreach (Window win in App.Current.Windows)
            {
                if(win.Title.Equals("GRH"))
                {
                    listeDesEmployes.Width = (win as MainWindow).Main.Width;
                    listeDesEmployes.Height = (win as MainWindow).Main.Height;
                    titre.Width= (win as MainWindow).Main.Width-140;
                    barreDeRecherche.Width= (win as MainWindow).Main.Width+20 ;
                    Datagridgraph.Width= (win as MainWindow).Main.Width - 80;
                    Datagridgraph.Height= (win as MainWindow).Main.Height - 210;

                }
            }
            
            Mouse.OverrideCursor = Cursors.AppStarting;
            liste = GAdministrative.toutlesemploye();
            Datagridgraph.ItemsSource = liste;
            Mouse.OverrideCursor = null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            index_detaille = Datagridgraph.SelectedIndex;
            if (index_detaille<0)
            {
                MessageBox.Show("Veillez selectionner Un Employe", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.Title.Equals("GRH"))
                    {
                        (win as MainWindow).Main.Content = new Detaille_Employe();
                    }
                }
            }
            

        }
        private void Cv_button_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllBytes(@"C:\Windows\Temp\cv.docx", cv);
            System.Diagnostics.Process.Start(@"C:\Windows\Temp\cv.docx");     
        }
        
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            index_detaille = Datagridgraph.SelectedIndex;
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new ModifierEmploye();
                }
            }
        }
        

        private void Datagridgraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_detaille.IsEnabled = true;
            Butto_Modifier.IsEnabled = true;
            
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
        

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 wind = new Window1();
            wind.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window2 wind = new Window2();
            wind.ShowDialog();
        }

        private void Supprimer_Button_Click(object sender, RoutedEventArgs e)
        {
           /* Employe emp = (Employe)Datagridgraph.SelectedItem;
            int matricule = emp.Matricule;
            string message = "Etes-Vous sur de vouloir supprimer l’employé " + emp.Nom + " " + emp.Prenom + "?";

            MessageBoxResult result =MessageBox.Show(message, "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result== MessageBoxResult.Yes)
            {
                GAdministrative.supprimer(matricule);
                liste = GAdministrative.toutlesemploye();
                Datagridgraph.ItemsSource = liste;

            }
           */

        }
    }
}

