using PangyaAPI.BinaryModels;
//using ProjectGClient;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace PangyaAPI
{
    public partial class Player
    {
        #region Delegates
        public delegate void PacketChangedEvent();
        #endregion

        #region Events
        public event PacketChangedEvent OnPacketChanged;
        #endregion

        #region Public Fields

        public PangyaBinaryWriter Response;

        //Conexão
        public TcpClient Tcp;

        /// <summary>
        /// Packet Atual
        /// </summary>
        private Packet _currentPacket;

        public Packet CurrentPacket
        {
            get { return _currentPacket; }
            set
            {
                _currentPacket = value;
                OnPacketChanged?.Invoke();
            }
        }


        /// <summary>
        /// Packet Anterior
        /// </summary>
        public Packet PreviousPacket { get; set; }

        //Teste
        public UInt16 GameID = 65535;

        public UInt32 Visible = 0; //0 ou 1?


        public byte Game_Role = 0x07; //is 7

        public byte Game_Slot = 0x01; //dica se colocar 0 ao invez de 1, o character não aparece no room

        public byte game_ready = 0x00; //coloco 0

        public UInt16 RoomID = 123;



        public int ConnectionId { get; set; }

        //Identificador da conta no banco de dados
        public PlayerMemberInfo UserInfo { get; set; }

        //estatica do player 
        public PlayerStaticInfo Statistic { get; set; }

        //Chave de criptografia e decriptografia
        public byte Key { get; private set; }

        #endregion

        //private ProjectG _projectG { get; set; }

        public byte[] GeraBytesDinamicos(int tamanho)
        {
            var result = new byte[16];

            new Random().NextBytes(result);

            return result;
        }

        public Player(TcpClient tcp)
        {
            Tcp = tcp;

            //Gera uma chave dinâmica
            Key = Convert.ToByte(new Random().Next(1, 17));

            //Maximo hexadecimal value: FF (255)

            ////Chave Fixa
            Key = 0x0A;

            //_responseStream = new MemoryStream();
            Response = new PangyaBinaryWriter(new MemoryStream());
        }

        public string GetIpAdress()
        {
            return ((IPEndPoint)Tcp.Client.RemoteEndPoint).Address.ToString();
        }

        public void Disconnect()
        {
            Tcp.Close();
        }

    }

    public class PlayerMemberInfo
    {
        public int UID { get; set; }

        public byte? IDState { get; set; }

        public byte? Logon { get; set; }

        public byte? FirstSet { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public string AuthKey_Login { get; set; }
        public string AuthKey_Game { get; set; }
        public UInt32 Cookie { get; set; }
        public int Pang { get; set; }
        public byte? Sex { get; set; }
        public byte? Capabilities { get; set; }

        public int? DailyLoginCount { get; set; }

        public UInt32 CHARACTER_ID { get; set; }

      
    }

    public class PlayerStaticInfo
    {
        public UInt32 Drive { get; set; }

        public UInt32 Putt { get; set; }

        public UInt32 PlayTime { get; set; } // Second

        public UInt32 ShotTime { get; set; }

        public float LongestDistance { get; set; }

        public UInt32 Pangya { get; set; } //acerto de pangya

        public UInt32 TimeOut { get; set; }

        public UInt32 OB { get; set; }

        public UInt32 DistanceTotal { get; set; }

        public UInt32 Hole { get; set; }

        public UInt32 TeamHole { get; set; }

        public UInt32 Hio { get; set; }

        public UInt16 Bunker { get; set; }

        public UInt32 Fairway { get; set; }

        public UInt32 Albratoss { get; set; }
        public UInt32 Holein { get; set; }
        public UInt32 Puttin { get; set; }
        public float LongestPutt { get; set; }
        public float LongestChip { get; set; }
        public UInt32 EXP { get; set; }
        public byte Level { get; set; }
        public UInt64 Pang { get; set; }
        public UInt32 TotalScore { get; set; }
        public byte[] Score { get; set; } = new byte[5]; //teste >> Score: Array[$0..$4] of Int8;
        public byte Unknown { get; set; } //
        public UInt64 MaxPang0 { get; set; }
        public UInt64 MaxPang1 { get; set; }
        public UInt64 MaxPang2 { get; set; }
        public UInt64 MaxPang3 { get; set; }
        public UInt64 MaxPang4 { get; set; }
        public UInt64 SumPang { get; set; }
        public UInt32 GamePlayed { get; set; }
        public UInt32 Disconnected { get; set; }
        public UInt32 TeamWin { get; set; }
        public UInt32 TeamGame { get; set; }
        public UInt32 LadderPoint { get; set; }
        public UInt32 LadderWin { get; set; }
        public UInt32 LadderLose { get; set; }
        public UInt32 LadderDraw { get; set; }
        public UInt32 LadderHole { get; set; }
        public UInt32 ComboCount { get; set; }
        public UInt32 MaxCombo { get; set; }
        public UInt32 NoMannerGameCount { get; set; }
        public UInt32 SkinsPang { get; set; }
        public UInt32 SkinsWin { get; set; }
        public UInt32 SkinsLose { get; set; }
        public UInt32 SkinsRunHole { get; set; }
        public UInt32 SkinsStrikePoint { get; set; }
        public UInt32 SKinsAllinCount { get; set; }
        public byte[] Unknown1 { get; set; } = new byte[5]; // teste >> Unknown1: Array[$0..$5] of AnsiChar;
        public UInt32 GameCountSeason { get; set; }
        public byte[] Unknown2 { get; set; } = new byte[8]; // teste >> Unknown2: Array[$0..7] of AnsiChar;

    }
}
