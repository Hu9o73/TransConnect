using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projet_S6
{
    internal class Arbrenaire
    {
        private Noeud racine;
        public Arbrenaire(string valeur){ this.racine = new Noeud(valeur); }
        public Arbrenaire(Noeud racine) { this.racine = racine; }
        public Arbrenaire() { racine = null; }

        public Noeud Racine
        {
            get { return racine; }
            set { racine = value; }
        }

        public void AjouterEnfant(Noeud father, string enfant)
        {
            father.AjouterEnfant(enfant);
        }
        private void AjouterEnfants(List<Salarie> enfants, Noeud father)
        {
            foreach (Salarie c in enfants)
            {
                father.AjouterEnfant(c.Nom + " (" + c.Poste + ")");
            }
        }


    }
}
