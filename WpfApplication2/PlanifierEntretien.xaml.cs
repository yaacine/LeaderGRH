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
    /// Logique d'interaction pour PlanifierEntretien.xaml
    /// </summary>
    public partial class PlanifierEntretien : Window
    {
        private Candidat condidat;

        public PlanifierEntretien(Candidat condid)
        {
            InitializeComponent();
            condidat = condid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string resultat = GEntretiens.ajouterFutureEntretien(condidat,(DateTime) FutureDatePicker.SelectedDate);
            MessageBox.Show(resultat);

        }

        private void FutureDatePicker_Initialized(object sender, EventArgs e)
        {
            FutureDatePicker.DisplayDateStart = DateTime.Today;
        }
    }
    }

