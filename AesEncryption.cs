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

        private static byte[] aesKeyServer;
        private static byte[] aesIVServer;

        public static void AddkeysForServer(byte[] key, byte[] Iv)
        {
            aesKeyServer = RsaEncryption.DecryptToByte(key);
            aesIVServer = RsaEncryption.DecryptToByte(Iv);
        }
        public static void AddkeysForClient(byte[] key, byte[] Iv)
        {
            
            aesKey = RsaEncryption.DecryptToByte(key);
            aesIV = RsaEncryption.DecryptToByte(Iv);
            
            
            
            
            //new Thread(ChengeIv).Start();
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
                Thread.Sleep(delay);
                if (!MicroController.IsClientConnected())
                    return;
                byte[] AESTempIV;
                byte[] AESTempKEY;
                using (Aes aesServise = Aes.Create())
                {
                    aesServise.KeySize = 256;
                    AESTempIV = aesServise.IV;
                    AESTempKEY = aesServise.Key;
                }
                byte[] bytes = Encoding.UTF8.GetBytes("CHANGEIVANDKEY;").Concat(AESTempIV).Concat(AESTempKEY).ToArray();

                MicroController.SendToClient(bytes);
                aesIV = AESTempIV;
                aesKey = AESTempKEY;
            }
        }

        public static byte[] EncryptedDataForClient(byte[] data)
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

        public static byte[] DecryptDataForClient(byte[] encryptedData)
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





        public static byte[] EncryptedDataForServer(byte[] data)
        {
            if (aesKeyServer == null || aesIVServer == null)
                return null;
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = aesKeyServer;
                    aes.IV = aesIVServer;

                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream Cry = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        Cry.Write(data, 0, data.Length);

                        Cry.FlushFinalBlock();
                        return ms.ToArray();
                    }

                }
            }
            catch (Exception e) { }

            return null;
        }

        public static byte[] DecryptDataForServer(byte[] encryptedData)
        {
            if (aesKeyServer == null || aesIVServer == null)
                return null;
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = aesKeyServer;
                    aes.IV = aesIVServer;

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
        public static string DecryptToServerToString(byte[] encryptedData)
        {
            try
            {
                return Encoding.UTF8.GetString(DecryptDataForServer(encryptedData));
            }catch(Exception e)
            {
                return "";
            }
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
