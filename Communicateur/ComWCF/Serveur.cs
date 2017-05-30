using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ServiceModel;
using Communicateur.ComProxy;

namespace Communicateur.ComWCF
{
    public class Serveur
    {
        private bool marche = true;
        private Thread th;
        //private IPipeObservateur obs;

        private ServiceHost host;

        public Serveur(IProxyCom prox)
        {
            SimpleService.AjoutProxyCom(prox);
        }

        public void Init()
        {
            marche = true;

            th = new Thread(run);

            //th.Start();
        }

        public void run()
        {

            try
            {
                host = new ServiceHost(typeof(SimpleService), new Uri("net.pipe://localhost"));
                host.AddServiceEndpoint(typeof(ISimpleService), new NetNamedPipeBinding(), "SimpleService");
                host.Open();

                Console.WriteLine("Simple Service Running...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            while (marche)
            {

            }
        }

        public void start()
        {
            if (!th.IsAlive)
            {
                Console.WriteLine("Ouverture");
                marche = true;
                th.Start();
            }
        }

        public void stop()
        {
            if (th.IsAlive)
            {
                Console.WriteLine("fermeture");
                marche = false;
                host.Close();
            }
        }
    }
}
