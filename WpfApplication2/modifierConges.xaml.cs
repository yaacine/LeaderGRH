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
    /// Logique d'interaction pour modifierConges.xaml
    /// </summary>
    public partial class modifierConges : Window
    {
        public modifierConges(DateTime debut, DateTime fin, String type, int matricule)
        {
            InitializeComponent();

            globalVar.Text = matricule.ToString();
           
            datedebutModifier1.SelectedDate = debut;
            datefinModifier.SelectedDate = fin;
            if (debut <= DateTime.Today)
            {
                datedebutModifier1.IsEnabled = false;
                typeModifier.IsEnabled = false;
            }
            if (fin <= DateTime.Today) datefinModifier.IsEnabled = false;


            switch (type.ToUpper())
            {
                case "ANNUEL":
                    {
                        typeModifier.SelectedIndex = 0;
                    }
                    break;
                case "MALADIE":
                    {
                        typeModifier.SelectedIndex = 1;
                    }
                    break;
                case "SANS SOLDE":
                    {
                        typeModifier.SelectedIndex = 2;
                    }
                    break;
            }
            
           
        }

        

        private void appelModificationConges(object sender, RoutedEventArgs e)
        {
            if (typeModifier.SelectedIndex<0)
            {
                MessageBox.Show("Veuillez choisir le type du congé");
            }
            else
            {
                DateTime debut = (DateTime)datedebutModifier1.SelectedDate;
                DateTime fin = (DateTime)datefinModifier.SelectedDate;
                string typeConges="";
                switch (typeModifier.SelectedIndex)
                {
                    case 0:
                        {
                            typeConges = "annuel";
                        }break;

                    case 1:
                        {
                            typeConges = "maladie";
                        }break;

                    case 2:
                        {
                            typeConges = "sans solde";
                        }break;
                }
                
                int matricule = int.Parse(globalVar.Text);


               string result= GConges.modifierConges(matricule, debut, fin, typeConges);
                if (result=="1")
                {
                    MessageBox.Show("Modification terminée avec succés","Terminé", MessageBoxButton.OK,MessageBoxImage.Asterisk);
                }
            }
           
           
        }

        private void Datefuture(object sender, EventArgs e)
        {
            datedebutModifier1.DisplayDateStart = DateTime.Today;
        }

        private void Datefuturefin(object sender, EventArgs e)
        {
            datefinModifier.DisplayDateStart = DateTime.Today;
        }
    }
}
