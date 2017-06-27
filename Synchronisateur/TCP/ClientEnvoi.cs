using Synchronisateur.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Synchronisateur.TCP
{
    public class ClientEnvoi
    {
        private TcpClient client;
        private bool marche;

        public ClientEnvoi(TcpClient client)
        {
            marche = true;
            this.client = client;
        }


        public void Stop()
        {
            marche = false;
        }

        public void envoi(MessageEnvoi message)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                Serial s = new Serial();

                Crypteur crypt = new Crypteur();

                byte[] b = default(byte[]);

                using (MemoryStream mem = new MemoryStream())
                {

                    s.serial<MessageEnvoi>(message, mem);

                    b = mem.ToArray();
                }

                byte[] c = crypt.crypt(b);

                stream.Write(c, 0, (int)c.Length);
                stream.Flush();

                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
