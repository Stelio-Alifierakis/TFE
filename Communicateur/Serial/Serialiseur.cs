using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Communicateur.Serial
{
    public class Serialiseur
    {
        public void serial(IEnvoyable env, Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(stream, env);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnvoyable Deserial(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();

            return (IEnvoyable)formatter.Deserialize(stream);
        }
    }
}
