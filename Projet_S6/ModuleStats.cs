using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class ModuleStats
    {

        public void LivraisonParChauffeurvlistes()
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            aff.TitreMin("Nombre de livraisons effectuées par chaque chauffeur");
            List<string> nsschauffeurs = new List<string>();
            int numcommandes = 0;
            List<Commande> listcommandes = bdd.RecoverOrders();
            List<Salarie> listsalaries = bdd.RecoverSalaries();

            //Construction rapide de la liste des NSS de chauffeurs
            foreach (Salarie s in listsalaries)
            {
                if (s.Poste == "Chauffeur")
                {
                    nsschauffeurs.Add(s.NSS);
                }
            }
            if (listcommandes.Count == 0 || listcommandes == null)
            {
                Console.WriteLine("Erreur - Aucune commande n'a été effectuée");
            }
            else
            {
                //Affichage le nombre de commandes comptées pour chaque NSS de chauffeur
                foreach (string nss in nsschauffeurs)
                {
                    numcommandes = 0;
                    foreach (Commande c in listcommandes)
                    {
                        if (c.Chauffeur.NSS == nss)
                        {
                            numcommandes++;
                        }
                    }
                    Console.WriteLine("Le chauffeur n°" + nss + " a effectué " + numcommandes + " livraisons.");
                }
            }
        }
        public void LivraisonParChauffeurvdico()
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            aff.TitreMin("Nombre de livraisons effectuées par chaque chauffeur");
            Dictionary<int, int> chauffeurscommandes = new Dictionary<int, int>();
            List<Commande> listcommandes = bdd.RecoverOrders();

            if (listcommandes.Count == 0 || listcommandes == null)
            {
                Console.WriteLine("Erreur - Aucune commande n'a été effectuée");
            }
            else
            {
                foreach (Commande c in listcommandes)
                {
                    if (chauffeurscommandes.ContainsKey(Convert.ToInt32(c.Chauffeur.NSS)))
                    {
                        chauffeurscommandes[Convert.ToInt32(c.Chauffeur.NSS)]++;
                    }
                    else
                    {
                        chauffeurscommandes[Convert.ToInt32(c.Chauffeur.NSS)] = 1;
                    }
                }

                foreach (int chauff in chauffeurscommandes.Keys)
                {
                    Console.WriteLine("Le chauffeur n°" + chauff + " a effectué " + chauffeurscommandes[chauff] + " livraisons.");
                }
            }


        }
        public void LivraisonParChauffeur()
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            aff.TitreMin("Nombre de livraisons effectuées par chaque chauffeur");
            Dictionary<int,int> chauffeurscommandes = new Dictionary<int,int>();
            List<Commande> listcommandes = bdd.RecoverOrders();

            if (listcommandes.Count == 0 || listcommandes == null)
            {
                Console.WriteLine("Erreur - Aucune commande n'a été effectuée");
            }
            else
            {

                foreach (Commande c in listcommandes)
                {
                    if (chauffeurscommandes.ContainsKey(Convert.ToInt32(c.Chauffeur.NSS)))
                    {
                        chauffeurscommandes[Convert.ToInt32(c.Chauffeur.NSS)]++;

                    }
                    /*else
                    {
                        chauffeurscommandes[Convert.ToInt32(c.Chauffeur.NSS)] = 1;
                    }*/
                }

                foreach (int chauff in chauffeurscommandes.Keys)
                {
                    Console.WriteLine("Le chauffeur n°" + chauff + " a effectué " + chauffeurscommandes[chauff] + " livraisons.");
                }
            }
            

        }
        public void CommandePeriode()
        {
            Affichage aff = new Affichage();
            //On récupère l'année de départ et l'année de fin de période que l'on souhaite étudier
            aff.TitreMin("Liste des commandes sur une période donnée");
            Console.WriteLine("Rentrez l'année de départ de la période que vous souhaitez étudier");
            int anneedepart = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Rentrez l'année de fin de la période que vous souhaitez étudier");
            int anneefin = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            // On affiche la liste des commandes sur la période demandée (si elle est non-vide)
            aff.TitreMin("Liste des commandes entre " + anneedepart + " et " + anneefin);
            GestionBDD bdd = new GestionBDD();
            List<Commande> listcom = bdd.RecoverOrders();
            bool res = false;
            foreach (Commande c in listcom)
            {
                if (c.Date.Year >= anneedepart && c.Date.Year <= anneefin)
                {
                    Console.WriteLine(c.ToString());
                    res = true;
                }
            }
            if (!res) { Console.WriteLine("Il n'y a aucune commande qui a été effectuée entre " + anneedepart + " et " + anneefin); }


        }

        //Fonction comparer deux dates à faire
        public void AfficherMoyennePrix()
        {
            GestionBDD bdd = new GestionBDD();
            List<Commande> listcom = bdd.RecoverOrders();
            double sum = 0;
            for (int i = 0; i < listcom.Count; i++)
            {
                sum += listcom[i].Prix;
            }
            double moy = sum / listcom.Count;
            Console.WriteLine("La moyenne des prix des commandes s'élève à " + moy);
        }


        public void ListeCommandeClient()
        {
            Console.Clear();
            Affichage aff = new Affichage();
            GestionBDD bdd = new GestionBDD();
            List<Client> listclients = bdd.RecoverClients();
            aff.TitreMin("Liste des commandes d'un client");
            aff.Separator(1);
            Console.WriteLine("Saisir le NSS du client dont vous souhaitez la liste des commandes");
            string nss = Console.ReadLine();
            int index = 0;
            if (bdd.RecoverClient(nss) == null)
            {
                Console.Clear();
                Console.WriteLine("Erreur 404 - Ce numéro de NSS est erronné. \nAppuyez sur Entrée puis refaites la saisie.");
                Console.ReadKey();
                ListeCommandeClient();
            }
            else
            {

                foreach (Client c in listclients)
                {
                    if (c.NSS != nss) { index++; }
                }
                if (listclients[index].Commandes.Count == 0)
                {
                    Console.WriteLine("Ce client n'a effectué aucune commande");
                }
                else
                {
                    Console.WriteLine("Liste des commandes du client n°" + listclients[index].NSS);
                    for (int i = 0; i < listclients[index].Commandes.Count(); i++)
                    {
                        Console.WriteLine(listclients[index].Commandes[i].ToString());
                    }
                }
            }

        }
       
        public void MoyennePrixCommandesParClient()
        {
            Console.Clear();
            
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            aff.TitreMin("Liste des dépenses moyennes par commande par client");
            List<Client> listclients = bdd.RecoverClients();
            List<int> listnbcom= new List<int>();
            double sum = 0;

            for (int i = 0; i<listclients.Count(); i++)
            {
                for (int j = 0; j < listclients[i].Commandes.Count(); j++)
                {
                    sum += listclients[i].Commandes[j].Prix;
                }
                double avg = sum / listclients[i].Commandes.Count();
                Console.WriteLine("Le client n° " + listclients[i].NSS + "paye en moyenne " + avg + " euros par commande");
            }

        }
        /*
        public void MontantTotalParClient()
        {
            GestionBDD bdd = new GestionBDD();
            
            List<Client> listclient = bdd.RecoverClients();
            Dictionary<string, int> clientmontant = new Dictionary<string, int>();
            int sum = 0;
            foreach(Client c in listclient)
            {
               
                if (c.Commandes == null || c.Commandes.Count == 0)
                {
                    sum = 0;
                }
                else
                {
                    foreach (Commande order in c.Commandes)
                    {
                        sum += order.Prix;
                    }   
                }
                clientmontant.Add(c.NSS, sum);

            }

            //Sortbyvaleurdu dico
            //Affichage du client.NSS + prenom + nom + Affichage de la valeur

            

        }
        */



        public void Stat5()
        {

        }



    }
}
