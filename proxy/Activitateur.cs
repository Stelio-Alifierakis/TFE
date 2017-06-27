using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proxy
{
    /// <summary>
    /// Classe détient des valeurs booléennes
    /// </summary>
    public class Activitateur
    {
        /// <summary>
        /// Valeur d'activation
        /// </summary>
        public bool actif { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="actif">Valeur affectée</param>
        public Activitateur(bool actif)
        {
            this.actif = actif;
        }

        /// <summary>
        /// Ajout d'une valeur booléenne
        /// </summary>
        /// <param name="actif">Valeur booléenne</param>
        public void AjoutBool(bool actif)
        {
            //throw new NotImplementedException();
            this.actif = actif;
        }

        /// <summary>
        /// Retour de la valeur booléenne
        /// </summary>
        /// <returns>Valeur booléenne</returns>
        public bool RetourBool()
        {
            //throw new NotImplementedException();
            return actif;
        }
    }
}
