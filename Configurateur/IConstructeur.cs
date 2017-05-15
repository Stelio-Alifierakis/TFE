using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rechercheur;
using ProxyNavigateur;
using proxy;
using Profileur;

namespace Configurateur
{
    public interface IConstructeur
    {
        void construction(IDAL dal, IRechercheur rechercheur, IEproxy eproxy, IListUtilisateur listUtilisateur);
    }
}
