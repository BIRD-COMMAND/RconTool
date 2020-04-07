namespace RconTool
{
	partial class ServerSettingsEditor
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerSettingsEditor));
			this.panelPathConfiguration = new System.Windows.Forms.Panel();
			this.textBoxRelativePathToVotingJSONFilesDescription = new System.Windows.Forms.TextBox();
			this.buttonBrowsePathToServerExecutable = new System.Windows.Forms.Button();
			this.textBoxPathToServerExecutable = new System.Windows.Forms.TextBox();
			this.textBoxRelativeVotingPath = new System.Windows.Forms.TextBox();
			this.labelPathToServerExecutable = new System.Windows.Forms.Label();
			this.labelRelativePathtoVotingJSONFiles = new System.Windows.Forms.Label();
			this.buttonToggleDynamicFileManagement = new System.Windows.Forms.Button();
			this.labelToggleDynamicFileManagement = new System.Windows.Forms.Label();
			this.panelServerSelection = new System.Windows.Forms.Panel();
			this.comboBoxSelectedServer = new System.Windows.Forms.ComboBox();
			this.labelAssociatedServer = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.labelUnsavedChangesIndicator = new System.Windows.Forms.Label();
			this.pictureBoxUnsavedChangesWarningIcon = new System.Windows.Forms.PictureBox();
			this.panelSaveChanges = new System.Windows.Forms.Panel();
			this.panelDynamicJsonFiles = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.buttonAddNewFile = new System.Windows.Forms.Button();
			this.buttonDeleteFile = new System.Windows.Forms.Button();
			this.buttonEditFile = new System.Windows.Forms.Button();
			this.labelVotingFiles = new System.Windows.Forms.Label();
			this.listViewDynamicVotingFiles = new System.Windows.Forms.ListView();
			this.toolTipEnableFileManagement = new System.Windows.Forms.ToolTip(this.components);
			this.panelPathConfiguration.SuspendLayout();
			this.panelServerSelection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUnsavedChangesWarningIcon)).BeginInit();
			this.panelSaveChanges.SuspendLayout();
			this.panelDynamicJsonFiles.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelPathConfiguration
			// 
			this.panelPathConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPathConfiguration.Controls.Add(this.textBoxRelativePathToVotingJSONFilesDescription);
			this.panelPathConfiguration.Controls.Add(this.buttonBrowsePathToServerExecutable);
			this.panelPathConfiguration.Controls.Add(this.textBoxPathToServerExecutable);
			this.panelPathConfiguration.Controls.Add(this.textBoxRelativeVotingPath);
			this.panelPathConfiguration.Controls.Add(this.labelPathToServerExecutable);
			this.panelPathConfiguration.Controls.Add(this.labelRelativePathtoVotingJSONFiles);
			this.panelPathConfiguration.Location = new System.Drawing.Point(12, 70);
			this.panelPathConfiguration.Name = "panelPathConfiguration";
			this.panelPathConfiguration.Size = new System.Drawing.Size(334, 126);
			this.panelPathConfiguration.TabIndex = 0;
			// 
			// textBoxRelativePathToVotingJSONFilesDescription
			// 
			this.textBoxRelativePathToVotingJSONFilesDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRelativePathToVotingJSONFilesDescription.Location = new System.Drawing.Point(3, 72);
			this.textBoxRelativePathToVotingJSONFilesDescription.Multiline = true;
			this.textBoxRelativePathToVotingJSONFilesDescription.Name = "textBoxRelativePathToVotingJSONFilesDescription";
			this.textBoxRelativePathToVotingJSONFilesDescription.ReadOnly = true;
			this.textBoxRelativePathToVotingJSONFilesDescription.ShortcutsEnabled = false;
			this.textBoxRelativePathToVotingJSONFilesDescription.Size = new System.Drawing.Size(328, 50);
			this.textBoxRelativePathToVotingJSONFilesDescription.TabIndex = 4;
			this.textBoxRelativePathToVotingJSONFilesDescription.Text = "This is the path from the server executable\'s directory to the directory where vo" +
    "ting JSON files are located. The default path would be \"mods\\server\".";
			// 
			// buttonBrowsePathToServerExecutable
			// 
			this.buttonBrowsePathToServerExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowsePathToServerExecutable.Location = new System.Drawing.Point(278, 17);
			this.buttonBrowsePathToServerExecutable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.buttonBrowsePathToServerExecutable.Name = "buttonBrowsePathToServerExecutable";
			this.buttonBrowsePathToServerExecutable.Size = new System.Drawing.Size(53, 23);
			this.buttonBrowsePathToServerExecutable.TabIndex = 3;
			this.buttonBrowsePathToServerExecutable.Text = "Browse";
			this.buttonBrowsePathToServerExecutable.UseVisualStyleBackColor = true;
			// 
			// textBoxPathToServerExecutable
			// 
			this.textBoxPathToServerExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPathToServerExecutable.Location = new System.Drawing.Point(3, 19);
			this.textBoxPathToServerExecutable.Name = "textBoxPathToServerExecutable";
			this.textBoxPathToServerExecutable.Size = new System.Drawing.Size(269, 20);
			this.textBoxPathToServerExecutable.TabIndex = 2;
			this.textBoxPathToServerExecutable.TextChanged += new System.EventHandler(this.CheckUnsavedChanges);
			// 
			// textBoxRelativeVotingPath
			// 
			this.textBoxRelativeVotingPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRelativeVotingPath.Location = new System.Drawing.Point(180, 46);
			this.textBoxRelativeVotingPath.Name = "textBoxRelativeVotingPath";
			this.textBoxRelativeVotingPath.Size = new System.Drawing.Size(151, 20);
			this.textBoxRelativeVotingPath.TabIndex = 2;
			this.textBoxRelativeVotingPath.Text = "mods\\server";
			this.textBoxRelativeVotingPath.TextChanged += new System.EventHandler(this.CheckUnsavedChanges);
			// 
			// labelPathToServerExecutable
			// 
			this.labelPathToServerExecutable.AutoSize = true;
			this.labelPathToServerExecutable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPathToServerExecutable.Location = new System.Drawing.Point(3, 3);
			this.labelPathToServerExecutable.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.labelPathToServerExecutable.Name = "labelPathToServerExecutable";
			this.labelPathToServerExecutable.Size = new System.Drawing.Size(131, 13);
			this.labelPathToServerExecutable.TabIndex = 0;
			this.labelPathToServerExecutable.Text = "Path to Server Executable";
			// 
			// labelRelativePathtoVotingJSONFiles
			// 
			this.labelRelativePathtoVotingJSONFiles.AutoSize = true;
			this.labelRelativePathtoVotingJSONFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelRelativePathtoVotingJSONFiles.Location = new System.Drawing.Point(3, 49);
			this.labelRelativePathtoVotingJSONFiles.Name = "labelRelativePathtoVotingJSONFiles";
			this.labelRelativePathtoVotingJSONFiles.Size = new System.Drawing.Size(171, 13);
			this.labelRelativePathtoVotingJSONFiles.TabIndex = 0;
			this.labelRelativePathtoVotingJSONFiles.Text = "Relative Path to Voting JSON Files";
			// 
			// buttonToggleDynamicFileManagement
			// 
			this.buttonToggleDynamicFileManagement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonToggleDynamicFileManagement.Enabled = false;
			this.buttonToggleDynamicFileManagement.Location = new System.Drawing.Point(222, 41);
			this.buttonToggleDynamicFileManagement.Name = "buttonToggleDynamicFileManagement";
			this.buttonToggleDynamicFileManagement.Size = new System.Drawing.Size(53, 23);
			this.buttonToggleDynamicFileManagement.TabIndex = 3;
			this.buttonToggleDynamicFileManagement.Text = "Enable";
			this.toolTipEnableFileManagement.SetToolTip(this.buttonToggleDynamicFileManagement, "Add a Voting JSON Files Directory Path to enable this option.");
			this.buttonToggleDynamicFileManagement.UseVisualStyleBackColor = true;
			this.buttonToggleDynamicFileManagement.Click += new System.EventHandler(this.buttonToggleDynamicVotingFileManagement_Click);
			// 
			// labelToggleDynamicFileManagement
			// 
			this.labelToggleDynamicFileManagement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelToggleDynamicFileManagement.AutoSize = true;
			this.labelToggleDynamicFileManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelToggleDynamicFileManagement.Location = new System.Drawing.Point(15, 46);
			this.labelToggleDynamicFileManagement.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.labelToggleDynamicFileManagement.Name = "labelToggleDynamicFileManagement";
			this.labelToggleDynamicFileManagement.Size = new System.Drawing.Size(201, 13);
			this.labelToggleDynamicFileManagement.TabIndex = 0;
			this.labelToggleDynamicFileManagement.Text = "Enable Dynamic Voting File Management";
			this.toolTipEnableFileManagement.SetToolTip(this.labelToggleDynamicFileManagement, "Add a Voting JSON Files Directory Path to enable this option.");
			// 
			// panelServerSelection
			// 
			this.panelServerSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelServerSelection.Controls.Add(this.comboBoxSelectedServer);
			this.panelServerSelection.Controls.Add(this.labelAssociatedServer);
			this.panelServerSelection.Location = new System.Drawing.Point(12, 12);
			this.panelServerSelection.Name = "panelServerSelection";
			this.panelServerSelection.Size = new System.Drawing.Size(334, 28);
			this.panelServerSelection.TabIndex = 1;
			// 
			// comboBoxSelectedServer
			// 
			this.comboBoxSelectedServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxSelectedServer.FormattingEnabled = true;
			this.comboBoxSelectedServer.Location = new System.Drawing.Point(139, 3);
			this.comboBoxSelectedServer.Name = "comboBoxSelectedServer";
			this.comboBoxSelectedServer.Size = new System.Drawing.Size(192, 21);
			this.comboBoxSelectedServer.TabIndex = 1;
			this.comboBoxSelectedServer.Text = "Select Server";
			this.comboBoxSelectedServer.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectedServer_SelectedIndexChanged);
			// 
			// labelAssociatedServer
			// 
			this.labelAssociatedServer.AutoSize = true;
			this.labelAssociatedServer.Location = new System.Drawing.Point(3, 6);
			this.labelAssociatedServer.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.labelAssociatedServer.Name = "labelAssociatedServer";
			this.labelAssociatedServer.Size = new System.Drawing.Size(135, 13);
			this.labelAssociatedServer.TabIndex = 0;
			this.labelAssociatedServer.Text = "Editing Settings For Server:";
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.AutoSize = true;
			this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonSave.Enabled = false;
			this.buttonSave.Location = new System.Drawing.Point(137, 12);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(42, 23);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// labelUnsavedChangesIndicator
			// 
			this.labelUnsavedChangesIndicator.AutoSize = true;
			this.labelUnsavedChangesIndicator.Location = new System.Drawing.Point(11, 17);
			this.labelUnsavedChangesIndicator.Margin = new System.Windows.Forms.Padding(0);
			this.labelUnsavedChangesIndicator.Name = "labelUnsavedChangesIndicator";
			this.labelUnsavedChangesIndicator.Size = new System.Drawing.Size(95, 13);
			this.labelUnsavedChangesIndicator.TabIndex = 2;
			this.labelUnsavedChangesIndicator.Text = "Unsaved Changes";
			this.labelUnsavedChangesIndicator.Visible = false;
			// 
			// pictureBoxUnsavedChangesWarningIcon
			// 
			this.pictureBoxUnsavedChangesWarningIcon.BackgroundImage = global::RconTool.Properties.Resources.Image_WarningSign32x32;
			this.pictureBoxUnsavedChangesWarningIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pictureBoxUnsavedChangesWarningIcon.Location = new System.Drawing.Point(109, 14);
			this.pictureBoxUnsavedChangesWarningIcon.Name = "pictureBoxUnsavedChangesWarningIcon";
			this.pictureBoxUnsavedChangesWarningIcon.Size = new System.Drawing.Size(18, 18);
			this.pictureBoxUnsavedChangesWarningIcon.TabIndex = 3;
			this.pictureBoxUnsavedChangesWarningIcon.TabStop = false;
			this.pictureBoxUnsavedChangesWarningIcon.Visible = false;
			// 
			// panelSaveChanges
			// 
			this.panelSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.panelSaveChanges.Controls.Add(this.labelUnsavedChangesIndicator);
			this.panelSaveChanges.Controls.Add(this.pictureBoxUnsavedChangesWarningIcon);
			this.panelSaveChanges.Controls.Add(this.buttonSave);
			this.panelSaveChanges.Location = new System.Drawing.Point(167, 391);
			this.panelSaveChanges.Name = "panelSaveChanges";
			this.panelSaveChanges.Size = new System.Drawing.Size(191, 47);
			this.panelSaveChanges.TabIndex = 4;
			// 
			// panelDynamicJsonFiles
			// 
			this.panelDynamicJsonFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelDynamicJsonFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelDynamicJsonFiles.Controls.Add(this.tableLayoutPanel1);
			this.panelDynamicJsonFiles.Controls.Add(this.labelVotingFiles);
			this.panelDynamicJsonFiles.Controls.Add(this.listViewDynamicVotingFiles);
			this.panelDynamicJsonFiles.Enabled = false;
			this.panelDynamicJsonFiles.Location = new System.Drawing.Point(12, 202);
			this.panelDynamicJsonFiles.Name = "panelDynamicJsonFiles";
			this.panelDynamicJsonFiles.Size = new System.Drawing.Size(334, 195);
			this.panelDynamicJsonFiles.TabIndex = 5;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.buttonAddNewFile, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.buttonDeleteFile, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.buttonEditFile, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 19);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(80, 171);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// buttonAddNewFile
			// 
			this.buttonAddNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddNewFile.AutoSize = true;
			this.buttonAddNewFile.Location = new System.Drawing.Point(3, 16);
			this.buttonAddNewFile.Name = "buttonAddNewFile";
			this.buttonAddNewFile.Size = new System.Drawing.Size(74, 23);
			this.buttonAddNewFile.TabIndex = 2;
			this.buttonAddNewFile.Text = "Add New";
			this.buttonAddNewFile.UseVisualStyleBackColor = true;
			this.buttonAddNewFile.Click += new System.EventHandler(this.buttonAddNewFile_Click);
			// 
			// buttonDeleteFile
			// 
			this.buttonDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDeleteFile.AutoSize = true;
			this.buttonDeleteFile.Location = new System.Drawing.Point(3, 130);
			this.buttonDeleteFile.Name = "buttonDeleteFile";
			this.buttonDeleteFile.Size = new System.Drawing.Size(74, 23);
			this.buttonDeleteFile.TabIndex = 2;
			this.buttonDeleteFile.Text = "Delete File";
			this.buttonDeleteFile.UseVisualStyleBackColor = true;
			this.buttonDeleteFile.Click += new System.EventHandler(this.buttonDeleteFile_Click);
			// 
			// buttonEditFile
			// 
			this.buttonEditFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditFile.AutoSize = true;
			this.buttonEditFile.Enabled = false;
			this.buttonEditFile.Location = new System.Drawing.Point(3, 72);
			this.buttonEditFile.Name = "buttonEditFile";
			this.buttonEditFile.Size = new System.Drawing.Size(74, 23);
			this.buttonEditFile.TabIndex = 2;
			this.buttonEditFile.Text = "Edit File";
			this.toolTipEnableFileManagement.SetToolTip(this.buttonEditFile, "This feature has not yet been added.");
			this.buttonEditFile.UseVisualStyleBackColor = true;
			this.buttonEditFile.Click += new System.EventHandler(this.buttonEditFile_Click);
			// 
			// labelVotingFiles
			// 
			this.labelVotingFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelVotingFiles.AutoSize = true;
			this.labelVotingFiles.Location = new System.Drawing.Point(118, 3);
			this.labelVotingFiles.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.labelVotingFiles.Name = "labelVotingFiles";
			this.labelVotingFiles.Size = new System.Drawing.Size(92, 13);
			this.labelVotingFiles.TabIndex = 1;
			this.labelVotingFiles.Text = "JSON Voting Files";
			// 
			// listViewDynamicVotingFiles
			// 
			this.listViewDynamicVotingFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewDynamicVotingFiles.HideSelection = false;
			this.listViewDynamicVotingFiles.Location = new System.Drawing.Point(85, 19);
			this.listViewDynamicVotingFiles.Name = "listViewDynamicVotingFiles";
			this.listViewDynamicVotingFiles.Size = new System.Drawing.Size(244, 171);
			this.listViewDynamicVotingFiles.TabIndex = 0;
			this.listViewDynamicVotingFiles.UseCompatibleStateImageBehavior = false;
			// 
			// ServerSettingsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(358, 436);
			this.Controls.Add(this.panelDynamicJsonFiles);
			this.Controls.Add(this.buttonToggleDynamicFileManagement);
			this.Controls.Add(this.panelSaveChanges);
			this.Controls.Add(this.panelServerSelection);
			this.Controls.Add(this.panelPathConfiguration);
			this.Controls.Add(this.labelToggleDynamicFileManagement);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ServerSettingsEditor";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Server Settings";
			this.panelPathConfiguration.ResumeLayout(false);
			this.panelPathConfiguration.PerformLayout();
			this.panelServerSelection.ResumeLayout(false);
			this.panelServerSelection.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUnsavedChangesWarningIcon)).EndInit();
			this.panelSaveChanges.ResumeLayout(false);
			this.panelSaveChanges.PerformLayout();
			this.panelDynamicJsonFiles.ResumeLayout(false);
			this.panelDynamicJsonFiles.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelPathConfiguration;
		private System.Windows.Forms.TextBox textBoxRelativeVotingPath;
		private System.Windows.Forms.Label labelRelativePathtoVotingJSONFiles;
		private System.Windows.Forms.TextBox textBoxRelativePathToVotingJSONFilesDescription;
		private System.Windows.Forms.Button buttonBrowsePathToServerExecutable;
		private System.Windows.Forms.TextBox textBoxPathToServerExecutable;
		private System.Windows.Forms.Label labelPathToServerExecutable;
		private System.Windows.Forms.Panel panelServerSelection;
		private System.Windows.Forms.Label labelAssociatedServer;
		private System.Windows.Forms.ComboBox comboBoxSelectedServer;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelUnsavedChangesIndicator;
		private System.Windows.Forms.PictureBox pictureBoxUnsavedChangesWarningIcon;
		private System.Windows.Forms.Button buttonToggleDynamicFileManagement;
		private System.Windows.Forms.Label labelToggleDynamicFileManagement;
		private System.Windows.Forms.Panel panelSaveChanges;
		private System.Windows.Forms.Panel panelDynamicJsonFiles;
		private System.Windows.Forms.Label labelVotingFiles;
		private System.Windows.Forms.ListView listViewDynamicVotingFiles;
		private System.Windows.Forms.Button buttonDeleteFile;
		private System.Windows.Forms.Button buttonEditFile;
		private System.Windows.Forms.Button buttonAddNewFile;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolTip toolTipEnableFileManagement;
	}
}