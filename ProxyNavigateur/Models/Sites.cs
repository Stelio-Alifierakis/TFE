using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    /// <summary>
    /// Modèle de site
    /// </summary>
    public sealed class Sites
    {
        /// <summary>
        /// Nom du site
        /// </summary>
        public string nomSite { get; set; }

        /// <summary>
        /// Date d'ajout du site
        /// </summary>
        public DateTime DateAjout { get; set; }

        /// <summary>
        /// Clé étrangère de la date de synchronisation
        /// </summary>
        public DateTime fk_Date { get; set; }

        /// <summary>
        /// Clé étrangère du thème
        /// </summary>
        public string fk_theme { get; set; }

        /// <summary>
        /// Clé étrangère de la liste
        /// </summary>
        public string fk_liste { get; set; }

        /// <summary>
        /// Liste (permet la jointure)
        /// </summary>
        public Listes Listes { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Sites() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nomSite">Nom du site</param>
        /// <param name="DateAjout">Date d'ajout du site</param>
        public Sites(string nomSite, DateTime DateAjout)
        {
            this.nomSite = nomSite;
            this.DateAjout = DateAjout;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nomSite">Nom du site</param>
        /// <param name="DateAjout">Date d'ajout du site</param>
        /// <param name="fk_theme">Nom du thème</param>
        /// <param name="fk_liste">Nom de la liste</param>
        public Sites(string nomSite, DateTime DateAjout, string fk_theme, string fk_liste)
        {
            this.nomSite = nomSite;
            this.DateAjout = DateAjout;
            this.fk_theme = fk_theme;
            this.fk_liste = fk_liste;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nomSite">Nom du site</param>
        /// <param name="DateAjout">Date d'ajout du site</param>
        /// <param name="fk_Date">Date de création</param>
        /// <param name="fk_theme">Nom du thème</param>
        /// <param name="fk_liste">Nom de la liste</param>
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
