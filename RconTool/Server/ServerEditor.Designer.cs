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
			this.components = new System.ComponentModel.Container();
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
			this.checkBoxUseServerHook = new System.Windows.Forms.CheckBox();
			this.panelPathConfiguration = new System.Windows.Forms.Panel();
			this.pictureBoxMapsFolderFoundIndicator = new System.Windows.Forms.PictureBox();
			this.pictureBoxGamesFolderFoundIndicator = new System.Windows.Forms.PictureBox();
			this.pictureBoxVoteFilesFolderFoundIndicator = new System.Windows.Forms.PictureBox();
			this.labelVoteFilesFolderFound = new System.Windows.Forms.Label();
			this.labelGamesFolderFound = new System.Windows.Forms.Label();
			this.labelFound = new System.Windows.Forms.Label();
			this.labelMapsFolderFound = new System.Windows.Forms.Label();
			this.checkBoxUseLocalData = new System.Windows.Forms.CheckBox();
			this.buttonBrowseGameExecutable = new System.Windows.Forms.Button();
			this.textBoxGameExecutable = new System.Windows.Forms.TextBox();
			this.labelGameExecutable = new System.Windows.Forms.Label();
			this.checkBoxIncludeDefaultFilteredPhrases = new System.Windows.Forms.CheckBox();
			this.checkBoxEnableAutoTranslate = new System.Windows.Forms.CheckBox();
			this.comboBoxServerLanguage = new System.Windows.Forms.ComboBox();
			this.labelServerLanguage = new System.Windows.Forms.Label();
			this.labelName = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelServerPassword = new System.Windows.Forms.Label();
			this.textBoxServerPassword = new System.Windows.Forms.TextBox();
			this.panelCommandsAndSaveButton = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panelDetailEntry.SuspendLayout();
			this.panelPathConfiguration.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMapsFolderFoundIndicator)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGamesFolderFoundIndicator)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVoteFilesFolderFoundIndicator)).BeginInit();
			this.panelCommandsAndSaveButton.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxServerIP
			// 
			this.textBoxServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServerIP.Location = new System.Drawing.Point(100, 12);
			this.textBoxServerIP.Name = "textBoxServerIP";
			this.textBoxServerIP.Size = new System.Drawing.Size(213, 23);
			this.textBoxServerIP.TabIndex = 0;
			// 
			// labelServerIP
			// 
			this.labelServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelServerIP.AutoSize = true;
			this.labelServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelServerIP.Location = new System.Drawing.Point(40, 16);
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
			this.labelServerPort.Location = new System.Drawing.Point(31, 46);
			this.labelServerPort.Name = "labelServerPort";
			this.labelServerPort.Size = new System.Drawing.Size(63, 13);
			this.labelServerPort.TabIndex = 0;
			this.labelServerPort.Text = "Server Port:";
			// 
			// textBoxServerPort
			// 
			this.textBoxServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServerPort.Location = new System.Drawing.Point(100, 42);
			this.textBoxServerPort.Name = "textBoxServerPort";
			this.textBoxServerPort.Size = new System.Drawing.Size(213, 23);
			this.textBoxServerPort.TabIndex = 1;
			// 
			// labelRconPassword
			// 
			this.labelRconPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRconPassword.AutoSize = true;
			this.labelRconPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelRconPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelRconPassword.Location = new System.Drawing.Point(9, 136);
			this.labelRconPassword.Name = "labelRconPassword";
			this.labelRconPassword.Size = new System.Drawing.Size(85, 13);
			this.labelRconPassword.TabIndex = 0;
			this.labelRconPassword.Text = "Rcon Password:";
			// 
			// textBoxRconPassword
			// 
			this.textBoxRconPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRconPassword.Location = new System.Drawing.Point(100, 132);
			this.textBoxRconPassword.Name = "textBoxRconPassword";
			this.textBoxRconPassword.Size = new System.Drawing.Size(213, 23);
			this.textBoxRconPassword.TabIndex = 4;
			this.textBoxRconPassword.UseSystemPasswordChar = true;
			// 
			// labelRconPort
			// 
			this.labelRconPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRconPort.AutoSize = true;
			this.labelRconPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelRconPort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelRconPort.Location = new System.Drawing.Point(36, 106);
			this.labelRconPort.Name = "labelRconPort";
			this.labelRconPort.Size = new System.Drawing.Size(58, 13);
			this.labelRconPort.TabIndex = 0;
			this.labelRconPort.Text = "Rcon Port:";
			// 
			// textBoxRconPort
			// 
			this.textBoxRconPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRconPort.Location = new System.Drawing.Point(100, 102);
			this.textBoxRconPort.Name = "textBoxRconPort";
			this.textBoxRconPort.Size = new System.Drawing.Size(213, 23);
			this.textBoxRconPort.TabIndex = 3;
			// 
			// buttonSaveServer
			// 
			this.buttonSaveServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSaveServer.Location = new System.Drawing.Point(229, 154);
			this.buttonSaveServer.Name = "buttonSaveServer";
			this.buttonSaveServer.Size = new System.Drawing.Size(87, 25);
			this.buttonSaveServer.TabIndex = 7;
			this.buttonSaveServer.Text = "Save Server";
			this.buttonSaveServer.UseVisualStyleBackColor = true;
			this.buttonSaveServer.Click += new System.EventHandler(this.buttonSaveServer_Click);
			// 
			// labelCommandsToSendUponConnection
			// 
			this.labelCommandsToSendUponConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelCommandsToSendUponConnection.AutoSize = true;
			this.labelCommandsToSendUponConnection.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.labelCommandsToSendUponConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelCommandsToSendUponConnection.Location = new System.Drawing.Point(4, 2);
			this.labelCommandsToSendUponConnection.Name = "labelCommandsToSendUponConnection";
			this.labelCommandsToSendUponConnection.Size = new System.Drawing.Size(201, 13);
			this.labelCommandsToSendUponConnection.TabIndex = 0;
			this.labelCommandsToSendUponConnection.Text = "Commands to send upon connection:";
			// 
			// textBoxCommandsToSendUponConnection
			// 
			this.textBoxCommandsToSendUponConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCommandsToSendUponConnection.Location = new System.Drawing.Point(7, 18);
			this.textBoxCommandsToSendUponConnection.Multiline = true;
			this.textBoxCommandsToSendUponConnection.Name = "textBoxCommandsToSendUponConnection";
			this.textBoxCommandsToSendUponConnection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxCommandsToSendUponConnection.Size = new System.Drawing.Size(309, 130);
			this.textBoxCommandsToSendUponConnection.TabIndex = 7;
			// 
			// panelDetailEntry
			// 
			this.panelDetailEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelDetailEntry.Controls.Add(this.checkBoxUseServerHook);
			this.panelDetailEntry.Controls.Add(this.panelPathConfiguration);
			this.panelDetailEntry.Controls.Add(this.checkBoxIncludeDefaultFilteredPhrases);
			this.panelDetailEntry.Controls.Add(this.checkBoxEnableAutoTranslate);
			this.panelDetailEntry.Controls.Add(this.comboBoxServerLanguage);
			this.panelDetailEntry.Controls.Add(this.labelServerLanguage);
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
			this.panelDetailEntry.Size = new System.Drawing.Size(314, 390);
			this.panelDetailEntry.TabIndex = 17;
			// 
			// checkBoxUseServerHook
			// 
			this.checkBoxUseServerHook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxUseServerHook.AutoSize = true;
			this.checkBoxUseServerHook.Location = new System.Drawing.Point(7, 277);
			this.checkBoxUseServerHook.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.checkBoxUseServerHook.Name = "checkBoxUseServerHook";
			this.checkBoxUseServerHook.Size = new System.Drawing.Size(294, 19);
			this.checkBoxUseServerHook.TabIndex = 9;
			this.checkBoxUseServerHook.Text = "Use Server Hook? (Server Must Be Running Locally)";
			this.checkBoxUseServerHook.UseVisualStyleBackColor = true;
			// 
			// panelPathConfiguration
			// 
			this.panelPathConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPathConfiguration.Controls.Add(this.pictureBoxMapsFolderFoundIndicator);
			this.panelPathConfiguration.Controls.Add(this.pictureBoxGamesFolderFoundIndicator);
			this.panelPathConfiguration.Controls.Add(this.pictureBoxVoteFilesFolderFoundIndicator);
			this.panelPathConfiguration.Controls.Add(this.labelVoteFilesFolderFound);
			this.panelPathConfiguration.Controls.Add(this.labelGamesFolderFound);
			this.panelPathConfiguration.Controls.Add(this.labelFound);
			this.panelPathConfiguration.Controls.Add(this.labelMapsFolderFound);
			this.panelPathConfiguration.Controls.Add(this.checkBoxUseLocalData);
			this.panelPathConfiguration.Controls.Add(this.buttonBrowseGameExecutable);
			this.panelPathConfiguration.Controls.Add(this.textBoxGameExecutable);
			this.panelPathConfiguration.Controls.Add(this.labelGameExecutable);
			this.panelPathConfiguration.Location = new System.Drawing.Point(3, 296);
			this.panelPathConfiguration.Name = "panelPathConfiguration";
			this.panelPathConfiguration.Size = new System.Drawing.Size(311, 96);
			this.panelPathConfiguration.TabIndex = 8;
			// 
			// pictureBoxMapsFolderFoundIndicator
			// 
			this.pictureBoxMapsFolderFoundIndicator.Enabled = false;
			this.pictureBoxMapsFolderFoundIndicator.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMapsFolderFoundIndicator.Image")));
			this.pictureBoxMapsFolderFoundIndicator.Location = new System.Drawing.Point(112, 76);
			this.pictureBoxMapsFolderFoundIndicator.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.pictureBoxMapsFolderFoundIndicator.Name = "pictureBoxMapsFolderFoundIndicator";
			this.pictureBoxMapsFolderFoundIndicator.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxMapsFolderFoundIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxMapsFolderFoundIndicator.TabIndex = 8;
			this.pictureBoxMapsFolderFoundIndicator.TabStop = false;
			// 
			// pictureBoxGamesFolderFoundIndicator
			// 
			this.pictureBoxGamesFolderFoundIndicator.Enabled = false;
			this.pictureBoxGamesFolderFoundIndicator.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxGamesFolderFoundIndicator.Image")));
			this.pictureBoxGamesFolderFoundIndicator.Location = new System.Drawing.Point(204, 76);
			this.pictureBoxGamesFolderFoundIndicator.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.pictureBoxGamesFolderFoundIndicator.Name = "pictureBoxGamesFolderFoundIndicator";
			this.pictureBoxGamesFolderFoundIndicator.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxGamesFolderFoundIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxGamesFolderFoundIndicator.TabIndex = 8;
			this.pictureBoxGamesFolderFoundIndicator.TabStop = false;
			// 
			// pictureBoxVoteFilesFolderFoundIndicator
			// 
			this.pictureBoxVoteFilesFolderFoundIndicator.Enabled = false;
			this.pictureBoxVoteFilesFolderFoundIndicator.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxVoteFilesFolderFoundIndicator.Image")));
			this.pictureBoxVoteFilesFolderFoundIndicator.Location = new System.Drawing.Point(285, 76);
			this.pictureBoxVoteFilesFolderFoundIndicator.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.pictureBoxVoteFilesFolderFoundIndicator.Name = "pictureBoxVoteFilesFolderFoundIndicator";
			this.pictureBoxVoteFilesFolderFoundIndicator.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxVoteFilesFolderFoundIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxVoteFilesFolderFoundIndicator.TabIndex = 8;
			this.pictureBoxVoteFilesFolderFoundIndicator.TabStop = false;
			// 
			// labelVoteFilesFolderFound
			// 
			this.labelVoteFilesFolderFound.AutoSize = true;
			this.labelVoteFilesFolderFound.Enabled = false;
			this.labelVoteFilesFolderFound.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelVoteFilesFolderFound.Location = new System.Drawing.Point(226, 76);
			this.labelVoteFilesFolderFound.Name = "labelVoteFilesFolderFound";
			this.labelVoteFilesFolderFound.Size = new System.Drawing.Size(56, 15);
			this.labelVoteFilesFolderFound.TabIndex = 7;
			this.labelVoteFilesFolderFound.Text = "Vote Files";
			// 
			// labelGamesFolderFound
			// 
			this.labelGamesFolderFound.AutoSize = true;
			this.labelGamesFolderFound.Enabled = false;
			this.labelGamesFolderFound.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelGamesFolderFound.Location = new System.Drawing.Point(134, 76);
			this.labelGamesFolderFound.Name = "labelGamesFolderFound";
			this.labelGamesFolderFound.Size = new System.Drawing.Size(67, 15);
			this.labelGamesFolderFound.TabIndex = 7;
			this.labelGamesFolderFound.Text = "GameTypes";
			// 
			// labelFound
			// 
			this.labelFound.AutoSize = true;
			this.labelFound.Enabled = false;
			this.labelFound.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelFound.Location = new System.Drawing.Point(14, 76);
			this.labelFound.Name = "labelFound";
			this.labelFound.Size = new System.Drawing.Size(44, 15);
			this.labelFound.TabIndex = 7;
			this.labelFound.Text = "Found:";
			// 
			// labelMapsFolderFound
			// 
			this.labelMapsFolderFound.AutoSize = true;
			this.labelMapsFolderFound.Enabled = false;
			this.labelMapsFolderFound.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelMapsFolderFound.Location = new System.Drawing.Point(73, 76);
			this.labelMapsFolderFound.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelMapsFolderFound.Name = "labelMapsFolderFound";
			this.labelMapsFolderFound.Size = new System.Drawing.Size(36, 15);
			this.labelMapsFolderFound.TabIndex = 7;
			this.labelMapsFolderFound.Text = "Maps";
			// 
			// checkBoxUseLocalData
			// 
			this.checkBoxUseLocalData.AutoSize = true;
			this.checkBoxUseLocalData.Location = new System.Drawing.Point(4, 3);
			this.checkBoxUseLocalData.Name = "checkBoxUseLocalData";
			this.checkBoxUseLocalData.Size = new System.Drawing.Size(261, 19);
			this.checkBoxUseLocalData.TabIndex = 6;
			this.checkBoxUseLocalData.Text = "Use Local Data? Maps, GameTypes, Vote Files";
			this.checkBoxUseLocalData.UseVisualStyleBackColor = true;
			this.checkBoxUseLocalData.CheckedChanged += new System.EventHandler(this.checkBoxUseLocalData_CheckedChanged);
			// 
			// buttonBrowseGameExecutable
			// 
			this.buttonBrowseGameExecutable.Enabled = false;
			this.buttonBrowseGameExecutable.Location = new System.Drawing.Point(4, 47);
			this.buttonBrowseGameExecutable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.buttonBrowseGameExecutable.Name = "buttonBrowseGameExecutable";
			this.buttonBrowseGameExecutable.Size = new System.Drawing.Size(62, 25);
			this.buttonBrowseGameExecutable.TabIndex = 2;
			this.buttonBrowseGameExecutable.Text = "Browse";
			this.buttonBrowseGameExecutable.UseVisualStyleBackColor = true;
			this.buttonBrowseGameExecutable.Click += new System.EventHandler(this.buttonBrowseGameExecutable_Click);
			// 
			// textBoxGameExecutable
			// 
			this.textBoxGameExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxGameExecutable.Enabled = false;
			this.textBoxGameExecutable.Location = new System.Drawing.Point(73, 48);
			this.textBoxGameExecutable.Name = "textBoxGameExecutable";
			this.textBoxGameExecutable.Size = new System.Drawing.Size(230, 23);
			this.textBoxGameExecutable.TabIndex = 3;
			// 
			// labelGameExecutable
			// 
			this.labelGameExecutable.AutoSize = true;
			this.labelGameExecutable.Enabled = false;
			this.labelGameExecutable.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.labelGameExecutable.Location = new System.Drawing.Point(1, 28);
			this.labelGameExecutable.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.labelGameExecutable.Name = "labelGameExecutable";
			this.labelGameExecutable.Size = new System.Drawing.Size(233, 13);
			this.labelGameExecutable.TabIndex = 0;
			this.labelGameExecutable.Text = "Directory of Game Executable (eldorado.exe)";
			// 
			// checkBoxIncludeDefaultFilteredPhrases
			// 
			this.checkBoxIncludeDefaultFilteredPhrases.AutoSize = true;
			this.checkBoxIncludeDefaultFilteredPhrases.Location = new System.Drawing.Point(123, 246);
			this.checkBoxIncludeDefaultFilteredPhrases.Name = "checkBoxIncludeDefaultFilteredPhrases";
			this.checkBoxIncludeDefaultFilteredPhrases.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBoxIncludeDefaultFilteredPhrases.Size = new System.Drawing.Size(191, 19);
			this.checkBoxIncludeDefaultFilteredPhrases.TabIndex = 7;
			this.checkBoxIncludeDefaultFilteredPhrases.Text = "Include Default Filtered Phrases";
			this.toolTip1.SetToolTip(this.checkBoxIncludeDefaultFilteredPhrases, "Prevents AutoTranslate from triggering on many common phrases (like \"lol\", or \"yo" +
        "\")");
			this.checkBoxIncludeDefaultFilteredPhrases.UseVisualStyleBackColor = true;
			// 
			// checkBoxEnableAutoTranslate
			// 
			this.checkBoxEnableAutoTranslate.AutoSize = true;
			this.checkBoxEnableAutoTranslate.Location = new System.Drawing.Point(101, 221);
			this.checkBoxEnableAutoTranslate.Name = "checkBoxEnableAutoTranslate";
			this.checkBoxEnableAutoTranslate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBoxEnableAutoTranslate.Size = new System.Drawing.Size(213, 19);
			this.checkBoxEnableAutoTranslate.TabIndex = 7;
			this.checkBoxEnableAutoTranslate.Text = "Enable Server AutoTranslate Feature";
			this.toolTip1.SetToolTip(this.checkBoxEnableAutoTranslate, "If enabled, the server will detect foreign language chat messages and automatical" +
        "ly post a server-language translation for them in chat for everyone to see");
			this.checkBoxEnableAutoTranslate.UseVisualStyleBackColor = true;
			// 
			// comboBoxServerLanguage
			// 
			this.comboBoxServerLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxServerLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxServerLanguage.FormattingEnabled = true;
			this.comboBoxServerLanguage.Items.AddRange(new object[] {
            "English",
            "Spanish",
            "French",
            "German",
            "Afrikaans",
            "Albanian",
            "Amharic",
            "Arabic",
            "Armenian",
            "Azerbaijani",
            "Basque",
            "Belarusian",
            "Bengali",
            "Bosnian",
            "Bulgarian",
            "Catalan",
            "Cebuano",
            "Chichewa",
            "Chinese_Simplified",
            "Chinese_Simplified_CN",
            "Chinese_Traditional_TW",
            "Corsican",
            "Croatian",
            "Czech",
            "Danish",
            "Dutch",
            "Esperanto",
            "Estonian",
            "Filipino",
            "Finnish",
            "Frisian",
            "Galician",
            "Georgian",
            "Greek",
            "Gujarati",
            "Haitian_Creole",
            "Hausa",
            "Hawaiian",
            "Hebrew_IW",
            "Hebrew_HE",
            "Hindi",
            "Hmong",
            "Hungarian",
            "Icelandic",
            "Igbo",
            "Indonesian",
            "Irish",
            "Italian",
            "Japanese",
            "Javanese",
            "Kannada",
            "Kazakh",
            "Khmer",
            "Kinyarwanda",
            "Korean",
            "Kurdish_Kurmanji",
            "Kyrgyz",
            "Lao",
            "Latin",
            "Latvian",
            "Lithuanian",
            "Luxembourgish",
            "Macedonian",
            "Malagasy",
            "Malay",
            "Malayalam",
            "Maltese",
            "Maori",
            "Marathi",
            "Mongolian",
            "Myanmar_Burmese",
            "Nepali",
            "Norwegian",
            "Odia_Oriya",
            "Pashto",
            "Persian",
            "Polish",
            "Portuguese",
            "Punjabi",
            "Romanian",
            "Russian",
            "Samoan",
            "Scots_Gaelic",
            "Serbian",
            "Sesotho",
            "Shona",
            "Sindhi",
            "Sinhala",
            "Slovak",
            "Slovenian",
            "Somali",
            "Sundanese",
            "Swahili",
            "Swedish",
            "Tajik",
            "Tamil",
            "Tatar",
            "Telugu",
            "Thai",
            "Turkish",
            "Turkmen",
            "Ukrainian",
            "Urdu",
            "Uyghur",
            "Uzbek",
            "Vietnamese",
            "Welsh",
            "Xhosa",
            "Yiddish",
            "Yoruba",
            "Zulu"});
			this.comboBoxServerLanguage.Location = new System.Drawing.Point(100, 192);
			this.comboBoxServerLanguage.Name = "comboBoxServerLanguage";
			this.comboBoxServerLanguage.Size = new System.Drawing.Size(213, 23);
			this.comboBoxServerLanguage.TabIndex = 6;
			// 
			// labelServerLanguage
			// 
			this.labelServerLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelServerLanguage.AutoSize = true;
			this.labelServerLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelServerLanguage.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelServerLanguage.Location = new System.Drawing.Point(2, 196);
			this.labelServerLanguage.Name = "labelServerLanguage";
			this.labelServerLanguage.Size = new System.Drawing.Size(92, 13);
			this.labelServerLanguage.TabIndex = 0;
			this.labelServerLanguage.Text = "Server Language:";
			// 
			// labelName
			// 
			this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelName.AutoSize = true;
			this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelName.Location = new System.Drawing.Point(8, 166);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(86, 13);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "Name (Optional):";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(100, 162);
			this.textBoxName.MaxLength = 64;
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(213, 23);
			this.textBoxName.TabIndex = 5;
			// 
			// labelServerPassword
			// 
			this.labelServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelServerPassword.AutoSize = true;
			this.labelServerPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelServerPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelServerPassword.Location = new System.Drawing.Point(4, 76);
			this.labelServerPassword.Name = "labelServerPassword";
			this.labelServerPassword.Size = new System.Drawing.Size(90, 13);
			this.labelServerPassword.TabIndex = 0;
			this.labelServerPassword.Text = "Server Password:";
			// 
			// textBoxServerPassword
			// 
			this.textBoxServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServerPassword.Location = new System.Drawing.Point(100, 72);
			this.textBoxServerPassword.Name = "textBoxServerPassword";
			this.textBoxServerPassword.Size = new System.Drawing.Size(213, 23);
			this.textBoxServerPassword.TabIndex = 2;
			this.textBoxServerPassword.UseSystemPasswordChar = true;
			// 
			// panelCommandsAndSaveButton
			// 
			this.panelCommandsAndSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelCommandsAndSaveButton.Controls.Add(this.labelCommandsToSendUponConnection);
			this.panelCommandsAndSaveButton.Controls.Add(this.textBoxCommandsToSendUponConnection);
			this.panelCommandsAndSaveButton.Controls.Add(this.buttonSaveServer);
			this.panelCommandsAndSaveButton.Location = new System.Drawing.Point(0, 394);
			this.panelCommandsAndSaveButton.Name = "panelCommandsAndSaveButton";
			this.panelCommandsAndSaveButton.Size = new System.Drawing.Size(316, 185);
			this.panelCommandsAndSaveButton.TabIndex = 18;
			// 
			// ServerEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(321, 582);
			this.Controls.Add(this.panelCommandsAndSaveButton);
			this.Controls.Add(this.panelDetailEntry);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServerEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Server Editor";
			this.panelDetailEntry.ResumeLayout(false);
			this.panelDetailEntry.PerformLayout();
			this.panelPathConfiguration.ResumeLayout(false);
			this.panelPathConfiguration.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMapsFolderFoundIndicator)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGamesFolderFoundIndicator)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVoteFilesFolderFoundIndicator)).EndInit();
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
		private System.Windows.Forms.ComboBox comboBoxServerLanguage;
		private System.Windows.Forms.Label labelServerLanguage;
		private System.Windows.Forms.CheckBox checkBoxIncludeDefaultFilteredPhrases;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox checkBoxEnableAutoTranslate;
		private System.Windows.Forms.Panel panelPathConfiguration;
		private System.Windows.Forms.CheckBox checkBoxUseLocalData;
		private System.Windows.Forms.Button buttonBrowseGameExecutable;
		private System.Windows.Forms.TextBox textBoxGameExecutable;
		private System.Windows.Forms.Label labelGameExecutable;
		private System.Windows.Forms.PictureBox pictureBoxMapsFolderFoundIndicator;
		private System.Windows.Forms.PictureBox pictureBoxGamesFolderFoundIndicator;
		private System.Windows.Forms.PictureBox pictureBoxVoteFilesFolderFoundIndicator;
		private System.Windows.Forms.Label labelVoteFilesFolderFound;
		private System.Windows.Forms.Label labelGamesFolderFound;
		private System.Windows.Forms.Label labelMapsFolderFound;
		private System.Windows.Forms.CheckBox checkBoxUseServerHook;
		private System.Windows.Forms.Label labelFound;
	}
}