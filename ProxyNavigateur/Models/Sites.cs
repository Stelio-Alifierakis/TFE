using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class Sites
    {
        public string nomSite { get; set; }
        public DateTime DateAjout { get; set; }
        public DateTime fk_Date { get; set; }
        public string fk_theme { get; set; }
        public string fk_liste { get; set; }
        public Listes Listes { get; set; }

        public Sites() { }

        public Sites(string nomSite, DateTime DateAjout)
        {
            this.nomSite = nomSite;
            this.DateAjout = DateAjout;
        }

        public Sites(string nomSite, DateTime DateAjout, string fk_theme, string fk_liste)
        {
            this.nomSite = nomSite;
            this.DateAjout = DateAjout;
            this.fk_theme = fk_theme;
            this.fk_liste = fk_liste;
        }

        public Sites(string nomSite, DateTime DateAjout, DateTime fk_Date, string fk_theme, string fk_liste)
        {
            this.nomSite = nomSite;
            this.DateAjout = DateAjout;
            this.fk_Date = fk_Date;
            this.fk_theme = fk_theme;
            this.fk_liste = fk_liste;
        }
    }
}
