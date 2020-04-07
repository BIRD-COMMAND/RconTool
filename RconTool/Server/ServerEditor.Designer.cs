namespace RconTool
{
    partial class ServerEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerEditor));
			this.textBoxServerIP = new System.Windows.Forms.TextBox();
			this.labelServerIP = new System.Windows.Forms.Label();
			this.labelServerPort = new System.Windows.Forms.Label();
			this.textBoxServerPort = new System.Windows.Forms.TextBox();
			this.labelRconPassword = new System.Windows.Forms.Label();
			this.textBoxRconPassword = new System.Windows.Forms.TextBox();
			this.labelRconPort = new System.Windows.Forms.Label();
			this.textBoxRconPort = new System.Windows.Forms.TextBox();
			this.buttonSaveServer = new System.Windows.Forms.Button();
			this.labelCommandsToSendUponConnection = new System.Windows.Forms.Label();
			this.textBoxCommandsToSendUponConnection = new System.Windows.Forms.TextBox();
			this.panelDetailEntry = new System.Windows.Forms.Panel();
			this.labelName = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelServerPassword = new System.Windows.Forms.Label();
			this.textBoxServerPassword = new System.Windows.Forms.TextBox();
			this.panelCommandsAndSaveButton = new System.Windows.Forms.Panel();
			this.panelDetailEntry.SuspendLayout();
			this.panelCommandsAndSaveButton.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxServerIP
			// 
			this.textBoxServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServerIP.Location = new System.Drawing.Point(93, 10);
			this.textBoxServerIP.Name = "textBoxServerIP";
			this.textBoxServerIP.Size = new System.Drawing.Size(169, 20);
			this.textBoxServerIP.TabIndex = 0;
			// 
			// labelServerIP
			// 
			this.labelServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelServerIP.AutoSize = true;
			this.labelServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelServerIP.Location = new System.Drawing.Point(33, 13);
			this.labelServerIP.Name = "labelServerIP";
			this.labelServerIP.Size = new System.Drawing.Size(54, 13);
			this.labelServerIP.TabIndex = 0;
			this.labelServerIP.Text = "Server IP:";
			// 
			// labelServerPort
			// 
			this.labelServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelServerPort.AutoSize = true;
			this.labelServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelServerPort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelServerPort.Location = new System.Drawing.Point(24, 39);
			this.labelServerPort.Name = "labelServerPort";
			this.labelServerPort.Size = new System.Drawing.Size(63, 13);
			this.labelServerPort.TabIndex = 0;
			this.labelServerPort.Text = "Server Port:";
			// 
			// textBoxServerPort
			// 
			this.textBoxServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServerPort.Location = new System.Drawing.Point(93, 36);
			this.textBoxServerPort.Name = "textBoxServerPort";
			this.textBoxServerPort.Size = new System.Drawing.Size(169, 20);
			this.textBoxServerPort.TabIndex = 1;
			// 
			// labelRconPassword
			// 
			this.labelRconPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRconPassword.AutoSize = true;
			this.labelRconPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelRconPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelRconPassword.Location = new System.Drawing.Point(2, 117);
			this.labelRconPassword.Name = "labelRconPassword";
			this.labelRconPassword.Size = new System.Drawing.Size(85, 13);
			this.labelRconPassword.TabIndex = 0;
			this.labelRconPassword.Text = "Rcon Password:";
			// 
			// textBoxRconPassword
			// 
			this.textBoxRconPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRconPassword.Location = new System.Drawing.Point(93, 114);
			this.textBoxRconPassword.Name = "textBoxRconPassword";
			this.textBoxRconPassword.Size = new System.Drawing.Size(169, 20);
			this.textBoxRconPassword.TabIndex = 4;
			this.textBoxRconPassword.UseSystemPasswordChar = true;
			// 
			// labelRconPort
			// 
			this.labelRconPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRconPort.AutoSize = true;
			this.labelRconPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelRconPort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelRconPort.Location = new System.Drawing.Point(29, 91);
			this.labelRconPort.Name = "labelRconPort";
			this.labelRconPort.Size = new System.Drawing.Size(58, 13);
			this.labelRconPort.TabIndex = 0;
			this.labelRconPort.Text = "Rcon Port:";
			// 
			// textBoxRconPort
			// 
			this.textBoxRconPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRconPort.Location = new System.Drawing.Point(93, 88);
			this.textBoxRconPort.Name = "textBoxRconPort";
			this.textBoxRconPort.Size = new System.Drawing.Size(169, 20);
			this.textBoxRconPort.TabIndex = 3;
			// 
			// buttonSaveServer
			// 
			this.buttonSaveServer.Location = new System.Drawing.Point(182, 144);
			this.buttonSaveServer.Name = "buttonSaveServer";
			this.buttonSaveServer.Size = new System.Drawing.Size(75, 22);
			this.buttonSaveServer.TabIndex = 7;
			this.buttonSaveServer.Text = "Save Server";
			this.buttonSaveServer.UseVisualStyleBackColor = true;
			this.buttonSaveServer.Click += new System.EventHandler(this.buttonSaveServer_Click);
			// 
			// labelCommandsToSendUponConnection
			// 
			this.labelCommandsToSendUponConnection.AutoSize = true;
			this.labelCommandsToSendUponConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelCommandsToSendUponConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelCommandsToSendUponConnection.Location = new System.Drawing.Point(3, 9);
			this.labelCommandsToSendUponConnection.Name = "labelCommandsToSendUponConnection";
			this.labelCommandsToSendUponConnection.Size = new System.Drawing.Size(183, 13);
			this.labelCommandsToSendUponConnection.TabIndex = 0;
			this.labelCommandsToSendUponConnection.Text = "Commands to send upon connection:";
			// 
			// textBoxCommandsToSendUponConnection
			// 
			this.textBoxCommandsToSendUponConnection.Location = new System.Drawing.Point(6, 25);
			this.textBoxCommandsToSendUponConnection.Multiline = true;
			this.textBoxCommandsToSendUponConnection.Name = "textBoxCommandsToSendUponConnection";
			this.textBoxCommandsToSendUponConnection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxCommandsToSendUponConnection.Size = new System.Drawing.Size(256, 113);
			this.textBoxCommandsToSendUponConnection.TabIndex = 6;
			// 
			// panelDetailEntry
			// 
			this.panelDetailEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panelDetailEntry.Controls.Add(this.labelName);
			this.panelDetailEntry.Controls.Add(this.textBoxName);
			this.panelDetailEntry.Controls.Add(this.labelRconPort);
			this.panelDetailEntry.Controls.Add(this.textBoxRconPort);
			this.panelDetailEntry.Controls.Add(this.labelServerPassword);
			this.panelDetailEntry.Controls.Add(this.labelRconPassword);
			this.panelDetailEntry.Controls.Add(this.textBoxServerPassword);
			this.panelDetailEntry.Controls.Add(this.textBoxRconPassword);
			this.panelDetailEntry.Controls.Add(this.labelServerPort);
			this.panelDetailEntry.Controls.Add(this.textBoxServerPort);
			this.panelDetailEntry.Controls.Add(this.labelServerIP);
			this.panelDetailEntry.Controls.Add(this.textBoxServerIP);
			this.panelDetailEntry.Location = new System.Drawing.Point(2, 1);
			this.panelDetailEntry.Margin = new System.Windows.Forms.Padding(0);
			this.panelDetailEntry.Name = "panelDetailEntry";
			this.panelDetailEntry.Size = new System.Drawing.Size(262, 162);
			this.panelDetailEntry.TabIndex = 17;
			// 
			// labelName
			// 
			this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelName.AutoSize = true;
			this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelName.Location = new System.Drawing.Point(1, 143);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(86, 13);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "Name (Optional):";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(93, 140);
			this.textBoxName.MaxLength = 64;
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(169, 20);
			this.textBoxName.TabIndex = 5;
			// 
			// labelServerPassword
			// 
			this.labelServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelServerPassword.AutoSize = true;
			this.labelServerPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelServerPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelServerPassword.Location = new System.Drawing.Point(-3, 65);
			this.labelServerPassword.Name = "labelServerPassword";
			this.labelServerPassword.Size = new System.Drawing.Size(90, 13);
			this.labelServerPassword.TabIndex = 0;
			this.labelServerPassword.Text = "Server Password:";
			// 
			// textBoxServerPassword
			// 
			this.textBoxServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServerPassword.Location = new System.Drawing.Point(93, 62);
			this.textBoxServerPassword.Name = "textBoxServerPassword";
			this.textBoxServerPassword.Size = new System.Drawing.Size(169, 20);
			this.textBoxServerPassword.TabIndex = 2;
			this.textBoxServerPassword.UseSystemPasswordChar = true;
			// 
			// panelCommandsAndSaveButton
			// 
			this.panelCommandsAndSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panelCommandsAndSaveButton.Controls.Add(this.labelCommandsToSendUponConnection);
			this.panelCommandsAndSaveButton.Controls.Add(this.textBoxCommandsToSendUponConnection);
			this.panelCommandsAndSaveButton.Controls.Add(this.buttonSaveServer);
			this.panelCommandsAndSaveButton.Location = new System.Drawing.Point(0, 166);
			this.panelCommandsAndSaveButton.Name = "panelCommandsAndSaveButton";
			this.panelCommandsAndSaveButton.Size = new System.Drawing.Size(264, 170);
			this.panelCommandsAndSaveButton.TabIndex = 18;
			// 
			// ServerEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(268, 338);
			this.Controls.Add(this.panelCommandsAndSaveButton);
			this.Controls.Add(this.panelDetailEntry);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServerEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Server Editor";
			this.panelDetailEntry.ResumeLayout(false);
			this.panelDetailEntry.PerformLayout();
			this.panelCommandsAndSaveButton.ResumeLayout(false);
			this.panelCommandsAndSaveButton.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.Label labelServerPort;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.Label labelRconPassword;
        private System.Windows.Forms.TextBox textBoxRconPassword;
        private System.Windows.Forms.Label labelRconPort;
        private System.Windows.Forms.TextBox textBoxRconPort;
        private System.Windows.Forms.Button buttonSaveServer;
        private System.Windows.Forms.Label labelCommandsToSendUponConnection;
        private System.Windows.Forms.TextBox textBoxCommandsToSendUponConnection;
		private System.Windows.Forms.Panel panelDetailEntry;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Panel panelCommandsAndSaveButton;
		private System.Windows.Forms.Label labelServerPassword;
		private System.Windows.Forms.TextBox textBoxServerPassword;
	}
}