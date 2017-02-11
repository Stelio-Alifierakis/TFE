using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class Topologie
    {
        public string idMachine { get; set; }
        public DateTime fk_Date { get; set; }

        public Topologie() { }

        public Topologie(string idMachine, DateTime fk_Date)
        {
            this.idMachine = idMachine;
            this.fk_Date = fk_Date;
        }
    }
}
