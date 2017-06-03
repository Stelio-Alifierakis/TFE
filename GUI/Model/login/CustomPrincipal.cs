using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

using GUI.Model.login;

namespace GUI.Model.login
{
    public class CustomPrincipal : IPrincipal
    {
        private IdentiteCustom _identite;

        public IIdentity Identity
        {
            get
            {
                return _identite ?? new AnonymousIdentity();
            }
            set
            {
                _identite = (IdentiteCustom)value;
            }
        }

        public bool IsInRole(string role)
        {
            //throw new NotImplementedException();
            return _identite.Roles;
        }
    }
}
