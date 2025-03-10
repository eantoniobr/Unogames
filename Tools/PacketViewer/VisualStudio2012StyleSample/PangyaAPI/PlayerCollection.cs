using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public class PlayerCollection : List<Player>
    {
        public void SendToAll(byte[] message)
        {
            ForEach(p => p.SendResponse(message));
        }

        public void DisconnectPlayer() { }


        #region Methods GET

        public Player GetByNickName(string nickName)
        {
            return this.FirstOrDefault(p => p.UserInfo.Nickname == nickName);
        }

        public Player GetById(int uid)
        {
            return this.FirstOrDefault(p => p.UserInfo.UID == uid);
        }

        #endregion


    }
}
