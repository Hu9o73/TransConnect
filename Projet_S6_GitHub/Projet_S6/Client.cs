using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class Client : Personne
    {
        private List<Commande> commandes;

        public Client(string nSS, string nom, string prenom, DateTime date_de_naissance, Adresse adresse, string mail, string telephone, List<Commande> commandes):base(nSS, nom, prenom, date_de_naissance, adresse, mail, telephone)
        {
            this.commandes = commandes;
        }

        #region GETSET

        public List<Commande> Commandes
        {
            get { return commandes; }
            set { commandes = value; }
        }

        #endregion

        
        public override string ToString()
        {
            if(commandes != null && commandes.Count > 0)
            {
                string[] c = new string[commandes.Count];
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = Convert.ToString(commandes[i].IdC);
                }

                string retour = "";

                foreach(string idC in c)
                {
                    retour += idC + " , ";
                }
                return base.ToString() + "\nListe id commandes :\n" + retour;
            }
            else
            {
                return base.ToString() + "\nCe client n'a pas encore passé commande !";
            }
            
        }
        
    }
}
