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
using System.Threading;
using WpfApplication2;

namespace WindowWPf
{
    /// <summary>
    /// Logique d'interaction pour Assistant.xaml
    /// </summary>
    public partial class Assistant : Window
    {
        
        public Frame myFrame
        {
            get
            {
                return MyFrame;
            }
        }

        public int selectedIndex { get; set; }

        private void Update()
        {
            while (true)
            {
                MyFrame.UpdateLayout();
            }
        }
        public Assistant()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

       

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            Exit.Opacity = 1;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            Exit.Opacity = 0.3;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void Window_Closed(object sender, EventArgs e)
        {
            Owner.ShowDialog();
            this.RemoveLogicalChild(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new ChoixUser();
        }

        private void Suivant_Click(object sender, RoutedEventArgs e)
        {
            RestaurerMDP restauration = new RestaurerMDP();
            if (Suivant.Content.Equals("Suivant"))
            {
                MessageShown.Visibility = Visibility.Hidden;
                Suivant.Content = "Confirmer";
                Suivant.IsEnabled = false;
                restauration.selectedIndex = selectedIndex;
                MyFrame.Content = restauration;
            }
            else
            {
                bool resultat =(MyFrame.Content as RestaurerMDP).VerifInfos();
                if (!resultat)
                {
                    MessageBox.Show("Verifiez vos réponses", "Fausses Réponses", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if( Suivant.Content.Equals("Confirmer"))
                {
                    MyFrame.Visibility = Visibility.Hidden;
                    Fin.Visibility = Visibility.Visible;
                    Suivant.Content = "Quitter";    
                }
                else
                {
                    this.Close();
                }
            }
           
        }

        
    }
}
