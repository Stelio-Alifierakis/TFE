using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    /// <summary>
    /// Modèle de mot-clé
    /// </summary>
    public sealed class MotCle
    {
        /// <summary>
        /// Le mot-clé
        /// </summary>
        public string mot { get; set; }

        /// <summary>
        /// La valeur du mot
        /// </summary>
        public int valeur { get; set; }

        /// <summary>
        /// Date d'ajout du mot
        /// </summary>
        public DateTime DateAjout { get; set; }

        /// <summary>
        /// Clé étrangère de la date de synchronisation
        /// </summary>
        public DateTime fk_Date { get; set; }

        /// <summary>
        /// La clé étrangère du thème
        /// </summary>
        public string fk_theme { get; set; }

        /// <summary>
        /// Le synonyme (permet la jointure)
        /// </summary>
        public Synonyme Synonyme { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public MotCle() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mot">Le mot</param>
        /// <param name="valeur">La valeur du mot</param>
        /// <param name="DateAjout">La date d'ajout du mot</param>
        public MotCle(string mot, int valeur, DateTime DateAjout)
        {
            this.mot = mot;
            this.valeur = valeur;
            this.DateAjout = DateAjout;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mot">Le mot</param>
        /// <param name="valeur">La valeur du mot</param>
        /// <param name="DateAjout">La date d'ajout du mot</param>
        /// <param name="fk_theme">Le thème du mot</param>
        public MotCle(string mot, int valeur, DateTime DateAjout, string fk_theme)
        {
            this.mot = mot;
            this.valeur = valeur;
            this.DateAjout = DateAjout;
            this.fk_theme = fk_theme;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mot">Le mot</param>
        /// <param name="valeur">La valeur du mot</param>
        /// <param name="DateAjout">La date d'ajout du mot</param>
        /// <param name="fk_Date">La date de synchronisation</param>
        /// <param name="fk_theme">Le thème du mot</param>
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
