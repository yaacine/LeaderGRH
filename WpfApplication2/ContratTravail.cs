using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
using System.IO;
using WpfApplication2;
namespace WindowWPf
{
    public static class ContratTravail
    {
        
        
        public static void genererContrat(Employe e)
        {
            var entreprise = Variables.db.Parametres.First();
            Document document = new Document();
            document.LoadFromFile($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Resources\REF-CONTRAT DE TRAVAIL.doc");

            if (e.Nom != null) document.Replace("NOM",e.Nom, true, true);
            if (e.Prenom != null) document.Replace("PRENOM", e.Prenom, true, true);
            if (entreprise.NomGerant != null) document.Replace("NOM_G", entreprise.NomGerant, true, true);
            if (entreprise.PrenomGerant != null) document.Replace("PRENOM_G", entreprise.PrenomGerant, true, true);

            if (e.DateDeNaissance != null) document.Replace("DATE_NAISSANCE", e.DateDeNaissance.Value.ToShortDateString(), true, true);
            if (entreprise.Wilaya != null) document.Replace("WILAYA1",entreprise.Wilaya, true, true);
            if (e.Wilaya != null) document.Replace("WILAYA",e.Wilaya, true, true);
            if (e.DateEmbauche != null) document.Replace("DATE_EMBAUCHE", e.DateEmbauche.Value.ToShortDateString(), true, true);
            if (e.Poste != null) document.Replace("POSTE", e.Poste, true, true);
            if (e.Salaires != null) document.Replace("SALAIRE",e.Salaires.Last().Salaire.ToString() , true, true);
            if ((e.Salaires != null)) document.Replace("SALAIRE_annee",(e.Salaires.Last().Salaire * 12).ToString() , true, true);
            if (entreprise.Raison_Sociale != null) document.Replace("RAISON_SOCIALE", entreprise.Raison_Sociale, true, true);
            if (entreprise.Speciaite != null) document.Replace("SPECIALITE", entreprise.Speciaite, true, true);
            if (entreprise.IdFiscale != null) document.Replace("MATRICULE_FISCAL",entreprise.IdFiscale, true, true);
             document.Replace("MATRICULE", $"{e.Matricule:D4}", true, true);
            if (entreprise.Adresse != null) document.Replace("ADRESSE", entreprise.Adresse, true, true);

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
                file.Title = "Choissisez un emplacement ";
                file.Filter = "Document Excel(*.docx)|*.docx";
                var result = file.ShowDialog();
                string nom_fichier = file.FileName;

                if (nom_fichier != null && nom_fichier != "")
                {
                    document.SaveToFile(nom_fichier, FileFormat.Docx);
                    if (e.ContratTravail == null)
                    {
                        byte[] array = System.IO.File.ReadAllBytes(nom_fichier);
                        e.ContratTravail = array;
                        try
                        {
                            Variables.db.SubmitChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Impossible de sauvegarder le contrat dans la base de données");
                        }
                    }
                    System.Diagnostics.Process.Start(nom_fichier);
                }
                
            }
        }
    }
}
