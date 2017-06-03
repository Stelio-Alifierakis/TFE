using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;

using Synchronisateur.Message;

namespace Synchronisateur.UDP
{
    public class UDPClient : IUDPClient
    {
        public int port { get; set; }
        private string adresse;

        public void ChangePort(int port)
        {
            this.port = port;
        }

        public void setAdresse(IPAddress adresse)
        {
            this.adresse = adresse.ToString();
        }

        public string getAdresse()
        {
            return adresse;
        }

        public UDPClient()
        {

        }

        public UDPClient(int port, IPAddress adresse)
        {
            this.port = port;
            this.adresse = adresse.ToString();
        }

        public void Envoi(string msg)
        {
            Serial s = new Serial();

            byte[] b = s.serial(msg);

            Console.WriteLine("Message envoyé");

            UdpClient udpCli = new UdpClient();

            udpCli.Send(b, b.Length, IPAddress.Broadcast.ToString(), 16043);

            udpCli.Close();
        }
    }
}
