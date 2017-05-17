using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Profileur;
using proxy;
using BaseDonnees;
using Rechercheur;

namespace Configurateur
{
    /// <summary>
    /// Classe qui va servir à configurer le constructeur
    /// </summary>
    public class Configurateur
    {

        /// <summary>
        /// Variable qui va stocker le proxy
        /// </summary>
        private static readonly IEproxy Controller = new Eproxy();

        /// <summary>
        /// Variable qui va stocker le constructeur
        /// </summary>
        private Constructeur construction;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public Configurateur()
        {
           
        }

        /// <summary>
        /// Fonction qui va lancer l'initialisation du configurateur
        /// </summary>
        public void init()
        {
            DAL d = new DAL(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/bdd/test.sqlite");
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);
            //Controller.setRechercheur(r);
            ListeUtilisateurs list = new ListeUtilisateurs();
            construction = new Constructeur();
            construction.construction(d, r, Controller, list);
        }

        /// <summary>
        /// Fonction qui lance la fonction demarrage du constructeur
        /// </summary>
        public void Demarrage()
        {
            construction.demarrage();
        }

        /// <summary>
        /// Fonction qui lance la fonction stop du constructeur
        /// </summary>
        public void Stop()
        {
            construction.stop();
        }

    }
}
