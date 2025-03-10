using PangyaCryptography;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PangyaAPI
{
    public class PangyaBuffer
    {
        #region Fields

        /// <summary>
        /// Buffer
        /// </summary>
        public byte[] _message;

        private PacketCompression CompressionV2 = new PacketCompression();

        private Pang pang = new Pang();

        private bool _compress = false;

        #endregion

        #region Constructors

        public PangyaBuffer(bool compress)
        {
            this._compress = compress;
        }

        #endregion

        #region Private Methods    

        public byte[] GetPacket(byte key)
        {
            var encrypted = CompressionV2.Encrypt(_message, key);
            return encrypted;


            //Se a mensagem ultrapassa de 255
            if (_message.Length + 17 > 0xFF)
                _compress = true; //Habilita compressão

            //Teste Nova versão
            var packetIdInfo = pang.GeneratePacketId(key);

            var packetId = packetIdInfo[0];

            var packetDynamicValue = packetIdInfo[3];

            //Cria result Primeiros 7 bytes
            var _packetResult = new List<byte>()
            {
                packetId, 0x00, 0x00, packetDynamicValue
            };

            if (_compress)
            {
                 var compressed = CompressionV2.Compress(_message);
                //var compressedOld = PangyaCryt.PacketCompression.Compress(_message, PangyaCryt.PacketCompression.TipoOperacao.Compress);
                //var compressedOld = CompressionV2.Compress_Old(_message);

                //Console.WriteLine("Versão 1: " + BitConverter.ToString(compressed.Take(10).ToArray()).Replace("-", " "));
                //Console.WriteLine("Versão 2: " + BitConverter.ToString(compressedOld.Take(10).ToArray()).Replace("-", " "));
                //Console.WriteLine();

                _packetResult.AddRange(compressed);

                //Final 3 Bytes
                _packetResult.AddRange(new byte[] { 0x11, 0x00, 0x00 });
            }
            else
            {
                //Add 0x00, 0x00, 0x00, 0x00, sendo estes 4 bytes o tamanho da mensagem.
                _packetResult.AddRange(LengthToByteArray(_message.Length));

                _packetResult.Add(Convert.ToByte(17 + _message.Length));

                //Concatena mensagem
                _packetResult.AddRange(_message);

                //Final 3 Bytes
                _packetResult.AddRange(new byte[] { 0x11, 0x00, 0x00 });
            }

            //Teste Tamanho do Packet....
            var packetLength = ToShortByteLength((_packetResult.Count - 3));

            //Seta Tamanho do Packet
            _packetResult[1] = packetLength[0];
            _packetResult[2] = packetLength[1];

            System.IO.File.WriteAllBytes(@"C:\Users\Administrador\Desktop\Capture\old.hex", _packetResult.ToArray().Encrypt(key));


            return _packetResult.ToArray().Encrypt(key);
        }

        public byte[] ToShortByteLength(int number)
        {
            var result = new byte[2];
            result[1] = (byte)(number >> 8);
            result[0] = (byte)(number & 255);

            return result;
        }

        private byte[] LengthToByteArray(int length)
        {
            byte[] intBytes = BitConverter.GetBytes(length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            byte[] result = intBytes;

            return result;
        }

        #endregion

    }
}
