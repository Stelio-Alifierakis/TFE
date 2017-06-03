using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Synchronisateur.UDP;
using BaseDonnees;

namespace Synchronisateur
{
    public class ComSynchro : IComSynchro
    {
        IUDPClient clientUDP;
        IUDPServeur serveurUDP;
        string IDProg;

        public ComSynchro(IDAL bdd)
        {
            clientUDP = new UDPClient();
            serveurUDP = new UDPServeur(bdd);
        }

        public void Start(string IDProg)
        {
            this.IDProg = IDProg;
            //throw new NotImplementedException();
            clientUDP.setAdresse(IPAddress.Broadcast);
            clientUDP.ChangePort(16043);
            serveurUDP.Start();

            clientUDP.Envoi(this.IDProg);
        }

        public void Stop()
        {
            serveurUDP.Stop();
            //throw new NotImplementedException();
        }
    }
}
