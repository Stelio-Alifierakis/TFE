using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Profileur;
using proxy;
using ProxyNavigateur;
using Rechercheur;

namespace Configurateur
{
    public class Constructeur : IConstructeur
    {
        private IDAL dal;
        private IRechercheur rechercheur;
        private IEproxy eproxy;
        private IListUtilisateur listUtilisateur;

        public void construction(IDAL dal, IRechercheur rechercheur, IEproxy eproxy, IListUtilisateur listUtilisateur)
        {
            this.dal = dal;
            this.rechercheur = rechercheur;
            this.eproxy = eproxy;
            this.listUtilisateur=listUtilisateur;

            initBdd();
            initProxy();
        }

        private void initBdd()
        {
            /*d.suppressionDB();
             d.creation();
             d.seed();*/
            rechercheur.setBdd(dal);
        }

        public void initProxy()
        {
            eproxy.setRechercheur(rechercheur);
        }

        public void demarrage()
        {
            eproxy.StartProxy();
        }

        public void stop()
        {
            eproxy.Stop();
        }
    }
}
