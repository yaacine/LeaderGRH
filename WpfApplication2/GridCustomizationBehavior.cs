using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using Syncfusion.Windows.Controls.Gantt;
using Syncfusion.Windows.Controls.Grid;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApplication2
{

    /* Cette classe sert a definir les differents attributs de l'afficheur des congées
     * plusieurs elements on été utilisées a partir de la documentation de syncfusion
    */


    class GridCustomizationBehavior : Behavior<GanttControl>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
        }

        /// <summary>
        /// Handles the Loaded event of the AssociatedObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            /// Removing Resource Column from the Grid(Table)
            this.AssociatedObject.GanttGrid.ShowAddNewColumn = false; // Interdir l'ajout d'une nouvelle colonne.
            
            GridTreeColumn ColumnStart = this.AssociatedObject.GanttGrid.Columns.FirstOrDefault(k => k.HeaderText.Equals("Start")); // Recherche de la colonne start
            GridTreeColumn ColumnFinish = this.AssociatedObject.GanttGrid.Columns.FirstOrDefault(k => k.HeaderText.Equals("Finish")); // Recherche de la colonne finish
            GridTreeColumn ColumnDuration = this.AssociatedObject.GanttGrid.Columns.FirstOrDefault(k => k.HeaderText.Equals("Duration"));//Recherche de la colonne duration
            ColumnStart.HeaderText = "Debut du congé";  // Changement des titres (Headers) des colonnes.
            ColumnFinish.HeaderText = "Fin du congé";
            ColumnDuration.HeaderText = "Jours Restants";
            this.AssociatedObject.GanttGrid.Columns[0].HeaderText = "Personne Concerné";

            this.AssociatedObject.GanttGrid.Columns.Remove(ColumnStart);  
            this.AssociatedObject.GanttGrid.Columns.Remove(ColumnFinish);
            this.AssociatedObject.GanttGrid.Columns.Remove(ColumnDuration);

            this.AssociatedObject.GanttGrid.Columns.Insert(2, ColumnStart);
            this.AssociatedObject.GanttGrid.Columns.Insert(3, ColumnFinish);
            this.AssociatedObject.GanttGrid.Columns.Insert(4, ColumnDuration);
            // Les 4 derniers instructions servent a déplacer les colonnes debut et fin resepectivement au positions 2 et 3.

            while (true)
            {
                // Suppression des colonnes restantes (les colonnes non nécessaires)
                try
                {
                    this.AssociatedObject.GanttGrid.Columns.RemoveAt(5);
                }
                catch (Exception ex)
                {
                    break;
                }
            }

            ///////////////////////
            GridTreeColumn column = new GridTreeColumn
            {
                MappingName = "Type",
                HeaderText = "Type de Congé",
                Width = 250,
                StyleInfo = new GridStyleInfo
                {
                    CellType = "Static",
                    CellItemTemplateKey = "TypeCell"
                  
                }
                
            };
            column.StyleInfo.TextMargins = new Syncfusion.Windows.Controls.Cells.CellMarginsInfo(35, 5, 0, 0);

            //////////////////////
            this.AssociatedObject.GanttGrid.Columns.Insert(5, column);
            this.AssociatedObject.GanttGrid.DefaultColumnWidth = 110;
            this.AssociatedObject.GanttGrid.MaxWidth = 1000;
            this.AssociatedObject.GanttGrid.HorizontalAlignment = HorizontalAlignment.Center;
            this.AssociatedObject.ShowDateWithTime = false;
            this.AssociatedObject.ChartWidth = new GridLength(480);
            this.AssociatedObject.GanttGrid.ClipToBounds = true;
            this.AssociatedObject.ScrollGanttChartTo(DateTime.Today);
            this.AssociatedObject.GanttGrid.SupportNodeImages = false;
            
            
        }

        /// <summary>
        /// Handles the RequestNodeImage event of the GanttGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="Syncfusion.Windows.Controls.Grid.GridTreeRequestNodeImageEventArgs"/> instance containing the event data.</param>
        void GanttGrid_RequestNodeImage(object sender, Syncfusion.Windows.Controls.Grid.GridTreeRequestNodeImageEventArgs args)
        {
           
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
            this.AssociatedObject.GanttGrid.RequestNodeImage -= new Syncfusion.Windows.Controls.Grid.GridTreeRequestNodeImageHandler(GanttGrid_RequestNodeImage);
        }
    }
}
