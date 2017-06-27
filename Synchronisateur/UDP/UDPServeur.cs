using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

using Synchronisateur.Message;
using BaseDonnees;

namespace Synchronisateur.UDP
{
    public class UDPServeur : IUDPServeur
    {
        private Thread th;
        private bool marche = true;
        private UdpClient serveur = null;
        private Serial s;
        private IDAL bdd;

        public UDPServeur(IDAL bdd)
        {
            this.bdd = bdd;
        }

        public void Start()
        {
            marche = true;

            bool erreur = false;
            int i = 0;

            s = new Serial();

            do
            {
                try
                {
                    IPEndPoint ip = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 16043);
                    serveur = new UdpClient(ip);
                    //serveur = new UdpClient(16043);
                    Console.WriteLine("Serveur lancé");
                }
                catch (Exception ex)
                {
                    erreur = true;
                    i++;
                    Console.WriteLine(ex.Message);
                }
            } while (erreur && i < 4);


            th = new Thread(new ThreadStart(BoucleServeur));
            th.Start();
        }

        public void Stop()
        {
            marche = false;
            serveur.Close();
        }

        private void BoucleServeur()
        {

            while (marche)
            {
                try
                {
                    IPEndPoint client = null;

                    Crypteur crypt = new Crypteur();

                    byte[] donnee = serveur.Receive(ref client);

                    Console.WriteLine("Données reçues en provenance de {0}:{1}.", client.Address, client.Port);

                    byte[] c = crypt.decrypt(donnee);

                    string msg = s.messageString<MessageEnvoi>(c).message;

                    //string msg = s.messageString(donnee);

                    Console.WriteLine("--------------> Message reçu " + msg);
                }
                catch { }
                finally
                {
                    //serveur.Close();
                }
            }

            serveur.Close();
            Console.WriteLine("Serveur UDP fini");

        }
    }
}
