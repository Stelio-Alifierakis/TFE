using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communicateur.Serial
{
    public interface IEnvoyable
    {
        void SetType(string Nomtype);
        String GetType();
        void SetRaison(string raison);
        string getRaison();
    }
}
