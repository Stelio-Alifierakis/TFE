using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

using Rechercheur.Gest;

using BaseDonnees;
using BaseDonnees.Models;

using Profileur;

namespace Rechercheur
{
    /// <summary>
    /// Classe qui sert pour la recherche de la base de données
    /// </summary>
    public class Rechercheur : IRechercheur
    {
        /// <summary>
        /// Variable qui sert à stocker la classe d'accès à la base de données
        /// </summary>
        private IDAL bdd;

        /// <summary>
        /// Classe qui gère tout ce qui a trait à l'utilisateur en cours
        /// </summary>
        private GestionUtilisateurs gestUser;

        /// <summary>
        /// Verrous pour les threads
        /// </summary>
        private static Object _lock = new object();

        /// <summary>
        /// Valeur
        /// </summary>
        private double value = 0;

        /// <summary>
        /// Variable qui stocke les valeurs des mots interdits
        /// </summary>
        private List<string> listWordValue = new List<string>();

        /// <summary>
        /// Dictionnaire qui stocke les mots et leurs valeurs
        /// </summary>
        private Dictionary<string, int> dictValMot;

        /// <summary>
        /// Dictionnaire qui stocke les mot-clés et les thèmes associées
        /// </summary>
        private Dictionary<string, string> lienThemeMot;

        /// <summary>
        /// Dictionnaire qui stocke les sites d'un côté et leur autorisation (vraie ou fausse) de l'autre
        /// </summary>
        private Dictionary<string, bool> listeUrlACheck;

        /// <summary>
        /// Dictionnaire qui stocke les sites dynamiques d'un côté et leur autorisation (vraie ou fausse) de l'autre
        /// </summary>
        private Dictionary<string, bool> listeDynUrlACheck;

        /// <summary>
        /// Variable qui stocke une valeur d'arrêt
        /// </summary>
        private int valArret=0;

        /// <summary>
        /// Chaînes de caractères utilisées pour séparer les URL en plusieurs morceaux
        /// </summary>
        private readonly string[] separateurURL = {
               "/","&", "=", "?", ":"
            };

        /// <summary>
        /// Chaînes de caractères utilisées pour séparer le contenu web en 2, une partie head et une partie body
        /// </summary>
        private readonly string[] separateurBodyHead = {
               "<head","<body"
            };

        /// <summary>
        /// Chaînes de caractères utilisées pour séparer le contenu du head
        /// </summary>
        private readonly string[] separateurHead = {
                "<title","</title>", "name=\"description"
            };

        /// <summary>
        /// Chaînes de caractères utilisées pour séparer le contenu du body
        /// </summary>
        private readonly string[] separateurBody = {
                "<p",
        };

        /// <summary>
        /// Chaînes de caractères utilisées pour séparer les phrases
        /// </summary>
        string[] separateurMot = {
                " ", "\n", "\r\n", "=", "_", ",", "'","&","\""
            };

        /// <summary>
        /// Getter et setter de ValArret
        /// </summary>
        public int ValArret
        {
            get
            {
                return valArret;
            }

            set
            {
                valArret = value;
            }
        }

        /// <summary>
        /// Getter et setter de la gestion des utilisateurs
        /// </summary>
        public GestionUtilisateurs GestUser
        {
            get
            {
                return gestUser;
            }

            set
            {
                gestUser = value;
            }
        }

