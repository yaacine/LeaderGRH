using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;
using WpfApplication2;
using WindowWPf;
using System.Windows;

namespace WpfApplication2
{
    public static class GCondidat
    {
        
        public static List<Candidat> tousLesCandidats()
        {
            var result =
                from var in Variables.db.Candidat
                select var;
            return result.ToList();

        }

        public static List<Candidat> RechercheCondidat(String mat, int x)
        {
            bool a = false;
            bool b = false;
            bool c = false;
            if (x == 1) { c = true; b = true; }
            if (x == 2) { a = true; c = true; }
            if (x == 3) { b = true; a = true; }

            var result =
            from var in Variables.db.Candidat
            where (((String.Compare(var.Nom, mat) == 0) || a) && ((String.Compare(var.Prenom, mat) == 0) || b) && ((String.Compare(var.IntitulePoste, mat) == 0) || c))
            //  orderby var.
            select var;
            return result.ToList();
        }

        public static string ajouterCondidat(string nom, string prenom, string numTele, string poste,string cv)
        {
            Candidat condidat = new Candidat();
            condidat.Nom = nom;
            condidat.Prenom = prenom;
            condidat.NumeroTel = numTele;
            condidat.IntitulePoste = poste;
           
            condidat.cvCabdidat = File.ReadAllBytes(cv);

            try
            {
                Variables.db.Candidat.InsertOnSubmit(condidat);
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Candidat Ajouté avec succès";
        }



    }
}

