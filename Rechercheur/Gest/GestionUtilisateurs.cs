using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Profileur;
using Profileur.Observateur;

using BaseDonnees.Models;

namespace Rechercheur.Gest
{
    /// <summary>
    /// Classe qui va gérer l'utilisateur pour le rechercheur
    /// </summary>
    public class GestionUtilisateurs : IObservateurProfileur
    {
        /// <summary>
        /// Variable qui stocke l'utilisateur
        /// </summary>
        private Utilisateur utilisateur;

        /// <summary>
        /// Dictionnaire qui va stocker les noms de thèmes et les valeurs booléennes.
        /// </summary>
        private Dictionary<string, bool> validMot;

        /// <summary>
        /// Constructeur
        /// </summary>
        public GestionUtilisateurs()
        {

        }

        /// <summary>
        /// Getter et setter de l'utilisateur
        /// </summary>
        public Utilisateur Utilisateur
        {
            get
            {
                return utilisateur;
            }

            set
            {
                utilisateur = value;
            }
        }

        /// <summary>
        /// Getter et setter du dictionnaire qui valide les mots
        /// </summary>
        public Dictionary<string, bool> ValidMot
        {
            get
            {
                return validMot;
            }

            set
            {
                validMot = value;
            }
        }

        /// <summary>
        /// Fonction qui va mettre à jour l'utilisateur
        /// </summary>
        /// <param name="u">Utilisateur</param>
        void IObservateurProfileur.update(Utilisateur u)
        {
            //throw new NotImplementedException();
            Utilisateur = u;
            Console.WriteLine(utilisateur.Login);

        }

        /// <summary>
        /// Fonction qui teste si le thème en entrée existe pour l'utilisateur
        /// </summary>
        /// <returns></returns>
        public bool testTheme(string nomTheme)
        {
            List<string> listeTheme = utilisateur.Profil.ListeTheme;
            foreach (string theme in listeTheme)
            {
                if (nomTheme==theme)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
