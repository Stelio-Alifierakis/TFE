using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseDonnees;
using BaseDonnees.Models;

namespace Rechercheur
{
    /// <summary>
    /// Interface qui sert de contrat à la classe <c>Rechercheur</c>
    /// </summary>
    public interface IRechercheur
    {
        bool goodWord(string msg);
        bool checkWord(string msg);
        bool checkPartWord(string msg);
        double valPhrase(string phrase);
        List<Sites> getListeSites(string nomListe);
        List<ListeDynamique> GetListeDynamiques(string nomTheme);
        string themePage(string phrase);
        int checkUrl(string url);
        void setBdd(IDAL bdd);
        void MiseAjourListeDyn(string url, string theme);
    }
}
