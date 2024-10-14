using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projet_S6
{
    internal class Noeud
    {
        // Variables
        private string valeur;
        private List<Noeud> fils;
        private List<int> poids;

        // Constructeur
        public Noeud(string v)
        {
            valeur = v;
            fils = new List<Noeud>();
            poids = new List<int>();
        }

        // Accesseurs
        public string Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }

        public List<Noeud> Fils
        {
            get { return fils; }
            set { fils = value; }
        }

        public List<int> Poids
        {
            get { return poids; }
            set { poids = value; }
        }
        public void AjouterEnfant(string data)
        {
            //Noeud N = new Noeud(data);
            fils.Add(new Noeud(data));
        }

        
        // Fonctions et méthodes
        public bool ASuccesseur(string v)
        {
            foreach (Noeud n in fils)
            {
                if (n.Valeur == v)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
