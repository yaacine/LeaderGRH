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

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour ListeCongés.xaml
    /// </summary>
    public partial class ListeCongés : Page
    {
        public ListeCongés()
        {
            InitializeComponent();
        }

        private void Searche_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (Matricule_filtre.IsSelected == true)
            {
                if (IsNumeric(searche_info.Text))
                {
                    if (MoisCombo.SelectedIndex > 0)
                    {
                        if (AnneeCombo.SelectedIndex > 0)
                        {
                            var liste = GConges.Recherche(Convert.ToInt32(searche_info.Text), (int)MoisCombo.SelectedIndex, (int)AnneeCombo.SelectedItem);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                            CongesFormatte c;
                            if (liste != null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;


                        }
                        else
                        {
                            var liste = GConges.Recherche(Convert.ToInt32(searche_info.Text), (int)MoisCombo.SelectedIndex, null);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                            CongesFormatte c;
                            if (liste!=null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;

                        }
                    }
                    else
                    {
                        if (AnneeCombo.SelectedIndex > 0)
                        {
                            var liste = GConges.Recherche(Convert.ToInt32(searche_info.Text), null, (int)AnneeCombo.SelectedItem);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                            
                            CongesFormatte c;
                            if(liste!= null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;

                        }
                        else
                        {
                            var liste = GConges.Recherche(Convert.ToInt32(searche_info.Text), null, null);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                            
                            CongesFormatte c;
                            if (liste!= null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez entrez une Matricule !");
                }

            }
            else if (Nom_filtre.IsSelected == true)
            {
                if (IsChar(searche_info.Text))
                {
                    if (MoisCombo.SelectedIndex > 0)
                    {
                        if (AnneeCombo.SelectedIndex > 0)
                        {
                            var liste = GConges.Recherche2(searche_info.Text, (int)MoisCombo.SelectedIndex, (int)AnneeCombo.SelectedItem);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                            
                            CongesFormatte c;
                            if(liste!= null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;

                        }
                        else
                        {
                            var liste = GConges.Recherche2(searche_info.Text, (int)MoisCombo.SelectedIndex, null);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                           
                            CongesFormatte c;
                            if (liste!= null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;

                        }
                    }
                    else
                    {
                        if (AnneeCombo.SelectedIndex > 0)
                        {
                            var liste = GConges.Recherche2(searche_info.Text, null, (int)AnneeCombo.SelectedItem);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                            
                            CongesFormatte c;
                            if(liste!= null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;

                        }
                        else
                        {
                            var liste = GConges.Recherche2(searche_info.Text, null, null);
                            List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                           
                            CongesFormatte c;
                            if(liste!= null)
                            {
                                foreach (Conges var in liste)
                                {
                                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                                    listeCongeFormat.Add(c);
                                }
                                Datagridgraph.ItemsSource = listeCongeFormat;
                            }
                            else Datagridgraph.ItemsSource = null;

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez entrez un Nom !");
                }

            }
            else if (MoisCombo.SelectedIndex > 0)
            {
                if (AnneeCombo.SelectedIndex > 0)
                {
                    var liste = GConges.Recherche2(null, (int)MoisCombo.SelectedIndex, (int)AnneeCombo.SelectedItem);
                    List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                    
                    CongesFormatte c;
                    if (liste != null)
                    {
                        foreach (Conges var in liste)
                        {
                            c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                            listeCongeFormat.Add(c);
                        }
                        Datagridgraph.ItemsSource = listeCongeFormat;
                    }
                    else Datagridgraph.ItemsSource = null;


                }
                else
                {
                    var liste = GConges.Recherche2(null, (int)MoisCombo.SelectedIndex, null);
                    List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                   
                    CongesFormatte c;
                    if(liste!= null)
                    {
                        foreach (Conges var in liste)
                        {
                            c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                            listeCongeFormat.Add(c);
                        }
                        Datagridgraph.ItemsSource = listeCongeFormat;
                    }
                    else
                    {
                        Datagridgraph.ItemsSource = null;
                    }
                    
                }
            }
            else if (AnneeCombo.SelectedIndex > 0)
            {
                var liste = GConges.Recherche2(null, null, (int)AnneeCombo.SelectedItem);
                List<CongesFormatte> listeCongeFormat = new List<CongesFormatte>();
                
                CongesFormatte c;
                if(liste!= null)
                {
                    foreach (Conges var in liste)
                    {
                        c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                        listeCongeFormat.Add(c);
                    }
                    Datagridgraph.ItemsSource = listeCongeFormat;
                }
                else
                { Datagridgraph.ItemsSource = null; }
               
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

        private bool IsChar(string Chaine)
        {
            bool k = true;
            for (int i = 0; i < Chaine.Length; i++)
            {
                if (Chaine[i] >= 48 && Chaine[i] <= 57)
                {
                    k = false;
                }
            }
            return k;
        }

         // class d'un congé formatté selon l'affichage 
        public class CongesFormatte
        {
            public string nom { get; set; }
            public string prenom { get; set; }
            public string matricule { get; set; }
            public string DateDebut { get; set; }
            public string DateFin { get; set; }
            public string Type { get; set; }

            public CongesFormatte(string nom, string prenom,string matricule, string dateD, string DateF, string type)
            {
                this.nom = nom;
                this.prenom = prenom;
                this.matricule = matricule;
                DateDebut = dateD;
                DateFin = DateF;
                this.Type = type;

            }

        }

        private void Datagridgraph_Initialized(object sender, EventArgs e)
        {
            List<CongesFormatte> listeCongeFormat=new List<CongesFormatte>();
            var liste = GConges.ListeAllConges();
            CongesFormatte c;
            if (liste!= null)
            {
                foreach (Conges var in liste)
                {
                    c = new CongesFormatte(var.Employe.Nom, var.Employe.Prenom, var.Employe.Matricule.ToString(), var.DateDebut.ToShortDateString(), var.DateFin.ToShortDateString(), var.Type);
                    listeCongeFormat.Add(c);
                }
                Datagridgraph.ItemsSource = listeCongeFormat;
            }
            else Datagridgraph.ItemsSource = null;

        }

        private void MoisCombo_Initialized(object sender, EventArgs e)
        {
            MoisCombo.Items.Add("Aucun");
            MoisCombo.Items.Add("Janvier");
            MoisCombo.Items.Add("Février");
            MoisCombo.Items.Add("Mars");
            MoisCombo.Items.Add("Avril");
            MoisCombo.Items.Add("Mai");
            MoisCombo.Items.Add("Juin");
            MoisCombo.Items.Add("Juillet");
            MoisCombo.Items.Add("Aout");
            MoisCombo.Items.Add("Septembre");
            MoisCombo.Items.Add("Octobre");
            MoisCombo.Items.Add("Novembre");
            MoisCombo.Items.Add("Decembre");
        }

        private void AnneeCombo_Initialized(object sender, EventArgs e)
        {
            AnneeCombo.Items.Add("Aucun");
            for (int i =2017 ; i < 2019; i++)
            {
                AnneeCombo.Items.Add(i);
            }
        }

        private void ModifierCongeButton(object sender, RoutedEventArgs e)
        {
            int index = Datagridgraph.SelectedIndex;
            if (index <0)
            {
                MessageBox.Show("Veuillez selectionner un Congé !" , "Attention", MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {

               // Conges c = GConges.ListeAllConges().ElementAt(index);
                CongesFormatte conge= (CongesFormatte)Datagridgraph.SelectedItem;

                // DateTime myDate = DateTime.ParseExact(conge.DateFin+"14:40:52,531", "MM-dd-yyyy HH:mm:ss,fff",
                //                      System.Globalization.CultureInfo.InvariantCulture);

                DateTime myDate = new DateTime();
                if (conge.DateFin.Contains('-'))
                {
                    myDate = DateTime.ParseExact(conge.DateFin, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    myDate = DateTime.ParseExact(conge.DateFin, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }

                Conges c = Variables.db.Conges.FirstOrDefault(ce => (ce.Employe.Matricule.Equals(conge.matricule) && ((ce.DateFin)).Equals(myDate)));
                int matricule = c.Employe.Matricule;  
                modifierConges windowModifierConges = new modifierConges((DateTime)c.DateDebut, (DateTime)c.DateFin, c.Type , matricule);
                windowModifierConges.ShowDialog();
            }
      
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ApercuConge conge = new ApercuConge();
            conge.ShowDialog();
        }

        private void Datagridgraph_Initialized(object sender, RoutedEventArgs e)
        {

            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    listeConges1.Width = (win as MainWindow).Main.Width;
                    listeConges1.Height = (win as MainWindow).Main.Height;
                    titre.Width = (win as MainWindow).Main.Width - 140;
                    barreDeRecherche.Width = (win as MainWindow).Main.Width + 20;
                    stackPanelfiltres.Width = (win as MainWindow).Main.Width + 50;
                    Datagridgraph.Width = (win as MainWindow).Main.Width - 80;
                    Datagridgraph.Height = (win as MainWindow).Main.Height - 210;

                }
            }
        }

        private void searche_info_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Datagridgraph_Initialized_1(object sender, EventArgs e)
        {

        }
    }
}
