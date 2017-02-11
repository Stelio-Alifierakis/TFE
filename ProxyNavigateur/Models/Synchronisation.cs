using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class Synchronisation
    {
        public DateTime date { get; set; }

        public Synchronisation() { }

        public Synchronisation(DateTime date)
        {
            this.date = date;
        }

    }
}
