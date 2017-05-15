using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur.DB;
using ProxyNavigateur.Models;

namespace ProxyNavigateur
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class DAL : IDAL
    {
        /// <summary>
        /// Variable qui va stocker la base de données
        /// </summary>
        private BDDInterface bd;

        /// <summary>
        /// Constructeur
        /// </summary>
        public DAL()
        {
            
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nomBase">Paramètre qui stocke le nom de la base de données</param>
        public DAL(string nomBase)
        {
            bd = new db(nomBase);
        }

        /// <summary>
        /// Constructeur de la base de données
        /// </summary>
        /// <param name="bd">Variable qui stocke une base de données déjà existante</param>
        public DAL(db bd)
        {
            this.bd = bd;
        }

        /// <summary>
        /// Fonction qui va appeler la création de la base de données de l'interface <c>BDDInterface</c>
        /// </summary>
        public void creation()
        {
            bd.creationTables();
        }

        /// <summary>
        /// Fonction qui va appeler la fonction de seed de la base de donnée de l'interface <c>BDDInterface</c>
        /// </summary>
        public void seed()
        {
            bd.seeder();
        }

        /// <summary>
        /// Fonction qui va supprimer la base de données
        /// </summary>
        public void suppressionDB()
        {
            bd.suppressionDB();
        }

        /// <summary>
        /// Setter de la synchronisation
        /// </summary>
        /// <param name="date">Paramètre de date</param>
        public void setSyncrho(DateTime date)
        {
            Synchronisation sync = new Synchronisation(date);
        }

        /// <summary>
        /// getter de la synchronisation
        /// </summary>
        /// <param name="date">paramètre de date</param>
        /// <returns></returns>
        public Synchronisation GetSynchro(DateTime date)
        {
            return bd.GetSynchro(date);
        }

        /// <summary>
        /// récupère une liste de synchronisation sous forme de <c>IEnumerable</c>
        /// </summary>
        /// <returns>Liste de synchronisation</returns>
        public IEnumerable<Synchronisation> GetSynchros()
        {
            return bd.GetSynchros();
        }

        /// <summary>
        /// Fonction qui va crééer un champ liste dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="liste">nom de la liste à ajouter</param>
        public void SetListe(string liste)
        {
            Listes l = new Listes(liste);
            bd.SetListe(l);
        }

        /// <summary>
        /// Fonction qui va récupérer un champs liste depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="ms">nom de la liste à récupérer</param>
        /// <returns>Champ liste voulu</returns>
        public Listes GetListes(string ms)
        {
            return bd.GetListes(ms);
        }

        /// <summary>
        /// Récupération de toutes les listes depuis <c>BDDInterface</c>
        /// </summary>
        /// <returns>Listes de BDDInterface</returns>
        public IEnumerable<Listes> GetListes()
        {
            return bd.GetListes();
        }

        /// <summary>
        /// Fonction qui va crééer un champ thème dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="theme">nom du thème</param>
        public void SetListeTheme(string theme)
        {
            ListeTheme lt = new ListeTheme(theme);
            bd.SetListeTheme(lt);
        }

        /// <summary>
        /// Fonction qui va récupérer un champ thème depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="ms">nom du thème à récupérer</param>
        /// <returns>thème à récupérer</returns>
        public ListeTheme GetTheme(string ms)
        {
            return bd.GetTheme(ms);
        }

        /// <summary>
        /// Fonction qui va récupérer tous les thèmes depuis <c>BDDInterface</c> sous forme de <c>IEnumerable</c>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ListeTheme> GetListeThemes()
        {
            return GetListeThemes();
        }

        /// <summary>
        /// Fonction qui va initialiser une machine dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="idMachine">Numéro d'ID de la machine</param>
        /// <param name="fk_Date">date de création/modifiction</param>
        public void SetTopologie(string idMachine, DateTime fk_Date)
        {
            Topologie topo = new Topologie(idMachine,fk_Date);
            bd.SetTopologie(topo);
        }

        /// <summary>
        /// Fonction qui va récupérer une machine depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="ms">ID de la machine</param>
        /// <returns>Machine</returns>
        public Topologie GetTopo(string ms)
        {
            return bd.GetTopo(ms);
        }

        /// <summary>
        /// Fonction qui va récupérer toutes les machines depuis <c>BDDInterface</c> sous forme de <c>IEnumerable</c>
        /// </summary>
        /// <returns>Liste des machines</returns>
        public IEnumerable<Topologie> GetTopos()
        {
            return GetTopos();
        }

        /// <summary>
        /// Fonction qui va crééer un champ site dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="nomSite">Nom du site</param>
        /// <param name="DateAjout">Date d'ajout</param>
        public void SetSites(string nomSite, DateTime DateAjout)
        {
            Sites s = new Sites(nomSite, DateAjout);
            bd.SetSites(s);
        }

        /// <summary>
        /// Fonction qui va crééer un champ site dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="nomSite">Nom du site</param>
        /// <param name="DateAjout">Date d'ajout</param>
        /// <param name="fk_theme">Clé étrangère du thème (nom)</param>
        /// <param name="fk_liste">Clé étrangère de la liste (nom)</param>
        public void SetSites(string nomSite, DateTime DateAjout, string fk_theme, string fk_liste)
        {
            Sites s = new Sites(nomSite, DateAjout, fk_theme, fk_liste);
            bd.SetSites(s);
        }

        /// <summary>
        /// Fonction qui va crééer un champ site dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="nomSite">Nom du site</param>
        /// <param name="DateAjout">Date d'ajout</param>
        /// <param name="fk_Date">Clé étrangère de la date</param>
        /// <param name="fk_theme">Clé étrangère du thème (nom)</param>
        /// <param name="fk_liste">Clé étrangère de la liste (nom)</param>
        public void SetSites(string nomSite, DateTime DateAjout, DateTime fk_Date, string fk_theme, string fk_liste)
        {
            Sites s = new Sites(nomSite, DateAjout, fk_Date, fk_theme, fk_liste);
            bd.SetSites(s);
        }

        /// <summary>
        /// Fonction qui va récupérer un champ site depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="ms">Nom du site</param>
        /// <returns>Site</returns>
        public Sites GetSite(string ms)
        {
            return bd.GetSite(ms);
        }

        /// <summary>
        /// Fonction qui va récupérer tous les sites depuis <c>BDDInterface</c> sous forme de <c>IEnumerable</c>
        /// </summary>
        /// <returns>Liste de tous les sites</returns>
        public IEnumerable<Sites> GetSites()
        {
            return bd.GetSites();
        }

        /// <summary>
        /// Fonction qui va créer un champ liste dynamique dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="url">URL du site</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        public void SetListeDynamique(string url, DateTime DateAjout)
        {
            ListeDynamique ls = new ListeDynamique(url, DateAjout);
            bd.SetListeDynamique(ls);
        }

        /// <summary>
        /// Fonction qui va créer un champ liste dynamique dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="url">URL du site</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        /// <param name="fk_theme">Clé étrangère du thème</param>
        public void SetListeDynamique(string url, DateTime DateAjout, string fk_theme)
        {
            ListeDynamique ls = new ListeDynamique(url, DateAjout, fk_theme);
            bd.SetListeDynamique(ls);
        }

        /// <summary>
        /// Fonction qui va créer un champ liste dynamique dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="url">URL du site</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        /// <param name="fk_Date">Clé étrangère de la date</param>
        /// <param name="fk_theme">Clé étrangère du thème</param>
        public void SetListeDynamique(string url, DateTime DateAjout, DateTime fk_Date, string fk_theme)
        {
            ListeDynamique ls = new ListeDynamique(url, DateAjout, fk_Date, fk_theme);
            bd.SetListeDynamique(ls);
        }

        /// <summary>
        /// Fonction qui va récupérer un champ liste dynamique depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="ms">URL de la liste dynamique</param>
        /// <returns>champ liste dynamique</returns>
        public ListeDynamique GetListeDynamique(string ms)
        {
            return bd.GetListeDynamique(ms);
        }

        /// <summary>
        /// Fonction qui va récupérer tous les champs liste dynamique depuis <c>BDDInterface</c> sous forme de <c>IEnumerable</c>
        /// </summary>
        /// <returns>Liste des entrées dynamiques</returns>
        public IEnumerable<ListeDynamique> GetListeDynamiques()
        {
            return bd.GetListeDynamiques();
        }

        /// <summary>
        /// Fonction qui va récupérer tous les champs liste dynamique depuis <c>BDDInterface</c> sous forme de liste
        /// </summary>
        /// <param name="nomTheme">Nom du thème</param>
        /// <returns>Liste des entrées dynamique</returns>
        public List<ListeDynamique> GetListeDynamiques(string nomTheme)
        {
            List<ListeDynamique> dyn = bd.GetListeDynamiques(nomTheme).ToList();
            return dyn;
        }

        /// <summary>
        /// Fonction qui va créer un champ mot-clé dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="mot">Mot</param>
        /// <param name="valeur">Valeur attribuée au mot</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        public void SetMotCle(string mot, int valeur, DateTime DateAjout)
        {
            MotCle mc = new MotCle(mot, valeur, DateAjout);
            bd.SetMotCle(mc);
        }

        /// <summary>
        /// Fonction qui va créer un champ mot-clé dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="mot">Mot</param>
        /// <param name="valeur">Valeur attribuée au mot</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        /// <param name="fk_theme">Clé étrangère du thème</param>
        public void SetMotCle(string mot, int valeur, DateTime DateAjout, string fk_theme)
        {
            MotCle mc = new MotCle(mot, valeur, DateAjout, fk_theme);
            bd.SetMotCle(mc);
        }

        /// <summary>
        /// Fonction qui va créer un champ mot-clé dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="mot">Mot</param>
        /// <param name="valeur">Valeur attribuée au mot</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        /// <param name="fk_Date">Clé étrangère de la date (date)</param>
        /// <param name="fk_theme">Clé étrangère du thème (nom)</param>
        public void SetMotCle(string mot, int valeur, DateTime DateAjout, DateTime fk_Date, string fk_theme)
        {
            MotCle mc = new MotCle(mot, valeur, DateAjout, fk_Date, fk_theme);
            bd.SetMotCle(mc);
        }

        /// <summary>
        /// Fonction qui va récupérer un champ mot-clé depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="ms">Le mot-clé</param>
        /// <returns>Mot-clé</returns>
        public MotCle GetMotCle(string ms)
        {
            return bd.GetMotCle(ms);
        }

        /// <summary>
        /// Fonction qui va récupérer tous les champs mot-clé depuis <c>BDDInterface</c> sous forme de <c>IEnumerable</c>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MotCle> GetMotCles()
        {
            return bd.GetMotCles();
        }

        /// <summary>
        /// Fonction qui va créer un champ synonyme dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="mot">Mot</param>
        /// <param name="fk_trad">Clé étrangère mot (mot synonyme)</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        public void SetSynonyme(string mot, string fk_trad, DateTime DateAjout)
        {
            Synonyme syn = new Synonyme(mot, fk_trad, DateAjout);
            bd.SetSynonyme(syn);
        }

        /// <summary>
        /// Fonction qui va créer un champ synonyme dans <c>BDDInterface</c>
        /// </summary>
        /// <param name="mot">Mot</param>
        /// <param name="fk_trad">Clé étrangère mot (mot synonyme)</param>
        /// <param name="DateAjout">Date de l'ajout</param>
        /// <param name="fk_Date">Clé étrangère de la date (date)</param>
        public void SetSynonyme(string mot, string fk_trad, DateTime DateAjout, DateTime fk_Date)
        {
            Synonyme syn = new Synonyme(mot, fk_trad, DateAjout, fk_Date);
            bd.SetSynonyme(syn);
        }

        /// <summary>
        /// Fonction qui va récupérer un champ synonyme depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="ms">Mot</param>
        /// <returns>Mot synonyme</returns>
        public Synonyme GetSynonyme(string ms)
        {
            return bd.GetSynonyme(ms);
        }

        /// <summary>
        /// Fonction qui va récupérer tous les champs synonymes depuis <c>BDDInterface</c> sous forme de <c>IEnumerable</c>
        /// </summary>
        /// <returns>Liste des synonyme</returns>
        public IEnumerable<Synonyme> GetSynonymes()
        {
            return bd.GetSynonymes();
        }

        /// <summary>
        /// Lance la fonction de <c>BDDInterface</c> qui va vérifier si le site est valide.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <remarks>La fonction est obsolète et ne marche pas</remarks>
        [System.Obsolete]
        public bool verifSite(string msg)
        {
            return bd.verifSite(msg);
        }

        /// <summary>
        /// Lance la fonction qui va vérifier si un mot se trouve dans une phrase depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="phrase">Variable qui stocke la phrase à verifier</param>
        /// <returns>Retourne vrai ou faux</returns>
        public bool checkPartWord(string phrase)
        {
            return bd.checkPartWord(phrase);
        }

        /// <summary>
        /// Lance la fonction de <c>BDDInterface</c> qui va retourner la valeur d'un mot-clé
        /// </summary>
        /// <param name="mot">Mot-clé</param>
        /// <returns>Valeur retournée</returns>
        public int retourVal(string mot)
        {
            return bd.retourVal(mot);
        }

        /// <summary>
        /// Lance la fonction qui retourne les thèmes depuis <c>BDDInterface</c>
        /// </summary>
        /// <param name="word">Mot-clé ou synonyme</param>
        /// <returns>Liste de mot-clé à retourner</returns>
        public string returnTheme(string word)
        {
            return bd.returnTheme(word);
        }

        /// <summary>
        /// Fonction qui va retourner la liste des sites
        /// </summary>
        /// <param name="nomListe">Nom de la liste sur laquelle chercher</param>
        /// <returns>Liste des sites</returns>
        public List<Sites> retourSites(string nomListe)
        {
            switch (nomListe)
            {
                case "Liste Verte":
                case "Liste Rouge":
                    List<Sites> l1 = bd.getURL(nomListe).ToList();
                    return l1;
                default:
                    //throw new Exception("Invalide liste");
                    return null;
            }            
        }

        /// <summary>
        /// Fonction qui appelle la fonction de <c>BDDInterface</c> qui cherche la valeur d'un mot interdit
        /// </summary>
        /// <returns>Retourne les valeurs des mots interdits</returns>
        public Dictionary<string, int> motsInterditVal()
        {
            return bd.motsInterditVal();
        }

        /// <summary>
        /// Récupère la liste des mot-clé "interdit" depuis <c>BDDInterface</c> sous forme de <c>Dictionary</c>
        /// </summary>
        /// <returns>Liste des mot-clé</returns>
        public Dictionary<string, int> motsInterdit()
        {
            return bd.motsInterdit();
        }

        /// <summary>
        /// Récupère la liste des thèmes depuis <c>BDDInterface</c> sous forme de <c>Dictionary</c>
        /// </summary>
        /// <returns>Liste des thèmes</returns>
        public Dictionary<string, string> retourTheme()
        {
            return bd.retourTheme();
        }

        /// <summary>
        /// Récupère la liste des thèmes depuis <c>BDDInterface</c> sous forme de <c>Dictionary</c>
        /// </summary>
        /// <returns>Liste des thèmes</returns>
        public Dictionary<string, int> themeVal()
        {
            return bd.themeVal();
        }

        /// <summary>
        /// Récupère la liste des valeurs booléenne des sites depuis <c>BDDInterface</c> sous forme de <c>Dictionary</c>
        /// </summary>
        /// <returns>Liste des valeurs booléenne</returns>
        public Dictionary<string, bool> listeSiteBool()
        {
            return bd.retourListeSites();
        }

        /// <summary>
        /// Récupère la liste des valeurs booléennes des sites dynamique depuis <c>BDDInterface</c> sous forme de <c>Dictionary</c>
        /// </summary>
        /// <returns>Liste des valeurs booléennes des sites dynamiques</returns>
        public Dictionary<string, bool> retourListeDynamiqueSites()
        {
            return bd.retourListeDynamiqueSites();
        }
    }
}
