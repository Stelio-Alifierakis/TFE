using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDonnees.Models
{
    /// <summary>
    /// Modèle des thèmes
    /// </summary>
    public sealed class ListeTheme
    {
        /// <summary>
        /// Nom du thème
        /// </summary>
        public string theme { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public ListeTheme()
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="theme"></param>
        public ListeTheme(string theme)
        {
            this.theme = theme;
        }
    }
}
