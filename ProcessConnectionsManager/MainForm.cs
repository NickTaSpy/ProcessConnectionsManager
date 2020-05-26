using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using NetFwTypeLib;
using ProcessConnectionsManager.Block;
using ProcessConnectionsManager.PacketCapture;

namespace ProcessConnectionsManager
{
    public partial class MainForm : Form
    {
        private AbstractCapturer Capturer;

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
                if (string.IsNullOrEmpty(ProcessNameBox.Text))
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
                ProcessPathTextBox.Text = process.GetMainModuleFileName();
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
            switch (WinFirewall.IsFirewallEnabled())
            {
                case FirewallStatus.Enabled:
                    MessageBox.Show("Windows firewall is enabled.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    break;
                case FirewallStatus.PrivateDisabled:
                    MessageBox.Show("Private networks are disabled in windows firewall. This may not allow you to block connections successfully.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    break;
                case FirewallStatus.PublicDisabled:
                    MessageBox.Show("Public networks are disabled in windows firewall. This may not allow you to block connections successfully.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    break;
                default:
                    MessageBox.Show("Windows firewall is disabled. This will not allow you to block connections successfully.", "Windows Firewall Check", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    break;
            }
        }

        private void PIDRadio_CheckedChanged(object sender, EventArgs e)
        {
            ProcessNameBox.ReadOnly = !ProcessNameBox.ReadOnly;
            PIDBox.ReadOnly = !PIDBox.ReadOnly;
        }

        private void PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Capturer?.StopCapturing();
            SetUDPListenerStatusLabel(false);

            ForeignIPList.Items.Clear();

            if (PortList.SelectedItems.Count == 0)
            {
                UDPListenButton.Enabled = false;
                return;
            }

            var selectedItem = PortList.SelectedItems[0];
            UDPListenButton.Enabled = selectedItem.SubItems[2].Text == "UDP";

            PortTextBox.Text = selectedItem.SubItems[0].Text;
            ForeignIPTextBox.Text = selectedItem.SubItems[1].Text;
            ProtocolTextBox.Text = selectedItem.SubItems[2].Text;
        }

        private void ForeignIPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ForeignIPList.SelectedItems.Count < 1)
            {
                ForeignIPList.Items.Clear();
                return;
            }

            var selectedItem = ForeignIPList.SelectedItems[0];
            ForeignIPTextBox.Text = selectedItem.Text;
        }

        private void BlockButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PortTextBox.Text) || string.IsNullOrEmpty(ProtocolTextBox.Text))
            {
                MessageBox.Show("Missing selection. Choose a result from the 'Find ports' section.", "Missing selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(ForeignIPTextBox.Text))
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
            if (Capturer != null)
                Capturer.StopCapturing();

            Capturer = new Sharppcap(int.Parse(PortList.SelectedItems[0].SubItems[0].Text), ForeignIPList);
            Capturer.StartCapturing();
            SetUDPListenerStatusLabel(true);
        }

        private void SetUDPListenerStatusLabel(bool on)
        {
            if (on)
            {
                UDPListenerStatusLabel.Text = "ON";
                UDPListenerStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                UDPListenerStatusLabel.Text = "OFF";
                UDPListenerStatusLabel.ForeColor = Color.Red;
            }
        }
    }
}
