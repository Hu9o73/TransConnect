using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class Affichage
    {
        /// <summary>
        /// Permet l'affichage d'un titre majeur.
        /// </summary>
        /// <param name="titre">Nom de votre titre, à afficher</param>
        public void TitreMaj(string titre)
        {
            // Ligne 1
            Console.Write("==");
            foreach (char c in titre)
            {
                Console.Write("=");
            }
            Console.Write("==\n");


            // Ligne 2
            Console.Write("| ");
            foreach(char c in titre)
            {
                Console.Write(" ");
            }
            Console.Write(" |\n");


            // Ligne 3
            Console.Write("| ");
            foreach (char c in titre)
            {
                Console.Write(c);
            }
            Console.Write(" |\n");


            // Ligne 4
            Console.Write("| ");
            foreach (char c in titre)
            {
                Console.Write(" ");
            }
            Console.Write(" |\n");

            //Ligne 5
            // Ligne 1
            Console.Write("==");
            foreach (char c in titre)
            {
                Console.Write("=");
            }
            Console.Write("==\n");

        }

        /// <summary>
        /// Permet l'affichage d'un titre mineur.
        /// </summary>
        /// <param name="titre">Nom de votre titre, à afficher</param>
        public void TitreMin(string titre)
        {
            // Ligne 1
            Console.Write("  ");
            foreach (char c in titre)
            {
                Console.Write(c);
            }
            Console.Write("  \n");

            // Ligne 2
            Console.Write("==");
            foreach (char c in titre)
            {
                Console.Write("=");
            }
            Console.Write("==\n");
        }

        /// <summary>
        /// Permet l'affichage d'un menu.
        /// </summary>
        /// <param name="titre">Nom de votre menu</param>
        /// <param name="titres">Liste des options du menu</param>
        public void Menu(string titre, string[] titres)
        {
            TitreMin(titre);
            for (int i = 1; i <= titres.Length; i++)
            {
                Console.Write("[" + i + "] | " + titres[i-1] + "\n");
            }
            Console.Write("====");
            foreach(char c in titre)
            {
                Console.Write("=");
            }
            Console.Write("\n");
        }


        /// <summary>
        /// Séparer des éléments d'affichage
        /// </summary>
        /// <param name="n">Taille de l'espace</param>
        public void Separator(int n)
        {
            for(int i = 0; i < n; i++)
            {
                Console.Write("\n");
            }
        }


        /// <summary>
        /// Génère et gère l'affichage d'un "appuyez sur entrée pour continuer".
        /// </summary>
        public void AppuyezPourContinuer()
        {
            Console.Write("Appuyez sur entrée pour continuer...\n");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
