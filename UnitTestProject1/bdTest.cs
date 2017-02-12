using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProxyNavigateur;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class bdTest
    {
        [TestMethod]
        public void Creation_BDD_sqlite()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetListe(new ProxyNavigateur.Models.Listes { liste="test" });
            List <ProxyNavigateur.Models.Listes> l = bd.GetListes().ToList();
            Assert.IsNotNull(l);
            Assert.AreEqual("test",l[0].liste);
            Assert.AreEqual(1,l.Count);
            bd.suppressionDB();
        }

        [TestMethod]
        public void Test_Get_Liste()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetListe(new ProxyNavigateur.Models.Listes { liste = "test" });
            ProxyNavigateur.Models.Listes l = bd.GetListes("test");
            Assert.IsNotNull(l);
            Assert.AreEqual("test", l.liste);
            bd.suppressionDB();

        }

        [TestMethod]
        public void Test_Get_ListeDynamique()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetListeDynamique(new ProxyNavigateur.Models.ListeDynamique {
                url ="testUrl",
                fk_theme ="testTheme",
                DateAjout =DateTime.Today,
                fk_Date =DateTime.Today
            });
            ProxyNavigateur.Models.ListeDynamique dyn = bd.GetListeDynamique("testUrl");
            Assert.IsNotNull(dyn);
            Assert.AreEqual("testUrl", dyn.url);
            Assert.AreEqual("testTheme", dyn.fk_theme);
            Assert.AreEqual(DateTime.Today, dyn.DateAjout);
            Assert.AreEqual(DateTime.Today, dyn.fk_Date);
            bd.suppressionDB();

        }

        [TestMethod]
        public void Test_Get_ListeTheme()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetListeTheme(new ProxyNavigateur.Models.ListeTheme { theme = "testTheme" });
            ProxyNavigateur.Models.ListeTheme lt = bd.GetTheme("testTheme");
            Assert.IsNotNull(lt);
            Assert.AreEqual("testTheme", lt.theme);
            bd.suppressionDB();

        }

        [TestMethod]
        public void Test_Get_MotCle()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetMotCle(new ProxyNavigateur.Models.MotCle {
                mot ="test",
                valeur =15,
                fk_theme="TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today
            });

            ProxyNavigateur.Models.MotCle mc = bd.GetMotCle("test");
            Assert.IsNotNull(mc);
            Assert.AreEqual("test", mc.mot);
            Assert.AreEqual(15, mc.valeur);
            Assert.AreEqual("TestTheme", mc.fk_theme);
            Assert.AreEqual(DateTime.Today, mc.DateAjout);
            Assert.AreEqual(DateTime.Today, mc.fk_Date);
            bd.suppressionDB();
        }

        [TestMethod]
        public void Test_Get_Site()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();

            bd.SetSites(new ProxyNavigateur.Models.Sites {
                nomSite ="testSite",
                fk_theme ="testTheme",
                fk_liste="testListe",
                DateAjout= DateTime.Today,
                fk_Date= DateTime.Today
            });

            ProxyNavigateur.Models.Sites s = bd.GetSite("testSite");
            Assert.IsNotNull(s);
            Assert.AreEqual("testSite", s.nomSite);
            Assert.AreEqual("testTheme", s.fk_theme);
            Assert.AreEqual("testListe", s.fk_liste);
            Assert.AreEqual(DateTime.Today, s.DateAjout);
            Assert.AreEqual(DateTime.Today, s.fk_Date);
            bd.suppressionDB();

        }

        [TestMethod]
        public void Test_Get_Synchro()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetSynchro(new ProxyNavigateur.Models.Synchronisation { date=DateTime.Today });
            ProxyNavigateur.Models.Synchronisation syn = bd.GetSynchro(DateTime.Today);
            Assert.IsNotNull(syn);
            Assert.AreEqual(DateTime.Today, syn.date);
            bd.suppressionDB();

        }

        [TestMethod]
        public void Test_Get_Synonyme()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetSynonyme(new ProxyNavigateur.Models.Synonyme {
                mot="testSynonyme",
                DateAjout= DateTime.Today,
                fk_Date= DateTime.Today,
                fk_trad="testTrad"
            });
            ProxyNavigateur.Models.Synonyme syn = bd.GetSynonyme("testSynonyme");
            Assert.IsNotNull(syn);
            Assert.AreEqual("testSynonyme", syn.mot);
            Assert.AreEqual("testTrad", syn.fk_trad);
            Assert.AreEqual(DateTime.Today, syn.DateAjout);
            Assert.AreEqual(DateTime.Today, syn.fk_Date);
            bd.suppressionDB();

        }

        [TestMethod]
        public void Test_Get_Topologie()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.SetTopologie(new ProxyNavigateur.Models.Topologie {
                idMachine="testId",
                fk_Date =DateTime.Today
            });
            ProxyNavigateur.Models.Topologie topo = bd.GetTopo("testId");
            Assert.IsNotNull(topo);
            Assert.AreEqual("testId", topo.idMachine);
            Assert.AreEqual(DateTime.Today, topo.fk_Date);
            bd.suppressionDB();

        }

        [TestMethod]
        public void Seeder_test()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();
            bd.seeder();

            List<ProxyNavigateur.Models.Listes> l = bd.GetListes().ToList();
            List<ProxyNavigateur.Models.ListeDynamique> ld = bd.GetListeDynamiques().ToList();
            List<ProxyNavigateur.Models.ListeTheme> lt = bd.GetListeThemes().ToList();
            List<ProxyNavigateur.Models.MotCle> m = bd.GetMotCles().ToList();
            List<ProxyNavigateur.Models.Synonyme> syn= bd.GetSynonymes().ToList();
            List<ProxyNavigateur.Models.Topologie> topo = bd.GetTopos().ToList();
            List<ProxyNavigateur.Models.Sites> sites = bd.GetSites().ToList();

            Assert.IsNotNull(l);
            Assert.AreEqual("Liste Verte", l[0].liste);
            Assert.AreEqual("Liste Rouge", l[1].liste);
            Assert.AreEqual(2, l.Count);

            Assert.IsNotNull(ld);
            Assert.AreEqual("www.youtube.com", ld[0].url);
            Assert.AreEqual("Approprie", ld[0].fk_theme);
            Assert.AreEqual("www.sex.com", ld[1].url);
            Assert.AreEqual("Pornographie", ld[1].fk_theme);
            Assert.AreEqual(2, ld.Count);

            Assert.IsNotNull(lt);
            Assert.AreEqual("Approprie", lt[0].theme);
            Assert.AreEqual("Pornographie", lt[1].theme);
            Assert.AreEqual(2, lt.Count);

            Assert.IsNotNull(m);
            Assert.AreEqual("sex", m[0].mot);
            Assert.AreEqual(4, m[0].valeur);
            Assert.AreEqual("Pornographie", m[0].fk_theme);
            Assert.AreEqual("porn", m[1].mot);
            Assert.AreEqual(15, m[1].valeur);
            Assert.AreEqual("Pornographie", m[1].fk_theme);
            Assert.AreEqual(2, m.Count);

            Assert.IsNotNull(syn);
            Assert.AreEqual("sexe", syn[0].mot);
            Assert.AreEqual("sex", syn[0].fk_trad);
            Assert.AreEqual("seks", syn[1].mot);
            Assert.AreEqual("sex", syn[1].fk_trad);
            Assert.AreEqual(2, syn.Count);

            Assert.IsNotNull(topo);
            Assert.AreEqual("000001", topo[0].idMachine);
            Assert.AreEqual(1, topo.Count);

            Assert.IsNotNull(sites);
            Assert.AreEqual("www.openclassroom.com", sites[0].nomSite);
            Assert.AreEqual("Approprie", sites[0].fk_theme);
            Assert.AreEqual("Liste Verte", sites[0].fk_liste);
            Assert.AreEqual("www.youporn.com", sites[1].nomSite);
            Assert.AreEqual("Pornographie", sites[1].fk_theme);
            Assert.AreEqual("Liste Rouge", sites[1].fk_liste);
            Assert.AreEqual(2, sites.Count);

            bd.suppressionDB();
        }

        [TestMethod]
        public void Test_verifMot()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();

            bd.SetSites(new ProxyNavigateur.Models.Sites
            {
                nomSite = "testSite",
                fk_theme = "testTheme",
                fk_liste = "testListe",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today
            });

            Assert.AreEqual(true, bd.verifSite("testSite"));
            Assert.AreEqual(false, bd.verifSite("test"));

            bd.suppressionDB();
        }

        [TestMethod]
        public void Test_checkPartWord()
        {
            ProxyNavigateur.DB.db bd = new ProxyNavigateur.DB.db("test.sqlite");
            bd.creationTables();

            bd.SetMotCle(new ProxyNavigateur.Models.MotCle
            {
                mot = "test",
                valeur = 15,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today
            });
            bd.SetMotCle(new ProxyNavigateur.Models.MotCle
            {
                mot = "carotte",
                valeur = 7,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today
            });
            bd.SetMotCle(new ProxyNavigateur.Models.MotCle
            {
                mot = "grand",
                valeur = 2,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today
            });

            bd.SetSynonyme(new ProxyNavigateur.Models.Synonyme
            {
                mot = "testSynonyme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = "test"
            });
            bd.SetSynonyme(new ProxyNavigateur.Models.Synonyme
            {
                mot = "beurre",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = "carotte"
            });



            //Assert.AreEqual(true, bd.verifSite("je veux du beurre"));
            //Assert.AreEqual(true, bd.verifSite("test ton code"));
            //Assert.AreEqual(true, bd.verifSite("carotte tomate"));
            //Assert.AreEqual(true, bd.verifSite("j'ai testSynonyme"));
            //Assert.AreEqual(true, bd.verifSite("je suis grand"));
            //Assert.AreEqual(true, bd.verifSite("petit Test"));
            Assert.AreEqual(false, bd.verifSite("ici se trouve une phrase au hasard"));

            bd.suppressionDB();
        }
    }
}
