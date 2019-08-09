
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyFrontEnd {

   public partial class Form1 : Form {

      bool reallyClose = false;
      IniFile iniFile;

      public Form1() {

         InitializeComponent();

         string iniFilename = "config.ini";

         #if DEBUG
            string iniPath = Application.StartupPath + "\\..\\..\\..\\" + iniFilename;
         #else
            string iniPath = ".\\" + iniFilename;
         #endif

         iniFile = new IniFile(iniPath);
         this.txtEthAddress.Text = iniFile.Read("EthAddress", "Miner");

      }

      private void restoreToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Show();
         this.WindowState = FormWindowState.Normal;
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
         if (!reallyClose) {
            this.Hide();
            e.Cancel = true;
         }
      }

      private void mnuExit_Click(object sender, EventArgs e) {
         reallyClose = true;
         this.Close();
      }

      private void txtEthAddress_TextChanged(object sender, EventArgs e) {
         System.Diagnostics.Debug.WriteLine(txtEthAddress.Text);
      }
   }
}


