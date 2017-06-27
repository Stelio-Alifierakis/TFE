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
    public class ClientReception
    {
        private TcpClient client;
        private bool marche;

        public ClientReception(TcpClient client)
        {
            marche = true;
            this.client = client;
        }

        public void Stop()
        {
            marche = false;
        }

        public void Run()
        {
            string commande = null;

            try
            {
                NetworkStream stream = client.GetStream();

                while (marche)
                {

                    MessageEnvoi message = new MessageEnvoi();

                    if (stream.CanRead)
                    {
                        var bytes = default(byte[]);
                        var buffer = new byte[512];
                        var bytesRead = 0;

                        Crypteur crypt = new Crypteur();
                        Serial s = new Serial();

                        using (MemoryStream mem = new MemoryStream())
                        {
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                mem.Write(buffer, 0, bytesRead);

                                bytes = mem.ToArray();

                                if (bytesRead < 512)
                                {
                                    break;
                                }
                            }

                            mem.Position = 0;

                            byte[] c = crypt.decrypt(mem.ToArray());

                            message = s.messageString<MessageEnvoi>(c);
                        }
                    }

                    commande = message.message;


                    switch (commande)
                    {
                        case "":
                            break;
                        case "fin":
                            marche = false;
                            break;
                        default:
                            marche = false;
                            break;
                    }
                }

                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
