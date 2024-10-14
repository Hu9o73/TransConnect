using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class Commande
    {
        private int idC;
        private Livraison livraison;
        private int prix;
        private string vehicule;
        private Salarie chauffeur;
        private DateTime date;

        public Commande(int idC, Livraison livraison, int prix, string vehicule, Salarie chauffeur, DateTime date)
        {
            this.idC = idC;
            this.livraison = livraison;
            this.prix = prix;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.date = date;
        }

        #region GETSET

        public int IdC
        {
            get { return idC; }
            set { idC = value; }
        }

        public Livraison Livraison
        {
            get { return livraison; }
            set { livraison = value; }
        }

        public int Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public string Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }

        public Salarie Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        #endregion

        public override string ToString()
        {
            return "IdC : " + Convert.ToString(idC) + livraison.ToString() + "\nPrix : " + prix + "\nVéhicule : " + vehicule 
                + "\nChauffeur : " + chauffeur.Nom + " " + chauffeur.Prenom + "\nDate : " + date.Day + "/" + date.Month + "/" + date.Year ;
        }
    }
}
