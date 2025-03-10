using System;
using System.Runtime.InteropServices;

namespace PangyaCryptography
{
    public class Pang
    {
        /// <summary>
        /// Criptografa os packets
        /// </summary>
        /// <param name="src">Bytes a ser criptografados</param>
        /// <param name="key">Chave de criptografia</param>
        public bool Encrypt_Packet(ref byte[] src, byte key)
        {
            //Cria buffer temporário
            byte[] tmp = new byte[src.Length];

            //Copia o buffin para o buffer temporário
            Buffer.BlockCopy(src, 0, tmp, 0, src.Length);

            for (int i = 8; i < src.Length; i++)
            {
                src[i] ^= tmp[i - 4];
            }

            src[4] ^= PublicKeyTable.Table[key + src[0]];

            //Resultado da operação
            return true;
        }

        //Quase lá....
        //public byte[] GeneratePacketId(byte key)
        //{
        //    int parseKey = key << 8;
        //    var nrand = new Random().Next(0, 255);

        //    int total = parseKey + nrand + 4097 - 1;

        //    var result = PublicKeyTable.Table[total];

        //    return new byte[]
        //    {
        //        Convert.ToByte(nrand),
        //        0x00,
        //        0x00,
        //        result
        //    };
        //}

        public byte[] GeneratePacketId(byte key)
        {
            switch (key)
            {
                case 0:
                    return new byte[] { 0x02, 0x15, 0x00, 0x07 };
                case 1:
                    return new byte[] { 0x20, 0x15, 0x00, 0x21 };
                case 2:
                    return new byte[] { 0xB9, 0x15, 0x00, 0x17 };
                case 3:
                    return new byte[] { 0xF1, 0x15, 0x00, 0x17 };
                case 4:
                    return new byte[] { 0x18, 0x15, 0x00, 0xD4 };
                case 5:
                    return new byte[] { 0x69, 0x15, 0x00, 0x3D };
                case 6:
                    return new byte[] { 0x76, 0x15, 0x00, 0x89 };
                case 7:
                    return new byte[] { 0x9F, 0x15, 0x00, 0x3F };
                case 8:
                    return new byte[] { 0xAC, 0x15, 0x00, 0xD1 };
                case 9:
                    return new byte[] { 0x0E, 0x15, 0x00, 0xD2 };
                case 10:
                    return new byte[] { 0xDC, 0x15, 0x00, 0x3C };
                case 11:
                    return new byte[] { 0x22, 0x15, 0x00, 0x2D };
                case 12:
                    return new byte[] { 0x38, 0x15, 0x00, 0xE6 };
                case 13:
                    return new byte[] { 0xCB, 0x15, 0x00, 0xC4 };
                case 14:
                    return new byte[] { 0x2A, 0x15, 0x00, 0xD8 };
                case 15:
                    return new byte[] { 0x3C, 0x15, 0x00, 0x99 };
                case 16:
                    return new byte[] { 0xA1, 0x15, 0x00, 0xFE };
                case 17:
                    return new byte[] { 0xF2, 0x15, 0x00, 0x24 };
                case 18:
                    return new byte[] { 0x26, 0x15, 0x00, 0x88 };
                case 19:
                    return new byte[] { 0x11, 0x15, 0x00, 0xAB };

                default: return new byte[] { };
            }
        }

        public bool Decrypt_Packet(ref byte[] src, int key)
        {
            //Operador XOR Lógico
            //Referência: https://msdn.microsoft.com/pt-br/library/0zbsw2z6.aspx , https://msdn.microsoft.com/pt-BR/library/6a71f45d.aspx
            src[4] = Convert.ToByte(PublicKeyTable.Table[src[0] + key] ^ src[4]);

            int len = src.Length - 4;

            byte[] data = new byte[len];

            Buffer.BlockCopy(src, 4, data, 0, len);

            //Decrypt Data Packet
            for (int i = 4; i < len; i++)
            {
                data[i] = Convert.ToByte(data[i] ^ data[i - 4]);
            }

            //Delete One byte
            len -= 1;
            Buffer.BlockCopy(data, 1, src, 0, len);

            return true;
        }

        public byte[] _pangya_client_decrypt(byte[] buffin, byte key)
        {
            //Cria buffer temporário
            byte[] packet_decrypt = new byte[buffin.Length];

            //Copia o buffin para o buffer temporário
            Buffer.BlockCopy(buffin, 0, packet_decrypt, 0, buffin.Length);

            int ParseKey = key << 8;

            //Send to Decrypt Part
            Decrypt_Packet(ref packet_decrypt, ParseKey);


            int buffOutSize = buffin.Length - 5;

            //Copia o buffin para o buffer temporário
            Buffer.BlockCopy(packet_decrypt, 0, buffin, 0, buffOutSize);

            // *buffout is the pointer of the decrypted data starting with the packetId as a WORD
            return packet_decrypt;
        }

