using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rechercheur;

namespace proxy
{
    public interface IEproxy
    {
        void StartProxy();
        void Stop();
        void setRechercheur(IRechercheur r);
    }
}
