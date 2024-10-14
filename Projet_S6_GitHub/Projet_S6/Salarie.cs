using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class Salarie : Personne
    {
        private DateTime date_entree;
        private string poste;
        private int salaire;

        public Salarie(string nSS, string nom, string prenom, DateTime date_de_naissance, Adresse adresse, string mail, string telephone, DateTime date_entree, string poste, int salaire) : base(nSS, nom, prenom, date_de_naissance, adresse, mail, telephone)
        {
            this.date_entree = date_entree;
            this.poste = poste;
            this.salaire = salaire;
        }
        


        #region GETSET

        public DateTime Date_entree
        {
            get { return date_entree; }
            set { date_entree = value; }
        }

        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }

        public int Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }

        #endregion

        public override string ToString()
        {
            return base.ToString() + "\nDate d'entrée : " + date_entree.Day + "/" + date_entree.Month + "/" + date_entree.Year +
                "\nPoste : " + poste + "\nSalaire : " + salaire;
        }
    }
}
