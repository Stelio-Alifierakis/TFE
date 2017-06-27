using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Synchronisateur.Message
{
    public class Crypteur
    {
        private byte[] cle;
        private byte[] iv;

        public Crypteur()
        {
            using (Rijndael crypt = Rijndael.Create())
            {
                cle = Encoding.ASCII.GetBytes("HR$2pIjHR$2pIj12");
                iv = Encoding.ASCII.GetBytes("HR$2pIjHR$2pIj12");
            }
        }

        public byte[] crypt(byte[] donnee)
        {
            if (cle == null || cle.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] cryptByte = default(byte[]);

            using (Rijndael crypt = Rijndael.Create())
            {

                try
                {
                    using (MemoryStream mem = new MemoryStream())
                    {
                        using (CryptoStream cryptStream = new CryptoStream(mem, crypt.CreateEncryptor(cle, iv), CryptoStreamMode.Write))
                        {

                            cryptStream.Write(donnee, 0, (int)donnee.Length);
                            cryptStream.FlushFinalBlock();

                            mem.Position = 0;

                            cryptByte = mem.ToArray();
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            return cryptByte;
        }

        public byte[] decrypt(byte[] donnee)
        {
            byte[] decryptByte = default(byte[]);

            using (Rijndael alg = Rijndael.Create())
            {
                try
                {
                    using (MemoryStream mem = new MemoryStream(donnee))
                    {
                        using (CryptoStream crypt = new CryptoStream(mem, alg.CreateDecryptor(cle, iv), CryptoStreamMode.Read))
                        {
                            decryptByte = mem.ToArray();
                            crypt.Read(decryptByte, 0, (int)decryptByte.Length);
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return decryptByte;
        }
    }
}
