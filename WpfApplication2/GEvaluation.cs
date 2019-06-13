using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class GEvaluation
    {
        //retourne une liste qi contient les evaluations d'un employe au matricule matricule
        public static List<Evaluation> Recherche_Evaluation_Employe(int matricule)
        {
            var result =
                from var in Variables.db.Evaluation
                where (var.Matricule == matricule)
                orderby var.DateEval 
                select var;
            return result.ToList();
        }
        public static string Planifier_Entretien_evaluation(int matricule, DateTime DateEvaluation, string Path_Fichier, Decimal? Note)
        {
            Evaluation eval = new Evaluation();
            eval.Matricule = matricule;
            eval.NoteEval = Note;
            eval.DateEval = DateEvaluation;
            try
            {
                eval.FichierEval = File.ReadAllBytes(Path_Fichier);
            }
            catch (IOException ex)
            {
                return ex.Message;
            }
            try
            {
                Variables.db.Evaluation.InsertOnSubmit(eval);
                Variables.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }
        public static void Evaluer(string matricule,string path)
        {
            Evaluation eval = Variables.db.Evaluation.FirstOrDefault(e => e.Matricule.Equals(matricule));
            if (path != null)
            {
                eval.FichierEval = File.ReadAllBytes(path);
            }
            Variables.db.SubmitChanges();
        }
        public static void Noter(string matricule ,string Note)
        {
            List<Evaluation> list = (from var in Variables.db.Evaluation
                                  select var).ToList();
            Evaluation eval = list.Last();
            if(Note.Contains("/"))
            {
                char [] note = new char[100];
                if(Note.Contains(","))
                {
                    int i = 0;
                    while(Note[i]!=',')
                    {
                        i++;
                    }
                    eval.NoteEval = decimal.Parse(Note.Substring(0,i));
                }
                else
                {
                    string note2 = Note.First().ToString() + Note.ElementAt(1).ToString();
                }
            }    
            else
            {
                eval.NoteEval = decimal.Parse(Note);
            }
            
            Variables.db.SubmitChanges();
        }

        public static void Modifier_Date(int matricule, DateTime date)
        {
            Evaluation eval = Variables.db.Evaluation.FirstOrDefault(e => e.Matricule.Equals(matricule));
            eval.DateEval = date;
            Variables.db.SubmitChanges();
        }

        public static List<Evaluation> EvalFuture()
        {
            var result =
                from var in Variables.db.Evaluation
                where (var.DateEval >= DateTime.Now)
                orderby var.DateEval
                select var;
            return result.ToList();
        }
        public static List<Evaluation_Info> Evalfutureinfo(List<Evaluation> liste)
        {
           
            var result =
               from var in Variables.db.Evaluation
               where (var.DateEval >= DateTime.Now)
               orderby var.DateEval
               select new Evaluation_Info
               {
                   nom = var.Employe.Nom,
                   Prenom =var.Employe.Prenom,
                   Poste = var.Employe.Poste,
                   Eval = var.DateEval.ToString()

        };
            return result.ToList();
        }


    }
}
