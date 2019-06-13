using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Threading;
using WindowWPf;
using System.IO;

namespace WpfApplication2
{
    public class GestionEntretien
    {
      
        public static List<Candidat> Toutlescandidats(string nom = null, string prenom = null, string poste = null, string responsable = null, string status = null)
        {
            var result =
                from var in Variables.db.Candidat
             
                orderby var.NumeroCandidat,var.Nom
                select var;
            return result.ToList();
        }

        public static List<Entretient> RechercheCandidat(string nom="", string prenom="", string date=null)
        {
           
                var result =
                    from var in Variables.db.Entretient
                where (var.Candidat.Nom == nom || var.Candidat.Prenom == prenom || var.DateEntretien == (date == null ? DateTime.Today : DateTime.Parse(date)))
                    orderby var.DateEntretien, var.Candidat.Nom
                    select var;
                
                
                return result.ToList() ?? null;
         }
          /*  catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }*/
        

        public static void ModiferDate(string date,int NUmero)
        {
            DateTime trydate = new DateTime();
            if (DateTime.TryParse(date,out trydate) == false)
            {
                
                return;
            }
                
            Entretient entr = Variables.db.Entretient.FirstOrDefault(e => e.NumeroCandidat.Equals(NUmero));
            entr.DateEntretien = trydate;
            Variables.db.SubmitChanges();
        }

        public static List<Entretient> TousLesEntretienFuture()
        {
            var result =
                from var in Variables.db.Entretient
                where (var.DateEntretien >= DateTime.Today)
                orderby var.NumeroCandidat, var.DateEntretien, var.Candidat.Nom 
                select var;
            return result.ToList();
        }

        internal static object TousLesCandidat()
        {
            throw new NotImplementedException();
        }

        public static void Ajouter_Entretien(int num_candidat,string Experience,string q1,string eq1,string q2, string eq2,decimal salaire,string status,string comment ,string etape,string Date)
        { 
            Candidat e = Variables.db.Candidat.FirstOrDefault(emm => emm.NumeroCandidat.Equals(num_candidat));
            Entretient entr = new Entretient();
            entr.Experience = Experience;
            entr.Question1 = q1;
            entr.EvaluationQuestion1 = eq1;
            entr.Question2 = q2;
            entr.EvaluationQuestion2 = eq2;
            entr.SalaireDesire = salaire ;
            entr.Status = status;
            entr.Commentaires = comment;
            entr.EtapeSuivante = etape;
            entr.DateEntretien = DateTime.Parse(Date);
            entr.fait = 1;
            int i = GestionEntretien.Toutlescandidats().ElementAt(Page2.index).NumeroCandidat;
          
            entr.NumeroCandidat = i;
            e.Entretient.Add(entr);
         //   Variables.db.Entretient.InsertOnSubmit(entr);
            try
            {
                Variables.db.SubmitChanges();
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show( ex.Message);
            }

        }

    }
}
