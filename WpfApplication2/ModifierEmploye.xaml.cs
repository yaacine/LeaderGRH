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
    /// Logique d'interaction pour ModifierEmploye.xaml
    /// </summary>
    public partial class ModifierEmploye : Page
    {
        private string Photo = null;
        private static string cv_modif;
        private bool enabel = true;
        public ModifierEmploye()
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
                    if (Croix_prenom.Visibility == Visibility.Hidden)
                    {
                        if (Croix_Telephone.Visibility == Visibility.Hidden)
                        {
                            k = true;
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
            return k;
        }
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
        private void Issituation(string chaine)
        {
            if (chaine == "Celibataire")
            {
                Celibataire_modif.IsChecked = true;
            }
            else
            {
                Marie_modif.IsChecked = true;
            }
        }
        private void Issexe(string chaine)
        {
            if (chaine == "Homme")
            {
                Homme_modif.IsChecked = true;
            }
            else
            {
                Femme_modif.IsChecked = true;
            }
        }
        private void Isstatus(string chaine)
        {
            if (chaine == "Actif")
            {
                Actif_modif.IsChecked = true;
            }
            else
            {
                if (chaine == "Retraite")
                {
                    Retraite_Modif.IsChecked = true;
                }
                else
                {
                    Desactif_modif.IsChecked = true;
                }
            }
        }
        private String Status()
        {
            string chaine;
            if (Actif_modif.IsChecked == true)
            {
                chaine = "Actif";
            }
            else
            {
                if (Retraite_Modif.IsChecked == true)
                {
                    chaine = "Retraite";
                }
                else
                {
                    chaine = "Desactif";
                }
            }
            return chaine;
        }
        private string Sexe() //retourne une chaine de caractere
        {
            if (Homme_modif.IsChecked == true)
            {
                return "Homme";
            }
            else
            {
                return "Femelle";
            }
        }
        private void Appliquer_Click(object sender, RoutedEventArgs e)
        {
            GAdministrative.ModifierEmployé(nom_modif.Text, Prenom_modif.Text, null, Situation(), null,
                poste_modif.Text, Responsable_modif.Text, Projet_modif.Text,Modifier_Employe.numim, Status(), Adress_modif.Text, decimal.Parse(Salaire_modif.Text), Telephone_modif.Text
                , Email_modif.Text, Coordo_bank_modif.Text, Commentaire_modif.Text, cv_modif,Photo);
            MessageBox.Show("Employe modifie avec succes !");
        }

        private void Nom_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nom_modif.Text == "")
            {
                commentaire_Nom.Text = "Le Nom est obligatoire !";
                Croix_nom.Visibility = Visibility.Visible;
            }
            else
            {
                if (IsChar(nom_modif.Text) == false)
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

            if (Prenom_modif.Text == "")
            {
                Croix_prenom.Visibility = Visibility.Visible;
                commentaire_prenom.Text = "Le Prenom est obligatoire !";
            }
            else
            {
                if (IsChar(Prenom_modif.Text) == false)
                {
                    Croix_prenom.Visibility = Visibility.Visible;
                    commentaire_prenom.Text = "Le Prenom ne doit contenir que des caracteres!";
                }
                else
                {
                    Croix_prenom.Visibility = Visibility.Hidden;
                    commentaire_prenom.Text = "";
                }
            }
            enabel = Enable_submit();
        }

        private void Telephone_modif_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Telephone_modif.Text == "")
            {
                Croix_Telephone.Visibility = Visibility.Visible;
                commentaire_Telephone1.Text = "Champ Obligatoire !";
            }
            else
            {
                if (IsNumeric(Telephone_modif.Text) == false)
                {
                    Croix_Telephone.Visibility = Visibility.Visible;
                    commentaire_Telephone1.Text = "Le numero de telephone contient que des chiffres!";
                }
                else
                {
                    if (Telephone_modif.Text.Length != 10)
                    {
                        Croix_Telephone.Visibility = Visibility.Visible;
                        commentaire_Telephone1.Text = "Le numero de telephone doit contenir 10 chiffres !";
                    }
                    else
                    {
                        if (Telephone_modif.Text[0] != '0')
                        {
                            Croix_Telephone.Visibility = Visibility.Visible;
                            commentaire_Telephone1.Text = "Le numero de telephone doit debuter par un 0 !";
                        }
                        else
                        {
                            Croix_Telephone.Visibility = Visibility.Hidden;
                            commentaire_Telephone1.Text = "";
                        }
                    }
                }
            }
            enabel = Enable_submit();
        }

        private void Email_modif_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Email_modif.Text == "")
            {
                Croix_Email.Visibility = Visibility.Visible;
                comm_Email.Text = "Champ Obligatoire !";
            }
            else
            {
                if (Email_modif.Text.Contains("@") == false)
                {
                    Croix_Email.Visibility = Visibility.Visible;
                    comm_Email.Text = "Format Email Incorrect !";
                }
                else
                {
                    if (Email_modif.Text.Contains(".") == false)
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

        private void Salaire_modif_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Salaire_modif.Text == "")
            {
                Croix_Salaire.Visibility = Visibility.Visible;
                commentaire_salaire.Text = "Champ Obligatoire !";
            }
            else
            {
                if (IsNumeric(Salaire_modif.Text) == false)
                {
                    Croix_Salaire.Visibility = Visibility.Visible;
                    commentaire_salaire.Text = "Le salaire ne peux contenir que des chiffres !";
                }
                else
                {
                    Croix_Salaire.Visibility = Visibility.Hidden;
                    commentaire_salaire.Text = "";
                }
            }
            enabel = Enable_submit();
        }

        private void Modifier_MouseMove(object sender, MouseEventArgs e)
        {
            if (enabel == true)
            {
                Appliquer.IsEnabled = true;
            }
            else
            {
                Appliquer.IsEnabled = false;
            }
        }

        private void Charger_nouveau_CV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir le CV";
            try { op.Filter = "CV File(*.docx )|*.docx"; }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

            if (op.ShowDialog() == true)
            {
                cv_modif = op.FileName;
            }
        }
        private string Situation()
        {
            if (Celibataire_modif.IsChecked == true)
            {
                return "Celibataire";
            }
            else
            {
                return "Marie";
            }
        }

        private void Importer_Phooto(object sender, RoutedEventArgs e)
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Variables.cpt++;
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    Modif_Employe.Width = (win as MainWindow).Main.Width;
                    Modif_Employe.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    formulaire.Height = (win as MainWindow).Main.Height -180;
                    formulaire.Width = (win as MainWindow).Main.Width - 90;
                }
            }
            nom_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Nom;
            Prenom_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Prenom;
            Adress_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Adresse;
            Email_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Email;
            Issexe(Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Sexe);
            Coordo_bank_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).CoorBancaires;
            Commentaire_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Commentaires;
            datedemission_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).DateEmbauche.ToString();
            Modifier_Employe.numim = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).NumImatSocial;
            naissance_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).DateDeNaissance.ToString();
            Projet_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Projet;
            poste_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Poste;
            Responsable_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Responsable;
            Issituation(Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).SituationFamille);
            Salaire_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Salaires.Last().Salaire.ToString();
            Isstatus(Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).Status);
            Telephone_modif.Text = Modifier_Employe.liste.ElementAt(Modifier_Employe.index_detaille).NumeroTel;
            if (Modifier_Employe.liste.ElementAt(Modifier_Employe.index_modifier).PhotoProfil != null)
            {
                byte[] fichier = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).PhotoProfil.ToArray();
                File.WriteAllBytes($@"C:\Windows\Temp\foto_Employe_profil_modif{Variables.cpt}.png", fichier); 
                image.Source = new BitmapImage(new Uri($@"C:\Windows\Temp\foto_Employe_profil_modif{Variables.cpt}.png"));

            }
        }
    }
}
