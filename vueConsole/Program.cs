using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur;
using Rechercheur;
using proxy;
using System.Runtime.InteropServices;

namespace vueConsole
{
    class Program
    {
        private static readonly IEproxy Controller = new Eproxy();

        static void Main(string[] args)
        {
            NativeMethods.Handler = ConsoleEventCallback;
            NativeMethods.SetConsoleCtrlHandler(NativeMethods.Handler, true);

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
            Console.WriteLine(r.valPhrase(@"").ToString());

            Controller.StartProxy();

            Console.WriteLine("Hit any key to exit..");
            Console.WriteLine();
            Console.Read();

            Controller.Stop();

            //bool essai = (Console.ReadKey().Key == ConsoleKey.O);
        }

        private static bool ConsoleEventCallback(int eventType)
        {
            if (eventType != 2) return false;
            try
            {
                Controller.Stop();
            }
            catch
            {
                // ignored
            }
            return false;
        }
    }

    internal static class NativeMethods
    {
        // Keeps it from getting garbage collected
        internal static ConsoleEventDelegate Handler;

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        // Pinvoke
        internal delegate bool ConsoleEventDelegate(int eventType);
    }
}
