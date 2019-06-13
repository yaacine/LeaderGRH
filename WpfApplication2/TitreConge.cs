using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
using WpfApplication2;

namespace WindowWPf
{
    public static class TitreConge
    {
       

        public static void genererTitreConge(Employe e)
        {
            var entreprise = Variables.db.Parametres.FirstOrDefault();
            Document document = new Document();
            document.LoadFromFile($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Resources\REF-TITRE DE CONGES.docx");

            if (e.Nom != null) document.Replace("NOM_EMPLOYE", e.Nom, true, true);
            if (e.Prenom != null) document.Replace("PRENOM_EMPLOYE",e.Prenom,true, true);
            if (entreprise.NomGerant != null) document.Replace("NOM_G", entreprise.NomGerant, true, true);
            if (entreprise.PrenomGerant != null) document.Replace("PRENOM_G", entreprise.PrenomGerant, true, true);
            if (entreprise.Wilaya != null) document.Replace("WILAYA1", entreprise.Wilaya, true, true);
            if (e.Poste != null) document.Replace("POSTE",e.Poste, true, true);
             document.Replace("DATE", DateTime.Now.ToShortDateString(), true, true);
            if (entreprise.Raison_Sociale != null) document.Replace("RAISON_SOCIALE", entreprise.Raison_Sociale, true, true);
            if (entreprise.Speciaite != null) document.Replace("SPECIALITE", entreprise.Speciaite, true, true);
            if (entreprise.IdFiscale != null) document.Replace("MATRICULE_FISCAL", entreprise.IdFiscale, true, true);
            if (entreprise.Adresse != null) document.Replace("ADRESSE",entreprise.Adresse, true, true);
             document.Replace("NB_JOUR_CONGE", (e.Conges.Last().DateFin- e.Conges.Last().DateDebut).TotalDays.ToString(), true, true);
            document.Replace("DATE_DEBUT", e.Conges.Last().DateDebut.ToShortDateString(), true, true);
            document.Replace("DATE_FIN", e.Conges.Last().DateFin.ToShortDateString(), true, true);
            document.Replace("DATE_RETOUR", e.Conges.Last().DateFin.AddDays(1).ToShortDateString(), true, true);
            byte[] tab = entreprise.Logo.ToArray();
            MemoryStream buffer = new MemoryStream(tab);
            Image image = Image.FromStream(buffer);
            TextSelection[] selections = document.FindAllString("LOGO", true, true);
            int index = 0;
            TextRange range = null;

            foreach (TextSelection selection in selections)
            {
                DocPicture pic = new DocPicture(document);
                pic.LoadImage(image);

                range = selection.GetAsOneRange();
                index = range.OwnerParagraph.ChildObjects.IndexOf(range);
                pic.Width = 75f;
                pic.Height = 75f;
                range.OwnerParagraph.ChildObjects.Insert(index, pic);
                range.OwnerParagraph.ChildObjects.Remove(range);
            }

            using (var file = new SaveFileDialog())
            {
                file.Title = "Choisissez un emplacement pour le titre du congé ";
                file.Filter = "Document(*.docx)|*.docx";
                var result = file.ShowDialog();
                string nom_fichier = file.FileName;
                if(nom_fichier!=null && nom_fichier!="")
                {
                    document.SaveToFile(nom_fichier, FileFormat.Docx);
                    System.Diagnostics.Process.Start(nom_fichier);
                }
               
            }
        }


    }
}
