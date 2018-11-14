using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ProcessConnectionsManager
{
    public static class UDPListener
    {
        private static UdpClient UDPClient;
        private static Task Listener;
        private static CancellationTokenSource CancelTokenSource;

        public static void StartListening(int port, ListView displayList)
        {
            displayList.Items.Clear();
            StopListening();
            CancelTokenSource = new CancellationTokenSource();

            Listener = new Task(() =>
            {
                CancelTokenSource.Token.ThrowIfCancellationRequested();
                UDPClient = new UdpClient(new IPEndPoint(IPAddress.Any, port));

                while (!CancelTokenSource.IsCancellationRequested)
                {                    
                    var task = UDPClient.ReceiveAsync();
                    task.Start();

                    while (!CancelTokenSource.IsCancellationRequested)
                    {
                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            var result = task.Result;
                            ListViewItem listItem = new ListViewItem(result.RemoteEndPoint.Address.ToString());
                            if (!displayList.Items.Contains(listItem))
                            {
                                displayList.Items.Add(listItem);
                            }
                            break;
                        }
                    }
                }

                // Task cancellation requested.
                UDPClient.Close();
                CancelTokenSource.Token.ThrowIfCancellationRequested();
            }, CancelTokenSource.Token, TaskCreationOptions.LongRunning);
            Listener.Start();
        }

        public static void StopListening()
        {
            if (CancelTokenSource != null && CancelTokenSource.Token.CanBeCanceled && !CancelTokenSource.IsCancellationRequested)
            {
                CancelTokenSource.Cancel();

                try
                {
                    Listener.Wait();
                }
                catch (AggregateException)
                {
                    CancelTokenSource.Dispose();
                }
            }
        }
    }
}
