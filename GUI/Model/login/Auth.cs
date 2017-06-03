using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Profileur;
using Communicateur.ComWCF;

namespace GUI.Model.login
{
    public interface IAuthenticationService
    {
        Utilisateur Authentification(string login, string mdp);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public IListUtilisateur listeUser { get; set; }

        private bool AuthBon = false;

        private Utilisateur userCourant;

        private Thread th;

        public AuthenticationService()
        {
           
        }

        public Utilisateur Authentification(string login, string mdp)
        {

            /*if (listeUser==null)
            {
                th = new Thread(RetourListeUtilisateur);

                th.Start();

                if (th.IsAlive)
                {
                    th.Join();
                }
            }*/

            if (login==null)
            {
                login = "";
            }

            Authentification auth = new Authentification(login, mdp);

            th = new Thread(new ParameterizedThreadStart(StartAuth));

            th.Start(auth);

            if (th.IsAlive)
            {
                th.Join();
            }

            if (AuthBon)
            {
                return userCourant;
            }

            //throw new NotImplementedException();
            /*if (listeUser.ChangeUtilisateurEnCours(login, mdp))
            {
                return listeUser.obtientUtilisateur(login);
            }*/

            throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
        }

        #region ComServeur

        private void StartAuth(object parm)
        {
            Authentification auth = (Authentification)parm;

            Client cli = new Client();
            AuthBon = cli.startAuth(auth.login, auth.mdp);

            userCourant = cli.UtilisateurCourant();
        }

        private void RetourListeUtilisateur()
        {
            Client cli = new Client();

            listeUser=cli.RetourListeUser();
        }

        #endregion
    }
}
