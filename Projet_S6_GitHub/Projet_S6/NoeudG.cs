using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class NoeudG
    {
        public string Nom { get; set; }
        public Dictionary<NoeudG, int> Voisins { get; private set; }

        public NoeudG(string nom)
        {
            Nom = nom;
            Voisins = new Dictionary<NoeudG, int>();
        }

        public void AjouterVoisin(NoeudG voisin, int distance)
        {
            if (!Voisins.ContainsKey(voisin))
            {
                Voisins[voisin] = distance;
            }
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
