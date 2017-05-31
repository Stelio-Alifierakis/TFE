using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Profileur;

namespace Communicateur.ComWCF
{
    public class Client : IMyCallbackService
    {
        private bool testConnexionUser = false;

        public void startTest()
        {
            new Client().Run();
        }

        public bool startAuth(string login, string mdp)
        {
            return new Client().Authentification(login, mdp);
        }

        private ISimpleService initFactory()
        {
            var factory = new DuplexChannelFactory<ISimpleService>(new InstanceContext(this), new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/SimpleService"));
            var proxy = factory.CreateChannel();
            return proxy;
        }

        public void Run()
        {
            /*var factory = new DuplexChannelFactory<ISimpleService>(new InstanceContext(this), new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/SimpleService"));
            var proxy = factory.CreateChannel();*/

            var proxy = initFactory();

            //proxy.ProcessData();
            Console.WriteLine(proxy.ProcessData());
            Console.WriteLine(proxy.test("Zut flûte crotte de bique")); //juste pour l'exemple
        }

        public void NotifyClient()
        {
            Console.WriteLine("Notification from Server"); //juste pour l'exemple
        }

        public void Notification(string testouille)
        {
            Console.WriteLine(testouille); //juste pour l'exemple
        }

        public bool Authentification(string login, string mdp)
        {
            Console.WriteLine("WCF authentification lancée");
            var proxy = initFactory();
            /*var factory = new DuplexChannelFactory<ISimpleService>(new InstanceContext(this), new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/SimpleService"));
            var proxy = factory.CreateChannel();*/
            bool test= proxy.VerifIdentifiant(login, mdp);
            Console.WriteLine(test);
            return test;
        }

        public Utilisateur UtilisateurCourant()
        {
            //throw new NotImplementedException();
            var proxy = initFactory();
            return proxy.RetourUtilisateurCourant();
        }
    }
}
