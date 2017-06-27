using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Profileur.Observateur;
using System.Runtime.Serialization;

namespace Profileur
{
    /// <summary>
    /// Classe abstraite qui va servir surtout pour implémenter le pattern observateur avec le rechercheur
    /// </summary>
    [DataContract]
    [KnownType(typeof(ListeUtilisateurs))]
    public abstract class AbstractListUtilisateurs : IListUtilisateur
    {
        /// <summary>
        /// Liste des observateurs
        /// </summary>
        private List<IObservateurProfileur> listObservateur;

        /// <summary>
        /// Constructeur
        /// </summary>
        public AbstractListUtilisateurs()
        {
            listObservateur = new List<IObservateurProfileur>();
        }


        //Fonctions abstraites

        /// <summary>
        /// Fonction abstraite
        /// </summary>
        public abstract List<Utilisateur> initListe();

        /// <summary>
        /// Fonction abstraite
        /// </summary>
        /// <param name="user">Utilisateur</param>
        public abstract void AjoutUtilisateur(Utilisateur user);

        /// <summary>
        /// Fonction abstraite
        /// </summary>
        /// <param name="nomUtilisateur">Nom de l'utilisateur</param>
        public abstract void RetirerUtilisateur(string nomUtilisateur);

        /// <summary>
        /// Fonction abstraite
        /// </summary>
        /// <param name="login"></param>
        /// <param name="mdp"></param>
        /// <returns></returns>
        public abstract bool ChangeUtilisateurEnCours(string login, string mdp);

        /// <summary>
        /// Fonction abstraite
        /// </summary>
        /// <param name="login">Login de l'utilisateur</param>
        /// <returns>Utilisateur</returns>
        public abstract Utilisateur obtientUtilisateur(string login);

        //Fonctions utilisées pour l'observateur

        /// <summary>
        /// Ajoute un observateur
        /// </summary>
        /// <param name="obs">Observateur</param>
        public void Ajout(IObservateurProfileur obs)
        {
            listObservateur.Add(obs);
        }

        /// <summary>
        /// Retire un observateur
        /// </summary>
        /// <param name="obs">Observateur</param>
        public void Retirer(IObservateurProfileur obs)
        {
            listObservateur.Remove(obs);
        }

        /// <summary>
        /// Notifie les observateurs
        /// </summary>
        /// <param name="u">Utilisateur</param>
        public void Notifier(Utilisateur u)
        {
            foreach (IObservateurProfileur obs in listObservateur)
            {
                obs.update(u);
            }
        }
    }
}
