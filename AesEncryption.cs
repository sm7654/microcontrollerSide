using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace microcontrollerSide
{

    static class AesEncryption
    {
        private static byte[] aesKey;
        private static byte[] aesIV;

        public static void Addkeys(byte[] key, byte[] Iv)
        {
            aesKey = RsaEncryption.DecryptToByte(key);
            aesIV = RsaEncryption.DecryptToByte(Iv);
        }


        public static byte[] EncryptedData(byte[] data)
        {
            if (aesKey == null || aesIV == null)
                return null;
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = aesKey;
                    aes.IV = aesIV;

                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream Cry = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        Cry.Write(data, 0, data.Length);

                        Cry.FlushFinalBlock();
                        return ms.ToArray();
                    }

                }
            } catch (Exception e) { }

            return null;
        }
        public static byte[] FileEn()
        {
            byte[] data = File.ReadAllBytes("C:\\Users\\user\\Desktop\\2024-11-16.IBC_UNLIMITED.Faulty_Sites.xlsx");
            if (aesKey == null || aesIV == null)
                return null;
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = aesKey;
                    aes.IV = aesIV;

                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream Cry = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        Cry.Write(data, 0, data.Length);
                        Cry.Close();
                        return ms.ToArray();
                    }

                }
            }
            catch (Exception e) { }
            return null;
        }
    }
}
