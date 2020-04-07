namespace RconTool
{
    partial class App
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
			this.textBoxConsoleTextEntry = new System.Windows.Forms.TextBox();
			this.buttonConsoleTextSend = new System.Windows.Forms.Button();
			this.menuBarApp = new System.Windows.Forms.MenuStrip();
			this.menuButtonFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemManageServers = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemConfigureSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPlaySoundOnPlayerJoin = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPlaySoundOnPlayerLeave = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemChangeScoreboardFontSize = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemManageConditionalCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRetryConnection = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControlServerInterfaces = new System.Windows.Forms.TabControl();
			this.tabPageConsole = new System.Windows.Forms.TabPage();
			this.panelConsoleTextEntry = new System.Windows.Forms.Panel();
			this.buttonConsoleTextClear = new System.Windows.Forms.Button();
			this.textBoxConsoleText = new System.Windows.Forms.TextBox();
			this.tabPageChat = new System.Windows.Forms.TabPage();
			this.panelChatTextEntry = new System.Windows.Forms.Panel();
			this.textBoxChatTextEntry = new System.Windows.Forms.TextBox();
			this.buttonChatTextSend = new System.Windows.Forms.Button();
			this.buttonChatTextClear = new System.Windows.Forms.Button();
			this.textBoxChatText = new System.Windows.Forms.TextBox();
			this.tabPageInfo = new System.Windows.Forms.TabPage();
			this.panelInfo = new System.Windows.Forms.Panel();
			this.labelServerName = new System.Windows.Forms.Label();
			this.labelHost = new System.Windows.Forms.Label();
			this.labelSprintEnabled = new System.Windows.Forms.Label();
			this.labelAssassinations = new System.Windows.Forms.Label();
			this.labelMap = new System.Windows.Forms.Label();
			this.labelVariant = new System.Windows.Forms.Label();
			this.labelVariantType = new System.Windows.Forms.Label();
			this.labelStatus = new System.Windows.Forms.Label();
			this.labelPlayers = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.tabPageControls = new System.Windows.Forms.TabPage();
			this.buttonReloadVetoJson = new System.Windows.Forms.Button();
			this.buttonShuffleTeams = new System.Windows.Forms.Button();
			this.buttonReloadVotingJson = new System.Windows.Forms.Button();
			this.buttonDisableTeamShuffle = new System.Windows.Forms.Button();
			this.buttonEnableTeamShuffle = new System.Windows.Forms.Button();
			this.buttonSetMaxPlayers = new System.Windows.Forms.Button();
			this.textBoxMaxPlayersCount = new System.Windows.Forms.TextBox();
			this.buttonEnableAssasinations = new System.Windows.Forms.Button();
			this.buttonEnableUnlimitedSprint = new System.Windows.Forms.Button();
			this.buttonEnableSprint = new System.Windows.Forms.Button();
			this.buttonStopGame = new System.Windows.Forms.Button();
			this.buttonStartGame = new System.Windows.Forms.Button();
			this.tabPageJoinLeaveLog = new System.Windows.Forms.TabPage();
			this.textBoxJoinLeaveLog = new System.Windows.Forms.TextBox();
			this.tabPageAppLog = new System.Windows.Forms.TabPage();
			this.textBoxAppLog = new System.Windows.Forms.TextBox();
			this.statusStripStatusInformation = new System.Windows.Forms.StatusStrip();
			this.toolStripSplitButtonServerSelect = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelRconConnection = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelStatsConnection = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripSplitButtonAutoUpdate = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripMenuItemAutoUpdateEnabled = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemAutoUpdateDisabled = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStripVersion = new System.Windows.Forms.StatusStrip();
			this.toolStripFontSizeDecrease = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripFontSizeIncrease = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripResizeToFitScoreboard = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.menuBarApp.SuspendLayout();
			this.tabControlServerInterfaces.SuspendLayout();
			this.tabPageConsole.SuspendLayout();
			this.panelConsoleTextEntry.SuspendLayout();
			this.tabPageChat.SuspendLayout();
			this.panelChatTextEntry.SuspendLayout();
			this.tabPageInfo.SuspendLayout();
			this.panelInfo.SuspendLayout();
			this.tabPageControls.SuspendLayout();
			this.tabPageJoinLeaveLog.SuspendLayout();
			this.tabPageAppLog.SuspendLayout();
			this.statusStripStatusInformation.SuspendLayout();
			this.statusStripVersion.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxConsoleTextEntry
			// 
			this.textBoxConsoleTextEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxConsoleTextEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.textBoxConsoleTextEntry.Location = new System.Drawing.Point(0, 3);
			this.textBoxConsoleTextEntry.Name = "textBoxConsoleTextEntry";
			this.textBoxConsoleTextEntry.Size = new System.Drawing.Size(622, 26);
			this.textBoxConsoleTextEntry.TabIndex = 0;
			this.textBoxConsoleTextEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxConsoleTextEntry_KeyDown);
			// 
			// buttonConsoleTextSend
			// 
			this.buttonConsoleTextSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonConsoleTextSend.AutoSize = true;
			this.buttonConsoleTextSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonConsoleTextSend.Location = new System.Drawing.Point(628, 3);
			this.buttonConsoleTextSend.Name = "buttonConsoleTextSend";
			this.buttonConsoleTextSend.Size = new System.Drawing.Size(66, 26);
			this.buttonConsoleTextSend.TabIndex = 1;
			this.buttonConsoleTextSend.TabStop = false;
			this.buttonConsoleTextSend.Text = "Send";
			this.buttonConsoleTextSend.UseVisualStyleBackColor = true;
			this.buttonConsoleTextSend.Click += new System.EventHandler(this.buttonConsoleTextSend_Click);
			// 
			// menuBarApp
			// 
			this.menuBarApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuButtonFile});
			this.menuBarApp.Location = new System.Drawing.Point(0, 0);
			this.menuBarApp.Name = "menuBarApp";
			this.menuBarApp.Size = new System.Drawing.Size(761, 24);
			this.menuBarApp.TabIndex = 3;
			this.menuBarApp.Text = "menuStrip1";
			// 
			// menuButtonFile
			// 
			this.menuButtonFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManageServers,
            this.menuItemSettings,
            this.menuItemCommands,
            this.menuItemRetryConnection,
            this.menuItemAbout,
            this.menuItemExit});
			this.menuButtonFile.Name = "menuButtonFile";
			this.menuButtonFile.Size = new System.Drawing.Size(37, 20);
			this.menuButtonFile.Text = "File";
			// 
			// menuItemManageServers
			// 
			this.menuItemManageServers.Name = "menuItemManageServers";
			this.menuItemManageServers.Size = new System.Drawing.Size(180, 22);
			this.menuItemManageServers.Text = "Manage Servers";
			this.menuItemManageServers.Click += new System.EventHandler(this.menuItemManageServers_Click);
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPlaySoundOnPlayerJoin,
            this.toolStripMenuItemPlaySoundOnPlayerLeave,
            this.toolStripMenuItemChangeScoreboardFontSize,
            this.toolStripMenuItemConfigureSettings});
			this.menuItemSettings.Name = "menuItemSettings";
			this.menuItemSettings.Size = new System.Drawing.Size(180, 22);
			this.menuItemSettings.Text = "Settings";
			// 
			// toolStripMenuItemConfigureSettings
			// 
			this.toolStripMenuItemConfigureSettings.Name = "toolStripMenuItemConfigureSettings";
			this.toolStripMenuItemConfigureSettings.Size = new System.Drawing.Size(230, 22);
			this.toolStripMenuItemConfigureSettings.Text = "Configure Votefile Integration";
			this.toolStripMenuItemConfigureSettings.Click += new System.EventHandler(this.toolStripMenuItemConfigureSettings_Click);
			// 
			// toolStripMenuItemPlaySoundOnPlayerJoin
			// 
			this.toolStripMenuItemPlaySoundOnPlayerJoin.CheckOnClick = true;
			this.toolStripMenuItemPlaySoundOnPlayerJoin.Name = "toolStripMenuItemPlaySoundOnPlayerJoin";
			this.toolStripMenuItemPlaySoundOnPlayerJoin.Size = new System.Drawing.Size(230, 22);
			this.toolStripMenuItemPlaySoundOnPlayerJoin.Text = "Play Sound On Player Join";
			this.toolStripMenuItemPlaySoundOnPlayerJoin.Click += new System.EventHandler(this.toolStripMenuItemPlaySoundOnPlayerJoin_Click);
			// 
			// toolStripMenuItemPlaySoundOnPlayerLeave
			// 
			this.toolStripMenuItemPlaySoundOnPlayerLeave.CheckOnClick = true;
			this.toolStripMenuItemPlaySoundOnPlayerLeave.Name = "toolStripMenuItemPlaySoundOnPlayerLeave";
			this.toolStripMenuItemPlaySoundOnPlayerLeave.Size = new System.Drawing.Size(230, 22);
			this.toolStripMenuItemPlaySoundOnPlayerLeave.Text = "Play Sound On Player Leave";
			this.toolStripMenuItemPlaySoundOnPlayerLeave.Click += new System.EventHandler(this.toolStripMenuItemPlaySoundOnPlayerLeave_Click);
			// 
			// toolStripMenuItemChangeScoreboardFontSize
			// 
			this.toolStripMenuItemChangeScoreboardFontSize.Name = "toolStripMenuItemChangeScoreboardFontSize";
			this.toolStripMenuItemChangeScoreboardFontSize.Size = new System.Drawing.Size(230, 22);
			this.toolStripMenuItemChangeScoreboardFontSize.Text = "Change Scoreboard Font Size";
			this.toolStripMenuItemChangeScoreboardFontSize.Click += new System.EventHandler(this.toolStripMenuItemChangeScoreboardFontSize_Click);
			// 
			// menuItemCommands
			// 
			this.menuItemCommands.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManageConditionalCommands});
			this.menuItemCommands.Name = "menuItemCommands";
			this.menuItemCommands.Size = new System.Drawing.Size(180, 22);
			this.menuItemCommands.Text = "Commands";
			// 
			// menuItemManageConditionalCommands
			// 
			this.menuItemManageConditionalCommands.Name = "menuItemManageConditionalCommands";
			this.menuItemManageConditionalCommands.Size = new System.Drawing.Size(182, 22);
			this.menuItemManageConditionalCommands.Text = "Manage Commands";
			this.menuItemManageConditionalCommands.Click += new System.EventHandler(this.menuItemManageCommands_Click);
			// 
			// menuItemRetryConnection
			// 
			this.menuItemRetryConnection.Name = "menuItemRetryConnection";
			this.menuItemRetryConnection.Size = new System.Drawing.Size(180, 22);
			this.menuItemRetryConnection.Text = "Retry Connection";
			this.menuItemRetryConnection.Click += new System.EventHandler(this.menuItemRetryConnection_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Name = "menuItemAbout";
			this.menuItemAbout.Size = new System.Drawing.Size(180, 22);
			this.menuItemAbout.Text = "About";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// menuItemExit
			// 
			this.menuItemExit.Name = "menuItemExit";
			this.menuItemExit.Size = new System.Drawing.Size(180, 22);
			this.menuItemExit.Text = "Exit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// tabControlServerInterfaces
			// 
			this.tabControlServerInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlServerInterfaces.Controls.Add(this.tabPageConsole);
			this.tabControlServerInterfaces.Controls.Add(this.tabPageChat);
			this.tabControlServerInterfaces.Controls.Add(this.tabPageInfo);
			this.tabControlServerInterfaces.Controls.Add(this.tabPageControls);
			this.tabControlServerInterfaces.Controls.Add(this.tabPageJoinLeaveLog);
			this.tabControlServerInterfaces.Controls.Add(this.tabPageAppLog);
			this.tabControlServerInterfaces.Location = new System.Drawing.Point(0, 448);
			this.tabControlServerInterfaces.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabControlServerInterfaces.Name = "tabControlServerInterfaces";
			this.tabControlServerInterfaces.SelectedIndex = 0;
			this.tabControlServerInterfaces.Size = new System.Drawing.Size(761, 291);
			this.tabControlServerInterfaces.TabIndex = 4;
			// 
			// tabPageConsole
			// 
			this.tabPageConsole.Controls.Add(this.panelConsoleTextEntry);
			this.tabPageConsole.Controls.Add(this.textBoxConsoleText);
			this.tabPageConsole.Location = new System.Drawing.Point(4, 22);
			this.tabPageConsole.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPageConsole.Name = "tabPageConsole";
			this.tabPageConsole.Size = new System.Drawing.Size(753, 265);
			this.tabPageConsole.TabIndex = 0;
			this.tabPageConsole.Text = "Console";
			this.tabPageConsole.UseVisualStyleBackColor = true;
			// 
			// panelConsoleTextEntry
			// 
			this.panelConsoleTextEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelConsoleTextEntry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelConsoleTextEntry.Controls.Add(this.textBoxConsoleTextEntry);
			this.panelConsoleTextEntry.Controls.Add(this.buttonConsoleTextSend);
			this.panelConsoleTextEntry.Controls.Add(this.buttonConsoleTextClear);
			this.panelConsoleTextEntry.Location = new System.Drawing.Point(0, 233);
			this.panelConsoleTextEntry.Margin = new System.Windows.Forms.Padding(0);
			this.panelConsoleTextEntry.Name = "panelConsoleTextEntry";
			this.panelConsoleTextEntry.Size = new System.Drawing.Size(753, 32);
			this.panelConsoleTextEntry.TabIndex = 11;
			// 
			// buttonConsoleTextClear
			// 
			this.buttonConsoleTextClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonConsoleTextClear.AutoSize = true;
			this.buttonConsoleTextClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.buttonConsoleTextClear.Location = new System.Drawing.Point(700, 3);
			this.buttonConsoleTextClear.Name = "buttonConsoleTextClear";
			this.buttonConsoleTextClear.Size = new System.Drawing.Size(53, 26);
			this.buttonConsoleTextClear.TabIndex = 6;
			this.buttonConsoleTextClear.TabStop = false;
			this.buttonConsoleTextClear.Text = "Clear";
			this.buttonConsoleTextClear.UseVisualStyleBackColor = true;
			this.buttonConsoleTextClear.Click += new System.EventHandler(this.buttonConsoleTextClear_Click);
			// 
			// textBoxConsoleText
			// 
			this.textBoxConsoleText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxConsoleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxConsoleText.Location = new System.Drawing.Point(0, 0);
			this.textBoxConsoleText.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxConsoleText.MaxLength = 52767;
			this.textBoxConsoleText.Multiline = true;
			this.textBoxConsoleText.Name = "textBoxConsoleText";
			this.textBoxConsoleText.ReadOnly = true;
			this.textBoxConsoleText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxConsoleText.Size = new System.Drawing.Size(753, 233);
			this.textBoxConsoleText.TabIndex = 2;
			this.textBoxConsoleText.TabStop = false;
			this.textBoxConsoleText.TextChanged += new System.EventHandler(this.textBoxConsoleText_TextChanged);
			// 
			// tabPageChat
			// 
			this.tabPageChat.Controls.Add(this.panelChatTextEntry);
			this.tabPageChat.Controls.Add(this.textBoxChatText);
			this.tabPageChat.Location = new System.Drawing.Point(4, 22);
			this.tabPageChat.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPageChat.Name = "tabPageChat";
			this.tabPageChat.Size = new System.Drawing.Size(753, 265);
			this.tabPageChat.TabIndex = 1;
			this.tabPageChat.Text = "Chat";
			this.tabPageChat.UseVisualStyleBackColor = true;
			// 
			// panelChatTextEntry
			// 
			this.panelChatTextEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelChatTextEntry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelChatTextEntry.Controls.Add(this.textBoxChatTextEntry);
			this.panelChatTextEntry.Controls.Add(this.buttonChatTextSend);
			this.panelChatTextEntry.Controls.Add(this.buttonChatTextClear);
			this.panelChatTextEntry.Location = new System.Drawing.Point(0, 233);
			this.panelChatTextEntry.Margin = new System.Windows.Forms.Padding(0);
			this.panelChatTextEntry.Name = "panelChatTextEntry";
			this.panelChatTextEntry.Size = new System.Drawing.Size(753, 32);
			this.panelChatTextEntry.TabIndex = 8;
			// 
			// textBoxChatTextEntry
			// 
			this.textBoxChatTextEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxChatTextEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.textBoxChatTextEntry.Location = new System.Drawing.Point(0, 3);
			this.textBoxChatTextEntry.Name = "textBoxChatTextEntry";
			this.textBoxChatTextEntry.Size = new System.Drawing.Size(622, 26);
			this.textBoxChatTextEntry.TabIndex = 0;
			this.textBoxChatTextEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChatTextEntry_KeyDown);
			// 
			// buttonChatTextSend
			// 
			this.buttonChatTextSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonChatTextSend.AutoSize = true;
			this.buttonChatTextSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonChatTextSend.Location = new System.Drawing.Point(628, 3);
			this.buttonChatTextSend.Name = "buttonChatTextSend";
			this.buttonChatTextSend.Size = new System.Drawing.Size(66, 26);
			this.buttonChatTextSend.TabIndex = 1;
			this.buttonChatTextSend.Text = "Send";
			this.buttonChatTextSend.UseVisualStyleBackColor = true;
			this.buttonChatTextSend.Click += new System.EventHandler(this.buttonChatTextSend_Click);
			// 
			// buttonChatTextClear
			// 
			this.buttonChatTextClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonChatTextClear.AutoSize = true;
			this.buttonChatTextClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.buttonChatTextClear.Location = new System.Drawing.Point(700, 3);
			this.buttonChatTextClear.Name = "buttonChatTextClear";
			this.buttonChatTextClear.Size = new System.Drawing.Size(53, 26);
			this.buttonChatTextClear.TabIndex = 6;
			this.buttonChatTextClear.Text = "Clear";
			this.buttonChatTextClear.UseVisualStyleBackColor = true;
			this.buttonChatTextClear.Click += new System.EventHandler(this.buttonChatTextClear_Click);
			// 
			// textBoxChatText
			// 
			this.textBoxChatText.AcceptsReturn = true;
			this.textBoxChatText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxChatText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxChatText.Location = new System.Drawing.Point(0, 0);
			this.textBoxChatText.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxChatText.MaxLength = 52767;
			this.textBoxChatText.Multiline = true;
			this.textBoxChatText.Name = "textBoxChatText";
			this.textBoxChatText.ReadOnly = true;
			this.textBoxChatText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxChatText.Size = new System.Drawing.Size(753, 233);
			this.textBoxChatText.TabIndex = 5;
			this.textBoxChatText.TextChanged += new System.EventHandler(this.textBoxChatText_TextChanged);
			// 
			// tabPageInfo
			// 
			this.tabPageInfo.Controls.Add(this.panelInfo);
			this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
			this.tabPageInfo.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPageInfo.Name = "tabPageInfo";
			this.tabPageInfo.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.tabPageInfo.Size = new System.Drawing.Size(753, 265);
			this.tabPageInfo.TabIndex = 2;
			this.tabPageInfo.Text = "Info";
			this.tabPageInfo.UseVisualStyleBackColor = true;
			// 
			// panelInfo
			// 
			this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelInfo.AutoScroll = true;
			this.panelInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelInfo.Controls.Add(this.labelServerName);
			this.panelInfo.Controls.Add(this.labelHost);
			this.panelInfo.Controls.Add(this.labelSprintEnabled);
			this.panelInfo.Controls.Add(this.labelAssassinations);
			this.panelInfo.Controls.Add(this.labelMap);
			this.panelInfo.Controls.Add(this.labelVariant);
			this.panelInfo.Controls.Add(this.labelVariantType);
			this.panelInfo.Controls.Add(this.labelStatus);
			this.panelInfo.Controls.Add(this.labelPlayers);
			this.panelInfo.Controls.Add(this.labelVersion);
			this.panelInfo.Location = new System.Drawing.Point(0, 0);
			this.panelInfo.Margin = new System.Windows.Forms.Padding(0);
			this.panelInfo.Name = "panelInfo";
			this.panelInfo.Padding = new System.Windows.Forms.Padding(3);
			this.panelInfo.Size = new System.Drawing.Size(736, 265);
			this.panelInfo.TabIndex = 12;
			// 
			// labelServerName
			// 
			this.labelServerName.AutoSize = true;
			this.labelServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelServerName.Location = new System.Drawing.Point(3, 3);
			this.labelServerName.Name = "labelServerName";
			this.labelServerName.Size = new System.Drawing.Size(55, 20);
			this.labelServerName.TabIndex = 0;
			this.labelServerName.Text = "Name:";
			// 
			// labelHost
			// 
			this.labelHost.AutoSize = true;
			this.labelHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelHost.Location = new System.Drawing.Point(3, 23);
			this.labelHost.Name = "labelHost";
			this.labelHost.Size = new System.Drawing.Size(51, 20);
			this.labelHost.TabIndex = 1;
			this.labelHost.Text = "Host: ";
			// 
			// labelSprintEnabled
			// 
			this.labelSprintEnabled.AutoSize = true;
			this.labelSprintEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelSprintEnabled.Location = new System.Drawing.Point(3, 43);
			this.labelSprintEnabled.Name = "labelSprintEnabled";
			this.labelSprintEnabled.Size = new System.Drawing.Size(122, 20);
			this.labelSprintEnabled.TabIndex = 2;
			this.labelSprintEnabled.Text = "Sprint Enabled: ";
			// 
			// labelAssassinations
			// 
			this.labelAssassinations.AutoSize = true;
			this.labelAssassinations.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelAssassinations.Location = new System.Drawing.Point(3, 63);
			this.labelAssassinations.Name = "labelAssassinations";
			this.labelAssassinations.Size = new System.Drawing.Size(124, 20);
			this.labelAssassinations.TabIndex = 3;
			this.labelAssassinations.Text = "Assassinations: ";
			// 
			// labelMap
			// 
			this.labelMap.AutoSize = true;
			this.labelMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelMap.Location = new System.Drawing.Point(3, 82);
			this.labelMap.Name = "labelMap";
			this.labelMap.Size = new System.Drawing.Size(48, 20);
			this.labelMap.TabIndex = 5;
			this.labelMap.Text = "Map: ";
			// 
			// labelVariant
			// 
			this.labelVariant.AutoSize = true;
			this.labelVariant.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVariant.Location = new System.Drawing.Point(3, 102);
			this.labelVariant.Name = "labelVariant";
			this.labelVariant.Size = new System.Drawing.Size(68, 20);
			this.labelVariant.TabIndex = 6;
			this.labelVariant.Text = "Variant: ";
			// 
			// labelVariantType
			// 
			this.labelVariantType.AutoSize = true;
			this.labelVariantType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVariantType.Location = new System.Drawing.Point(3, 122);
			this.labelVariantType.Name = "labelVariantType";
			this.labelVariantType.Size = new System.Drawing.Size(98, 20);
			this.labelVariantType.TabIndex = 7;
			this.labelVariantType.Text = "Variant Type";
			// 
			// labelStatus
			// 
			this.labelStatus.AutoSize = true;
			this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelStatus.Location = new System.Drawing.Point(3, 142);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(64, 20);
			this.labelStatus.TabIndex = 8;
			this.labelStatus.Text = "Status: ";
			// 
			// labelPlayers
			// 
			this.labelPlayers.AutoSize = true;
			this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayers.Location = new System.Drawing.Point(3, 162);
			this.labelPlayers.Name = "labelPlayers";
			this.labelPlayers.Size = new System.Drawing.Size(68, 20);
			this.labelPlayers.TabIndex = 9;
			this.labelPlayers.Text = "Players: ";
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVersion.Location = new System.Drawing.Point(3, 182);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(71, 20);
			this.labelVersion.TabIndex = 10;
			this.labelVersion.Text = "Version: ";
			// 
			// tabPageControls
			// 
			this.tabPageControls.AutoScroll = true;
			this.tabPageControls.Controls.Add(this.buttonReloadVetoJson);
			this.tabPageControls.Controls.Add(this.buttonShuffleTeams);
			this.tabPageControls.Controls.Add(this.buttonReloadVotingJson);
			this.tabPageControls.Controls.Add(this.buttonDisableTeamShuffle);
			this.tabPageControls.Controls.Add(this.buttonEnableTeamShuffle);
			this.tabPageControls.Controls.Add(this.buttonSetMaxPlayers);
			this.tabPageControls.Controls.Add(this.textBoxMaxPlayersCount);
			this.tabPageControls.Controls.Add(this.buttonEnableAssasinations);
			this.tabPageControls.Controls.Add(this.buttonEnableUnlimitedSprint);
			this.tabPageControls.Controls.Add(this.buttonEnableSprint);
			this.tabPageControls.Controls.Add(this.buttonStopGame);
			this.tabPageControls.Controls.Add(this.buttonStartGame);
			this.tabPageControls.Location = new System.Drawing.Point(4, 22);
			this.tabPageControls.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPageControls.Name = "tabPageControls";
			this.tabPageControls.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageControls.Size = new System.Drawing.Size(753, 265);
			this.tabPageControls.TabIndex = 3;
			this.tabPageControls.Text = "Controls";
			this.tabPageControls.UseVisualStyleBackColor = true;
			// 
			// buttonReloadVetoJson
			// 
			this.buttonReloadVetoJson.Location = new System.Drawing.Point(168, 35);
			this.buttonReloadVetoJson.Name = "buttonReloadVetoJson";
			this.buttonReloadVetoJson.Size = new System.Drawing.Size(156, 23);
			this.buttonReloadVetoJson.TabIndex = 11;
			this.buttonReloadVetoJson.Text = "Reload Veto Json";
			this.buttonReloadVetoJson.UseVisualStyleBackColor = true;
			this.buttonReloadVetoJson.Click += new System.EventHandler(this.buttonReloadVetoJson_Click);
			// 
			// buttonShuffleTeams
			// 
			this.buttonShuffleTeams.Location = new System.Drawing.Point(168, 64);
			this.buttonShuffleTeams.Name = "buttonShuffleTeams";
			this.buttonShuffleTeams.Size = new System.Drawing.Size(156, 23);
			this.buttonShuffleTeams.TabIndex = 10;
			this.buttonShuffleTeams.Text = "Shuffle Teams";
			this.buttonShuffleTeams.UseVisualStyleBackColor = true;
			this.buttonShuffleTeams.Click += new System.EventHandler(this.buttonShuffleTeams_Click);
			// 
			// buttonReloadVotingJson
			// 
			this.buttonReloadVotingJson.Location = new System.Drawing.Point(168, 6);
			this.buttonReloadVotingJson.Name = "buttonReloadVotingJson";
			this.buttonReloadVotingJson.Size = new System.Drawing.Size(156, 23);
			this.buttonReloadVotingJson.TabIndex = 9;
			this.buttonReloadVotingJson.Text = "Reload Voting Json";
			this.buttonReloadVotingJson.UseVisualStyleBackColor = true;
			this.buttonReloadVotingJson.Click += new System.EventHandler(this.buttonReloadVotingJson_Click);
			// 
			// buttonDisableTeamShuffle
			// 
			this.buttonDisableTeamShuffle.Location = new System.Drawing.Point(6, 180);
			this.buttonDisableTeamShuffle.Name = "buttonDisableTeamShuffle";
			this.buttonDisableTeamShuffle.Size = new System.Drawing.Size(156, 23);
			this.buttonDisableTeamShuffle.TabIndex = 8;
			this.buttonDisableTeamShuffle.Text = "Disable Team Shuffle";
			this.buttonDisableTeamShuffle.UseVisualStyleBackColor = true;
			this.buttonDisableTeamShuffle.Click += new System.EventHandler(this.buttonDisableTeamShuffle_Click);
			// 
			// buttonEnableTeamShuffle
			// 
			this.buttonEnableTeamShuffle.Location = new System.Drawing.Point(6, 151);
			this.buttonEnableTeamShuffle.Name = "buttonEnableTeamShuffle";
			this.buttonEnableTeamShuffle.Size = new System.Drawing.Size(156, 23);
			this.buttonEnableTeamShuffle.TabIndex = 7;
			this.buttonEnableTeamShuffle.Text = "Enable Team Shuffle";
			this.buttonEnableTeamShuffle.UseVisualStyleBackColor = true;
			this.buttonEnableTeamShuffle.Click += new System.EventHandler(this.buttonEnableTeamShuffle_Click);
			// 
			// buttonSetMaxPlayers
			// 
			this.buttonSetMaxPlayers.Location = new System.Drawing.Point(67, 122);
			this.buttonSetMaxPlayers.Name = "buttonSetMaxPlayers";
			this.buttonSetMaxPlayers.Size = new System.Drawing.Size(95, 23);
			this.buttonSetMaxPlayers.TabIndex = 6;
			this.buttonSetMaxPlayers.Text = "Set Max Players";
			this.buttonSetMaxPlayers.UseVisualStyleBackColor = true;
			this.buttonSetMaxPlayers.Click += new System.EventHandler(this.buttonSetMaxPlayers_Click);
			// 
			// textBoxMaxPlayersCount
			// 
			this.textBoxMaxPlayersCount.Location = new System.Drawing.Point(8, 124);
			this.textBoxMaxPlayersCount.Name = "textBoxMaxPlayersCount";
			this.textBoxMaxPlayersCount.Size = new System.Drawing.Size(55, 20);
			this.textBoxMaxPlayersCount.TabIndex = 5;
			// 
			// buttonEnableAssasinations
			// 
			this.buttonEnableAssasinations.Location = new System.Drawing.Point(6, 93);
			this.buttonEnableAssasinations.Name = "buttonEnableAssasinations";
			this.buttonEnableAssasinations.Size = new System.Drawing.Size(156, 23);
			this.buttonEnableAssasinations.TabIndex = 4;
			this.buttonEnableAssasinations.Text = "Enable Assassinations";
			this.buttonEnableAssasinations.UseVisualStyleBackColor = true;
			this.buttonEnableAssasinations.Click += new System.EventHandler(this.buttonEnableAssasinations_Click);
			// 
			// buttonEnableUnlimitedSprint
			// 
			this.buttonEnableUnlimitedSprint.Location = new System.Drawing.Point(6, 64);
			this.buttonEnableUnlimitedSprint.Name = "buttonEnableUnlimitedSprint";
			this.buttonEnableUnlimitedSprint.Size = new System.Drawing.Size(156, 23);
			this.buttonEnableUnlimitedSprint.TabIndex = 3;
			this.buttonEnableUnlimitedSprint.Text = "Enable Unlimited Sprint";
			this.buttonEnableUnlimitedSprint.UseVisualStyleBackColor = true;
			this.buttonEnableUnlimitedSprint.Click += new System.EventHandler(this.buttonEnableUnlimitedSprint_Click);
			// 
			// buttonEnableSprint
			// 
			this.buttonEnableSprint.Location = new System.Drawing.Point(6, 35);
			this.buttonEnableSprint.Name = "buttonEnableSprint";
			this.buttonEnableSprint.Size = new System.Drawing.Size(156, 23);
			this.buttonEnableSprint.TabIndex = 2;
			this.buttonEnableSprint.Text = "Enable Sprint";
			this.buttonEnableSprint.UseVisualStyleBackColor = true;
			this.buttonEnableSprint.Click += new System.EventHandler(this.buttonEnableSprint_Click);
			// 
			// buttonStopGame
			// 
			this.buttonStopGame.Location = new System.Drawing.Point(87, 6);
			this.buttonStopGame.Name = "buttonStopGame";
			this.buttonStopGame.Size = new System.Drawing.Size(75, 23);
			this.buttonStopGame.TabIndex = 1;
			this.buttonStopGame.Text = "Stop Game";
			this.buttonStopGame.UseVisualStyleBackColor = true;
			this.buttonStopGame.Click += new System.EventHandler(this.buttonStopGame_Click);
			// 
			// buttonStartGame
			// 
			this.buttonStartGame.Location = new System.Drawing.Point(6, 6);
			this.buttonStartGame.Name = "buttonStartGame";
			this.buttonStartGame.Size = new System.Drawing.Size(75, 23);
			this.buttonStartGame.TabIndex = 0;
			this.buttonStartGame.Text = "Start Game";
			this.buttonStartGame.UseVisualStyleBackColor = true;
			this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
			// 
			// tabPageJoinLeaveLog
			// 
			this.tabPageJoinLeaveLog.Controls.Add(this.textBoxJoinLeaveLog);
			this.tabPageJoinLeaveLog.Location = new System.Drawing.Point(4, 22);
			this.tabPageJoinLeaveLog.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPageJoinLeaveLog.Name = "tabPageJoinLeaveLog";
			this.tabPageJoinLeaveLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageJoinLeaveLog.Size = new System.Drawing.Size(753, 265);
			this.tabPageJoinLeaveLog.TabIndex = 4;
			this.tabPageJoinLeaveLog.Text = "Join/Leave Log";
			this.tabPageJoinLeaveLog.UseVisualStyleBackColor = true;
			// 
			// textBoxJoinLeaveLog
			// 
			this.textBoxJoinLeaveLog.AcceptsReturn = true;
			this.textBoxJoinLeaveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxJoinLeaveLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxJoinLeaveLog.Location = new System.Drawing.Point(0, 0);
			this.textBoxJoinLeaveLog.MaxLength = 52767;
			this.textBoxJoinLeaveLog.Multiline = true;
			this.textBoxJoinLeaveLog.Name = "textBoxJoinLeaveLog";
			this.textBoxJoinLeaveLog.ReadOnly = true;
			this.textBoxJoinLeaveLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxJoinLeaveLog.Size = new System.Drawing.Size(753, 265);
			this.textBoxJoinLeaveLog.TabIndex = 6;
			// 
			// tabPageAppLog
			// 
			this.tabPageAppLog.Controls.Add(this.textBoxAppLog);
			this.tabPageAppLog.Location = new System.Drawing.Point(4, 22);
			this.tabPageAppLog.Name = "tabPageAppLog";
			this.tabPageAppLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAppLog.Size = new System.Drawing.Size(753, 265);
			this.tabPageAppLog.TabIndex = 5;
			this.tabPageAppLog.Text = "Application Log";
			this.tabPageAppLog.UseVisualStyleBackColor = true;
			// 
			// textBoxAppLog
			// 
			this.textBoxAppLog.AcceptsReturn = true;
			this.textBoxAppLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAppLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxAppLog.Location = new System.Drawing.Point(0, 0);
			this.textBoxAppLog.MaxLength = 52767;
			this.textBoxAppLog.Multiline = true;
			this.textBoxAppLog.Name = "textBoxAppLog";
			this.textBoxAppLog.ReadOnly = true;
			this.textBoxAppLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxAppLog.Size = new System.Drawing.Size(753, 265);
			this.textBoxAppLog.TabIndex = 7;
			// 
			// statusStripStatusInformation
			// 
			this.statusStripStatusInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statusStripStatusInformation.AutoSize = false;
			this.statusStripStatusInformation.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStripStatusInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonServerSelect,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelRconConnection,
            this.toolStripStatusLabelStatsConnection,
            this.toolStripSplitButtonAutoUpdate});
			this.statusStripStatusInformation.Location = new System.Drawing.Point(0, 737);
			this.statusStripStatusInformation.Name = "statusStripStatusInformation";
			this.statusStripStatusInformation.Size = new System.Drawing.Size(605, 24);
			this.statusStripStatusInformation.SizingGrip = false;
			this.statusStripStatusInformation.TabIndex = 5;
			// 
			// toolStripSplitButtonServerSelect
			// 
			this.toolStripSplitButtonServerSelect.Image = global::RconTool.Properties.Resources.Icon_Server;
			this.toolStripSplitButtonServerSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripSplitButtonServerSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonServerSelect.Name = "toolStripSplitButtonServerSelect";
			this.toolStripSplitButtonServerSelect.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripSplitButtonServerSelect.Size = new System.Drawing.Size(105, 22);
			this.toolStripSplitButtonServerSelect.Text = "Select Server";
			this.toolStripSplitButtonServerSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripSplitButtonServerSelect.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripSplitButtonServerSelect_DropDownItemClicked);
			this.toolStripSplitButtonServerSelect.Click += new System.EventHandler(this.toolStripSplitButtonServerSelect_Click);
			// 
			// toolStripStatusLabelStatus
			// 
			this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
			this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(51, 19);
			this.toolStripStatusLabelStatus.Text = " Status ⎮";
			// 
			// toolStripStatusLabelRconConnection
			// 
			this.toolStripStatusLabelRconConnection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabelRconConnection.Image")));
			this.toolStripStatusLabelRconConnection.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
			this.toolStripStatusLabelRconConnection.Name = "toolStripStatusLabelRconConnection";
			this.toolStripStatusLabelRconConnection.Size = new System.Drawing.Size(53, 19);
			this.toolStripStatusLabelRconConnection.Text = "Rcon:";
			this.toolStripStatusLabelRconConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabelRconConnection.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			// 
			// toolStripStatusLabelStatsConnection
			// 
			this.toolStripStatusLabelStatsConnection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabelStatsConnection.Image")));
			this.toolStripStatusLabelStatsConnection.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
			this.toolStripStatusLabelStatsConnection.Name = "toolStripStatusLabelStatsConnection";
			this.toolStripStatusLabelStatsConnection.Size = new System.Drawing.Size(51, 19);
			this.toolStripStatusLabelStatsConnection.Text = "Stats:";
			this.toolStripStatusLabelStatsConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabelStatsConnection.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			// 
			// toolStripSplitButtonAutoUpdate
			// 
			this.toolStripSplitButtonAutoUpdate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAutoUpdateEnabled,
            this.toolStripMenuItemAutoUpdateDisabled});
			this.toolStripSplitButtonAutoUpdate.Image = global::RconTool.Properties.Resources.Image_CheckMark32x32;
			this.toolStripSplitButtonAutoUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonAutoUpdate.Name = "toolStripSplitButtonAutoUpdate";
			this.toolStripSplitButtonAutoUpdate.Size = new System.Drawing.Size(108, 22);
			this.toolStripSplitButtonAutoUpdate.Text = "Auto-Update";
			this.toolStripSplitButtonAutoUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.toolStripSplitButtonAutoUpdate.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripSplitButtonAutoUpdate_DropDownItemClicked);
			this.toolStripSplitButtonAutoUpdate.Click += new System.EventHandler(this.toolStripSplitButtonAutoUpdate_Click);
			// 
			// toolStripMenuItemAutoUpdateEnabled
			// 
			this.toolStripMenuItemAutoUpdateEnabled.Checked = true;
			this.toolStripMenuItemAutoUpdateEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItemAutoUpdateEnabled.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripMenuItemAutoUpdateEnabled.Image = global::RconTool.Properties.Resources.Image_CheckMark32x32;
			this.toolStripMenuItemAutoUpdateEnabled.Name = "toolStripMenuItemAutoUpdateEnabled";
			this.toolStripMenuItemAutoUpdateEnabled.Size = new System.Drawing.Size(119, 22);
			this.toolStripMenuItemAutoUpdateEnabled.Text = "Enabled";
			// 
			// toolStripMenuItemAutoUpdateDisabled
			// 
			this.toolStripMenuItemAutoUpdateDisabled.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripMenuItemAutoUpdateDisabled.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemAutoUpdateDisabled.Image")));
			this.toolStripMenuItemAutoUpdateDisabled.Name = "toolStripMenuItemAutoUpdateDisabled";
			this.toolStripMenuItemAutoUpdateDisabled.Size = new System.Drawing.Size(119, 22);
			this.toolStripMenuItemAutoUpdateDisabled.Text = "Disabled";
			// 
			// statusStripVersion
			// 
			this.statusStripVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.statusStripVersion.AutoSize = false;
			this.statusStripVersion.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStripVersion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFontSizeDecrease,
            this.toolStripFontSizeIncrease,
            this.toolStripResizeToFitScoreboard,
            this.toolStripStatusLabelVersion});
			this.statusStripVersion.Location = new System.Drawing.Point(605, 737);
			this.statusStripVersion.Name = "statusStripVersion";
			this.statusStripVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusStripVersion.Size = new System.Drawing.Size(156, 24);
			this.statusStripVersion.TabIndex = 12;
			// 
			// toolStripFontSizeDecrease
			// 
			this.toolStripFontSizeDecrease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripFontSizeDecrease.DropDownButtonWidth = 0;
			this.toolStripFontSizeDecrease.Image = global::RconTool.Properties.Resources.Icon_TriangleDown;
			this.toolStripFontSizeDecrease.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripFontSizeDecrease.Name = "toolStripFontSizeDecrease";
			this.toolStripFontSizeDecrease.Size = new System.Drawing.Size(21, 22);
			this.toolStripFontSizeDecrease.ToolTipText = "Decrease Font Size";
			this.toolStripFontSizeDecrease.ButtonClick += new System.EventHandler(this.toolStripFontSizeDecrease_ButtonClick);
			// 
			// toolStripFontSizeIncrease
			// 
			this.toolStripFontSizeIncrease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripFontSizeIncrease.DropDownButtonWidth = 0;
			this.toolStripFontSizeIncrease.Image = global::RconTool.Properties.Resources.Icon_TriangleUp;
			this.toolStripFontSizeIncrease.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripFontSizeIncrease.Name = "toolStripFontSizeIncrease";
			this.toolStripFontSizeIncrease.Size = new System.Drawing.Size(21, 22);
			this.toolStripFontSizeIncrease.ToolTipText = "Increase Font Size";
			this.toolStripFontSizeIncrease.ButtonClick += new System.EventHandler(this.toolStripFontSizeIncrease_ButtonClick);
			// 
			// toolStripResizeToFitScoreboard
			// 
			this.toolStripResizeToFitScoreboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripResizeToFitScoreboard.DropDownButtonWidth = 0;
			this.toolStripResizeToFitScoreboard.Image = global::RconTool.Properties.Resources.Icon_DoubleArrowExpandListVertical;
			this.toolStripResizeToFitScoreboard.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripResizeToFitScoreboard.Name = "toolStripResizeToFitScoreboard";
			this.toolStripResizeToFitScoreboard.Size = new System.Drawing.Size(21, 22);
			this.toolStripResizeToFitScoreboard.ToolTipText = "Resize To Fit Scoreboard";
			this.toolStripResizeToFitScoreboard.ButtonClick += new System.EventHandler(this.toolStripResizeToFitScoreboard_ButtonClick);
			// 
			// toolStripStatusLabelVersion
			// 
			this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
			this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(75, 19);
			this.toolStripStatusLabelVersion.Text = "Version: 3.5.0";
			this.toolStripStatusLabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// App
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(761, 761);
			this.Controls.Add(this.menuBarApp);
			this.Controls.Add(this.tabControlServerInterfaces);
			this.Controls.Add(this.statusStripStatusInformation);
			this.Controls.Add(this.statusStripVersion);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuBarApp;
			this.MinimumSize = new System.Drawing.Size(777, 800);
			this.Name = "App";
			this.RightToLeftLayout = true;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Dedicated Rcon Tool";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formApp_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.formApp_FormClosed);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.App_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.App_MouseDown);
			this.MouseLeave += new System.EventHandler(this.App_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.App_MouseMove);
			this.menuBarApp.ResumeLayout(false);
			this.menuBarApp.PerformLayout();
			this.tabControlServerInterfaces.ResumeLayout(false);
			this.tabPageConsole.ResumeLayout(false);
			this.tabPageConsole.PerformLayout();
			this.panelConsoleTextEntry.ResumeLayout(false);
			this.panelConsoleTextEntry.PerformLayout();
			this.tabPageChat.ResumeLayout(false);
			this.tabPageChat.PerformLayout();
			this.panelChatTextEntry.ResumeLayout(false);
			this.panelChatTextEntry.PerformLayout();
			this.tabPageInfo.ResumeLayout(false);
			this.panelInfo.ResumeLayout(false);
			this.panelInfo.PerformLayout();
			this.tabPageControls.ResumeLayout(false);
			this.tabPageControls.PerformLayout();
			this.tabPageJoinLeaveLog.ResumeLayout(false);
			this.tabPageJoinLeaveLog.PerformLayout();
			this.tabPageAppLog.ResumeLayout(false);
			this.tabPageAppLog.PerformLayout();
			this.statusStripStatusInformation.ResumeLayout(false);
			this.statusStripStatusInformation.PerformLayout();
			this.statusStripVersion.ResumeLayout(false);
			this.statusStripVersion.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxConsoleTextEntry;
        private System.Windows.Forms.Button buttonConsoleTextSend;
        private System.Windows.Forms.MenuStrip menuBarApp;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.TabControl tabControlServerInterfaces;
        public System.Windows.Forms.TextBox textBoxConsoleText;
        private System.Windows.Forms.TabPage tabPageConsole;
        private System.Windows.Forms.TabPage tabPageChat;
        public System.Windows.Forms.TextBox textBoxChatText;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelVariantType;
        private System.Windows.Forms.Label labelVariant;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.Label labelAssassinations;
        private System.Windows.Forms.Label labelSprintEnabled;
        private System.Windows.Forms.StatusStrip statusStripStatusInformation;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRconConnection;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatsConnection;
        private System.Windows.Forms.TabPage tabPageControls;
        private System.Windows.Forms.Button buttonDisableTeamShuffle;
        private System.Windows.Forms.Button buttonEnableTeamShuffle;
        private System.Windows.Forms.Button buttonSetMaxPlayers;
        private System.Windows.Forms.TextBox textBoxMaxPlayersCount;
        private System.Windows.Forms.Button buttonEnableAssasinations;
        private System.Windows.Forms.Button buttonEnableUnlimitedSprint;
        private System.Windows.Forms.Button buttonStopGame;
        public System.Windows.Forms.Button buttonEnableSprint;
        public System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Button buttonConsoleTextClear;
        private System.Windows.Forms.TabPage tabPageJoinLeaveLog;
        public System.Windows.Forms.TextBox textBoxJoinLeaveLog;
        private System.Windows.Forms.Button buttonShuffleTeams;
        private System.Windows.Forms.Button buttonReloadVotingJson;
        private System.Windows.Forms.Button buttonReloadVetoJson;
		private System.Windows.Forms.TextBox textBoxChatTextEntry;
		private System.Windows.Forms.Button buttonChatTextSend;
		private System.Windows.Forms.Button buttonChatTextClear;
		private System.Windows.Forms.Panel panelConsoleTextEntry;
		private System.Windows.Forms.Panel panelChatTextEntry;
		private System.Windows.Forms.Panel panelInfo;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonAutoUpdate;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoUpdateEnabled;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoUpdateDisabled;
		private System.Windows.Forms.StatusStrip statusStripVersion;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonServerSelect;
		private System.Windows.Forms.TabPage tabPageAppLog;
		public System.Windows.Forms.TextBox textBoxAppLog;
		private System.Windows.Forms.ToolStripMenuItem menuItemRetryConnection;
		public System.Windows.Forms.ToolStripMenuItem menuButtonFile;
		public System.Windows.Forms.ToolStripMenuItem menuItemSettings;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaySoundOnPlayerJoin;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaySoundOnPlayerLeave;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConfigureSettings;
		public System.Windows.Forms.ToolStripMenuItem menuItemCommands;
		public System.Windows.Forms.ToolStripMenuItem menuItemManageConditionalCommands;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private System.Windows.Forms.ToolStripMenuItem menuItemManageServers;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeScoreboardFontSize;
		private System.Windows.Forms.ToolStripSplitButton toolStripResizeToFitScoreboard;
		private System.Windows.Forms.ToolStripSplitButton toolStripFontSizeIncrease;
		private System.Windows.Forms.ToolStripSplitButton toolStripFontSizeDecrease;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

