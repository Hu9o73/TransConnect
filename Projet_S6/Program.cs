namespace Projet_S6
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Navigation nav = new Navigation();
            nav.PageAccueil();
        }

        public void Stats()
        {
            Console.WriteLine("BIENVENUE SUR LE MODULE STATS");

        }
    }
}