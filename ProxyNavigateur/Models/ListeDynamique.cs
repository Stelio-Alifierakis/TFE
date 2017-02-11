using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    class ListeDynamique
    {
        public string url { get; set; }
        public DateTime fk_synchronisation { get; set; }
        public string fk_theme { get; set; }
        public DateTime dateAjout { get; set; }
    }
}
