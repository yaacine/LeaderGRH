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
    /// Logique d'interaction pour DetailsEntretien.xaml
    /// </summary>
    public partial class DetailsEntretien : Window
    {
        public DetailsEntretien(Entretient e)
        {
            InitializeComponent();
            numEntretien.Text = e.NumEntret.ToString();
            numCandidat.Text = e.Candidat.NumeroCandidat.ToString();
            DateEntretien.Text =e.DateEntretien.ToString();
            NomCandidat.Text = e.Candidat.Nom;
            PrenomCandidat.Text = e.Candidat.Prenom;
            experience.Text = e.Experience;
            question1.Text = e.Question1;
            reponse1.Text = e.EvaluationQuestion1;
            question2.Text = e.Question2;
            reponse2.Text = e.EvaluationQuestion2;
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
