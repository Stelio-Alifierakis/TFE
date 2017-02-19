using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur;
using System.Collections;
using System.Threading;

namespace Rechercheur
{
    public class Rechercheur
    {
        DAL bdd;

        private static Object _lock = new object();

        private double value = 0;
        private ArrayList listWordValue = new ArrayList();
        private string[] separateur;
        private int valArret=0;

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
            value = 0;

            if (valArret == 0)
            {
                valArret = 20;
            }

            while (listWordValue.Count>0)
            {
                listWordValue.RemoveAt(0);
            }

            List<Thread> listTh = new List<Thread>();

            string[] testSeparator = { " ", "\n", "\r\n", "=", "_", ",", "'" };
            separateur = phrase.Split(testSeparator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in separateur)
            {
                bool ifWordExist = false;

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
                }
            }

            foreach (string mot in listWordValue)
            {
                Thread th = new Thread(new ParameterizedThreadStart(thValWord));
                listTh.Add(th);
                th.Start(mot);
            }

            foreach (Thread th in listTh)
            {
                if (th.IsAlive)
                {
                    th.Join();
                }
            }

            return value;
        }

        private void thValWord(object wordTh)
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
        }
    }
}
