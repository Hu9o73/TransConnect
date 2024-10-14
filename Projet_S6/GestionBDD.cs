using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Projet_S6
{
    internal class GestionBDD
    {
        private List<Client> clients = new List<Client>();
        private List<Salarie> salaries = new List<Salarie>();
        private List<Commande> commandes = new List<Commande>();
        private List<Adresse> adresses = new List<Adresse>();
        private List<Livraison> livraisons = new List<Livraison>();

        private List<List<string>> clientsFile = new List<List<string>>();
        private List<List<string>> commandesFile = new List<List<string>>();
        private List<List<string>> adressesFile = new List<List<string>>();

        public GestionBDD()
        {
            
        }

        

    

        #region AddRemoveModifyClient

        public void AddClient(Client client)
        {

            // Ajout du client à la BDD

            List<Client> clients = RecoverClients();

            clients.Add(client);

            List<List<string>> clientsFile = new List<List<string>>();

            foreach(Client c in clients)
            {
                List<string> clientParams = new List<string>() { Convert.ToString(c.NSS), c.Nom, c.Prenom, Convert.ToString(c.Date_de_naissance.Year),
                Convert.ToString(c.Date_de_naissance.Month), Convert.ToString(c.Date_de_naissance.Day),
                Convert.ToString(c.Adresse.Ida), c.Mail, c.Telephone };

                foreach (Commande cmd in c.Commandes)
                {
                    clientParams.Add(Convert.ToString(cmd.IdC));
                }

                clientsFile.Add(clientParams);
            }
            
            
            // Ajout adresse à la BDD

            List<Adresse> adresses = RecoverAdresses();
            List<List<string>> adressesFile = new List<List<string>>();

            adresses.Add(client.Adresse);

            foreach (Adresse adresse in adresses)
            {        
                List<string> adresseStr = new List<string>() { Convert.ToString(adresse.Ida), Convert.ToString(adresse.Numero), adresse.Rue, adresse.Ville, Convert.ToString(adresse.CodePostal), adresse.Pays };
                adressesFile.Add(adresseStr);    
            }

            SaveCSV("adressesData", adressesFile, ',');
            SaveCSV("clientsData", clientsFile , ',');
        }
        public void RemoveClient(Client clientASuppr, List<Client> clients)
        {
            List<List<string>> clientsFile = new List<List<string>>();
            clients.Remove(clientASuppr);

            foreach (Client c in clients)
            {
                List<string> clientParams = new List<string>() { Convert.ToString(c.NSS), c.Nom, c.Prenom, Convert.ToString(c.Date_de_naissance.Year),
                Convert.ToString(c.Date_de_naissance.Month), Convert.ToString(c.Date_de_naissance.Day),
                Convert.ToString(c.Adresse.Ida), c.Mail, c.Telephone };

                foreach (Commande cmd in c.Commandes)
                {
                    clientParams.Add(Convert.ToString(cmd.IdC));
                }

                clientsFile.Add(clientParams);
            }

            SaveCSV("clientsData", clientsFile, ',');
        }

        /// <summary>
        /// Remplacer le client A par le client B
        /// </summary>
        /// <param name="clientA">Client à remplacer</param>
        /// <param name="clientB">Client par lequel le remplacer</param>
        public void ReplaceClient(Client clientA, Client clientB, List<Client> clients)
        {
            RemoveClient(clientA, clients);
            AddClient(clientB);
        }

        #endregion

        #region Commandes et livraisons
        public void AddOrder(Commande commande)
        {
            List<List<string>> commandesFile = LoadCSV("commandesData",',');

            List<string> orderParam = new List<string>() { Convert.ToString(commande.IdC), Convert.ToString(commande.Livraison.IdL),
                Convert.ToString(commande.Prix), commande.Vehicule, Convert.ToString(commande.Chauffeur.NSS), Convert.ToString(commande.Date.Year),
                Convert.ToString(commande.Date.Month), Convert.ToString(commande.Date.Day)};
            
            commandesFile.Add(orderParam);

            AddLivraison(commande.Livraison);

            SaveCSV("commandesData", commandesFile, ',');
        }

        public void AddLivraison(Livraison livraison)
        {

            // Sauvegarde livraison

            List<List<string>> livraisonsFile = LoadCSV("livraisonsData", ',');

            List<string> livraisonParam = new List<string>() { Convert.ToString(livraison.IdL), livraison.Marchandise, Convert.ToString(livraison.Volume),
            Convert.ToString(livraison.AdresseA.Ida), Convert.ToString(livraison.AdresseB.Ida)};
            foreach(string s in livraison.PlanDeRoute)
            {
                livraisonParam.Add(s);
            }
            livraisonsFile.Add(livraisonParam);

            SaveCSV("livraisonsData", livraisonsFile, ',');
        }

        //endregion Commandes et livraisons
        #endregion

        #region AddRemoveModifySalarie

        public void AddSalarie(Salarie salarie)
        {

            // Ajout du client à la BDD

            List<Salarie> salaries = RecoverSalaries();

            salaries.Add(salarie);

            List<List<string>> salariesFile = new List<List<string>>();

            foreach (Salarie s in salaries)
            {
                List<string> salarieParams = new List<string>() { Convert.ToString(s.NSS), s.Nom, s.Prenom, Convert.ToString(s.Date_de_naissance.Year),
                Convert.ToString(s.Date_de_naissance.Month), Convert.ToString(s.Date_de_naissance.Day),
                Convert.ToString(s.Adresse.Ida), s.Mail, s.Telephone, Convert.ToString(s.Date_entree.Year), Convert.ToString(s.Date_entree.Month),
                Convert.ToString(s.Date_entree.Day), s.Poste, Convert.ToString(s.Salaire) };


                salariesFile.Add(salarieParams);
            }


            // Ajout adresse à la BDD

            List<Adresse> adresses = RecoverAdresses();
            List<List<string>> adressesFile = new List<List<string>>();

            adresses.Add(salarie.Adresse);

            foreach (Adresse adresse in adresses)
            {
                List<string> adresseStr = new List<string>() { Convert.ToString(adresse.Ida), Convert.ToString(adresse.Numero), adresse.Rue, adresse.Ville, Convert.ToString(adresse.CodePostal), adresse.Pays };
                adressesFile.Add(adresseStr);
            }

            SaveCSV("adressesData", adressesFile, ',');
            SaveCSV("salariesData", salariesFile, ',');
        }
        public void RemoveSalarie(Salarie salarieASuppr, List<Salarie> salaries)
        {
            List<List<string>> salariesFile = new List<List<string>>();
            salaries.Remove(salarieASuppr);

            foreach (Salarie s in salaries)
            {
                List<string> salarieParams = new List<string>() { Convert.ToString(s.NSS), s.Nom, s.Prenom, Convert.ToString(s.Date_de_naissance.Year),
                Convert.ToString(s.Date_de_naissance.Month), Convert.ToString(s.Date_de_naissance.Day),
                Convert.ToString(s.Adresse.Ida), s.Mail, s.Telephone, Convert.ToString(s.Date_entree.Year), Convert.ToString(s.Date_entree.Month),
                Convert.ToString(s.Date_entree.Day), s.Poste, Convert.ToString(s.Salaire)};



                salariesFile.Add(salarieParams);
            }

            SaveCSV("salariesData", salariesFile, ',');
        }

        /// <summary>
        /// Remplacer un salarié par un autre dans la BDD
        /// </summary>
        /// <param name="salarieA">Salarié à remplacer</param>
        /// <param name="salarieB">Salarié à entrer dans la BDD</param>
        /// <param name="salaries">Liste des salariés à manipuler</param>
        public void ReplaceSalarie(Salarie salarieA, Salarie salarieB, List<Salarie> salaries)
        {
            RemoveSalarie(salarieA, salaries);
            AddSalarie(salarieB);
        }

        #endregion


        #region Récupérations

        #region Récupération clients


        public List<Client> RecoverClients()
        {
            List<Client> clients = new List<Client>();

            List<List<string>> clientsFile = LoadCSV("clientsData", ',');

            if (clientsFile != null &&  clientsFile.Count > 0)
            {
                foreach (List<string> c in clientsFile)
                {
                    DateTime ddn = new DateTime(Convert.ToInt32(c[3]), Convert.ToInt32(c[4]), Convert.ToInt32(c[5]));

                    Adresse adresse = RecoverAdress(Convert.ToInt32(c[6]));

                    List<Commande> listeCommandes = new List<Commande>();
                    for (int i = 9; i < c.Count; i++)
                    {
                        listeCommandes.Add(RecoverOrder(Convert.ToInt32(c[i])));
                    }

                    
                    Client client = new Client(c[0], c[1], c[2], ddn, adresse, c[7], c[8], listeCommandes);
                    clients.Add(client);
                }

                return clients;
            }
            else
            {
                return new List<Client>();
            }

            
        }

        public Client? RecoverClient(string nss)
        {
            clients = RecoverClients();
            foreach(Client c in clients)
            {
                if(c.NSS == nss)
                {
                    return c;
                }
            }
            return null;
        }


        #endregion


        #region Récupération de commandes
        public List<Commande> RecoverOrders()
        {
            List<Commande> commandes = new List<Commande>();

            List<List<string>> commandesFile = LoadCSV("commandesData", ',');

            if(commandesFile != null && commandesFile.Count > 0)
            {
                foreach (List<string> cmd in commandesFile)
                {
                    DateTime date = new DateTime(Convert.ToInt32(cmd[5]), Convert.ToInt32(cmd[6]), Convert.ToInt32(cmd[7]));
                    Commande commande = new Commande(Convert.ToInt32(cmd[0]), RecoverLivraison(Convert.ToInt32(cmd[1])), Convert.ToInt32(cmd[2]), cmd[3], RecoverSalarie(cmd[4]), date);

                    commandes.Add(commande);
                }

                return commandes;
            }
            else
            {
                return new List<Commande>();
            }

            
        }

        public Commande? RecoverOrder(int idc)
        {
            commandes = RecoverOrders();
            foreach(Commande c in commandes)
            {
                if(c.IdC == idc)
                {
                    return c;
                }
            }
            return null;
        }

#endregion


        #region Récupération Salariés

        public List<Salarie> RecoverSalaries()
        {
            List<Salarie> salaries = new List<Salarie>();

            List<List<string>> salariesFile = LoadCSV("salariesData", ',');

            if(salariesFile != null && salariesFile.Count >0 )
            {
                foreach (List<string> s in salariesFile)
                {
                    DateTime ddn = new DateTime(Convert.ToInt32(s[3]), Convert.ToInt32(s[4]), Convert.ToInt32(s[5]));
                    DateTime dde = new DateTime(Convert.ToInt32(s[9]), Convert.ToInt32(s[10]), Convert.ToInt32(s[11]));
                    Adresse adresse = RecoverAdress(Convert.ToInt32(s[6]));

                    Salarie salarie = new Salarie(s[0], s[1], s[2], ddn, adresse, s[7], s[8], dde, s[12], Convert.ToInt32(s[13]));
                    salaries.Add(salarie);
                }

                return salaries;
            }
            else
            {
                return new List<Salarie>();
            }
            
        }

        public Salarie? RecoverSalarie(string num)
        {
            salaries = RecoverSalaries();
            foreach(Salarie s in salaries)
            {
                if(s.NSS == num)
                {
                    return s;
                }
            }
            return null;
        }


#endregion


        #region Récupération Livraisons

        public List<Livraison> RecoverLivraisons()
        {
            List<Livraison> livraisons = new List<Livraison>();

            List<List<string>> livraisonsFile = LoadCSV("livraisonsData", ',');

            if(livraisonsFile != null && livraisonsFile.Count > 0)
            {
                foreach (List<string> l in livraisonsFile)
                {
                    List<string> planDeRoute = new List<string>();
                    for (int i = 5; i < l.Count; i++)
                    {
                        planDeRoute.Add(l[i]);
                    }

                    Livraison livraison = new Livraison(Convert.ToInt32(l[0]), l[1], Convert.ToInt32(l[2]), RecoverAdress(Convert.ToInt32(l[3])),
                        RecoverAdress(Convert.ToInt32(l[4])), planDeRoute);

                    livraisons.Add(livraison);
                }

                return livraisons;
            }
            else
            {
                return new List<Livraison>();
            }
            
        }

        public Livraison? RecoverLivraison(int idl)
        {
            livraisons = RecoverLivraisons();
            foreach (Livraison l in livraisons)
            {
                if (l.IdL == idl)
                {
                    return l;
                }
            }
            return null;
        }

        #endregion


        #region Récupération d'adresses

        public List<Adresse> RecoverAdresses()
        {
            List<Adresse> adresses = new List<Adresse>();

            List<List<string>> adressesFile = LoadCSV("adressesData", ',');
            if(adressesFile != null && adressesFile.Count > 0)
            {
                foreach (List<string> c in adressesFile)
                {
                    Adresse adresse = new Adresse(Convert.ToInt32(c[0]));
                    adresses.Add(adresse);
                }

                return adresses;
            }
            else
            {
                return new List<Adresse>();
            }
            
        }

        public Adresse RecoverAdress(int ida)
        {
            List<Adresse> adresses = RecoverAdresses();
            foreach(Adresse a in adresses)
            {
                if(a.Ida == ida)
                {
                    return a;
                }
            }
            return null;                // Si c'est tout cassé chercher ici !
        }

        #endregion

        //endregion Récupérations
        #endregion

        #region Chargement de CSV

        public List<List<string>> LoadCSV(string filename, char sep)
        {
            // Récupérer le chemin d'accès au fichier CSV
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\" + filename);

            // Vérifier son existence
            if (File.Exists(filePath))
            {
                //Création de la List<List<string>>
                List<List<string>> myFile = new List<List<string>>();

                // Lecture de toutes les lignes du fichier
                string[] lines = File.ReadAllLines(filePath);

                // Pour chaque ligne...
                for (int i = 0; i < lines.Length; i++)
                {
                    // Split the line into individual data elements using comma as the delimiter
                    string[] elementsInLine = lines[i].Split(sep);

                    if (elementsInLine[0] == null || elementsInLine[0].Length == 0 || elementsInLine[0] == "" || elementsInLine[0] == " ")
                    {
                        //Do nothing, don't integrate rows if there is nothing in it.
                        Debug.WriteLine("Detected an empty row at position : " + Convert.ToString(i) + "| File : " + filePath);
                    }
                    else
                    {
                        // Create a list to store the elements of the current line
                        List<string> rowData = new List<string>();
                        // Add each element to the list
                        foreach (string element in elementsInLine)
                        {
                            rowData.Add(element);
                        }
                        myFile.Add(rowData);
                    }
                }
                
                // Débogage
                //Debug.WriteLine("Successfuly Loaded CSV File : " + filePath);
                return myFile;
            }
            else
            {
                // File not founds => Débogage
                Debug.WriteLine("CSV file not found at path: " + filePath);
                return null;
            }
        }

        public void SaveCSV(string filename, List<List<string>> myFile, char sep)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/" + filename);

            // Créer ou réécrire le fichier CSV
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < myFile.Count; i++)
                {
                    for (int j = 0; j < myFile[i].Count - 1; j++)
                    {
                        writer.Write(myFile[i][j] + Convert.ToString(sep));
                    }
                    writer.WriteLine(myFile[i][myFile[i].Count - 1]);
                }
            }

            Debug.WriteLine("CSV file created at: Data/" + filename);
        }

        #endregion

        #region GETSET

        public List<Adresse> Adresses
        {
            get { return adresses; }
            set { adresses = value; }
        }


        #endregion
    }
}
