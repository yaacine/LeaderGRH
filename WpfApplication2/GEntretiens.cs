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
    public static class GEntretiens
    {
        public static List<Entretient> tousLesEntretiens()
        {
            var result =
                from var in Variables.db.Entretient
                select var;
            return result.ToList();

        }

        public static string ajouterFutureEntretien(Candidat condidat, DateTime date)
        {
            Entretient entretien = new Entretient();
            //entretien.Candidat = condidat;
            entretien.DateEntretien = date;
           
            entretien.NumeroCandidat = condidat.NumeroCandidat;
            
          //  MessageBox.Show(date.ToString()+condidat.Nom);
            try
            {
                Variables.db.Entretient.InsertOnSubmit(entretien);
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Entretien Ajouter";
        }
    }
    
}
