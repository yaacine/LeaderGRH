using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;

namespace WpfApplication2
{
    public static class GConges
    {

        public const int maxConges = 2;// a charger de la tables de paramtres generaux de l'application

        // les desu tables suivantes sont necessaires pour faire les tests de chauvauchements des congés

        public static DateTime[] jours; 
        public static int[] nbConges;


        public static List<Conges> listeConges;
        public static List<Conges> listeTousConges;



        public static bool estEnConge(Employe e) //retourne vrai si lemployé e est en congé
        {

            if (e.Conges.Any()) //Verifie si il y'a au moins un congé pris.
            {
                if (e.Conges.Last().DateFin.CompareTo(DateTime.Now) == 1 ) // Verifie si la date de fin du congé est supérieure                                                                            // a la date actuelle.
                {
                    return true;
                }
            }
            return false;

        }


        public static string AjouterConge(int Matricule, string DateD, string DateF, String TypeConge, String Form)
        {
            if(TypeConge.ToUpper()!="ANNUEL" && TypeConge.ToUpper() != "MALADIE" && TypeConge.ToUpper() != "SANS SOLDE")
            {
                MainWindow.typeNonChoisi();
                return "0";
            }
            else
            {
                Employe e = Variables.db.Employe.FirstOrDefault(emm => emm.Matricule.Equals(Matricule));

                if (e != null)
                {
                    if (e.Status.ToUpper() != "ACTIF")
                    {
                        MainWindow.employInactif(e);
                        return "0";
                    }
                    else
                    {
                        if (DateTime.Parse(DateD) >= DateTime.Parse(DateF))
                        {
                            MainWindow.alertErreurDate();
                            return "0";
                        }
                        else
                        {
                            int nbDemandes = (int)(DateTime.Parse(DateF) - DateTime.Parse(DateD)).TotalDays;
                            bool congesPossible = false;
                            int nbCongesPossibles = 0;
                            Conges Conge = new Conges();
                            Conge.DateDebut = DateTime.Parse(DateD);
                            Conge.DateFin = DateTime.Parse(DateF);
                            Conge.DateRetour = DateTime.Parse(DateF);
                            Conge.Type = TypeConge;
                            try
                            {
                                Conge.Formulaire = File.ReadAllBytes(Form);
                            }
                            catch (IOException ex)
                            {
                                return ex.Message;
                            }


                            if (estEnConge(e))
                            {
                                MainWindow.employeAPrisConges(e);
                                return "conges non ajouté";
                            }
                            else
                            {
                                if (TypeConge.ToUpper() == "MALADIE" || TypeConge.ToUpper() == "SANS SOLDE")
                                {
                                    testChauvauchement(DateTime.Parse(DateD), DateTime.Parse(DateF), out congesPossible, out nbCongesPossibles);
                                    if (congesPossible)
                                    {
                                        // accorder le congé et afficher que c'est acquis

                                        e.Conges.Add(Conge);

                                        try
                                        {
                                            Variables.db.SubmitChanges();
                                        }
                                        catch (IOException ex)
                                        {
                                            return ex.Message;
                                        }

                                        MainWindow.CongeBienAjoute();
                                        return "1";
                                    }
                                    else
                                    {
                                        bool reponse1 = MainWindow.alerteChauvauchement(nbCongesPossibles);
                                        if (reponse1) // le cas d'un ajout mamlgré le chauvauchement
                                        {
                                            // accorder le congé et afficher que c'est acquis
                                            e.Conges.Add(Conge);

                                            try
                                            {
                                                Variables.db.SubmitChanges();
                                            }
                                            catch (IOException ex)
                                            {
                                                return ex.Message;
                                            }

                                            MainWindow.CongeBienAjoute();
                                            return "1";
                                        }
                                        else
                                        {
                                            return "Conges non ajouté";
                                        }
                                    }
                                }

                                else // cas de conges annuel
                                {
                                    if (nbDemandes > e.NbJourConge)  // nombre demandé > nombre possible --> alerte
                                    {
                                        MainWindow.alerteDepassmentJoursPermis((int)e.NbJourConge, nbDemandes);
                                        return "Conges non Ajouté";
                                    }
                                    else
                                    {
                                        testChauvauchement(DateTime.Parse(DateD), DateTime.Parse(DateF), out congesPossible, out nbCongesPossibles);
                                        if (congesPossible)
                                        {
                                            // accorder le congé et afficher que c'est acquis
                                            e.Conges.Add(Conge);
                                            e.NbJourConge = e.NbJourConge - nbDemandes;
                                            e.CongesConsomme += nbDemandes;
                                            try
                                            {
                                                Variables.db.SubmitChanges();
                                            }
                                            catch (IOException ex)
                                            {
                                                return ex.Message;
                                            }

                                            MainWindow.CongeBienAjoute();
                                            return "1";
                                        }
                                        else
                                        {
                                            bool reponse1 = MainWindow.alerteChauvauchement(nbCongesPossibles);
                                            if (reponse1) // le cas d'un ajout mamlgré le chauvauchement
                                            {
                                                // accorder le congé et afficher que c'est acquis
                                                e.Conges.Add(Conge);
                                                e.NbJourConge = e.NbJourConge - nbDemandes;
                                                e.CongesConsomme -= nbDemandes;
                                                try
                                                {
                                                    Variables.db.SubmitChanges();
                                                }
                                                catch (IOException ex)
                                                {
                                                    return ex.Message;
                                                }

                                                MainWindow.CongeBienAjoute();
                                                return "1";
                                            }
                                            else
                                            {
                                                return "Conges non ajouté";
                                            }

                                        }




                                    }


                                }
                            }
                        }


                    }
                }
                else
                {
                    MainWindow.matriculeNexistePas();
                    return "0";
                }
            }
            
             
           
        }