        /// <summary>
        /// Constructeur.
        /// Reprend les différents dictionnaires depuis la classe d'accès à la base de données.
        /// </summary>
        /// <param name="bdd">Classe d'accès à la base de données</param>
        public Rechercheur(IDAL bdd)
        {
            this.bdd = bdd;

            gestUser = new GestionUtilisateurs();

            try
            {
                dictValMot = bdd.motsInterditVal();
                lienThemeMot = bdd.retourTheme();
                listeUrlACheck = bdd.listeSiteBool();
                listeDynUrlACheck = bdd.retourListeDynamiqueSites();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Classe qui va définir la classe d'accès à la base de données.
        /// Reprend les différents dictionnaires depuis la classe d'accès à la base de données.
        /// </summary>
        /// <param name="bdd">Classe d'accès à la base de données</param>
        public void setBdd(IDAL bdd)
        {
            this.bdd = bdd;
            try
            {
                dictValMot = bdd.motsInterditVal();
                lienThemeMot = bdd.retourTheme();
                listeUrlACheck = bdd.listeSiteBool();
                listeDynUrlACheck = bdd.retourListeDynamiqueSites();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Classe qui ne marche plus
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [System.Obsolete]
        public bool goodWord(string msg) //à changer
        {
            bool validWord = false;

            return validWord;
        }

        /// <summary>
        /// Classe à ne pas utiliser.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [System.Obsolete]
        public bool checkWord(string msg) //à changer
        {
            return bdd.verifSite(msg);
        }

        /// <summary>
        /// Vérifie les mots partiellement (obsolète)
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [System.Obsolete]
        public bool checkPartWord(string msg)
        {
            return bdd.checkPartWord(msg);
        }

        /// <summary>
        /// Fonction qui va attribuer une valeur à une phrase.
        /// Elle va vérifier le nombre de doublon d'un mot.
        /// Chaque mot aura pour valeur, la valeur du mot exposant le nombre de fois que le mot est présent.
        /// </summary>
        /// <param name="phrase">Phrase à évaluer</param>
        /// <returns>Valeur de retour</returns>
        public double valPhrase(string phrase)
        {
            Dictionary<string, int> nombreMots=new Dictionary<string, int>();

            try
            {
                nombreMots = bdd.motsInterdit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            double val = 0;

            string[] separateur = phrase.Split(separateurMot, StringSplitOptions.RemoveEmptyEntries);

            foreach (string mot in separateur)
            {
                if (nombreMots.ContainsKey(mot))
                {
                    Console.WriteLine(mot);
                    int nb = nombreMots[mot];
                    nb++;
                    nombreMots[mot] = nb;
                }
            }

            foreach (KeyValuePair<string,int> mot in nombreMots)
            {
                if (lienThemeMot.ContainsKey(mot.Key))
                {
                    if (GestUser.testTheme(lienThemeMot[mot.Key]))
                    {
                        if (mot.Value == 1)
                        {
                            val += dictValMot[mot.Key];
                        }
                        else if (mot.Value > 1)
                        {
                            val += Math.Pow(dictValMot[mot.Key], mot.Value);
                        }

                        if (value > 100)
                        {
                            return val;
                        }
                    }
                }
            }
            return val;
        }

        /// <summary>
        /// Récupère la liste des sites sous forme de listes
        /// </summary>
        /// <param name="nomListe">Nom de liste</param>
        /// <returns>Liste des site</returns>
        public List<Sites> getListeSites(string nomListe)
        {
            List<Sites> l = bdd.retourSites(nomListe);

            return l;
        }

        /// <summary>
        /// Récupère la liste des sites dynamiques sous forme de listes
        /// </summary>
        /// <param name="nomTheme">Nom du thème</param>
        /// <returns>Liste des sites dynamiques</returns>
        public List<ListeDynamique> GetListeDynamiques(string nomTheme)
        {
            List<ListeDynamique> dyn = bdd.GetListeDynamiques(nomTheme);

            return dyn;
        }

        /// <summary>
        /// Fonction qui va attribuer un thème à une phrase selon le thème qui aura eu le plus de valeur.
        /// Elle va vérifier le nombre de doublon d'un mot.
        /// Chaque mot aura pour valeur, la valeur du mot exposant le nombre de fois que le mot est présent.
        /// </summary>
        /// <param name="phrase">Phrase à analyser</param>
        /// <returns>Mot du thème</returns>
        public string themePage(string phrase)
        {
            Dictionary<string, int> valParTheme = new Dictionary<string, int>();
            Dictionary<string, int> nombreMots = new Dictionary<string, int>();

            try
            {
                nombreMots = bdd.motsInterdit();
                valParTheme = bdd.themeVal();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string[] separateur = phrase.Split(separateurMot, StringSplitOptions.RemoveEmptyEntries);

            foreach (string mot in separateur)
            {
                if (nombreMots.ContainsKey(mot))
                {
                    int nb = nombreMots[mot];
                    nb++;
                    nombreMots[mot] = nb;
                }
            }

            foreach (KeyValuePair<string, int> mot in nombreMots)
            {
                if (mot.Value >0)
                {
                    int valTheme=valParTheme[lienThemeMot[mot.Key]];
                    valTheme++;
                    valParTheme[lienThemeMot[mot.Key]] = valTheme;
                }
            }

            int max = 0;
            string motTheme = "";

            foreach (KeyValuePair<string, int> theme in valParTheme)
            {
                if (theme.Value>max)
                {
                    max = theme.Value;
                    motTheme = theme.Key;
                }
            }

            return motTheme;
        }

        /// <summary>
        /// Vérifie si une URL fait partie des URL bannie ou contient des mots interdits
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public int checkUrl(string url)
        {
            int verif = 2;

            string[] separateur = url.Split(separateurURL, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, string> siteTheme=bdd.retourSiteTheme();

            foreach (string urlACheck in separateur)
            {

                if (listeUrlACheck.ContainsKey(urlACheck))
                {

                    Console.WriteLine(listeUrlACheck[urlACheck]);
                    verif = urlCheckTheme(urlACheck, listeUrlACheck[urlACheck], siteTheme);
                    //verif = 1;
                }
                else if (listeDynUrlACheck.ContainsKey(urlACheck))
                {
                    Console.WriteLine(listeDynUrlACheck[urlACheck]);
                    verif = urlCheckTheme(urlACheck, listeDynUrlACheck[urlACheck], siteTheme);
                    //verif = 1;
                }
            }

            if (listeDynUrlACheck.ContainsKey(url))
            {
                verif = urlCheckTheme(url, listeDynUrlACheck[url], siteTheme);
            }

            return verif;
        }

        private int urlCheckTheme(string urlACheck, bool valeurBooleenne, Dictionary<string, string> siteTheme)
        {
            if (GestUser.testTheme(siteTheme[urlACheck]))
            {
               return valeurBooleenne == true ? 1 : 0 ;
            }
            else
            {
                return 1;
            }
        }

        public void MiseAjourListeDyn(string url, string theme)
        {
            bdd.SetListeDynamique(url, DateTime.Now, theme);
        }
    }
}
