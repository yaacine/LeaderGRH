using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using WindowWPf;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= {System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Project_Data.mdf;Integrated Security=True;Connect Timeout=30";
            Variables.db = new BaseDataContext(connectionString);

            if (Variables.db.Users.Any()) //Si au moins un utilisateur existe ,on ouvre la fênetre du login
            {
                Login login = new Login();
                login.Show();
                
            }
            else // Si aucun utilisateur existe ,on ouvre l'assistant
            {
                AssistantDemarrage assist = new AssistantDemarrage();
                assist.Show();
                
            }

        }
    }

   
}
