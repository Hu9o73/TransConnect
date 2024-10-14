using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class ModuleClient
    {
        public Client AjoutClientManuel()
        {
            Affichage aff = new Affichage();
            Client client;

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le nom du client :\n");
            string nom = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le prénom du client :\n");
            string prenom = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le numéro de sécurité sociale du client :\n");
            string nSS = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez l'année de naissance du client :\n");
            int annee = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le mois de naissance du client (nombre):\n");
            int mois = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le jour de naissance du client :\n");
            int jour = Convert.ToInt32(Console.ReadLine());
            DateTime date_de_naissance = new DateTime(annee, mois, jour);

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez l'adresse e-mail du client :\n");
            string mail = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le téléphone du client :\n");
            string telephone = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nAdresse du client :\nEntrez le pays de résidence du client :\n");
            string pays = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le code postal du client :\n");
            int codePostal = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez la ville de résidence du client :\n");
            string ville = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez la rue de résidence du client :\n");
            string rue = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de client");
            Console.WriteLine("\nEntrez le numéro de résidence du client :\n");
            int numero = Convert.ToInt32(Console.ReadLine());


            Adresse adresse = new Adresse(numero, rue, ville, codePostal, pays);
            
            client = new Client(nSS, nom, prenom, date_de_naissance, adresse, mail, telephone, new List<Commande>());
            
            return client;
        }
        public bool ModifierClient(string nssAModif)
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            List<Client> clients = bdd.RecoverClients();
            Client clientAModif;
            Client nouveauClient;
            bool clientTrouve = false;


            for(int i = 0; i < clients.Count; i++)
            {
                if (clients[i].NSS == nssAModif)
                {
                    clientTrouve = true;
                    clientAModif = clients[i];

                    // Affichage
                    Console.Clear();
                    aff.TitreMin("Modification d'un client");
                    aff.Separator(1);
                    Console.WriteLine("Client trouvé :\n");
                    Console.WriteLine(clientAModif.ToString());
                    aff.Separator(2);
                    Console.WriteLine("Que voulez-vous modifier ?");
                    string[] options = { "Nom", "Prénom", "Téléphone", "Mail" };
                    aff.Menu("Caractéristiques", options);
                    int choix = Convert.ToInt32(Console.ReadLine());
                    
                    switch (choix)
                    {
                        case 1:
                            Console.WriteLine("Entrez le nouveau nom du client :\n");
                            string nom = Console.ReadLine();
                            nouveauClient = new Client(clientAModif.NSS, nom, clientAModif.Prenom, clientAModif.Date_de_naissance, clientAModif.Adresse, clientAModif.Mail, clientAModif.Telephone, clientAModif.Commandes);
                            bdd.ReplaceClient(clientAModif, nouveauClient, clients);
                            break;
                        case 2:
                            Console.WriteLine("Entrez le nouveau prénom du client :\n");
                            string prenom = Console.ReadLine();
                            nouveauClient = new Client(clientAModif.NSS, clientAModif.Nom, prenom, clientAModif.Date_de_naissance, clientAModif.Adresse, clientAModif.Mail, clientAModif.Telephone, clientAModif.Commandes);
                            bdd.ReplaceClient(clientAModif, nouveauClient, clients);
                            break;
                        case 3:
                            Console.WriteLine("Entrez le nouveau téléphone du client :\n");
                            string tel = Console.ReadLine();
                            nouveauClient = new Client(clientAModif.NSS, clientAModif.Nom, clientAModif.Prenom, clientAModif.Date_de_naissance, clientAModif.Adresse, clientAModif.Mail, tel, clientAModif.Commandes);
                            bdd.ReplaceClient(clientAModif, nouveauClient, clients);
                            break;
                        case 4:
                            Console.WriteLine("Entrez le nouveau mail du client :\n");
                            string mail = Console.ReadLine();
                            nouveauClient = new Client(clientAModif.NSS, clientAModif.Nom, clientAModif.Prenom, clientAModif.Date_de_naissance, clientAModif.Adresse, mail, clientAModif.Telephone, clientAModif.Commandes);
                            bdd.ReplaceClient(clientAModif, nouveauClient, clients);
                            break;
                        default:
                            clientTrouve = false;
                            break;
                    }
                    break;
                }
            }


            return clientTrouve;
        }
        public bool SupprimerClient(string nssASuppr)
        {
            GestionBDD bdd = new GestionBDD();
            List<Client> clients = bdd.RecoverClients();
            bool clientTrouve = false;

            for(int i = 0; i < clients.Count; i++)
            {
                if (clients[i].NSS ==  nssASuppr)
                {
                    bdd.RemoveClient(clients[i], clients);
                    clientTrouve = true;
                    break;
                }
            }

            return clientTrouve;

        }
    }
}
