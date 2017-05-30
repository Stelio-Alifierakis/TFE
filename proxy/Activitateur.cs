using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proxy
{
    public class Activitateur
    {
        public bool actif { get; set; }

        public Activitateur(bool actif)
        {
            this.actif = actif;
        }

        public void AjoutBool(bool actif)
        {
            //throw new NotImplementedException();
            this.actif = actif;
        }

        public bool RetourBool()
        {
            //throw new NotImplementedException();
            return actif;
        }
    }
}
