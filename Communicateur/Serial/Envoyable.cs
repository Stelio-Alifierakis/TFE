using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communicateur.Serial
{
    [Serializable]
    public abstract class Envoyable : IEnvoyable
    {
        private string type;
        private string raison;

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        string IEnvoyable.GetType()
        {
            //throw new NotImplementedException();
            return Type;
        }

        void IEnvoyable.SetType(string Nomtype)
        {
            //throw new NotImplementedException();
            Type = Nomtype;
        }

        public void SetRaison(string raison)
        {
            //throw new NotImplementedException();
            this.raison = raison;
        }

        public string getRaison()
        {
            //throw new NotImplementedException();
            return raison;
        }
    }
}
