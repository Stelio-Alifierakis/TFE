using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    /// <summary>
    /// Modèle de synonymes
    /// </summary>
    public sealed class Synonyme
    {
        /// <summary>
        /// Le mot synonyme
        /// </summary>
        public string mot { get; set; }

        /// <summary>
        /// Le mot d'origine
        /// </summary>
        public string fk_trad { get; set; }

        /// <summary>
        /// La date d'ajout
        /// </summary>
        public DateTime DateAjout { get; set; }

        /// <summary>
        /// La date de synchronisation
        /// </summary>
        public DateTime fk_Date { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Synonyme() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mot">Le mot synonyme</param>
        /// <param name="fk_trad">Le mot d'origine</param>
        /// <param name="DateAjout">La date d'ajout</param>
        public Synonyme(string mot, string fk_trad, DateTime DateAjout)
        {
            this.mot = mot;
            this.fk_trad = fk_trad;
            this.DateAjout = DateAjout;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mot">Le mot synonyme</param>
        /// <param name="fk_trad">Le mot d'origine</param>
        /// <param name="DateAjout">La date d'ajout</param>
        /// <param name="fk_Date">La date de synchronisation</param>
        public Synonyme(string mot, string fk_trad, DateTime DateAjout, DateTime fk_Date)
        {
            this.mot = mot;
            this.fk_trad = fk_trad;
            this.DateAjout = DateAjout;
            this.fk_Date = fk_Date;
        }
    }
}
