using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronisateur
{
    public interface IComSynchro
    {
        void Start(string IDProg);
        void Stop();
    }
}
