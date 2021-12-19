namespace RconTool
{
    partial class ServerManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerManager));
			this.listBoxServerList = new System.Windows.Forms.ListBox();
			this.buttonEditServer = new System.Windows.Forms.Button();
			this.buttonDeleteServer = new System.Windows.Forms.Button();
			this.buttonAddNewServer = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.comboBoxWindowTitle = new System.Windows.Forms.ComboBox();
			this.textBoxWebHookURL = new System.Windows.Forms.TextBox();
			this.labelWebHookURL = new System.Windows.Forms.Label();
			this.labelReportCommand = new System.Windows.Forms.Label();
			this.textBoxReportCommand = new System.Windows.Forms.TextBox();
			this.labelWindowTitle = new System.Windows.Forms.Label();
			this.labelRoleID = new System.Windows.Forms.Label();
			this.textBoxRoleID = new System.Windows.Forms.TextBox();
			this.labelDiscordIntegration = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listBoxServerList
			// 
			this.listBoxServerList.DisplayMember = "ServerDisplayName";
			this.listBoxServerList.FormattingEnabled = true;
			this.listBoxServerList.ItemHeight = 17;
			this.listBoxServerList.Location = new System.Drawing.Point(7, 9);
			this.listBoxServerList.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.listBoxServerList.Name = "listBoxServerList";
			this.listBoxServerList.Size = new System.Drawing.Size(317, 157);
			this.listBoxServerList.TabIndex = 0;
			this.listBoxServerList.SelectedIndexChanged += new System.EventHandler(this.listBoxServerList_SelectedIndexChanged);
			// 
			// buttonEditServer
			// 
			this.buttonEditServer.Location = new System.Drawing.Point(115, 175);
			this.buttonEditServer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonEditServer.Name = "buttonEditServer";
			this.buttonEditServer.Size = new System.Drawing.Size(100, 29);
			this.buttonEditServer.TabIndex = 1;
			this.buttonEditServer.Text = "Edit Server";
			this.buttonEditServer.UseVisualStyleBackColor = true;
			this.buttonEditServer.Click += new System.EventHandler(this.ButtonEditServer_Click);
			// 
			// buttonDeleteServer
			// 
			this.buttonDeleteServer.Location = new System.Drawing.Point(224, 175);
			this.buttonDeleteServer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonDeleteServer.Name = "buttonDeleteServer";
			this.buttonDeleteServer.Size = new System.Drawing.Size(100, 29);
			this.buttonDeleteServer.TabIndex = 2;
			this.buttonDeleteServer.Text = "Delete Server";
			this.buttonDeleteServer.UseVisualStyleBackColor = true;
			this.buttonDeleteServer.Click += new System.EventHandler(this.ButtonDeleteServer_Click);
			// 
			// buttonAddNewServer
			// 
			this.buttonAddNewServer.Location = new System.Drawing.Point(7, 175);
			this.buttonAddNewServer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonAddNewServer.MaximumSize = new System.Drawing.Size(317, 29);
			this.buttonAddNewServer.Name = "buttonAddNewServer";
			this.buttonAddNewServer.Size = new System.Drawing.Size(100, 29);
			this.buttonAddNewServer.TabIndex = 3;
			this.buttonAddNewServer.Text = "Add Server";
			this.buttonAddNewServer.UseVisualStyleBackColor = true;
			this.buttonAddNewServer.Click += new System.EventHandler(this.ButtonAddNewServer_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonSave.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.buttonSave.Location = new System.Drawing.Point(277, 362);
			this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(47, 27);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// comboBoxWindowTitle
			// 
			this.comboBoxWindowTitle.FormattingEnabled = true;
			this.comboBoxWindowTitle.Items.AddRange(new object[] {
            "None",
            "Server Name",
            "Server Game",
            "Server IP"});
			this.comboBoxWindowTitle.Location = new System.Drawing.Point(130, 363);
			this.comboBoxWindowTitle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.comboBoxWindowTitle.Name = "comboBoxWindowTitle";
			this.comboBoxWindowTitle.Size = new System.Drawing.Size(141, 25);
			this.comboBoxWindowTitle.TabIndex = 5;
			// 
			// textBoxWebHookURL
			// 
			this.textBoxWebHookURL.Location = new System.Drawing.Point(130, 280);
			this.textBoxWebHookURL.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.textBoxWebHookURL.Name = "textBoxWebHookURL";
			this.textBoxWebHookURL.Size = new System.Drawing.Size(193, 25);
			this.textBoxWebHookURL.TabIndex = 6;
			// 
			// labelWebHookURL
			// 
			this.labelWebHookURL.AutoSize = true;
			this.labelWebHookURL.Location = new System.Drawing.Point(23, 283);
			this.labelWebHookURL.Name = "labelWebHookURL";
			this.labelWebHookURL.Size = new System.Drawing.Size(101, 19);
			this.labelWebHookURL.TabIndex = 7;
			this.labelWebHookURL.Text = "WebHook URL:";
			// 
			// labelReportCommand
			// 
			this.labelReportCommand.AutoSize = true;
			this.labelReportCommand.Location = new System.Drawing.Point(3, 317);
			this.labelReportCommand.Name = "labelReportCommand";
			this.labelReportCommand.Size = new System.Drawing.Size(121, 19);
			this.labelReportCommand.TabIndex = 9;
			this.labelReportCommand.Text = "Report Command:";
			// 
			// textBoxReportCommand
			// 
			this.textBoxReportCommand.Location = new System.Drawing.Point(130, 314);
			this.textBoxReportCommand.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.textBoxReportCommand.Name = "textBoxReportCommand";
			this.textBoxReportCommand.Size = new System.Drawing.Size(193, 25);
			this.textBoxReportCommand.TabIndex = 8;
			// 
			// labelWindowTitle
			// 
			this.labelWindowTitle.AutoSize = true;
			this.labelWindowTitle.Location = new System.Drawing.Point(33, 366);
			this.labelWindowTitle.Name = "labelWindowTitle";
			this.labelWindowTitle.Size = new System.Drawing.Size(91, 19);
			this.labelWindowTitle.TabIndex = 10;
			this.labelWindowTitle.Text = "Window Title:";
			// 
			// labelRoleID
			// 
			this.labelRoleID.AutoSize = true;
			this.labelRoleID.Location = new System.Drawing.Point(68, 250);
			this.labelRoleID.Name = "labelRoleID";
			this.labelRoleID.Size = new System.Drawing.Size(56, 19);
			this.labelRoleID.TabIndex = 12;
			this.labelRoleID.Text = "Role ID:";
			// 
			// textBoxRoleID
			// 
			this.textBoxRoleID.Location = new System.Drawing.Point(130, 247);
			this.textBoxRoleID.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.textBoxRoleID.Name = "textBoxRoleID";
			this.textBoxRoleID.Size = new System.Drawing.Size(193, 25);
			this.textBoxRoleID.TabIndex = 11;
			// 
			// labelDiscordIntegration
			// 
			this.labelDiscordIntegration.AutoSize = true;
			this.labelDiscordIntegration.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.labelDiscordIntegration.Location = new System.Drawing.Point(186, 216);
			this.labelDiscordIntegration.Name = "labelDiscordIntegration";
			this.labelDiscordIntegration.Size = new System.Drawing.Size(143, 21);
			this.labelDiscordIntegration.TabIndex = 13;
			this.labelDiscordIntegration.Text = "Discord Integration";
			// 
			// ServerManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(331, 402);
			this.Controls.Add(this.labelDiscordIntegration);
			this.Controls.Add(this.listBoxServerList);
			this.Controls.Add(this.buttonDeleteServer);
			this.Controls.Add(this.buttonEditServer);
			this.Controls.Add(this.buttonAddNewServer);
			this.Controls.Add(this.labelWebHookURL);
			this.Controls.Add(this.textBoxWebHookURL);
			this.Controls.Add(this.labelReportCommand);
			this.Controls.Add(this.textBoxReportCommand);
			this.Controls.Add(this.labelRoleID);
			this.Controls.Add(this.textBoxRoleID);
			this.Controls.Add(this.labelWindowTitle);
			this.Controls.Add(this.comboBoxWindowTitle);
			this.Controls.Add(this.buttonSave);
			this.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServerManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Server Manager";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxServerList;
        private System.Windows.Forms.Button buttonEditServer;
        private System.Windows.Forms.Button buttonDeleteServer;
        private System.Windows.Forms.Button buttonAddNewServer;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxWindowTitle;
        private System.Windows.Forms.TextBox textBoxWebHookURL;
        private System.Windows.Forms.Label labelWebHookURL;
        private System.Windows.Forms.Label labelReportCommand;
        private System.Windows.Forms.TextBox textBoxReportCommand;
        private System.Windows.Forms.Label labelWindowTitle;
        private System.Windows.Forms.Label labelRoleID;
        private System.Windows.Forms.TextBox textBoxRoleID;
		private System.Windows.Forms.Label labelDiscordIntegration;
	}
}