using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDonnees.Models
{
    /// <summary>
    /// Modèle de sites dynamique
    /// </summary>
    public sealed class ListeDynamique
    {
        /// <summary>
        /// URL du site
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// La date d'ajout du site
        /// </summary>
        public DateTime DateAjout { get; set; }

        /// <summary>
        /// La clé étrangère de la synchronisation sous format date
        /// </summary>
        public DateTime fk_Date { get; set; }

        /// <summary>
        /// La clé étrangère du thème sous format nom
        /// </summary>
        public string fk_theme { get; set; }

        /// <summary>
        /// Le thème du site dynamique
        /// </summary>
        public ListeTheme theme { get; set; }


        /// <summary>
        /// Constructeur
        /// </summary>
        public ListeDynamique() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="url">URL du site</param>
        /// <param name="DateAjout">Date d'ajout du site</param>
        public ListeDynamique(string url, DateTime DateAjout)
        {
            this.url = url;
            this.DateAjout = DateAjout;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="url">URL du site</param>
        /// <param name="DateAjout">Date d'ajout du site</param>
        /// <param name="fk_theme">Thème du site</param>
        public ListeDynamique(string url, DateTime DateAjout, string fk_theme)
        {
            this.url = url;
            this.DateAjout = DateAjout;
            this.fk_theme = fk_theme;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="url">URL du site</param>
        /// <param name="DateAjout">Date d'ajout du site</param>
        /// <param name="fk_Date">Date de synchronisation</param>
        /// <param name="fk_theme">Thème du site</param>
        public ListeDynamique(string url, DateTime DateAjout, DateTime fk_Date, string fk_theme)
        {
            this.url = url;
            this.DateAjout = DateAjout;
            this.fk_Date = fk_Date;
            this.fk_theme = fk_theme;
        }
    }
}
