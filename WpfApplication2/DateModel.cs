using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using Syncfusion.Windows.Controls.Gantt;
using System.ComponentModel;
using System.Collections.Specialized;

namespace WpfApplication2
{
    public class Task : NotificationObject
    {
        public Task()
        {
            ChildTask = new ObservableCollection<Task>();
            Predecessor = new ObservableCollection<Predecessor>();
            Resource = new ObservableCollection<Resource>();
        }

        private int id;
        private string nom;
        private DateTime debut;
        private DateTime fin;
        private TimeSpan duration;
        private ObservableCollection<Resource> resource;
        private ObservableCollection<Task> childTask;
        private ObservableCollection<Predecessor> predecessor;
        private string type;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is complted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is complted; otherwise, <c>false</c>.
        /// </value>
       

        /// <summary>
        /// Gets or sets the risk percentage.
        /// </summary>
        /// <value>The risk percentage.</value>
       

        /// <summary>
        /// Gets or sets the complete.
        /// </summary>
        /// <value>The complete.</value>
       

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>The resource.</value>
        public ObservableCollection<Resource> Resource
        {
            get
            {
                return resource;
            }
            set
            {
                resource = value;
                RaisePropertyChanged("Resource");
            }
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public TimeSpan Duration
        {
            get
            {
                if (childTask != null && childTask.Count >= 1)
                {
                    var sum = new TimeSpan(0, 0, 0, 0);
                    sum = childTask.Aggregate(sum, (current, task) => current + task.Duration);
                    return sum;
                }

                /// The Difference Between the EndDate and StartDate is Calculated exactly.
                duration = fin.Subtract(debut);
                return duration;
            }

            set
            {
                if (childTask != null && childTask.Count >= 1)
                {
                    var sum = new TimeSpan(0, 0, 0, 0);
                    sum = childTask.Aggregate(sum, (current, task) => current + task.Duration);
                    duration = sum;
                    return;
                }

                duration = value;

                /// End date is beeing calcuated here to make the change in endate based on duration. Duration is interlinked with start and end date, so will affect both based on the change.
                Fin = debut.AddDays(Double.Parse(duration.TotalDays.ToString()));

            }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime Fin
        {
            get
            {
                return fin;
            }
            set
            {
                if (childTask != null && childTask.Count >= 1)
                {
                    /// If this task is a parent task, then it should have the maximum end time. Hence comparing the date with maximum date of its children.

                    if (value >= childTask.Max(s => s.Fin) && fin != value)
                        fin = value;
                }
                else
                    fin = value;
                RaisePropertyChanged("Fin");
                /// Duration changed is invoked to notify the chagne in duration based on the new end date.
                RaisePropertyChanged("Duration");
            }
        }

        /// <summary>
        /// Gets or sets the st date.
        /// </summary>
        /// <value>The st date.</value>
        public DateTime Debut
        {
            get
            {
                return debut;
            }
            set
            {
                /// If this task is a parent task, then it should have the minimum start time. Hence comparing the date with minimum date of its children.

                if (childTask != null && childTask.Count >= 1)
                {
                    if (value <= childTask.Min(s => s.Debut) && debut != value)
                        debut = value;
                }
                else
                    debut = value;
                RaisePropertyChanged("Debut");

                /// Duration chagned is invoked to notify the chagne in duration based on the new start date.
                RaisePropertyChanged("Duration");
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
                RaisePropertyChanged("Nom");
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Type {
            get
            {
                return type;
            }
            set
            {
                type = value;
                RaisePropertyChanged("Type");
            }
        }
        /// <summary>
        /// Gets or sets the predecessor.
        /// </summary>
        /// <value>The predecessor.</value>
        public ObservableCollection<Predecessor> Predecessor
        {
            get
            {
                return predecessor;
            }
            set
            {
                predecessor = value;
                RaisePropertyChanged("Predecessor");
            }
        }

        #region ChildTask Collection

        /// <summary>
        /// Gets or sets the child task.
        /// </summary>
        /// <value>The child task.</value>
        public ObservableCollection<Task> ChildTask
        {
            get
            {
                if (childTask == null)
                {
                    childTask = new ObservableCollection<Task>();
                    /// Collection changed of child tasks are hooked to listen and refresh the parent node based on the changes made in Child.
                    childTask.CollectionChanged += ChildNodesCollectionChanged;
                }
                return childTask;
            }
            set
            {
                childTask = value;
                ///Collection changed of child tasks are hooked to listen and refresh the parent node based on the changes made in Child.

                childTask.CollectionChanged += ChildNodesCollectionChanged;

                if (value.Count > 0)
                {
                    childTask.ToList().ForEach(n =>
                    {
                        /// To listen the changes occuring in child task.
                        n.PropertyChanged += ChildNodePropertyChanged;

                    });
                    UpdateData();
                }
                RaisePropertyChanged("ChildTask");
            }
        }

        /// <summary>
        /// The following does the calculations to update the Parent Task, when child collection property changes.
        /// </summary>
        /// <param name="sender">The source</param>
        /// <param name="e">Property changed event args</param>
        void ChildNodePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != null)
                if (e.PropertyName == "StDate" || e.PropertyName == "EndDate" || e.PropertyName == "IsComplted" || e.PropertyName == "Complete")
                {
                    UpdateData();
                }
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        private void UpdateData()
        {
            if (childTask != null && childTask.Count >= 1)
            {
                /// Updating the start and end date based on the chagne occur in the date of child task
                Debut = childTask.Select(c => c.Debut).Min();
                Fin = childTask.Select(c => c.Fin).Max();
                var sum = 0d;
                
                
            }

        }

        /// <summary>
        /// Childs the nodes collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        public void ChildNodesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Task node in e.NewItems)
                {
                    node.PropertyChanged += ChildNodePropertyChanged;
                }
            }
            else
            {
                foreach (Task node in e.OldItems)
                    node.PropertyChanged -= ChildNodePropertyChanged;
            }
            UpdateData();
        }
        #endregion
    }
}
