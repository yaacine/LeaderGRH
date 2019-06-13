using Microsoft.Win32;
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
    /// Logique d'interaction pour AjouterCondidat.xaml
    /// </summary>
    public partial class AjouterCondidat : Window
    {
        private string cv = null;
        private bool enabel = false;
        public AjouterCondidat()
        {
            InitializeComponent();
        }

        private Boolean Enable_submit()
        {
            bool k;
            if ((string)Commentaire_nom.Content == "")
            {
                if ((string)Commentaire_prenom.Content == "")
                {
                    if ((string)Commentaire_poste.Content == "")
                    {
                        if((string)Commentaire_CV.Content =="")
                        {
                            if((string)Commentaire_telephone.Content == "")
                            {
                                k = true;
                            }
                            else
                            {
                                k = false;
                            }
                        }
                        else
                        {
                            k = false;
                        }   
                    }
                    else
                    {
                        k = false;
                    }
                }
                else
                {
                    k = false;
                }
            }
            else k = false;
            return k;         
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string resultat = GCondidat.ajouterCondidat(NomChamp.Text, PrenomChamp.Text, NumeroTeleChamp.Text, PosteChamp.Text,cv);
            MessageBox.Show(resultat);

            foreach (Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    (win as MainWindow).Main.Content = new ListeCondidat();
                }
            }
            this.Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir le CV";
            try { op.Filter = "CV File(*.docx )|*.docx"; }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

            if (op.ShowDialog() == true)
            {
                cv = op.FileName;
                Commentaire_CV.Content = "";
            }
            enabel = Enable_submit();
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


        private void NomChamp_LostFocus(object sender, RoutedEventArgs e)
        {        
            if (NomChamp.Text == "")
            {
                Commentaire_nom.Content = "Champ obligatoire!";
            }
            else
            {
                if (IsChar(NomChamp.Text) == false)
                {
                    Commentaire_nom.Content = "Les chiffres sont interdits!";
                    NomChamp.Foreground = Brushes.Red;
                }  
                else
                {
                    Commentaire_nom.Content = "";
                    NomChamp.Foreground = Brushes.Black;
                }
            }
            enabel = Enable_submit();
        }

        private void PrenomChamp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PrenomChamp.Text == "")
            {
                Commentaire_prenom.Content = "Champ obligatoire!";
            }
            else
            {
                if (IsChar(PrenomChamp.Text) == false)
                {
                    Commentaire_prenom.Content = "Les chiffres sont interdits!";
                    PrenomChamp.Foreground = Brushes.Red;
                }
                else
                {
                    Commentaire_prenom.Content = "";
                    PrenomChamp.Foreground = Brushes.Black;
                }
            }
            enabel = Enable_submit();
        }

        private void NumeroTeleChamp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NumeroTeleChamp.Text == "")
            {
                Commentaire_telephone.Content = "Champ obligatoire!";
            }
            else
            {
                Commentaire_telephone.Content = "";
            }
        }

        private void PosteChamp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PosteChamp.Text == "")
            {
                Commentaire_poste.Content = "Champ obligatoire!";
            }
            else
            {
                if (IsChar(PosteChamp.Text) == false)
                {
                   Commentaire_poste.Content= "les chiffres sont interdits!";
                   PosteChamp.Foreground = Brushes.Red;
                }
                else
                {
                    Commentaire_poste.Content = "";
                    PosteChamp.Foreground = Brushes.Black;
                }
            }
            enabel = Enable_submit();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (enabel == true)
            {
                Enregistrer.IsEnabled = true;
            }
            else
            {
                Enregistrer.IsEnabled = false;
            }
        }

        private void NumeroTeleChamp_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in NumeroTeleChamp.Text)
            {
                if (Char.IsDigit(c))
                    sb.Append(c);
            }
            NumeroTeleChamp.Text = sb.ToString();
            NumeroTeleChamp.Select(NumeroTeleChamp.Text.Length, 0);
        }
    }
}
