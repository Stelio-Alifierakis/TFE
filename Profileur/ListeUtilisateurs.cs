using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Profileur
{
    /// <summary>
    /// Classe qui sert à stocker la liste des utilisateurs
    /// </summary>
    public class ListeUtilisateurs : IListUtilisateur
    {
        /// <summary>
        /// Liste des utilisateurs
        /// </summary>
        private List<Utilisateur> listUtilisateur;

        /// <summary>
        /// Constructeur
        /// </summary>
        public ListeUtilisateurs()
        {
            listUtilisateur = new List<Utilisateur>();
        }

        /// <summary>
        /// Getter et setter de la liste des utilisateurs
        /// </summary>
        public List<Utilisateur> ListUtilisateur
        {
            get
            {
                return listUtilisateur;
            }

            set
            {
                listUtilisateur = value;
            }
        }

        /// <summary>
        /// Ajoute un utilisateur dans la liste
        /// </summary>
        /// <param name="user">Utilisateur</param>
        public void AjoutUtilisateur(Utilisateur user){
            ListUtilisateur.Add(user);
       }

        /// <summary>
        /// Supprime un utilisateur à un index donné
        /// </summary>
        /// <param name="id">Index</param>
        private void RetirerUtilisateur(int id)
        {
            ListUtilisateur.RemoveAt(id);
        }

        /// <summary>
        /// Retire un utilisateur dont on connait le nom
        /// </summary>
        /// <param name="nomUtilisateur">Nom de l'utilisateur</param>
        public void RetirerUtilisateur(string nomUtilisateur)
        {
            for (int i=0; i<=ListUtilisateur.Count; i++)
            {
                if (ListUtilisateur[i].Login==nomUtilisateur)
                {
                    RetirerUtilisateur(i);
                    break;
                }
            }
        }

    }
}
