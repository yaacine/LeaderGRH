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
    /// Logique d'interaction pour Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    { public static string d { get; set; }
        public Window3()
        {
            InitializeComponent();
        }

        private void DatePicker_Initialized(object sender, EventArgs e)
        {
            datee.DisplayDateStart = DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { if (datee.Text == null || datee.Text =="")
            {
                MessageBox.Show("aucune modification a été proposée");
            }
            else
            {
                int i = GestionEntretien.Toutlescandidats().ElementAt(Page2.index).NumeroCandidat;
                d = datee.Text;
                GestionEntretien.ModiferDate(datee.Text, i);
                MessageBox.Show("Fin De Modification Avec Succées");
                this.Close();
            }

        }
    }
}
