using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rechercheur;
using BaseDonnees;
using proxy;
using Profileur;

namespace Configurateur
{
    /// <summary>
    /// Interface qui va servir de contrat au constructeur
    /// </summary>
    public interface IConstructeur
    {
        void construction(IDAL dal, IRechercheur rechercheur, IEproxy eproxy, IListUtilisateur listUtilisateur);
    }
}