        public static List<Conges> Recherche(int? mat = null, int? mois = null, int? an = null)
        {
            bool a = false;
            bool b = false;
            bool c = false;
            if (an == null) { c = true; }
            if (mat == null) { a = true; }
            if (mois == null) { b = true; }

            var result =
            from var in Variables.db.Conges
            where (((var.Matricule == mat) || a) && (((var.DateFin.Month >= mois) && (var.DateDebut.Month <= mois)) || b) && (((var.DateFin.Year >= an) && (var.DateDebut.Year <= an)) || c))
            orderby var.DateDebut
            select var;
            return result.ToList();


        }
        public static List<Conges> Recherche2(String nom = null, int? mois = null, int? an = null)
        {
            bool a = false;
            bool b = false;
            bool c = false;
            if (an == null) { c = true; }
            if (nom == null) { a = true; }
            if (mois == null) { b = true; }

            var result =
            from var in Variables.db.Conges
            where (((String.Compare(var.Employe.Nom, nom) == 0) || a) && (((var.DateFin.Month >= mois) && (var.DateDebut.Month <= mois)) || b) && (((var.DateFin.Year >= an) && (var.DateDebut.Year <= an)) || c))
            orderby var.DateDebut
            select var;
            return result.ToList();


        }
        public static List<Conges> ListeAllConges()
        {

            var result1 =
            from var in Variables.db.Conges
            orderby var.DateFin
            select var;
            if (result1.Any())
            {
                return result1.ToList();
            }
            else return null;
        }






        //Methode pour recuperer la liste des congés qui sont superiers à la date d'aujordhui
        public static List<Conges> ListeConges()
        {

            if (Variables.db.Conges.Any())
            {
                var result1 =
            from var in Variables.db.Conges
            where (var.DateFin >= DateTime.Today)
            orderby var.DateFin
            select var;
                if (result1.Any())
                {
                    return result1.ToList();
                }
                else return null;
            }
            else
            {
                return null;
            }
            
            
        }


