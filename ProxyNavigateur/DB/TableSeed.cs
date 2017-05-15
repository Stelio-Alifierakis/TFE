using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur.Models;

namespace ProxyNavigateur.DB
{
    /// <summary>
    /// Classe qui va envoyer les seed de la base de données
    /// </summary>
    class TableSeed
    {
        /// <summary>
        /// Variable qui contient la base de données
        /// </summary>
        public BDDInterface bd;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="bd">Base de données</param>
        public TableSeed(BDDInterface bd)
        {
            this.bd = bd;
        }

        /// <summary>
        /// Fonction qui envoie le seed de la base de données
        /// </summary>
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

        /// <summary>
        /// Fonction privée qui seed une synchronisation et renvoie la date de synchronisation
        /// </summary>
        /// <param name="dateSynchro">Date</param>
        /// <returns>Synchronisation</returns>
        private Synchronisation setSynchro(DateTime dateSynchro)
        {
            Synchronisation sync = new Synchronisation(dateSynchro);
            bd.SetSynchro(sync);
            return sync;
        }

        /// <summary>
        /// Fonction privée qui seed un thème et renvoie le thème
        /// </summary>
        /// <param name="nomTheme"></param>
        /// <returns></returns>
        private ListeTheme setListeTheme(string nomTheme)
        {
            ListeTheme th = new ListeTheme(nomTheme);
            bd.SetListeTheme(th);
            return th;
        }

        /// <summary>
        /// Fonction privée qui seed une liste et renvoie la liste
        /// </summary>
        /// <param name="nomListe">Nom de la liste</param>
        /// <returns>Liste</returns>
        private Listes setListes(string nomListe)
        {
            Listes liste = new Listes(nomListe);
            bd.SetListe(liste);
            return liste;
        }

        /// <summary>
        /// Fonction privée qui seed un site dynamique et renvoie un site dynamique
        /// </summary>
        /// <param name="urlSite">L'URL du site</param>
        /// <param name="dateSynchro">La date de la synchronisation</param>
        /// <param name="nomTheme">Le nom du thème</param>
        /// <returns></returns>
        private ListeDynamique setListeDynamique(string urlSite, DateTime dateSynchro, string nomTheme)
        {
            ListeDynamique dyn = new ListeDynamique(urlSite, dateSynchro, nomTheme);
            bd.SetListeDynamique(dyn);
            return dyn;
        }

        /// <summary>
        /// Fonction privée qui seed une topologie
        /// </summary>
        /// <param name="numSerie">ID de machine</param>
        /// <param name="dateSynchro">La date de la synchronisation</param>
        private void setTopo(string numSerie, DateTime dateSynchro)
        {
            Topologie topo = new Topologie(numSerie, dateSynchro);
            bd.SetTopologie(topo);
        }

        /// <summary>
        /// Fonction privée qui seed un site
        /// </summary>
        /// <param name="urlSite">L'URL du site</param>
        /// <param name="dateSynchro">La date de la synchronisation</param>
        /// <param name="themeSite">Le thème du site</param>
        /// <param name="listeSite">La nom de la liste</param>
        private void setSite(string urlSite, DateTime dateSynchro, string themeSite, string listeSite)
        {
            Sites site = new Sites(urlSite, dateSynchro, themeSite, listeSite);
            bd.SetSites(site);
        }

        /// <summary>
        /// Fonction privée qui seed un mot-clé et le renvoie
        /// </summary>
        /// <param name="mot">Le mot</param>
        /// <param name="valeur">La valeur du mot</param>
        /// <param name="dateSynchro">La date de synchronisation du mot</param>
        /// <param name="themeMot">Le thème du mot</param>
        /// <returns>Mot-clé</returns>
        private MotCle setMotCle(string mot, int valeur, DateTime dateSynchro, string themeMot)
        {
            MotCle mc = new MotCle(mot, valeur, dateSynchro, themeMot);
            bd.SetMotCle(mc);
            return mc;
        }

        /// <summary>
        /// Fonction privée qui seed les synonymes
        /// </summary>
        /// <param name="synonyme">Le mot synonyme</param>
        /// <param name="mot">Le mot d'origine</param>
        /// <param name="dateSynchro">La date de synchronisation</param>
        private void setSynonyme(string synonyme, string mot, DateTime dateSynchro)
        {
            Synonyme syn = new Synonyme(synonyme, mot, dateSynchro);
            bd.SetSynonyme(syn);
        }
    }
}
