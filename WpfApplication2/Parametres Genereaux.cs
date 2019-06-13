using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowWPf;
namespace WpfApplication2
{
    using System.IO;
    public static class Parametres_Genereaux
    {
        
        public static string ModifierParametres(string Raison ,string Specialité,string SiteWeb,string NGerant,string PreGerant
            ,string Adresse,string Tel,string Fax,string MailGerant,string NumeroRC,string NumFiscal,string Logo,string Wilaya)
        {
            bool nouveau = false;
            var param = Variables.db.Parametres?.First();
            if (param == null)
            {
                param = new Parametres();
                nouveau = true;
            }
            param.Raison_Sociale = Raison;
            param.Speciaite = Specialité;
            param.Site_Web = SiteWeb;
            param.NomGerant = NGerant;
            param.PrenomGerant = PreGerant;
            param.Adresse = Adresse;
            param.Telephone = Tel;
            param.Fax = Fax;
            param.MailGerant = MailGerant;
            param.NumeroRC = NumeroRC;
            param.IdFiscale = NumFiscal;
            param.Wilaya = Wilaya;
            try
            {
                param.Logo = System.IO.File.ReadAllBytes(Logo);
            }
            catch 
            {
                return "Erreur lors du chargement du Logo";
            }

            if (nouveau)
            {
                try
                { 
                Variables.db.Parametres.InsertOnSubmit(param);
                }

                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            try
            {
                
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }

        public static string AddUser(string IDUser,string Mdp,string Qualité,string Question1,string Question2,string Reponse1,string Reponse2,string photo)
        {
            
           if (Variables.userlist != null) { 
                if (Variables.userlist.FirstOrDefault(e => e.Identifiant.Equals(IDUser)) != null )
                {
                 return "L'utilisateur existe déja ";
                }
            }

            Users user = new Users();
            user.Identifiant = IDUser;
            user.MDP = Mdp;
            user.Qualite = Qualité;
            user.QuestSec1 = Question1;
            user.QuestSec2 = Question2;
            user.Reponse1 = Reponse1;
            user.Reponse2 = Reponse2;
            user.ThemeApp = "Bleu";
            try
            {
                user.Photo_Profil = File.ReadAllBytes(photo);
            }
            catch(Exception ex)
            {
                
            }
            try
            {
                Variables.db.Users.InsertOnSubmit(user);
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }
        public static List<Users> toutlescomptes()
        {
            var result =
                from var in Variables.db.Users
                select var;
            return result.ToList();
        }
        public static List<Users> recherche(string ID)
        {
            var result =
                from var in Variables.db.Users
                where (var.Identifiant == ID)
                select var;
            return result.ToList();
        }
        public static string ModifyUser(string IDUser,string Mdp, string ThemeApp)
        {
            var user = Variables.db.Users.FirstOrDefault(k => k.Identifiant.Equals(IDUser));
            user.MDP = Mdp;
            try
            {   
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }

        public static string ModifyUserTheme(string IDUser,string Theme)
        {
            var user = Variables.db.Users.FirstOrDefault(k => k.Identifiant.Equals(IDUser));
            user.ThemeApp = Theme;
            try
            {
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public static string AjouterParametres(string Raison, string Specialité, string SiteWeb, string NGerant, string PreGerant
          , string Adresse, string Tel, string Fax, string MailGerant, string NumeroRC, string NumFiscal, string Logo, string Wilaya)
        {

            Parametres param = new Parametres();

            param.Raison_Sociale = Raison;
            param.Speciaite = Specialité;
            param.Site_Web = SiteWeb;
            param.NomGerant = NGerant;
            param.PrenomGerant = PreGerant;
            param.Adresse = Adresse;
            param.Telephone = Tel;
            param.Fax = Fax;
            param.MailGerant = MailGerant;
            param.NumeroRC = NumeroRC;
            param.IdFiscale = NumFiscal;
            param.Wilaya = Wilaya;
            try
            {
                param.Logo = System.IO.File.ReadAllBytes(Logo);
            }
            catch
            {
                return "Erreur lors du chargement du Logo";
            }


            try
            {
                Variables.db.Parametres.InsertOnSubmit(param);
                Variables.db.SubmitChanges();
            }

            catch (Exception ex)
            {
                return ex.Message;
            }


            return null;
        }



        public static string ModifyUserPhoto(string IDUser, string path)
        {
            var user = Variables.db.Users.FirstOrDefault(k => k.Identifiant.Equals(IDUser));
            user.Photo_Profil = File.ReadAllBytes(path);
            try
            {
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }


    }
}
