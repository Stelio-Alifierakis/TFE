using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace wsProxy
{
    public partial class wsProxy : ServiceBase
    {
        /*private static readonly IEproxy Controller = new Eproxy();
        private DAL d;*/

        static Process cons;

        public wsProxy()
        {

            InitializeComponent();
            eventLog1.Source = "testLog";
            eventLog1.Log = "newLog";
            /*if (!System.Diagnostics.EventLog.SourceExists("testLog"))
            {
                System.Diagnostics.EventLog.CreateEventSource("testLog","newLog");
            }

            eventLog1.Source = "testLog";
            eventLog1.Log = "newLog";

            d = new DAL(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/bdd/test.sqlite");
            //d.creation();
            //d.seed();

            NativeMethods.Handler = ConsoleEventCallback;
            NativeMethods.SetConsoleCtrlHandler(NativeMethods.Handler, true);*/
        }

        protected override void OnStart(string[] args)
        {
            //DAL d = new DAL(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/test.sqlite");


            /*Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);

            Controller.setRechercheur(r);*/
            cons = new Process();
            cons.StartInfo.FileName="console/vueConsole.exe";
            cons.StartInfo.CreateNoWindow = true;
            cons.StartInfo.UseShellExecute = false;
            cons.Start();

            eventLog1.WriteEntry("est démarré");
            /*Controller.StartProxy();*/
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("est éteint");
            cons.Close();
            /*Controller.Stop();*/
        }

        /*private static bool ConsoleEventCallback(int eventType)
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
        internal delegate bool ConsoleEventDelegate(int eventType);*/
    }
}
