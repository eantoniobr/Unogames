using PangyaAPI.BinaryModels;
using PangyaCryptography;
using System;
using System.IO;

namespace PangyaAPI
{
    public class Packet
    {
        #region Private Fields

        private Pang _pang = new Pang();

        private MemoryStream _stream;

        #endregion

        #region Public Fields

        public PangyaBinaryReader Reader;

        #endregion

        /// <summary>
        /// Chave do Packet
        /// </summary>
        public byte Key { get; private set; }

        /// <summary>
        /// Id do Packet
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        /// Seta uma descrição a este packet
        /// </summary>
        public string Description { get; set; } 

        /// <summary>
        /// Mensagem do Packet
        /// </summary>
        public byte[] Message { get; set; }

        public byte[] MessageCrypted { get; set; }

        public Packet(byte[] message, byte key, bool isPacketClient = true)
        {
            Id = BitConverter.ToInt16(new byte[] { message[5], message[6] }, 0);
            Key = key;

            MessageCrypted = new byte[message.Length];
            Buffer.BlockCopy(message, 0, MessageCrypted, 0, message.Length); //Copia mensagem recebida criptografada

            Message = isPacketClient ? message.Decript(key) : message.DecryptServer(key);

            _stream = new MemoryStream(Message);

            _stream.Seek(2, SeekOrigin.Current); //Seek Inicial
            Reader = new PangyaBinaryReader(_stream);
        }

        //public bool IsSecurity(byte[] message)
        //{
        //    int rand = message[1];

        //    var x = (PublicKeyTable.Table[Key << 8] + rand + 1);
        //    var y = (PublicKeyTable.Table[Key << 8] + rand + 4097);

        //    var x2 = (PublicKeyTable.Table[Key << 8] + rand);
        //    var y2 = (PublicKeyTable.Table[Key << 8] + rand + 4096);

        //    var test = (x ^ (int)message[5]);

        //    var isSecurity = (y == test);

        //    return isSecurity;
        //}

    }
}
