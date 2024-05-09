using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Xml.Linq;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SerialTerminal
{
    public partial class MainForm : Form
    {
        SerialPort CommPort;
        TextWriter fs;
        bool logging = false;
        public delegate void UpdateTerminal(string data);
        public UpdateTerminal UpdateTerm;

        public MainForm()
        {
            InitializeComponent();
            this.UpdateTerm = new UpdateTerminal(UpdateTerminalMethod);
            CommPort = new SerialPort();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateComPortNames();
            PopulateBaudRates();
            bCommOpenClose.Text = "Open";

            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey("Software\\BeyondLogic\\TerminalProgram");

            cbBaudRate.Text = (string)key.GetValue("Baud Rate");
            cbPortNumber.Text = (string)key.GetValue("COM Port");
            tbLogFileName.Text = (string)key.GetValue("Log Filename");
            tbSend1.Text = (string)key.GetValue("Send Line 1");
            tbSend2.Text = (string)key.GetValue("Send Line 2");
            tbSend3.Text = (string)key.GetValue("Send Line 3");
            tbSend4.Text = (string)key.GetValue("Send Line 4");
        }
        Hashtable BuildPortNameHash(string[] oPortsToMap)
        {
            Hashtable oReturnTable = new Hashtable();
            MineRegistryForPortName("SYSTEM\\CurrentControlSet\\Enum", oReturnTable, oPortsToMap);
            return oReturnTable;
        }

        void MineRegistryForPortName(string strStartKey, Hashtable oTargetMap, string[] oPortNamesToMatch)
        {
            if (oTargetMap.Count >= oPortNamesToMatch.Length)
                return;

            RegistryKey oCurrentKey = Registry.LocalMachine;
            try
            {
                oCurrentKey = oCurrentKey.OpenSubKey(strStartKey);

                string[] oSubKeyNames = oCurrentKey.GetSubKeyNames();
                if (oSubKeyNames.Contains("Device Parameters") && strStartKey != "SYSTEM\\CurrentControlSet\\Enum")
                {
                    //listBox1.Items.Add("HKEY_LOCAL_MACHINE\\" + strStartKey + "\\Device Parameters");
                    object oPortNameValue = Registry.GetValue("HKEY_LOCAL_MACHINE\\" + strStartKey + "\\Device Parameters", "PortName", null);

                    if (oPortNameValue == null || oPortNamesToMatch.Contains(oPortNameValue.ToString()) == false) return;

                    object oFriendlyName = Registry.GetValue("HKEY_LOCAL_MACHINE\\" + strStartKey, "FriendlyName", null);

                    string strFriendlyName = "N/A";

                    if (oFriendlyName != null)
                    {
                        strFriendlyName = oFriendlyName.ToString();
                    }                    

                    if (strFriendlyName.Contains(oPortNameValue.ToString()) == false)
                        strFriendlyName = string.Format("{0} ({1})", strFriendlyName, oPortNameValue);

                    oTargetMap[strFriendlyName] = oPortNameValue;
                }
                else
                {
                    foreach (string strSubKey in oSubKeyNames)
                        MineRegistryForPortName(strStartKey + "\\" + strSubKey, oTargetMap, oPortNamesToMatch);
                }
            }
            catch
            {
            }
        }

        private void PopulateComPortNames()
        {
            cbPortNumber.Items.Clear();

            Hashtable m_oFriendlyNameMap = BuildPortNameHash(SerialPort.GetPortNames());
            IDictionaryEnumerator en = m_oFriendlyNameMap.GetEnumerator();
            while (en.MoveNext())
            {
                string value = en.Value.ToString();
                string key = en.Key.ToString();

                string[] Array = key.Split(new char[] { '(', ')' });
                cbPortNumber.Items.Add(value + ": " + Array[0]);
            }

            //string[] CommPorts;
            //CommPorts = SerialPort.GetPortNames();
            //foreach (string portName in CommPorts)
            //{
            //    cbPortNumber.Items.Add(portName);
            //}
        }

        private void PopulateBaudRates()
        {
            cbBaudRate.Items.Clear();
            cbBaudRate.Items.Add("1200");
            cbBaudRate.Items.Add("2400");
            cbBaudRate.Items.Add("9600");
            cbBaudRate.Items.Add("14400");
            cbBaudRate.Items.Add("19200");
            cbBaudRate.Items.Add("38400");
            cbBaudRate.Items.Add("57600");
            cbBaudRate.Items.Add("115200");

            // Select first value
            cbBaudRate.SelectedIndex = cbBaudRate.Items.IndexOf("115200");
        }

        private void UpdateTerminalMethod(string data)
        {
            this.Invoke((MethodInvoker)delegate
            {
                tbOutput.Text += data;
            });

            if (logging)
            {
                fs.Write(data);
            }
        }

        private void DataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string data = CommPort.ReadExisting();
            UpdateTerm(data);
        }

        private void OpenCommPort()
        {
            if (CommPort.IsOpen)
            {
                CommPort.Close();
            }

            // String may include friendly name i.e. "COM3: USB Serial Port";
            // Only use part of string before :
            if (cbPortNumber.Text != null)
            {
                string[] name = cbPortNumber.Text.Split(':');
                CommPort.PortName = name[0];
            } else
            {
                tsStatus.Text = "Error: Serial port name is null";
                return;
            }
            
            CommPort.BaudRate = Convert.ToInt32(cbBaudRate.Text);
            // 8N1:
            CommPort.DataBits = 8;
            CommPort.Parity = Parity.None;
            CommPort.StopBits = StopBits.One;
            // No flow control:
            CommPort.Handshake = Handshake.None;
            // Timeouts:
            CommPort.ReadTimeout = 500;
            CommPort.WriteTimeout = 500;

            CommPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedEventHandler);

            try
            {
                CommPort.Open();
                CommPort.DiscardInBuffer();
                tsStatus.Text = CommPort.PortName.ToString() + " opened at " + CommPort.BaudRate.ToString() + "bps";
                bCommOpenClose.Text = "Close";
            }
            catch (Exception ex)
            {
                tsStatus.Text = ex.Message.ToString();
            }
        }

        private void CommPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CloseCommPort()
        {
            if (CommPort != null)
            {
                if (CommPort.IsOpen)
                {
                    CommPort.Close();
                    tsStatus.Text = CommPort.PortName.ToString() + " closed";
                    bCommOpenClose.Text = "Open";
                }
            }
        }

        private void cbPortNumber_DropDown(object sender, EventArgs e)
        {
            PopulateComPortNames();
        }

        private void cbPortNumber_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OpenCommPort();
        }
        private void cbBaudRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // BaudRate Changed. Reopen port with new rate:
            OpenCommPort();
        }

        private void bCommOpenClose_Click(object sender, EventArgs e)
        {
            if (CommPort != null)
            {
                if (CommPort.IsOpen)
                {
                    CloseCommPort();
                }
                else
                {
                    OpenCommPort();
                }
            }
        }

        private void bSetFileName_Click(object sender, EventArgs e)
        {
            saveFileDialogLog.Filter = "Log Files (*.log)|*.log";
            saveFileDialogLog.FileName = tbLogFileName.Text;
            saveFileDialogLog.OverwritePrompt = false;
            saveFileDialogLog.ShowDialog();
            tbLogFileName.Text = saveFileDialogLog.FileName;
        }

        private void bLogStartStop_Click(object sender, EventArgs e)
        {
            if (logging)
            {
                // Stop Logging
                fs.Close();
                logging = false;
                bLogStartStop.Text = "Start";
                tsStatus.Text = "Logging stopped";
            } else
            {
                // Start Logging
                try
                {
                    fs = new StreamWriter(tbLogFileName.Text, append: true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Filename");
                    return;
                }
                logging = true;
                bLogStartStop.Text = "Stop";
                tsStatus.Text = "Started logging to " + tbLogFileName.Text;
            }
        }

        private void gbLogging_Enter(object sender, EventArgs e)
        {

        }

        private void CommPortSend(string str)
        {
            if (CommPort != null)
            {
                if (CommPort.IsOpen)
                {
                    CommPort.WriteLine(str);
                    // Local Echo 
                    tbOutput.Text += str;
                }
            }
        }

        private void bSend1_Click(object sender, EventArgs e)
        {
            CommPortSend(tbSend1.Text + "\r\n");
        }

        private void bSend2_Click(object sender, EventArgs e)
        {
            CommPortSend(tbSend2.Text + "\r\n");
        }

        private void bSend3_Click(object sender, EventArgs e)
        {
            CommPortSend(tbSend3.Text + "\r\n");
        }

        private void bSend4_Click(object sender, EventArgs e)
        {
            CommPortSend(tbSend4.Text + "\r\n");
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCommPort();

            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey("Software\\BeyondLogic\\TerminalProgram");
            string[] name = cbPortNumber.Text.Split(':');
            key.SetValue("COM Port", (string)name[0]);
            key.SetValue("Baud Rate", (string)cbBaudRate.Text);
            key.SetValue("Log Filename", (string)tbLogFileName.Text);
            key.SetValue("Send Line 1", (string)tbSend1.Text);
            key.SetValue("Send Line 2", (string)tbSend2.Text);
            key.SetValue("Send Line 3", (string)tbSend3.Text);
            key.SetValue("Send Line 4", (string)tbSend4.Text);
            key.Close();
        }
    }
}
