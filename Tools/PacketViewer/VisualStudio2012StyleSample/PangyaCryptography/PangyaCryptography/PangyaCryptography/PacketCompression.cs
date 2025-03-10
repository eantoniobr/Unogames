using Simplicit.Net.Lzo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaCryptography
{
    public class PacketCompression
    {
        private LZOCompressor _lzo;

        public PacketCompression()
        {
            _lzo = new LZOCompressor();
        }

        public void Teste()
        {
            // Cria uma sequência bastante redundante
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append("unogames.net");
            }
            string str = sb.ToString();
            Console.WriteLine("Original-Length: " + str.Length);

            // Now compress the 70000 byte string to something much smaller
            byte[] compressed = _lzo.Compress(Encoding.Default.GetBytes(str));
            Console.WriteLine("Compressed-Length: " + compressed.Length);

            // Decompress the string to its original content
            string str2 = Encoding.Default.GetString(_lzo.Decompress(compressed));
            Console.WriteLine("Decompressed-Length: " + str2.Length);
            Console.WriteLine("Equality: " + str.Equals(str2));
        }

        private static byte AddByte(byte Left, byte Right)
        {
            short num = (short)(Left + Right);

            return (byte)num;
        }

        public byte[] Compress(byte[] source)
        {
            var result = _lzo.Compress(source).ToList();

            //Tratativas
            var ultimos4Bytes = new byte[4];
            Buffer.BlockCopy(result.ToArray(), result.Count - 4, ultimos4Bytes, 0, 4);
            Array.Reverse(ultimos4Bytes);

            result.RemoveRange(result.Count - 4, 4); //Remove os ultimos 4 bytes para voltar ao inicio como invertido
            result.RemoveRange(result.Count - 3, 3); //Remove 11 00 00

            result.InsertRange(0, ultimos4Bytes); //Retorna ao inicio os ultimos 4 bytes invertido

            result[3] = AddByte(result[2], result[3]);

            return result.ToArray();
        }

        public byte[] Encrypt(byte[] packet, int key)
        {
            var client = new System.Net.Sockets.TcpClient();

            client.Connect("127.0.0.1", 2018);

            var result = Send(client, packet);

            return result;
        }
        private static byte[] Send(System.Net.Sockets.TcpClient tcpclnt, byte[] str)
        {
            var stream = tcpclnt.GetStream();

            ASCIIEncoding asen = new ASCIIEncoding();
            //byte[] ba = asen.GetBytes(str);

            //stream.Write(ba, 0, ba.Length);
            stream.Write(str, 0, str.Length);

            byte[] messageBufferRead;
            int bytesRead; //Total de bytes da mensagem

            messageBufferRead = new byte[24096]; //Tamanho do BUFFER á ler

            //Lê mensagem do cliente
            bytesRead = stream.Read(messageBufferRead, 0, 24096);

            //variável para armazenar a mensagem recebida
            byte[] message = new byte[bytesRead];

            //Copia mensagem recebida
            Buffer.BlockCopy(messageBufferRead, 0, message, 0, bytesRead);

            //var responseData = System.Text.Encoding.ASCII.GetString(message, 0, bytesRead);
            //var responseData = BitConverter.ToString(message).Replace("-", String.Empty);

            //if(ShowCommunication)
            //Console.WriteLine(responseData);

            return message;
        }

        public byte[] CompressOriginal(byte[] source)
        {
            return _lzo.Compress(source);
        }
        public byte[] Decompress(byte[] source)
        {
            return _lzo.Decompress(source);
        }
    }
}
