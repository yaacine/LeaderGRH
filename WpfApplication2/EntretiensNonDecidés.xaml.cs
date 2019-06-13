using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
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
    /// Logique d'interaction pour EntretiensNonDecidés.xaml
    /// </summary>
    public partial class EntretiensNonDecidés : Page
    {
       public static  List<infoEntretien> entretiens;


        public EntretiensNonDecidés()
        {
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= {System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Project_Data.mdf;Integrated Security=True;Connect Timeout=30";
            Variables.db = new BaseDataContext(connectionString);
            infoEntretien element;
            InitializeComponent();
            SuiviRecrutements.EntretienNonDecides();
            if(SuiviRecrutements.listeEntretiensNonDecides!=null)
            {
                entretiens = new List<infoEntretien>();
                foreach (Entretient var in SuiviRecrutements.listeEntretiensNonDecides)
                {
                    element = new infoEntretien(var.NumEntret.ToString(),var.Candidat.NumeroCandidat.ToString(), var.Candidat.Nom,var.Candidat.Prenom, var.Candidat.IntitulePoste, var.DateEntretien.ToString());
                    entretiens.Add(element);
                    
                }
            }

            entretienIndecis.ItemsSource = entretiens;

        }


        public class infoEntretien
        {
            public string numEntretien { get; set; }
            public string numCandidat { get; set; }
            public string nom { get; set; }
            public string prenom { get; set; }
            public string poste { get; set; }
            public string dateEntretien { get; set; }

            public infoEntretien(string nument, string numcan, string nom, string prenom, string poste, string dateEnt )
            {
                this.numEntretien = nument;
                this.numCandidat = numcan;
                this.nom = nom;
                this.prenom = prenom;
                this.poste = poste;
                this.dateEntretien = dateEnt;
            }

        }

        private void entrerienDetails(object sender, RoutedEventArgs e)
        {
            if (entretienIndecis.SelectedIndex<0)
            {
                MainWindow.aucunEntretienSelectionne();
            }
            else
            {
                infoEntretien infoentr = (infoEntretien)entretienIndecis.SelectedItem;
                Entretient ent = Variables.db.Entretient.FirstOrDefault(en => en.NumEntret.ToString().Equals(infoentr.numEntretien));
                ent = Variables.db.Entretient.FirstOrDefault();
                DetailsEntretien fenetre = new DetailsEntretien(ent);
                fenetre.ShowDialog();
            }
            
        }

        private void priseDecision(object sender, RoutedEventArgs e)
        {
            if (entretienIndecis.SelectedIndex < 0)
            {
                MainWindow.aucunEntretienSelectionne();
            }
            else
            {
                infoEntretien infoentr = (infoEntretien)entretienIndecis.SelectedItem;
                Entretient ent = Variables.db.Entretient.FirstOrDefault(en => en.NumEntret.ToString().Equals(infoentr.numEntretien));

                DecisionEntretien decision = new DecisionEntretien(ent);  // passer l'entretien en parametre pour pouvoir le modifier selon le choix du gerant
                decision.ShowDialog();
            }
           
        }

        private void entretiensEnAprobation_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    entretiensEnAprobation.Width = (win as MainWindow).Main.Width;
                    entretiensEnAprobation.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;

                    entretienIndecis.Width = (win as MainWindow).Main.Width - 80;
                    entretienIndecis.Height = (win as MainWindow).Main.Height - 210;

                }
            }
        }
    }
}
