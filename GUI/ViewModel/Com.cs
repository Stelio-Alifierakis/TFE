using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Communicateur.ComWCF;
using GUI.Model;

namespace GUI.ViewModel
{
    public class Com
    {
        private Thread th;

        public void testCom()
        {
            th = new Thread(Run);
            th.Start();
            
        }

        public void LancementAuth(string login, string mdp)
        {
            Authentification auth = new Authentification(login, mdp);

            th = new Thread(new ParameterizedThreadStart(RunAuth));
            th.Start(auth);
        }

        private void Run()
        {
            Client cli = new Client();

            cli.startTest();
            
        }

        private void RunAuth(object parm)
        {
            Authentification auth = (Authentification)parm;

            Client cli = new Client();
            //cli.startTest();
            cli.startAuth(auth.login, auth.mdp);
            
        }
    }
}
