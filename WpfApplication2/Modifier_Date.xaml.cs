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

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Modifier_Date.xaml
    /// </summary>
    public partial class Modifier_Date : Window
    {
        public Modifier_Date()
        {
            InitializeComponent();
        }

        private void Datedenaissance_Initialized(object sender, EventArgs e)
        {
            DateTime date = DateTime.Today;
            NewDateEntretien.DisplayDateStart = date;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            GEvaluation.Modifier_Date(ListeDesEmploye_Evaluation.liste.ElementAt(ListeDesEmploye_Evaluation.index).Matricule,DateTime.Parse(NewDateEntretien.Text));
            MessageBox.Show("Changement appliquer avec succes!");
            this.Close();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(NewDateEntretien.Text != "")
            {
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = false;
            }
        }
    }
}
