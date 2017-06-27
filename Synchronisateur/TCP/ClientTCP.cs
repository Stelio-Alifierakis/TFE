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
    public class ClientTCP
    {
        private TcpClient client;
        private int port;
        private IPAddress serveur;

        public ClientTCP()
        {
            client = null;
            port = 16044;

            serveur = IPAddress.Parse("192.168.0.24");
        }

        public void init()
        {

            try
            {
                IPEndPoint ipendpoint = new IPEndPoint(serveur, port);

                client = new TcpClient();

                client.Connect(ipendpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Thread thReceive = new Thread(new ThreadStart(new ClientReception(client).Run));
        }

        public void stop()
        {
            client.Close();
        }
    }
}
