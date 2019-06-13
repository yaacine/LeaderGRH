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
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Planifier_Entretien.xaml
    /// </summary>
    public partial class Planifier_Entretien : Window
    {
        private bool enabel = false;
        private static byte[] logo;
        public Planifier_Entretien()
        {
            InitializeComponent();
        }

        private void Datedenaissance_Initialized(object sender, EventArgs e)
        {
            DateTime date = DateTime.Today;
            dateentretien.DisplayDateStart = date;
        }
        private Boolean Enable_submit()
        {
            bool k;
            if (dateentretien.Text != "")
            {
                if (commentaire_Objectif1.Visibility == Visibility.Hidden)
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
            return k;
        }

        private void dateentretien_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dateentretien.Text == "")
            {
                Commentaire_date_entretien.Visibility = Visibility.Visible;
            }
            else
            {
                Commentaire_date_entretien.Visibility = Visibility.Hidden;
            }
            enabel = Enable_submit();
            EnableBouton();
        }

        private void EnableBouton()
        {
            if (enabel == true)
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
            Mouse.OverrideCursor = Cursors.Wait;
            string path = System.IO.Path.GetFullPath("Copie de Fiche d'entretien d'évaluation annuel(1) - Copy.xlsx");
            string path2 = System.IO.Path.GetFullPath(@"photos\Settings.png");
            string nom = ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Nom;
            string prenom = ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Prenom;
            string poste = ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Poste;
            string responsable = ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Responsable;
            string DateEmbauche = ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).DateEmbauche.ToString();
            string NomPrenom = nom + " " + prenom + " :";
            Excel excel = new Excel(path, 1);
            var param = Variables.db.Parametres?.First();
            logo = (byte[])param.Logo.ToArray();
            File.WriteAllBytes(@"C:\Windows\Temp\logo.docx", logo);
            try
            {
                excel.creer_fichier_entretien(@"C:\Windows\Temp\logo.docx", param.Raison_Sociale, param.Speciaite, NomPrenom, poste, responsable, DateEmbauche, objectif1.Text, Objectif2.Text, Objectif3.Text);
                if(File.Exists((@"C:\Windows\Temp\fichier.docx")))
                {
                    File.Delete(@"C:\Windows\Temp\fichier.docx");
                }
                excel.saveas(@"C:\Windows\Temp\fichier.docx");
            }
            catch(Exception ex)
            {
                MessageBox.Show("erreur lors de la generation du fichier Excel.");
            }
            excel.Close();
            GEvaluation.Planifier_Entretien_evaluation(ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Matricule, DateTime.Parse(dateentretien.Text), @"C:\Windows\Temp\fichier.docx",null);
            MessageBox.Show("Entretien Programme avec succes!");
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new HistoriqueDesEntretiens();
                    break;
                }
            }
            Mouse.OverrideCursor = null;
            this.Close();
        }

        private void Liste_Objectif_MouseMove(object sender, MouseEventArgs e)
        {
            if(objectif1.Text != "")
            {
                Objectif2.IsEnabled = true;
                if(Objectif2.Text != "")
                {
                    Objectif3.IsEnabled = true;
                }
                else
                {
                    Objectif3.IsEnabled = false;
                }
            }
            else
            {
                Objectif2.IsEnabled = false;
                Objectif3.IsEnabled = false;
            }
        }

        private void objectif1_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void Objectif2_GotFocus(object sender, RoutedEventArgs e)
        {
          
        }

        private void Objectif3_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void objectif1_LostFocus(object sender, RoutedEventArgs e)
        {
            if(objectif1.Text !="")
            {
                commentaire_Objectif1.Visibility = Visibility.Hidden;
            }
            else
            {
                commentaire_Objectif1.Visibility = Visibility.Visible;
            }
            enabel = Enable_submit();
            EnableBouton();
        }

        private void dateentretien_Initialized(object sender, EventArgs e)
        {
            DateTime date = DateTime.Today;
            dateentretien.DisplayDateStart = date;
        }
    }
}
