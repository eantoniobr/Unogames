using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaCryptography
{
    public static class Cryptor
    {
        private static Pang _pang = new Pang();

        public static byte[] Encrypt(this byte[] message, byte key)
        {
            return _pang._pangya_server_encrypt(message, key);
        }

        public static byte[] Decript(this byte[] message, byte key)
        {
           return _pang._pangya_client_decrypt(message, key);
        }

        public static byte[] DecryptServer(this byte[] message, byte key)
        {
            return _pang._pangya_server_decrypt(message, key);
        }
    }
}
