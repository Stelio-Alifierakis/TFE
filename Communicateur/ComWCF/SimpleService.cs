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

        public bool testConnexionUser { get; set; }

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

            testConnexionUser = false;
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


        public bool VerifIdentifiant(string login, string mdp){

            var callback = OperationContext.Current.GetCallbackChannel<IMyCallbackService>();
            Console.WriteLine("WCF authentification lancée");
            testConnexionUser = ListUserCom.ChangeUtilisateurEnCours(login, mdp);

            if (testConnexionUser)
            {
                UtilisateurCom = ListUserCom.obtientUtilisateur(login);
                proxyCommunication.setUtilisateur(UtilisateurCom);
            }

            return testConnexionUser;
            //callback.Notification("test");
            //ListUserCom.verifCom(login, mdp);
        }

        public Utilisateur RetourUtilisateurCourant()
        {
            //throw new NotImplementedException();
            return UtilisateurCom;
        }

        public ListeUtilisateurs RetourListeUser()
        {
            Console.WriteLine("La demande de la liste d'utilisateur a été effectuée");
            //throw new NotImplementedException();

            ListeUtilisateurs l = (ListeUtilisateurs)ListUserCom;

            Console.WriteLine(l.ListUtilisateur[0].MotDePasse);

            return (ListeUtilisateurs)ListUserCom;
        }


        public bool retourTestUser()
        {
            //throw new NotImplementedException();

            return testConnexionUser;
        }
    }
}
