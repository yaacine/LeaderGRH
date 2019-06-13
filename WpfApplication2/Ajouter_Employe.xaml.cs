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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Ajouter_Employe.xaml
    /// </summary>
    public partial class Ajouter_Employe : Page
    {
        private string cv = null;
        private string Photo = null;
        private bool enabel= false;
        public Ajouter_Employe()
        {
            InitializeComponent();
        }
        private Boolean Enable_submit()
        {
            bool k;
            if (Croix_Email.Visibility == Visibility.Hidden)
            {
                if (Croix_nom.Visibility == Visibility.Hidden)
                {
                    if (Croix_NumImSoc.Visibility == Visibility.Hidden)
                    {
                        if (Croix_prenom.Visibility == Visibility.Hidden)
                        {                            
                            if (Croix_telephone.Visibility == Visibility.Hidden)
                            {
                                if(Croix_Cv.Visibility== Visibility.Hidden)
                                {
                                    k = true;
                                }
                                else
                                {
                                    k = false;
                                }
                            }
                            else
                            {
                                k = false;
                            }           
                        }
                        else k = false;
                    }
                    else k = false;
                }
                else k = false;
            }
            else k = false;
            return k;
        }
        private bool IsNumeric(string Nombre)
        {
            bool k =true;
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
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
 	  foreach (Window win in App.Current.Windows)
         	{
                if (win.Title.Equals("GRH"))
                {
                    ajouterEmploye.Width = (win as MainWindow).Main.Width;
                    ajouterEmploye.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    formulaire.Width = (win as MainWindow).Main.Width - 100;
                    formulaire.Width = (win as MainWindow).Main.Width - 100;
                }
            }
            Nom.Text = "Nom";
            Prenom.Text = "Prenom";
            Adresse.Text = "Adresse";
            Telephone.Text = "Telephone";
            NumImsociale.Text = "Numero";
            Homme.IsChecked = true;
            Celibataire.IsChecked = true;
            Projet.Text = "Projet";
            Poste.Text = "Poste";
            cooBanc.Text = "Coordonnes Bancaire";
            Salaire.Text = "Salaire";
            Email.Text = "E-mail";
            Responsable.Text = "Responsable";
        }

        private void Nom_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Nom.Text == "Nom")
            {
                Nom.Text = "";
            }
            
        }

        private void Prenom_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Prenom.Text == "Prenom")
            {
                Prenom.Text = "";
            }
            
        }

        private void NumImsociale_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (NumImsociale.Text == "Numero")
            {
                NumImsociale.Text = "";
            }
            
        }

        private void Adresse_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Adresse.Text == "Adresse")
            {
                Adresse.Text = "";
            }          
        }

        private void Telephone_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Telephone.Text == "Telephone")
            {
                Telephone.Text = "";
            }
        }

        private void Salaire_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Salaire.Text == "Salaire")
            {
                Salaire.Text = "";
            }
        }

        private void Email_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Email.Text == "E-mail")
            {
                Email.Text = "";
            }
        }

        private void cooBanc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cooBanc.Text == "Coordonnes Bancaire")
            {
                cooBanc.Text = "";
            }
        }

        private void Poste_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Poste.Text == "Poste")
            {
                Poste.Text = "";
            }
        }

        private void Projet_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Projet.Text == "Projet")
            {
                Projet.Text = "";
            }
        }

        private void Responsable_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Responsable.Text == "Responsable")
            {
                Responsable.Text = "";
            }
        }
        private void File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir le CV";
            try { op.Filter = "CV File(*.docx )|*.docx"; }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

            if (op.ShowDialog() == true)
            {
                cv = op.FileName;
                Commentaire_CV.Visibility = Visibility.Hidden ;
                Croix_Cv.Visibility = Visibility.Hidden;
                enabel = Enable_submit();
            }
        }
        private string Sexe() //retourne une chaine de caractere
        {
            if(Homme.IsChecked == true)
            {
                return "Homme";
            }
            else
            {
                return "Femelle";
            }
        }

        private string Situation()
        {
            if (Celibataire.IsChecked == true)
            {
                return "Celibataire";
            }
            else
            {
                return "Marie";
            }
        }

        private void Nom_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Nom.Text == "")
            {
                commentaire_Nom.Text = "Le Nom est obligatoire !";
                Croix_nom.Visibility = Visibility.Visible;
            }
            else
            {
                if(IsChar(Nom.Text)==false)
                {
                    commentaire_Nom.Text = "Le nom ne doit contenir que des caracteres!";
                    Croix_nom.Visibility = Visibility.Visible;
                }
                else
                {
                    commentaire_Nom.Text = "";
                    Croix_nom.Visibility = Visibility.Hidden;
                }
            }
            enabel = Enable_submit();
        }

        private void Prenom_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (Prenom.Text == "")
            {
                Croix_prenom.Visibility = Visibility.Visible;
                commentaire_Prenom.Text = "Le Prenom est obligatoire !";
            }
            else
            {
                if (IsChar(Prenom.Text) == false)
                {
                    Croix_prenom.Visibility = Visibility.Visible;
                    commentaire_Prenom.Text = "Le Prenom ne doit contenir que des caracteres!";
                }
                else
                {
                    Croix_prenom.Visibility = Visibility.Hidden;
                    commentaire_Prenom.Text = "";
                }
            }
            enabel = Enable_submit();
        }

        private void NumImsociale_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if(NumImsociale.Text=="")
            {
                Croix_NumImSoc.Visibility = Visibility.Visible;
                commentaire_Numimsoc.Text = "Champ Obligatoire !";
            }
            else
            {
                if(IsNumeric(NumImsociale.Text)==false)
                {
                    Croix_NumImSoc.Visibility = Visibility.Visible;
                    commentaire_Numimsoc.Text = "Ce champ ne doit contenir que des chiffres!";
                }
                else
                {
                    commentaire_Numimsoc.Text = "";
                    Croix_NumImSoc.Visibility = Visibility.Hidden;
                }
            }
            enabel = Enable_submit();
        }
        //test sur le champ telephone
        private void Telephone_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (Telephone.Text == "")
            {
                Croix_telephone.Visibility = Visibility.Visible;
                commentaire_Telephone.Text = "Champ Obligatoire !";
            }
            else
            {
                if(IsNumeric(Telephone.Text)==false)
                {
                    Croix_telephone.Visibility = Visibility.Visible;
                    commentaire_Telephone.Text = "Le numero de telephone contient que des chiffres!";
                }
                else
                {
                    if(Telephone.Text.Length != 10)
                    {
                        Croix_telephone.Visibility = Visibility.Visible;
                        commentaire_Telephone.Text = "Le numero de telephone doit contenir 10 chiffres !";
                    }
                    else
                    {
                        if(Telephone.Text[0]!='0')
                        {
                            Croix_telephone.Visibility = Visibility.Visible;
                            commentaire_Telephone.Text = "Le numero de telephone doit debuter par un 0 !";
                        }
                        else
                        {
                            Croix_telephone.Visibility = Visibility.Hidden;
                            commentaire_Telephone.Text = "";
                        }
                    }
                }
            }
            enabel = Enable_submit();
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if(Email.Text == "")
            {
                Croix_Email.Visibility = Visibility.Visible;
                comm_Email.Text = "Champ Obligatoire !";
            }
            else
            {
                if(Email.Text.Contains("@")==false)
                {
                    Croix_Email.Visibility = Visibility.Visible;
                    comm_Email.Text = "Format Email Incorrect !";
                }
                else
                {
                    if(Email.Text.Contains(".")==false)
                    {
                        Croix_Email.Visibility = Visibility.Visible;
                        comm_Email.Text = "Format Email Incorrect !";
                    }
                    else
                    {
                        Croix_Email.Visibility = Visibility.Hidden;
                        comm_Email.Text = "";
                    }
                }
            }
            enabel = Enable_submit();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(enabel==true  )
            {
                Submit.IsEnabled = true;
            }
            else
            {
                Submit.IsEnabled = false;
            }
        }

        private void Salaire_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Salaire.Text == "")
            {
                Croix_Salaire.Visibility = Visibility.Visible;
                Commentaire_Salaire.Text = "Champ Obligatoire !";
            }
            else
            {
                if(IsNumeric(Salaire.Text)==false)
                {
                    Croix_Salaire.Visibility = Visibility.Visible;
                    Commentaire_Salaire.Text = "Le salire ne peux contenir que des chiffres !";
                }
                else
                {
                    Croix_Salaire.Visibility = Visibility.Hidden;
                    Commentaire_Salaire.Text = "";
                }
            }
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string message;
            message = GAdministrative.AjouterEmployé(Nom.Text,Prenom.Text, DateTime.Parse(Datedenaissance.Text), Situation(), Sexe(), DateTime.Parse(Dateembauche.Text),
                  Poste.Text, Responsable.Text, Salaire.Text, Projet.Text, NumImsociale.Text, cv, Photo, "actif", Adresse.Text, Telephone.Text, Email.Text, cooBanc.Text, Commentaire.Text);
            MessageBox.Show(message ??  "l'employé "+Nom.Text+" "+Prenom.Text+" a été ajouté avec succès !","Réussi",MessageBoxButton.OK, MessageBoxImage.Information );
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir la photo de profil";
            try { op.Filter = "Photo(*.png )|*.png"; }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

            if (op.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(op.FileName));
                Photo = op.FileName;
            }
        }
    }
}
