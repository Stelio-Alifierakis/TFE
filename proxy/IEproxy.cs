using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rechercheur;

namespace proxy
{
    /// <summary>
    /// Interface qui sert de contrat à la classe de proxy
    /// </summary>
    public interface IEproxy
    {
        void StartProxy();
        void Stop();
        void setRechercheur(IRechercheur r);
    }
}
