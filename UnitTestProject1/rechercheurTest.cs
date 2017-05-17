using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDonnees;
using Moq;
using Rechercheur;
using System.IO;

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

        /// <summary>
        /// Variable qui stocke la BDD
        /// </summary>
        BaseDonnees.DB.db bd;

        /// <summary>
        /// Fonction qui initialise le test.
        /// Initialise la BDD et les tables.
        /// </summary>
        [TestInitialize]
        public void Init_Test()
        {
            bd = new BaseDonnees.DB.db("test.sqlite");
            d = new DAL(bd);
            d.creation();

            BaseDonnees.Models.ListeTheme th = new BaseDonnees.Models.ListeTheme("TestTheme");
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

            Assert.AreEqual(15, r.valPhrase("test"));
            Assert.AreEqual(15, r.valPhrase("essai"));
            Assert.AreEqual(225, r.valPhrase("test test"));
            Assert.AreEqual(0, r.valPhrase("carotte"));
            Assert.AreNotEqual(225, r.valPhrase("test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test"));
            Assert.AreNotEqual(240, r.valPhrase(@"a"));
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

            Assert.AreEqual(1, r.checkUrl("www.facebook.com"));
            Assert.AreEqual(0, r.checkUrl("www.youporn.com"));
            Assert.AreEqual(0, r.checkUrl("http://www.youporn.com"));
            Assert.AreEqual(1, r.checkUrl("http://www.facebook.com"));
            Assert.AreEqual(1, r.checkUrl("http://www.facebook.com/test"));
            Assert.AreEqual(2, r.checkUrl("www.youtube.com"));
            Assert.AreEqual(2, r.checkUrl("www.youpagon.fr"));
            Assert.AreEqual(2, r.checkUrl("http://www.youtube.com"));
        }
    }
}
