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
    /// Logique d'interaction pour RestaurerMDP.xaml
    /// </summary>
    using static Visibility; 
    public partial class RestaurerMDP : Page
    {
        
        public int selectedIndex
        {
            get; set;
        }
        public RestaurerMDP()
        {
            InitializeComponent();
           
        }
       
       
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Question1.Text = Variables.userlist[selectedIndex].QuestSec1;
            Question2.Text = Variables.userlist[selectedIndex].QuestSec2;
        }

        private void Reponse1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Reponse1.Text.Length >= 3)
            {
                image1.Visibility = Visible;
            }
            else
            {
                image1.Visibility = Hidden;
            }
        }

        private void Reponse2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Reponse2.Text.Length >= 3)
            {
                image2.Visibility = Visible;
            }
            else
            {
                image2.Visibility = Hidden;
            }
            
        }

        private void Mdp_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Mdp.Password.Length >=6)
            {
                image3.Visibility = Visible;
            }
            else
            {
                image3.Visibility = Hidden;
            }
        }

        private void Confirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Confirm.Password.Equals(Mdp.Password))
            {
                image4.Visibility = Visible;
            }
            else
            {
                image4.Visibility = Hidden;
            }
        }

        private void image1_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (image1.Visibility == Visible && image2.Visibility == Visible && image3.Visibility == Visible && image4.Visibility == Visible)
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.Title.Equals("Assistant"))
                    {
                        (win as Assistant).Suivant.IsEnabled = true;
                        break;
                    }
                }
            }
            else
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.Title.Equals("Assistant"))
                    {
                        (win as Assistant).Suivant.IsEnabled = false;
                        break;
                    }
                }
            }
        }

        public bool VerifInfos ()
        {
            
            if (Reponse1.Text.Equals(Variables.userlist[selectedIndex].Reponse1) && Reponse2.Text.Equals(Variables.userlist[selectedIndex].Reponse2))
            {
                Variables.userlist[selectedIndex].MDP = Mdp.Password;
                try
                {
                    Variables.db.SubmitChanges();
                    return true;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
