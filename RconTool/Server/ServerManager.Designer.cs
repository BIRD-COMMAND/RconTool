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
			this.SuspendLayout();
			// 
			// listBoxServerList
			// 
			this.listBoxServerList.FormattingEnabled = true;
			this.listBoxServerList.Location = new System.Drawing.Point(6, 7);
			this.listBoxServerList.Name = "listBoxServerList";
			this.listBoxServerList.Size = new System.Drawing.Size(272, 121);
			this.listBoxServerList.TabIndex = 0;
			// 
			// buttonEditServer
			// 
			this.buttonEditServer.Location = new System.Drawing.Point(194, 134);
			this.buttonEditServer.Name = "buttonEditServer";
			this.buttonEditServer.Size = new System.Drawing.Size(84, 23);
			this.buttonEditServer.TabIndex = 1;
			this.buttonEditServer.Text = "Edit Server";
			this.buttonEditServer.UseVisualStyleBackColor = true;
			this.buttonEditServer.Click += new System.EventHandler(this.ButtonEditServer_Click);
			// 
			// buttonDeleteServer
			// 
			this.buttonDeleteServer.Location = new System.Drawing.Point(6, 134);
			this.buttonDeleteServer.Name = "buttonDeleteServer";
			this.buttonDeleteServer.Size = new System.Drawing.Size(97, 23);
			this.buttonDeleteServer.TabIndex = 2;
			this.buttonDeleteServer.Text = "Delete Server";
			this.buttonDeleteServer.UseVisualStyleBackColor = true;
			this.buttonDeleteServer.Click += new System.EventHandler(this.ButtonDeleteServer_Click);
			// 
			// buttonAddNewServer
			// 
			this.buttonAddNewServer.Location = new System.Drawing.Point(6, 163);
			this.buttonAddNewServer.MaximumSize = new System.Drawing.Size(272, 23);
			this.buttonAddNewServer.MinimumSize = new System.Drawing.Size(272, 23);
			this.buttonAddNewServer.Name = "buttonAddNewServer";
			this.buttonAddNewServer.Size = new System.Drawing.Size(272, 23);
			this.buttonAddNewServer.TabIndex = 3;
			this.buttonAddNewServer.Text = "Add New Server";
			this.buttonAddNewServer.UseVisualStyleBackColor = true;
			this.buttonAddNewServer.Click += new System.EventHandler(this.ButtonAddNewServer_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(206, 269);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// comboBoxWindowTitle
			// 
			this.comboBoxWindowTitle.FormattingEnabled = true;
			this.comboBoxWindowTitle.Items.AddRange(new object[] {
            "Server IP",
            "Server Name",
            "Server Game",
            "None"});
			this.comboBoxWindowTitle.Location = new System.Drawing.Point(84, 269);
			this.comboBoxWindowTitle.Name = "comboBoxWindowTitle";
			this.comboBoxWindowTitle.Size = new System.Drawing.Size(114, 21);
			this.comboBoxWindowTitle.TabIndex = 5;
			// 
			// textBoxWebHookURL
			// 
			this.textBoxWebHookURL.Location = new System.Drawing.Point(90, 192);
			this.textBoxWebHookURL.Name = "textBoxWebHookURL";
			this.textBoxWebHookURL.Size = new System.Drawing.Size(188, 20);
			this.textBoxWebHookURL.TabIndex = 6;
			// 
			// labelWebHookURL
			// 
			this.labelWebHookURL.AutoSize = true;
			this.labelWebHookURL.Location = new System.Drawing.Point(6, 195);
			this.labelWebHookURL.Name = "labelWebHookURL";
			this.labelWebHookURL.Size = new System.Drawing.Size(81, 13);
			this.labelWebHookURL.TabIndex = 7;
			this.labelWebHookURL.Text = "WebHookURL:";
			// 
			// labelReportCommand
			// 
			this.labelReportCommand.AutoSize = true;
			this.labelReportCommand.Location = new System.Drawing.Point(6, 221);
			this.labelReportCommand.Name = "labelReportCommand";
			this.labelReportCommand.Size = new System.Drawing.Size(92, 13);
			this.labelReportCommand.TabIndex = 9;
			this.labelReportCommand.Text = "Report Command:";
			// 
			// textBoxReportCommand
			// 
			this.textBoxReportCommand.Location = new System.Drawing.Point(104, 218);
			this.textBoxReportCommand.Name = "textBoxReportCommand";
			this.textBoxReportCommand.Size = new System.Drawing.Size(174, 20);
			this.textBoxReportCommand.TabIndex = 8;
			// 
			// labelWindowTitle
			// 
			this.labelWindowTitle.AutoSize = true;
			this.labelWindowTitle.Location = new System.Drawing.Point(6, 272);
			this.labelWindowTitle.Name = "labelWindowTitle";
			this.labelWindowTitle.Size = new System.Drawing.Size(72, 13);
			this.labelWindowTitle.TabIndex = 10;
			this.labelWindowTitle.Text = "Window Title:";
			// 
			// labelRoleID
			// 
			this.labelRoleID.AutoSize = true;
			this.labelRoleID.Location = new System.Drawing.Point(6, 247);
			this.labelRoleID.Name = "labelRoleID";
			this.labelRoleID.Size = new System.Drawing.Size(46, 13);
			this.labelRoleID.TabIndex = 12;
			this.labelRoleID.Text = "Role ID:";
			// 
			// textBoxRoleID
			// 
			this.textBoxRoleID.Location = new System.Drawing.Point(58, 244);
			this.textBoxRoleID.Name = "textBoxRoleID";
			this.textBoxRoleID.Size = new System.Drawing.Size(220, 20);
			this.textBoxRoleID.TabIndex = 11;
			// 
			// ServerManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 303);
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
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}