using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profileur
{
    public interface IListUtilisateur
    {
        void AjoutUtilisateur(Utilisateur user);
        void RetirerUtilisateur(string nomUtilisateur);
    }
}