        public static void creerTableJours() // creer un tableau de jours à utiliser pour le calcul des chauvauchements de congés
        {

            if( !Variables.db.Conges.Any())
            {

            }
            else
            {
                var listeConges = ListeConges();  // creation effective de la liste des congés
                Conges lastItem = listeConges.Last();
                DateTime lastDate = (DateTime)lastItem.DateFin;  // recuperer la derniere date
                DateTime counter;
                int i = 0;

                int nbDays = (int)(lastDate - DateTime.Today).TotalDays;
                jours = new DateTime[nbDays + 1];  // allocation dynamique de la tabels des jours
                nbConges = new int[nbDays + 1];    // allocation dynamique de la teble des congés

                for (counter = DateTime.Today; counter <= lastDate; counter = counter.AddDays(1))
                {
                    jours[i] = counter;
                    nbConges[i] = 0;
                    foreach (var conge in listeConges)
                    {
                        if (counter >= conge.DateDebut && counter <= conge.DateFin)
                        {
                            nbConges[i]++;
                        }
                    }

                    i++;
                }
                int taille = jours.Length;
                DateTime date2 = jours[jours.Length - 1];
                DateTime date3 = jours[jours.Length - 1];
            }
            

        }

        public static void testChauvauchement(DateTime datedebut, DateTime datefin, out bool possible, out int maxJoursPossibles)// retourne vrai s'il ya un chauvauchement critique
        {

            creerTableJours();
            int nbJours = (int)(datefin - datedebut).TotalDays;
            if (jours == null && !Variables.db.Conges.Any())
            {
                possible = true;
                maxJoursPossibles = nbJours;
            }
            else
            {
                bool stop = false;
                int i = 0;
                int difference;
                maxJoursPossibles = nbJours;
                possible = false;

                int dernier = jours.Length - 1;
                if (datedebut > jours[dernier])
                {
                    possible = true;
                    maxJoursPossibles = nbJours;
                }
                else
                {
                    difference = (int)((datedebut - (DateTime.Today)).TotalDays);
                    i = difference;
                    while (jours[i] <= datefin && (!stop) && i < jours.Length - 1)
                    {

                        DateTime t = jours[i];
                        if (nbConges[i] > maxConges)
                        {
                            stop = true;
                            maxJoursPossibles = i - difference; // retourne le nombre de jours maximal pour lesquels il n'ya pas de chauvauchements
                        }
                        else
                        {
                            i++;
                        }
                    }

                    if (!stop)
                    {
                        possible = true;
                    }


                }
            }



        }


        public static List<Conges> RechercheMatricule(int matricule) // retourne une liste de congés qui ont le meme matricule demandé
        {
            var result =
                from var in Variables.db.Conges
                where (var.Matricule == matricule)
                select var;
            if (result.Any())
            {
                return result.ToList();
            }
            else
            {
                return null;
            }

        }



