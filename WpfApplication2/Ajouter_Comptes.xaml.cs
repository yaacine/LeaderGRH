using Microsoft.Win32;
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
using System.Windows.Shapes;
using WindowWPf;
namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Ajouter_Comptes.xaml
    /// </summary>
    public partial class Ajouter_Comptes : Window
    {
        public Ajouter_Comptes()
        {
            InitializeComponent();
        }
        private string Photo = null;
        private void Pseudo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pseudo.Text.Length < 6)
            {
                UserWarning.Visibility = Visibility.Visible;
            }
            else
            {
                UserWarning.Visibility = Visibility.Hidden;
            }
        }

        private void MotDePasse_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (MotDePasse.Password.Length < 6)
            {
                PassWarning.Visibility = Visibility.Visible;
            }

            else
            {
                PassWarning.Visibility = Visibility.Hidden;
            }
        }

        private void Confirmation_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Confirmation.Password.Equals(MotDePasse.Password))
            {
                ConfirWarning.Visibility = Visibility.Hidden;
            }

            else
            {
                ConfirWarning.Visibility = Visibility.Visible;
            }
        }

        private new void IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden)
            {
                Submit.IsEnabled = true;
            }
            else
            {
                Submit.IsEnabled = false;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string result = Parametres_Genereaux.AddUser(Pseudo.Text, MotDePasse.Password, Variables.userlist.Any() ? "Gérant" : "Administrateur", Questions1.Text, Questions2.Text, reponse1.Text, reponse2.Text,Photo);
            MessageBox.Show(result ?? "L'utilisateur a été ajouté", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void importer_photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir la photo de profil";
            try { op.Filter = "Photo(*.png )|*.png"; }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

            if (op.ShowDialog() == true)
            {
                photo_profil.Source = new BitmapImage(new Uri(op.FileName));
                Photo = op.FileName;
            }
        }
    }
}
