using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BaseDonnees;
using BaseDonnees.Models;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Dapper;
using System.Linq;

namespace UnitTestProject1
{
    /// <summary>
    /// Classe de teste qui va tester les classes et fonctions relatives à la base de données
    /// </summary>
    [TestClass]
    public class bdTest
    {
        BaseDonnees.DB.db bd;

        /// <summary>
        /// Initialise les test en créant la BDD sqlite de test et en créant les différentes tables.
        /// </summary>
       [TestInitialize]
        public void Init_Test()
        {
            if (File.Exists("test.sqlite"))
            {
                try
                {
                    File.Delete("test.sqlite");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            bd = new BaseDonnees.DB.db("test.sqlite");
            bd.creationTables();
        }

        /// <summary>
        /// Fonction qui se lance à la fin des test. Elle va supprimer le fichier de BDD.
        /// </summary>
        [TestCleanup]
        public void Fermeture_Tests()
        {
            bd.suppressionDB();
        }

        /// <summary>
        /// Teste la création des différentes tables
        /// </summary>
        [TestMethod]
        public void Creation_BDD_sqlite()
        {
            
            bd.SetListe(new BaseDonnees.Models.Listes { liste="test" });
            List <BaseDonnees.Models.Listes> l = bd.GetListes().ToList();
            Assert.IsNotNull(l);
            Assert.AreEqual("test",l[0].liste);
            Assert.AreEqual(1,l.Count);            
        }

        /// <summary>
        /// Teste l'obtention d'une liste
        /// </summary>
        [TestMethod]
        public void Test_Get_Liste()
        {
            bd.SetListe(new BaseDonnees.Models.Listes { liste = "test" });
            BaseDonnees.Models.Listes l = bd.GetListes("test");
            Assert.IsNotNull(l);
            Assert.AreEqual("test", l.liste);
        }

        /// <summary>
        /// Testes l'obtention d'un site dynamique
        /// </summary>
        [TestMethod]
        public void Test_Get_ListeDynamique()
        {
            bd.SetListeDynamique(new BaseDonnees.Models.ListeDynamique {
                url ="testUrl",
                fk_theme ="testTheme",
                DateAjout =DateTime.Today,
                fk_Date =DateTime.Today
            });
            BaseDonnees.Models.ListeDynamique dyn = bd.GetListeDynamique("testUrl");
            Assert.IsNotNull(dyn);
            Assert.AreEqual("testUrl", dyn.url);
            Assert.AreEqual("testTheme", dyn.fk_theme);
            Assert.AreEqual(DateTime.Today, dyn.DateAjout);
            Assert.AreEqual(DateTime.Today, dyn.fk_Date);
        }

        /// <summary>
        /// Teste l'obtention d'un thème
        /// </summary>
        [TestMethod]
        public void Test_Get_ListeTheme()
        {
            bd.SetListeTheme(new BaseDonnees.Models.ListeTheme { theme = "testTheme" });
            BaseDonnees.Models.ListeTheme lt = bd.GetTheme("testTheme");
            Assert.IsNotNull(lt);
            Assert.AreEqual("testTheme", lt.theme);
        }

        /// <summary>
        /// Teste l'obtention d'un mot-clé
        /// </summary>
        [TestMethod]
        public void Test_Get_MotCle()
        {
            bd.SetMotCle(new BaseDonnees.Models.MotCle {
                mot ="test",
                valeur =15,
                fk_theme="TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today
            });

            BaseDonnees.Models.MotCle mc = bd.GetMotCle("test");
            Assert.IsNotNull(mc);
            Assert.AreEqual("test", mc.mot);
            Assert.AreEqual(15, mc.valeur);
            Assert.AreEqual("TestTheme", mc.fk_theme);
            Assert.AreEqual(DateTime.Today, mc.DateAjout);
            Assert.AreEqual(DateTime.Today, mc.fk_Date);
        }

        /// <summary>
        /// Teste l'obtention d'un site
        /// </summary>
        [TestMethod]
        public void Test_Get_Site()
        {
            bd.SetSites(new BaseDonnees.Models.Sites {
                nomSite ="testSite",
                fk_theme ="testTheme",
                fk_liste="testListe",
                DateAjout= DateTime.Today,
                fk_Date= DateTime.Today
            });

            BaseDonnees.Models.Sites s = bd.GetSite("testSite");
            Assert.IsNotNull(s);
            Assert.AreEqual("testSite", s.nomSite);
            Assert.AreEqual("testTheme", s.fk_theme);
            Assert.AreEqual("testListe", s.fk_liste);
            Assert.AreEqual(DateTime.Today, s.DateAjout);
            Assert.AreEqual(DateTime.Today, s.fk_Date);
        }

        /// <summary>
        /// Teste l'obtention d'une synchronisation
        /// </summary>
        [TestMethod]
        public void Test_Get_Synchro()
        {
            bd.SetSynchro(new BaseDonnees.Models.Synchronisation { date=DateTime.Today });
            BaseDonnees.Models.Synchronisation syn = bd.GetSynchro(DateTime.Today);
            Assert.IsNotNull(syn);
            Assert.AreEqual(DateTime.Today, syn.date);
        }

        /// <summary>
        /// Teste l'obtention d'un synonyme
        /// </summary>
        [TestMethod]
        public void Test_Get_Synonyme()
        {
            bd.SetSynonyme(new BaseDonnees.Models.Synonyme {
                mot="testSynonyme",
                DateAjout= DateTime.Today,
                fk_Date= DateTime.Today,
                fk_trad="testTrad"
            });
            BaseDonnees.Models.Synonyme syn = bd.GetSynonyme("testSynonyme");
            Assert.IsNotNull(syn);
            Assert.AreEqual("testSynonyme", syn.mot);
            Assert.AreEqual("testTrad", syn.fk_trad);
            Assert.AreEqual(DateTime.Today, syn.DateAjout);
            Assert.AreEqual(DateTime.Today, syn.fk_Date);
        }

        /// <summary>
        /// Teste l'obtention d'une topologie
        /// </summary>
        [TestMethod]
        public void Test_Get_Topologie()
        {
            bd.SetTopologie(new BaseDonnees.Models.Topologie {
                idMachine="testId",
                fk_Date =DateTime.Today
            });
            BaseDonnees.Models.Topologie topo = bd.GetTopo("testId");
            Assert.IsNotNull(topo);
            Assert.AreEqual("testId", topo.idMachine);
            Assert.AreEqual(DateTime.Today, topo.fk_Date);
        }

        /// <summary>
        /// Teste le seeder de la BDD
        /// </summary>
        [TestMethod]
        public void Seeder_test()
        {
            bd.seeder();

            List<BaseDonnees.Models.Listes> l = bd.GetListes().ToList();
            List<BaseDonnees.Models.ListeDynamique> ld = bd.GetListeDynamiques().ToList();
            List<BaseDonnees.Models.ListeTheme> lt = bd.GetListeThemes().ToList();
            List<BaseDonnees.Models.MotCle> m = bd.GetMotCles().ToList();
            List<BaseDonnees.Models.Synonyme> syn= bd.GetSynonymes().ToList();
            List<BaseDonnees.Models.Topologie> topo = bd.GetTopos().ToList();
            List<BaseDonnees.Models.Sites> sites = bd.GetSites().ToList();

            Assert.IsNotNull(l);
            /*Assert.AreEqual("Liste Verte", l[0].liste);
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
            Assert.AreEqual(2, sites.Count);*/
        }

        /// <summary>
        /// Teste l'obtention des sites d'une liste donnée
        /// </summary>
        [TestMethod]
        public void getURL_test()
        {
            bd.seeder();

            List<Sites> l1 = bd.getURL("Liste Verte").ToList();
            List<Sites> l2 = bd.getURL("Liste Rouge").ToList();

            Assert.IsNotNull(l1);
            Assert.IsNotNull(l2);

            Assert.AreEqual("www.openclassroom.com",l1[0].nomSite);
            Assert.AreEqual("www.youporn.com", l2[0].nomSite);
        }

        /// <summary>
        /// Teste l'obtention des sites d'une liste donnée (via le DAL)
        /// </summary>
        [TestMethod]
        public void getURLDAL_test()
        {
            bd.seeder();

            DAL d = new DAL(bd);

            List<Sites> l1 = d.retourSites("Liste Verte");
            List<Sites> l2 = d.retourSites("Liste Rouge");
            List<Sites> l3 = d.retourSites("Carotte");

            Assert.IsNotNull(l1);
            Assert.IsNotNull(l2);
            Assert.IsNull(l3);


            Assert.AreEqual("www.openclassroom.com", l1[0].nomSite);
            Assert.AreEqual("www.youporn.com", l2[0].nomSite);
        }

        /// <summary>
        /// Teste l'existence d'un site
        /// </summary>
        [TestMethod]
        public void Test_verifMot()
        {
            bd.SetSites(new BaseDonnees.Models.Sites
            {
                nomSite = "testSite",
                fk_theme = "testTheme",
                fk_liste = "testListe",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today
            });

            Assert.AreEqual(true, bd.verifSite("testSite"));
            Assert.AreEqual(false, bd.verifSite("test"));
        }

        /// <summary>
        /// Vérifie l'existence d'un site
        /// </summary>
        [TestMethod]
        public void Test_VerifSite()
        {

            BaseDonnees.Models.MotCle mc = new BaseDonnees.Models.MotCle
            {
                mot = "test",
                valeur = 15,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            bd.SetSynonyme(new BaseDonnees.Models.Synonyme
            {
                mot = "testSynonyme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            });

            mc = new BaseDonnees.Models.MotCle
            {
                mot = "carotte",
                valeur = 7,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            bd.SetSynonyme(new BaseDonnees.Models.Synonyme
            {
                mot = "beurre",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            });

            mc = new BaseDonnees.Models.MotCle
            {
                mot = "grand",
                valeur = 2,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);


            //Assert.AreEqual(true, bd.verifSite("je veux du beurre"));
            Assert.AreEqual(true, bd.verifSite("test"));
            //Assert.AreEqual(true, bd.verifSite("carotte tomate"));
            //Assert.AreEqual(true, bd.verifSite("j'ai testSynonyme"));
            //Assert.AreEqual(true, bd.verifSite("je suis grand"));
            //Assert.AreEqual(false, bd.verifSite("petit Test"));
            Assert.AreEqual(false, bd.verifSite("ici se trouve une phrase au hasard"));
        }

        /// <summary>
        /// Vérifie l'existence d'un mot dans une phrase
        /// </summary>
        [TestMethod]
        public void test_bdd_CheckPartWord()
        {
            BaseDonnees.Models.MotCle mc = new BaseDonnees.Models.MotCle
            {
                mot = "test",
                valeur = 15,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            bd.SetSynonyme(new BaseDonnees.Models.Synonyme
            {
                mot = "testSynonyme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            });

            mc = new BaseDonnees.Models.MotCle
            {
                mot = "carotte",
                valeur = 7,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            bd.SetSynonyme(new BaseDonnees.Models.Synonyme
            {
                mot = "beurre",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            });

            mc = new BaseDonnees.Models.MotCle
            {
                mot = "grand",
                valeur = 2,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            Assert.AreEqual(true, bd.checkPartWord("je veux du beurre"));
            Assert.AreEqual(true, bd.checkPartWord("petit test"));
            Assert.AreEqual(true, bd.checkPartWord("carotte tomate"));
            Assert.AreEqual(true, bd.checkPartWord("j'ai testSynonyme"));
            Assert.AreEqual(true, bd.checkPartWord("je suis grand"));
            Assert.AreEqual(false, bd.checkPartWord("petit Test"));
            Assert.AreEqual(false, bd.checkPartWord("ici se trouve une phrase au hasard"));
        }

        /// <summary>
        /// Vérifie le retour de valeur d'un mot
        /// </summary>
        [TestMethod]
        public void test_bdd_ReturnValeur()
        {
            BaseDonnees.Models.MotCle mc = new BaseDonnees.Models.MotCle
            {
                mot = "test",
                valeur = 15,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            bd.SetSynonyme(new BaseDonnees.Models.Synonyme
            {
                mot = "testSynonyme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            });

            mc = new BaseDonnees.Models.MotCle
            {
                mot = "carotte",
                valeur = 7,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            bd.SetSynonyme(new BaseDonnees.Models.Synonyme
            {
                mot = "beurre",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                fk_trad = mc.mot
            });

            mc = new BaseDonnees.Models.MotCle
            {
                mot = "grand",
                valeur = 2,
                fk_theme = "TestTheme",
                DateAjout = DateTime.Today,
                fk_Date = DateTime.Today,
                Synonyme = new BaseDonnees.Models.Synonyme()
            };

            bd.SetMotCle(mc);

            Assert.AreEqual(7, bd.retourVal("beurre"));
            Assert.AreEqual(15, bd.retourVal("test"));
            Assert.AreEqual(7, bd.retourVal("carotte"));
            Assert.AreEqual(15, bd.retourVal("testSynonyme"));
            Assert.AreEqual(2, bd.retourVal("grand"));
            Assert.AreEqual(0, bd.retourVal("plouf"));
        }

        /// <summary>
        /// Testes le retour de liste de sites dynamiques
        /// </summary>
        [TestMethod]
        public void test_listeDyn()
        {
            bd.seeder();

            List<ListeDynamique> l= bd.GetListeDynamiques("Approprie").ToList();

            Assert.IsNotNull(l);
            Assert.AreEqual("reallifecam.com",l[0].url);
        }

        /// <summary>
        /// Testes le dictionnaire mot-clé et nombres associé
        /// </summary>
        [TestMethod]
        public void test_dictionnaryMotCle()
        {
            bd.seeder();

            Dictionary<string, int> dictTest = bd.motsInterdit();

            Assert.IsNotNull(dictTest);
            Assert.AreEqual(0, dictTest["sex"]);
            Assert.AreEqual(0, dictTest["sexe"]);
            Assert.AreEqual(0, dictTest["seks"]);
            Assert.AreEqual(0, dictTest["porn"]);
        }

        /// <summary>
        /// Testes le dictionnaire mot-clé et valeur
        /// </summary>
        [TestMethod]
        public void test_dictionnaryMotCleVal()
        {
            bd.seeder();

            Dictionary<string, int> dictTest = bd.motsInterditVal();

            Assert.IsNotNull(dictTest);
            Assert.AreEqual(2, dictTest["sex"]);
            Assert.AreEqual(2, dictTest["sexe"]);
            Assert.AreEqual(2, dictTest["seks"]);
            Assert.AreEqual(15, dictTest["porn"]);
        }

        /// <summary>
        /// Testes le dictionnaire thème et mot
        /// </summary>
        [TestMethod]
        public void test_dictionnaryMotTheme()
        {
            bd.seeder();

            Dictionary<string, string> dictTest = bd.retourTheme();

            Assert.IsNotNull(dictTest);
            Assert.AreEqual("Pornographie", dictTest["sex"]);
            Assert.AreEqual("Pornographie", dictTest["sexe"]);
            Assert.AreEqual("Pornographie", dictTest["seks"]);
            Assert.AreEqual("Pornographie", dictTest["porn"]);
        }

        /// <summary>
        /// Testes le dictionnaire thème et valeur
        /// </summary>
        [TestMethod]
        public void test_dictionnaryValTheme()
        {
            bd.seeder();

            Dictionary<string, int> dictTest = bd.themeVal();

            Assert.IsNotNull(dictTest);
            Assert.AreEqual(0, dictTest["Pornographie"]);
            Assert.AreEqual(0, dictTest["Approprie"]);
        }

        /// <summary>
        /// Testes le dictionnaire entre valeur booléenne et nom du site
        /// </summary>
        [TestMethod]
        public void test_val_site_liste()
        {
            bd.seeder();
            Dictionary<string, bool> dictTest = bd.retourListeSites();

            Assert.IsNotNull(dictTest);
            Assert.AreEqual(false, dictTest["www.youporn.com"]);
            Assert.AreEqual(true, dictTest["www.facebook.com"]);
            Assert.AreEqual(true, dictTest["mail.google.com"]);
            Assert.AreEqual(true, dictTest["localhost"]);
        }

        /// <summary>
        /// Testes le dictionnaire entre valeur booléenne et nom du site dynamique
        /// </summary>
        [TestMethod]
        public void test_val_site_lite_dynamique()
        {
            bd.seeder();
            Dictionary<string, bool> dictTest = bd.retourListeDynamiqueSites();

            Assert.IsNotNull(dictTest);
            Assert.AreEqual(false, dictTest["reallifecam.com"]);
            Assert.AreEqual(true, dictTest["www.penofchaos.com"]);
        }
    }
}
