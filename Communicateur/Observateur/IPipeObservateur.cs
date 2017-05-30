using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Communicateur.Serial;

namespace Communicateur.Observateur
{
    public interface IPipeObservateur
    {
        void MiseAJour(IEnvoyable env);
    }
}
