using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    class MotCle
    {
        public string mot { get; set; }
        public int valeur { get; set; }
        public DateTime fk_synchronisation { get; set; }
        public string fk_theme { get; set; }
        public DateTime dateAjout { get; set; }
    }
}
