using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronisateur.TCP
{
    public class ServeurTCP
    {
        private bool marche;
        private Thread th;
        private TcpListener ecoute;

        public ServeurTCP()
        {
            marche = true;
        }

        public void Start()
        {
            if (!th.IsAlive)
            {
                marche = true;
                int port = 16044;

                try
                {
                    ecoute = new TcpListener(IPAddress.Parse("0.0.0.0"), port);
                    th = new Thread(new ThreadStart(boucle));

                    th.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Stop()
        {
            if (th.IsAlive)
            {
                marche = false;
                ecoute.Stop();
            }
        }

        private void boucle()
        {
            int nbClients = 0;
            TcpClient liaisonClient = null;
            ecoute.Start();

            while (marche)
            {
                liaisonClient = ecoute.AcceptTcpClient();
                nbClients++;

                new Thread(new ThreadStart(new InstanceServeur(liaisonClient, nbClients).Run)).Start();
            }
            ecoute.Stop();
        }
    }
}
