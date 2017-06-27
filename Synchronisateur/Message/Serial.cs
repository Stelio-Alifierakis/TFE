using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Synchronisateur.Message
{
    public class Serial
    {
        public byte[] serial(string msg)
        {
            return Encoding.Default.GetBytes(msg);
        }

        public void serial<T>(T msg, Stream stream)
        {
            
            try
            {
                BinaryFormatter bin = new BinaryFormatter();

                bin.Serialize(stream, (object)msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string messageString(byte[] msg)
        {
            return Encoding.Default.GetString(msg);
        }

        public T messageString<T>(byte[] donnee)
        {
            T objet = default(T);

            try
            {

                using (MemoryStream mem=new MemoryStream(donnee))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    objet = (T)bin.Deserialize(mem);
                }  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return objet;
        }
    }
}
