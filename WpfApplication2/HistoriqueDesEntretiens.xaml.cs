using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication2
{
    public partial class HistoriqueDesEntretiens : Page
    {
        public HistoriqueDesEntretiens()
        {
            InitializeComponent();
        }
        public static List<Evaluation> list = null;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    Historique.Width = (win as MainWindow).Main.Width;
                    Historique.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;

                }
            }
            list = GEvaluation.Recherche_Evaluation_Employe(ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Matricule);
           
            Datagridgraph.ItemsSource = list;
            if (list.Any())
            {
                if (list.Last().NoteEval == null)
                {
                    Planifier_entretien.IsEnabled = false;
                }
                else
                {
                    Planifier_entretien.IsEnabled = true;
                }
            }
            else
            {
                Planifier_entretien.IsEnabled = true;
            }
            
        }

        private void planifier_Entretien_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Planifier_Entretien();
            win.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            list = GEvaluation.Recherche_Evaluation_Employe(ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Matricule);
            Datagridgraph.ItemsSource = list;  
            if (list.Any())
            {
                if (list.Last().NoteEval == null)
                {
                    Planifier_entretien.IsEnabled = false;
                }
                else
                {
                    Planifier_entretien.IsEnabled = true;
                }
            }
            else
            {
                Planifier_entretien.IsEnabled = true;
            }
           
        }

        private void Datagridgraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Visualiser.IsEnabled = true;
          
            Modifier_Date_Evaluer_Enabled();
        }
        private void Modifier_Date_Evaluer_Enabled()
        {
            if(list.Any())
            {
                if (list.ElementAt(Datagridgraph.SelectedIndex).NoteEval == null )
                {
                    Modifier_Date.IsEnabled = true;
                   if (list.ElementAt(Datagridgraph.SelectedIndex).DateEval.Equals(System.DateTime.Today)) Evaluer.IsEnabled = true;
                }
                else
                {
                    Modifier_Date.IsEnabled = false;
                    Evaluer.IsEnabled = false;
                }
            }
            else
            {
                Modifier_Date.IsEnabled = false;
                Evaluer.IsEnabled = false;
            }
            
        }
        private void Visualiser_Click(object sender, RoutedEventArgs e)
        {
            byte[] fichier = (byte[])list.ElementAt(Datagridgraph.SelectedIndex).FichierEval.ToArray();
            File.WriteAllBytes(@"C:\Windows\Temp\fichier.xlsx",fichier);
            System.Diagnostics.Process.Start(@"C:\Windows\Temp\fichier.xlsx");
        }

        private void Evaluer_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            byte[] fichier = (byte[])list.ElementAt(Datagridgraph.SelectedIndex).FichierEval.ToArray();
            File.WriteAllBytes(@"C:\Windows\Temp\fichier.xlsx", fichier);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.WorkingDirectory = @"C:\Windows\Temp";
            p.StartInfo.FileName = "fichier.xlsx";
            p.Start();
            p.WaitForExit();
            GEvaluation.Evaluer(ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Matricule.ToString(), @"C:\Windows\Temp\fichier.xlsx");
            Excel excel = new Excel(@"C:\Windows\Temp\fichier.xlsx", 1);
            string note = excel.readExcelcell(16, 11);//on lis la note introduite
            GEvaluation.Noter(ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Matricule.ToString(), note);
            excel.Close();
            Modifier_Date_Evaluer_Enabled();
            Mouse.OverrideCursor = null;
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new HistoriqueDesEntretiens();
                    break;
                }
            }
        }
    }
}
