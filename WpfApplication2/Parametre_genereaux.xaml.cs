using Microsoft.Win32;
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
    /// Logique d'interaction pour Parametre_genereaux.xaml
    /// </summary>
    public partial class Parametre_genereaux : Page
    {
        public static string path = null;
        public Parametre_genereaux()
        {
            InitializeComponent();
        }

        private void textBlock2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Image_logo.Source = new BitmapImage(new Uri(op.FileName));
                path = op.FileName;
            }
        }

        private void Parametre_appliquer_Click(object sender, RoutedEventArgs e)
        {
            Parametres_Genereaux.ModifierParametres(Param_Raison_social.Text, Param_Specialite.Text, Param_Site.Text, Param_Gerant.Text, null, Param_Adresse.Text,
                Param_Telephone.Text, Param_Fax.Text, null, Parame_RC.Text, Param_Id_fiscale.Text, path, null);
            MessageBox.Show("Parametres Modifies avec succes.");
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Footer_Adresse.Content = Param_Adresse.Text;
                    (win as MainWindow).Footer_Fax.Content = Param_Fax.Text;
                    (win as MainWindow).Footer_Gerant.Content = Param_Gerant.Text;
                    (win as MainWindow).Footer_Mail.Content = Param_Email.Text;
                    (win as MainWindow).Footer_Telephone.Content = Param_Telephone.Text;
                    (win as MainWindow).Image_entreprise.Source = new BitmapImage(new Uri(path));
                }
            }
        }

        private void Theme_page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    param_generaux_page.Width = (win as MainWindow).Main.Width;
                    param_generaux_page.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    Parametres_genereaux.Width = (win as MainWindow).Main.Width - 100;
                    Parametres_genereaux.Width = (win as MainWindow).Main.Width - 100;
                }
            }
            var param = Variables.db.Parametres?.First();
            if (param != null)
            {
                Param_Adresse.Text = param.Adresse;
                Param_Email.Text = param.MailGerant;
                Parame_RC.Text = param.NumeroRC;
                Param_Fax.Text = param.Fax;
                Param_Gerant.Text = param.NomGerant;
                Param_Raison_social.Text = param.Raison_Sociale;
                Param_Site.Text = param.Site_Web;
                Param_Specialite.Text = param.Speciaite;
                Param_Id_fiscale.Text = param.IdFiscale;
               
                File.WriteAllBytes($@"C:\Windows\Temp\logo_entreprise_new{Variables.cpt}.png", param.Logo.ToArray());
                path = $@"C:\Windows\Temp\logo_entreprise_new{Variables.cpt}.png";
                Variables.cpt++;
                Image_logo.Source = new BitmapImage(new Uri(path));
            }
        }
    }
}
