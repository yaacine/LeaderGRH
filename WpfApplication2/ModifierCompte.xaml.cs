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
using WpfApplication2;

namespace WindowWPf
{
    using Microsoft.Win32;
    using System.IO;
    /// <summary>
    /// Logique d'interaction pour ModifierCompte.xaml
    using static Variables;
    using static Visibility;
    public partial class ModifierCompte : Page
    {
        private int selectedindex;
        private bool listeComptes;
        private string identifiant;
        private List<Users> list;
        public ModifierCompte(bool listeComptes)
        {
            InitializeComponent();
            selectedindex = Variables.index;
            this.listeComptes = listeComptes;
        }
        private string photo = null;
        private void Pseudo_TextChanged(object sender, TextChangedEventArgs e)
        {
           if (Pseudo.Text.Length < 6)
            {
                Ok1.Visibility = Hidden;
                CAncel1.Visibility = Visibility.Visible;
            }
           else
            {
                CAncel1.Visibility = Visibility.Hidden;
                Ok1.Visibility = Visible;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           if(listeComptes == true)
            {
                Pseudo.Text = userlist[selectedindex].Identifiant;
                MotDePasse.Password = userlist[selectedindex].MDP;
                if (userlist[selectedindex].Photo_Profil != null)//si il a deja mis une photo de profil.
                {
                    byte[] fichier = userlist[selectedindex].Photo_Profil.ToArray();
                    if (File.Exists(@"C:\Windows\Temp\o.png"))
                    {
                        File.Delete(@"C:\Windows\Temp\o.png");
                    }
                    File.WriteAllBytes(@"C:\Windows\Temp\o.png", fichier);
                    photo_profil.Source = new BitmapImage(new Uri(@"C:\Windows\Temp\o.png"));
                }
            }
            else
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.Title.Equals("GRH"))
                    {
                        identifiant = (win as MainWindow).NomUser.Content.ToString();
                        break;
                    }
                }
                list=Parametres_Genereaux.recherche(identifiant);
                Pseudo.Text = list.First().Identifiant;
                MotDePasse.Password = list.First().MDP;
                if (list.First().Photo_Profil != null)//si il a deja mis une photo de profil.
                {
                    byte[] fichier = list.First().Photo_Profil.ToArray();
                    if (File.Exists(@"C:\Windows\Temp\fotoo.png"))
                    {
                        File.Delete(@"C:\Windows\Temp\fotoo.png");
                    }
                    File.WriteAllBytes(@"C:\Windows\Temp\fotoo.png", fichier);
                    photo_profil.Source = new BitmapImage(new Uri(@"C:\Windows\Temp\fotoo.png"));
                }
            }
        }

        private void MotDePasse_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (MotDePasse.Password.Length < 6)
            {
                Cancel_2.Visibility = Visibility.Visible;
                Ok2.Visibility = Hidden;
            }
            else
            {
                Cancel_2.Visibility = Visibility.Hidden;
                Ok2.Visibility = Visible;
            }

            if (Confirmation.Password.Equals(MotDePasse.Password) && Confirmation.Password.Length >= 6)
            {
                Cancel_3.Visibility = Visibility.Hidden;
                Ok3.Visibility = Visible;
            }
            else
            {
                Cancel_3.Visibility = Visibility.Visible;
                Ok3.Visibility = Hidden;
            }
        }

        private void Confirmation_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Confirmation.Password.Equals(MotDePasse.Password) && Confirmation.Password.Length >=6)
            {
                Cancel_3.Visibility = Visibility.Hidden;
                Ok3.Visibility = Visible;
            }
            else
            {
                Cancel_3.Visibility = Visibility.Visible;
                Ok3.Visibility = Hidden;
            }
        }

        private void AncienMDP_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (AncienMDP.Password.Length < 6 )
            {
                Cancel4.Visibility = Visibility.Visible;
                Ok4.Visibility = Hidden;
            }
            else
            {
                Cancel4.Visibility = Visibility.Hidden;
                Ok4.Visibility = Visible;
            }
        }

        private void Ok4_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if( Ok1.Visibility == Visible && Ok2.Visibility == Visible && Ok3.Visibility == Visible && Ok4.Visibility == Visible)
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
            if(listeComptes == true)
            {
                if (userlist[selectedindex].MDP.Equals(AncienMDP.Password))
                {
                    try
                    {
                        string msg = Parametres_Genereaux.ModifyUser(userlist[selectedindex].Identifiant, MotDePasse.Password, userlist[selectedindex].ThemeApp);
                        Parametres_Genereaux.ModifyUserPhoto(userlist[selectedindex].Identifiant, photo);
                        Variables.userlist = Parametres_Genereaux.toutlescomptes();
                        if (photo!=null)
                        {
                            foreach (Window win in App.Current.Windows)
                            {
                                if (win.Title.Equals("GRH"))
                                {
                                    byte[] fichier = userlist[selectedindex].Photo_Profil.ToArray();
                                    if (File.Exists(@"C:\Windows\Temp\o.png"))
                                    {
                                        File.Delete(@"C:\Windows\Temp\o.png");
                                    }
                                    File.WriteAllBytes(@"C:\Windows\Temp\o.png", fichier);
                                    (win as MainWindow).Photo_Profil.Source = new BitmapImage(new Uri(@"C:\Windows\Temp\o.png"));
                                    break;
                                }
                            }
                        }
                        MessageBox.Show(msg ?? "Les changements ont été enregistrées", "Modification Terminée", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("L'ancien mot de passe est erroné");
                }
            }
            else
            {
                if(list.First().MDP.Equals(AncienMDP.Password))
                {
                    try
                    {
                        string msg = Parametres_Genereaux.ModifyUser(list.First().Identifiant, MotDePasse.Password,list.First().ThemeApp);
                        Parametres_Genereaux.ModifyUserPhoto(list.First().Identifiant, photo);
                        Variables.userlist = Parametres_Genereaux.toutlescomptes();
                        if (photo != null)
                        {
                            foreach (Window win in App.Current.Windows)
                            {
                                if (win.Title.Equals("GRH"))
                                {
                                    list = Parametres_Genereaux.recherche(list.First().Identifiant);
                                    byte[] fichier = list.First().Photo_Profil.ToArray();
                                    if (File.Exists(@"C:\Windows\Temp\fotoo.png"))
                                    {
                                        File.Delete(@"C:\Windows\Temp\fotoo.png");
                                    }
                                    File.WriteAllBytes(@"C:\Windows\Temp\fotoo.png", fichier);
                                    (win as MainWindow).Photo_Profil.Source = new BitmapImage(new Uri(@"C:\Windows\Temp\fotoo.png"));
                                    break;
                                }
                            }
                         }
                            MessageBox.Show(msg ?? "Les changements ont été enregistrées", "Modification Terminée", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("L'ancien mot de passe est erroné");
                }
            }
        }

        private void importer_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir la photo de profil";
            try { op.Filter = "Photo(*.png )|*.png"; }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

            if (op.ShowDialog() == true)
            {
                photo_profil.Source = new BitmapImage(new Uri(op.FileName));
                photo = op.FileName;
            }
        }
    }
}
