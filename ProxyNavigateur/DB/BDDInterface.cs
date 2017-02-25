using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyNavigateur.Models;

namespace ProxyNavigateur.DB
{
    public interface BDDInterface
    {
        void creationTables();
        void seeder();

        void suppressionDB();

        void SetSynchro(Synchronisation synch);
        Synchronisation GetSynchro(DateTime date);
        IEnumerable<Synchronisation> GetSynchros();

        void SetListe(Listes l);
        Listes GetListes(string ms);
        IEnumerable<Listes> GetListes();

        void SetListeTheme(ListeTheme theme);
        ListeTheme GetTheme(string ms);
        IEnumerable<ListeTheme> GetListeThemes();

        void SetTopologie(Topologie topo);
        Topologie GetTopo(string ms);
        //Topologie GetTopo(DateTime date);
        IEnumerable<Topologie> GetTopos();

        void SetSites(Sites site);
        Sites GetSite(string ms);
        IEnumerable<Sites> GetSites();

        void SetListeDynamique(ListeDynamique dyn);
        ListeDynamique GetListeDynamique(string ms);
        IEnumerable<ListeDynamique> GetListeDynamiques();
        IEnumerable<ListeDynamique> GetListeDynamiques(string nomTheme);

        void SetMotCle(MotCle mc);
        MotCle GetMotCle(string ms);
        IEnumerable<MotCle> GetMotCles();

        void SetSynonyme(Synonyme syn);
        Synonyme GetSynonyme(string ms);
        IEnumerable<Synonyme> GetSynonymes();

        bool verifSite(string msg);
        bool checkPartWord(string phrase);

        int retourVal(string mot);

        string returnTheme(string word);

        IEnumerable<Sites> getURL(string listeURL);
    }
}
