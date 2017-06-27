using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Configurateur
{
    /// <summary>
    /// Classe qui représente l'identificateur du programme
    /// </summary>
    public class IdentificateurProgramme
    {
        /// <summary>
        /// Variable qui représente le numéro d'identification du programme
        /// </summary>
        public string numeroProgramme { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public IdentificateurProgramme()
        {
            numeroProgramme = "000001";
        }
    }
}
