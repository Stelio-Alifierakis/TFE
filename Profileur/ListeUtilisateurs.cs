using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Profileur.Observateur;
using System.Runtime.Serialization;

namespace Profileur
{
    /// <summary>
    /// Classe qui sert à stocker la liste des utilisateurs
    /// </summary>
    [DataContract]
    public class ListeUtilisateurs : AbstractListUtilisateurs
    {
        /// <summary>
        /// Liste des utilisateurs
        /// </summary>
        private List<Utilisateur> listUtilisateur;

        /// <summary>
        /// Constructeur
        /// </summary>
        public ListeUtilisateurs()
        {
            //listUtilisateur = new List<Utilisateur>();
            listUtilisateur = initListe();
        }

        /// <summary>
        /// Fonction qui va lire les fichiers d'utilisateurs et qui va les mettre dans une liste d'utilisateur
        /// </summary>
        /// <returns>Liste d'utilisateurs</returns>
        public override List<Utilisateur> initListe()
        {
            //throw new NotImplementedException();
            List<Utilisateur> liste = new List<Utilisateur>();

            /*
                Sert pour le test
            */
            Profil prof = new Profil(true);
            prof.ajoutTheme("Pornographie");

            Utilisateur Defaut = new Utilisateur
            {
                Profil = prof,
                Nom = "Default",
                Prenom = "Default",
                Age = 14,
                Login = "",
                MotDePasse = ""
            };

            Utilisateur user = new Utilisateur
            {
                Profil = prof,
                Nom = "Bolivar",
                Prenom = "Ernesto",
                Age = 14,
                Login = "B.E",
                MotDePasse = "test"
            };

            Profil prof2 = new Profil();
            prof2.ajoutTheme("Pornographie");
            prof2.ajoutTheme("Violence");

            Utilisateur user2 = new Utilisateur
            {
                Profil = prof2,
                Nom = "Bon",
                Prenom = "Jean",
                Age = 18,
                Login = "B.J",
                MotDePasse = "test"
            };

            liste.Add(Defaut);
            liste.Add(user);
            liste.Add(user2);

            /*
                Fin de la zone de texte
            */

            return liste;
        }

        /// <summary>
        /// Getter et setter de la liste des utilisateurs
        /// </summary>
        [DataMember]
        public List<Utilisateur> ListUtilisateur
        {
            get
            {
                return listUtilisateur;
            }

            set
            {
                listUtilisateur = value;
            }
        }



        /// <summary>
        /// Ajoute un utilisateur dans la liste
        /// </summary>
        /// <param name="user">Utilisateur</param>
        public override void AjoutUtilisateur(Utilisateur user){
            ListUtilisateur.Add(user);
       }

        /// <summary>
        /// Supprime un utilisateur à un index donné
        /// </summary>
        /// <param name="id">Index</param>
        private void RetirerUtilisateur(int id)
        {
            ListUtilisateur.RemoveAt(id);
        }

        /// <summary>
        /// Retire un utilisateur dont on connait le nom
        /// </summary>
        /// <param name="nomUtilisateur">Nom de l'utilisateur</param>
        public override void RetirerUtilisateur(string nomUtilisateur)
        {
            for (int i=0; i<=ListUtilisateur.Count; i++)
            {
                if (ListUtilisateur[i].Login==nomUtilisateur)
                {
                    RetirerUtilisateur(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Fonction qui va changer l'utilisateur en cours (utilisé pour l'authentification)
        /// </summary>
        /// <param name="login">Identifiant</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns>Valeur booléenne pour savoir si l'opération a réussi ou échoué</returns>
        public override bool ChangeUtilisateurEnCours(string login, string mdp)
        {
            //throw new NotImplementedException();

            Utilisateur utilisateurCourant = obtientUtilisateur(login);

            if (utilisateurCourant!=null && utilisateurCourant.verificationMotDePasse(mdp))
            {
                base.Notifier(utilisateurCourant);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Fonction qui va récupérer l'utilisateur dont on connait le login depuis la liste des utilisateurs
        /// </summary>
        /// <param name="login">Login demandé</param>
        /// <returns>Utilisateur demandé</returns>
        public override Utilisateur obtientUtilisateur(string login)
        {
            Utilisateur utilisateurCourant = new Utilisateur();

            foreach (Utilisateur u in listUtilisateur)
            {
                if (u.Login==login)
                {
                    utilisateurCourant = u;
                    //base.Notifier(utilisateurCourant);
                    break;
                }
            }

            return utilisateurCourant;
        }
    }
}
