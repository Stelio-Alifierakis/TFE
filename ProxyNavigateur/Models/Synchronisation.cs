using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    /// <summary>
    /// Modèle de synchronisation
    /// </summary>
    public sealed class Synchronisation
    {
        /// <summary>
        /// Date de la synchronisation
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Synchronisation() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="date">Date de la synchronisation</param>
        public Synchronisation(DateTime date)
        {
            this.date = date;
        }

    }
}
