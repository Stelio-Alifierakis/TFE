using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur;
using System.Collections;
using System.Threading;

using ProxyNavigateur.Models;

namespace Rechercheur
{
    public class Rechercheur
    {
        DAL bdd;

        private static Object _lock = new object();

        private double value = 0;
        //private ArrayList listWordValue = new ArrayList();
        private List<string> listWordValue = new List<string>();
        private Dictionary<string, int> dictValMot;
        private Dictionary<string, string> lienThemeMot;
        private Dictionary<string, bool> listeUrlACheck;
        private Dictionary<string, bool> listeDynUrlACheck;
        //private string[] separateur;
        private int valArret=0;

        string[] separateurURL = {
               "/","&", "=", "?", ":"
            };

        string[] separateurBodyHead = {
               "<head","<body"
            };

        string[] separateurHead = {
                "<title","</title>", "name=\"description"
            };

        string[] separateurBody = {
                "<p",
            };

        string[] separateurMot = {
                " ", "\n", "\r\n", "=", "_", ",", "'","&","\""
            };

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

        public Rechercheur(DAL bdd)
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

        public bool goodWord(string msg) //à changer
        {
            bool validWord = false;

            return validWord;
        }

        public bool checkWord(string msg) //à changer
        {
            return bdd.verifSite(msg);
        }

        public bool checkPartWord(string msg)
        {
            return bdd.checkPartWord(msg);
        }

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
                if (mot.Value==1)
                {
                    val += dictValMot[mot.Key];
                }
                else if(mot.Value >1)
                {
                    val += Math.Pow(dictValMot[mot.Key],mot.Value);
                }

                if (value>100)
                {
                    return val;
                }
            }
            return val;
        }

        public List<Sites> getListeSites(string nomListe)
        {
            List<Sites> l = bdd.retourSites(nomListe);

            return l;
        }

        public List<ListeDynamique> GetListeDynamiques(string nomTheme)
        {
            List<ListeDynamique> dyn = bdd.GetListeDynamiques(nomTheme);

            return dyn;
        }

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

        public int checkUrl(string url)
        {
            int verif = 2;

            string[] separateur = url.Split(separateurURL, StringSplitOptions.RemoveEmptyEntries);

            foreach (string urlACheck in separateur)
            {
                if (listeUrlACheck.ContainsKey(urlACheck) && listeUrlACheck[urlACheck]==true)
                {
                    Console.WriteLine(listeUrlACheck[urlACheck]);
                    verif = 1;
                    break;
                }
                else if(listeUrlACheck.ContainsKey(urlACheck) && listeUrlACheck[urlACheck] == false)
                {
                    Console.WriteLine(listeUrlACheck[urlACheck]);
                    verif = 0;
                    break;
                }
                else if (listeDynUrlACheck.ContainsKey(urlACheck) && listeDynUrlACheck[urlACheck] == true)
                {
                    Console.WriteLine(listeUrlACheck[urlACheck]);
                    verif = 1;
                    break;
                }
                else if (listeDynUrlACheck.ContainsKey(urlACheck) && listeDynUrlACheck[urlACheck] == false)
                {
                    Console.WriteLine(listeUrlACheck[urlACheck]);
                    verif = 0;
                    break;
                }
            }
            return verif;
        }
    }
}
