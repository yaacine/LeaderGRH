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
using System.IO;
using System.Reflection;
using System.Threading;
using WpfApplication2;
namespace WindowWPf
{
    using static Variables;
    public partial class Login : Window
    {
        private string connectionstring = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Project_Data.mdf;Integrated Security=True;Connect Timeout=30";
        Thread Rech;
        public Login()
        {

            //Variables.db = new BaseDataContext(connectionstring);
            InitializeComponent();
            if (Keyboard.IsKeyToggled(Key.CapsLock)) Maj.Visibility = Visibility.Visible;
            Rech = new Thread(LoadUserList);  // Chargement de la liste des utilisateurs dans la mémoire centrale avec un thread séparé.
            try
            {
                Rech.Start();
            }
            catch (ThreadStartException ex)
            {
                MessageBox.Show("Erreur lors du chargement de la liste d'utilisateurs ,veuillez relancer le logiciel", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
              
            }
        }

        public void LoadUserList() //Charger la liste des utilisateurs de la BDD.
        {
            userlist = (from var in Variables.db.Users
                        select var).ToList();
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Exit.Opacity = 1;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Exit.Opacity = 0.5;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reduce_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Reduce_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Reduce.Opacity = 1;
        }

        private void Reduce_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Reduce.Opacity = 0.5;
        }

        private void Authentification_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void UserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && UserName.Foreground != Brushes.White)
            {
                UserName.Text = "";
                UserName.Foreground = Brushes.White;
            }
        }

        private void PasswordField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && PasswordField.Foreground != Brushes.White)
            {

                PasswordField.Clear();
                PasswordField.Foreground = Brushes.White;
            }
        }

        private void SeConnecter_Click(object sender, RoutedEventArgs e) //Verification du username et du mot de passe.
        {
            List<Users> rech;
            debut: //Etiquette
            if (!Rech.IsAlive) // Verifie si le thread Rech est inactif (La liste des utilisateurs a été chargée en mémoire centrale ).
            {
                rech = (from a in userlist
                        where a.Identifiant.Equals(UserName.Text)
                        select a).ToList();


                if (!rech.Any())
                {
                    UserName.Foreground = Brushes.Red;
                    UserName.Text = "L'Utilisateur n'existe pas";
                }

                else
                {
                    var user = rech[0];
                    if (!user.MDP.Equals(PasswordField.Password.ToString()))
                    {
                        UserName.Foreground = Brushes.Red;
                        UserName.Text = "Mot de passe erroné";
                        PasswordField.Clear();
                    }
                    else
                    {
                        MainWindow win = new MainWindow();
                        win.NomUser.Content = user.Identifiant;
                        win.Show();
                        this.Close();

                    }
                }
            }
            else
            {
                goto debut; // Verfier une autre fois.
            }
        }

        private void SeConnecter_MouseEnter(object sender, MouseEventArgs e)
        {
            SeConnecter.Opacity = 1;

        }

        private void SeConnecter_MouseLeave(object sender, MouseEventArgs e)
        {
            SeConnecter.Opacity = 0.5;

        }

        private void Oublié_MouseEnter(object sender, MouseEventArgs e)
        {
            Oublié.Opacity = 1;
            Oublié.FontWeight = FontWeights.Bold;

        }

        private void Oublié_MouseLeave(object sender, MouseEventArgs e)
        {
            Oublié.Opacity = 0.5;
            Oublié.FontWeight = FontWeights.Normal;

        }

        private void SeConnecter_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SeConnecter.Background = Brushes.Snow;
            SeConnecter.Foreground = Brushes.Black;
        }

        private void SeConnecter_LostMouseCapture(object sender, MouseEventArgs e)
        {
            SeConnecter.Background = Brushes.Black;
            SeConnecter.Foreground = Brushes.White;
        }

        private void Oublié_GotMouseCapture(object sender, MouseEventArgs e)

        {
            Oublié.Foreground = Brushes.DarkBlue;
        }

        private void Authentification_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.CapsLock) && Maj.Visibility == Visibility.Hidden)
            {
                Maj.Visibility = Visibility.Visible;
            }
            else if (Keyboard.IsKeyDown(Key.CapsLock) && Maj.Visibility == Visibility.Visible)
            {
                Maj.Visibility = Visibility.Hidden;
            }


        }

        private void Authentification_StateChanged(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyToggled(Key.CapsLock)) Maj.Visibility = Visibility.Visible;
            else if (Keyboard.IsKeyUp(Key.CapsLock)) Maj.Visibility = Visibility.Hidden;
        }

        private void Authentification_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Keyboard.IsKeyToggled(Key.CapsLock)) Maj.Visibility = Visibility.Visible;
            else if (Keyboard.IsKeyUp(Key.CapsLock)) Maj.Visibility = Visibility.Hidden;
        }

        private void Oublié_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Assistant assitant = new Assistant();
            assitant.Owner = this;
            this.Hide();
            assitant.ShowDialog();
        }

        private void PasswordField_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                List<Users> rech;
                debut: //Etiquette
                if (!Rech.IsAlive) // Verifie si le thread Rech est inactif (La liste des utilisateurs a été chargée en mémoire centrale ).
                {
                    rech = (from a in userlist
                            where a.Identifiant.Equals(UserName.Text)
                            select a).ToList();


                    if (!rech.Any())
                    {
                        UserName.Foreground = Brushes.Red;
                        UserName.Text = "L'Utilisateur n'existe pas";
                    }

                    else
                    {
                        var user = rech[0];
                        if (!user.MDP.Equals(PasswordField.Password.ToString()))
                        {
                            UserName.Foreground = Brushes.Red;
                            UserName.Text = "Mot de passe erroné";
                            PasswordField.Clear();
                        }
                        else
                        {
                            MainWindow win = new MainWindow();
                            win.NomUser.Content = user.Identifiant;
                            win.Show();
                            this.Close();

                        }
                    }
                }
                else
                {
                    goto debut; // Verfier une autre fois.
                }
            }
        }
    }
}

