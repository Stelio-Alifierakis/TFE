using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using BCrypt;

namespace Profileur.MotDePasse
{
    public class HashCreator
    {
        public string HashMDP(string mdp)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            return BCrypt.Net.BCrypt.HashPassword(mdp,salt);
        }

        public bool checkMotDePasse(string mdp, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(mdp,hash);
        }
    }
}
