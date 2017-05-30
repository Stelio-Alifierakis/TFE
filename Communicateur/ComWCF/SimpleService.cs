using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using Communicateur.ComProxy;
using proxy;
using Profileur;

namespace Communicateur.ComWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SimpleService : ISimpleService
    {

        private static IProxyCom proxyCommunication;

        private Activitateur ActifParUrl;
        private Activitateur ActifParContenu;
        private Utilisateur UtilisateurCom;
        private IListUtilisateur ListUserCom;

        public static void AjoutProxyCom(IProxyCom proxCom)
        {
            proxyCommunication = proxCom;
        }

        public SimpleService()
        {
            ActifParContenu = proxyCommunication.RetourActiveParContenu();
            ActifParUrl = proxyCommunication.RetourActiveParURL();
            UtilisateurCom = proxyCommunication.RetourUtilisateur();
            ListUserCom = proxyCommunication.RetourListeUtilisateur();
        }

        public string ProcessData()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IMyCallbackService>();
            //Console.WriteLine("retest");
            callback.NotifyClient();
            return DateTime.Now.ToString();
        }

        public string test(string testouille)
        {
            //throw new NotImplementedException();
            var callback = OperationContext.Current.GetCallbackChannel<IMyCallbackService>();
            Console.WriteLine("testouille");
            Console.WriteLine("retest");
            callback.Notification(testouille);
            return testouille;
        }

        public void VerifIdentifiant(string login, string mdp){

            var callback = OperationContext.Current.GetCallbackChannel<IMyCallbackService>();
            Console.WriteLine("WCF authentification lancée");
            Console.WriteLine(ListUserCom.ChangeUtilisateurEnCours(login, mdp));
            callback.Notification("test");
            //ListUserCom.verifCom(login, mdp);
        }
    }
}
