using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using BCrypt;

namespace Profileur.MotDePasse
{
    /// <summary>
    /// Classe qui va servir pour les mots de passe.
    /// Sert autant pour créer les hashage BCrypt que vérifier si le mot de passe hashé est bon.
    /// </summary>
    public class HashCreator
    {
        string salt;

        /// <summary>
        /// Fonction qui va hasher un mot de passe
        /// </summary>
        /// <param name="mdp">Mot de passe entré</param>
        /// <returns>Hashage de mot de passe</returns>
        public string HashMDP(string mdp)
        {
            this.salt = BCrypt.Net.BCrypt.GenerateSalt();

            return BCrypt.Net.BCrypt.HashPassword(mdp,this.salt);
        }

        /// <summary>
        /// Fonction qui va hasher un mot de passe.
        /// </summary>
        /// <param name="mdp">Mot de passe à hasher</param>
        /// <param name="salt">Salt</param>
        /// <returns>Hashage de mot de passe</returns>
        public string HashMDP(string mdp, string salt)
        {

            return BCrypt.Net.BCrypt.HashPassword(mdp, salt);
        }

        /// <summary>
        /// Fonction qui va vérifier si un mot de passe entré est identique à un hashage
        /// </summary>
        /// <param name="mdp">Mot de passe entrée</param>
        /// <param name="hash">Hashage</param>
        /// <returns>Retourne vrai ou faux selon si les hashages sont identiques</returns>
        public bool checkMotDePasse(string mdp, string hash)
        {
            
            return BCrypt.Net.BCrypt.Verify(mdp,hash);
        }
    }
}
