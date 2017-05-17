using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profileur
{
    /// <summary>
    /// Interface qui sert de contrat à la liste des utilisateurs
    /// </summary>
    public interface IListUtilisateur
    {
        void AjoutUtilisateur(Utilisateur user);
        void RetirerUtilisateur(string nomUtilisateur);
    }
}
