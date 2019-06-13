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
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using WindowWPf;
namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Page1 : Page 
    {
        private string filename;
        public static List<Employe> listeEmployes;
        
        public Page1()
        {
            InitializeComponent();
            listeEmployes = GAdministrative.listeEmployesactifs;
             List<String> emlpoyesPourCombobox= new List<string>();  // liste a utiliser dans le combobox
            foreach (Employe var in listeEmployes)
            {
                //emlpoyesPourCombobox.Add(var.Matricule.ToString() + "  " + var.Nom + "  " + var.Prenom);
                MAT.Items.Add(var.Matricule.ToString() + "  " + var.Nom + "  " + var.Prenom);
            }


            
        }
        private void cv_Click(object sender, RoutedEventArgs e)
        {
            var file = new OpenFileDialog();
            {
                file.Title = "Chargez le CV";
                file.Filter = "Document(*.docx)|*.docx|Image(*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp";
                var result = file.ShowDialog();
                filename = file.FileName;
            }

            CheminFormulaire.Text = filename;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index1 = MAT.SelectedIndex;
            int matricule;
            if(index1<0)
            {
                //alerter de choisir un employé de la liste
            }
            else
            {
                if (filename == null || filename == "")
                {
                    MainWindow.FormNonCharge();
                }

                else
                {
                    matricule = listeEmployes.ElementAt(index1).Matricule;
                    string b = GConges.AjouterConge(matricule, DateD.Text, DateF.Text, Type.Text, filename);
                    if(b=="1")
                    {
                        Employe e1 = Variables.db.Employe.FirstOrDefault(t => t.Matricule.Equals(matricule));
                        TitreConge.genererTitreConge(e1);
                    }
                    
                }
            }
           
                
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var file = new OpenFileDialog();
            {
                file.Title = "Chargez le CV";
                file.Filter = "Document(*.docx)|*.docx|Image(*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp";
                var result = file.ShowDialog();
                filename = file.FileName;
            }
            if(file.FileName!=null)
            {
                CheminFormulaire.Text = filename;
            }
        }


        private bool IsNumeric(string Nombre)
        {
            bool k = true;
            for (int i = 0; i < Nombre.Length; i++)
            {
                if (Nombre[i] < 48 || Nombre[i] > 57)
                {
                    k = false;
                }
            }
            return k;
        }


     

      

        private void detefutureDebut_Ajout(object sender, EventArgs e)
        {
            DateD.DisplayDateStart = DateTime.Today;
        }

        private void futureDateFin_Ajouter(object sender, EventArgs e)
        {
            DateF.DisplayDateStart = DateTime.Today;
        }


        private void ajouterConge_Loaded(object sender, RoutedEventArgs e)
        {
            
            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    ajouterConge.Width = (win as MainWindow).Main.Width;
                    ajouterConge.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    
                }
            }
        }

        private void combolisteEmploes_Initialized(object sender, EventArgs e)
        {
           
        }
    }
}

