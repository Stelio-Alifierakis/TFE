using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur;

namespace vueConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
            DAL d = new DAL(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])+ "/test.sqlite");
            //d.creation();
            //d.seed();

            d.checkPartWord("pas de sex");
            d.checkPartWord("seks");
            d.checkPartWord("partout");

            bool essai = (Console.ReadKey().Key == ConsoleKey.O);
        }
    }
}
