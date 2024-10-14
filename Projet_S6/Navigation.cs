using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projet_S6
{
    public delegate void AfficherPage();
    internal class Navigation
    {
        #region Pages de navigation

        /// <summary>
        /// Affichage de la page d'accueil de l'app
        /// </summary>
        public void PageAccueil()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] optionsAccueil = { "Clients", "Salariés", "Commandes", "Statistiques", "Import de base (.CSV)" , "Autre", "Crédits" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { PageClients, PageSalaries, PageCommandes, PageStats, RemplacerBaseCSV, PasImplemente, PageCredits, PageStats};


            // Affichage
            Console.Clear();
            aff.TitreMaj("TransConnect");
            aff.Separator(2);
            aff.Menu("Menu principal", optionsAccueil);
            SelectionMenu(pagesCorrespondantes, PageAccueil);
        }


        /// <summary>
        /// Affichage de la page des crédits de l'app
        /// </summary>
        public void PageCredits()
        {
            Affichage aff = new Affichage();

            Console.Clear();
            aff.TitreMin("Crédits");
            Console.WriteLine("\nBONNELL Hugo\nCOWAN Mathieu\n\nESILV A3 - Projet de langage C#\n\n--------\nAppuyez sur entrée pour revenir au menu principal...");
            Console.ReadLine();
            PageAccueil();
        }


        public void PageStats()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Afficher par chauffeur le nombre de livraisons effectuées", "Afficher les commandes selon une période de temps", "Afficher la moyenne des prix des commandes", "Afficher la moyenne des comptes clients", "Afficher la liste des commandes pour un client", "Retour au menu principal" };
            ModuleStats modulestats = new ModuleStats();
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { modulestats.LivraisonParChauffeurvlistes, modulestats.CommandePeriode, modulestats.AfficherMoyennePrix, modulestats.MoyennePrixCommandesParClient, modulestats.ListeCommandeClient, PageAccueil };



            // Affichage
            Console.Clear();
            aff.Menu("Statistiques", options);
            SelectionMenu(pagesCorrespondantes, PageStats);
            aff.AppuyezPourContinuer();
            PageStats();
        }


        #region Pages clients

        /// <summary>
        /// Affichage de la page de gestion des clients.
        /// </summary>
        public void PageClients()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Ajout, suppression, modification de client", "Lister les clients", "Retour à l'accueil" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { ModifClient, ListeClient, PageAccueil };

            // Affichage
            Console.Clear();
            aff.Menu("Gestion clients", options);
            SelectionMenu(pagesCorrespondantes, PageClients);
        }


        #region Modif Client

        /// <summary>
        /// Affichage de la page de modification des clients.
        /// </summary>
        public void ModifClient()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Ajout manuel de client","Suppression d'un client","Modification d'un client", "Retour à l'accueil" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { AjoutManuelClient, SuppressionClient, ModificationClient, PageAccueil };

            // Affichage
            Console.Clear();
            aff.Menu("Ajout, suppression, modification", options);
            SelectionMenu(pagesCorrespondantes, ModifClient);
        }

        #region AjoutSupprModif

        /// <summary>
        /// Ajouter manuellement un client
        /// </summary>
        public void AjoutManuelClient()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleClient moduleClient = new ModuleClient();
            GestionBDD bdd = new GestionBDD();

            // Affichage
            Console.Clear();
            aff.TitreMin("Ajout manuel de client");
            aff.Separator(2);
            aff.AppuyezPourContinuer();
            Client client = moduleClient.AjoutClientManuel();
            bdd.AddClient(client);
            Console.Clear();
            aff.TitreMin("Ajout manuel de client");
            aff.Separator(2);
            Console.WriteLine("Ajout réussi et sauvegardé dans la bdd !\nVous allez être redirigé vers le menu principal.");
            aff.Separator(2);
            aff.AppuyezPourContinuer();

            PageAccueil();

        }

        public void SuppressionClient()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleClient moduleClient = new ModuleClient();
            GestionBDD bdd = new GestionBDD();
            List<Client> clients = bdd.RecoverClients();


            // Affichage
            Console.Clear();
            aff.TitreMin("Suppression d'un client");
            aff.Separator(1);
            Console.WriteLine("Liste des clients :");
            
            if (clients.Count > 0 && clients != null)
            {
                // Trie la liste des clients par leur nom en utilisant LINQ
                List<Client> clientsTries = clients.OrderBy(client => client.Nom).ToList();

                foreach (Client c in clientsTries)
                {
                    Console.WriteLine(c.Nom + " " + c.Prenom + " " + " NSS : " + c.NSS);
                }
            }
            else
            {
                Console.WriteLine("Pas encore de clients !");
            }

            Console.WriteLine("Entrez le NSS du client à supprimer : \n");
            string nssASuppr = Console.ReadLine();
            bool suppression;
            if (nssASuppr != null && nssASuppr.Length > 0)
            {
                suppression = moduleClient.SupprimerClient(nssASuppr);
            }
            else
            {
                suppression = false;
            }
            
            Console.Clear();
            aff.TitreMin("Suppression d'un client");
            aff.Separator(2);
            if(suppression == true)
            {
                Console.WriteLine("Suppression réussie et bdd mise à jour !\nVous allez être redirigé vers le menu principal.");
            }
            else
            {
                Console.WriteLine("Pas de client trouvé pour le NSS : " + nssASuppr + "\nVous allez être redirigé vers le menu principal.");
            }
            aff.Separator(2);
            aff.AppuyezPourContinuer();

            PageAccueil();
        }

        public void ModificationClient()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleClient moduleClient = new ModuleClient();
            GestionBDD bdd = new GestionBDD();
            List<Client> clients = bdd.RecoverClients();


            // Affichage
            Console.Clear();
            aff.TitreMin("Modification d'un client");
            aff.Separator(1);
            Console.WriteLine("Liste des clients :");

            if (clients.Count > 0 && clients != null)
            {
                // Trie la liste des clients par leur nom en utilisant LINQ
                List<Client> clientsTries = clients.OrderBy(client => client.Nom).ToList();

                foreach (Client c in clientsTries)
                {
                    Console.WriteLine(c.Nom + " " + c.Prenom + " " + " NSS : " + c.NSS);
                }
            }
            else
            {
                Console.WriteLine("Pas encore de clients !");
            }

            Console.WriteLine("Entrez le NSS du client à modifier : \n");
            string nssAModif = Console.ReadLine();
            bool modification;
            if(nssAModif != null && nssAModif.Length > 0)
            {
                modification = moduleClient.ModifierClient(nssAModif);
            }
            else
            {
                modification = false;
            }
            Console.Clear();
            aff.TitreMin("Modification d'un client");
            aff.Separator(2);
            if (modification == true)
            {
                Console.WriteLine("Modification réussie et bdd mise à jour !\nVous allez être redirigé vers le menu principal.");
            }
            else
            {
                Console.WriteLine("Pas de client trouvé pour le NSS : " + nssAModif + "\nVous allez être redirigé vers le menu principal.");
            }
            aff.Separator(2);
            aff.AppuyezPourContinuer();

            PageAccueil();
        }
        #endregion

        

        // endregion Modif Client
        #endregion


        #region Listes Clients
        public void ListeClient()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Noms" , "Villes", "Montant Total Acheté"};
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { ListeParNom, ListeParVille, ListeMeilleurClientvlistes };

            // Affichage
            Console.Clear();
            aff.Menu("Lister vos clients par :", options);
            SelectionMenu(pagesCorrespondantes, ListeClient);
        }
        public void ListeParNom()
        {
            Debug.WriteLine("Début ListeParNom()");
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleClient moduleClient = new ModuleClient();
            GestionBDD bdd = new GestionBDD();

            // Affichage
            Console.Clear();
            aff.TitreMin("Liste des clients par noms");
            List<Client> clients = new List<Client>();
            clients = bdd.RecoverClients();

            if(clients.Count > 0 && clients != null)
            {
                // Trie la liste des clients par leur nom en utilisant LINQ
                List<Client> clientsTries = new List<Client>();
                clientsTries = clients.OrderBy(client => client.Nom).ToList();

                foreach (Client client in clientsTries)
                {
                    Console.WriteLine(client.ToString());
                    aff.Separator(2);
                }
                Debug.WriteLine("Count clients : " + clients.Count);
            }
            else
            {
                Console.WriteLine("Pas encore de clients !");
            }
            
            
            aff.Separator(2);
            Console.WriteLine("Fin de la liste.\nVous allez être redirigé vers le menu principal.");
            aff.Separator(2);
            aff.AppuyezPourContinuer();
            Debug.WriteLine("Fin ListeParNom()");
            aff.Separator(1000);
            PageAccueil();
        }
        public void ListeParVille()
        {
            Debug.WriteLine("Début ListeParVille()");
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleClient moduleClient = new ModuleClient();
            GestionBDD bdd = new GestionBDD();

            // Affichage
            Console.Clear();
            aff.TitreMin("Liste des clients par ville d'adresse");
            List<Client> clients = new List<Client>();
            List<string> villes = new List<string>();
            clients = bdd.RecoverClients();

            if (clients.Count > 0 && clients != null)
            {

                List<Client> clientsTries = new List<Client>();
                clientsTries = clients.OrderBy(client => client.Adresse.Ville).ToList();
  
                foreach (Client client in clientsTries)
                {
                    Console.WriteLine(client.ToString());
                    aff.Separator(2);
                }
                Debug.WriteLine("Count clients : " + clients.Count);

            }
            else
            {
                Console.WriteLine("Pas encore de clients !");
            }


            aff.Separator(2);
            Console.WriteLine("Fin de la liste.\nVous allez être redirigé vers le menu principal.");
            aff.Separator(2);
            aff.AppuyezPourContinuer();
            aff.Separator(1000);
            PageAccueil();
        }
        
        public string Resultat(Salarie c)
        {
            string res;
            if (c == null)
            {
                res = " ";
            }
            else
            {
                res = c.Nom + " (" + c.Poste + ")";
            }
            return res;
        }

        public int MontantTotalClient(Client C)
        {
            int res = 0;
            GestionBDD bdd = new GestionBDD();
            List<int> MontantClient = new List<int>();
            foreach (Commande c in C.Commandes)
            {
                res += c.Prix;
            }
            return res;
        }

        public void ListeMeilleurClientvlistes()
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            Console.Clear();
            aff.TitreMin("Liste des meilleurs clients");
            List<Client> clients = new List<Client>();
            clients = bdd.RecoverClients();
            Dictionary<string, int> dico = new Dictionary<string, int>();
            foreach (Client c in clients){
                dico.Add(c.NSS, MontantTotalClient(c));
            }
            Dictionary<string, int> dicotrie = new Dictionary<string, int>();
            dicotrie = dico.OrderByDescending(montant => montant.Value).ToDictionary(montant => montant.Key, montant => montant.Value);
            int meilleurclient = 1;
            aff.Separator(2);
            foreach (string nss in dicotrie.Keys)
            {
                string prenom = "";
                string nom = "";
                foreach (Client c in clients) {
                    if (nss == c.NSS){prenom = c.Prenom; nom = c.Nom;}
                }
                Console.Write("Client " + nss + " " + prenom + " " + nom + " => " + dicotrie[nss] + " euros");
                if (meilleurclient == 1) { 
                    Console.WriteLine(" -- MEILLEUR CLIENT");
                    
                }
                aff.Separator(1);
                meilleurclient = 0;
            }

            aff.Separator(2);
            Console.WriteLine("Fin de la liste.\nVous allez être redirigé vers le menu principal.");
            aff.Separator(2);
            aff.AppuyezPourContinuer();
            aff.Separator(1000);
            PageAccueil();
        }
        



        // Endregion listes clients
        #endregion

        // Endregion pages clients
        #endregion


        #region Pages salariés

        /// <summary>
        /// Affichage de la page de gestion des clients.
        /// </summary>
        public void PageSalaries()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Ajout, suppression, modification de salarié", "Lister les salariés", "Organigramme", "Retour à l'accueil" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { ModifSalarie, ListeSalaries, AffichageOrganigramme, PageAccueil };

            // Affichage
            Console.Clear();
            aff.Menu("Gestion clients", options);
            SelectionMenu(pagesCorrespondantes, PageSalaries);
        }

        public void AffichageOrganigramme()
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            List<Salarie> salaries = bdd.RecoverSalaries();
            string poste = "DIRECTION GENERALE";
            Salarie DG, DC, DO, DRH, DF, DCP, CG;

            //Initialisation des différents postes non directifs
            List<Salarie> Com = new List<Salarie>();
            List<Salarie> Chauff = new List<Salarie>();
            List<Salarie> Formation = new List<Salarie>();
            List<Salarie> Contrat = new List<Salarie>();
            List<Salarie> Comptable = new List<Salarie>();
            List<Salarie> Gestionnaire = new List<Salarie>();

            //initialiser les Salariés de direction à null pour ne pas avoir de bugs
            DG = null;
            DC = null;
            DO = null;
            DRH = null;
            DF = null;
            DCP = null;
            CG = null;

            //Affectation des salariés aux differentes listes/variables de salariés
            foreach (Salarie s in salaries)
            {
                switch (s.Poste)
                {
                    case "DIRECTION GENERALE":
                        DG = s;
                        break;
                    case "DIRECTION COMMERCIALE":
                        DC = s;
                        break;
                    case "DIRECTION DES OPERATIONS":
                        DO = s;
                        break;
                    case "DIRECTION DES RH":
                        DRH = s;
                        break;
                    case "DIRECTION FINANCIERE":
                        DF = s;
                        break;
                    case "DIRECTION COMPTABLE":
                        DCP = s;
                        break;
                    case "CONTROLEUR DE GESTION":
                        CG = s;
                        break;
                    case "Commercial":
                        Com.Add(s);
                        break;
                    case "Chauffeur":
                        Chauff.Add(s);
                        break;
                    case "Formation":
                        Formation.Add(s);
                        break;
                    case "Contrat":
                        Contrat.Add(s);
                        break;
                    case "Gestionnaire":
                        Gestionnaire.Add(s);
                        break;
                    case "Comptable":
                        Comptable.Add(s);
                        break;
                }
            }
            //Cas où il n'y a pas de directeur général
            if (DG == null)
            {
                Console.WriteLine("Erreur, il s'avère qu'il n'y a pas de Directeur Général dans la base de données des salariés \nVeuillez effectuer une vérification svp");
                aff.Separator(2);
                aff.AppuyezPourContinuer();
                Console.Clear();
                PageAccueil();
                return;
            }


            Noeud root = new Noeud(Resultat(DG));
            Arbrenaire organigramme = new Arbrenaire(Resultat(DG)); // Initialisation de l'affichage du directeur général comme racine

            //Ajout des sous-directeurs commes fils de la racine
            organigramme.AjouterEnfant(root, Resultat(DC));
            organigramme.AjouterEnfant(root, Resultat(DO));
            organigramme.AjouterEnfant(root, Resultat(DRH));
            organigramme.AjouterEnfant(root, Resultat(DF));

            //Ajout des différents postes non-directifs aux divisions correspondantes
            foreach (Salarie s in Com)
            {
                organigramme.AjouterEnfant(root.Fils[0], Resultat(s));
            }
            foreach (Salarie s in Chauff)
            {
                organigramme.AjouterEnfant(root.Fils[1], Resultat(s));
            }
            foreach (Salarie s in Formation)
            {
                organigramme.AjouterEnfant(root.Fils[2], Resultat(s));
            }
            foreach (Salarie s in Contrat)
            {
                organigramme.AjouterEnfant(root.Fils[2], Resultat(s));
            }
            foreach (Salarie s in Comptable)
            {
                organigramme.AjouterEnfant(root.Fils[3], Resultat(s));
            }
            foreach (Salarie s in Gestionnaire)
            {
                organigramme.AjouterEnfant(root.Fils[3], Resultat(s));
            }

            Afficher(root);

            aff.Separator(3);
            Console.WriteLine("Choisir jusqu'à l'option " + root.Fils.Count());
            List<string> options = new List<string>();
            for (int i = 1; i <= root.Fils.Count(); i++)
            {
                options.Add(Convert.ToString(i));
            }
            string answer = Console.ReadLine();
            while (!options.Contains(answer))
            {
                Console.WriteLine("Erreur, saisir une option entre " + options[0] + " et " + options[root.Fils.Count()]);
                answer = Console.ReadLine();
            }
            Afficher(root.Fils[Convert.ToInt32(answer)-1]);

            // PARTIE AFFICHAGE 
            // AFFICHAGE DIRECTEUR GENERAL ET DIRECTEURS DE DIVISIONS
            aff.AppuyezPourContinuer();
            PageSalaries();

        }
        public void Afficher(Noeud root)
        {
            Affichage aff = new Affichage();
            Console.Clear();
            Console.WriteLine(root.Valeur + "\n");

            foreach (Noeud fils in root.Fils)
            {
                Console.WriteLine("\t" + fils.Valeur + "\n");
            }

        }

        #region Modif Salarié

        /// <summary>
        /// Affichage de la page de modification des clients.
        /// </summary>
        public void ModifSalarie()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Ajout manuel de salarié", "Suppression d'un salarié", "Modification d'un salarié", "Retour à l'accueil" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { AjoutManuelSalarie, SuppressionSalarie, ModificationSalarie, PageAccueil };

            // Affichage
            Console.Clear();
            aff.Menu("Ajout, suppression, modification", options);
            SelectionMenu(pagesCorrespondantes, ModifSalarie);
        }

        #region AjoutSupprModif

        /// <summary>
        /// Ajouter manuellement un client
        /// </summary>
        public void AjoutManuelSalarie()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleSalarie moduleSalarie = new ModuleSalarie();
            GestionBDD bdd = new GestionBDD();

            // Affichage
            Console.Clear();
            aff.TitreMin("Ajout manuel de salarié");
            aff.Separator(2);
            aff.AppuyezPourContinuer();
            Salarie salarie = moduleSalarie.AjoutSalarieManuel();
            bdd.AddSalarie(salarie);
            Console.Clear();
            aff.TitreMin("Ajout manuel de salarié");
            aff.Separator(2);
            Console.WriteLine("Ajout réussi et sauvegardé dans la bdd !\nVous allez être redirigé vers le menu principal.");
            aff.Separator(2);
            aff.AppuyezPourContinuer();

            PageAccueil();

        }

        public void SuppressionSalarie()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleSalarie moduleSalarie = new ModuleSalarie();
            GestionBDD bdd = new GestionBDD();
            List<Salarie> salaries = bdd.RecoverSalaries();


            // Affichage
            Console.Clear();
            aff.TitreMin("Suppression d'un salarie");
            aff.Separator(1);
            Console.WriteLine("Liste des salaries :");

            if (salaries.Count > 0 && salaries != null)
            {
                // Trie la liste des clients par leur nom en utilisant LINQ
                List<Salarie> salariesTries = salaries.OrderBy(salarie => salarie.Nom).ToList();

                foreach (Salarie s in salariesTries)
                {
                    Console.WriteLine(s.Nom + " " + s.Prenom + " " + " NSS : " + s.NSS);
                }
            }
            else
            {
                Console.WriteLine("Pas encore de salariés !");
            }

            Console.WriteLine("Entrez le NSS du salarié à supprimer : \n");
            string nssASuppr = Console.ReadLine();
            bool suppression;
            if (nssASuppr != null && nssASuppr.Length > 0)
            {
                suppression = moduleSalarie.SupprimerClient(nssASuppr);
            }
            else
            {
                suppression = false;
            }

            Console.Clear();
            aff.TitreMin("Suppression d'un salarié");
            aff.Separator(2);
            if (suppression == true)
            {
                Console.WriteLine("Suppression réussie et bdd mise à jour !\nVous allez être redirigé vers le menu principal.");
            }
            else
            {
                Console.WriteLine("Pas de salarié trouvé pour le NSS : " + nssASuppr + "\nVous allez être redirigé vers le menu principal.");
            }
            aff.Separator(2);
            aff.AppuyezPourContinuer();

            PageAccueil();
        }

        public void ModificationSalarie()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleSalarie moduleSalarie = new ModuleSalarie();
            GestionBDD bdd = new GestionBDD();
            List<Salarie> salaries = bdd.RecoverSalaries();


            // Affichage
            Console.Clear();
            aff.TitreMin("Modification d'un salarié");
            aff.Separator(1);
            Console.WriteLine("Liste des salariés :");

            if (salaries.Count > 0 && salaries != null)
            {
                // Trie la liste des clients par leur nom en utilisant LINQ
                List<Salarie> salariesTries = salaries.OrderBy(salarie => salarie.Nom).ToList();

                foreach (Salarie s in salariesTries)
                {
                    Console.WriteLine(s.Nom + " " + s.Prenom + " " + " NSS : " + s.NSS);
                }
            }
            else
            {
                Console.WriteLine("Pas encore de salariés !");
            }

            Console.WriteLine("Entrez le NSS du salarié à modifier : \n");
            string nssAModif = Console.ReadLine();
            bool modification;
            if (nssAModif != null && nssAModif.Length > 0)
            {
                modification = moduleSalarie.ModifierSalarie(nssAModif);
            }
            else
            {
                modification = false;
            }
            Console.Clear();
            aff.TitreMin("Modification d'un salarié");
            aff.Separator(2);
            if (modification == true)
            {
                Console.WriteLine("Modification réussie et bdd mise à jour !\nVous allez être redirigé vers le menu principal.");
            }
            else
            {
                Console.WriteLine("Pas de salarié trouvé pour le NSS : " + nssAModif + "\nVous allez être redirigé vers le menu principal.");
            }
            aff.Separator(2);
            aff.AppuyezPourContinuer();

            PageAccueil();
        }
        #endregion

        // endregion Modif Client
        #endregion

        #region Listes Salariés
        public void ListeSalaries()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Noms" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { ListeParNomS };

            // Affichage
            Console.Clear();
            aff.Menu("Lister vos salariés par :", options);
            SelectionMenu(pagesCorrespondantes, ListeSalaries);
        }

        public void ListeParNomS()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            ModuleSalarie moduleSalarie = new ModuleSalarie();
            GestionBDD bdd = new GestionBDD();

            // Affichage
            Console.Clear();
            aff.TitreMin("Liste des salariés par noms");
            List<Salarie> salaries = bdd.RecoverSalaries();

            if (salaries.Count > 0 && salaries != null)
            {
                // Trie la liste des clients par leur nom en utilisant LINQ
                List<Salarie> salariesTries = salaries.OrderBy(salarie => salarie.Nom).ToList();

                foreach (Salarie s in salariesTries)
                {
                    Console.WriteLine(s.ToString());
                    aff.Separator(2);
                }
            }
            else
            {
                Console.WriteLine("Pas encore de salariés !");
            }


            aff.Separator(2);
            Console.WriteLine("Fin de la liste.\nVous allez être redirigé vers le menu principal.");
            aff.Separator(2);
            aff.AppuyezPourContinuer();

            PageAccueil();
        }

        // Endregion listes clients
        #endregion


        // Endregion des pages salariés
        #endregion


        #region Pages Commandes

        /// <summary>
        /// Affichage de la page de gestion des commandes.
        /// </summary>
        public void PageCommandes()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Ajout de commande", "Lister les commandes", "Retour à l'accueil" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { AjoutCommande, PasImplemente, PageAccueil };

            // Affichage
            Console.Clear();
            aff.Menu("Gestion commandes", options);
            SelectionMenu(pagesCorrespondantes, PageClients);
        }

        public void AjoutCommande()
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            ModuleCommande moduleCommande = new ModuleCommande();

            Console.Clear();
            aff.TitreMin("Création manuelle de commande");
            Console.WriteLine("\nEntrez le nss du client ayant passé commande :\n");


            List<Client> clients = bdd.RecoverClients();

            if (clients.Count > 0 && clients != null)
            {
                Console.WriteLine("\nChargement de la liste des clients...");
                // Trie la liste des clients par leur nom en utilisant LINQ
                List<Client> clientsTries = clients.OrderBy(client => client.Nom).ToList();

                Console.Clear();
                aff.TitreMin("Création manuelle de commande");
                Console.WriteLine("\nEntrez le nss du client ayant passé commande :\n");


                foreach (Client client in clientsTries)
                {
                    Console.WriteLine(client.Nom + client.Prenom + " | NSS : " + client.NSS);
                }
                aff.Separator(2);

                string nss;
                bool nssValide = false;
                Client clientCommande;
                do
                {
                    nss = Console.ReadLine();

                    foreach(Client client in clients)
                    {
                        if (nss == client.NSS)
                        {
                            nssValide = true;
                            clientCommande = client;
                            Commande maCommande = moduleCommande.AjoutCommandeManuel();
                            clientCommande.Commandes.Add(maCommande);
                            bdd.ReplaceClient(client, clientCommande, clients);
                            Console.Clear();
                            Console.WriteLine(maCommande.ToString());
                            bdd.AddOrder(maCommande);
                            break;
                        }
                    }

                } while (nssValide == false);

                aff.AppuyezPourContinuer();
                PageAccueil();
            }
            else
            {
                Console.WriteLine("Pas encore de clients ! Vous allez être redirigé vers le menu principal !");
                aff.AppuyezPourContinuer();
                PageAccueil();
            }



            
        }



        //endregion pages commandes
        #endregion


        #region Import CSV Base

        #region Menu supplémentaire pour ajout base, pas implémenté

        public void ImportCSV()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            string[] options = { "Ajouter à la base", "Remplacer la base" };
            List<AfficherPage> pagesCorrespondantes = new List<AfficherPage>() { AjouterBaseCSV, RemplacerBaseCSV };

            // Affichage
            Console.Clear();
            aff.Menu("Gestion clients", options);
            SelectionMenu(pagesCorrespondantes, PageClients);
        }

        public void AjouterBaseCSV()
        {


        }

        #endregion

        public void RemplacerBaseCSV()
        {
            // Variables de la page
            Affichage aff = new Affichage();
            GestionBDD bdd = new GestionBDD();

            // Affichage
            Console.Clear();
            aff.TitreMin("Remplacer la base (CSV)");
            aff.Separator(2);
            Console.WriteLine("Entrez le nom du fichier CSV que vous souhaitez utiliser comme base de clients:\n");
            string nomFichier = Console.ReadLine();

            List<List<string>> nouvelleBaseClient = bdd.LoadCSV(nomFichier, ',');
            bdd.SaveCSV("clientsData", nouvelleBaseClient, ',');


            Console.WriteLine("Entrez le nom du fichier CSV que vous souhaitez utiliser comme base de salriés:\n");
            nomFichier = Console.ReadLine();
            List<List<string>> nouvelleBaseSalarie = bdd.LoadCSV(nomFichier, ',');
            bdd.SaveCSV("salariesData", nouvelleBaseSalarie, ',');



            Console.WriteLine("Entrez le nom du fichier CSV que vous souhaitez utiliser comme base d'adresses:\n");
            nomFichier = Console.ReadLine();
            List<List<string>> nouvellesAdresses = bdd.LoadCSV(nomFichier, ',');
            bdd.SaveCSV("adressesData", nouvellesAdresses, ',');


            List<List<string>> csvVierge = bdd.LoadCSV("csvVierge", ',');
            bdd.SaveCSV("commandesData", csvVierge, ',');
            bdd.SaveCSV("livraisonsData", csvVierge, ',');

            Console.Clear();
            aff.TitreMin("Remplacer la base (CSV)");
            aff.Separator(2);
            Console.WriteLine("La base a été remplacée avec succès. Vous allez être redirigé vers le menu.");
            aff.Separator(2);
            aff.AppuyezPourContinuer();
            Console.Clear();
            PageAccueil();
        }

        //endregion Import CSV Client
        #endregion

        public void PasImplemente()
        {
            Affichage aff = new Affichage();

            Console.Clear();
            aff.TitreMin("Erreur 404 : Page non trouvée");
            Console.WriteLine("\nCette page n'est pas encore implémentée mais c'est pour bientôt !\n\n--------\nAppuyez sur entrée pour revenir au menu principal...");
            Console.ReadLine();
            PageAccueil();
        }

        // Endregion des pages
        #endregion


        #region Fonctions de navigation


        /// <summary>
        /// Fonction propre à la classe navigation.
        /// Permet de naviguer vers une page correspondante au numéro entré par l'utilisateur, dans la liste des pages correspondantes au Menu qui est supposé affiché juste avant à l'aide de la classe Affichage. 
        /// </summary>
        /// <param name="pagesCorrespondantes">Liste de fonctions déléguées correspondant aux pages vers lesquelles ont peut naviguer.</param>
        /// <param name="pageActuelle">Page actuelle, à recharger en cas d'échec de l'entrée du choix de l'utilisateur.</param>
        private void SelectionMenu(List<AfficherPage> pagesCorrespondantes, AfficherPage pageActuelle)
        {
            int choix = 0;
            do
            {
                choix = Convert.ToInt32(Console.ReadLine());
                if(choix <= 0 || choix > pagesCorrespondantes.Count)
                {
                    Console.WriteLine("\nErreur avec le numéro choisi : Veuillez choisir un numéro entre 1 et " + pagesCorrespondantes.Count +"\nAppuyez sur entrée avant de ré-essayer...");
                    Console.ReadLine();
                    Console.Clear();
                    pageActuelle();
                }
                else
                {
                    pagesCorrespondantes[choix - 1]();
                } 
            } while (choix <= 0 || choix > pagesCorrespondantes.Count);
        }
        
        #endregion
    }
}
