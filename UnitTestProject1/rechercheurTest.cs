using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rechercheur;
using System.IO;

using BaseDonnees;
using Profileur;
using Profileur.MotDePasse;

namespace UnitTestProject1
{
    /// <summary>
    /// Classe de test pour le rechercheur
    /// </summary>
    [TestClass]
    public class rechercheurTest
    {
        /// <summary>
        /// Variable qui stocke la classe d'accès à la base de données
        /// </summary>
        DAL d;

        ListeUtilisateurs l;

        /// <summary>
        /// Variable qui stocke la BDD
        /// </summary>
        BaseDonnees.DB.db bd;

        /// <summary>
        /// Fonction qui initialise le test.
        /// Initialise la BDD et les tables.
        /// Initialise le profileur en lien avec le rechercheur
        /// </summary>
        [TestInitialize]
        public void Init_Test()
        {
            //création de la BDD de test

            bd = new BaseDonnees.DB.db("test.sqlite");
            d = new DAL(bd);
            d.creation();

            BaseDonnees.Models.ListeTheme th = new BaseDonnees.Models.ListeTheme("TestTheme");
            bd.SetListeTheme(th);

            th = new BaseDonnees.Models.ListeTheme("EssaiTheme");
            bd.SetListeTheme(th);

            BaseDonnees.Models.MotCle mc = new BaseDonnees.Models.MotCle
            {
                mot = "test",
                valeur = 15,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            BaseDonnees.Models.Synonyme syn = new BaseDonnees.Models.Synonyme
            {
                mot = "essai",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            };

            bd.SetMotCle(mc);
            bd.SetSynonyme(syn);

            mc = new BaseDonnees.Models.MotCle
            {
                mot = "Foudre",
                valeur = 15,
                fk_theme = "EssaiTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            BaseDonnees.Models.Listes liste = new BaseDonnees.Models.Listes("Liste Verte");
            BaseDonnees.Models.Sites site = new BaseDonnees.Models.Sites
            {
                nomSite = "www.facebook.com",
                DateAjout = DateTime.Now,
                fk_Date = DateTime.Now,
                fk_theme = "TestTheme",
                fk_liste = liste.liste
            };
            bd.SetListe(liste);
            bd.SetSites(site);

            liste = new BaseDonnees.Models.Listes("Liste Rouge");
            site = new BaseDonnees.Models.Sites
            {
                nomSite = "www.youporn.com",
                DateAjout = DateTime.Now,
                fk_Date = DateTime.Now,
                fk_theme = "TestTheme",
                fk_liste = liste.liste
            };
            bd.SetListe(liste);
            bd.SetSites(site);

            BaseDonnees.Models.ListeDynamique siteDyn = new BaseDonnees.Models.ListeDynamique {
                url = "http://www.youporn.com",
                DateAjout = DateTime.Now,
                fk_Date = DateTime.Now,
                fk_theme = "TestTheme"
            };

            bd.SetListeDynamique(siteDyn);

            //création des profils utilisateurs

            l = new ListeUtilisateurs();

            Profil prof = new Profil();

            prof.ajoutTheme("TestTheme");

            /*HashCreator h = new HashCreator();

            string mdpHash = h.HashMDP("test");*/

            Utilisateur user = new Utilisateur {
                Nom = "Ofaringite",
                Prenom = "Carine",
                Age = 14,
                Login="O.A",
                MotDePasse= "test"
            };


            user.Profil = prof;

            Profil prof2 = new Profil();
            prof2.ajoutTheme("EssaiTheme");
            prof2.ajoutTheme("TestTheme");

            Utilisateur user2 = new Utilisateur
            {
                Profil = prof,
                Nom = "Samson",
                Prenom = "Carole",
                Age = 18,
                Login = "S.C",
                MotDePasse = "test"
            };

            user2.Profil = prof2;

            l.AjoutUtilisateur(user);
            l.AjoutUtilisateur(user2);
        }

        /// <summary>
        /// Fonction qui ferme les testes.
        /// Supprime la BDD.
        /// </summary>
        [TestCleanup]
        public void Fermeture_Tests()
        {
            d.suppressionDB();
        }

        /// <summary>
        /// Test un mot
        /// </summary>
        [TestMethod]
        public void test_checkWord()
        {
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);
            Assert.AreEqual(true, r.checkPartWord("test"));
            Assert.AreEqual(true, r.checkPartWord("essai"));
            Assert.AreEqual(false, r.checkPartWord("carotte"));
        }

        /// <summary>
        /// Teste la valeur d'une phrase
        /// </summary>
        [TestMethod]
        public void test_returnValue()
        {
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);

            l.Ajout(r.GestUser);

            l.ChangeUtilisateurEnCours("O.A", "test");

            Assert.AreEqual(15, r.valPhrase("test"));
            Assert.AreEqual(15, r.valPhrase("essai"));
            Assert.AreEqual(225, r.valPhrase("test test"));
            Assert.AreEqual(0, r.valPhrase("carotte"));
            Assert.AreNotEqual(225, r.valPhrase("test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test"));
            Assert.AreEqual(15, r.valPhrase("test Foudre"));
            Assert.AreNotEqual(240, r.valPhrase(@"a"));

            l.ChangeUtilisateurEnCours("S.C", "test");

            Assert.AreEqual(15, r.valPhrase("test"));
            Assert.AreEqual(15, r.valPhrase("essai"));
            Assert.AreEqual(225, r.valPhrase("test test"));
            Assert.AreEqual(0, r.valPhrase("carotte"));
            Assert.AreNotEqual(225, r.valPhrase("test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test"));
            Assert.AreEqual(30, r.valPhrase("test Foudre"));
            Assert.AreNotEqual(240, r.valPhrase(@"a"));

            l.Retirer(r.GestUser);
        }

        /// <summary>
        /// Teste le thème d'une phrase
        /// </summary>
        [TestMethod]
        public void test_returnValueTheme() {
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);

            Assert.AreEqual("TestTheme", r.themePage("test"));
        }

        /// <summary>
        /// Teste si un site web est accepté, refusé ou indifférent
        /// </summary>
        [TestMethod]
        public void test_return_valeur_url()
        {
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);

            l.Ajout(r.GestUser);

            l.ChangeUtilisateurEnCours("S.C", "test");

            Assert.AreEqual(1, r.checkUrl("www.facebook.com"));
            Assert.AreEqual(0, r.checkUrl("www.youporn.com"));
            Assert.AreEqual(0, r.checkUrl("http://www.youporn.com"));
            Assert.AreEqual(1, r.checkUrl("http://www.facebook.com"));
            Assert.AreEqual(1, r.checkUrl("http://www.facebook.com/test"));
            Assert.AreEqual(2, r.checkUrl("www.youtube.com"));
            Assert.AreEqual(2, r.checkUrl("www.youpagon.fr"));
            Assert.AreEqual(2, r.checkUrl("http://www.youtube.com"));
        }

        [TestMethod]
        public void test_liste_themes_gestion_utilisateur()
        {
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);

            l.Ajout(r.GestUser);

            l.ChangeUtilisateurEnCours("S.C", "test");

            Assert.AreEqual("S.C", r.GestUser.Utilisateur.Login);
            Assert.AreEqual("EssaiTheme", r.GestUser.Utilisateur.Profil.ListeTheme[0]);
            Assert.AreEqual(true, r.GestUser.testTheme("TestTheme"));
            Assert.AreEqual(false, r.GestUser.testTheme("Fromage"));

            l.Retirer(r.GestUser);
        }
    }
}
