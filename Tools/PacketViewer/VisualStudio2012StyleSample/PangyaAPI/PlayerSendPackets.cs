using PangyaAPI.BinaryModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public partial class Player
    {
        public void Send(byte[] message, bool compress = false)
        {
            var buffer = new PangyaBuffer(compress);
            buffer._message = message;

            var result = buffer.GetPacket(Key);

            Tcp.GetStream().Write(result, 0, result.Length);
        }

        //Envia Resposta através do PangyaBinaryWriter
        public void SendResponse(bool compress = false)
        {
            Send(Response.GetBytes(), compress);

            Response = new PangyaBinaryWriter(new MemoryStream());
        }

        public void SendResponse(byte[] message, bool compress = false)
        {
            Response.Write(message);
            SendResponse(compress);
        }

        /// <summary>
        /// Teste              
        /// </summary>
        /// <param name="send"></param>
        public void SendBytes(byte[] send)
        {
            Tcp.GetStream().Write(send, 0, send.Length);
        }

        /// <summary>
        /// Envia mensagem de um jogador para o outro
        /// </summary>
        /// <param name="player">Player Destino</param>
        /// <param name="message">Mensagem a ser enviada</param>
        /// <param name="sendToMe">Indica se o player de origem deve reseber a mesma mensagem</param>
        public void SendToAnotherPlayer(Player player, byte[] message, bool sendToMe = true)
        {
            player.SendResponse(message);
            this.SendResponse(message);
        }


        #region PROJECTG Emulator Sends 
        //public List<byte[]> SendFromProjectGEmulator()
        //{
        //    //Obtém resposta do GameServer externo
        //    //var response = _projectG.Send(CurrentPacket.MessageCrypted).ToList();

        //    //Encaminha resposta para o player
        //    response.ForEach(r => SendBytes(r));

        //    return response;
        //}
        #endregion
    }
}
