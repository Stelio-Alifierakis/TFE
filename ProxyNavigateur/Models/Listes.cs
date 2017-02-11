using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class Listes
    {
        public string liste { get; set; }

        public Listes() { }

        public Listes(string liste)
        {
            this.liste = liste;
        }
    }
}
