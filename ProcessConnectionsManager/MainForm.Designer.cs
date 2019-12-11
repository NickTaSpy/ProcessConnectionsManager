namespace ProcessConnectionsManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ProcessNameBox = new System.Windows.Forms.TextBox();
            this.PortList = new System.Windows.Forms.ListView();
            this.Port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ForeignIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Protocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FindPortsButton = new System.Windows.Forms.Button();
            this.FwCheckButton = new System.Windows.Forms.Button();
            this.BlockButton = new System.Windows.Forms.Button();
            this.RemoveBlocksButton = new System.Windows.Forms.Button();
            this.ProcessPathTextBox = new System.Windows.Forms.TextBox();
            this.ProcessNameRadio = new System.Windows.Forms.RadioButton();
            this.PIDRadio = new System.Windows.Forms.RadioButton();
            this.PIDBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.ProtocolTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ForeignIPTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UDPListenButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ForeignIPList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListenerToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessNameBox
            // 
            this.ProcessNameBox.Location = new System.Drawing.Point(6, 39);
            this.ProcessNameBox.Name = "ProcessNameBox";
            this.ProcessNameBox.ReadOnly = true;
            this.ProcessNameBox.Size = new System.Drawing.Size(119, 20);
            this.ProcessNameBox.TabIndex = 0;
            this.ProcessNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProcessNameBox_KeyDown);
            // 
            // PortList
            // 
            this.PortList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PortList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Port,
            this.ForeignIP,
            this.Protocol});
            this.PortList.FullRowSelect = true;
            this.PortList.GridLines = true;
            this.PortList.HideSelection = false;
            this.PortList.Location = new System.Drawing.Point(6, 147);
            this.PortList.MultiSelect = false;
            this.PortList.Name = "PortList";
            this.PortList.Size = new System.Drawing.Size(264, 273);
            this.PortList.TabIndex = 2;
            this.PortList.UseCompatibleStateImageBehavior = false;
            this.PortList.View = System.Windows.Forms.View.Details;
            this.PortList.SelectedIndexChanged += new System.EventHandler(this.PortList_SelectedIndexChanged);
            // 
            // Port
            // 
            this.Port.Text = "Port";
            this.Port.Width = 54;
            // 
            // ForeignIP
            // 
            this.ForeignIP.Text = "Foreign IP";
            this.ForeignIP.Width = 120;
            // 
            // Protocol
            // 
            this.Protocol.Text = "Protocol";
            this.Protocol.Width = 54;
            // 
            // FindPortsButton
            // 
            this.FindPortsButton.Location = new System.Drawing.Point(100, 65);
            this.FindPortsButton.Name = "FindPortsButton";
            this.FindPortsButton.Size = new System.Drawing.Size(75, 32);
            this.FindPortsButton.TabIndex = 3;
            this.FindPortsButton.Text = "Find Ports";
            this.FindPortsButton.UseVisualStyleBackColor = true;
            this.FindPortsButton.Click += new System.EventHandler(this.FindPortsButton_Click);
            // 
            // FwCheckButton
            // 
            this.FwCheckButton.Location = new System.Drawing.Point(6, 147);
            this.FwCheckButton.Name = "FwCheckButton";
            this.FwCheckButton.Size = new System.Drawing.Size(119, 34);
            this.FwCheckButton.TabIndex = 5;
            this.FwCheckButton.Text = "Firewall Status Check";
            this.FwCheckButton.UseVisualStyleBackColor = true;
            this.FwCheckButton.Click += new System.EventHandler(this.FwCheckButton_Click);
            // 
            // BlockButton
            // 
            this.BlockButton.Location = new System.Drawing.Point(98, 73);
            this.BlockButton.Name = "BlockButton";
            this.BlockButton.Size = new System.Drawing.Size(86, 29);
            this.BlockButton.TabIndex = 6;
            this.BlockButton.Text = "Block";
            this.BlockButton.UseVisualStyleBackColor = true;
            this.BlockButton.Click += new System.EventHandler(this.BlockButton_Click);
            // 
            // RemoveBlocksButton
            // 
            this.RemoveBlocksButton.Location = new System.Drawing.Point(170, 147);
            this.RemoveBlocksButton.Name = "RemoveBlocksButton";
            this.RemoveBlocksButton.Size = new System.Drawing.Size(111, 34);
            this.RemoveBlocksButton.TabIndex = 11;
            this.RemoveBlocksButton.Text = "Remove All Blocks";
            this.RemoveBlocksButton.UseVisualStyleBackColor = true;
            this.RemoveBlocksButton.Click += new System.EventHandler(this.RemoveBlocksButton_Click);
            // 
            // ProcessPathTextBox
            // 
            this.ProcessPathTextBox.Location = new System.Drawing.Point(6, 121);
            this.ProcessPathTextBox.Name = "ProcessPathTextBox";
            this.ProcessPathTextBox.ReadOnly = true;
            this.ProcessPathTextBox.Size = new System.Drawing.Size(264, 20);
            this.ProcessPathTextBox.TabIndex = 12;
            // 
            // ProcessNameRadio
            // 
            this.ProcessNameRadio.AutoSize = true;
            this.ProcessNameRadio.Location = new System.Drawing.Point(6, 16);
            this.ProcessNameRadio.Name = "ProcessNameRadio";
            this.ProcessNameRadio.Size = new System.Drawing.Size(92, 17);
            this.ProcessNameRadio.TabIndex = 14;
            this.ProcessNameRadio.Text = "Process name";
            this.ProcessNameRadio.UseVisualStyleBackColor = true;
            // 
            // PIDRadio
            // 
            this.PIDRadio.AutoSize = true;
            this.PIDRadio.Checked = true;
            this.PIDRadio.Location = new System.Drawing.Point(149, 16);
            this.PIDRadio.Name = "PIDRadio";
            this.PIDRadio.Size = new System.Drawing.Size(120, 17);
            this.PIDRadio.TabIndex = 15;
            this.PIDRadio.TabStop = true;
            this.PIDRadio.Text = "PID (more accurate)";
            this.PIDRadio.UseVisualStyleBackColor = true;
            this.PIDRadio.CheckedChanged += new System.EventHandler(this.PIDRadio_CheckedChanged);
            // 
            // PIDBox
            // 
            this.PIDBox.Location = new System.Drawing.Point(149, 39);
            this.PIDBox.Name = "PIDBox";
            this.PIDBox.Size = new System.Drawing.Size(121, 20);
            this.PIDBox.TabIndex = 16;
            this.PIDBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PIDBox_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.PortList);
            this.groupBox1.Controls.Add(this.ProcessPathTextBox);
            this.groupBox1.Controls.Add(this.PIDBox);
            this.groupBox1.Controls.Add(this.FindPortsButton);
            this.groupBox1.Controls.Add(this.PIDRadio);
            this.groupBox1.Controls.Add(this.ProcessNameBox);
            this.groupBox1.Controls.Add(this.ProcessNameRadio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 426);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find ports";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Process file path (sometimes not available)";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.RemoveBlocksButton);
            this.groupBox2.Controls.Add(this.FwCheckButton);
            this.groupBox2.Location = new System.Drawing.Point(467, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 426);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Windows Firewall";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PortTextBox);
            this.groupBox4.Controls.Add(this.BlockButton);
            this.groupBox4.Controls.Add(this.ProtocolTextBox);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.ForeignIPTextBox);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(6, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 115);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selection";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(20, 42);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.ReadOnly = true;
            this.PortTextBox.Size = new System.Drawing.Size(72, 20);
            this.PortTextBox.TabIndex = 25;
            // 
            // ProtocolTextBox
            // 
            this.ProtocolTextBox.Location = new System.Drawing.Point(206, 42);
            this.ProtocolTextBox.Name = "ProtocolTextBox";
            this.ProtocolTextBox.ReadOnly = true;
            this.ProtocolTextBox.Size = new System.Drawing.Size(47, 20);
            this.ProtocolTextBox.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Foreign IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Port";
            // 
            // ForeignIPTextBox
            // 
            this.ForeignIPTextBox.Location = new System.Drawing.Point(98, 42);
            this.ForeignIPTextBox.Name = "ForeignIPTextBox";
            this.ForeignIPTextBox.ReadOnly = true;
            this.ForeignIPTextBox.Size = new System.Drawing.Size(102, 20);
            this.ForeignIPTextBox.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Protocol";
            // 
            // UDPListenButton
            // 
            this.UDPListenButton.Enabled = false;
            this.UDPListenButton.Location = new System.Drawing.Point(6, 19);
            this.UDPListenButton.Name = "UDPListenButton";
            this.UDPListenButton.Size = new System.Drawing.Size(156, 34);
            this.UDPListenButton.TabIndex = 20;
            this.UDPListenButton.Text = "Listen for remote connections";
            this.ListenerToolTip.SetToolTip(this.UDPListenButton, "When a UDP port is selected from the \'Find Ports\' list you need to press this but" +
        "ton to look for the remote IP Addresses that communicate through this port.");
            this.UDPListenButton.UseVisualStyleBackColor = true;
            this.UDPListenButton.Click += new System.EventHandler(this.UDPListenButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.ForeignIPList);
            this.groupBox3.Controls.Add(this.UDPListenButton);
            this.groupBox3.Location = new System.Drawing.Point(293, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 426);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "UDP Listener";
            // 
            // ForeignIPList
            // 
            this.ForeignIPList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ForeignIPList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.ForeignIPList.FullRowSelect = true;
            this.ForeignIPList.GridLines = true;
            this.ForeignIPList.HideSelection = false;
            this.ForeignIPList.Location = new System.Drawing.Point(6, 59);
            this.ForeignIPList.MultiSelect = false;
            this.ForeignIPList.Name = "ForeignIPList";
            this.ForeignIPList.Size = new System.Drawing.Size(156, 361);
            this.ForeignIPList.TabIndex = 22;
            this.ForeignIPList.UseCompatibleStateImageBehavior = false;
            this.ForeignIPList.View = System.Windows.Forms.View.Details;
            this.ForeignIPList.SelectedIndexChanged += new System.EventHandler(this.ForeignIPList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Foreign IP";
            this.columnHeader1.Width = 144;
            // 
            // ListenerToolTip
            // 
            this.ListenerToolTip.AutoPopDelay = 10000;
            this.ListenerToolTip.InitialDelay = 500;
            this.ListenerToolTip.ReshowDelay = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(780, 429);
            this.Name = "MainForm";
            this.Text = "Process Connections Manager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ProcessNameBox;
        private System.Windows.Forms.ListView PortList;
        private System.Windows.Forms.ColumnHeader Port;
        private System.Windows.Forms.ColumnHeader Protocol;
        private System.Windows.Forms.Button FindPortsButton;
        private System.Windows.Forms.ColumnHeader ForeignIP;
        private System.Windows.Forms.Button FwCheckButton;
        private System.Windows.Forms.Button BlockButton;
        private System.Windows.Forms.Button RemoveBlocksButton;
        private System.Windows.Forms.TextBox ProcessPathTextBox;
        private System.Windows.Forms.RadioButton ProcessNameRadio;
        private System.Windows.Forms.RadioButton PIDRadio;
        private System.Windows.Forms.TextBox PIDBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.TextBox ProtocolTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ForeignIPTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button UDPListenButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip ListenerToolTip;
        private System.Windows.Forms.ListView ForeignIPList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

