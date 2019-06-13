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
    /// Logique d'interaction pour Detaille_Employe.xaml
    /// </summary>
    public partial class Detaille_Employe : Page
    {
        public static int index_detaille;
             
        public Detaille_Employe()
        {
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    detailsEmploye.Width = (win as MainWindow).Main.Width;
                    detailsEmploye.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    groupeBox1.Width = (win as MainWindow).Main.Width -320;
                    groupeBox1.Height = (win as MainWindow).Main.Height - 200;
                    scrollDetails.Height = (win as MainWindow).Main.Height - 230;
                    scrollDetails.Width = (win as MainWindow).Main.Width - 350;

                }
            }


            Matricule.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Matricule.ToString();
                nom_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Nom;
                Prenom_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Prenom;
                Adress_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Adresse;
                Email_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Email;
                Homme_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Sexe;
                naissance_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).DateDeNaissance.ToString();
                Coordo_bank_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).CoorBancaires;
                Commentaire_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Commentaires;
                dateambauche_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).DateEmbauche.ToString();
                Projet_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Projet;
                Actif_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Status;
                poste_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Poste;
                Salaire_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Salaires.Last().Salaire.ToString();
                Responsable_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).Responsable;
                Marie_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).SituationFamille;
                Telephone_modif1.Text = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).NumeroTel;
                Modifier_Employe.cv = (byte[])Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).CV.ToArray();
                if(Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).PhotoProfil != null)
                {
                    byte[] fichier = Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille).PhotoProfil.ToArray();
                    if (File.Exists(@"C:\Windows\Temp\foto_Employe_profil.png"))
                    {
                        File.Delete(@"C:\Windows\Temp\foto_Employe_profil.png");
                    }
                    File.WriteAllBytes(@"C:\Windows\Temp\foto_Employe_profil.png", fichier);
                    image_profil_detaille.Source = new BitmapImage(new Uri(@"C:\Windows\Temp\foto_Employe_profil.png"));
                }
          
        }

        private void CV_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllBytes(@"C:\Windows\Temp\cv.docx", Modifier_Employe.cv);
            System.Diagnostics.Process.Start(@"C:\Windows\Temp\cv.docx");
        }

        private void ListeDesSalaire_Click(object sender, RoutedEventArgs e)
        {
            Window2 wind = new Window2();
            wind.ShowDialog();
        }

        private void ListeDesConges_Click(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            win.ShowDialog();
        }

        private void AttestationTravaille_Click(object sender, RoutedEventArgs e)
        {
            AttestationTravail.genererAttest(Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille));
        }

        private void detailsEmploye_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ContratTravail_Click(object sender, RoutedEventArgs e)
        {
            ContratTravail.genererContrat(Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille));
        }

        private void cetificatTravail_Click(object sender, RoutedEventArgs e)
        {
           CertificatTravail.genererCertificat(Modifier_Employe.liste_detaille.ElementAt(Modifier_Employe.index_detaille));
        }
    }
}
