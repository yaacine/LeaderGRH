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
    /// Logique d'interaction pour DecisionEntretien.xaml
    /// </summary>
    public partial class DecisionEntretien : Window
    {
        public static Entretient AmettreAjour;
        public DecisionEntretien(Entretient entretien)
        {

            InitializeComponent();
            AmettreAjour = entretien;
            
        }


        private void decisionPrise(object sender, RoutedEventArgs e)  // au clique sur le bouton d'enregistrement d'une decision
        {
            if(decisionCombo.SelectedIndex<0)
            {

            }
            else
            {
                string dec = decisionCombo.SelectedIndex.ToString();
                bool enregisterMaintenat = false;
                switch (dec)
                {
                    case "0":
                        {
                            MainWindow.recruterEmploye(out enregisterMaintenat);  // demander si le gerant veut enregistrer le nouveau employé maintanat ou plus tard
                            if (enregisterMaintenat == true)
                            {
                                AmettreAjour.EtapeSuivante = "Recrute";
                                Variables.db.SubmitChanges();                               
                                foreach (Window win in App.Current.Windows)
                                {
                                    if (win.Title.Equals("GRH"))
                                    {
                                        (win as MainWindow).Main.Content = new Ajouter_Employe();
                                    }
                                }
                            }
                            else
                            {
                                AmettreAjour.EtapeSuivante = "Recrute";                   
                                Variables.db.SubmitChanges();                                
                                // rien a faire le gerant decide de l'ntregidtrer ultérieurement
                            }
                        }
                        break;
                    case "1":
                        {   
                            // candidat refusé
                            AmettreAjour.EtapeSuivante = "Refuse";
                            Variables.db.SubmitChanges();                            
                        }
                        break;
                    case "2":
                        {
                            PlanifierEntretien aa = new PlanifierEntretien((Candidat)AmettreAjour.Candidat);
                            aa.ShowDialog();
                        }
                        break;
                }
            }
            this.Close();
        }

        private void decisionCombo_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(decisionCombo.SelectedIndex<0)
            {
                Sauvegarder.IsEnabled = false;
            }
            else
            {
                Sauvegarder.IsEnabled = true;
            }
        }
    }
}
