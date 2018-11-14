using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetFwTypeLib;

namespace ProcessConnectionsManager
{
    public partial class MainForm : Form
    {
        private Pcap PcapReceiver;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FindPortsButton_Click(object sender, EventArgs e)
        {
            Process process;
            List<Port> ports;

            if (PIDRadio.Checked)
            {
                if (!int.TryParse(PIDBox.Text, out int pid))
                {
                    return;
                }
                ports = ProcessInformation.GetPortsByPID(pid, out process);
            }
            else
            {
                if (ProcessNameBox.Text == "")
                {
                    return;
                }
                ports = ProcessInformation.GetPortsByProcessName(ProcessNameBox.Text, out process);
            }

            PortList.Items.Clear();
            ProcessPathTextBox.Text = "";

            foreach (var port in ports)
            {
                PortList.Items.Add(new ListViewItem(new string[] { port.PortNumber, port.ForeignIP, port.Protocol }));
            }

            if (process != null)
            {
                try
                {
                    ProcessPathTextBox.Text = process.MainModule.FileName;
                }
                catch (Exception)
                {
                    ProcessPathTextBox.Text = "";
                }
            }
        }

        private void ProcessNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindPortsButton_Click(sender, EventArgs.Empty);
            }
        }

        private void PIDBox_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessNameBox_KeyDown(sender, e);
        }

        private void FwCheckButton_Click(object sender, EventArgs e)
        {
            FirewallStatus fwStatus = WinFirewall.IsFirewallEnabled();

            if (fwStatus == FirewallStatus.Enabled)
            {
                MessageBox.Show("Windows firewall is enabled.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else if (fwStatus == FirewallStatus.PrivateDisabled)
            {
                MessageBox.Show("Private networks are disabled in windows firewall. This may not allow you to block connections successfully.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else if (fwStatus == FirewallStatus.PublicDisabled)
            {
                MessageBox.Show("Public networks are disabled in windows firewall. This may not allow you to block connections successfully.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Windows firewall is disabled. This will not allow you to block connections successfully.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void PIDRadio_CheckedChanged(object sender, EventArgs e)
        {
            ProcessNameBox.ReadOnly = !ProcessNameBox.ReadOnly;
            PIDBox.ReadOnly = !PIDBox.ReadOnly;
        }

        private void PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PcapReceiver != null)
            {
                PcapReceiver.StopCapturing();
            }

            ForeignIPList.Items.Clear();

            if (PortList.SelectedItems.Count == 0)
            {
                UDPListenButton.Enabled = false;
                return;
            }

            var selectedItem = PortList.SelectedItems[0];
            if (selectedItem.SubItems[2].Text == "UDP")
            {
                UDPListenButton.Enabled = true;
            }
            else
            {
                UDPListenButton.Enabled = false;
            }

            PortTextBox.Text = selectedItem.SubItems[0].Text;
            ForeignIPTextBox.Text = selectedItem.SubItems[1].Text;
            ProtocolTextBox.Text = selectedItem.SubItems[2].Text;
        }

        private void ForeignIPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ForeignIPList.SelectedItems.Count == 0)
            {
                ForeignIPList.Items.Clear();
                return;
            }

            var selectedItem = ForeignIPList.SelectedItems[0];
            ForeignIPTextBox.Text = selectedItem.Text;
        }

        private void BlockButton_Click(object sender, EventArgs e)
        {
            if (PortTextBox.Text == "" || ProtocolTextBox.Text == "")
            {
                MessageBox.Show("Missing selection. Choose a result from the 'Find ports' section.", "Missing selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ForeignIPTextBox.Text == "")
            {
                MessageBox.Show("A foreign IP is required. Use the 'Listen for remote connections' button if the protocol is UDP and choose one of the IP addresses that may appear over time.", "No foreign IP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FirewallStatus status = WinFirewall.IsFirewallEnabled();
            if (status == FirewallStatus.PrivateDisabled || status == FirewallStatus.PublicDisabled || status == FirewallStatus.AllDisabled)
            {
                MessageBox.Show("Your windows firewall is not fully enabled which may prevent blocks from being effective. Change your firewall settings through the control panel. Use the 'Firewall Status Check' button for more information.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WinFirewall.AddFirewallRule(ProtocolTextBox.Text == "UDP" ? NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP : NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, ForeignIPTextBox.Text, PortTextBox.Text, ProcessPathTextBox.Text);
        }

        private void RemoveBlocksButton_Click(object sender, EventArgs e)
        {
            WinFirewall.RemoveAllRules();
        }

        private void UDPListenButton_Click(object sender, EventArgs e)
        {
            PcapReceiver = new Pcap(int.Parse(PortList.SelectedItems[0].SubItems[0].Text), ForeignIPList);
            PcapReceiver.StartCapturing();
        }
    }
}
