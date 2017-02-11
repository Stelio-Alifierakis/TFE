using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class MotCle
    {
        public string mot { get; set; }
        public int valeur { get; set; }
        public DateTime DateAjout { get; set; }
        public DateTime fk_Date { get; set; }
        public string fk_theme { get; set; }
        public Synonyme Synonyme { get; set; }

        public MotCle() { }

        public MotCle(string mot, int valeur, DateTime DateAjout)
        {
            this.mot = mot;
            this.valeur = valeur;
            this.DateAjout = DateAjout;
        }

        public MotCle(string mot, int valeur, DateTime DateAjout, string fk_theme)
        {
            this.mot = mot;
            this.valeur = valeur;
            this.DateAjout = DateAjout;
            this.fk_theme = fk_theme;
        }

        public MotCle(string mot, int valeur, DateTime DateAjout, DateTime fk_Date, string fk_theme)
        {
            this.mot = mot;
            this.valeur = valeur;
            this.DateAjout = DateAjout;
            this.fk_Date = fk_Date;
            this.fk_theme = fk_theme;
        }
    }
}
