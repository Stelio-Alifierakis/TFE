using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Communicateur.ComWCF
{
    public class Client : IMyCallbackService
    {
        public void startTest()
        {
            new Client().Run();
        }

        public void startAuth(string login, string mdp)
        {
            new Client().Authentification(login, mdp);
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

        public void Authentification(string login, string mdp)
        {
            Console.WriteLine("WCF authentification lancée");
            var proxy = initFactory();
            /*var factory = new DuplexChannelFactory<ISimpleService>(new InstanceContext(this), new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/SimpleService"));
            var proxy = factory.CreateChannel();*/
            proxy.VerifIdentifiant(login, mdp);
        }
    }
}
