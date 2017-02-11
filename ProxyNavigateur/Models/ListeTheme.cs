using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyNavigateur.Models
{
    public sealed class ListeTheme
    {
        public string theme { get; set; }

        public ListeTheme()
        {

        }

        public ListeTheme(string theme)
        {
            this.theme = theme;
        }
    }
}
