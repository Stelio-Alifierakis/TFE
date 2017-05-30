using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicateur.ComProxy
{
    public interface IBool
    {
        void AjoutBool(bool actif);
        bool RetourBool();
    }
}
