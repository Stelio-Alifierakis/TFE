using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Communicateur.Serial;

namespace GUI.Model
{
    public class Authentification
    {
        public string login { get; set; }
        public string mdp { get; set; }

        public Authentification(string login, string mdp)
        {
            this.login = login;
            this.mdp = mdp;
        }
    }
}
