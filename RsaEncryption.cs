using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace microcontrollerSide
{
    static class RsaEncryption
    {
        private static string publicKey;
        private static string privateKey;
        private static string ServerPublickey;
        private static RSA Service;

        public static byte[] GenerateKeys()
        {
            Service = RSA.Create();
            publicKey = Service.ToXmlString(false);
            privateKey = Service.ToXmlString(true);
            return Encoding.UTF8.GetBytes(publicKey);
        }

        public static string Decrypt(byte[] data)
        {
            byte[] bytes = Service.Decrypt(data, RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(bytes);
        }
        public static byte[] DecryptToByte(byte[] data)
        {
            
            return Service.Decrypt(data, RSAEncryptionPadding.Pkcs1);
        }

        public static byte[] EncryptToServer(byte[] data)
        {
            using (RSA rsa = RSA.Create()) {
                rsa.FromXmlString(ServerPublickey);
                return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            }

            
        }

        public static void SetServerPublicKey(string Temppublickey)
        {
            ServerPublickey = Temppublickey;
        }
    }
}