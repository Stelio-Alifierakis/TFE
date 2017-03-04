using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProxyNavigateur;
using Moq;
using Rechercheur;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class rechercheurTest
    {
        DAL d;
        ProxyNavigateur.DB.db bd;

        [TestInitialize]
        public void Init_Test()
        {
            bd = new ProxyNavigateur.DB.db("test.sqlite");
            d = new DAL(bd);
            d.creation();

            ProxyNavigateur.Models.ListeTheme th = new ProxyNavigateur.Models.ListeTheme("TestTheme");
            bd.SetListeTheme(th);
            ProxyNavigateur.Models.MotCle mc = new ProxyNavigateur.Models.MotCle
            {
                mot = "test",
                valeur = 15,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new ProxyNavigateur.Models.Synonyme()
            };

            ProxyNavigateur.Models.Synonyme syn = new ProxyNavigateur.Models.Synonyme
            {
                mot = "essai",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            };

            bd.SetMotCle(mc);
            bd.SetSynonyme(syn);
        }

        [TestCleanup]
        public void Fermeture_Tests()
        {
            d.suppressionDB();
        }

        [TestMethod]
        public void test_checkWord()
        {
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);
            Assert.AreEqual(true, r.checkPartWord("test"));
            Assert.AreEqual(true, r.checkPartWord("essai"));
            Assert.AreEqual(false, r.checkPartWord("carotte"));
        }

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
    }
}
