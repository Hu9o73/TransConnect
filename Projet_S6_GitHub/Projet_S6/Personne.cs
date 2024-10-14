using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    abstract class Personne
    {
        private string nSS;
        private string nom;
        private string prenom;
        private DateTime date_de_naissance;
        private Adresse adresse;
        private string mail;
        private string telephone;

        protected Personne(string nSS, string nom, string prenom, DateTime date_de_naissance, Adresse adresse, string mail, string telephone)
        {
            this.nSS = nSS;
            this.nom = nom;
            this.prenom = prenom;
            this.date_de_naissance = date_de_naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.telephone = telephone;
        }

        #region GETSET

        public string NSS
        {
            get { return nSS; }
            set { nSS = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public DateTime Date_de_naissance
        {
            get { return date_de_naissance; }
            set { date_de_naissance = value; }
        }

        public Adresse Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        #endregion

        public override string ToString()
        {
            return "nSS : " + nSS + "\nNom : " + nom + "\nPrenom : " + prenom +
                "\nDate de naissance : " + Convert.ToString( date_de_naissance.Day ) + "/" + 
                Convert.ToString(date_de_naissance.Month) + "/" + Convert.ToString(date_de_naissance.Year)+
                "\nAdresse : " + adresse.ToString() +"\nE-mail : " + mail + "\nTéléphone : " + telephone;
        }
    }
}
