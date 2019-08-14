namespace ProxyFrontEnd {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
         this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
         this.mnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.mnuRestore = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
         this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
         this.txtEthAddress = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.chkCloseToTray = new System.Windows.Forms.CheckBox();
         this.label2 = new System.Windows.Forms.Label();
         this.timerAddress = new System.Windows.Forms.Timer(this.components);
         this.lblAddressValid = new System.Windows.Forms.Label();
         this.txtLog = new System.Windows.Forms.TextBox();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.mnuExit1 = new System.Windows.Forms.ToolStripMenuItem();
         this.label3 = new System.Windows.Forms.Label();
         this.btnCopy = new System.Windows.Forms.Button();
         this.mnuTray.SuspendLayout();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // notifyIcon1
         // 
         this.notifyIcon1.ContextMenuStrip = this.mnuTray;
         this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
         this.notifyIcon1.Text = "Stratum Proxy";
         this.notifyIcon1.Visible = true;
         // 
         // mnuTray
         // 
         this.mnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRestore,
            this.toolStripMenuItem1,
            this.mnuExit});
         this.mnuTray.Name = "mnuTray";
         this.mnuTray.Size = new System.Drawing.Size(114, 54);
         // 
         // mnuRestore
         // 
         this.mnuRestore.Name = "mnuRestore";
         this.mnuRestore.Size = new System.Drawing.Size(113, 22);
         this.mnuRestore.Text = "Restore";
         this.mnuRestore.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
         // 
         // toolStripMenuItem1
         // 
         this.toolStripMenuItem1.Name = "toolStripMenuItem1";
         this.toolStripMenuItem1.Size = new System.Drawing.Size(110, 6);
         // 
         // mnuExit
         // 
         this.mnuExit.Name = "mnuExit";
         this.mnuExit.Size = new System.Drawing.Size(113, 22);
         this.mnuExit.Text = "Exit";
         this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
         // 
         // txtEthAddress
         // 
         this.txtEthAddress.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.txtEthAddress.Location = new System.Drawing.Point(106, 52);
         this.txtEthAddress.Name = "txtEthAddress";
         this.txtEthAddress.Size = new System.Drawing.Size(364, 22);
         this.txtEthAddress.TabIndex = 1;
         this.txtEthAddress.TextChanged += new System.EventHandler(this.txtEthAddress_TextChanged);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(29, 55);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(70, 13);
         this.label1.TabIndex = 2;
         this.label1.Text = "ETH Address";
         // 
         // chkCloseToTray
         // 
         this.chkCloseToTray.AutoSize = true;
         this.chkCloseToTray.Location = new System.Drawing.Point(106, 96);
         this.chkCloseToTray.Name = "chkCloseToTray";
         this.chkCloseToTray.Size = new System.Drawing.Size(15, 14);
         this.chkCloseToTray.TabIndex = 3;
         this.chkCloseToTray.UseVisualStyleBackColor = true;
         this.chkCloseToTray.CheckedChanged += new System.EventHandler(this.chkCloseToTray_CheckedChanged);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(29, 96);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(65, 13);
         this.label2.TabIndex = 4;
         this.label2.Text = "Close to tray";
         // 
         // timerAddress
         // 
         this.timerAddress.Interval = 1100;
         this.timerAddress.Tick += new System.EventHandler(this.timerAddress_Tick);
         // 
         // lblAddressValid
         // 
         this.lblAddressValid.Location = new System.Drawing.Point(480, 56);
         this.lblAddressValid.Name = "lblAddressValid";
         this.lblAddressValid.Size = new System.Drawing.Size(228, 13);
         this.lblAddressValid.TabIndex = 5;
         this.lblAddressValid.Text = "Address valid";
         // 
         // txtLog
         // 
         this.txtLog.Location = new System.Drawing.Point(32, 158);
         this.txtLog.Multiline = true;
         this.txtLog.Name = "txtLog";
         this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.txtLog.Size = new System.Drawing.Size(658, 168);
         this.txtLog.TabIndex = 6;
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(720, 24);
         this.menuStrip1.TabIndex = 10;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit1});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // mnuExit1
         // 
         this.mnuExit1.Name = "mnuExit1";
         this.mnuExit1.Size = new System.Drawing.Size(92, 22);
         this.mnuExit1.Text = "Exit";
         this.mnuExit1.Click += new System.EventHandler(this.mnuExit_Click);
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(29, 142);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(80, 13);
         this.label3.TabIndex = 11;
         this.label3.Text = "Console Output";
         // 
         // btnCopy
         // 
         this.btnCopy.Location = new System.Drawing.Point(615, 129);
         this.btnCopy.Name = "btnCopy";
         this.btnCopy.Size = new System.Drawing.Size(75, 23);
         this.btnCopy.TabIndex = 12;
         this.btnCopy.Text = "Copy";
         this.btnCopy.UseVisualStyleBackColor = true;
         this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(720, 354);
         this.Controls.Add(this.btnCopy);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.menuStrip1);
         this.Controls.Add(this.txtLog);
         this.Controls.Add(this.lblAddressValid);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.chkCloseToTray);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.txtEthAddress);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "Form1";
         this.Text = "Stratum Proxy";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.Shown += new System.EventHandler(this.Form1_Shown);
         this.Resize += new System.EventHandler(this.Form1_Resize);
         this.mnuTray.ResumeLayout(false);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip mnuTray;
        private System.Windows.Forms.ToolStripMenuItem mnuRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.TextBox txtEthAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCloseToTray;
        private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Timer timerAddress;
      private System.Windows.Forms.Label lblAddressValid;
      private System.Windows.Forms.TextBox txtLog;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mnuExit1;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Button btnCopy;
   }
}

