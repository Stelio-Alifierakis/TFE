using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur;
using Rechercheur;

namespace vueConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
            DAL d = new DAL(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])+ "/test.sqlite");
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);
            r.ValArret = 20;

            //d.creation();
            //d.seed();

            //d.checkPartWord("pas de sex");
            //d.checkPartWord("seks");
            //d.checkPartWord("partout");

            //d.verifSite("sex");
            //d.verifSite("seks");
            //d.verifSite("partout");
            Console.WriteLine(r.checkPartWord("pas de sex").ToString());
            Console.WriteLine(r.checkPartWord("seks").ToString());
            Console.WriteLine(r.checkPartWord("partout").ToString());
            Console.WriteLine(r.valPhrase("pas de sex, pas de sex, pas de sex, pas de sex, pas de sex, pas de seks <br> du porn, encore du porn et du sexe, pas de sex, pas de seks <br> du porn, encore du porn et du sexe, pas de sex, pas de seks <br> du porn, encore du porn et du sexe, pas de sex, pas de seks <br> du porn, encore du porn et du sexe, pas de sex, pas de seks <br> du porn, encore du porn et du sexe,pas de sex, pas de seks <br> du porn, encore du porn et du sexe").ToString());

            bool essai = (Console.ReadKey().Key == ConsoleKey.O);
        }
    }
}
