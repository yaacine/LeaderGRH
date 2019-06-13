using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using WindowWPf;

namespace WpfApplication2
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int selectedindex { get; set; }
        public List<Evaluation> list = null;
        public static string path = null;
        public MainWindow()
        {
            InitializeComponent();
            //Main.Content = new acceuil();
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= {System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Project_Data.mdf;Integrated Security=True;Connect Timeout=30";
            Variables.db = new BaseDataContext(connectionString);
            GAdministrative.InitialiserListe();
        }

      

        private void Acceuil1_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuGestionDuPersonnel.Visibility = Visibility.Visible;
            MenuGestionDesConges.Visibility = Visibility.Hidden;
            MenuParametre.Visibility = Visibility.Hidden;
            MenuGestionDesEvaluations.Visibility = Visibility.Hidden;
            MenuGestionDesRecrutements.Visibility = Visibility.Hidden;
            Menuannuaire.Visibility = Visibility.Hidden;
        }

        private void MenuGestionDuPersonnel_MouseLeave(object sender, MouseEventArgs e)
        {
            if(Onglet_Personnel.IsMouseOver == false)
            {
                MenuGestionDuPersonnel.Visibility = Visibility.Hidden;
            }
        }
        private void Acceuil2_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuGestionDesConges.Visibility = Visibility.Visible;
            MenuGestionDuPersonnel.Visibility = Visibility.Hidden;
            MenuParametre.Visibility = Visibility.Hidden;
            MenuGestionDesEvaluations.Visibility = Visibility.Hidden;
            MenuGestionDesRecrutements.Visibility = Visibility.Hidden;
            Menuannuaire.Visibility = Visibility.Hidden;
            if (Vert.IsChecked == true)
            {
                Onglet_Conges.Background = new SolidColorBrush(Color.FromArgb(250, 0, 121, 107));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Onglet_Conges.Background = new SolidColorBrush(Color.FromArgb(250, 0, 16, 112));
                }
                else
                {
                    Onglet_Conges.Background = new SolidColorBrush(Color.FromArgb(250, 85, 85, 85));
                }
            }
        }

        private void GestionPersonnel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Onglet_Conges.IsMouseOver == false)
            {
                MenuGestionDesConges.Visibility = Visibility.Hidden;
                Onglet_Conges.Background = Brushes.Transparent;
            }

        }
        private void dockpanel_parametres_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuGestionDesConges.Visibility = Visibility.Hidden;
            MenuGestionDuPersonnel.Visibility = Visibility.Hidden;
            MenuGestionDesEvaluations.Visibility = Visibility.Hidden;
            MenuParametre.Visibility = Visibility.Visible;
            MenuGestionDesRecrutements.Visibility = Visibility.Hidden;
            Menuannuaire.Visibility = Visibility.Hidden;
            if (Vert.IsChecked == true)
            {
                Onglet_Parametre.Background = new SolidColorBrush(Color.FromArgb(250, 0, 121, 107));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Onglet_Parametre.Background = new SolidColorBrush(Color.FromArgb(250, 0, 16, 112));
                }
                else
                {
                    Onglet_Parametre.Background = new SolidColorBrush(Color.FromArgb(250,85,85,85));
                }
            }
        }
        private void dockpanel_parametres_MouseLeave(object sender, MouseEventArgs e)
        {
            Onglet_Parametre.Background = Brushes.Transparent;
        }
        private void dockpnel_acceuil2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                Onglet_Personnel.Background = new SolidColorBrush(Color.FromArgb(250, 0, 121, 107));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Onglet_Personnel.Background = new SolidColorBrush(Color.FromArgb(250, 0, 16, 112));
                }
                else
                {
                    Onglet_Personnel.Background = new SolidColorBrush(Color.FromArgb(250,85,85,85));
                }
            }
        }

        private void dockpnel_acceuil2_MouseLeave(object sender, MouseEventArgs e)
        {
            Onglet_Personnel.Background = Brushes.Transparent;
        }
        // methode d'initialisation des dimensions de la fentre selon les dimension 
        private void fenetre_principale_Loaded(object sender, RoutedEventArgs e)
        {
            fenetre_principale.Width = System.Windows.SystemParameters.PrimaryScreenWidth - 50;
            fenetre_principale.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 80;
            Main.Height = fenetre_principale.Height - 125;
            Main.Width = fenetre_principale.Width - 90;
            Variables.cpt++;
            List<Users> rech;
            rech = (from a in Variables.userlist
                    where a.Identifiant.ToString().Equals(NomUser.Content.ToString())
                    select a).ToList();
            if (rech.ElementAt(0).Qualite.Equals("Gérant"))
            {
                Expander.Visibility = Visibility.Hidden;

            }
            if (rech.ElementAt(0).Photo_Profil != null)//si il a deja mis une photo de profil.
            {
                byte[] fichier = rech.First().Photo_Profil.ToArray();
                if (File.Exists(@"C:\Windows\Temp\foto"+Variables.cpt.ToString()+".png"))
                {
                    File.Delete(@"C:\Windows\Temp\foto" + Variables.cpt.ToString() + ".png");
                }
                File.WriteAllBytes(@"C:\Windows\Temp\foto" + Variables.cpt.ToString() + ".png", fichier);
                Photo_Profil.Source = new BitmapImage(new Uri(@"C:\Windows\Temp\foto" + Variables.cpt.ToString() + ".png"));
            }
            if (rech.ElementAt(0).ThemeApp == null)
            {
                ApplyMarron();
                Marron.IsChecked = true;
            }
            else
            {
                if (rech.ElementAt(0).ThemeApp == "Bleu")
                {
                    ApplyBleu();
                    Bleu.IsChecked = true;
                }
                else
                {
                    if (rech.ElementAt(0).ThemeApp == "Marron")
                    {
                        ApplyMarron();
                        Marron.IsChecked = true;
                    }
                    else
                    {
                        ApplyVert();
                        Vert.IsChecked = true;
                    }
                }
               
            }
            list = GEvaluation.EvalFuture();
            if (list.Any())
            {
                if (list.First().DateEval == DateTime.Today)
                {
                    Notification.Visibility = Visibility.Visible;
                }
            }
            var param = Variables.db.Parametres?.First();
            if (param != null)
            {
                Footer_Adresse.Content = param.Adresse;
                Footer_Fax.Content = param.Fax;
                Footer_Gerant.Content = param.NomGerant + "" + param.PrenomGerant;
                Footer_Mail.Content = param.MailGerant;
                Footer_Telephone.Content = param.Telephone;
                if(File.Exists(@"C:\Windows\Temp\logo_entreprise" + Variables.cpt.ToString() + ".png"))
                {
                    File.Delete(@"C:\Windows\Temp\logo_entreprise" + Variables.cpt.ToString() + ".png");
                }
                File.WriteAllBytes(@"C:\Windows\Temp\logo_entreprise" + Variables.cpt.ToString() + ".png", param.Logo.ToArray());
                path = @"C:\Windows\Temp\logo_entreprise" + Variables.cpt.ToString() + ".png";
                Image_entreprise.Source = new BitmapImage(new Uri(@"C:\Windows\Temp\logo_entreprise" + Variables.cpt.ToString() + ".png"));
            }
            Main.Content = new acceuil();
        }

        private void MenuGestionDesConges_MouseLeave(object sender, MouseEventArgs e)
        {
           if((Onglet_Conges.IsMouseOver)==false)
            {
                MenuGestionDesConges.Visibility = Visibility.Hidden;
                Onglet_Conges.Background = Brushes.Transparent;
            }
        }

        private void menuitem1_1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                menuitem1_1.Background = new SolidColorBrush(Color.FromArgb(250,2,171,152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    menuitem1_1.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    menuitem1_1.Background = new SolidColorBrush(Color.FromArgb(250,113,113,113));
                }
            }
        }

        private void menuitem1_1_MouseLeave(object sender, MouseEventArgs e)
        {
            menuitem1_1.Background = Brushes.Transparent;
        }

        private void menuitem2_1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                menuitem2_1.Background = new SolidColorBrush(Color.FromArgb(250,2,171,152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    menuitem2_1.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    menuitem2_1.Background = new SolidColorBrush(Color.FromArgb(250,113,113,113));
                }
            }
        }

        private void menuitem2_1_MouseLeave(object sender, MouseEventArgs e)
        {
            menuitem2_1.Background = Brushes.Transparent;         
        }
        public void ApplyBleu()
        {
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 4, 15, 75));
            Header.Background = new SolidColorBrush(Color.FromArgb(255, 4, 15, 75));
            MenuGestionDuPersonnel.Background = new SolidColorBrush(Color.FromArgb(255, 0, 16, 112));
            MenuGestionDesConges.Background = new SolidColorBrush(Color.FromArgb(255, 0, 16, 112));
            MenuParametre.Background = new SolidColorBrush(Color.FromArgb(255, 0, 16, 112));
            Menuannuaire.Background = new SolidColorBrush(Color.FromArgb(255, 0, 16, 112));
            MenuGestionDesEvaluations.Background = new SolidColorBrush(Color.FromArgb(255, 0, 16, 112));
            MenuGestionDesRecrutements.Background = new SolidColorBrush(Color.FromArgb(255, 0, 16, 112));
            NomUser.Background = new SolidColorBrush(Color.FromArgb(255, 0, 50, 130));
            Footer_Mail.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 40, 240));
            Footer_Telephone.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 40, 240));
            Footer_Fax.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 40, 240));
            Footer_Adresse.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 40, 240));
            Footer_Gerant.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 40, 240));
            Logo_Gadministrative.Foreground = new SolidColorBrush(Color.FromArgb(255, 28, 40, 229));
            Logo_Gconges.Background = new SolidColorBrush(Color.FromArgb(255, 28, 40, 229));
            Logo_annuaire.Foreground = new SolidColorBrush(Color.FromArgb(255, 28, 40, 229));
            Logo_GParametres.Background = new SolidColorBrush(Color.FromArgb(255, 28, 40, 229));
            Logo_Gevaluation.Background = new SolidColorBrush(Color.FromArgb(255, 28, 40, 229));
            Logo_GRectrutement.Background = new SolidColorBrush(Color.FromArgb(255, 28, 40, 229));
            Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml");
        }
        public void ApplyVert()
        {
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 0, 67, 74));
            Header.Background = new SolidColorBrush(Color.FromArgb(255, 0, 67, 74));
            MenuGestionDuPersonnel.Background = new SolidColorBrush(Color.FromArgb(255, 0, 121, 107));
            MenuGestionDesConges.Background = new SolidColorBrush(Color.FromArgb(255, 0, 121, 107));
            MenuParametre.Background = new SolidColorBrush(Color.FromArgb(255, 0, 121, 107));
            MenuGestionDesRecrutements.Background = new SolidColorBrush(Color.FromArgb(255, 0, 121, 107));
            MenuGestionDesEvaluations.Background = new SolidColorBrush(Color.FromArgb(255, 0, 121, 107));
            Menuannuaire.Background = new SolidColorBrush(Color.FromArgb(255, 0, 121, 107));
            NomUser.Background = new SolidColorBrush(Color.FromArgb(255, 9, 93, 37));
            Footer_Mail.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 240, 30));
            Footer_Telephone.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 240, 30));
            Footer_Fax.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 240, 30));
            Footer_Adresse.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 240, 30));
            Footer_Gerant.IconBackground = new SolidColorBrush(Color.FromArgb(255, 20, 240, 30));
            Logo_Gadministrative.Foreground = new SolidColorBrush(Color.FromArgb(255, 40, 209, 66));
            Logo_Gconges.Background = new SolidColorBrush(Color.FromArgb(255, 40, 209, 66));
            Logo_GParametres.Background = new SolidColorBrush(Color.FromArgb(255, 40, 209, 66));
            Logo_Gevaluation.Background = new SolidColorBrush(Color.FromArgb(255, 40, 209, 66));
            Logo_annuaire.Foreground = new SolidColorBrush(Color.FromArgb(255, 40, 209, 66));
            Logo_GRectrutement.Background = new SolidColorBrush(Color.FromArgb(255, 40, 209, 66));
            Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Green.xaml");
        }

        public void ApplyMarron()
        {
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 60, 60, 62));
            Header.Background = new SolidColorBrush(Color.FromArgb(255, 60, 60, 62));
            MenuGestionDuPersonnel.Background = new SolidColorBrush(Color.FromArgb(255, 85, 85, 85));
            MenuGestionDesConges.Background = new SolidColorBrush(Color.FromArgb(255, 85, 85, 85));
            MenuParametre.Background = new SolidColorBrush(Color.FromArgb(255, 85, 85, 85));
            MenuGestionDesEvaluations.Background = new SolidColorBrush(Color.FromArgb(255, 85, 85, 85));
            MenuGestionDesRecrutements.Background = new SolidColorBrush(Color.FromArgb(255, 85, 85, 85));
            Menuannuaire.Background = new SolidColorBrush(Color.FromArgb(255, 85, 85, 85));
            NomUser.Background = new SolidColorBrush(Color.FromArgb(255, 88, 91, 88));
            Footer_Mail.IconBackground = new SolidColorBrush(Color.FromArgb(255, 150, 135, 110));
            Footer_Telephone.IconBackground = new SolidColorBrush(Color.FromArgb(255, 150, 135, 110));
            Footer_Fax.IconBackground = new SolidColorBrush(Color.FromArgb(255, 150, 135, 110));
            Footer_Adresse.IconBackground = new SolidColorBrush(Color.FromArgb(255, 150, 135, 110));
            Footer_Gerant.IconBackground = new SolidColorBrush(Color.FromArgb(255, 150, 135, 110));
            Logo_Gadministrative.Foreground = new SolidColorBrush(Color.FromArgb(255, 158, 100, 51));
            Logo_Gconges.Background = new SolidColorBrush(Color.FromArgb(255, 158, 100, 51));
            Logo_GParametres.Background = new SolidColorBrush(Color.FromArgb(255, 158, 100, 51));
            Logo_Gevaluation.Background = new SolidColorBrush(Color.FromArgb(255, 158, 100, 51));
            Logo_GRectrutement.Background = new SolidColorBrush(Color.FromArgb(255, 158, 100, 51));
            Logo_annuaire.Foreground = new SolidColorBrush(Color.FromArgb(255, 158, 100, 51));
            Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Brown.xaml");
        }
        private void Vert_Click(object sender, RoutedEventArgs e)
        {
            ApplyVert();
            Parametres_Genereaux.ModifyUserTheme(NomUser.Content.ToString(), "Vert");
        }

        private void Bleu_Click(object sender, RoutedEventArgs e)
        {
            ApplyBleu();
            Parametres_Genereaux.ModifyUserTheme(NomUser.Content.ToString(),"Bleu");
        }

        private void Ajouter_employer(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Ajouter_Employe();
        }
        private void MenuParametre_MouseLeave(object sender, MouseEventArgs e)
        {
                MenuParametre.Visibility = Visibility.Hidden;
        }
        private void menuitem1_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Modifier_Employe();
        }

        private void menuitem1_2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                menuitem1_2.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    menuitem1_2.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    menuitem1_2.Background = new SolidColorBrush(Color.FromArgb(250,113,113,113));
                }
            }
        }

        private void menuitem1_2_MouseLeave(object sender, MouseEventArgs e)
        {
            menuitem1_2.Background = Brushes.Transparent;
        }

        private void Marron_Click(object sender, RoutedEventArgs e)
        {
            ApplyMarron();
            Parametres_Genereaux.ModifyUserTheme(NomUser.Content.ToString(),"Marron");
        }

        private void Menu_item_parametres_1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                Menu_item_parametres_1.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Menu_item_parametres_1.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    Menu_item_parametres_1.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void Menu_item_parametres_1_MouseLeave(object sender, MouseEventArgs e)
        {
            Menu_item_parametres_1.Background = Brushes.Transparent;
        }
       
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Parametre_genereaux();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                Param_Genereux_button.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Param_Genereux_button.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    Param_Genereux_button.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void Param_Genereux_button_MouseLeave(object sender, MouseEventArgs e)
        {
            Param_Genereux_button.Background = Brushes.Transparent;
        }

        private void Gestion_des_comptes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Gestion_Users();
        }

        private void Onglet_Conges_MouseLeave(object sender, MouseEventArgs e)
        {
            Onglet_Conges.Background = Brushes.Transparent;
        }

        private void Item_Listedesconges_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ListeCongés();
        }

        private void Menu_item_parametres_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Theme();
        }

        private void Liste_des_comptes_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                Liste_des_comptes.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Liste_des_comptes.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    Liste_des_comptes.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void Liste_des_comptes_MouseLeave(object sender, MouseEventArgs e)
        {
            Liste_des_comptes.Background = Brushes.Transparent;
        }

        private void Ajouter_Compte_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ajouter_Comptes window = new Ajouter_Comptes();
            window.ShowDialog();
        }

        private void Ajouter_Compte_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                Ajouter_Compte.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Ajouter_Compte.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    Ajouter_Compte.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }
        private void Ajouter_Compte_MouseLeave(object sender, MouseEventArgs e)
        {
            Ajouter_Compte.Background = Brushes.Transparent;
        }
        private void Ajouter_Un_Conge_Menu_Parametres_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Page1();
        }
        public static void pasDeChangements()
        {
            MessageBox.Show("Aucune modification n'a été spécifiée pour ce congé", "Modifier un Congé", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static bool alerteChauvauchement(int nb)
        {
            MessageBoxResult result = MessageBox.Show("Ce congé cause un chauvauchement critique avec d'autres congés \n\n  Voules vous continuer ?", "Alerte", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void alertErreurDate()
        {
            MessageBox.Show("Erreur dans les dates!  \n la date de fin doit etre suprieure à la date de debut du congé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void CongeBienAjoute()
        {
            MessageBox.Show("Congé Enregistré avec succes !", "Enregistré", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void FormNonCharge()
        {
            MessageBox.Show("Le formulaire de demande de congé n'est pas encore chargé ! \n Cliquez fur Form pour le charger ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void typeNonChoisi()
        {
            MessageBox.Show("Veuillez choisir un type correcte du congé !", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        public static void alerteDepassmentJoursPermis(int nbrestant, int nbdemand)
        {
            String message = "Cet employé ne peut prendre que " + nbrestant + " jous alors que la demande est de " + nbdemand + " jours !";
            MessageBox.Show(message, "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static void alerteModifierDateDebut()
        {
            MessageBox.Show("Ce congés est en cours de consommation  \n \t Vous ne pouvez pas modifier la date de debut pour le meme type de congés", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void employeAPrisConges(Employe emp)
        {
            string message = "L'epmloyé " + emp.Nom + " " + emp.Prenom + " a déja reservé un congé ! \n\n  Veuillez vous rendre à l'anglet de modification des congés si vous souhaiter moifier son congé";
            MessageBox.Show(message, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void matriculeNexistePas()
        {
            MessageBox.Show("Le matricule saisi n'existe pas dans la liste des employés! \n Veuillez consulter le liste des employés", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void employInactif(Employe e)
        {
            string message = "L'employé " + e.Nom + " " + e.Prenom + "  est inactif , vous ne pouvez pa lui accorder un congé !";
            MessageBox.Show(message, "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void employeExisteDeja()
        {

            MessageBox.Show("L'employé saisi existe déja dans la liste des employés !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        public static void aucunEntretienSelectionne()
        {
            MessageBox.Show("Aucun entretien n'est selectionné ! \n", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        public static void recruterEmploye(out bool sortie)
        {
            string message = "Vous avez choisi de recruter le candidat \n Voulez-vous l'ajouter maintenant à la liste des employés ?";
            MessageBoxResult result = MessageBox.Show(message, "", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                sortie = true;
            }
            else if (result == MessageBoxResult.No)
            {
                sortie = false;
            }
            else
            {
                sortie = false;
            }

        }
        private void Onglet_Evaluation_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuGestionDesConges.Visibility = Visibility.Hidden;
            MenuGestionDuPersonnel.Visibility = Visibility.Hidden;
            MenuParametre.Visibility = Visibility.Hidden;
            Menuannuaire.Visibility = Visibility.Hidden;
            MenuGestionDesRecrutements.Visibility = Visibility.Hidden;
            MenuGestionDesEvaluations.Visibility = Visibility.Visible;
            if (Vert.IsChecked == true)
            {
                Onglet_Evaluation.Background = new SolidColorBrush(Color.FromArgb(250, 0, 121, 107));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Onglet_Evaluation.Background = new SolidColorBrush(Color.FromArgb(250, 0, 16, 112));
                }
                else
                {
                    Onglet_Evaluation.Background = new SolidColorBrush(Color.FromArgb(250, 85, 85, 85));
                }
            }
        }

        private void Onglet_Evaluation_MouseLeave(object sender, MouseEventArgs e)
        {
            Onglet_Evaluation.Background = Brushes.Transparent;
        }

        private void MenuGestionDesEvaluations_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((Onglet_Evaluation.IsMouseOver) == false)
            {
                MenuGestionDesEvaluations.Visibility = Visibility.Hidden;
                Onglet_Evaluation.Background = Brushes.Transparent;
            }
        }

        private void Onglet_Recrutement_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuGestionDesConges.Visibility = Visibility.Hidden;
            MenuGestionDuPersonnel.Visibility = Visibility.Hidden;
            MenuParametre.Visibility = Visibility.Hidden;
            MenuGestionDesEvaluations.Visibility = Visibility.Hidden;
            MenuGestionDesRecrutements.Visibility = Visibility.Visible;
            Menuannuaire.Visibility = Visibility.Hidden;
            if (Vert.IsChecked == true)
            {
                Onglet_Recrutement.Background = new SolidColorBrush(Color.FromArgb(250, 0, 121, 107));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Onglet_Recrutement.Background = new SolidColorBrush(Color.FromArgb(250, 0, 16, 112));
                }
                else
                {
                    Onglet_Recrutement.Background = new SolidColorBrush(Color.FromArgb(250, 85, 85, 85));
                }
            }
        }

        private void Onglet_Recrutement_MouseLeave(object sender, MouseEventArgs e)
        {
            Onglet_Recrutement.Background = Brushes.Transparent;
        }

        private void MenuGestionDesRecrutements_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((Onglet_Recrutement.IsMouseOver) == false)
            {
                MenuGestionDesRecrutements.Visibility = Visibility.Hidden;
                Onglet_Recrutement.Background = Brushes.Transparent;
            }
        }


        private void appelEntretinesNonDecides(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new EntretiensNonDecidés();
        }

        public string getTheme()
        {
            string theme = null;
            if(Bleu.IsChecked == true)
            {
                theme = "Bleu";
            }
            else
            {
                if(Vert.IsChecked == true)
                {
                    theme = "Vert";
                }
                else
                {
                    theme = "Marron";
                }
            }
            return theme;
        }

        private void Vue_Globale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ListeDesEmploye_Evaluation();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void TextBlock_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Main.Content = new Page2();

        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ListeCondidat();
        }

        private void Enlever_Notif_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Notification.Visibility = Visibility.Hidden;
        }

        private void Voir_Prochaine_Entretien_Click(object sender, RoutedEventArgs e)
        {
            Notification.Visibility = Visibility.Hidden;
            Main.Content = new Prochaine_Entretien();
        }

        private void NomUser_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ModifierCompte(false);
        }

        private void Vue_Globale_Dock_MouseEnter(object sender, MouseEventArgs e)
        {
            Vue_Globale_Dock.Background = Brushes.Transparent;
        }

        private void Vue_Globale_Dock_MouseEnter_1(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                Vue_Globale_Dock.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Vue_Globale_Dock.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    Vue_Globale_Dock.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void Prochain_Entretien_Dock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                Prochain_Entretien_Dock.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    Prochain_Entretien_Dock.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    Prochain_Entretien_Dock.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void Prochain_Entretien_Dock_MouseLeave(object sender, MouseEventArgs e)
        {
            Prochain_Entretien_Dock.Background = Brushes.Transparent;
        }

        private void Prochaine_ENtretien_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Prochaine_Entretien();
        }



        private void image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Annuaire();
        }

        private void TextBlock_PreviewMouseMove(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Page2();
        }

        private void Acceuil1_MouseDown(object sender, MouseButtonEventArgs e)
        {
           Main.Content = new acceuil();
        }

        private void item1mouseenter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                item1.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    item1.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    item1.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void item1mouseleqve(object sender, MouseEventArgs e)
        {
            item1.Background = Brushes.Transparent;
        }

        private void item1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuAnnuaireleave(object sender, MouseEventArgs e)
        {
            if ((ongletannuaire.IsMouseOver) == false)
            {
                Menuannuaire.Visibility = Visibility.Hidden;
                ongletannuaire.Background = Brushes.Transparent;
            }
        }

        private void ongletannuaire_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuGestionDesConges.Visibility = Visibility.Hidden;
            MenuGestionDuPersonnel.Visibility = Visibility.Hidden;
            MenuParametre.Visibility = Visibility.Hidden;
            MenuGestionDesEvaluations.Visibility = Visibility.Hidden;
            MenuGestionDesRecrutements.Visibility = Visibility.Hidden;
            Menuannuaire.Visibility = Visibility.Visible;
            if (Vert.IsChecked == true)
            {
                ongletannuaire.Background = new SolidColorBrush(Color.FromArgb(250, 0, 121, 107));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    ongletannuaire.Background = new SolidColorBrush(Color.FromArgb(250, 0, 16, 112));
                }
                else
                {
                    ongletannuaire.Background = new SolidColorBrush(Color.FromArgb(250, 85, 85, 85));
                }
            }
        }

        private void ongletannuaire_MouseLeave(object sender, MouseEventArgs e)
        {
            ongletannuaire.Background = Brushes.Transparent;
        }

        private void menuitem2_2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                ajouterconger.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    ajouterconger.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    ajouterconger.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void ajouterconger_MouseLeave(object sender, MouseEventArgs e)
        {
            ajouterconger.Background = Brushes.Transparent;               }

        private void listecandidat_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                listecandidat.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    listecandidat.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    listecandidat.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void listecandidat_MouseLeave(object sender, MouseEventArgs e)
        {
            listecandidat.Background = Brushes.Transparent;
        }

        private void futurentretien_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                futurentretien.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    futurentretien.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    futurentretien.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void futurentretien_MouseLeave(object sender, MouseEventArgs e)
        {
            futurentretien.Background = Brushes.Transparent;
        }

        private void entreitnindecis_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Vert.IsChecked == true)
            {
                entreitnindecis.Background = new SolidColorBrush(Color.FromArgb(250, 2, 171, 152));
            }
            else
            {
                if (Bleu.IsChecked == true)
                {
                    entreitnindecis.Background = new SolidColorBrush(Color.FromArgb(250, 9, 62, 156));
                }
                else
                {
                    entreitnindecis.Background = new SolidColorBrush(Color.FromArgb(250, 113, 113, 113));
                }
            }
        }

        private void entreitnindecis_MouseLeave(object sender, MouseEventArgs e)
        {
            entreitnindecis.Background = Brushes.Transparent;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Login();
            Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml");
            win.Show();
            this.Close();
        }
    }
    public static class Variables
    {
        public static BaseDataContext db;
        public static int cpt = 0;
        public static List<Users> userlist;
        public static int index;
    }
}

