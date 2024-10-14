using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class Graphe
    {
        public Dictionary<string, NoeudG> Noeuds { get; private set; }

        public Graphe()
        {
            Noeuds = new Dictionary<string, NoeudG>();
        }

        public void AjouterArete(string ville1, string ville2, int distance)
        {
            if (!Noeuds.ContainsKey(ville1))
            {
                Noeuds[ville1] = new NoeudG(ville1);
            }
            if (!Noeuds.ContainsKey(ville2))
            {
                Noeuds[ville2] = new NoeudG(ville2);
            }

            Noeuds[ville1].AjouterVoisin(Noeuds[ville2], distance);
            Noeuds[ville2].AjouterVoisin(Noeuds[ville1], distance);
        }

        public void LireFichier(string chemin)
        {
            foreach (var ligne in File.ReadAllLines(chemin))
            {
                var parties = ligne.Split(',');
                var ville1 = parties[0];
                var ville2 = parties[1];
                var distance = int.Parse(parties[2]);
                AjouterArete(ville1, ville2, distance);
            }
        }

        public void AfficherGraphe()
        {
            foreach (var noeud in Noeuds.Values)
            {
                Console.Write(noeud.Nom + " -> ");
                foreach (var voisin in noeud.Voisins)
                {
                    Console.Write($"{voisin.Key.Nom} ({voisin.Value}) ");
                }
                Console.WriteLine();
            }
        }

        public List<string> PlusCourtChemin(string depart, string arrivee)
        {
            var distances = new Dictionary<NoeudG, int>();
            var precedents = new Dictionary<NoeudG, NoeudG>();
            var nonVisites = new List<NoeudG>();

            foreach (var noeud in Noeuds.Values)
            {
                distances[noeud] = int.MaxValue;
                nonVisites.Add(noeud);
            }
            distances[Noeuds[depart]] = 0;

            while (nonVisites.Count > 0)
            {
                nonVisites.Sort((x, y) => distances[x] - distances[y]);
                var noeudCourant = nonVisites[0];
                nonVisites.Remove(noeudCourant);

                if (noeudCourant.Nom == arrivee)
                {
                    var chemin = new List<string>();
                    while (precedents.ContainsKey(noeudCourant))
                    {
                        chemin.Add(noeudCourant.Nom);
                        noeudCourant = precedents[noeudCourant];
                    }
                    chemin.Add(depart);
                    chemin.Reverse();
                    return chemin;
                }

                foreach (var voisin in noeudCourant.Voisins)
                {
                    var tentativeDistance = distances[noeudCourant] + voisin.Value;
                    if (tentativeDistance < distances[voisin.Key])
                    {
                        distances[voisin.Key] = tentativeDistance;
                        precedents[voisin.Key] = noeudCourant;
                    }
                }
            }

            return null; // Pas de chemin trouvé
        }
    }
}
