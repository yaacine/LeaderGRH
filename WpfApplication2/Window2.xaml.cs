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
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Datagridgraph1_Initialized(object sender, EventArgs e)
        {
            var liste1 = GAdministrative.toutlesemploye();
            var liste = GAdministrative.Recherche(matricule: liste1.ElementAt(Modifier_Employe.index_detaille).Matricule.ToString());
            Datagridgraph1.ItemsSource = liste.ElementAt(0).Salaires;
        }
    }
}
