using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    /// <summary>
    /// Le modèle de la topologie
    /// </summary>
    public sealed class Topologie
    {
        /// <summary>
        /// L'ID programme
        /// </summary>
        public string idMachine { get; set; }

        /// <summary>
        /// Clé étrangère de la synchronisation
        /// </summary>
        public DateTime fk_Date { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Topologie() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="idMachine">L'ID de la machine</param>
        /// <param name="fk_Date">La date de synchronisation</param>
        public Topologie(string idMachine, DateTime fk_Date)
        {
            this.idMachine = idMachine;
            this.fk_Date = fk_Date;
        }
    }
}