        public static String modifierConges(int Matricule, DateTime dateDebut, DateTime dateFin, String typeConges)
        {
            // si le date de fin du conges est expirée l'utilisateur sera alerté lors de la selection et clique sur modifier

          if (dateDebut>= dateFin)
            {
                MainWindow.alertErreurDate();
                return "0";
            }
            else
            {
                Employe em = Variables.db.Employe.FirstOrDefault(e => e.Matricule.Equals(Matricule));
                Conges c = em.Conges.LastOrDefault();
                //Conges c = Variables.db.Conges.FirstOrDefault(t => t.Matricule.Equals(Matricule));           

                string type = c.Type;


                if (typeConges.ToUpper() == "ANNUEL" && typeConges.ToUpper() == "SANS SOLDE" && typeConges.ToUpper() == "MALADIE")
                {
                    MainWindow.typeNonChoisi();
                    return "0";
                }
                else
                {
                    if (c.DateDebut == dateDebut && c.DateFin == dateFin && c.Type.ToUpper() == typeConges.ToUpper())
                    {
                        MainWindow.pasDeChangements();
                        return "pas de changements ";

                    }
                    else
                    {
                        int nbJoursPossibles = 0;
                        bool congesPossible = false;
                        bool reponseChauvauchement = false;
                        int totalJout = 0;
                        int ajoutDebut = 0;
                        int ajoutFin = 0;

                        if (dateDebut != c.DateDebut) ajoutDebut = (int)((DateTime)c.DateDebut - dateDebut).TotalDays;
                        if (dateFin != c.DateFin) ajoutFin = (int)(dateFin - (DateTime)c.DateFin).TotalDays;

                        if (c.DateDebut >= DateTime.Today)
                        {
                            if (c.Type.ToUpper() == "MALADIE")
                            {
                                if (typeConges.ToUpper() == "ANNUEL")
                                {
                                    if ((dateFin - dateDebut).TotalDays <= em.NbJourConge)
                                    {
                                        testChauvauchement(dateDebut, dateDebut, out congesPossible, out nbJoursPossibles);
                                        if (congesPossible == true)
                                        {
                                            c.Type = "annuel";
                                            c.DateDebut = dateDebut;
                                            c.DateFin = dateFin;
                                            c.DateRetour = dateFin;
                                            totalJout = ajoutDebut + ajoutFin;
                                            em.NbJourConge -= (dateFin - dateDebut).TotalDays;
                                            em.CongesConsomme += (int)(dateFin - dateDebut).TotalDays;
                                            try
                                            {
                                                Variables.db.SubmitChanges();
                                                return "1";
                                            }
                                            catch (IOException ex)
                                            {
                                                return ex.Message;
                                            }

                                        }
                                        else // il ya chauvauchement
                                        {
                                            reponseChauvauchement = MainWindow.alerteChauvauchement(nbJoursPossibles);
                                            if (reponseChauvauchement == true)
                                            {
                                                c.Type = "annuel";
                                                c.DateDebut = dateDebut;
                                                c.DateFin = dateFin;
                                                c.DateRetour = dateFin;
                                                totalJout = ajoutDebut + ajoutFin;
                                                em.NbJourConge -= (dateFin - dateDebut).TotalDays;
                                                em.CongesConsomme += (int)(dateFin - dateDebut).TotalDays;
                                                try
                                                {
                                                    Variables.db.SubmitChanges();
                                                    return "1";
                                                }
                                                catch (IOException ex)
                                                {
                                                    return ex.Message;
                                                }
                                            }
                                            else
                                            {
                                                return "La modification a été abondonnée ";
                                            }
                                        }

                                    }
                                    else  // n'a pas le nombre de joures de conges qu"il demande
                                    {

                                        MainWindow.alerteDepassmentJoursPermis((int)em.NbJourConge, (int)(dateFin - dateDebut).TotalDays);
                                        return "Depassement du nombre des jours permis";
                                    }

                                }// typeConges == annuel
                                else
                                {
                                    if (typeConges.ToUpper() == "MALADIE" || typeConges.ToUpper() == "SANS SOLDE")
                                    {
                                        testChauvauchement(dateDebut, dateFin, out congesPossible, out nbJoursPossibles);
                                        if (congesPossible)
                                        {
                                            c.DateDebut = dateDebut;
                                            c.DateFin = dateFin;
                                            c.DateRetour = dateFin;
                                            try
                                            {
                                                Variables.db.SubmitChanges();
                                                return "1";
                                            }
                                            catch (IOException ex)
                                            {
                                                return ex.Message;
                                            }
                                        }
                                        else //il ya chauvauchement
                                        {
                                            reponseChauvauchement = MainWindow.alerteChauvauchement(nbJoursPossibles);
                                            if (reponseChauvauchement == true)
                                            {
                                                c.DateDebut = dateDebut;
                                                c.DateFin = dateFin;
                                                c.DateRetour = dateFin;
                                                c.Type = typeConges;
                                                try
                                                {
                                                    Variables.db.SubmitChanges();
                                                    return "1";
                                                }
                                                catch (IOException ex)
                                                {
                                                    return ex.Message;
                                                }
                                            }
                                            else
                                            {
                                                return "La modification a été abondonnée ";
                                            }
                                        }//il ya chauvauchement
                                    }
                                }
                            }//c.type== MALADIE
                            else
                            {
                                if (c.Type.ToUpper() == "ANNUEL")
                                {
                                    if (typeConges.ToUpper() == "ANNUEL")
                                    {
                                        totalJout = ajoutDebut + ajoutFin;
                                        if (totalJout > em.NbJourConge)
                                        {
                                            MainWindow.alerteDepassmentJoursPermis((int)em.NbJourConge, totalJout);
                                            return "Depassement du nombre des jours permis";
                                        }
                                        else
                                        {
                                            testChauvauchement(dateDebut, dateFin, out congesPossible, out nbJoursPossibles);
                                            if (congesPossible)
                                            {
                                                c.DateDebut = dateDebut;
                                                c.DateFin = dateFin;
                                                c.DateRetour = dateFin;
                                                c.Type = typeConges;
                                                em.NbJourConge -= totalJout;
                                                em.CongesConsomme += totalJout;
                                                try
                                                {
                                                    Variables.db.SubmitChanges();
                                                    return "1";
                                                }
                                                catch (IOException ex)
                                                {
                                                    return ex.Message;
                                                }
                                            }
                                            else
                                            {
                                                reponseChauvauchement = MainWindow.alerteChauvauchement(nbJoursPossibles);
                                                if (reponseChauvauchement == true)
                                                {
                                                    c.DateDebut = dateDebut;
                                                    c.DateFin = dateFin;
                                                    c.DateRetour = dateFin;
                                                    c.Type = typeConges;
                                                    em.NbJourConge -= totalJout;
                                                    em.CongesConsomme += totalJout;
                                                    try
                                                    {
                                                        Variables.db.SubmitChanges();
                                                        return "1";
                                                    }
                                                    catch (IOException ex)
                                                    {
                                                        return ex.Message;
                                                    }
                                                }
                                                else
                                                {
                                                    return "La modification a été abondonnée ";
                                                }
                                            }
                                        }
                                    }// nouveau conges est annuel
                                    else
                                    {
                                        if (typeConges.ToUpper() == "MALADIE" || typeConges.ToUpper() == "SANS SOLDE")
                                        {
                                            testChauvauchement(dateDebut, dateFin, out congesPossible, out nbJoursPossibles);
                                            if (congesPossible == true)
                                            {
                                                totalJout = (int)((DateTime)c.DateFin - (DateTime)c.DateDebut).TotalDays;
                                                em.NbJourConge += totalJout;
                                                em.CongesConsomme -= totalJout;
                                                c.DateDebut = dateDebut;
                                                c.DateFin = dateFin;
                                                c.DateRetour = dateFin;
                                                c.Type = typeConges;
                                                try
                                                {
                                                    Variables.db.SubmitChanges();
                                                    return "1";
                                                }
                                                catch (IOException ex)
                                                {
                                                    return ex.Message;
                                                }
                                            }
                                            else
                                            {
                                                reponseChauvauchement = MainWindow.alerteChauvauchement(nbJoursPossibles);
                                                if (reponseChauvauchement == true)
                                                {
                                                    totalJout = (int)((DateTime)c.DateFin - (DateTime)c.DateDebut).TotalDays;
                                                    em.NbJourConge += totalJout;
                                                    em.CongesConsomme -= totalJout;
                                                    c.DateDebut = dateDebut;
                                                    c.DateFin = dateFin;
                                                    c.DateRetour = dateFin;
                                                    c.Type = typeConges;
                                                    try
                                                    {
                                                        Variables.db.SubmitChanges();
                                                        return "1";
                                                    }
                                                    catch (IOException ex)
                                                    {
                                                        return ex.Message;
                                                    }
                                                }
                                                else
                                                {
                                                    return "La modification a été abondonnée ";
                                                }
                                            }
                                        }
                                    }

                                }// le congé a modifier est un annuel (c.Type== annuel )
                            }
                        } //date debut <= today  le conges n'a pas encore commencé
                        else    //dateDebut < today --> pas possible de modifier le date de debut 
                        {

                            if (c.Type.ToUpper() == "ANNUEL")
                            {
                                if (typeConges.ToUpper() == "MALADIE" || typeConges.ToUpper() == "SANS SOLDE")
                                {
                                    totalJout = (int)((DateTime)c.DateFin - dateDebut).TotalDays;
                                    c.DateFin = dateDebut;
                                    em.NbJourConge += totalJout;
                                    em.CongesConsomme -= totalJout;
                                    try
                                    {
                                        Variables.db.SubmitChanges();

                                    }
                                    catch (IOException ex)
                                    {
                                        return ex.Message;
                                    }
                                    AjouterConge(Matricule, dateDebut.ToString(), dateFin.ToString(), typeConges, "formulaire");
                                    return "1";
                                }
                                else   // typeconges= annuel
                                {
                                    if ((DateTime)c.DateDebut != dateDebut)
                                    {
                                        MainWindow.alerteModifierDateDebut();
                                    }
                                    else
                                    {
                                        c.DateFin = dateFin;
                                        em.NbJourConge -= ajoutFin;
                                        em.CongesConsomme += ajoutFin;
                                        try
                                        {
                                            Variables.db.SubmitChanges();
                                            return "1";
                                        }
                                        catch (IOException ex)
                                        {
                                            return ex.Message;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (c.Type.ToUpper() == "MALADIE" || c.Type.ToUpper() == "SANS SOLDE")
                                {
                                    if (typeConges.ToUpper() == "MALADIE" || typeConges.ToUpper() == "SANS SOLDE")
                                    {
                                        if (c.Type.ToUpper() == typeConges.ToUpper())  // changer juste la date de fin
                                        {
                                            c.DateFin = dateFin;
                                            c.DateRetour = dateFin;
                                            try
                                            {
                                                Variables.db.SubmitChanges();
                                                return "1";
                                            }
                                            catch (IOException ex)
                                            {
                                                return ex.Message;
                                            }
                                        }
                                        else
                                        {
                                            c.DateFin = dateDebut;
                                            c.DateRetour = dateDebut;
                                            try
                                            {
                                                Variables.db.SubmitChanges();

                                            }
                                            catch (IOException ex)
                                            {
                                                return ex.Message;
                                            }
                                            AjouterConge(Matricule, dateDebut.ToString(), dateFin.ToString(), typeConges, "form");
                                            return "1";
                                        }
                                    } //demande conges de maladie/ sans solde 
                                    else //demande de conges annuel
                                    {
                                        totalJout = ajoutDebut + ajoutFin;
                                        if (totalJout > em.NbJourConge)
                                        {
                                            MainWindow.alerteDepassmentJoursPermis((int)em.NbJourConge, totalJout);
                                            return "Depassement du nombre des jours permis";
                                        }
                                        else
                                        {
                                            testChauvauchement(dateDebut, dateFin, out congesPossible, out nbJoursPossibles);
                                            if (congesPossible)
                                            {
                                                c.DateDebut = dateDebut;
                                                c.DateFin = dateFin;
                                                c.DateRetour = dateFin;
                                                c.Type = typeConges;
                                                em.NbJourConge -= totalJout;
                                                em.CongesConsomme += totalJout;
                                                try
                                                {
                                                    Variables.db.SubmitChanges();
                                                    return "1";
                                                }
                                                catch (IOException ex)
                                                {
                                                    return ex.Message;
                                                }
                                            }
                                            else
                                            {
                                                reponseChauvauchement = MainWindow.alerteChauvauchement(nbJoursPossibles);
                                                if (reponseChauvauchement == true)
                                                {
                                                    c.DateDebut = dateDebut;
                                                    c.DateFin = dateFin;
                                                    c.DateRetour = dateFin;
                                                    c.Type = typeConges;
                                                    em.NbJourConge -= totalJout;
                                                    em.CongesConsomme += totalJout;
                                                    try
                                                    {
                                                        Variables.db.SubmitChanges();
                                                        return "1";
                                                    }
                                                    catch (IOException ex)
                                                    {
                                                        return ex.Message;
                                                    }
                                                }
                                                else
                                                {
                                                    return "La modification a été abondonnée ";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        return "modification terminée avec succes ";

                    }//il ya un changement
                }

            }


        }// modifier conges


   



    }
}
