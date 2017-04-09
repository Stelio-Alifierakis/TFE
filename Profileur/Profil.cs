using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profileur
{
    public class Profil
    {
        private List<string> listeTheme;

        public List<string> ListeTheme
        {
            get
            {
                return listeTheme;
            }

            set
            {
                listeTheme = value;
            }
        }

        public Profil()
        {
            ListeTheme = new List<string>();
        }

        public void ajoutTheme(string theme)
        {
            ListeTheme.Add(theme);
        }

        public void ajoutListeTheme(List<string> listeTheme)
        {
            ListeTheme = listeTheme;
        }

        private void retireTheme(int i)
        {
            ListeTheme.RemoveAt(i);
        }

        public void retireTheme(string nomTheme)
        {
            for(int i = 0; i <= ListeTheme.Count; i++)
            {
                if (ListeTheme[i]==nomTheme)
                {
                    retireTheme(i);
                    break;
                }
            }
        }


    }
}
