using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessConnectionsManager.PacketCapture
{
    public abstract class AbstractCapturer
    {
        protected readonly ListView IPList;
        protected readonly int Port;

        protected Task Receiver;
        protected CancellationTokenSource CTSource;

        protected AbstractCapturer(int port, ListView resultList)
        {
            IPList = resultList;
            Port = port;
        }

        public void StartCapturing()
        {
            StopCapturing();
            CTSource = new CancellationTokenSource();
            Receiver = Task.Factory.StartNew(Capture, CTSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void StopCapturing()
        {
            if (Receiver == null || CTSource?.IsCancellationRequested == true)
            {
                return;
            }

            try
            {
                CTSource.Cancel();
                Receiver.Wait();
            }
            finally
            {
                CTSource.Dispose();
            }
        }

        /// <summary>
        /// Must be implemented using the inherited cancellation token.
        /// </summary>
        protected abstract void Capture();

        protected void AddForeignIP(string currentIPText, params string[] addresses)
        {
            IPList.Invoke(new Action(() =>
            {
                foreach (var ip in addresses)
                {
                    if (!IPList.Items.ContainsKey(ip) && ip != currentIPText)
                    {
                        IPList.Items.Add(ip).Name = ip;
                    }
                }
            }));
        }

        protected IPAddress GetCurrentIP()
        {
            using var client = new UdpClient("google.com", 21684);
            return ((IPEndPoint)client.Client.LocalEndPoint).Address;
        }
    }
}
