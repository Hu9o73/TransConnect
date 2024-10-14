using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    class Adresse
    {
        private int ida;
        private int numero;
        private string rue;
        private string ville;
        private int codePostal;
        private string pays;

        /// <summary>
        /// Constructor when no id assigned
        /// </summary>
        /// <param name="numero">Numéro de résidence.</param>
        /// <param name="rue">Nom de la voie de résidence.</param>
        /// <param name="ville">Nom de la ville de résidence.</param>
        /// <param name="codePostal">Code postal de résidence.</param>
        /// <param name="pays">Pays de résidence.</param>
        public Adresse(int numero, string rue, string ville, int codePostal, string pays)
        {
            GestionBDD bdd = new GestionBDD();
            List<List<string>> tempAdresses = bdd.LoadCSV("adressesData", ',');

            if (tempAdresses != null && tempAdresses.Count > 0)
            {
                ida = tempAdresses.Count;
            }
            else
            {
                ida = 0;
            }

            this.numero = numero;
            this.rue = rue;
            this.ville = ville;
            this.codePostal = codePostal;
            this.pays = pays;
        }

        /// <summary>
        /// Recover an andress from the Database based on its id
        /// </summary>
        /// <param name="idaToFind">Id à trouver dans la base.</param>
        public Adresse(int idaToFind)
        {
            GestionBDD bdd = new GestionBDD();
            List<List<string>> tempAdresses = bdd.LoadCSV("adressesData", ',');
            
            foreach(List<string> adress in tempAdresses)
            {
                if(Convert.ToString(idaToFind) == adress[0])
                {
                    ida = idaToFind;
                    numero = Convert.ToInt32(adress[1]);
                    rue = adress[2];
                    ville = adress[3];
                    codePostal = Convert.ToInt32(adress[4]);
                    pays = adress[5];
                }
            }
        }

        #region GETSET

        public int Ida
        {
            get { return ida; }
            set { ida = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public string Rue
        {
            get { return rue; }
            set { rue = value; }
        }

        public string Ville
        {
            get { return ville; }
            set { ville = value; }
        }

        public int CodePostal
        {
            get { return codePostal; }
            set { codePostal = value; }
        }

        public string Pays
        {
            get { return pays; }
            set { pays = value; }
        }
        #endregion

        public override string ToString()
        {
            return numero + " " + rue + " , " + ville + " , " + codePostal + " , " + pays;
        }

        public static bool operator ==(Adresse a, Adresse b)
        {
            if(a.Numero == b.Numero && a.Rue == b.Rue && a.Ville == b.Ville && a.codePostal == b.codePostal && a.Pays == b.Pays)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Adresse a, Adresse b)
        {
            if (a.Numero != b.Numero || a.Rue != b.Rue || a.Ville != b.Ville || a.codePostal != b.codePostal || a.Pays != b.Pays)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
