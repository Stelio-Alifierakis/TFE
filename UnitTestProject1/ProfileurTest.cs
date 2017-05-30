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

using Profileur;
using Communicateur;
using Profileur.MotDePasse;

namespace UnitTestProject1
{
    /// <summary>
    /// Classe qui teste les profil
    /// </summary>
    [TestClass]
    public class ProfileurTest
    {
        /// <summary>
        /// Testes la vérification de mot de passe
        /// </summary>
        [TestMethod]
        public void verificationModDePasseTest()
        {
            Utilisateur u = new Utilisateur();
            u.MotDePasse = "test";

            Assert.IsNotNull(u);
            Assert.AreEqual(true,u.verificationMotDePasse("test"));
            Assert.AreEqual(false, u.verificationMotDePasse("bonbon"));
        }

        /// <summary>
        /// Vérifie l'existence des utilisateurs
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void listeUtilisateurTest()
        {
            Utilisateur u = new Utilisateur("Jacques", "Bernard", "Jacques.Bernard", 14, "test");
            ListeUtilisateurs lu = new ListeUtilisateurs();
            lu.AjoutUtilisateur(u);

            Assert.IsNotNull(lu);
            Assert.AreEqual("Bernard", lu.ListUtilisateur[4].Prenom);
            Assert.AreEqual(14, lu.ListUtilisateur[4].Age);

            lu.RetirerUtilisateur(u.Login);

            Assert.Fail("Bernard", lu.ListUtilisateur[0].Prenom);
        }

        /// <summary>
        /// Teste la liste des thèmes
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ProfilListeThemeTest()
        {
            Profil p = new Profil();

            p.ajoutTheme("test");
            p.ajoutTheme("essai");

            Assert.IsNotNull(p);
            Assert.AreEqual("test", p.ListeTheme[0]);
            Assert.AreEqual("essai", p.ListeTheme[1]);

            p.retireTheme("essai");
            
            Assert.Fail("essai", p.ListeTheme[1]);
        }

        /// <summary>
        /// Testes un utilisateur
        /// </summary>
        [TestMethod]
        public void UtilisateurProfilTest()
        {
            Profil p = new Profil();
            p.ajoutTheme("test");

            Utilisateur u = new Utilisateur("Jacques", "Bernard", "Jacques.Bernard", 14, p, "test");

            Assert.IsNotNull(u);
            Assert.AreEqual("Jacques", u.Nom);
            Assert.AreEqual("test", u.Profil.ListeTheme[0]);
        }
    }
}
