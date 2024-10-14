using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S6
{
    internal class ModuleSalarie
    {
        public Salarie AjoutSalarieManuel()
        {
            Affichage aff = new Affichage();
            Salarie salarie;

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le nom du salarié :\n");
            string nom = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le prénom du salarié :\n");
            string prenom = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le numéro de sécurité sociale du salarié :\n");
            string nSS = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez l'année de naissance du salarié :\n");
            int annee = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le mois de naissance du salarié (nombre):\n");
            int mois = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le jour de naissance du salarié :\n");
            int jour = Convert.ToInt32(Console.ReadLine());
            DateTime date_de_naissance = new DateTime(annee, mois, jour);

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez l'adresse e-mail du salarié :\n");
            string mail = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le téléphone du salarié :\n");
            string telephone = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nAdresse du client :\nEntrez le pays de résidence du salarié :\n");
            string pays = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le code postal du salarié :\n");
            int codePostal = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez la ville de résidence du salarié :\n");
            string ville = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez la rue de résidence du salarié :\n");
            string rue = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le numéro de résidence du salarié :\n");
            int numero = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez l'année d'entrée dans l'entreprise du salarié :\n");
            int anneeEntree = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le mois d'entrée dans l'entreprise du salarié :\n");
            int moisEntree = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le jour d'entrée dans l'entreprise du salarié :\n");
            int jourEntree = Convert.ToInt32(Console.ReadLine());

            DateTime dateEntree = new DateTime(anneeEntree, moisEntree, jourEntree);

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le poste du salarié :\n");
            string poste = Console.ReadLine();

            Console.Clear();
            aff.TitreMin("Création manuelle de salarié");
            Console.WriteLine("\nEntrez le salaire du salarié :\n");
            int salaire = Convert.ToInt32(Console.ReadLine());


            Adresse adresse = new Adresse(numero, rue, ville, codePostal, pays);

            salarie = new Salarie(nSS, nom, prenom, date_de_naissance, adresse, mail, telephone, dateEntree, poste, salaire);

            return salarie;
        }
        public bool ModifierSalarie(string nssAModif)
        {
            GestionBDD bdd = new GestionBDD();
            Affichage aff = new Affichage();
            List<Salarie> salaries = bdd.RecoverSalaries();
            Salarie salarieAModif;
            Salarie nouveauSalarie;
            bool salarieTrouve = false;


            for (int i = 0; i < salaries.Count; i++)
            {
                if (salaries[i].NSS == nssAModif)
                {
                    salarieTrouve = true;
                    salarieAModif = salaries[i];

                    // Affichage
                    Console.Clear();
                    aff.TitreMin("Modification d'un client");
                    aff.Separator(1);
                    Console.WriteLine("Client trouvé :\n");
                    Console.WriteLine(salarieAModif.ToString());
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
                            nouveauSalarie = new Salarie(salarieAModif.NSS, nom, salarieAModif.Prenom, salarieAModif.Date_de_naissance, salarieAModif.Adresse, salarieAModif.Mail, salarieAModif.Telephone, salarieAModif.Date_entree, salarieAModif.Poste, salarieAModif.Salaire);
                            bdd.ReplaceSalarie(salarieAModif, nouveauSalarie, salaries);
                            break;
                        case 2:
                            Console.WriteLine("Entrez le nouveau prénom du client :\n");
                            string prenom = Console.ReadLine();
                            nouveauSalarie = new Salarie(salarieAModif.NSS, salarieAModif.Nom, prenom, salarieAModif.Date_de_naissance, salarieAModif.Adresse, salarieAModif.Mail, salarieAModif.Telephone, salarieAModif.Date_entree, salarieAModif.Poste, salarieAModif.Salaire);
                            bdd.ReplaceSalarie(salarieAModif, nouveauSalarie, salaries);
                            break;
                        case 3:
                            Console.WriteLine("Entrez le nouveau téléphone du client :\n");
                            string tel = Console.ReadLine();
                            nouveauSalarie = new Salarie(salarieAModif.NSS, salarieAModif.Nom, salarieAModif.Prenom, salarieAModif.Date_de_naissance, salarieAModif.Adresse, salarieAModif.Mail, tel, salarieAModif.Date_entree, salarieAModif.Poste, salarieAModif.Salaire);
                            bdd.ReplaceSalarie(salarieAModif, nouveauSalarie, salaries);
                            break;
                        case 4:
                            Console.WriteLine("Entrez le nouveau mail du client :\n");
                            string mail = Console.ReadLine();
                            nouveauSalarie = new Salarie(salarieAModif.NSS, salarieAModif.Nom, salarieAModif.Prenom, salarieAModif.Date_de_naissance, salarieAModif.Adresse, mail, salarieAModif.Telephone, salarieAModif.Date_entree, salarieAModif.Poste, salarieAModif.Salaire);
                            bdd.ReplaceSalarie(salarieAModif, nouveauSalarie, salaries);
                            break;
                        default:
                            salarieTrouve = false;
                            break;
                    }
                    break;
                }
            }


            return salarieTrouve;
        }
        public bool SupprimerClient(string nssASuppr)
        {
            GestionBDD bdd = new GestionBDD();
            List<Salarie> salaries = bdd.RecoverSalaries();
            bool salarieTrouve = false;

            for (int i = 0; i < salaries.Count; i++)
            {
                if (salaries[i].NSS == nssASuppr)
                {
                    bdd.RemoveSalarie(salaries[i], salaries);
                    salarieTrouve = true;
                    break;
                }
            }

            return salarieTrouve;

        }
    }
}
