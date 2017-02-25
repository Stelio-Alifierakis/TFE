using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur.Models;

namespace ProxyNavigateur.DB
{

    class TableSeed
    {
        public BDDInterface bd;

        public TableSeed(BDDInterface bd)
        {
            this.bd = bd;
        }

        public void seeder()
        {
            Synchronisation synch = new Synchronisation(DateTime.Now);
            ListeTheme th = new ListeTheme("Approprie");
            Listes liste = new Listes("Liste Verte");
            Topologie topo = new Topologie("000001", synch.date);
            Sites site = new Sites("www.openclassroom.com", synch.date, th.theme, liste.liste);
            ListeDynamique dyn = new ListeDynamique("www.youtube.com", synch.date, th.theme);

            bd.SetListeTheme(th);
            th.theme = "Pornographie";
            bd.SetListeTheme(th);

            MotCle mc = new MotCle("sex", 4, synch.date, th.theme);
            Synonyme syn = new Synonyme("sexe", mc.mot, synch.date);

            bd.SetSynchro(synch);
            bd.SetListe(liste);
            bd.SetTopologie(topo);
            bd.SetSites(site);
            bd.SetListeDynamique(dyn);
            bd.SetMotCle(mc);
            bd.SetSynonyme(syn);

            liste.liste = "Liste Rouge";
            bd.SetListe(liste);

            site.nomSite = "www.youporn.com";
            site.fk_theme = th.theme;
            site.fk_liste = liste.liste;
            bd.SetSites(site);

            dyn.url = "www.sex.com";
            dyn.fk_theme = th.theme;
            bd.SetListeDynamique(dyn);

            mc.mot = "porn";
            mc.valeur = 15;
            mc.fk_theme = th.theme;
            bd.SetMotCle(mc);

            syn.mot = "seks";
            bd.SetSynonyme(syn);
        }
    }
}
