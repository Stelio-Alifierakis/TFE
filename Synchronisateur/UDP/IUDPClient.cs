using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Synchronisateur.UDP
{
    public interface IUDPClient
    {
        void setAdresse(IPAddress adresse);
        string getAdresse();
        void ChangePort(int port);
        void Envoi(string msg);
    }
}
