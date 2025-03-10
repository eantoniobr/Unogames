using Be.Windows.Forms;
using PcapSniffer;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VisualStudio2012Style.PacketsEnum;

namespace VisualStudio2012Style
{
    partial class DarkThemeForm
    {
        //private TcpServer _server;
        private PangyaCapturer _sniffer;
        private LibPcapLiveDevice _device;
        private ThreadedBindingList<DecryptedPacket> _packetsReceived { get; set; }

        public DarkThemeForm()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            _packetsReceived = new ThreadedBindingList<DecryptedPacket>();


            gridPackets.PrimaryGrid.DataSource = _packetsReceived;


            cbCapturar.SelectedIndex = 0;
            //subpacketsGrid.PrimaryGrid.DataSource = _subpackets;

            //var devices = CaptureDeviceList.Instance;

            //foreach (WinPcapDevice item in devices.Select(d => ((WinPcapDevice)d)))
            //{
            //    if (!string.IsNullOrEmpty(item.Interface.FriendlyName))
            //        cb_devices.Items.Add(item.Interface.FriendlyName + " | " + item.Name);
            //    else
            //        cb_devices.Items.Add(item.Interface.Description + " | " + item.Name);
            //}

            LibPcapLiveDeviceList devices = LibPcapLiveDeviceList.Instance;

            int i = 0;

            foreach (LibPcapLiveDevice dev in devices)
            {
                cb_devices.Items.Add(string.Format("{2}", i, dev.Name, dev.Description));
                i++;
            }
        }

        #region Menu Methods

        //--------- File ----------
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cb_devices.SelectedIndex < 0)
            {
                MessageBox.Show("Para iniciar, selecione um dispositivo");
                cb_devices.Focus();
                return;
            }

            if (_device != null && _device.Started)
            {
                StopCapture();
            }
            else
            {
                StartCapture();
            }

        }

        private void StartCapture()
        {
            _packetsReceived.Clear();

            _device = LibPcapLiveDeviceList.Instance[cb_devices.SelectedIndex];

            gridPackets.PrimaryGrid.Columns[5].FilterExpr = @"
((Id != ""PLAYER_KEEPLIVE"") and (Id != ""PLAYER_REQUEST_MESSENGER_LIST"")) and (Id != ""FC-00"")";


            _sniffer = new PangyaCapturer(_device);

            _sniffer.OnExceptionReceived += (_, ex) =>
            {
                MessageBox.Show(ex);
            };

            _sniffer.OnPacketReceived += (_, packet) =>
            {
                if (!packet.IsServer)
                {
                    switch (packet.ServiceType)
                    {
                        case ServiceType.LoginServer:
                            packet.Id = ((LoginServerPacketEnum)(packet.Data[0])).ToString();
                            break;
                        case ServiceType.GameServer:
                            packet.Id = ((GameServerPacketEnum)(packet.Data[0])).ToString();
                            break;
                        case ServiceType.RankServer:
                            packet.Id = ((MessageServerPacketEnum)(packet.Data[0])).ToString();
                            break;
                            //case ServiceType.MessageServer:
                            //    break;
                    }
                }
                else
                {
                    packet.Id = BitConverter.ToString(packet.Data.Take(2).ToArray());
                }

                if (cbCapturar.SelectedIndex == 0)
                {
                    _packetsReceived.Add(packet);
                    Text = "Packet Viewer UnoGames";
                }
                else
                {
                    Text = "Packet Ignorado: " + packet.Id;
                }
            };

            _sniffer.OnLogEvent += (_, message) =>
            {
                //logger.Information("{Message}", message);
            };

            _sniffer.StartCapture();

            //Change Button
            btnStart.Text = "Stop";
            btnStart.Image = Properties.Resources.Stop;
        }

        private void StopCapture()
        {
            if (_device.Started)
            {
                _device.Close();

                btnStart.Text = "Start";
                btnStart.Image = Properties.Resources.Start;
            }
        }

        //private void _server_OnPacketReceived(System.Net.Sockets.TcpClient player, byte[] packet)
        //{
        //    hexDecrypted.ByteProvider = new DynamicByteProvider(packet);
        //}

        private void menu_btnLimparPackets_Click(object sender, EventArgs e)
        {
            _packetsReceived.Clear();
        }
        //-------------------------

        //--------- View ----------
        private void menu_view_hexDecrypted_Click(object sender, EventArgs e)
        {
            var bar = dotNetBarManager1.Bars["bar_DecryptedViewer"];

            try
            {
                dotNetBarManager1.SuspendLayout = true;

                if (!bar.Visible)
                    bar.Visible = true;
            }
            finally
            {
                dotNetBarManager1.SuspendLayout = false;
            }
        }

        private void menu_view_hexCrypted_Click(object sender, EventArgs e)
        {
            var bar = dotNetBarManager1.Bars["bar_CryptedViewer"];

            try
            {
                dotNetBarManager1.SuspendLayout = true;

                if (!bar.Visible)
                    bar.Visible = true;
            }
            finally
            {
                dotNetBarManager1.SuspendLayout = false;
            }
        }
        //-------------------------

        #endregion


        private void gridPackets_SelectionChanged(object sender, DevComponents.DotNetBar.SuperGrid.GridEventArgs e)
        {
            try
            {
                hexDecrypted.Marks.Clear();
                hexDecrypted.Refresh();

                //gridPackets_CheckContextButtons();

                if (gridPackets.ActiveRow == null)
                    return;

                var rowIndex = gridPackets.ActiveRow.RowIndex;
                var packet = _packetsReceived[rowIndex];

                byteProviderDecrypted = new DynamicByteProvider(packet.Data);
                hexDecrypted.ByteProvider = byteProviderDecrypted;
            }
            catch { }
        }

    }
}
