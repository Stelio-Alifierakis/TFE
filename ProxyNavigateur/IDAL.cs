using BaseDonnees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDonnees
{

    /// <summary>
    /// Interface de la couche d'accès de la base de données
    /// </summary>
    public interface IDAL
    {
        void creation();
        void seed();
        void suppressionDB();
        void setSyncrho(DateTime date);
        Synchronisation GetSynchro(DateTime date);
        IEnumerable<Synchronisation> GetSynchros();
        void SetListe(string liste);
        Listes GetListes(string ms);
        IEnumerable<Listes> GetListes();
        void SetListeTheme(string theme);
        ListeTheme GetTheme(string ms);
        IEnumerable<ListeTheme> GetListeThemes();
        void SetTopologie(string idMachine, DateTime fk_Date);
        Topologie GetTopo(string ms);
        IEnumerable<Topologie> GetTopos();
        void SetSites(string nomSite, DateTime DateAjout);
        void SetSites(string nomSite, DateTime DateAjout, string fk_theme, string fk_liste);
        void SetSites(string nomSite, DateTime DateAjout, DateTime fk_Date, string fk_theme, string fk_liste);
        Sites GetSite(string ms);
        IEnumerable<Sites> GetSites();
        void SetListeDynamique(string url, DateTime DateAjout);
        void SetListeDynamique(string url, DateTime DateAjout, string fk_theme);
        void SetListeDynamique(string url, DateTime DateAjout, DateTime fk_Date, string fk_theme);
        ListeDynamique GetListeDynamique(string ms);
        IEnumerable<ListeDynamique> GetListeDynamiques();
        void SetMotCle(string mot, int valeur, DateTime DateAjout);
        void SetMotCle(string mot, int valeur, DateTime DateAjout, string fk_theme);
        void SetMotCle(string mot, int valeur, DateTime DateAjout, DateTime fk_Date, string fk_theme);
        MotCle GetMotCle(string ms);
        IEnumerable<MotCle> GetMotCles();
        void SetSynonyme(string mot, string fk_trad, DateTime DateAjout);
        void SetSynonyme(string mot, string fk_trad, DateTime DateAjout, DateTime fk_Date);
        Synonyme GetSynonyme(string ms);
        IEnumerable<Synonyme> GetSynonymes();
        bool verifSite(string msg);
        bool checkPartWord(string phrase);
        int retourVal(string mot);
        string returnTheme(string word);
        Dictionary<string, int> motsInterditVal();
        Dictionary<string, int> motsInterdit();
        Dictionary<string, string> retourTheme();
        Dictionary<string, int> themeVal();
        Dictionary<string, bool> listeSiteBool();
        Dictionary<string, bool> retourListeDynamiqueSites();
        List<Sites> retourSites(string nomListe);
        List<ListeDynamique> GetListeDynamiques(string nomTheme);
    }
}
