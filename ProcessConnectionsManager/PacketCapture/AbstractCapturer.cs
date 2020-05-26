using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessConnectionsManager.PacketCapture
{
    public abstract class AbstractCapturer
    {
        protected readonly ListView IPList;
        protected readonly int Port;

        protected AbstractCapturer(int port, ListView resultList)
        {
            IPList = resultList;
            Port = port;
        }

        public abstract void StartCapturing();

        public abstract void StopCapturing();

        protected void AddForeignIP(IEnumerable<string> ignoredAddresses, params string[] addresses)
        {
            IPList.Invoke(new Action(() =>
            {
                foreach (var ip in addresses)
                {
                    if (!IPList.Items.ContainsKey(ip) && !ignoredAddresses.Contains(ip))
                    {
                        IPList.Items.Add(ip).Name = ip;
                    }
                }
            }));
        }

        protected void AddForeignIP(params string[] addresses)
        {
            IPList.Invoke(new Action(() =>
            {
                foreach (var ip in addresses)
                {
                    if (!IPList.Items.ContainsKey(ip))
                    {
                        IPList.Items.Add(ip).Name = ip;
                    }
                }
            }));
        }
    }
}
