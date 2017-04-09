using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profileur
{
    public class ListeUtilisateurs
    {
        private List<Utilisateur> listUtilisateur;

        public ListeUtilisateurs()
        {
            listUtilisateur = new List<Utilisateur>();
        }

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

        public void AjoutUtilisateur(Utilisateur user){
            ListUtilisateur.Add(user);
       }

        private void RetirerUtilisateur(int id)
        {
            ListUtilisateur.RemoveAt(id);
        }

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
