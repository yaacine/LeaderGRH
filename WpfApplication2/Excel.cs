using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace WpfApplication2
{
    class Excel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
        }

        public string readExcelcell(int i, int j)
        {
            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2 + "";
            else
                return "0";
        }

        public void WriteToCell(int i, int j, string s)
        {
            ws.Cells[i, j].Value2 = s;
        }

        public void save()
        {
            wb.Save();
        }

        public void saveas(string path)
        {
            wb.SaveAs(path);
        }

        public void Close()
        {
            wb.Close();
        }

        public void insererLogo(string path)
        {
            ws.Shapes.AddPicture(path, Microsoft.Office.Core.MsoTriState.msoFalse,
                Microsoft.Office.Core.MsoTriState.msoCTrue, 20, 20, 64, 64);
        }
        
        public void creer_fichier_entretien(string path_logo,string RaisonSociale,string Specialite,string NomPrenom,string poste , string Responsable ,string dateEmbauche,string objectif1,string objectif2,string objectif3)
        {
            insererLogo(path_logo);
            WriteToCell(5, 2, RaisonSociale);
            WriteToCell(6, 2, Specialite);
            WriteToCell(11,5, NomPrenom);
            WriteToCell(16, 5, poste);
            WriteToCell(16, 2, Responsable);
            WriteToCell(12, 8, dateEmbauche);
            WriteToCell(76,3, objectif1);
            WriteToCell(77,3,objectif2);
            WriteToCell(78,3,objectif3);
            WriteToCell(16, 10, "");
        }

        public void exporter_annuaire()
        {
            WriteToCell(1, 1, "Matricule");
            WriteToCell(1, 2, "Nom");
            WriteToCell(1, 3, "Prénom");
            WriteToCell(1, 4, "Num Tel");
            WriteToCell(1, 5, "Adresse");
            WriteToCell(1, 6, "E-Mail");
        }
    }
}

