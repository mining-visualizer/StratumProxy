
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

namespace ProxyFrontEnd {

   public partial class Form1 : Form {

      const string NodeProxyExeName = "mvstprox.exe";
      const string NodeProxyWindowTitle = "Stratum Proxy";

      bool reallyClose = false;
      IniFile iniFile;
      string ethAddress;
      string appPath;
      Process nodeProcess;


      public Form1() {

         InitializeComponent();

         lblAddressValid.Text = "";

         #if DEBUG
            appPath = Application.StartupPath + "\\..\\..";
         #else
            appPath = ".";
         #endif

         iniFile = new IniFile(appPath + "\\config.ini");
         string addr = iniFile.Read("EthAddress", "Miner");
         if (addr.isEthAddress()) {
            txtEthAddress.Text = addr;
            ethAddress = txtEthAddress.Text;
            Process p = isNodeProxyRunning(NodeProxyWindowTitle);
            if (p != null) {
               //logMsg("orphaned " + NodeProxyExeName + " found ... terminating");
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
            Utils.shutdownNodeProxy(nodeProcess);
         }
      }

      private void mnuExit_Click(object sender, EventArgs e) {
         reallyClose = true;
         Close();
      }

      private void txtEthAddress_TextChanged(object sender, EventArgs e) {

         lblAddressValid.Text = "";
         timerAddress.Start();

         //System.Diagnostics.Debug.WriteLine(txtEthAddress.Text);

      }

      private void timerAddress_Tick(object sender, EventArgs e) {
         timerAddress.Stop();
         if (!txtEthAddress.Text.isEthAddress()) {
            lblAddressValid.Text = "Invalid!";
         } else {
            lblAddressValid.Text = "";
            if (!txtEthAddress.Text.equivalentAddress(ethAddress)) {
               lblAddressValid.Text = "Saved!";
               iniFile.Write("EthAddress", txtEthAddress.Text, "Miner");
               ethAddress = txtEthAddress.Text;
            }
         }
      }

      private Process isNodeProxyRunning(string filename) {

         foreach (Process proc in Process.GetProcesses()) {
            //logMsg(proc.MainWindowTitle + " - " + proc.ProcessName);
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
         if (txtLog.InvokeRequired) {
            logMsgCallback d = new logMsgCallback(logMsg);
            Invoke(d, new object[] { msg });
         } else {
            txtLog.AppendText(msg + Environment.NewLine);
         }
      }

      private void button1_Click(object sender, EventArgs e) {
         throw new System.Exception();
      }
   }

}


