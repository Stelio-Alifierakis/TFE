using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Profileur
{
    /// <summary>
    /// Interface qui sert de contrat à la liste des utilisateurs
    /// </summary>
    public interface IListUtilisateur
    {
        List<Utilisateur> initListe();
        void AjoutUtilisateur(Utilisateur user);
        void RetirerUtilisateur(string nomUtilisateur);
        bool ChangeUtilisateurEnCours(string login, string mdp);
        Utilisateur obtientUtilisateur(string login);
    }
}
