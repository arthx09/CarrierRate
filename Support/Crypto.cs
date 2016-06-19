using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Suporte.Criptografia
{
    public static class Crypto
    {
        private static byte[] _Salt = Encoding.Unicode.GetBytes("@b4n4n4323###");
        private static string _Key = "b@abuinagem";



        public static void SetValues(string key, string salt)
        {
            _Salt = Encoding.Unicode.GetBytes(salt);
            _Key = key;
        }

        public static byte[] Encrypt(string text)
        {
            var rm = CreateRijndael();

            ICryptoTransform encryptor = rm.CreateEncryptor(rm.Key, rm.IV);
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }

                    return msEncrypt.ToArray();
                }
            }
        }

        public static string Decrypt(byte[] decrypt)
        {
            var rm = CreateRijndael();

            ICryptoTransform decryptor = rm.CreateDecryptor(rm.Key, rm.IV);
            using (var msDecrypt = new MemoryStream(decrypt))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        private static RijndaelManaged CreateRijndael()
        {
            var rfc = new Rfc2898DeriveBytes(_Key, _Salt);
            var rm = new RijndaelManaged();
            rm.Key = rfc.GetBytes(16);
            rm.IV = rfc.GetBytes(16);
            return rm;
        }


    }
}
