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
    public class Configurateur
    {

        private static readonly IEproxy Controller = new Eproxy();
        private Constructeur construction;

        public Configurateur()
        {
           
        }

        public void init()
        {
            DAL d = new DAL(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/bdd/test.sqlite");
            Rechercheur.Rechercheur r = new Rechercheur.Rechercheur(d);
            //Controller.setRechercheur(r);
            ListeUtilisateurs list = new ListeUtilisateurs();
            construction = new Constructeur();
            construction.construction(d, r, Controller, list);
        }

        public void Demarrage()
        {
            construction.demarrage();
        }

        public void Stop()
        {
            construction.stop();
        }

    }
}
