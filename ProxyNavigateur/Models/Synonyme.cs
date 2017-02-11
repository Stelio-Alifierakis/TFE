using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class Synonyme
    {
        public string mot { get; set; }
        public string fk_trad { get; set; }
        public DateTime DateAjout { get; set; }
        public DateTime fk_Date { get; set; }

        public Synonyme() { }

        public Synonyme(string mot, string fk_trad, DateTime DateAjout)
        {
            this.mot = mot;
            this.fk_trad = fk_trad;
            this.DateAjout = DateAjout;
        }

        public Synonyme(string mot, string fk_trad, DateTime DateAjout, DateTime fk_Date)
        {
            this.mot = mot;
            this.fk_trad = fk_trad;
            this.DateAjout = DateAjout;
            this.fk_Date = fk_Date;
        }
    }
}
