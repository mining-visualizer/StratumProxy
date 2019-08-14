
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;


namespace ProxyFrontEnd {

   public partial class Form1 : Form {

      const string NodeProxyExeName = "mvsp.exe";
      const string NodeProxyWindowTitle = "Stratum Proxy";

      bool reallyClose = false;
      IniFile iniFile;
      string ethAddress = "";
      string appPath;
      Process nodeProcess;


      public Form1() {

         InitializeComponent();

         lblAddressValid.Text = "";

      }


      private void Form1_Shown(object sender, EventArgs e) {

         #if DEBUG
            appPath = Application.StartupPath + "\\..\\..";
         #else
            appPath = Application.StartupPath + ".";
         #endif

         var iniPath = appPath + "\\config.ini";
         if (!File.Exists(iniPath)) {
            logMsg("Unable to open " + iniPath);
            logMsg( "Proxy disabled!");
            label1.Enabled = false;
            label2.Enabled = false;
            txtEthAddress.Enabled = false;
            chkCloseToTray.Enabled = false;
            return;
         }

         iniFile = new IniFile(appPath + "\\config.ini");
         
         string addr = iniFile.Read("EthAddress", "Miner");
         string firstRun = iniFile.Read("FirstRun", "General");
         if (firstRun == "1") {
            iniFile.DeleteKey("FirstRun", "General");
         }
         string closeToTray = iniFile.Read("CloseToTray", "General");
         chkCloseToTray.Checked = closeToTray == "1";

         //if (firstRun != "1" && closeToTray == "1" && addr.isEthAddress()) {
         //   Hide();
         //}

         if (!addr.isEthAddress()) {
            logMsg("Please enter a valid ETH address to begin");
         } else { 
            txtEthAddress.Text = addr;
            ethAddress = txtEthAddress.Text;
            Process p = isNodeProxyRunning(NodeProxyWindowTitle);
            if (p != null) {
               Utils.shutdownNodeProxy(p);
            }
            nodeProcess = startNodeProxy();
         }
      }

      private void restoreToolStripMenuItem_Click(object sender, EventArgs e) {
         Show();
         WindowState = FormWindowState.Normal;
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
         if (!reallyClose && chkCloseToTray.Checked) {
            Hide();
            e.Cancel = true;
         } else {
            if (nodeProcess != null) {
               Utils.shutdownNodeProxy(nodeProcess);
            }
         }
      }

      private void Form1_Resize(object sender, EventArgs e) {
         if (WindowState == FormWindowState.Minimized && chkCloseToTray.Checked) {
            Hide();
         }
      }

      private void mnuExit_Click(object sender, EventArgs e) {
         reallyClose = true;
         Close();
      }

      private void txtEthAddress_TextChanged(object sender, EventArgs e) {

         lblAddressValid.Text = "";
         timerAddress.Tag = "TextChanged";
         timerAddress.Interval = 1100;
         timerAddress.Start();
      }

      private void timerAddress_Tick(object sender, EventArgs e) {

         timerAddress.Stop();

         switch (timerAddress.Tag) {
            case "TextChanged":
               if (!txtEthAddress.Text.isEthAddress()) {
                  lblAddressValid.Text = "Invalid!";
               } else {
                  lblAddressValid.Text = "";
                  if (!txtEthAddress.Text.equivalentAddress(ethAddress)) {
                     iniFile.Write("EthAddress", txtEthAddress.Text, "Miner");
                     ethAddress = txtEthAddress.Text;
                     timerAddress.Tag = "NewAddress";
                     timerAddress.Interval = 4000;
                     timerAddress.Start();
                     if (nodeProcess != null) {
                        Utils.shutdownNodeProxy(nodeProcess);
                        lblAddressValid.Text = "Saved to disk. Restarting proxy...";
                     } else {
                        lblAddressValid.Text = "Saved to disk. Starting proxy...";
                     }
                     nodeProcess = startNodeProxy();
                  }
               }
               break;

            case "NewAddress":
               lblAddressValid.Text = "";
               break;

            default:
               break;
         }

      }

      private Process isNodeProxyRunning(string filename) {

         foreach (Process proc in Process.GetProcesses()) {
            if (proc.MainWindowTitle.EndsWith(filename)) {
               return proc;
            }
         }
         return null;

      }


      private Process startNodeProxy() {

         string retMessage = String.Empty;
         ProcessStartInfo startInfo = new ProcessStartInfo();
         Process p = new Process();

         startInfo.CreateNoWindow = true;
         startInfo.RedirectStandardOutput = true;
         startInfo.RedirectStandardInput = true;
         startInfo.RedirectStandardError = true;

         startInfo.UseShellExecute = false;
         startInfo.FileName = appPath + "\\" + NodeProxyExeName;

         p.StartInfo = startInfo;

         DataReceivedEventHandler ev = new DataReceivedEventHandler(LogNodeOutput);
         p.ErrorDataReceived += ev;
         p.OutputDataReceived += ev;

         p.Start();
         p.BeginOutputReadLine();
         p.BeginErrorReadLine();

         return p;

      }

      private void LogNodeOutput(object sendingProcess, DataReceivedEventArgs outLine) {
         if (!String.IsNullOrEmpty(outLine.Data)) {
            logMsg(outLine.Data);
         }
      }

      delegate void logMsgCallback(string text);

      public void logMsg(string msg) {

         const string TotalShares = "Total shares:";

         if (txtLog.InvokeRequired) {
            logMsgCallback d = new logMsgCallback(logMsg);
            Invoke(d, new object[] { msg });

         } else {
            // since we always append a NewLine, we subtract 2
            string lastLine = txtLog.Lines.Length < 2 ? "" :  txtLog.Lines[txtLog.Lines.Length - 2];
            if (msg.StartsWith(TotalShares) && lastLine.StartsWith(TotalShares)) {
               // replace last line
               ReplaceLine(txtLog, txtLog.Lines.Length - 2, msg);
            } else {
               txtLog.AppendText(msg + Environment.NewLine);
            }
         }
      }

      private void chkCloseToTray_CheckedChanged(object sender, EventArgs e) {
         iniFile.Write("CloseToTray", chkCloseToTray.Checked ? "1" : "0", "General");
      }

      private void ReplaceLine(TextBox box, int lineNumber, string text) {

         int first;
         if (lineNumber < 0 || (first = box.GetFirstCharIndexFromLine(lineNumber)) < 0) {
            return;
         }
         int last = box.GetFirstCharIndexFromLine(lineNumber + 1);

         box.Select(first, last < 0 ? int.MaxValue : last - first - Environment.NewLine.Length);
         box.SelectedText = text;

      }

      private void btnCopy_Click(object sender, EventArgs e) {
         Clipboard.Clear();
         Clipboard.SetText(txtLog.Text);
      }
   }

}


