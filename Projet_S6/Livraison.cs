using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class Livraison
    {
        private int idL;
        private string marchandise;
        private int volume;
        private Adresse adresseA;
        private Adresse adresseB;
        private List<string> planDeRoute;

        public Livraison(int idL, string marchandise, int volume, Adresse adresseA, Adresse adresseB, List<string> planDeRoute)
        {
            this.idL = idL;
            this.marchandise = marchandise;
            this.volume = volume;
            this.adresseA = adresseA;
            this.adresseB = adresseB;
            this.planDeRoute = planDeRoute;
        }

        #region GETSET

        public int IdL
        {
            get { return idL; }
            set { idL = value; }
        }

        public string Marchandise
        {
            get { return marchandise; }
            set { marchandise = value; }
        }

        public int Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public Adresse AdresseA
        {
            get { return adresseA; }
            set { adresseA = value; }
        }

        public Adresse AdresseB
        {
            get { return adresseB; }
            set { adresseB = value; }
        }

        public List<string> PlanDeRoute
        {
            get { return planDeRoute; }
            set { planDeRoute = value; }
        }

        #endregion

        public override string ToString()
        {
            string retour = "\n--- LIVRAISON --- :\nIdL : " + idL + "\nMarchandise : " + marchandise + "\nVolume : " + Convert.ToString(volume) +
                "\nAdresse de départ : " + adresseA.ToString() + "\nAdresse d'arrivée : " + adresseB.ToString() +"\nPlan de route : ";

            foreach(string s in planDeRoute)
            {
                retour += s + " ";
            }
            retour += "\n-----------------";

            return retour;
        }
    }
}
