using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
           new Thread(ChengeIv).Start();
        }
        public static int GetRandomDelay()
        {
            Random _random = new Random();
            
            return _random.Next(1000 * 60 * 1, 1000 * 60 * 2); // Random time between 3-min to 4-min (inclusive)
        }

        public static void ChengeIv()
        {
            while (MicroController.IsClientConnected())
            {
                int delay = GetRandomDelay();
                Console.WriteLine(delay);
                Thread.Sleep(delay);
                if (!MicroController.IsClientConnected())
                    return;
                byte[] AESTemp;
                using (Aes aesServise = Aes.Create())
                {
                    aesServise.KeySize = 256;
                    AESTemp = aesServise.IV;
                }
                byte[] bytes = Encoding.UTF8.GetBytes("CHANGEIV;").Concat(AESTemp).ToArray();

                MessageBox.Show(Convert.ToBase64String(AESTemp));
                MicroController.SendToClient(bytes);
                aesIV = AESTemp;
            }
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

        public static byte[] DecryptData(byte[] encryptedData)
        {
            if (aesKey == null || aesIV == null)
                return null;
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = aesKey;
                    aes.IV = aesIV;

                    ICryptoTransform decryptor = aes.CreateDecryptor();
                    using (MemoryStream ms = new MemoryStream(encryptedData))
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (MemoryStream decryptedStream = new MemoryStream())
                    {
                        cs.CopyTo(decryptedStream);
                        return decryptedStream.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                // Handle the exception appropriately
            }

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
