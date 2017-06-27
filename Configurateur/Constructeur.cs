using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Profileur;
using proxy;
using BaseDonnees;
using Rechercheur;
using Communicateur.ComWCF;
using Communicateur.ComProxy;
using Profileur.Observateur;
using Synchronisateur;

namespace Configurateur
{
    /// <summary>
    /// Classe qui va rassembler les différents éléments du programme et les initialiser.
    /// </summary>
    public class Constructeur : IConstructeur
    {
        /// <summary>
        /// Variable qui stocke la classe d'accès à la base de donnée
        /// </summary>
        private IDAL dal;

        private IComSynchro synchro;

        private IdentificateurProgramme idProg;

        /// <summary>
        /// Variable qui stocke la classe de recherche sur les mots et URL.
        /// </summary>
        private IRechercheur rechercheur;

        /// <summary>
        /// Variable qui stocke la classe de proxy.
        /// </summary>
        private IEproxy eproxy;

        /// <summary>
        /// Variable qui stocke la classe utilisateur
        /// </summary>
        private IListUtilisateur listUtilisateur;

        private Serveur serv;

        private IProxyCom comProxy;

        /// <summary>
        /// Initialise la construction des différents éléments
        /// </summary>
        /// <param name="dal">Classe d'accès à la base de données</param>
        /// <param name="rechercheur">Classe de recherche sur les mots et URL</param>
        /// <param name="eproxy">Classe proxy</param>
        /// <param name="listUtilisateur">Classe utilisateur</param>
        public void construction(IDAL dal, IRechercheur rechercheur, IEproxy eproxy, IListUtilisateur listUtilisateur)
        {
            this.dal = dal;
            this.rechercheur = rechercheur;
            this.eproxy = eproxy;
            this.listUtilisateur=listUtilisateur;

            comProxy = new ComProxy();

            synchro = new ComSynchro(dal);

            idProg = new IdentificateurProgramme();

            comProxy.setActifContenu(eproxy.retourActifContenu());
            comProxy.setActifUrl(eproxy.retourActifURL());

            initBdd();

            initProxy();

            initRechercheur(rechercheur, listUtilisateur);

            initServeurPipe();

        }

        /// <summary>
        /// Fonction qui initialise la base de données
        /// </summary>
        private void initBdd()
        {
            rechercheur.setBdd(dal);
        }

        private void initRechercheur(IRechercheur r, IListUtilisateur l)
        {
            
            Rechercheur.Rechercheur rech = (Rechercheur.Rechercheur)r;

            ListeUtilisateurs list =(ListeUtilisateurs)l;

            comProxy.setListUtilisateur(list);

            list.Ajout(rech.GestUser);

            /*
                test
             */
            //Console.WriteLine(list.ChangeUtilisateurEnCours("B.J", "test"));
            Console.WriteLine(list.ChangeUtilisateurEnCours("B.E", "test"));

            comProxy.setUtilisateur(list.obtientUtilisateur("B.E"));

            /*
             Fin test
             */
        }

        private void initServeurPipe()
        {
            serv = new Serveur(comProxy);

            serv.Init();

        }

        private void initSynchro()
        {
            
        }

        /// <summary>
        /// Classe qui initialise le rechercheur
        /// </summary>
        private void initProxy()
        {
            eproxy.setRechercheur(rechercheur);
        }

        /// <summary>
        /// Classe qui démarre le proxy
        /// </summary>
        public void demarrage()
        {
            //synchro.Start(idProg.numeroProgramme);
            serv.start();
            eproxy.StartProxy();

            
        }

        /// <summary>
        /// Classe qui stoppe le proxy
        /// </summary>
        public void stop()
        {
            //synchro.Stop();
            serv.stop();
            eproxy.Stop();
        }

        /// <summary>
        /// Fonction qui va appeler la fonction de changement d'utilisateur de la liste des utilisateurs
        /// </summary>
        /// <param name="login">Login utilisateur</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns>Valeur booléenne</returns>
        public bool changeUtilisateur(string login, string mdp)
        {
            return listUtilisateur.ChangeUtilisateurEnCours(login, mdp);
        }
    }
}
