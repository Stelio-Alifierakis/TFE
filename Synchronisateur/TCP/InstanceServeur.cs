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
    public class InstanceServeur
    {
        private TcpClient liaisonClient;
        private int numClient;

        public InstanceServeur(TcpClient liaisonClient, int numClient)
        {
            this.liaisonClient = liaisonClient;
            this.numClient = numClient;


        }

        public void Run()
        {
            try
            {
                NetworkStream stream = liaisonClient.GetStream();

                MessageEnvoi msg = new MessageEnvoi();

                string demande = null;

                do
                {

                    if (stream.CanRead)
                    {
                        var bytes = default(byte[]);
                        var buffer = new byte[512];
                        var bytesRead = 0;

                        Crypteur crypt = new Crypteur();
                        Serial s=new Serial();

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

                            demande = s.messageString<MessageEnvoi>(c).message;

                            Console.WriteLine(demande);
                        }
                    }

                } while (demande != null);

                liaisonClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
