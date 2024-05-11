namespace SerialTerminal
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
            this.gbSerialPortSettings = new System.Windows.Forms.GroupBox();
            this.bCommOpenClose = new System.Windows.Forms.Button();
            this.lBaudRate = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.lPort = new System.Windows.Forms.Label();
            this.cbPortNumber = new System.Windows.Forms.ComboBox();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.gbLogging = new System.Windows.Forms.GroupBox();
            this.bSetFileName = new System.Windows.Forms.Button();
            this.bLogStartStop = new System.Windows.Forms.Button();
            this.tbLogFileName = new System.Windows.Forms.TextBox();
            this.saveFileDialogLog = new System.Windows.Forms.SaveFileDialog();
            this.gbSend = new System.Windows.Forms.GroupBox();
            this.bSend4 = new System.Windows.Forms.Button();
            this.tbSend4 = new System.Windows.Forms.TextBox();
            this.bSend3 = new System.Windows.Forms.Button();
            this.tbSend3 = new System.Windows.Forms.TextBox();
            this.bSend2 = new System.Windows.Forms.Button();
            this.tbSend2 = new System.Windows.Forms.TextBox();
            this.bSend1 = new System.Windows.Forms.Button();
            this.tbSend1 = new System.Windows.Forms.TextBox();
            this.cbANSIParse = new System.Windows.Forms.CheckBox();
            this.cbANSIRemove = new System.Windows.Forms.CheckBox();
            this.gbSerialPortSettings.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.gbLogging.SuspendLayout();
            this.gbSend.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSerialPortSettings
            // 
            this.gbSerialPortSettings.Controls.Add(this.bCommOpenClose);
            this.gbSerialPortSettings.Controls.Add(this.lBaudRate);
            this.gbSerialPortSettings.Controls.Add(this.cbBaudRate);
            this.gbSerialPortSettings.Controls.Add(this.lPort);
            this.gbSerialPortSettings.Controls.Add(this.cbPortNumber);
            this.gbSerialPortSettings.Location = new System.Drawing.Point(12, 3);
            this.gbSerialPortSettings.Name = "gbSerialPortSettings";
            this.gbSerialPortSettings.Size = new System.Drawing.Size(544, 50);
            this.gbSerialPortSettings.TabIndex = 0;
            this.gbSerialPortSettings.TabStop = false;
            this.gbSerialPortSettings.Text = "Serial Port";
            // 
            // bCommOpenClose
            // 
            this.bCommOpenClose.Location = new System.Drawing.Point(461, 15);
            this.bCommOpenClose.Name = "bCommOpenClose";
            this.bCommOpenClose.Size = new System.Drawing.Size(75, 28);
            this.bCommOpenClose.TabIndex = 3;
            this.bCommOpenClose.Text = "Open";
            this.bCommOpenClose.UseVisualStyleBackColor = true;
            this.bCommOpenClose.Click += new System.EventHandler(this.bCommOpenClose_Click);
            // 
            // lBaudRate
            // 
            this.lBaudRate.AutoSize = true;
            this.lBaudRate.Location = new System.Drawing.Point(327, 23);
            this.lBaudRate.Name = "lBaudRate";
            this.lBaudRate.Size = new System.Drawing.Size(58, 13);
            this.lBaudRate.TabIndex = 3;
            this.lBaudRate.Text = "Baud Rate";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(391, 18);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(64, 21);
            this.cbBaudRate.TabIndex = 1;
            this.cbBaudRate.SelectionChangeCommitted += new System.EventHandler(this.cbBaudRate_SelectionChangeCommitted);
            // 
            // lPort
            // 
            this.lPort.AutoSize = true;
            this.lPort.Location = new System.Drawing.Point(7, 23);
            this.lPort.Name = "lPort";
            this.lPort.Size = new System.Drawing.Size(26, 13);
            this.lPort.TabIndex = 1;
            this.lPort.Text = "Port";
            // 
            // cbPortNumber
            // 
            this.cbPortNumber.FormattingEnabled = true;
            this.cbPortNumber.Location = new System.Drawing.Point(39, 18);
            this.cbPortNumber.Name = "cbPortNumber";
            this.cbPortNumber.Size = new System.Drawing.Size(282, 21);
            this.cbPortNumber.TabIndex = 0;
            this.cbPortNumber.DropDown += new System.EventHandler(this.cbPortNumber_DropDown);
            this.cbPortNumber.SelectionChangeCommitted += new System.EventHandler(this.cbPortNumber_SelectionChangeCommitted);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatus});
            this.statusStripMain.Location = new System.Drawing.Point(0, 618);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(970, 22);
            this.statusStripMain.TabIndex = 1;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // tsStatus
            // 
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.tbOutput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.Color.LightGray;
            this.tbOutput.Location = new System.Drawing.Point(18, 53);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(946, 441);
            this.tbOutput.TabIndex = 0;
            this.tbOutput.TabStop = false;
            this.tbOutput.Text = "";
            // 
            // gbLogging
            // 
            this.gbLogging.Controls.Add(this.bSetFileName);
            this.gbLogging.Controls.Add(this.bLogStartStop);
            this.gbLogging.Controls.Add(this.tbLogFileName);
            this.gbLogging.Location = new System.Drawing.Point(562, 3);
            this.gbLogging.Name = "gbLogging";
            this.gbLogging.Size = new System.Drawing.Size(396, 50);
            this.gbLogging.TabIndex = 3;
            this.gbLogging.TabStop = false;
            this.gbLogging.Text = "Log";
            this.gbLogging.Enter += new System.EventHandler(this.gbLogging_Enter);
            // 
            // bSetFileName
            // 
            this.bSetFileName.Location = new System.Drawing.Point(283, 15);
            this.bSetFileName.Name = "bSetFileName";
            this.bSetFileName.Size = new System.Drawing.Size(26, 28);
            this.bSetFileName.TabIndex = 5;
            this.bSetFileName.Text = "...";
            this.bSetFileName.UseVisualStyleBackColor = true;
            this.bSetFileName.Click += new System.EventHandler(this.bSetFileName_Click);
            // 
            // bLogStartStop
            // 
            this.bLogStartStop.Location = new System.Drawing.Point(315, 16);
            this.bLogStartStop.Name = "bLogStartStop";
            this.bLogStartStop.Size = new System.Drawing.Size(75, 28);
            this.bLogStartStop.TabIndex = 6;
            this.bLogStartStop.Text = "Start";
            this.bLogStartStop.UseVisualStyleBackColor = true;
            this.bLogStartStop.Click += new System.EventHandler(this.bLogStartStop_Click);
            // 
            // tbLogFileName
            // 
            this.tbLogFileName.Location = new System.Drawing.Point(6, 20);
            this.tbLogFileName.Name = "tbLogFileName";
            this.tbLogFileName.Size = new System.Drawing.Size(271, 20);
            this.tbLogFileName.TabIndex = 4;
            // 
            // gbSend
            // 
            this.gbSend.Controls.Add(this.bSend4);
            this.gbSend.Controls.Add(this.tbSend4);
            this.gbSend.Controls.Add(this.bSend3);
            this.gbSend.Controls.Add(this.tbSend3);
            this.gbSend.Controls.Add(this.bSend2);
            this.gbSend.Controls.Add(this.tbSend2);
            this.gbSend.Controls.Add(this.bSend1);
            this.gbSend.Controls.Add(this.tbSend1);
            this.gbSend.Location = new System.Drawing.Point(12, 533);
            this.gbSend.Name = "gbSend";
            this.gbSend.Size = new System.Drawing.Size(946, 82);
            this.gbSend.TabIndex = 6;
            this.gbSend.TabStop = false;
            this.gbSend.Text = "Send";
            // 
            // bSend4
            // 
            this.bSend4.Location = new System.Drawing.Point(851, 46);
            this.bSend4.Name = "bSend4";
            this.bSend4.Size = new System.Drawing.Size(75, 23);
            this.bSend4.TabIndex = 12;
            this.bSend4.Text = "Send";
            this.bSend4.UseCompatibleTextRendering = true;
            this.bSend4.UseVisualStyleBackColor = true;
            this.bSend4.Click += new System.EventHandler(this.bSend4_Click);
            // 
            // tbSend4
            // 
            this.tbSend4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSend4.Location = new System.Drawing.Point(490, 48);
            this.tbSend4.Name = "tbSend4";
            this.tbSend4.Size = new System.Drawing.Size(355, 21);
            this.tbSend4.TabIndex = 11;
            // 
            // bSend3
            // 
            this.bSend3.Location = new System.Drawing.Point(851, 19);
            this.bSend3.Name = "bSend3";
            this.bSend3.Size = new System.Drawing.Size(75, 23);
            this.bSend3.TabIndex = 10;
            this.bSend3.Text = "Send";
            this.bSend3.UseCompatibleTextRendering = true;
            this.bSend3.UseVisualStyleBackColor = true;
            this.bSend3.Click += new System.EventHandler(this.bSend3_Click);
            // 
            // tbSend3
            // 
            this.tbSend3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSend3.Location = new System.Drawing.Point(490, 21);
            this.tbSend3.Name = "tbSend3";
            this.tbSend3.Size = new System.Drawing.Size(355, 21);
            this.tbSend3.TabIndex = 9;
            // 
            // bSend2
            // 
            this.bSend2.Location = new System.Drawing.Point(380, 48);
            this.bSend2.Name = "bSend2";
            this.bSend2.Size = new System.Drawing.Size(75, 23);
            this.bSend2.TabIndex = 8;
            this.bSend2.Text = "Send";
            this.bSend2.UseCompatibleTextRendering = true;
            this.bSend2.UseVisualStyleBackColor = true;
            this.bSend2.Click += new System.EventHandler(this.bSend2_Click);
            // 
            // tbSend2
            // 
            this.tbSend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSend2.Location = new System.Drawing.Point(19, 50);
            this.tbSend2.Name = "tbSend2";
            this.tbSend2.Size = new System.Drawing.Size(355, 21);
            this.tbSend2.TabIndex = 7;
            // 
            // bSend1
            // 
            this.bSend1.Location = new System.Drawing.Point(380, 19);
            this.bSend1.Name = "bSend1";
            this.bSend1.Size = new System.Drawing.Size(75, 23);
            this.bSend1.TabIndex = 6;
            this.bSend1.Text = "Send";
            this.bSend1.UseCompatibleTextRendering = true;
            this.bSend1.UseVisualStyleBackColor = true;
            this.bSend1.Click += new System.EventHandler(this.bSend1_Click);
            // 
            // tbSend1
            // 
            this.tbSend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSend1.Location = new System.Drawing.Point(19, 19);
            this.tbSend1.Name = "tbSend1";
            this.tbSend1.Size = new System.Drawing.Size(355, 21);
            this.tbSend1.TabIndex = 5;
            // 
            // cbANSIParse
            // 
            this.cbANSIParse.AutoSize = true;
            this.cbANSIParse.Location = new System.Drawing.Point(838, 505);
            this.cbANSIParse.Name = "cbANSIParse";
            this.cbANSIParse.Size = new System.Drawing.Size(114, 17);
            this.cbANSIParse.TabIndex = 9;
            this.cbANSIParse.Text = "Parse ANSI Codes";
            this.cbANSIParse.UseVisualStyleBackColor = true;
            // 
            // cbANSIRemove
            // 
            this.cbANSIRemove.AutoSize = true;
            this.cbANSIRemove.Location = new System.Drawing.Point(703, 505);
            this.cbANSIRemove.Name = "cbANSIRemove";
            this.cbANSIRemove.Size = new System.Drawing.Size(127, 17);
            this.cbANSIRemove.TabIndex = 10;
            this.cbANSIRemove.Text = "Remove ANSI Codes";
            this.cbANSIRemove.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 640);
            this.Controls.Add(this.cbANSIRemove);
            this.Controls.Add(this.cbANSIParse);
            this.Controls.Add(this.gbSend);
            this.Controls.Add(this.gbLogging);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.gbSerialPortSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbSerialPortSettings.ResumeLayout(false);
            this.gbSerialPortSettings.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.gbLogging.ResumeLayout(false);
            this.gbLogging.PerformLayout();
            this.gbSend.ResumeLayout(false);
            this.gbSend.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSerialPortSettings;
        private System.Windows.Forms.Label lPort;
        private System.Windows.Forms.ComboBox cbPortNumber;
        private System.Windows.Forms.Label lBaudRate;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.RichTextBox tbOutput;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.Button bCommOpenClose;
        private System.Windows.Forms.GroupBox gbLogging;
        private System.Windows.Forms.Button bLogStartStop;
        private System.Windows.Forms.TextBox tbLogFileName;
        private System.Windows.Forms.Button bSetFileName;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLog;
        private System.Windows.Forms.GroupBox gbSend;
        private System.Windows.Forms.Button bSend4;
        private System.Windows.Forms.TextBox tbSend4;
        private System.Windows.Forms.Button bSend3;
        private System.Windows.Forms.TextBox tbSend3;
        private System.Windows.Forms.Button bSend2;
        private System.Windows.Forms.TextBox tbSend2;
        private System.Windows.Forms.Button bSend1;
        private System.Windows.Forms.TextBox tbSend1;
        private System.Windows.Forms.CheckBox cbANSIParse;
        private System.Windows.Forms.CheckBox cbANSIRemove;
    }
}

