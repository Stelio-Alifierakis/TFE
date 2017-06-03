using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using Profileur;

namespace Communicateur.ComWCF
{
    public interface IMyCallbackService
    {
        [OperationContract(IsOneWay = true)]
        void NotifyClient();

        [OperationContract(IsOneWay = true)]
        void Notification(string testouille);

        [OperationContract]
        Utilisateur UtilisateurCourant();

        [OperationContract]
        ListeUtilisateurs RetourListeUser();
    }

    // Define your service contract and specify the callback contract
    [ServiceContract(CallbackContract = typeof(IMyCallbackService))]
    public interface ISimpleService
    {
        [OperationContract]
        string ProcessData();

        [OperationContract]
        string test(string testouille);

        [OperationContract]
        bool VerifIdentifiant(string login, string mdp);

        [OperationContract]
        Utilisateur RetourUtilisateurCourant();

        [OperationContract]
        ListeUtilisateurs RetourListeUser();
    }
}
