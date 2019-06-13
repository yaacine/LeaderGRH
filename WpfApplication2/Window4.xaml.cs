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
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        private string cv = null;
        public Window4()
        {
            InitializeComponent();
           
        }

        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            DATE.Text = DateTime.Today.ToShortDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { if (Exper.Text == "" || q1.Text == "" || eq1.Text == "" || q2.Text == "" || eq2.Text == "" || saldesir.Text == "" || status.Text == "" || comment.Text == "" || Etape.Text == "" )
            {
                MessageBox.Show("Tous les champs sont obligatoires a remplire !");
            }
            else
            {
                if (Etape.Text == "Refaire Entretien")
                {
                    Window3 wind = new Window3();
                    wind.ShowDialog();
                    int i = GestionEntretien.Toutlescandidats().ElementAt(Page2.index).NumeroCandidat;
                    GestionEntretien.ModiferDate(wind.datee.Text, i);
                }
                else
                { 
                    int i = GestionEntretien.Toutlescandidats().ElementAt(Page2.index).NumeroCandidat;
                    GestionEntretien.Ajouter_Entretien(i, Exper.Text, q1.Text, eq1.Text, q2.Text, eq2.Text, decimal.Parse(saldesir.Text), status.Text, comment.Text, Etape.Text,DateTime.Now.ToShortDateString());
                    MessageBox.Show("Donne de l'entretien sauvegarde.");
                }
                this.Close();
            }
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void saldesir_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in saldesir.Text)
            {
                if (Char.IsDigit(c))
                    sb.Append(c);
            }
            saldesir.Text = sb.ToString();
            saldesir.Select(saldesir.Text.Length, 0);
        }
    }
}
