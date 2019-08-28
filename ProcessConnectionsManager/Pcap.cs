using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Base;
using PcapDotNet.Packets;

namespace ProcessConnectionsManager
{
    public class Pcap
    {
        private readonly ListView IPList;
        private readonly int Port;

        private Task Receiver;
        private CancellationTokenSource CTSource;

        public Pcap(int port, ListView resultList)
        {
            IPList = resultList;
            Port = port;
        }

        public void StartCapturing()
        {
            StopCapturing();
            CTSource = new CancellationTokenSource();
            Receiver = new Task(() =>
            {
                CTSource.Token.ThrowIfCancellationRequested();

                IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

                if (allDevices.Count == 0)
                {
                    MessageBox.Show("No interfaces found! Make sure WinPcap is installed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IPAddress currentIP = GetCurrentIP();
                PacketDevice device = GetInterface(allDevices, currentIP);

                using (PacketCommunicator communicator = device.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
                using (BerkeleyPacketFilter filter = communicator.CreateFilter("ip and udp port " + Port))
                {
                    if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                    {
                        MessageBox.Show("This program works only on Ethernet networks.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string currentIPText = currentIP.ToString();

                    communicator.SetFilter(filter);
                    while (!CTSource.IsCancellationRequested)
                    {
                        PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out Packet packet);
                        switch (result)
                        {
                            case PacketCommunicatorReceiveResult.Timeout:
                                // Timeout elapsed
                                continue;
                            case PacketCommunicatorReceiveResult.Ok:
                                new Task(() => AddForeignIP(currentIPText, packet)).Start();
                                break;
                            default:
                                throw new InvalidOperationException("The result " + result + " should never be reached here");
                        }
                    }
                }
            }, CTSource.Token, TaskCreationOptions.LongRunning);
            Receiver.Start();
        }

        public void StopCapturing()
        {
            if (Receiver == null || CTSource == null || CTSource.IsCancellationRequested)
            {
                return;
            }

            CTSource.Cancel();

            try
            {
                Receiver.Wait();
            }
            catch (AggregateException)
            {

            }
            finally
            {
                CTSource.Dispose();
            }
        }

        private void AddForeignIP(string currentIPText, Packet packet)
        {
            IPList.Invoke(new Action(() =>
            {
                string ip = packet.Ethernet.IpV4.Source.ToString();
                if (!IPList.Items.ContainsKey(ip) && ip != currentIPText)
                {
                    IPList.Items.Add(ip).Name = ip;
                }

                ip = packet.Ethernet.IpV4.Destination.ToString();
                if (!IPList.Items.ContainsKey(ip) && ip != currentIPText)
                {
                    IPList.Items.Add(ip).Name = ip;
                }
            }));
        }

        private IPAddress GetCurrentIP()
        {
            UdpClient client = new UdpClient("google.com", 21684);
            var ip = ((IPEndPoint)client.Client.LocalEndPoint).Address;
            client.Close();
            return ip;
        }

        private PacketDevice GetInterface(IList<LivePacketDevice> allDevices, IPAddress currentIP)
        {
            foreach (var intfc in allDevices)
            {
                foreach (var address in intfc.Addresses)
                {
                    if (address.Address.ToString().Split(' ').Last() == currentIP.ToString())
                    {
                        return intfc;
                    }
                }
            }
            return null;
        }
    }
}
