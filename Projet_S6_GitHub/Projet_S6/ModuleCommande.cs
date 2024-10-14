using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class ModuleCommande
    {
        public Commande AjoutCommandeManuel()
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le nom de la marchandise commandée :\n");
            string marchandise = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le volume commandé :\n");
            int volume = Convert.ToInt32(Console.ReadLine());

            Adresse adresseA = CreationAdresse("de départ");
            Adresse adresseB = CreationAdresse("de l'arrivée");

            string ville;
            List<string> planDeRoute = new List<string>();
            int num = 1;


            var graphe = new Graphe();
            graphe.LireFichier("Data\\distances");

            planDeRoute = graphe.PlusCourtChemin(adresseA.Ville, adresseB.Ville);
            if (planDeRoute != null)
            {
                Console.WriteLine("Le plus court chemin est : " + string.Join(" -> ", planDeRoute));
            }
            else
            {
                Console.WriteLine("Aucun chemin trouvé.");
            }

            aff.AppuyezPourContinuer();


            int idL = bdd.RecoverLivraisons().Count;

            Livraison livraison = new Livraison(idL, marchandise, volume, adresseA, adresseB, planDeRoute);

            int idC = bdd.RecoverOrders().Count;

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le prix de la commande :\n");
            int prix = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            string[] options = { "Voiture", "Camionnette", "Camion citerne", "Cambion benne", "Camion frigorifique" };
            aff.Menu("Selectionnez le véhicule à utiliser", options);
            
            int choix = 0;
            do
            {
                choix = Convert.ToInt32(Console.ReadLine());
            } while (choix < 1 || choix > options.Length);

            string vehicule = options[choix - 1];

            List<Salarie> salaries = bdd.RecoverSalaries();
            List<string> chauffeurs = new List<string>();
            List<string> nssChauffeurs = new List<string>();
            foreach(Salarie s in salaries)
            {
                if (s.Poste == "Chauffeur" || s.Poste == "chauffeur")
                {
                    chauffeurs.Add(s.Nom + " " + s.Prenom + " | NSS : " + s.NSS);
                    nssChauffeurs.Add(s.NSS);
                }
            }

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le NSS du chaffeur parmi :\n");
            foreach(string s in chauffeurs)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n");

            string nssSelectionne;
            bool nssValide = false;
            do
            {
                nssSelectionne = Console.ReadLine();  
                foreach(string s in nssChauffeurs)
                {
                    if(nssSelectionne == s)
                    {
                        nssValide = true;
                    }
                }
            } while (nssValide == false);

            Salarie chauffeur = null;
            foreach(Salarie s in salaries)
            {
                if(s.NSS == nssSelectionne)
                {
                    chauffeur = s;
                }
            }

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez l'année de commande :\n");
            int annee = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le mois de la commande (nombre):\n");
            int mois = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le jour de la commande :\n");
            int jour = Convert.ToInt32(Console.ReadLine());
            DateTime date = new DateTime(annee, mois, jour);

            Commande maCommande = new Commande(idC, livraison, prix, vehicule, chauffeur, date);

            return maCommande;
        }


        public Adresse CreationAdresse(string s)
        {
            Affichage aff = new Affichage();
            GestionBDD bdd = new GestionBDD();
            string pays = null;
            do
            {
                Console.Clear();
                aff.TitreMin("Création manuelle de commande");
                Console.WriteLine("\nEntrez le pays " + s + " :\n");
                pays = Console.ReadLine();
                if(pays != "France")
                {
                    Console.WriteLine("Pays non desservit. Entrez en un nouveau !");
                    aff.AppuyezPourContinuer();
                }
            } while (pays != "France");
            

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le code postal " + s + " :\n");
            int codePostal = Convert.ToInt32(Console.ReadLine());



            List<string> villes = new List<string>
            {
            "Paris","Lille","Rouen","Metz","Nancy","Strasbourg","Rennes","Brest","Nantes",
            "Tours","Orléans","Dijon","Clermont-Ferrand","Bordeaux","Lyon","Saint-Etienne",
            "Grenoble","Marseille","Nice","Toulon","Montpellier","Toulouse"
            };

            string ville = DemanderVille(s, villes);


            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez la rue " + s + " :\n");
            string rue = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le numéro " + s + " :\n");
            int numero = Convert.ToInt32(Console.ReadLine());


            Adresse adresse = new Adresse(numero, rue, ville, codePostal, pays);



            // Sauvegarde adresse

            List<Adresse> adresses = bdd.RecoverAdresses();
            List<List<string>> adressesFile = new List<List<string>>();

            adresses.Add(adresse);

            foreach (Adresse a in adresses)
            {
                List<string> adresseStr = new List<string>() { Convert.ToString(a.Ida), Convert.ToString(a.Numero), a.Rue, a.Ville, Convert.ToString(a.CodePostal), a.Pays };
                adressesFile.Add(adresseStr);
            }

            bdd.SaveCSV("adressesData", adressesFile,',');

            return adresse;
        }



        static string DemanderVille(string s, List<string> villes)
        {
            string ville = null;
            Affichage aff = new Affichage();
            while (true)
            {
                Console.Clear();
                aff.TitreMin("Création manuelle de commande");
                Console.WriteLine("\nEntrez la ville " + s + " :\n");
                ville = Console.ReadLine();

                if (villes.Contains(ville))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("La ville entrée n'est pas valide. Veuillez réessayer.");
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                }
            }

            return ville;
        }
    }
}
