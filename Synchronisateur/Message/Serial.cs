using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronisateur.Message
{
    public class Serial
    {
        public byte[] serial(string msg)
        {
            return Encoding.Default.GetBytes(msg);
        }

        public string messageString(byte[] msg)
        {
            return Encoding.Default.GetString(msg);
        }
    }
}
