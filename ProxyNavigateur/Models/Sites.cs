using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    class Sites
    {
        public string nomSite { get; set; }
        public DateTime fk_synchronisation { get; set; }
        public string fk_theme { get; set; }
        public DateTime dateAjout { get; set; }
        public string fk_liste { get; set; }
    }
}
