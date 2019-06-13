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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using WpfApplication2;

namespace WindowWPf
{
    /// <summary>
    /// Logique d'interaction pour ChoixUser.xaml
    
    public partial class ChoixUser : Page
    {
      

        public ChoixUser()
        {
            InitializeComponent();
            
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserGrid.ItemsSource = Variables.userlist;
        }

        private void UserGrid_GotMouseCapture(object sender, MouseEventArgs e)
        {
           foreach(Window win in  App.Current.Windows)
            {
                if (win.Title.Equals("Assistant"))
                {
                    
                    (win as Assistant).selectedIndex = UserGrid.SelectedIndex;
                    (win as Assistant).Suivant.IsEnabled = true;
                    break;
                }
            }
        }

    }
}
