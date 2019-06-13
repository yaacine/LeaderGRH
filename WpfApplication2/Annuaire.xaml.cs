using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace WpfApplication2
{
    /// <summary>
    /// Logique d'interaction pour Annuaire.xaml
    /// </summary>
    using static Variables;
    public partial class Annuaire : System.Windows.Controls.Page
    {
        private ObservableCollection<AnnuaireModel> MyList = new ObservableCollection<AnnuaireModel>();
        public Annuaire()
        {
            InitializeComponent();
        }

        private void SfGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var list = from var in db.Employe
                       select var;

            foreach (var selec in list)
            {
                MyList.Add(new AnnuaireModel
                {
                    Matricule = $"{selec.Matricule:D5}",
                    Nom = selec.Nom,
                    Prenom = selec.Prenom,
                    Numtel = selec.NumeroTel,
                    Poste = selec.Poste,
                    Projet = selec.Projet,
                    Email = selec.Email

                });
            }
            SfGrid.ItemsSource = MyList;
        }


             private void annuaire_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (System.Windows.Window win in App.Current.Windows)
            {
                if (win.Title.Equals("GRH"))
                {
                    annuaire.Width = (win as MainWindow).Main.Width;
                    annuaire.Height = (win as MainWindow).Main.Height;
                    SfGrid.Width = (win as MainWindow).Main.Width - 150;
                    SfGrid.Height = (win as MainWindow).Main.Height - 220;
                    titre.Width= (win as MainWindow).Main.Width - 140;


                }
            }
        }

        private void Exporter_Click(object sender, RoutedEventArgs e)
        {
                 Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                 if (xlApp == null)
                 {
                     System.Windows.MessageBox.Show("Microsoft Excel est introuvable sur cet ordinateur");
                     return;
                 }
                 Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                 Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = new Microsoft.Office.Interop.Excel.Worksheet();
                 object misValue = System.Reflection.Missing.Value;
                 xlWorkBook = xlApp.Workbooks.Add(misValue);
                 xlWorkSheet = xlWorkBook.Worksheets[1];
                 xlWorkSheet.Range["G1", "I1"].Merge();
                 xlWorkSheet.Range["G1", "I1"].Font.Bold = true;
                 xlWorkSheet.Range["G1", "I1"].Font.Size = 17;
                 xlWorkSheet.Range["E3", "A1000"].NumberFormat = "@";
                 xlWorkSheet.Range["H3", "D1000"].NumberFormat = "@";
                 xlWorkSheet.Cells[1, 7] = $"               Annuaire : {DateTime.Today.ToShortDateString()}";
                 xlWorkSheet.Cells[3, 5] = "Matricule";
                 xlWorkSheet.Cells[3, 6] = "Nom";
                 xlWorkSheet.Cells[3, 7] = "Prénom";
                 xlWorkSheet.Cells[3, 8] = "Numero Téléphone";
                 xlWorkSheet.Cells[3, 9] = "Adresse";
                 xlWorkSheet.Cells[3, 10] = "E-Mail";
                 ((Range)xlWorkSheet.Range["E3","J3"]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                 var List = from var in Variables.db.Employe
                            select var;
                 int j = 4;
                 int cpt = 0;
                 for (int i = 5; i <= 10; i++)
                 {
                     foreach (var employe in List)
                     {
                         switch (i)
                         {
                             case 5:
                                 xlWorkSheet.Cells[j, i] = $"{employe.Matricule:D5}";
                                 break;
                             case 6:
                                 xlWorkSheet.Cells[j, i] = employe.Nom;
                                 break;
                             case 7:
                                 xlWorkSheet.Cells[j, i] = employe.Prenom;
                                 break;
                             case 8:
                                 xlWorkSheet.Cells[j, i] = employe.NumeroTel;
                                 break;
                             case 9:
                                 xlWorkSheet.Cells[j, i] = employe.Adresse;
                                 break;
                             case 10:
                                 xlWorkSheet.Cells[j, i] = employe.Email;
                                 break;
                         }
                         j++;
                         if (i == 5)
                         {
                             cpt++;
                         }
                     }
                     j = 4;
                 }
                 ((Range)xlWorkSheet.Range["E4", $"E{cpt+3}"]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                 ((Range)xlWorkSheet.Range["F4", $"F{cpt + 3}"]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                 ((Range)xlWorkSheet.Range["G4", $"G{cpt + 3}"]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                 ((Range)xlWorkSheet.Range["H4", $"H{cpt + 3}"]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                 ((Range)xlWorkSheet.Range["I4", $"I{cpt + 3}"]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                 ((Range)xlWorkSheet.Range["J4", $"J{cpt + 3}"]).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                 xlWorkSheet.Columns.AutoFit();
                 xlWorkSheet.Columns[8].ColumnWidth = 20;
                 using (var file = new SaveFileDialog())
                 {
                     file.Title = "Choissisez un emplacement ";
                     file.Filter = "Excel Workbook(*.xlsx)|*.xlsx";
                     var result = file.ShowDialog();
                     xlWorkBook.SaveAs(file.FileName);
                     xlWorkBook.Close(true, misValue, misValue);
                     Marshal.ReleaseComObject(xlWorkSheet);
                     Marshal.ReleaseComObject(xlWorkBook);
                     Marshal.ReleaseComObject(xlApp);
                 }
            
                  
        } 
    }
}
