using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Profileur.MotDePasse;

namespace Profileur
{
    /// <summary>
    /// Modèle utilisateur
    /// </summary>
    public class Utilisateur
    {
        /// <summary>
        /// Profil de l'utilisateur
        /// </summary>
        private Profil profil;

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        private string nom;

        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        private string prenom;

        /// <summary>
        /// Login de l'utilisateur
        /// </summary>
        private string login;

        /// <summary>
        /// Age de l'utilisateur
        /// </summary>
        private int age;

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        private string motDePasse;

        /// <summary>
        /// Getter et setter du profil de l'utilisateur
        /// </summary>
        public Profil Profil
        {
            get
            {
                return profil;
            }

            set
            {
                profil = value;
            }
        }

        /// <summary>
        /// Getter et setter du nom
        /// </summary>
        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        /// <summary>
        /// Getter et setter du prénom
        /// </summary>
        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                prenom = value;
            }
        }

        /// <summary>
        /// Getter et setter de l'âge
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        /// <summary>
        /// Getter et setter du login
        /// </summary>
        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
            }
        }

        /// <summary>
        /// Getter et setter du mot de passe
        /// </summary>
        public string MotDePasse
        {
            get
            {
                return motDePasse;
            }
            set
            {
                HashCreator h = new HashCreator();
                motDePasse =h.HashMDP(value);
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Utilisateur() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prénom de l'utilisateur</param>
        /// <param name="login">Login de l'utilisateur</param>
        /// <param name="age">Age de l'utilisateur</param>
        public Utilisateur(string nom, string prenom, string login, int age)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Login = login;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prénom de l'utilisateur</param>
        /// <param name="login">Login de l'utilisateur</param>
        /// <param name="age">Age de l'utilisateur</param>
        /// <param name="profil">Profil de l'utilisateur</param>
        public Utilisateur(string nom, string prenom, string login, int age, Profil profil)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Login = login;
            this.Profil = profil;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prénom de l'utilisateur</param>
        /// <param name="login">Login de l'utilisateur</param>
        /// <param name="age">Age de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        public Utilisateur(string nom, string prenom, string login, int age, string motDePasse)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Login = login;
            this.MotDePasse = motDePasse;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prénom de l'utilisateur</param>
        /// <param name="login">Login de l'utilisateur</param>
        /// <param name="age">Age de l'utilisateur</param>
        /// <param name="profil"> Profil de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        public Utilisateur(string nom, string prenom, string login, int age, Profil profil, string motDePasse)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Profil = profil;
            this.Login = login;
            this.MotDePasse = motDePasse;
        }

        /// <summary>
        /// Constructeur.
        /// Ce constructeur permet de différencier les anciens des nouveaux utilisateurs instancié.
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prénom de l'utilisateur</param>
        /// <param name="login">Login de l'utilisateur</param>
        /// <param name="age">Age de l'utilisateur</param>
        /// <param name="profil">Profil de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        /// <param name="existe">Indique si le mot de passe existe déjà ou pas</param>
        public Utilisateur(string nom, string prenom, string login, int age, Profil profil, string motDePasse, bool existe)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Profil = profil;
            this.Login = login;
            if (existe)
            {
                this.motDePasse = motDePasse;
            }
            else
            {
                this.MotDePasse = motDePasse;
            }
        }

        /// <summary>
        /// Fonction qui permet de changer le profil d'un utilisateur
        /// </summary>
        /// <param name="profil">Nouveau profil</param>
        public void changeProfil(Profil profil)
        {
            Profil = profil;
        }

        /// <summary>
        /// Fonction qui va vérifier le mot de passe d'un utilisateur
        /// </summary>
        /// <param name="mdp">Le mot de passe entré</param>
        /// <returns>Retourne vrai ou faux si le mot de passe correspond bien à ce qui est stocké en mémoire</returns>
        public bool verificationMotDePasse(string mdp)
        {
            HashCreator h = new HashCreator();
            return h.checkMotDePasse(mdp,MotDePasse);
        }
    }
}