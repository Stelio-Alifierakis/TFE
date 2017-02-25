using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class ListeDynamique
    {
        public string url { get; set; }
        public DateTime DateAjout { get; set; }
        public DateTime fk_Date { get; set; }
        public string fk_theme { get; set; }
        public ListeTheme theme { get; set; }

        public ListeDynamique() { }

        public ListeDynamique(string url, DateTime DateAjout)
        {
            this.url = url;
            this.DateAjout = DateAjout;
        }

        public ListeDynamique(string url, DateTime DateAjout, string fk_theme)
        {
            this.url = url;
            this.DateAjout = DateAjout;
            this.fk_theme = fk_theme;
        }

        public ListeDynamique(string url, DateTime DateAjout, DateTime fk_Date, string fk_theme)
        {
            this.url = url;
            this.DateAjout = DateAjout;
            this.fk_Date = fk_Date;
            this.fk_theme = fk_theme;
        }
    }
}
