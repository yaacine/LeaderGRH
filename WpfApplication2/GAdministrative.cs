using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using WindowWPf;
namespace WpfApplication2
{
    public static class GAdministrative
    {

        public static List<Employe>  listeEmployesactifs;

        public static string AjouterEmployé(string nom, string prenom, DateTime datedenaissance, string situation, string sexe,
           DateTime ddembauch, string poste, string responsable, string salaire, string projet, string numeroimat,
           string cv,string photo, string status, string adresse, string numerotel, string adressemail, string coorBanc, string comment)
        {

            List<Employe> existance;
            Employe employe;
            existance = RechercheExistance(nom, prenom, poste, responsable, status, numeroimat, numerotel);
            if (existance.Count != 0)
            {
                MainWindow.employeExisteDeja();
                return "0";
            }
            else
            {
                employe = new Employe();
                employe.Nom = nom; // champs limité a 50.
                employe.Prenom = prenom;  // champs limité a 50.
                employe.DateDeNaissance = datedenaissance;
                employe.SituationFamille = situation;
                employe.DateEmbauche = ddembauch;
                employe.Poste = poste;
                employe.Sexe = sexe;
                employe.Responsable = responsable;
                try
                {
                    Salaires sal = new Salaires();
                    sal.Salaire = decimal.Parse(salaire);
                    sal.DateDajout = ddembauch;
                    employe.Salaires.Add(sal);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                employe.Projet = projet;
                employe.NumImatSocial = numeroimat;
                try
                {
                    employe.CV = File.ReadAllBytes(cv);
                }
                catch (IOException ex)
                {
                    return ex.Message;
                }
                if (photo !=null && photo != "")
                {
                    try
                    {
                        employe.PhotoProfil = File.ReadAllBytes(photo);
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                {

                    byte[] array = System.IO.File.ReadAllBytes($@"{ System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Resources\Employe.png");
                    employe.PhotoProfil = array; 
                }
               
                employe.Status = status;
                employe.Adresse = adresse;
                employe.NumeroTel = numerotel;
                employe.Email = adressemail;
                employe.CoorBancaires = coorBanc;
                employe.Commentaires = comment;
                employe.NbJourConge = 0.0f;
                try
                {
                    Variables.db.Employe.InsertOnSubmit(employe);
                    Variables.db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                return null;
            }

        }


        public static List<Employe> Recherche(string nom = null, string prenom = null, string poste = null, string responsable = null, string status = null, string matricule = null)
        {
            var result =
                from var in Variables.db.Employe
                where (var.Nom == nom || var.Prenom == prenom || var.Poste == poste || var.Status == status || var.Responsable == responsable || var.Matricule.ToString() == matricule)
                orderby var.Nom, var.Prenom
                select var;
            return result.ToList();
        }


        public static string InitialiserListe()
        {
            var result =
                from var in Variables.db.Employe
                where var.Status.ToUpper() == "ACTIF"
                select var;
            listeEmployesactifs = result.ToList();

            if (listeEmployesactifs != null)
            {
                foreach (var emp in listeEmployesactifs)
                {
                    emp.NbJourConge = ((DateTime.Today.Year - ((DateTime)emp.DateEmbauche).Year) * 12 + (DateTime.Today.Month) - ((DateTime)emp.DateEmbauche).Month) * 1.81;
                }
                try
                {
                    Variables.db.SubmitChanges();
                }
                catch (IOException ex)
                {
                    return ex.Message;
                }
                return "1";
            }
            else
            {
                return "0";
            }
        }


        public static List<Employe> RechercheExistance(string nom = null, string prenom = null, string poste = null, string responsable = null, string status = null, string matSociale = null, string telephone = null)
        {
            var result =
                from var in Variables.db.Employe
                where (var.Nom == nom && var.Prenom == prenom && var.Poste == poste && var.Status == status && var.Responsable == responsable && var.NumImatSocial.ToString() == matSociale && var.NumeroTel.ToString() == telephone)
                orderby var.Nom, var.Prenom
                select var;
            return result.ToList();
        }

        public static List<Employe> toutlesemploye(string nom = null, string prenom = null, string poste = null, string responsable = null, string status = null)
        {
            var result =
                from var in Variables.db.Employe
                orderby var.Nom, var.Prenom
                select var;
            return result.ToList();
        }
        public static void ModifierEmployé(string nom, string prenom, DateTime? datedenaissance, string situation,
            DateTime? ddembauch, string poste, string responsable, string projet, string numeroimat
            , string status, string adresse,decimal? Salaire, string numerotel, string adressemail, string coorBanc, string comment, string cv,string photo)
        {
            Employe employe = Variables.db.Employe.FirstOrDefault(e => e.NumImatSocial.Equals(numeroimat));
            employe.Nom = nom;
            employe.Prenom = prenom;
            employe.DateDeNaissance = datedenaissance;
            employe.SituationFamille = situation;
            employe.DateEmbauche = ddembauch;
            employe.Poste = poste;
            employe.Responsable = responsable;
            employe.Projet = projet;
            employe.Status = status;
            employe.Adresse = adresse;
            employe.NumeroTel = numerotel;
            employe.Email = adressemail;
            employe.CoorBancaires = coorBanc;
            employe.Commentaires = comment;
            Salaires sal = new Salaires();
            sal.Salaire = Salaire;
            sal.DateDajout = DateTime.Today;
            employe.Salaires.Add(sal);
            if(cv != null)
            {
                employe.CV = File.ReadAllBytes(cv);
            }

            if (photo != null)
            {
                employe.PhotoProfil = File.ReadAllBytes(photo);
            }
            Variables.db.SubmitChanges();
        }


        public static void supprimer(int matricule)
        {
            Employe emp = Variables.db.Employe.FirstOrDefault(e => e.Matricule.Equals(matricule));
            /*
            emp.Salaires.Clear();  //suppression de la liste des salaires
            emp.Conges.Clear();
            emp.Evaluation.Clear();     
            Variables.db.Employe.DeleteOnSubmit(emp);
            Variables.db.SubmitChanges();*/
        }
    }
}
