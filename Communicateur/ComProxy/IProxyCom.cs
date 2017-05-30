using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using proxy;
using Profileur;

namespace Communicateur.ComProxy
{
    public interface IProxyCom
    {
        Activitateur RetourActiveParURL();
        Activitateur RetourActiveParContenu();
        IListUtilisateur RetourListeUtilisateur();
        Utilisateur RetourUtilisateur();

        Activitateur getActifContenu();
        void setActifContenu(Activitateur ActiveParContenu);
        Activitateur getActifUrl();
        void setActifUrl(Activitateur ActiveParUrl);
        IListUtilisateur getListUtilisateur();
        void setListUtilisateur(IListUtilisateur ListeUtilisateur);
        Utilisateur getUtilisateur();
        void setUtilisateur(Utilisateur Utilisateur);
    }
}
