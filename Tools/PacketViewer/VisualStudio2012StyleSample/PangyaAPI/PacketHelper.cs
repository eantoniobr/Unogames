using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public class PacketHelper
    {
        public byte[] Message { get; set; }

        public List<byte[]> Packets { get; set; }

        public List<string> PacketResultDescription { get; set; }


        public PacketHelper(byte[] message)
        {
            Message = new byte[message.Length];

            Buffer.BlockCopy(message, 0, Message, 0, message.Length);

            Packets = new List<byte[]>();
            PacketResultDescription = new List<string>();

            GeneratePackets();
        }

        public void GeneratePackets()
        {
            var reader = new PangyaAPI.BinaryModels.PangyaBinaryReader(new MemoryStream(Message));

            var packetResults = new List<byte[]>();

            //try
            //{

            int i = 0;
            while (reader.HasValueToRead())
            {
                if (Message.Length == 2824)
                {
                    var teste = "";
                }

                reader.Skip(1);

                var restantes = (int)(reader.BaseStream.Length - reader.BaseStream.Position);
                var length = reader.BaseStream.Length;
                var position = reader.BaseStream.Position;

                var packetLength = reader.ReadInt16();


                if (packetLength > restantes)
                {
                    var currentPacket = new byte[restantes + 3];
                    Buffer.BlockCopy(Message, (int)reader.BaseStream.Position - 3, currentPacket, 0, (int)Message.Length - (int)(reader.BaseStream.Position - 3));
                    reader.Skip(restantes);

                    Packets.Add(currentPacket);
                    PacketResultDescription.Add("Tamanho do Subpacket é maior que o tamanho do Buffer");
                }
                else
                {
                    reader.Skip(packetLength);

                    var currentPacket = new byte[packetLength + 3]; //Initial byte (1 byte) + Packet lenth (2 bytes) + PacketLenghResult
                    Buffer.BlockCopy(Message, (int)((reader.BaseStream.Position - (packetLength + 3))), currentPacket, 0, packetLength + 3);

                    Packets.Add(currentPacket);
                    PacketResultDescription.Add("Subpacket OK");
                }

                i++;
            }
        }

    }
}
