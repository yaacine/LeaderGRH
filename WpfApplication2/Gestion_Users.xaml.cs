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
using WindowWPf;
using static WindowWPf.Login;

namespace WpfApplication2
{
    using Microsoft.Win32;

    /// <summary>
    /// Logique d'interaction pour Gestion_Users.xaml
    /// </summary>
    using static Variables;
    public partial class Gestion_Users : Page
    {
        public Gestion_Users()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<comptes> newlist = new List<comptes>();
            comptes cpt = new comptes();
            Datagridgraph.ItemsSource = userlist;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                MessageBox.Show(op.FileName);
            }
        }
        
        private void Butto_Modifier_Copy_Click(object sender, RoutedEventArgs e)
        {
            Ajouter_Comptes window = new Ajouter_Comptes();
            window.Show();
        }

        private void Actualiser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Datagridgraph.ItemsSource = userlist;
        }

        private void Datagridgraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Butto_Modifier.IsEnabled = true;
        }

        private void Butto_Modifier_Click(object sender, RoutedEventArgs e)
        {
            
            foreach(Window win in App.Current.Windows)
            {
                
                if (win.Title.Equals("GRH"))
                {
                    Variables.index = Datagridgraph.SelectedIndex;
                    (win as MainWindow).Main.Content = new ModifierCompte(true);
                    break;
                }
            }
            
        }
    }
    public class comptes
    {
        public Users user;
        public String imagesource;
    }
}
