using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Profileur
{
    /// <summary>
    /// Classe qui servira de profil pour les utilisateur.
    /// Elle permet notamment de savoir quels seront les thèmes auquel l'utilisateur sera soumis.
    /// </summary>
    [DataContract]
    public class Profil
    {
        /// <summary>
        /// Liste des thèmes
        /// </summary>
        private List<string> listeTheme;

        /// <summary>
        /// Getter et setter de la liste des thème
        /// </summary>
        [DataMember]
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

        [DataMember]
        public bool adulte { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Profil()
        {
            ListeTheme = new List<string>();
            adulte = false;
        }

        public Profil(bool adulte)
        {
            ListeTheme = new List<string>();
            this.adulte = adulte;
        }

        /// <summary>
        /// Ajoute un thème dans la liste
        /// </summary>
        /// <param name="theme">Thème à ajouter</param>
        public void ajoutTheme(string theme)
        {
            ListeTheme.Add(theme);
        }

        /// <summary>
        /// Réinitialise la liste de thème selon une liste donnée
        /// </summary>
        /// <param name="listeTheme">Liste de thèmes</param>
        public void ajoutListeTheme(List<string> listeTheme)
        {
            ListeTheme = listeTheme;
        }

        /// <summary>
        /// Enlève un thème à une place donnée
        /// </summary>
        /// <param name="i">Index</param>
        private void retireTheme(int i)
        {
            ListeTheme.RemoveAt(i);
        }

        /// <summary>
        /// Retire un thème dont on connait le nom.
        /// </summary>
        /// <param name="nomTheme">Nom du thème</param>
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
