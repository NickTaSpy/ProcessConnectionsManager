using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.Npcap;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProcessConnectionsManager.PacketCapture
{
    public class Sharppcap : AbstractCapturer
    {
        private readonly List<string> IgnoredAddresses = new List<string>();

        public Sharppcap(int port, ListView resultList) : base(port, resultList) { }

        public override void StartCapturing()
        {
            IgnoredAddresses.Clear();
            string ver = SharpPcap.Version.VersionString;

            var devices = CaptureDeviceList.Instance;

            if (devices.Count < 1)
            {
                MessageBox.Show("No devices were found on this machine", "Sharppcap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            const int readTimeoutMilliseconds = 1000;
            string filter = $"udp and port {Port}";

            foreach (var device in devices)
            {
                if (device is NpcapDevice)
                {
                    var nPcap = device as NpcapDevice;
                    IgnoredAddresses.AddRange(nPcap.Addresses.Where(x => x?.Addr?.ipAddress != null).Select(x => x.Addr.ipAddress.ToString()));
                    nPcap.Open(OpenFlags.DataTransferUdp | OpenFlags.NoCaptureLocal, readTimeoutMilliseconds);
                    nPcap.Filter = filter;
                }
                else if (device is LibPcapLiveDevice)
                {
                    var livePcapDevice = device as LibPcapLiveDevice;
                    IgnoredAddresses.AddRange(livePcapDevice.Addresses.Where(x => x?.Addr?.ipAddress != null).Select(x => x.Addr.ipAddress.ToString()));
                    livePcapDevice.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                    livePcapDevice.Filter = filter;
                }
                else
                {
                    MessageBox.Show($"Unknown device type of {device.GetType()}", "Sharppcap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                device.OnPacketArrival += OnPacketArrival;
                device.StartCapture();
            }
        }

        public override void StopCapturing()
        {
            foreach (var device in CaptureDeviceList.Instance)
            {
                device.Close();
                device.StopCapture();
                device.OnPacketArrival -= OnPacketArrival;
            }
        }

        private void OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            var ip = packet.Extract<IPPacket>();
            if (ip != null)
            {
                AddForeignIP(IgnoredAddresses, ip.SourceAddress.ToString(), ip.DestinationAddress.ToString());
            }
        }
    }
}
