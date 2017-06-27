using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronisateur.Message
{
    [Serializable]
    public class MessageEnvoi
    {
        public string message { get; set; }
        public List<string> messages { get; set; }
        public Dictionary<string, IEnumerable<object>> listeTables { get; set; }

        public void ajoutTable<T>(string nomTable, IEnumerable<object> table)
        {
            listeTables.Add(nomTable, table);
        }
    }
}
