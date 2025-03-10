using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PangyaAPI
{
    public abstract class TcpServer
    {
        #region Delegates
        public delegate void ConnectedEvent(Player player);
        public delegate void PacketReceivedEvent(Player player);
        #endregion

        #region Events
        /// <summary>
        /// Este evento ocorre quando o ProjectG se conecta ao Servidor
        /// </summary>
        public event ConnectedEvent OnClientConnected;

        /// <summary>
        /// Este evento ocorre quando o Servidor Recebe um Packet do ProjectG
        /// </summary>
        public event PacketReceivedEvent OnPacketReceived;

        #endregion

        #region Fields

        /// <summary>
        /// Lista de Players conectados
        /// </summary>
        public List<Player> Players = new List<Player>();

        private int NextConnectionId { get; set; } = 1;

        private TcpListener _server;

        private bool _isRunning;

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Envia chave para o player
        /// </summary>
        protected abstract void SendKey(Player player);
       // protected abstract void PacketReceived(Player player);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcp"></param>
        protected abstract Player CreatePlayer(TcpClient tcp);

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="ip">IP do servidor (Local ou Global)</param>
        /// <param name="port">Porta</param>
        /// <param name="maxConnections">
        /// Número máximo de conexões 
        /// Quando o Player se conecta ao Game-server, automaticamente ele é desconectado do LoginServer pois não necessita mais desta comunicação
        /// </param>
        public TcpServer(string ip, int port, int maxConnections)
        {
            try
            {
                _server = new TcpListener(IPAddress.Parse(ip), port);

                //Inicia Servidor
                _server.Start(backlog: maxConnections);

                Console.WriteLine("Limite de conexões: " + maxConnections);
                Console.WriteLine("Servidor Iniciado na porta: " + port);

                _isRunning = true;

                //Inicia Thread para escuta de clientes
                var WaitConnectionsThread = new Thread(new ThreadStart(WaitConnections));
                WaitConnectionsThread.Start();
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro ao iniciar o servidor: " + erro.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Aguarda Conexões
        /// </summary>
        private void WaitConnections()
        {
            while (_isRunning)
            {
                // Inicia Escuta de novas conexões (Quando player se conecta).
                TcpClient newClient = _server.AcceptTcpClient();

                // Cliente conectado
                // Cria uma Thread para manusear a comunicação (uma thread por cliente)
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        /// <summary>
        /// Manuseia Comunicação do Cliente
        /// </summary>
        private void HandleClient(object obj)
        {
            //Recebe cliente a partir do parâmetro
            TcpClient tcpClient = (TcpClient)obj;

            //Cria novo player
            var player = CreatePlayer(tcpClient);

            ////Chama método que faz chamada ao evento OnClientConnected
            PlayerConnected(player);

            NetworkStream clientStream = player.Tcp.GetStream();

            //Escuta contínuamente as mensagens do player (ProjectG) enquanto estiver conectado
            while (player.Tcp.Connected)
            {
                try
                {
                    var messageBufferRead = new byte[18096]; //Tamanho do BUFFER á ler

                    //Lê mensagem do cliente
                    int bytesRead = clientStream.Read(messageBufferRead, 0, 18096);

                    //variável para armazenar a mensagem recebida
                    byte[] message = new byte[bytesRead];

                    //Copia mensagem recebida
                    Buffer.BlockCopy(messageBufferRead, 0, message, 0, bytesRead);

                    if (message.Length >= 5)
                    {
                        if(player.CurrentPacket != null)
                        player.PreviousPacket = new Packet(player.CurrentPacket.MessageCrypted, player.Key);

                        player.CurrentPacket = new Packet(message, player.Key);

                        //Dispara evento OnPacketReceived
                        OnPacketReceived?.Invoke(player);
                    }
                    else
                    {
                        //Sem Resposta
                        DisconnectPlayer(player);
                    }
                }
                catch (Exception erro)
                {
                    TextWriter tw = new StreamWriter(@"C:\error\error.log");
                    Console.SetError(tw);
                    Console.Error.WriteLine(erro);
                    Console.Error.Close();
                    Console.WriteLine("Exception error:" + Environment.NewLine);
                    Console.WriteLine(erro.Message + Environment.NewLine);

                    //Desconecta player
                    DisconnectPlayer(player);
                }
            }

            //Caso o player não estiver mais conectado
            Players.Remove(player);
        }

        private void PlayerConnected(Player player)
        {
            player.ConnectionId = NextConnectionId;

            NextConnectionId += 1;

            Console.WriteLine($"Player Connected With ConnectionId: {player.ConnectionId}");

            SendKey(player);

            //Chama evento OnClientConnected
            OnClientConnected?.Invoke(player);
        }
        #endregion

        #region Public Methods

        public void DisconnectPlayer(Player player)
        {
            //Fecha conexão do cliente
            if (player.Tcp.Connected)
                player.Tcp.Close();

            //Remove o mesmo da lista de clientes
            Players.Remove(player);
        }

        #endregion

    }
}
