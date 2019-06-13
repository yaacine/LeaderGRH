using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WindowWPf;


namespace WpfApplication2
{
    class SuiviRecrutements
    {
        public static List<Entretient> listeFutursEntretiens;
        public static List<Entretient> listeEntretiensNonDecides;

        public static List<Entretient> futurEntretiens()
        {
            var result =
                from var in Variables.db.Entretient
                where (DateTime)var.DateEntretien >= DateTime.Today
                orderby var.DateEntretien
                select var;

            return result.ToList();
        }


        // methode qui construit la liste des entretiens qui non encore décidés 
        public static void EntretienNonDecides()
        {
            var result =
                from var in Variables.db.Entretient
                where (var.fait==1 && var.EtapeSuivante.ToUpper()== "Ne pas décider maintenant" &&  (DateTime)var.DateEntretien<=DateTime.Today)
                orderby var.DateEntretien
                select var;
           
            listeEntretiensNonDecides = result.ToList();
        }
    }
}
