using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicateur.ComProxy
{
    public interface IListUtilisateurCom
    {
        bool verifCom(string login, string mdp);
    }
}
