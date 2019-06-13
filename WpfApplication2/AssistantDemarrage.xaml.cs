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
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Timers;
using System.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour AssistantDemarrage.xaml
    /// </summary>
    public partial class AssistantDemarrage : Window
    {
        string cheminlogo;
        bool logochoisie = false;

        public AssistantDemarrage()
        {
           
            InitializeComponent();
            
        }

        private void wizardControl_Help(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start($@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Resources\noticeUtilisation.pdf");
        }

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

            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden
                && Questions1.Text != "" && Questions2.Text != "" && reponse1.Text != "" && reponse2.Text != "" )
            {
                wizPage2.NextEnabled = true;
            }

            else
            {
                wizPage2.NextEnabled = false;
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

            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden
               && Questions1.Text != "" && Questions2.Text != "" && reponse1.Text != "" && reponse2.Text != "")
            {
                wizPage2.NextEnabled = true;
            }

            else
            {
                wizPage2.NextEnabled = false;
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

            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden
               && Questions1.Text != "" && Questions2.Text != "" && reponse1.Text != "" && reponse2.Text != "")
            {
                wizPage2.NextEnabled = true;
            }

            else
            {
                wizPage2.NextEnabled = false;
            }
        }

    

        private void reponse1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden
               && Questions1.Text != "" && Questions2.Text != "" && reponse1.Text != "" && reponse2.Text != "")
            {
                wizPage2.NextEnabled = true;
            }

            else
            {
                wizPage2.NextEnabled = false;
            }
        }

       

        private void reponse2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden
               && Questions1.Text != "" && Questions2.Text != "" && reponse1.Text != "" && reponse2.Text != "")
            {
                wizPage2.NextEnabled = true;
            }

            else
            {
                wizPage2.NextEnabled = false;
            }
        }

        private void Questions2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden
               && Questions1.Text != "" && Questions2.Text != "" && reponse1.Text != "" && reponse2.Text != "")
            {
                wizPage2.NextEnabled = true;
            }

            else
            {
                wizPage2.NextEnabled = false;
            }
        }

        private void Questions1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserWarning.Visibility == Visibility.Hidden && PassWarning.Visibility == Visibility.Hidden && ConfirWarning.Visibility == Visibility.Hidden
               && Questions1.Text != "" && Questions2.Text != "" && reponse1.Text != "" && reponse2.Text != "")
            {
                wizPage2.NextEnabled = true;
            }
            else
            {
                wizPage2.NextEnabled = false;
            }
        }


        private void wizardControl_Next(object sender, RoutedEventArgs e)
        {
      
            if (wizardControl.SelectedWizardPage == wizPage3)
            {
                try
                {
                    string message = Parametres_Genereaux.AddUser(Pseudo.Text, MotDePasse.Password, "Administrateur", Questions1.Text, Questions2.Text, reponse1.Text, reponse2.Text,"");
                    if (message != null)
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                string message2 = Parametres_Genereaux.AjouterParametres(RaisonSociale.Text, Specialite.Text, SiteWeb.Text, NomGerant.Text, PrenomGerant.Text,
                       Adresse.Text, Telephone.Text, Fax.Text, MailGerant.Text, NumeroRC.Text, IdFiscal.Text, cheminlogo, Wilaya.Text);
                if (message2 != null)
                {
                    MessageBox.Show(message2);
                }

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 5000;
                timer.Tick += new EventHandler(tick_handler);
                timer.Start();

            }
        }

        private void tick_handler(object sender, EventArgs e)
        {
            ProgressBar.Visibility = Visibility.Hidden;
            wizPage4.Title = "Configuration réussite";
            wizPage4.Description = "la configuration a aboutie au succès";
            acacher.Visibility = Visibility.Hidden;
            acacher2.Visibility = Visibility.Hidden;
            wizPage4.FinishVisible = true;
            wizPage4.FinishEnabled = true;
            fini.Visibility = Visibility.Visible;
            fini2.Visibility = Visibility.Visible;
            
            
        }

        private void LogoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisisez une image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true && op.FileName != null && op.FileName != "")
            {
                cheminlogo = op.FileName;
                 logochoisie = true;
                
            }

            if (RaisonSociale.Text != "" && Specialite.Text != "" && SiteWeb.Text != "" && NomGerant.Text != "" && PrenomGerant.Text != ""
                && Adresse.Text != "" && Telephone.Text != "" && Fax.Text != "" && MailGerant.Text != "" && NumeroRC.Text != "" && IdFiscal.Text != ""
                && logochoisie == true && Wilaya.Text != "")
            {
                wizPage3.NextEnabled = true;
            }
            else
            {
                wizPage3.NextEnabled = false;
            }

        }

        private void RaisonSociale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RaisonSociale.Text != "" && Specialite.Text != "" && SiteWeb.Text != "" && NomGerant.Text != "" && PrenomGerant.Text != ""
                && Adresse.Text != "" && Telephone.Text != "" && Fax.Text != "" && MailGerant.Text != "" && NumeroRC.Text != "" && IdFiscal.Text != ""
                && logochoisie == true && Wilaya.Text != "")
            {
                wizPage3.NextEnabled = true;
            }
            else
            {
                wizPage3.NextEnabled = false;
            }
        }

        private void Telephone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Fax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void wizardControl_Finish(object sender, RoutedEventArgs e)
        {
            Variables.userlist = (from var in Variables.db.Users
                                  select var).ToList();

            MainWindow win = new MainWindow();
            win.NomUser.Content = Variables.userlist[0].Identifiant;
            win.Show();
            this.Close();
        }

        private void wizardControl_Cancel(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        private void wizardControl_Help_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
