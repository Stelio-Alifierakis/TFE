using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using proxy;
using Profileur;

namespace Communicateur.ComProxy
{
    public class ComProxy : IProxyCom
    {

        public Activitateur ActiveParContenu { get; set; }

        public Activitateur ActiveParURL { get; set; }

        public IListUtilisateur ListeUtilisateur { get; set; }

        public Utilisateur Utilisateur { get; set; }


        public Activitateur getActifContenu()
        {
            return this.ActiveParContenu;
        }

        public void setActifContenu(Activitateur ActiveParContenu)
        {
            this.ActiveParContenu = ActiveParContenu;
        }

        public Activitateur getActifUrl()
        {
            return this.ActiveParURL;
        }

        public void setActifUrl(Activitateur ActiveParUrl)
        {
            this.ActiveParURL = ActiveParUrl;
        }

        public IListUtilisateur getListUtilisateur()
        {
            return this.ListeUtilisateur;
        }

        public void setListUtilisateur(IListUtilisateur ListeUtilisateur)
        {
            this.ListeUtilisateur = ListeUtilisateur;
        }

        public Utilisateur getUtilisateur()
        {
            return this.Utilisateur;
        }

        public void setUtilisateur(Utilisateur Utilisateur)
        {
            this.Utilisateur = Utilisateur;
        }


        public Activitateur RetourActiveParContenu()
        {
            return ActiveParContenu;
        }

        public Activitateur RetourActiveParURL()
        {
            //throw new NotImplementedException();
            return ActiveParURL;
        }

        public IListUtilisateur RetourListeUtilisateur()
        {
            //throw new NotImplementedException();
            return ListeUtilisateur;
        }

        public Utilisateur RetourUtilisateur()
        {
            //throw new NotImplementedException();
            return Utilisateur;
        }
    }
}
