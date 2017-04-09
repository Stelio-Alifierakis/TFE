using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Profileur.MotDePasse;

namespace Profileur
{
    public class Utilisateur
    {
        private Profil profil;
        private string nom;
        private string prenom;
        private string login;
        private int age;
        private string motDePasse;

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

        public Utilisateur() { }

        public Utilisateur(string nom, string prenom, string login, int age)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Login = login;
        }

        public Utilisateur(string nom, string prenom, string login, int age, Profil profil)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Login = login;
            this.Profil = profil;
        }

        public Utilisateur(string nom, string prenom, string login, int age, string motDePasse)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Login = login;
            this.MotDePasse = motDePasse;
        }

        public Utilisateur(string nom, string prenom, string login, int age, Profil profil, string motDePasse)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Profil = profil;
            this.Login = login;
            this.MotDePasse = motDePasse;
        }

        public void changeProfil(Profil profil)
        {
            Profil = profil;
        }

        public bool verificationMotDePasse(string mdp)
        {
            HashCreator h = new HashCreator();
            return h.checkMotDePasse(mdp,MotDePasse);
        }
    }
}
