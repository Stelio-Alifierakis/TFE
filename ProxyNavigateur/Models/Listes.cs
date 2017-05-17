using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDonnees.Models
{
    /// <summary>
    /// Modèle Liste
    /// </summary>
    public sealed class Listes
    {
        /// <summary>
        /// Nom de la liste
        /// </summary>
        public string liste { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Listes() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="liste">Nom de la liste</param>
        public Listes(string liste)
        {
            this.liste = liste;
        }
    }
}
