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
            Synchronisation sync = setSynchro(DateTime.Now);
            ListeTheme th = setListeTheme("Approprie");
            Listes liste = setListes("Liste Verte");
            setTopo("000001", sync.date);
            setSite("www.openclassroom.com", sync.date, th.theme, liste.liste);
            setSite("www.facebook.com", sync.date, th.theme, liste.liste);
            setSite("www.gmail.com", sync.date, th.theme, liste.liste);
            setSite("outlook.live.com", sync.date, th.theme, liste.liste);
            setSite("www.youtube.com", sync.date, th.theme, liste.liste);
            setSite("mail.google.com", sync.date, th.theme, liste.liste);
            setSite("localhost", sync.date, th.theme, liste.liste);
            ListeDynamique dyn = setListeDynamique("www.penofchaos.com", sync.date, th.theme);

            th = setListeTheme("Pornographie");
            MotCle mc = setMotCle("sex", 2, sync.date, th.theme);
            setSynonyme("sexe", mc.mot, sync.date);
            setSynonyme("seks", mc.mot, sync.date);
            liste = setListes("Liste Rouge");
            setSite("www.youporn.com", sync.date, th.theme, liste.liste);
            setSite("fr.youporn.com", sync.date, th.theme, liste.liste);
            setSite("www.sex.com", sync.date, th.theme, liste.liste);
            dyn = setListeDynamique("reallifecam.com", sync.date, th.theme);

            mc = setMotCle("porn", 15, sync.date, th.theme);

            mc = setMotCle("hentai", 15, sync.date, th.theme);

            mc = setMotCle("seins", 1, sync.date, th.theme);
            setSynonyme("tits", mc.mot, sync.date);
            setSynonyme("mamelon", mc.mot, sync.date);
            setSynonyme("boobs", mc.mot, sync.date);

            mc = setMotCle("chatte", 2, sync.date, th.theme);
            setSynonyme("kitten", mc.mot, sync.date);

            mc = setMotCle("fessée", 2, sync.date, th.theme);
            setSynonyme("spank", mc.mot, sync.date);

            mc = setMotCle("voyeur", 5, sync.date, th.theme);
            setSynonyme("peeping", mc.mot, sync.date);
            setSynonyme("voyeurisme", mc.mot, sync.date);

            mc = setMotCle("bite", 15, sync.date, th.theme);
            setSynonyme("queue", mc.mot, sync.date);
            setSynonyme("dick", mc.mot, sync.date);
            setSynonyme("cock", mc.mot, sync.date);

            mc = setMotCle("excite", 2, sync.date, th.theme);
            setSynonyme("excitation", mc.mot, sync.date);
            setSynonyme("exciter", mc.mot, sync.date);

            mc = setMotCle("partouze", 20, sync.date, th.theme);
            setSynonyme("orgy", mc.mot, sync.date);

            mc = setMotCle("pénétration", 5, sync.date, th.theme);

            mc = setMotCle("cheat", 10, sync.date, th.theme);
            setSynonyme("trompe", mc.mot, sync.date);
            setSynonyme("cheating", mc.mot, sync.date);

            mc = setMotCle("orgasm", 10, sync.date, th.theme);

            mc = setMotCle("masturbation", 10, sync.date, th.theme);

            mc = setMotCle("jouissance", 10, sync.date, th.theme);

            mc = setMotCle("salope", 10, sync.date, th.theme);
            setSynonyme("slut", mc.mot, sync.date);
            setSynonyme("whore", mc.mot, sync.date);
            setSynonyme("chienne", mc.mot, sync.date);


        }

        private Synchronisation setSynchro(DateTime dateSynchro)
        {
            Synchronisation sync = new Synchronisation(dateSynchro);
            bd.SetSynchro(sync);
            return sync;
        }

        private ListeTheme setListeTheme(string nomTheme)
        {
            ListeTheme th = new ListeTheme(nomTheme);
            bd.SetListeTheme(th);
            return th;
        }

        private Listes setListes(string nomListe)
        {
            Listes liste = new Listes(nomListe);
            bd.SetListe(liste);
            return liste;
        }

        private ListeDynamique setListeDynamique(string urlSite, DateTime dateSynchro, string nomTheme)
        {
            ListeDynamique dyn = new ListeDynamique(urlSite, dateSynchro, nomTheme);
            bd.SetListeDynamique(dyn);
            return dyn;
        }

        private void setTopo(string numSerie, DateTime dateSynchro)
        {
            Topologie topo = new Topologie(numSerie, dateSynchro);
            bd.SetTopologie(topo);
        }

        private void setSite(string urlSite, DateTime dateSynchro, string themeSite, string listeSite)
        {
            Sites site = new Sites(urlSite, dateSynchro, themeSite, listeSite);
            bd.SetSites(site);
        }

        private MotCle setMotCle(string mot, int valeur, DateTime dateSynchro, string themeMot)
        {
            MotCle mc = new MotCle(mot, valeur, dateSynchro, themeMot);
            bd.SetMotCle(mc);
            return mc;
        }


        private void setSynonyme(string synonyme, string mot, DateTime dateSynchro)
        {
            Synonyme syn = new Synonyme(synonyme, mot, dateSynchro);
            bd.SetSynonyme(syn);
        }
    }
}
