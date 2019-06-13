using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfApplication2
{
    public class AnnuaireModel : NotificationObject
    {
        
        private string matricule;
        private string nom;
        private string prenom;
        private string numtel;
        private string email;
        private string projet;
        private string poste;
        
       

        [Display(Name = "Matricule", Order = 1)]
        public string Matricule
        {
            get
            {
                return matricule;
            }

            set
            {
                matricule = value;
                RaisePropertyChanged("Matricule");
            }
        }

        [Display(Name = "Nom", Order = 2)]
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

        [Display(Name = "Prénom", Order = 3)]
        public string Prenom
        {
            get
            {
                return prenom;
            }
            
            set
            {
                prenom = value;
                RaisePropertyChanged("Prenom");
            }
        }

        [Display(Name = "Numero Tel", Order = 4)]
        public string Numtel
        {
            get
            {
                return numtel;
            }

            set
            {
                numtel = value;
                RaisePropertyChanged("Numtel");
            }
        }

       

        [Display(Name = "E-Mail", Order = 5)]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        [Display(Name = "Poste", Order = 6)]
        public string Poste
        {
            get
            {
                return poste;
            }

            set
            {
                poste = value;
                RaisePropertyChanged("Poste");
            }
        }

        [Display(Name = "Projet", Order = 7)]
        public string Projet
        {
            get
            {
                return projet;
            }

            set
            {
                projet = value;
                RaisePropertyChanged("Projet");
            }
        }

    }
}