        /// <summary>
        /// Teste Decriptografa pacote do Pangya Client
        /// </summary>
        private byte[] _pangya_client_decrypt_teste(byte[] buffin, byte key)
        {
            byte[] buffout = new byte[buffin.Length];
            Buffer.BlockCopy(buffin, 0, buffout, 0, buffin.Length);

            byte tmp;

            int tmp_byte;
            byte tmp_bl;
            byte tmp_dl;

            tmp_byte = key << 8;

            tmp_bl = PublicKeyTable.Table[tmp_byte + buffout[0]];
            tmp_dl = PublicKeyTable.Table[tmp_byte + buffout[0] + 0x1000];

            tmp_byte += buffout[0];
            buffout[4] = tmp_dl;

            for (int i = 8; i < 12; i++)
            {
                tmp = buffout[i - 4];
                tmp ^= buffout[i];
                buffout[i] = tmp;
            }

            try
            {
                for (int i = 8; i < buffin.Length; i++)
                {
                    tmp = buffout[i];
                    tmp ^= buffout[i + 4];
                    buffout[i + 4] = tmp;
                }
            }
            catch { }

            buffout[4] ^= tmp_bl;
            buffout[5] ^= 0x00;
            buffout[6] ^= 0x00;
            buffout[7] ^= 0x00;

            // *buffout is the pointer of the decrypted data starting with the packetId as a WORD
            return buffout;
        }
        /// <summary>
        /// Criptografa pacotes do pangya Client
        /// </summary>
        /// <param name="buffin">Packet</param>
        /// <param name="key">Chave de criptografia</param>
        /// <returns>Pacote criptografado</returns>
        public byte[] _pangya_client_encrypt(byte[] buffin, byte key)
        {
            byte[] buffout = new byte[buffin.Length];
            Buffer.BlockCopy(buffin, 0, buffout, 0, buffin.Length);

            byte tmp;

            int tmp_byte;
            byte tmp_bl;
            byte tmp_dl;

            tmp_byte = key << 8;

            tmp_bl = PublicKeyTable.Table[tmp_byte + buffin[0]];
            tmp_dl = PublicKeyTable.Table[tmp_byte + buffin[0] + 0x1000];

            tmp_byte += buffin[0];

            buffin[4] = tmp_dl;
            buffout[4] = tmp_dl;

            for (int i = 8; i <= 13; i++)
            {
                tmp = buffin[i - 4];
                tmp ^= buffin[i];
                buffout[i] = tmp;
            }

            for (int i = 12; i < buffin.Length; i++)
            {
                tmp = buffin[i - 4];
                tmp ^= buffin[i];
                buffout[i] = tmp;
            }

            buffin[4] ^= tmp_bl;
            buffin[5] ^= 0x00;
            buffin[6] ^= 0x00;
            buffin[7] ^= 0x00;

            buffout[4] = buffin[4];
            buffout[5] = buffin[5];
            buffout[6] = buffin[6];
            buffout[7] = buffin[7];

            try
            {
                for (int i = buffin.Length; i < buffin.Length + 4; i++)
                {
                    buffout[i] = buffin[i - 4];
                }
            }
            catch (Exception erro)
            {
                var message = erro.Message;
            }

            // *buffout is the pointer of the encrypted data ready to send to Pangya
            //return 1;

            return buffout;
        }

        public byte[] _pangya_server_decrypt(byte[] buffin, byte key)
        {
            //Cria buffer temporário
            byte[] packet_decrypt = new byte[buffin.Length];

            int tmp_byte;
            byte tmp_bl;
            byte tmp_xor;

            tmp_byte = key << 8;
            tmp_byte += buffin[0];

            tmp_bl = PublicKeyTable.Table[tmp_byte];

            tmp_xor = buffin[3];
            tmp_xor ^= tmp_bl;

            buffin[7] ^= tmp_xor;
            buffin[8] ^= 0x00;
            buffin[9] ^= 0x00;
            buffin[10] ^= 0x00;

            for (int i = 10; i < buffin.Length; i++)
            {
                buffin[i] ^= buffin[i - 4];
            }

            //Copia o buffin para o buffer temporário
            Buffer.BlockCopy(buffin, 0, packet_decrypt, 0, packet_decrypt.Length);

            return packet_decrypt;
        }

        /// <summary>
        /// Criptografa os packets do Servidor
        /// </summary>
        /// <param name="buffin">packet</param>
        /// <param name="key">Chave criptografia</param>
        /// <returns>Pacote criptografado</returns>
        public byte[] _pangya_server_encrypt(byte[] buffin, byte key)
        {
            GCHandle handle = GCHandle.Alloc(buffin, GCHandleType.Pinned);
            IntPtr address = handle.AddrOfPinnedObject();

            byte[] buffout = new byte[buffin.Length];
            Buffer.BlockCopy(buffin, 0, buffout, 0, buffin.Length);


            int tmp_byte;
            byte tmp_bl;
            byte tmp_xor;

            tmp_byte = key << 8;
            tmp_byte += buffout[0];
            tmp_bl = PublicKeyTable.Table[tmp_byte];

            tmp_xor = buffout[3];
            tmp_xor ^= tmp_bl;

            for (int i = buffin.Length - 1; i >= 10; i--)
            {
                buffout[i] ^= Marshal.ReadByte(address, i - 4);
            }

            buffout[7] ^= tmp_xor;
            buffout[8] ^= 0x00;
            buffout[9] ^= 0x00;
            buffout[10] ^= 0x00;

            handle.Free();

            // *buffout is the pointer of the encrypted data ready to send to Pangya
            //return 1;
            return buffout;
        }
    }
}
