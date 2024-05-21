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
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using System.Reflection;
using System.Diagnostics;

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
 
            string PortNumber = (string)key.GetValue("COM Port");
            if (PortNumber != null)
            {
                if (PortNumber.Length > 1)
                {
                    for (int i = 0; i < cbPortNumber.Items.Count; i++)
                    {
                        if (cbPortNumber.Items[i].ToString().StartsWith(PortNumber))
                        {
                            cbPortNumber.SelectedIndex = i;
                            //cbPortNumber.Text = cbPortNumber.Items[i].ToString();
                        }
                    }
                }
            }
 
            tbLogFileName.Text = (string)key.GetValue("Log Filename");
            tbSend1.Text = (string)key.GetValue("Send Line 1");
            tbSend2.Text = (string)key.GetValue("Send Line 2");
            tbSend3.Text = (string)key.GetValue("Send Line 3");
            tbSend4.Text = (string)key.GetValue("Send Line 4");
            cbAutoScroll.Checked = Convert.ToBoolean(key.GetValue("AutoScroll"));
            if (key.GetValue("Left") != null)
                this.Left = (int)key.GetValue("Left");
            if (key.GetValue("Top") != null)
                this.Top = (int)key.GetValue("Top");
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
                if (cbANSIParse.Checked)
                {
                    tbOutput.AppendANSIText(data);
                } 
                else if (cbANSIRemove.Checked)
                {
                    tbOutput.RemoveANSIText(data);
                } 
                else
                {
                    tbOutput.AppendText(data);
                    //tbOutput.Text += data;
                }

                if (cbAutoScroll.Checked)
                {
                    tbOutput.SelectionStart = tbOutput.Text.Length;
                    tbOutput.ScrollToCaret();
                }
            });

            if (logging)
            {
                fs.Write(data);
            }
        }

        private void DataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //string data = CommPort.ReadExisting();
                string data = CommPort.ReadLine();
                UpdateTerm(data + CommPort.NewLine);
            } catch (Exception ex)
            {
                //tsStatus.Text = ex.Message.ToString();
            }
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
            CommPort.ReadTimeout = 200;
            CommPort.WriteTimeout = 200;

            CommPort.NewLine = "\r\n";
            //CommPort.ReceivedBytesThreshold = 80;
            //CommPort.ReadBufferSize = 4096;


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
            if (CommPort.IsOpen)
            {
                CommPort.Close();
            }

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
            key.SetValue("AutoScroll", cbAutoScroll.Checked);
            key.SetValue("Left", this.Left);
            key.SetValue("Top", this.Top);
            key.Close();
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void RemoveANSIText(this RichTextBox box, string text)
        {
            string pattern = @"\x1B(?:[@-Z\\-_]|\[[0-?]*[ -/]*[@-~])";
            string replacement = "";
            string result = Regex.Replace(text, pattern, replacement);
            box.AppendText(result);
        }

        public const char ANSI_SGR_RESET =  (char)0;
        public const char ANSI_SGR_BOLD =   (char)1;

        public const char ANSI_FG_BLACK =   (char)30;
        public const char ANSI_FG_RED =     (char)31;
        public const char ANSI_FG_GREEN =   (char)32;
        public const char ANSI_FG_YELLOW =  (char)33;
        public const char ANSI_FG_BLUE =    (char)34;
        public const char ANSI_FG_MAGENTA = (char)35;
        public const char ANSI_FG_CYAN =    (char)36;
        public const char ANSI_FG_WHITE =   (char)37;

        public static void AppendANSIText(this RichTextBox box, string text)
        {
            int Seq = -1;

            // Parse CSI (Control Sequence Introducer) beginning with ESC [
            string[] input = text.Split(new string[] { "\u001b[" }, StringSplitOptions.RemoveEmptyEntries);

            if (input.Count() == 1)
            {
                // No escape codes found. Send straight though to text box.
                box.AppendText(text);
            } 
            else
            {
                foreach (string substring in input)
                {
                    // We should now have a string prefixed with an ANSI code (minus the '\e[' )
                    // Parse out the ANSI Sequence Codes.
                    //Debug.WriteLine("Substring = [" + substring + "]");

                    int index = substring.IndexOf("m");
                    if (index != -1)
                    {
                        string CSI = substring.Substring(0, index);

                        char[] delimiterChars = { ';', 'm' };
                        string[] ansi_seq = CSI.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                        //Debug.WriteLine("ANSI CSI = [" + CSI + "], SEQ Count " + ansi_seq.Count());

                        for (int i = 0; i < (ansi_seq.Count()); i++)
                        {
                            string seq = ansi_seq[i];
                            //Debug.WriteLine("Seq" + i + " = [" + seq + "]");

                            Seq = -1;
                            bool valid = int.TryParse(seq, out Seq);
                            if (valid)
                            {
                                switch (Seq)
                                {
                                    case ANSI_SGR_BOLD:
                                        break;

                                    case ANSI_SGR_RESET:
                                        box.SelectionColor = box.ForeColor;
                                        break;

                                    case ANSI_FG_BLACK:
                                        box.SelectionColor = Color.Black;
                                        break;

                                    case ANSI_FG_RED:
                                        //box.SelectionColor = Color.Red;
                                        //box.SelectionColor = Color.FromArgb(197, 15, 31);
                                        box.SelectionColor = Color.FromArgb(187, 0, 0);
                                        break;

                                    case ANSI_FG_GREEN:
                                        box.SelectionColor = Color.Green;
                                        break;

                                    case ANSI_FG_YELLOW:
                                        //box.SelectionColor = Color.Yellow;
                                        box.SelectionColor = Color.FromArgb(187, 187, 0);
                                        break;

                                    case ANSI_FG_BLUE:
                                        //box.SelectionColor = Color.Blue;
                                        //box.SelectionColor = Color.FromArgb(0, 55, 218);
                                        box.SelectionColor = Color.FromArgb(0, 0, 187);
                                        break;

                                    case ANSI_FG_MAGENTA:
                                        box.SelectionColor = Color.Magenta;
                                        break;

                                    case ANSI_FG_CYAN:
                                        box.SelectionColor = Color.Cyan;
                                        break;

                                    case ANSI_FG_WHITE:
                                        box.SelectionColor = Color.White;
                                        break;

                                    default:
                                        box.AppendText("[Unknown Sequence Code " + Seq.ToString() + "]\r\n");
                                        break;
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Unknown Sequence (" + seq + ")");
                            }
                        }
                        // We are finished parsing CSI
                        box.SuspendLayout();
                        box.SelectionStart = box.TextLength;
                        box.SelectionLength = substring.Length - (index + 1);
                        box.AppendText(substring.Remove(0, index + 1));
                        box.ResumeLayout();
                    }
                    else
                    {
                        // Couldn't find a terminating 'm' in CSI; Probably not a CSI;
                        box.AppendText(substring);
                    }

                }
                // Finished all the substrings in the line
                //Debug.WriteLine("Complete\r\n");
            }
        }

        public static void AppendColourText(this RichTextBox box, string text, Color color)
        {
            box.SuspendLayout();
            box.SelectionStart = box.TextLength;
            box.SelectionLength = text.Length;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.ResumeLayout();
        }

        public static void HexDump(this RichTextBox box, string text)
        {
            char[] values = text.ToCharArray();

            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                box.AppendText($"{value:X} ");
            }
        }
    }
}
