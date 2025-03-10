using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketViewer.BinaryModels
{
    public class PangyaBinaryWriter : BinaryWriter
    {
        #region  Construtores 

        public PangyaBinaryWriter(Stream output)
        : base(output)
        {
        }

        public PangyaBinaryWriter()
        {
            this.OutStream = new MemoryStream();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Obtém o array de bytes da stream
        /// </summary>
        public byte[] GetBytes()
        {
            if (OutStream is MemoryStream)
                return ((MemoryStream)OutStream).ToArray();


            using (var memoryStream = new MemoryStream())
            {
                memoryStream.GetBuffer();
                OutStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        #endregion

        #region Escrita de Bytes

        public void Write(byte[] message, int length)
        {
            var result = new byte[length];

            Buffer.BlockCopy(message, 0, result, 0, message.Length);

            Write(result);
        }

        /// <summary>
        /// Escreve String no Formato Pangya { 00, 00 (tamanho), data (valor)  } e avança a posição atual pelo número de bytes escritos
        /// </summary>
        public void WritePStrBytes(byte[] data)
        {
            int size = data.Length;

            if (size >= short.MaxValue)
                return;

            Write((short)size);

            Write(data);
        }

        public void WriteEmptyBytes(int length)
        {
            Write(new byte[length]);
        }

        #endregion

        #region Escrita de String

        /// <summary>
        /// Escreve String no Formato Pangya { 00, 00 (tamanho), data (valor)  } e avança a posição atual pelo número de bytes escritos
        /// </summary>
        public void WritePStr(string data)
        {
            int size = data.Length;

            if (size >= short.MaxValue)
                return;

            Write((short)size);

            Write(data.ToCharArray());
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }


        /// <summary>
        /// Escreve um texto baseado em um tamanho fixo de bytes
        /// </summary>
        /// <param name="message">String à escrever</param>
        /// <param name="length">Tamanho total de bytes</param>
        public void WriteStr(string message, int length)
        {
            if (message == null)
                message = string.Empty;

            message = message.PadRight(length, (char)0x00);
            Write(message.Select(Convert.ToByte).ToArray());
        }

        #endregion

        #region Escria de Numeros
        public void WriteByte(int? src)
        {
            if (src == null)
                src = 0;

            Write((byte)src);
        }
        public void WriteByte(ushort src)
        {
            Write((byte)src);
        }
        //converte em byte
        public void WriteByte(byte src)
        {
            Write(src);
        }

        public void WriteByte(ushort? src)
        {
            Write((byte)src);
        }
        public void WriteByte(short? src)
        {
            Write((byte)src);
        }
        public void WriteByte(byte? src)
        {
            if (src == null)
                src = 0;
            byte[] result = BitConverter.GetBytes((int)src);
            Write(result);
        }
        public void WriteUInt64(ulong? src)
        {
            if (src == null)
                src = 0;

            byte[] result = BitConverter.GetBytes((ulong)src);
            Write(result);
        }
        public void WriteUInt64(int? src)
        {
            if (src == null)
                src = 0;
            byte[] result = BitConverter.GetBytes((ulong)src);
            Write(result);
        }

        public void WriteUInt32(int? src)
        {
            if (src == null)
                src = 0;
            byte[] result = BitConverter.GetBytes((int)src);
            Write(result);
        }
        public void WriteUInt16(ushort? src)
        {
            if (src == null)
                src = 0;
            byte[] result = BitConverter.GetBytes((ushort)src);
            Write(result);
        }
        public void WriteUInt16(short? src)
        {
            if (src == null)
                src = 0;
            byte[] result = BitConverter.GetBytes((short)src);
            Write(result);
        }
        public void WriteUInt32(UInt32 src)
        {
            byte[] result = BitConverter.GetBytes(src);
            Write(result);
        }
        public void WriteUInt16(ushort src)
        {
            byte[] result = BitConverter.GetBytes(src);
            Write(result);
        }

        public void WriteUInt16(short src)
        {
            byte[] result = BitConverter.GetBytes(src);
            Write(result);
        }

        public void WriteUInt16(int src)
        {
            byte[] result = BitConverter.GetBytes((ushort)src);
            Write(result);
        }


        public void WriteUInt64(Int64 src)
        {
            byte[] result = BitConverter.GetBytes(src);
            Write(result);
        }


        public void WriteUInt64(int src)
        {
            byte[] result = BitConverter.GetBytes((ulong)src);
            Write(result);
        }
        public void WriteSingle(Single src)
        {
            byte[] result = BitConverter.GetBytes(src);
            Write(result);
        }
        #endregion

    }

}
