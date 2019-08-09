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
         this.mnuETHAddress = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
         this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
         this.txtEthAddress = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.checkBox1 = new System.Windows.Forms.CheckBox();
         this.label2 = new System.Windows.Forms.Label();
         this.mnuTray.SuspendLayout();
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
            this.mnuETHAddress,
            this.toolStripMenuItem1,
            this.mnuExit});
         this.mnuTray.Name = "mnuTray";
         this.mnuTray.Size = new System.Drawing.Size(165, 76);
         // 
         // mnuRestore
         // 
         this.mnuRestore.Name = "mnuRestore";
         this.mnuRestore.Size = new System.Drawing.Size(164, 22);
         this.mnuRestore.Text = "Restore";
         this.mnuRestore.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
         // 
         // mnuETHAddress
         // 
         this.mnuETHAddress.Name = "mnuETHAddress";
         this.mnuETHAddress.Size = new System.Drawing.Size(164, 22);
         this.mnuETHAddress.Text = "Edit ETH Address";
         // 
         // toolStripMenuItem1
         // 
         this.toolStripMenuItem1.Name = "toolStripMenuItem1";
         this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
         // 
         // mnuExit
         // 
         this.mnuExit.Name = "mnuExit";
         this.mnuExit.Size = new System.Drawing.Size(164, 22);
         this.mnuExit.Text = "Exit";
         this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
         // 
         // txtEthAddress
         // 
         this.txtEthAddress.Location = new System.Drawing.Point(103, 36);
         this.txtEthAddress.Name = "txtEthAddress";
         this.txtEthAddress.Size = new System.Drawing.Size(321, 20);
         this.txtEthAddress.TabIndex = 1;
         this.txtEthAddress.TextChanged += new System.EventHandler(this.txtEthAddress_TextChanged);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(26, 39);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(70, 13);
         this.label1.TabIndex = 2;
         this.label1.Text = "ETH Address";
         // 
         // checkBox1
         // 
         this.checkBox1.AutoSize = true;
         this.checkBox1.Location = new System.Drawing.Point(103, 83);
         this.checkBox1.Name = "checkBox1";
         this.checkBox1.Size = new System.Drawing.Size(15, 14);
         this.checkBox1.TabIndex = 3;
         this.checkBox1.UseVisualStyleBackColor = true;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(26, 84);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(65, 13);
         this.label2.TabIndex = 4;
         this.label2.Text = "Close to tray";
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(501, 228);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.checkBox1);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.txtEthAddress);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "Form1";
         this.Text = "Stratum Proxy";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.mnuTray.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip mnuTray;
        private System.Windows.Forms.ToolStripMenuItem mnuRestore;
        private System.Windows.Forms.ToolStripMenuItem mnuETHAddress;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.TextBox txtEthAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
    }
}

