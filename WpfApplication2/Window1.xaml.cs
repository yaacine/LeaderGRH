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
using WpfApplication2;

namespace WpfApplication2
{

    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Datagridgraph1_Initialized(object sender, EventArgs e)
        {
            var liste1 = GAdministrative.toutlesemploye();
            var Liste2= GConges.Recherche(mat:liste1.ElementAt(Modifier_Employe.index_detaille).Matricule);
            Datagridgraph1.ItemsSource = Liste2;
        }
    }
}
