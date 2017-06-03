using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

using Profileur;

namespace GUI.Model.login
{
    public class IdentiteCustom : IIdentity
    {
        public Utilisateur user { get; set; }

        public string Name { get; private set; }
        public bool Roles { get; private set; }

        public IdentiteCustom(Utilisateur user)
        {
            if (user!=null)
            {
                this.user = user;
                Name = user.Login;
                Roles = user.Profil.adulte;
            }
            
        }


        #region identification member

        public string AuthenticationType
        {
            get
            {
                return "Custom authentication";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(Name);
            }
        }

        #endregion

        
    }
}
