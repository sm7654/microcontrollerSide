using System;
using System.Collections.Generic;
using System.Linq;
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
            aesKey = key;
            aesIV = Iv;
        }

    }
}
