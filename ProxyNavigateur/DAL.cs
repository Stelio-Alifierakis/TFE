using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur.DB;
using ProxyNavigateur.Models;

namespace ProxyNavigateur
{
    public class DAL
    {
        private BDDInterface bd;

        public DAL()
        {
            
        }

        public DAL(string nomBase)
        {
            bd = new db(nomBase);
        }

        public DAL(db bd)
        {
            this.bd = bd;
        }

        public void creation()
        {
            bd.creationTables();
        }

        public void seed()
        {
            bd.seeder();
        }

        public void suppressionDB()
        {
            bd.suppressionDB();
        }

        public void setSyncrho(DateTime date)
        {
            Synchronisation sync = new Synchronisation(date);
        }

        public Synchronisation GetSynchro(DateTime date)
        {
            return bd.GetSynchro(date);
        }

        public IEnumerable<Synchronisation> GetSynchros()
        {
            return bd.GetSynchros();
        }

        public void SetListe(string liste)
        {
            Listes l = new Listes(liste);
            bd.SetListe(l);
        }

        public Listes GetListes(string ms)
        {
            return bd.GetListes(ms);
        }

        public IEnumerable<Listes> GetListes()
        {
            return bd.GetListes();
        }

        public void SetListeTheme(string theme)
        {
            ListeTheme lt = new ListeTheme(theme);
            bd.SetListeTheme(lt);
        }

        public ListeTheme GetTheme(string ms)
        {
            return bd.GetTheme(ms);
        }

        public IEnumerable<ListeTheme> GetListeThemes()
        {
            return GetListeThemes();
        }

        public void SetTopologie(string idMachine, DateTime fk_Date)
        {
            Topologie topo = new Topologie(idMachine,fk_Date);
            bd.SetTopologie(topo);
        }

        public Topologie GetTopo(string ms)
        {
            return bd.GetTopo(ms);
        }

        public IEnumerable<Topologie> GetTopos()
        {
            return GetTopos();
        }

        public void SetSites(string nomSite, DateTime DateAjout)
        {
            Sites s = new Sites(nomSite, DateAjout);
            bd.SetSites(s);
        }

        public void SetSites(string nomSite, DateTime DateAjout, string fk_theme, string fk_liste)
        {
            Sites s = new Sites(nomSite, DateAjout, fk_theme, fk_liste);
            bd.SetSites(s);
        }

        public void SetSites(string nomSite, DateTime DateAjout, DateTime fk_Date, string fk_theme, string fk_liste)
        {
            Sites s = new Sites(nomSite, DateAjout, fk_Date, fk_theme, fk_liste);
            bd.SetSites(s);
        }

        public Sites GetSite(string ms)
        {
            return bd.GetSite(ms);
        }

        public IEnumerable<Sites> GetSites()
        {
            return bd.GetSites();
        }

        public void SetListeDynamique(string url, DateTime DateAjout)
        {
            ListeDynamique ls = new ListeDynamique(url, DateAjout);
            bd.SetListeDynamique(ls);
        }

        public void SetListeDynamique(string url, DateTime DateAjout, string fk_theme)
        {
            ListeDynamique ls = new ListeDynamique(url, DateAjout, fk_theme);
            bd.SetListeDynamique(ls);
        }

        public void SetListeDynamique(string url, DateTime DateAjout, DateTime fk_Date, string fk_theme)
        {
            ListeDynamique ls = new ListeDynamique(url, DateAjout, fk_Date, fk_theme);
            bd.SetListeDynamique(ls);
        }

        public ListeDynamique GetListeDynamique(string ms)
        {
            return bd.GetListeDynamique(ms);
        }

        public IEnumerable<ListeDynamique> GetListeDynamiques()
        {
            return bd.GetListeDynamiques();
        }

        public List<ListeDynamique> GetListeDynamiques(string nomTheme)
        {
            List<ListeDynamique> dyn = bd.GetListeDynamiques(nomTheme).ToList();
            return dyn;
        }

        public void SetMotCle(string mot, int valeur, DateTime DateAjout)
        {
            MotCle mc = new MotCle(mot, valeur, DateAjout);
            bd.SetMotCle(mc);
        }

        public void SetMotCle(string mot, int valeur, DateTime DateAjout, string fk_theme)
        {
            MotCle mc = new MotCle(mot, valeur, DateAjout, fk_theme);
            bd.SetMotCle(mc);
        }

        public void SetMotCle(string mot, int valeur, DateTime DateAjout, DateTime fk_Date, string fk_theme)
        {
            MotCle mc = new MotCle(mot, valeur, DateAjout, fk_Date, fk_theme);
            bd.SetMotCle(mc);
        }

        public MotCle GetMotCle(string ms)
        {
            return bd.GetMotCle(ms);
        }

        public IEnumerable<MotCle> GetMotCles()
        {
            return bd.GetMotCles();
        }

        public void SetSynonyme(string mot, string fk_trad, DateTime DateAjout)
        {
            Synonyme syn = new Synonyme(mot, fk_trad, DateAjout);
            bd.SetSynonyme(syn);
        }

        public void SetSynonyme(string mot, string fk_trad, DateTime DateAjout, DateTime fk_Date)
        {
            Synonyme syn = new Synonyme(mot, fk_trad, DateAjout, fk_Date);
            bd.SetSynonyme(syn);
        }

        public Synonyme GetSynonyme(string ms)
        {
            return bd.GetSynonyme(ms);
        }

        public IEnumerable<Synonyme> GetSynonymes()
        {
            return bd.GetSynonymes();
        }

        public bool verifSite(string msg)
        {
            return bd.verifSite(msg);
        }

        public bool checkPartWord(string phrase)
        {
            return bd.checkPartWord(phrase);
        }

        public int retourVal(string mot)
        {
            return bd.retourVal(mot);
        }

        public string returnTheme(string word)
        {
            return bd.returnTheme(word);
        }

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

        public Dictionary<string, int> motsInterditVal()
        {
            return bd.motsInterditVal();
        }

        public Dictionary<string, int> motsInterdit()
        {
            return bd.motsInterdit();
        }

        public Dictionary<string, string> retourTheme()
        {
            return bd.retourTheme();
        }

        public Dictionary<string, int> themeVal()
        {
            return bd.themeVal();
        }

        public Dictionary<string, bool> listeSiteBool()
        {
            return bd.retourListeSites();
        }

        public Dictionary<string, bool> retourListeDynamiqueSites()
        {
            return bd.retourListeDynamiqueSites();
        }
    }
}
