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
        //private string[] separateur;
        private int valArret=0;

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
            dictValMot = bdd.motsInterditVal();
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
            Dictionary<string, int> nombreMots = bdd.motsInterdit();

            double val = 0;

            /*if (valArret == 0)
            {
                valArret = 20;
            }

            while (listWordValue.Count>0)
            {
                listWordValue.RemoveAt(0);
            }

            List<Thread> listTh = new List<Thread>();

            string[] separateur = phrase.Split(separateurBodyHead, StringSplitOptions.RemoveEmptyEntries);

            string[] diviseHead = separateur[1].Split(separateurHead, StringSplitOptions.RemoveEmptyEntries);

            string[] donneesHead = diviseHead[1].Split(separateurMot, StringSplitOptions.RemoveEmptyEntries);*/

            string[] separateur = phrase.Split(separateurMot, StringSplitOptions.RemoveEmptyEntries);

            foreach (string mot in separateur)
            {
                if (nombreMots.ContainsKey(mot))
                {
                    int nb = nombreMots[mot];
                    nb++;
                    nombreMots[mot] = nb;
                }

                /*bool ifWordExist = false;

                foreach (string mot in listWordValue)
                {
                    if (s == mot)
                    {
                        ifWordExist = true;
                        break;
                    }
                }
                if (!ifWordExist)
                {
                    listWordValue.Add(s);
                }*/
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

            /*int i = 0;
            int j = 0;
            while (i<listWordValue.Count && value < valArret)
            {
                if (j < 10)
                {
                    Thread th = new Thread(new ParameterizedThreadStart(thValWord));
                    listTh.Add(th);
                    th.Start(listWordValue[i]);

                    j++;
                    i++;
                }
                else
                {
                    foreach (Thread th in listTh)
                    {
                        if (th.IsAlive)
                        {
                            th.Join();
                        }
                    }
                    j = 0;
                }                
            }

            foreach (Thread th in listTh)
            {
                if (th.IsAlive)
                {
                    th.Join();
                }
            }*/
            return val;
        }

        /*private void thValWord(object wordTh)
        {
            string word = (string)wordTh;

            if (value < valArret)
            {
                string testWord = bdd.returnTheme(word);
                if (testWord != "" && testWord != "Approprie")
                {
                    int valTmp = 0;
                    int dubble = 0;
                    valTmp = bdd.retourVal(word);

                    foreach (string dubbleWord in separateur)
                    {
                        if (word == dubbleWord)
                        {
                            dubble++;
                        }
                    }
                    lock (_lock)
                    {
                        if (dubble > 1)
                        {
                            value += Math.Pow((double)valTmp, (double)dubble);
                        }
                        else
                        {
                            value += (double)valTmp;
                        }
                    }

                }
            }
        }*/

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
    }
}
