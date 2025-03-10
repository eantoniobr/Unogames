using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.BinaryModels
{
    public class PangyaBinaryWriter : BinaryWriter
    {
        public PangyaBinaryWriter(Stream output)
        : base(output)
        {
        }

        public PangyaBinaryWriter()
        {
            this.OutStream = new MemoryStream();
        }
        
        /// <summary>
        /// Obtém o array de bytes da stream
        /// </summary>
        public byte[] GetBytes()
        {
            if (OutStream is MemoryStream)
                return ((MemoryStream)OutStream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                OutStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public void Write(byte[][] messages)
        {
            foreach (var message in messages)
            {
                Write(message);
            }
        }

        /// <summary>
        /// Escreve String no Formato Pangya { 00, 00 (tamanho), data (valor)  } e avança a posição atual pelo número de bytes escritos
        /// </summary>
        public void WritePStr(string data)
        {
            var current = GetBytes();

            int size = data.Length;

            if (size >= short.MaxValue)
                return;

            Write((short)size);

            current = GetBytes();
            
            Write(data.ToCharArray());

            current = GetBytes();
        }

        /// <summary>
        /// Escreve um texto baseado em um tamanho fixo de bytes
        /// </summary>
        /// <param name="message">String à escrever</param>
        /// <param name="length">Tamanho total de bytes</param>
        public void WriteStr(string message, int length)
        {
            Write(Encoding.ASCII.GetBytes(message.PadRight(length, (char)0x00)));
        }

      

        //public void Write(byte[] message, int length)
        //{
        //    var result = new byte[length];

        //    Buffer.BlockCopy(message, 0, result, 0, message.Length);

        //    Write(result);
        //}




        public void WriteEmptyBytes(int length)
        {
            Write(new byte[length]);
        }
    }
}
