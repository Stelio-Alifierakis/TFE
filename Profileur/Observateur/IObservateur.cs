using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profileur.Observateur
{
    /// <summary>
    /// Interface qui servira lors des changements d'utilisateurs
    /// </summary>
    public interface IObservateur
    {
        void update(Utilisateur u);
    }
}
