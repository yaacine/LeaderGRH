using System.Windows;
using System;
using System.Collections.ObjectModel;
using Syncfusion.Windows.Controls.Gantt;
using Syncfusion.Windows.Controls.Grid;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using Syncfusion.Windows.Shared;
using System.Linq;
using WindowWPf;
using System.Reflection;
using System.Windows.Input;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour ApercuConge.xaml
    /// </summary>
    public partial class ApercuConge : Window
    {
        
        public ApercuConge()
        {
            InitializeComponent();
            GantView.ItemsSource = new ObservableCollection<Task>();
            GantView.ItemsSource = GetDataSourceStartToStart();
            
        }

        ObservableCollection<Task> GetDataSourceStartToStart()
        {
            ObservableCollection<Task> task = new ObservableCollection<Task>();
            var List = from var in Variables.db.Conges
                       where (var.DateFin >= DateTime.Today)
                       select var;

            int i = 1;

            foreach(var It in List)
            {
                task.Add(new Task
                {
                    Id = i,
                    Nom = It.Employe.Nom + " " + It.Employe.Prenom,
                    Debut = It.DateDebut ,
                    Fin = It.DateFin,
                    Type = It.Type

                });
                i++;
            }

           

            return task;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Reduce_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
