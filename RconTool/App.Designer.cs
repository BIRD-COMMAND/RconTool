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
			this.tabControlServerInterfaces = new System.Windows.Forms.TabControl();
			this.tabPageConsole = new System.Windows.Forms.TabPage();
			this.panelConsoleTextEntry = new System.Windows.Forms.Panel();
			this.textBoxAutoScrollConsoleText = new RconTool.TextBoxAutoScroll();
			this.buttonScrollLock = new System.Windows.Forms.Button();
			this.tabPageChat = new System.Windows.Forms.TabPage();
			this.panelChatTextEntry = new System.Windows.Forms.Panel();
			this.textBoxChatTextEntry = new System.Windows.Forms.TextBox();
			this.buttonChatTextSend = new System.Windows.Forms.Button();
			this.textBoxAutoScrollChatText = new RconTool.TextBoxAutoScroll();
			this.tabPagePlayerLog = new System.Windows.Forms.TabPage();
			this.textBoxAutoScrollPlayerLog = new RconTool.TextBoxAutoScroll();
			this.tabPageAppLog = new System.Windows.Forms.TabPage();
			this.textBoxAutoScrollApplicationLog = new RconTool.TextBoxAutoScroll();
			this.labelMap = new System.Windows.Forms.Label();
			this.labelVariant = new System.Windows.Forms.Label();
			this.labelPlayers = new System.Windows.Forms.Label();
			this.statusStripStatusInformation = new System.Windows.Forms.StatusStrip();
			this.toolStripDropDownButtonFileMenu = new System.Windows.Forms.ToolStripDropDownButton();
			this.fileMenuManageServersButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuSettingsItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuSettingsItemChangeScoreboardFontSizeButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuCommandsItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuCommandsItemManageCommandsButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuCommandsItemShuffleTeamsCommandToggle = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuCommandsItemKickPlayerCommandToggle = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuCommandsItemEndGameCommandToggle = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuTranslationItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuTranslationItemTranslationEnabledButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuTranslationItemSetGoogleTranslateAPIKeyButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuTranslationItemSetCharacterCountAndBillingCycleButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuTranslationItemTranslatedCharactersTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.fileMenuTranslationItemTranslationBillingCycleTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.fileMenuRetryConnectionButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuAboutButton = new System.Windows.Forms.ToolStripMenuItem();
			this.fileMenuExitButton = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSplitButtonServerSelect = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelRconConnection = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelStatsConnection = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripSplitButtonServerHook = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripSplitButtonAutoUpdate = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripMenuItemAutoUpdateEnabled = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemAutoUpdateDisabled = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStripVersion = new System.Windows.Forms.StatusStrip();
			this.toolStripFontSizeDecrease = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripFontSizeIncrease = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripResizeToFitScoreboard = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.comboBoxMaxPlayers = new System.Windows.Forms.ComboBox();
			this.buttonClearLog = new System.Windows.Forms.Button();
			this.textBoxServerNameEdit = new System.Windows.Forms.TextBox();
			this.statusStripServerCommandButtons = new System.Windows.Forms.StatusStrip();
			this.statusButtonStartGame = new System.Windows.Forms.ToolStripSplitButton();
			this.statusButtonStopGame = new System.Windows.Forms.ToolStripSplitButton();
			this.statusButtonReloadVoteOrVetoJSON = new System.Windows.Forms.ToolStripSplitButton();
			this.statusButtonShuffleTeams = new System.Windows.Forms.ToolStripSplitButton();
			this.statusButtonSprintToggle = new System.Windows.Forms.ToolStripSplitButton();
			this.statusButtonAssassinationToggle = new System.Windows.Forms.ToolStripSplitButton();
			this.panelPlayerCount = new System.Windows.Forms.Panel();
			this.labelVariantTypeIcon = new System.Windows.Forms.Label();
			this.labelPlayersOf = new System.Windows.Forms.Label();
			this.pictureBoxMapAndStatusOverlay = new System.Windows.Forms.PictureBox();
			this.labelMapVariantOn = new System.Windows.Forms.Label();
			this.pictureBoxGameVariantIcon = new System.Windows.Forms.PictureBox();
			this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.panelMapAndVariantDisplay = new System.Windows.Forms.Panel();
			this.toolStripMapSelect = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButtonGame = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButtonMap = new System.Windows.Forms.ToolStripDropDownButton();
			this.tableLayoutPanelMatchInfoAndServerName = new System.Windows.Forms.TableLayoutPanel();
			this.tabControlServerInterfaces.SuspendLayout();
			this.tabPageConsole.SuspendLayout();
			this.panelConsoleTextEntry.SuspendLayout();
			this.tabPageChat.SuspendLayout();
			this.panelChatTextEntry.SuspendLayout();
			this.tabPagePlayerLog.SuspendLayout();
			this.tabPageAppLog.SuspendLayout();
			this.statusStripStatusInformation.SuspendLayout();
			this.statusStripServerCommandButtons.SuspendLayout();
			this.panelPlayerCount.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMapAndStatusOverlay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameVariantIcon)).BeginInit();
			this.panelMapAndVariantDisplay.SuspendLayout();
			this.toolStripMapSelect.SuspendLayout();
			this.tableLayoutPanelMatchInfoAndServerName.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxConsoleTextEntry
			// 
			this.textBoxConsoleTextEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxConsoleTextEntry.Font = new System.Drawing.Font("Segoe UI", 10.25F);
			this.textBoxConsoleTextEntry.Location = new System.Drawing.Point(0, 3);
			this.textBoxConsoleTextEntry.Name = "textBoxConsoleTextEntry";
			this.textBoxConsoleTextEntry.Size = new System.Drawing.Size(692, 26);
			this.textBoxConsoleTextEntry.TabIndex = 1;
			this.textBoxConsoleTextEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxConsoleTextEntry_KeyDown);
			this.textBoxConsoleTextEntry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxConsoleTextEntry_KeyUp);
			// 
			// buttonConsoleTextSend
			// 
			this.buttonConsoleTextSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonConsoleTextSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonConsoleTextSend.Location = new System.Drawing.Point(698, 2);
			this.buttonConsoleTextSend.Name = "buttonConsoleTextSend";
			this.buttonConsoleTextSend.Size = new System.Drawing.Size(54, 28);
			this.buttonConsoleTextSend.TabIndex = 2;
			this.buttonConsoleTextSend.Text = "Send";
			this.buttonConsoleTextSend.UseVisualStyleBackColor = true;
			this.buttonConsoleTextSend.Click += new System.EventHandler(this.buttonConsoleTextSend_Click);
			// 
			// tabControlServerInterfaces
			// 
			this.tabControlServerInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlServerInterfaces.Controls.Add(this.tabPageConsole);
			this.tabControlServerInterfaces.Controls.Add(this.tabPageChat);
			this.tabControlServerInterfaces.Controls.Add(this.tabPagePlayerLog);
			this.tabControlServerInterfaces.Controls.Add(this.tabPageAppLog);
			this.tabControlServerInterfaces.Location = new System.Drawing.Point(0, 512);
			this.tabControlServerInterfaces.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabControlServerInterfaces.Name = "tabControlServerInterfaces";
			this.tabControlServerInterfaces.SelectedIndex = 0;
			this.tabControlServerInterfaces.Size = new System.Drawing.Size(761, 181);
			this.tabControlServerInterfaces.TabIndex = 4;
			this.tabControlServerInterfaces.TabStop = false;
			this.tabControlServerInterfaces.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlServerInterfaces_Selected);
			this.tabControlServerInterfaces.LocationChanged += new System.EventHandler(this.tabControlServerInterfaces_LocationChanged);
			this.tabControlServerInterfaces.SizeChanged += new System.EventHandler(this.tabControlServerInterfaces_SizeChanged);
			// 
			// tabPageConsole
			// 
			this.tabPageConsole.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageConsole.Controls.Add(this.panelConsoleTextEntry);
			this.tabPageConsole.Controls.Add(this.textBoxAutoScrollConsoleText);
			this.tabPageConsole.Location = new System.Drawing.Point(4, 24);
			this.tabPageConsole.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPageConsole.Name = "tabPageConsole";
			this.tabPageConsole.Size = new System.Drawing.Size(753, 153);
			this.tabPageConsole.TabIndex = 0;
			this.tabPageConsole.Text = "Console";
			// 
			// panelConsoleTextEntry
			// 
			this.panelConsoleTextEntry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelConsoleTextEntry.BackColor = System.Drawing.SystemColors.Control;
			this.panelConsoleTextEntry.Controls.Add(this.textBoxConsoleTextEntry);
			this.panelConsoleTextEntry.Controls.Add(this.buttonConsoleTextSend);
			this.panelConsoleTextEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelConsoleTextEntry.Location = new System.Drawing.Point(0, 121);
			this.panelConsoleTextEntry.Margin = new System.Windows.Forms.Padding(0);
			this.panelConsoleTextEntry.Name = "panelConsoleTextEntry";
			this.panelConsoleTextEntry.Size = new System.Drawing.Size(753, 32);
			this.panelConsoleTextEntry.TabIndex = 11;
			// 
			// textBoxAutoScrollConsoleText
			// 
			this.textBoxAutoScrollConsoleText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAutoScrollConsoleText.AutoScroll = true;
			this.textBoxAutoScrollConsoleText.ButtonAutoScrollToggle = this.buttonScrollLock;
			this.textBoxAutoScrollConsoleText.Font = new System.Drawing.Font("Segoe UI", 10.25F);
			this.textBoxAutoScrollConsoleText.Location = new System.Drawing.Point(0, 0);
			this.textBoxAutoScrollConsoleText.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxAutoScrollConsoleText.MaxLength = 52767;
			this.textBoxAutoScrollConsoleText.Name = "textBoxAutoScrollConsoleText";
			this.textBoxAutoScrollConsoleText.ReadOnly = true;
			this.textBoxAutoScrollConsoleText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBoxAutoScrollConsoleText.Size = new System.Drawing.Size(753, 121);
			this.textBoxAutoScrollConsoleText.TabIndex = 1;
			this.textBoxAutoScrollConsoleText.TabPage = this.tabPageConsole;
			this.textBoxAutoScrollConsoleText.TabStop = false;
			this.textBoxAutoScrollConsoleText.Text = "";
			// 
			// buttonScrollLock
			// 
			this.buttonScrollLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonScrollLock.BackColor = System.Drawing.SystemColors.Control;
			this.buttonScrollLock.BackgroundImage = global::RconTool.Properties.Resources.autoScrollButtonFade16x16;
			this.buttonScrollLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonScrollLock.FlatAppearance.BorderSize = 0;
			this.buttonScrollLock.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.buttonScrollLock.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
			this.buttonScrollLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonScrollLock.Location = new System.Drawing.Point(738, 516);
			this.buttonScrollLock.Name = "buttonScrollLock";
			this.buttonScrollLock.Size = new System.Drawing.Size(18, 18);
			this.buttonScrollLock.TabIndex = 0;
			this.buttonScrollLock.TabStop = false;
			this.toolTip1.SetToolTip(this.buttonScrollLock, "Auto-Scroll is currently enabled.\\nClick to disable Auto-Scroll.");
			this.buttonScrollLock.UseVisualStyleBackColor = false;
			// 
			// tabPageChat
			// 
			this.tabPageChat.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageChat.Controls.Add(this.panelChatTextEntry);
			this.tabPageChat.Controls.Add(this.textBoxAutoScrollChatText);
			this.tabPageChat.Location = new System.Drawing.Point(4, 24);
			this.tabPageChat.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPageChat.Name = "tabPageChat";
			this.tabPageChat.Size = new System.Drawing.Size(753, 153);
			this.tabPageChat.TabIndex = 1;
			this.tabPageChat.Text = "Chat";
			// 
			// panelChatTextEntry
			// 
			this.panelChatTextEntry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelChatTextEntry.BackColor = System.Drawing.SystemColors.Control;
			this.panelChatTextEntry.Controls.Add(this.textBoxChatTextEntry);
			this.panelChatTextEntry.Controls.Add(this.buttonChatTextSend);
			this.panelChatTextEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelChatTextEntry.Location = new System.Drawing.Point(0, 121);
			this.panelChatTextEntry.Margin = new System.Windows.Forms.Padding(0);
			this.panelChatTextEntry.Name = "panelChatTextEntry";
			this.panelChatTextEntry.Size = new System.Drawing.Size(753, 32);
			this.panelChatTextEntry.TabIndex = 8;
			// 
			// textBoxChatTextEntry
			// 
			this.textBoxChatTextEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxChatTextEntry.Font = new System.Drawing.Font("Segoe UI", 10.25F);
			this.textBoxChatTextEntry.Location = new System.Drawing.Point(0, 3);
			this.textBoxChatTextEntry.Name = "textBoxChatTextEntry";
			this.textBoxChatTextEntry.Size = new System.Drawing.Size(692, 26);
			this.textBoxChatTextEntry.TabIndex = 0;
			this.textBoxChatTextEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChatTextEntry_KeyDown);
			// 
			// buttonChatTextSend
			// 
			this.buttonChatTextSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonChatTextSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonChatTextSend.Location = new System.Drawing.Point(698, 2);
			this.buttonChatTextSend.Name = "buttonChatTextSend";
			this.buttonChatTextSend.Size = new System.Drawing.Size(54, 28);
			this.buttonChatTextSend.TabIndex = 1;
			this.buttonChatTextSend.Text = "Send";
			this.buttonChatTextSend.UseVisualStyleBackColor = true;
			this.buttonChatTextSend.Click += new System.EventHandler(this.buttonChatTextSend_Click);
			// 
			// textBoxAutoScrollChatText
			// 
			this.textBoxAutoScrollChatText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAutoScrollChatText.AutoScroll = true;
			this.textBoxAutoScrollChatText.ButtonAutoScrollToggle = this.buttonScrollLock;
			this.textBoxAutoScrollChatText.Font = new System.Drawing.Font("Segoe UI", 10.25F);
			this.textBoxAutoScrollChatText.Location = new System.Drawing.Point(0, 0);
			this.textBoxAutoScrollChatText.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxAutoScrollChatText.MaxLength = 52767;
			this.textBoxAutoScrollChatText.Name = "textBoxAutoScrollChatText";
			this.textBoxAutoScrollChatText.ReadOnly = true;
			this.textBoxAutoScrollChatText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBoxAutoScrollChatText.Size = new System.Drawing.Size(753, 121);
			this.textBoxAutoScrollChatText.TabIndex = 9;
			this.textBoxAutoScrollChatText.TabPage = this.tabPageChat;
			this.textBoxAutoScrollChatText.TabStop = false;
			this.textBoxAutoScrollChatText.Text = "";
			// 
			// tabPagePlayerLog
			// 
			this.tabPagePlayerLog.BackColor = System.Drawing.SystemColors.Control;
			this.tabPagePlayerLog.Controls.Add(this.textBoxAutoScrollPlayerLog);
			this.tabPagePlayerLog.Location = new System.Drawing.Point(4, 24);
			this.tabPagePlayerLog.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabPagePlayerLog.Name = "tabPagePlayerLog";
			this.tabPagePlayerLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePlayerLog.Size = new System.Drawing.Size(753, 153);
			this.tabPagePlayerLog.TabIndex = 4;
			this.tabPagePlayerLog.Text = "Player Log";
			// 
			// textBoxAutoScrollPlayerLog
			// 
			this.textBoxAutoScrollPlayerLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAutoScrollPlayerLog.AutoScroll = true;
			this.textBoxAutoScrollPlayerLog.ButtonAutoScrollToggle = this.buttonScrollLock;
			this.textBoxAutoScrollPlayerLog.Font = new System.Drawing.Font("Segoe UI", 10.25F);
			this.textBoxAutoScrollPlayerLog.Location = new System.Drawing.Point(0, 0);
			this.textBoxAutoScrollPlayerLog.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxAutoScrollPlayerLog.MaxLength = 52767;
			this.textBoxAutoScrollPlayerLog.Name = "textBoxAutoScrollPlayerLog";
			this.textBoxAutoScrollPlayerLog.ReadOnly = true;
			this.textBoxAutoScrollPlayerLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBoxAutoScrollPlayerLog.Size = new System.Drawing.Size(753, 153);
			this.textBoxAutoScrollPlayerLog.TabIndex = 10;
			this.textBoxAutoScrollPlayerLog.TabPage = this.tabPagePlayerLog;
			this.textBoxAutoScrollPlayerLog.TabStop = false;
			this.textBoxAutoScrollPlayerLog.Text = "";
			// 
			// tabPageAppLog
			// 
			this.tabPageAppLog.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageAppLog.Controls.Add(this.textBoxAutoScrollApplicationLog);
			this.tabPageAppLog.Location = new System.Drawing.Point(4, 24);
			this.tabPageAppLog.Name = "tabPageAppLog";
			this.tabPageAppLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAppLog.Size = new System.Drawing.Size(753, 153);
			this.tabPageAppLog.TabIndex = 5;
			this.tabPageAppLog.Text = "Application Log";
			// 
			// textBoxAutoScrollApplicationLog
			// 
			this.textBoxAutoScrollApplicationLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAutoScrollApplicationLog.AutoScroll = true;
			this.textBoxAutoScrollApplicationLog.ButtonAutoScrollToggle = this.buttonScrollLock;
			this.textBoxAutoScrollApplicationLog.Font = new System.Drawing.Font("Segoe UI", 10.25F);
			this.textBoxAutoScrollApplicationLog.Location = new System.Drawing.Point(0, 0);
			this.textBoxAutoScrollApplicationLog.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxAutoScrollApplicationLog.MaxLength = 52767;
			this.textBoxAutoScrollApplicationLog.Name = "textBoxAutoScrollApplicationLog";
			this.textBoxAutoScrollApplicationLog.ReadOnly = true;
			this.textBoxAutoScrollApplicationLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBoxAutoScrollApplicationLog.Size = new System.Drawing.Size(753, 153);
			this.textBoxAutoScrollApplicationLog.TabIndex = 11;
			this.textBoxAutoScrollApplicationLog.TabPage = this.tabPageAppLog;
			this.textBoxAutoScrollApplicationLog.TabStop = false;
			this.textBoxAutoScrollApplicationLog.Text = "";
			// 
			// labelMap
			// 
			this.labelMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelMap.AutoSize = true;
			this.labelMap.Enabled = false;
			this.labelMap.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.labelMap.Location = new System.Drawing.Point(152, 22);
			this.labelMap.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelMap.Name = "labelMap";
			this.labelMap.Size = new System.Drawing.Size(41, 21);
			this.labelMap.TabIndex = 5;
			this.labelMap.Text = "Map";
			this.toolTip1.SetToolTip(this.labelMap, "Right click to copy the map name to the clipboard");
			this.labelMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label_MouseRightClickCopyContents);
			// 
			// labelVariant
			// 
			this.labelVariant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelVariant.AutoSize = true;
			this.labelVariant.Enabled = false;
			this.labelVariant.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.labelVariant.Location = new System.Drawing.Point(152, 3);
			this.labelVariant.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelVariant.Name = "labelVariant";
			this.labelVariant.Size = new System.Drawing.Size(59, 21);
			this.labelVariant.TabIndex = 6;
			this.labelVariant.Text = "Variant";
			this.toolTip1.SetToolTip(this.labelVariant, "Right click to copy the gametype name to the clipboard");
			this.labelVariant.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label_MouseRightClickCopyContents);
			// 
			// labelPlayers
			// 
			this.labelPlayers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayers.Image = global::RconTool.Properties.Resources.Icon_Spartan20x20;
			this.labelPlayers.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.labelPlayers.Location = new System.Drawing.Point(1, -1);
			this.labelPlayers.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.labelPlayers.Name = "labelPlayers";
			this.labelPlayers.Size = new System.Drawing.Size(60, 20);
			this.labelPlayers.TabIndex = 9;
			this.labelPlayers.Text = "16";
			this.labelPlayers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolTip1.SetToolTip(this.labelPlayers, "Current Players");
			// 
			// statusStripStatusInformation
			// 
			this.statusStripStatusInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statusStripStatusInformation.AutoSize = false;
			this.statusStripStatusInformation.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStripStatusInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonFileMenu,
            this.toolStripSplitButtonServerSelect,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelRconConnection,
            this.toolStripStatusLabelStatsConnection,
            this.toolStripSplitButtonServerHook});
			this.statusStripStatusInformation.Location = new System.Drawing.Point(0, 737);
			this.statusStripStatusInformation.Name = "statusStripStatusInformation";
			this.statusStripStatusInformation.Size = new System.Drawing.Size(605, 24);
			this.statusStripStatusInformation.SizingGrip = false;
			this.statusStripStatusInformation.TabIndex = 5;
			// 
			// toolStripDropDownButtonFileMenu
			// 
			this.toolStripDropDownButtonFileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuManageServersButton,
            this.fileMenuSettingsItem,
            this.fileMenuCommandsItem,
            this.fileMenuTranslationItem,
            this.fileMenuRetryConnectionButton,
            this.fileMenuAboutButton,
            this.fileMenuExitButton});
			this.toolStripDropDownButtonFileMenu.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonFileMenu.Image")));
			this.toolStripDropDownButtonFileMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonFileMenu.Name = "toolStripDropDownButtonFileMenu";
			this.toolStripDropDownButtonFileMenu.ShowDropDownArrow = false;
			this.toolStripDropDownButtonFileMenu.Size = new System.Drawing.Size(42, 22);
			this.toolStripDropDownButtonFileMenu.Text = "Menu";
			// 
			// fileMenuManageServersButton
			// 
			this.fileMenuManageServersButton.Name = "fileMenuManageServersButton";
			this.fileMenuManageServersButton.Size = new System.Drawing.Size(166, 22);
			this.fileMenuManageServersButton.Text = "Manage Servers";
			this.fileMenuManageServersButton.Click += new System.EventHandler(this.menuItemManageServers_Click);
			// 
			// fileMenuSettingsItem
			// 
			this.fileMenuSettingsItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle,
            this.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle,
            this.fileMenuSettingsItemChangeScoreboardFontSizeButton});
			this.fileMenuSettingsItem.Name = "fileMenuSettingsItem";
			this.fileMenuSettingsItem.Size = new System.Drawing.Size(166, 22);
			this.fileMenuSettingsItem.Text = "Settings";
			// 
			// fileMenuSettingsItemPlaySoundOnPlayerJoinToggle
			// 
			this.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle.CheckOnClick = true;
			this.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle.Name = "fileMenuSettingsItemPlaySoundOnPlayerJoinToggle";
			this.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle.Size = new System.Drawing.Size(228, 22);
			this.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle.Text = "Play Sound On Player Join";
			this.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle.Click += new System.EventHandler(this.toolStripMenuItemPlaySoundOnPlayerJoin_Click);
			// 
			// fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle
			// 
			this.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle.CheckOnClick = true;
			this.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle.Name = "fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle";
			this.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle.Size = new System.Drawing.Size(228, 22);
			this.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle.Text = "Play Sound On Player Leave";
			this.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle.Click += new System.EventHandler(this.toolStripMenuItemPlaySoundOnPlayerLeave_Click);
			// 
			// fileMenuSettingsItemChangeScoreboardFontSizeButton
			// 
			this.fileMenuSettingsItemChangeScoreboardFontSizeButton.Name = "fileMenuSettingsItemChangeScoreboardFontSizeButton";
			this.fileMenuSettingsItemChangeScoreboardFontSizeButton.Size = new System.Drawing.Size(228, 22);
			this.fileMenuSettingsItemChangeScoreboardFontSizeButton.Text = "Change Scoreboard Font Size";
			this.fileMenuSettingsItemChangeScoreboardFontSizeButton.Click += new System.EventHandler(this.toolStripMenuItemChangeScoreboardFontSize_Click);
			// 
			// fileMenuCommandsItem
			// 
			this.fileMenuCommandsItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuCommandsItemManageCommandsButton,
            this.fileMenuCommandsItemShuffleTeamsCommandToggle,
            this.fileMenuCommandsItemKickPlayerCommandToggle,
            this.fileMenuCommandsItemEndGameCommandToggle});
			this.fileMenuCommandsItem.Name = "fileMenuCommandsItem";
			this.fileMenuCommandsItem.Size = new System.Drawing.Size(166, 22);
			this.fileMenuCommandsItem.Text = "Commands";
			this.fileMenuCommandsItem.DropDownOpening += new System.EventHandler(this.fileMenuCommandsItem_DropDownOpening);
			// 
			// fileMenuCommandsItemManageCommandsButton
			// 
			this.fileMenuCommandsItemManageCommandsButton.Name = "fileMenuCommandsItemManageCommandsButton";
			this.fileMenuCommandsItemManageCommandsButton.Size = new System.Drawing.Size(233, 22);
			this.fileMenuCommandsItemManageCommandsButton.Text = "Manage Commands";
			this.fileMenuCommandsItemManageCommandsButton.Click += new System.EventHandler(this.menuItemManageCommands_Click);
			// 
			// fileMenuCommandsItemShuffleTeamsCommandToggle
			// 
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.Checked = true;
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.CheckOnClick = true;
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.Name = "fileMenuCommandsItemShuffleTeamsCommandToggle";
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.Size = new System.Drawing.Size(233, 22);
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.Text = "Shuffle Teams Vote Command";
			this.fileMenuCommandsItemShuffleTeamsCommandToggle.Click += new System.EventHandler(this.buttonToggleShuffleTeamsCommand_Click);
			// 
			// fileMenuCommandsItemKickPlayerCommandToggle
			// 
			this.fileMenuCommandsItemKickPlayerCommandToggle.Checked = true;
			this.fileMenuCommandsItemKickPlayerCommandToggle.CheckOnClick = true;
			this.fileMenuCommandsItemKickPlayerCommandToggle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.fileMenuCommandsItemKickPlayerCommandToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.fileMenuCommandsItemKickPlayerCommandToggle.Name = "fileMenuCommandsItemKickPlayerCommandToggle";
			this.fileMenuCommandsItemKickPlayerCommandToggle.Size = new System.Drawing.Size(233, 22);
			this.fileMenuCommandsItemKickPlayerCommandToggle.Text = "Kick Player Vote Command";
			this.fileMenuCommandsItemKickPlayerCommandToggle.Click += new System.EventHandler(this.buttonToggleKickPlayerCommand_Click);
			// 
			// fileMenuCommandsItemEndGameCommandToggle
			// 
			this.fileMenuCommandsItemEndGameCommandToggle.Checked = true;
			this.fileMenuCommandsItemEndGameCommandToggle.CheckOnClick = true;
			this.fileMenuCommandsItemEndGameCommandToggle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.fileMenuCommandsItemEndGameCommandToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.fileMenuCommandsItemEndGameCommandToggle.Name = "fileMenuCommandsItemEndGameCommandToggle";
			this.fileMenuCommandsItemEndGameCommandToggle.Size = new System.Drawing.Size(233, 22);
			this.fileMenuCommandsItemEndGameCommandToggle.Text = "End Game Vote Command";
			this.fileMenuCommandsItemEndGameCommandToggle.Click += new System.EventHandler(this.buttonToggleEndGameCommand_Click);
			// 
			// fileMenuTranslationItem
			// 
			this.fileMenuTranslationItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuTranslationItemTranslationEnabledButton,
            this.fileMenuTranslationItemSetGoogleTranslateAPIKeyButton,
            this.fileMenuTranslationItemSetCharacterCountAndBillingCycleButton,
            this.fileMenuTranslationItemTranslatedCharactersTextBox,
            this.fileMenuTranslationItemTranslationBillingCycleTextBox});
			this.fileMenuTranslationItem.Name = "fileMenuTranslationItem";
			this.fileMenuTranslationItem.Size = new System.Drawing.Size(166, 22);
			this.fileMenuTranslationItem.Text = "Translation";
			this.fileMenuTranslationItem.DropDownOpening += new System.EventHandler(this.fileMenuTranslationItem_DropDownOpening);
			// 
			// fileMenuTranslationItemTranslationEnabledButton
			// 
			this.fileMenuTranslationItemTranslationEnabledButton.Name = "fileMenuTranslationItemTranslationEnabledButton";
			this.fileMenuTranslationItemTranslationEnabledButton.Size = new System.Drawing.Size(280, 22);
			this.fileMenuTranslationItemTranslationEnabledButton.Text = "Translation Enabled";
			this.fileMenuTranslationItemTranslationEnabledButton.Click += new System.EventHandler(this.fileMenuTranslationItemTranslationEnabledButton_Click);
			// 
			// fileMenuTranslationItemSetGoogleTranslateAPIKeyButton
			// 
			this.fileMenuTranslationItemSetGoogleTranslateAPIKeyButton.Name = "fileMenuTranslationItemSetGoogleTranslateAPIKeyButton";
			this.fileMenuTranslationItemSetGoogleTranslateAPIKeyButton.Size = new System.Drawing.Size(280, 22);
			this.fileMenuTranslationItemSetGoogleTranslateAPIKeyButton.Text = "Set Google Translate API Key";
			this.fileMenuTranslationItemSetGoogleTranslateAPIKeyButton.Click += new System.EventHandler(this.fileMenuTranslationItemSetGoogleTranslateApiKey_Click);
			// 
			// fileMenuTranslationItemSetCharacterCountAndBillingCycleButton
			// 
			this.fileMenuTranslationItemSetCharacterCountAndBillingCycleButton.Name = "fileMenuTranslationItemSetCharacterCountAndBillingCycleButton";
			this.fileMenuTranslationItemSetCharacterCountAndBillingCycleButton.Size = new System.Drawing.Size(280, 22);
			this.fileMenuTranslationItemSetCharacterCountAndBillingCycleButton.Text = "Set Character Count and Billing Cycle";
			this.fileMenuTranslationItemSetCharacterCountAndBillingCycleButton.Click += new System.EventHandler(this.fileMenuTranslationItemSetCharacterCountAndBillingCycleButton_Click);
			// 
			// fileMenuTranslationItemTranslatedCharactersTextBox
			// 
			this.fileMenuTranslationItemTranslatedCharactersTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.fileMenuTranslationItemTranslatedCharactersTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.fileMenuTranslationItemTranslatedCharactersTextBox.Enabled = false;
			this.fileMenuTranslationItemTranslatedCharactersTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.fileMenuTranslationItemTranslatedCharactersTextBox.Name = "fileMenuTranslationItemTranslatedCharactersTextBox";
			this.fileMenuTranslationItemTranslatedCharactersTextBox.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
			this.fileMenuTranslationItemTranslatedCharactersTextBox.ReadOnly = true;
			this.fileMenuTranslationItemTranslatedCharactersTextBox.ShortcutsEnabled = false;
			this.fileMenuTranslationItemTranslatedCharactersTextBox.Size = new System.Drawing.Size(220, 16);
			this.fileMenuTranslationItemTranslatedCharactersTextBox.Text = "Translated Characters: 0";
			// 
			// fileMenuTranslationItemTranslationBillingCycleTextBox
			// 
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.Enabled = false;
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.Name = "fileMenuTranslationItemTranslationBillingCycleTextBox";
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.ReadOnly = true;
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.ShortcutsEnabled = false;
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.Size = new System.Drawing.Size(220, 16);
			this.fileMenuTranslationItemTranslationBillingCycleTextBox.Text = "Billing Cycle: ";
			// 
			// fileMenuRetryConnectionButton
			// 
			this.fileMenuRetryConnectionButton.Name = "fileMenuRetryConnectionButton";
			this.fileMenuRetryConnectionButton.Size = new System.Drawing.Size(166, 22);
			this.fileMenuRetryConnectionButton.Text = "Retry Connection";
			this.fileMenuRetryConnectionButton.Click += new System.EventHandler(this.menuItemRetryConnection_Click);
			// 
			// fileMenuAboutButton
			// 
			this.fileMenuAboutButton.Name = "fileMenuAboutButton";
			this.fileMenuAboutButton.Size = new System.Drawing.Size(166, 22);
			this.fileMenuAboutButton.Text = "About";
			this.fileMenuAboutButton.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// fileMenuExitButton
			// 
			this.fileMenuExitButton.Name = "fileMenuExitButton";
			this.fileMenuExitButton.Size = new System.Drawing.Size(166, 22);
			this.fileMenuExitButton.Text = "Exit";
			this.fileMenuExitButton.Click += new System.EventHandler(this.menuItemExit_Click);
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
			this.toolStripStatusLabelRconConnection.Size = new System.Drawing.Size(59, 19);
			this.toolStripStatusLabelRconConnection.Text = "RCON:";
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
			// toolStripSplitButtonServerHook
			// 
			this.toolStripSplitButtonServerHook.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripSplitButtonServerHook.DropDownButtonWidth = 0;
			this.toolStripSplitButtonServerHook.Image = global::RconTool.Properties.Resources.Image_XMark32x32;
			this.toolStripSplitButtonServerHook.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonServerHook.Name = "toolStripSplitButtonServerHook";
			this.toolStripSplitButtonServerHook.Size = new System.Drawing.Size(89, 22);
			this.toolStripSplitButtonServerHook.Text = "ServerHook";
			this.toolStripSplitButtonServerHook.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.toolStripSplitButtonServerHook.ToolTipText = "Click to attempt to establish the ServerHook.\\nIn order to establish the ServerHo" +
    "ok:\\n\\tThe server process must be running on this computer.\\n\\tThis application " +
    "must have administrator privileges.";
			this.toolStripSplitButtonServerHook.Click += new System.EventHandler(this.toolStripSplitButtonServerHook_Click);
			// 
			// toolStripSplitButtonAutoUpdate
			// 
			this.toolStripSplitButtonAutoUpdate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAutoUpdateEnabled,
            this.toolStripMenuItemAutoUpdateDisabled});
			this.toolStripSplitButtonAutoUpdate.Enabled = false;
			this.toolStripSplitButtonAutoUpdate.Image = global::RconTool.Properties.Resources.Image_CheckMark32x32;
			this.toolStripSplitButtonAutoUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonAutoUpdate.Name = "toolStripSplitButtonAutoUpdate";
			this.toolStripSplitButtonAutoUpdate.Size = new System.Drawing.Size(108, 22);
			this.toolStripSplitButtonAutoUpdate.Text = "Auto-Update";
			this.toolStripSplitButtonAutoUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.toolStripSplitButtonAutoUpdate.Visible = false;
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
			this.statusStripVersion.Location = new System.Drawing.Point(605, 737);
			this.statusStripVersion.Name = "statusStripVersion";
			this.statusStripVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusStripVersion.Size = new System.Drawing.Size(156, 24);
			this.statusStripVersion.SizingGrip = false;
			this.statusStripVersion.TabIndex = 12;
			// 
			// toolStripFontSizeDecrease
			// 
			this.toolStripFontSizeDecrease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripFontSizeDecrease.DropDownButtonWidth = 0;
			this.toolStripFontSizeDecrease.Enabled = false;
			this.toolStripFontSizeDecrease.Image = global::RconTool.Properties.Resources.Icon_TriangleDown;
			this.toolStripFontSizeDecrease.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripFontSizeDecrease.Name = "toolStripFontSizeDecrease";
			this.toolStripFontSizeDecrease.Size = new System.Drawing.Size(21, 22);
			this.toolStripFontSizeDecrease.ToolTipText = "Decrease Font Size";
			this.toolStripFontSizeDecrease.Visible = false;
			this.toolStripFontSizeDecrease.ButtonClick += new System.EventHandler(this.toolStripFontSizeDecrease_ButtonClick);
			// 
			// toolStripFontSizeIncrease
			// 
			this.toolStripFontSizeIncrease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripFontSizeIncrease.DropDownButtonWidth = 0;
			this.toolStripFontSizeIncrease.Enabled = false;
			this.toolStripFontSizeIncrease.Image = global::RconTool.Properties.Resources.Icon_TriangleUp;
			this.toolStripFontSizeIncrease.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripFontSizeIncrease.Name = "toolStripFontSizeIncrease";
			this.toolStripFontSizeIncrease.Size = new System.Drawing.Size(21, 22);
			this.toolStripFontSizeIncrease.ToolTipText = "Increase Font Size";
			this.toolStripFontSizeIncrease.Visible = false;
			this.toolStripFontSizeIncrease.ButtonClick += new System.EventHandler(this.toolStripFontSizeIncrease_ButtonClick);
			// 
			// toolStripResizeToFitScoreboard
			// 
			this.toolStripResizeToFitScoreboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripResizeToFitScoreboard.DropDownButtonWidth = 0;
			this.toolStripResizeToFitScoreboard.Enabled = false;
			this.toolStripResizeToFitScoreboard.Image = global::RconTool.Properties.Resources.Icon_DoubleArrowExpandListVertical;
			this.toolStripResizeToFitScoreboard.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripResizeToFitScoreboard.Name = "toolStripResizeToFitScoreboard";
			this.toolStripResizeToFitScoreboard.Size = new System.Drawing.Size(21, 22);
			this.toolStripResizeToFitScoreboard.ToolTipText = "Resize To Fit Scoreboard";
			this.toolStripResizeToFitScoreboard.Visible = false;
			this.toolStripResizeToFitScoreboard.ButtonClick += new System.EventHandler(this.toolStripResizeToFitScoreboard_ButtonClick);
			// 
			// toolStripStatusLabelVersion
			// 
			this.toolStripStatusLabelVersion.Enabled = false;
			this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
			this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(75, 19);
			this.toolStripStatusLabelVersion.Text = "Version: 3.5.0";
			this.toolStripStatusLabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabelVersion.Visible = false;
			// 
			// comboBoxMaxPlayers
			// 
			this.comboBoxMaxPlayers.DisplayMember = "16";
			this.comboBoxMaxPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMaxPlayers.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboBoxMaxPlayers.FormattingEnabled = true;
			this.comboBoxMaxPlayers.Items.AddRange(new object[] {
            "16",
            "15",
            "14",
            "13",
            "12",
            "11",
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1"});
			this.comboBoxMaxPlayers.Location = new System.Drawing.Point(65, 0);
			this.comboBoxMaxPlayers.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.comboBoxMaxPlayers.Name = "comboBoxMaxPlayers";
			this.comboBoxMaxPlayers.Size = new System.Drawing.Size(36, 21);
			this.comboBoxMaxPlayers.TabIndex = 14;
			this.comboBoxMaxPlayers.TabStop = false;
			this.toolTip1.SetToolTip(this.comboBoxMaxPlayers, "Max Players");
			this.comboBoxMaxPlayers.SelectionChangeCommitted += new System.EventHandler(this.comboBoxMaxPlayers_SelectionChangeCommitted);
			// 
			// buttonClearLog
			// 
			this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearLog.BackColor = System.Drawing.SystemColors.Control;
			this.buttonClearLog.BackgroundImage = global::RconTool.Properties.Resources.deleteIcon14x14;
			this.buttonClearLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonClearLog.FlatAppearance.BorderSize = 0;
			this.buttonClearLog.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.buttonClearLog.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
			this.buttonClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonClearLog.Location = new System.Drawing.Point(718, 516);
			this.buttonClearLog.Name = "buttonClearLog";
			this.buttonClearLog.Size = new System.Drawing.Size(18, 18);
			this.buttonClearLog.TabIndex = 21;
			this.buttonClearLog.TabStop = false;
			this.toolTip1.SetToolTip(this.buttonClearLog, "Click to clear the Console Log text.");
			this.buttonClearLog.UseVisualStyleBackColor = false;
			this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
			// 
			// textBoxServerNameEdit
			// 
			this.textBoxServerNameEdit.AcceptsReturn = true;
			this.textBoxServerNameEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServerNameEdit.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxServerNameEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxServerNameEdit.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.textBoxServerNameEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxServerNameEdit.Location = new System.Drawing.Point(244, 9);
			this.textBoxServerNameEdit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.textBoxServerNameEdit.MinimumSize = new System.Drawing.Size(0, 27);
			this.textBoxServerNameEdit.Name = "textBoxServerNameEdit";
			this.textBoxServerNameEdit.ReadOnly = true;
			this.textBoxServerNameEdit.Size = new System.Drawing.Size(384, 27);
			this.textBoxServerNameEdit.TabIndex = 24;
			this.textBoxServerNameEdit.TabStop = false;
			this.textBoxServerNameEdit.Text = "Server Name";
			this.textBoxServerNameEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.toolTip1.SetToolTip(this.textBoxServerNameEdit, "CLICK to edit Server Name. ENTER to submit. ESCAPE to cancel.");
			this.textBoxServerNameEdit.WordWrap = false;
			this.textBoxServerNameEdit.Click += new System.EventHandler(this.textBoxServerNameEdit_Click);
			this.textBoxServerNameEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxServerNameEdit_KeyDown);
			this.textBoxServerNameEdit.Leave += new System.EventHandler(this.textBoxServerNameEdit_Leave);
			// 
			// statusStripServerCommandButtons
			// 
			this.statusStripServerCommandButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.statusStripServerCommandButtons.AutoSize = false;
			this.statusStripServerCommandButtons.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStripServerCommandButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusButtonStartGame,
            this.statusButtonStopGame,
            this.statusButtonReloadVoteOrVetoJSON,
            this.statusButtonShuffleTeams,
            this.statusButtonSprintToggle,
            this.statusButtonAssassinationToggle});
			this.statusStripServerCommandButtons.Location = new System.Drawing.Point(0, 715);
			this.statusStripServerCommandButtons.Name = "statusStripServerCommandButtons";
			this.statusStripServerCommandButtons.ShowItemToolTips = true;
			this.statusStripServerCommandButtons.Size = new System.Drawing.Size(128, 22);
			this.statusStripServerCommandButtons.SizingGrip = false;
			this.statusStripServerCommandButtons.TabIndex = 13;
			// 
			// statusButtonStartGame
			// 
			this.statusButtonStartGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.statusButtonStartGame.DropDownButtonWidth = 0;
			this.statusButtonStartGame.Image = global::RconTool.Properties.Resources.Icon_GreenPlayArrow16x16;
			this.statusButtonStartGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusButtonStartGame.Name = "statusButtonStartGame";
			this.statusButtonStartGame.Size = new System.Drawing.Size(21, 20);
			this.statusButtonStartGame.ToolTipText = "Start Game";
			this.statusButtonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
			// 
			// statusButtonStopGame
			// 
			this.statusButtonStopGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.statusButtonStopGame.DropDownButtonWidth = 0;
			this.statusButtonStopGame.Image = global::RconTool.Properties.Resources.Icon_StopSign16x16;
			this.statusButtonStopGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusButtonStopGame.Name = "statusButtonStopGame";
			this.statusButtonStopGame.Size = new System.Drawing.Size(21, 20);
			this.statusButtonStopGame.ToolTipText = "Stop Game";
			this.statusButtonStopGame.Click += new System.EventHandler(this.buttonStopGame_Click);
			// 
			// statusButtonReloadVoteOrVetoJSON
			// 
			this.statusButtonReloadVoteOrVetoJSON.BackColor = System.Drawing.SystemColors.Control;
			this.statusButtonReloadVoteOrVetoJSON.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.statusButtonReloadVoteOrVetoJSON.DropDownButtonWidth = 0;
			this.statusButtonReloadVoteOrVetoJSON.Image = global::RconTool.Properties.Resources.Icon_VoteCircle16x16;
			this.statusButtonReloadVoteOrVetoJSON.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusButtonReloadVoteOrVetoJSON.Name = "statusButtonReloadVoteOrVetoJSON";
			this.statusButtonReloadVoteOrVetoJSON.Size = new System.Drawing.Size(21, 20);
			this.statusButtonReloadVoteOrVetoJSON.ToolTipText = "Reload Voting JSON";
			this.statusButtonReloadVoteOrVetoJSON.Click += new System.EventHandler(this.buttonReloadVotingAndVetoJson_Click);
			// 
			// statusButtonShuffleTeams
			// 
			this.statusButtonShuffleTeams.BackColor = System.Drawing.SystemColors.Control;
			this.statusButtonShuffleTeams.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.statusButtonShuffleTeams.DropDownButtonWidth = 0;
			this.statusButtonShuffleTeams.Image = global::RconTool.Properties.Resources.Icon_ShuffleCircle16x16;
			this.statusButtonShuffleTeams.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusButtonShuffleTeams.Name = "statusButtonShuffleTeams";
			this.statusButtonShuffleTeams.Size = new System.Drawing.Size(21, 20);
			this.statusButtonShuffleTeams.ToolTipText = "Click to Shuffle Teams";
			this.statusButtonShuffleTeams.Click += new System.EventHandler(this.buttonShuffleTeams_Click);
			// 
			// statusButtonSprintToggle
			// 
			this.statusButtonSprintToggle.BackColor = System.Drawing.SystemColors.Control;
			this.statusButtonSprintToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.statusButtonSprintToggle.DropDownButtonWidth = 0;
			this.statusButtonSprintToggle.Image = global::RconTool.Properties.Resources.SprintTiny_Disabled;
			this.statusButtonSprintToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusButtonSprintToggle.Name = "statusButtonSprintToggle";
			this.statusButtonSprintToggle.Size = new System.Drawing.Size(21, 20);
			this.statusButtonSprintToggle.ToolTipText = "Sprint Disabled: Click to toggle";
			this.statusButtonSprintToggle.Click += new System.EventHandler(this.buttonToggleSprint_Click);
			// 
			// statusButtonAssassinationToggle
			// 
			this.statusButtonAssassinationToggle.BackColor = System.Drawing.SystemColors.Control;
			this.statusButtonAssassinationToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.statusButtonAssassinationToggle.DropDownButtonWidth = 0;
			this.statusButtonAssassinationToggle.Image = global::RconTool.Properties.Resources.AssassinationTiny_Disabled;
			this.statusButtonAssassinationToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusButtonAssassinationToggle.Name = "statusButtonAssassinationToggle";
			this.statusButtonAssassinationToggle.Size = new System.Drawing.Size(21, 20);
			this.statusButtonAssassinationToggle.ToolTipText = "Assassinations Disabled: Click to toggle";
			this.statusButtonAssassinationToggle.Click += new System.EventHandler(this.buttonToggleAssasinations_Click);
			// 
			// panelPlayerCount
			// 
			this.panelPlayerCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panelPlayerCount.Controls.Add(this.labelVariantTypeIcon);
			this.panelPlayerCount.Controls.Add(this.comboBoxMaxPlayers);
			this.panelPlayerCount.Controls.Add(this.labelPlayersOf);
			this.panelPlayerCount.Controls.Add(this.labelPlayers);
			this.panelPlayerCount.Location = new System.Drawing.Point(-4, 695);
			this.panelPlayerCount.Name = "panelPlayerCount";
			this.panelPlayerCount.Size = new System.Drawing.Size(132, 22);
			this.panelPlayerCount.TabIndex = 15;
			// 
			// labelVariantTypeIcon
			// 
			this.labelVariantTypeIcon.AutoSize = true;
			this.labelVariantTypeIcon.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVariantTypeIcon.Location = new System.Drawing.Point(104, 5);
			this.labelVariantTypeIcon.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.labelVariantTypeIcon.Name = "labelVariantTypeIcon";
			this.labelVariantTypeIcon.Size = new System.Drawing.Size(27, 13);
			this.labelVariantTypeIcon.TabIndex = 15;
			this.labelVariantTypeIcon.Text = "v6.1";
			this.labelVariantTypeIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelPlayersOf
			// 
			this.labelPlayersOf.AutoSize = true;
			this.labelPlayersOf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayersOf.Location = new System.Drawing.Point(38, -1);
			this.labelPlayersOf.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.labelPlayersOf.Name = "labelPlayersOf";
			this.labelPlayersOf.Size = new System.Drawing.Size(24, 21);
			this.labelPlayersOf.TabIndex = 9;
			this.labelPlayersOf.Text = "of";
			// 
			// pictureBoxMapAndStatusOverlay
			// 
			this.pictureBoxMapAndStatusOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBoxMapAndStatusOverlay.BackgroundImage = global::RconTool.Properties.Resources.map_icon_Valhalla;
			this.pictureBoxMapAndStatusOverlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBoxMapAndStatusOverlay.Image = global::RconTool.Properties.Resources.Image_StatusOverlay_InLobby_126x40;
			this.pictureBoxMapAndStatusOverlay.Location = new System.Drawing.Point(0, 3);
			this.pictureBoxMapAndStatusOverlay.Name = "pictureBoxMapAndStatusOverlay";
			this.pictureBoxMapAndStatusOverlay.Size = new System.Drawing.Size(126, 41);
			this.pictureBoxMapAndStatusOverlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxMapAndStatusOverlay.TabIndex = 17;
			this.pictureBoxMapAndStatusOverlay.TabStop = false;
			this.pictureBoxMapAndStatusOverlay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMapAndStatusOverlay_MouseClick);
			// 
			// labelMapVariantOn
			// 
			this.labelMapVariantOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelMapVariantOn.AutoSize = true;
			this.labelMapVariantOn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
			this.labelMapVariantOn.Location = new System.Drawing.Point(129, 26);
			this.labelMapVariantOn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelMapVariantOn.Name = "labelMapVariantOn";
			this.labelMapVariantOn.Size = new System.Drawing.Size(20, 15);
			this.labelMapVariantOn.TabIndex = 18;
			this.labelMapVariantOn.Text = "on";
			// 
			// pictureBoxGameVariantIcon
			// 
			this.pictureBoxGameVariantIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBoxGameVariantIcon.Image = global::RconTool.Properties.Resources.gt_icon_vip_black_20x20;
			this.pictureBoxGameVariantIcon.Location = new System.Drawing.Point(129, 5);
			this.pictureBoxGameVariantIcon.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.pictureBoxGameVariantIcon.Name = "pictureBoxGameVariantIcon";
			this.pictureBoxGameVariantIcon.Size = new System.Drawing.Size(20, 20);
			this.pictureBoxGameVariantIcon.TabIndex = 19;
			this.pictureBoxGameVariantIcon.TabStop = false;
			// 
			// toolStripSplitButton1
			// 
			this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
			this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
			this.toolStripSplitButton1.Text = "toolStripSplitButton1";
			// 
			// panelMapAndVariantDisplay
			// 
			this.panelMapAndVariantDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panelMapAndVariantDisplay.AutoSize = true;
			this.panelMapAndVariantDisplay.Controls.Add(this.toolStripMapSelect);
			this.panelMapAndVariantDisplay.Controls.Add(this.pictureBoxMapAndStatusOverlay);
			this.panelMapAndVariantDisplay.Controls.Add(this.pictureBoxGameVariantIcon);
			this.panelMapAndVariantDisplay.Controls.Add(this.labelMapVariantOn);
			this.panelMapAndVariantDisplay.Controls.Add(this.labelVariant);
			this.panelMapAndVariantDisplay.Controls.Add(this.labelMap);
			this.panelMapAndVariantDisplay.Location = new System.Drawing.Point(0, 0);
			this.panelMapAndVariantDisplay.Margin = new System.Windows.Forms.Padding(0);
			this.panelMapAndVariantDisplay.MaximumSize = new System.Drawing.Size(420, 46);
			this.panelMapAndVariantDisplay.MinimumSize = new System.Drawing.Size(240, 46);
			this.panelMapAndVariantDisplay.Name = "panelMapAndVariantDisplay";
			this.panelMapAndVariantDisplay.Size = new System.Drawing.Size(240, 46);
			this.panelMapAndVariantDisplay.TabIndex = 22;
			// 
			// toolStripMapSelect
			// 
			this.toolStripMapSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.toolStripMapSelect.BackColor = System.Drawing.SystemColors.Menu;
			this.toolStripMapSelect.CanOverflow = false;
			this.toolStripMapSelect.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripMapSelect.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStripMapSelect.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMapSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonGame,
            this.toolStripDropDownButtonMap});
			this.toolStripMapSelect.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.toolStripMapSelect.Location = new System.Drawing.Point(152, 2);
			this.toolStripMapSelect.Name = "toolStripMapSelect";
			this.toolStripMapSelect.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.toolStripMapSelect.Size = new System.Drawing.Size(56, 43);
			this.toolStripMapSelect.Stretch = true;
			this.toolStripMapSelect.TabIndex = 20;
			// 
			// toolStripDropDownButtonGame
			// 
			this.toolStripDropDownButtonGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonGame.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.toolStripDropDownButtonGame.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonGame.Image")));
			this.toolStripDropDownButtonGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonGame.Margin = new System.Windows.Forms.Padding(0);
			this.toolStripDropDownButtonGame.Name = "toolStripDropDownButtonGame";
			this.toolStripDropDownButtonGame.Size = new System.Drawing.Size(55, 19);
			this.toolStripDropDownButtonGame.Text = "Variant";
			this.toolStripDropDownButtonGame.DropDownOpening += new System.EventHandler(this.toolStripDropDownButtonGame_DropDownOpening);
			// 
			// toolStripDropDownButtonMap
			// 
			this.toolStripDropDownButtonMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonMap.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.toolStripDropDownButtonMap.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonMap.Image")));
			this.toolStripDropDownButtonMap.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonMap.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.toolStripDropDownButtonMap.Name = "toolStripDropDownButtonMap";
			this.toolStripDropDownButtonMap.Size = new System.Drawing.Size(55, 19);
			this.toolStripDropDownButtonMap.Text = "Map";
			this.toolStripDropDownButtonMap.DropDownOpening += new System.EventHandler(this.toolStripDropDownButtonMap_DropDownOpening);
			// 
			// tableLayoutPanelMatchInfoAndServerName
			// 
			this.tableLayoutPanelMatchInfoAndServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelMatchInfoAndServerName.ColumnCount = 2;
			this.tableLayoutPanelMatchInfoAndServerName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelMatchInfoAndServerName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelMatchInfoAndServerName.Controls.Add(this.panelMapAndVariantDisplay, 0, 0);
			this.tableLayoutPanelMatchInfoAndServerName.Controls.Add(this.textBoxServerNameEdit, 1, 0);
			this.tableLayoutPanelMatchInfoAndServerName.Location = new System.Drawing.Point(128, 692);
			this.tableLayoutPanelMatchInfoAndServerName.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelMatchInfoAndServerName.Name = "tableLayoutPanelMatchInfoAndServerName";
			this.tableLayoutPanelMatchInfoAndServerName.RowCount = 1;
			this.tableLayoutPanelMatchInfoAndServerName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMatchInfoAndServerName.Size = new System.Drawing.Size(629, 45);
			this.tableLayoutPanelMatchInfoAndServerName.TabIndex = 23;
			// 
			// App
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(761, 761);
			this.Controls.Add(this.tableLayoutPanelMatchInfoAndServerName);
			this.Controls.Add(this.buttonClearLog);
			this.Controls.Add(this.buttonScrollLock);
			this.Controls.Add(this.panelPlayerCount);
			this.Controls.Add(this.statusStripServerCommandButtons);
			this.Controls.Add(this.tabControlServerInterfaces);
			this.Controls.Add(this.statusStripStatusInformation);
			this.Controls.Add(this.statusStripVersion);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(777, 800);
			this.Name = "App";
			this.RightToLeftLayout = true;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RCON Tool";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.formApp_FormClosed);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.App_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.App_MouseDown);
			this.MouseLeave += new System.EventHandler(this.App_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.App_MouseMove);
			this.tabControlServerInterfaces.ResumeLayout(false);
			this.tabPageConsole.ResumeLayout(false);
			this.panelConsoleTextEntry.ResumeLayout(false);
			this.panelConsoleTextEntry.PerformLayout();
			this.tabPageChat.ResumeLayout(false);
			this.panelChatTextEntry.ResumeLayout(false);
			this.panelChatTextEntry.PerformLayout();
			this.tabPagePlayerLog.ResumeLayout(false);
			this.tabPageAppLog.ResumeLayout(false);
			this.statusStripStatusInformation.ResumeLayout(false);
			this.statusStripStatusInformation.PerformLayout();
			this.statusStripServerCommandButtons.ResumeLayout(false);
			this.statusStripServerCommandButtons.PerformLayout();
			this.panelPlayerCount.ResumeLayout(false);
			this.panelPlayerCount.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMapAndStatusOverlay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameVariantIcon)).EndInit();
			this.panelMapAndVariantDisplay.ResumeLayout(false);
			this.panelMapAndVariantDisplay.PerformLayout();
			this.toolStripMapSelect.ResumeLayout(false);
			this.toolStripMapSelect.PerformLayout();
			this.tableLayoutPanelMatchInfoAndServerName.ResumeLayout(false);
			this.tableLayoutPanelMatchInfoAndServerName.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxConsoleTextEntry;
        private System.Windows.Forms.Button buttonConsoleTextSend;
        private System.Windows.Forms.TabControl tabControlServerInterfaces;
        private System.Windows.Forms.TabPage tabPageConsole;
        private System.Windows.Forms.TabPage tabPageChat;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelVariant;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.StatusStrip statusStripStatusInformation;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRconConnection;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatsConnection;
        private System.Windows.Forms.TabPage tabPagePlayerLog;
		private System.Windows.Forms.TextBox textBoxChatTextEntry;
		private System.Windows.Forms.Button buttonChatTextSend;
		private System.Windows.Forms.Panel panelConsoleTextEntry;
		private System.Windows.Forms.Panel panelChatTextEntry;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonAutoUpdate;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoUpdateEnabled;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoUpdateDisabled;
		private System.Windows.Forms.StatusStrip statusStripVersion;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonServerSelect;
		private System.Windows.Forms.TabPage tabPageAppLog;
		private System.Windows.Forms.ToolStripSplitButton toolStripResizeToFitScoreboard;
		private System.Windows.Forms.ToolStripSplitButton toolStripFontSizeIncrease;
		private System.Windows.Forms.ToolStripSplitButton toolStripFontSizeDecrease;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonFileMenu;
		private System.Windows.Forms.ToolStripMenuItem fileMenuManageServersButton;
		private System.Windows.Forms.ToolStripMenuItem fileMenuSettingsItem;
		private System.Windows.Forms.ToolStripMenuItem fileMenuSettingsItemPlaySoundOnPlayerJoinToggle;
		private System.Windows.Forms.ToolStripMenuItem fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle;
		private System.Windows.Forms.ToolStripMenuItem fileMenuSettingsItemChangeScoreboardFontSizeButton;
		private System.Windows.Forms.ToolStripMenuItem fileMenuCommandsItem;
		private System.Windows.Forms.ToolStripMenuItem fileMenuCommandsItemManageCommandsButton;
		private System.Windows.Forms.ToolStripMenuItem fileMenuRetryConnectionButton;
		private System.Windows.Forms.ToolStripMenuItem fileMenuAboutButton;
		private System.Windows.Forms.ToolStripMenuItem fileMenuExitButton;
		private System.Windows.Forms.StatusStrip statusStripServerCommandButtons;
		private System.Windows.Forms.ToolStripSplitButton statusButtonStartGame;
		private System.Windows.Forms.ToolStripSplitButton statusButtonStopGame;
		private System.Windows.Forms.ToolStripSplitButton statusButtonSprintToggle;
		private System.Windows.Forms.ToolStripSplitButton statusButtonAssassinationToggle;
		private System.Windows.Forms.ToolStripSplitButton statusButtonReloadVoteOrVetoJSON;
		private System.Windows.Forms.ToolStripSplitButton statusButtonShuffleTeams;
		private System.Windows.Forms.ToolStripMenuItem fileMenuCommandsItemShuffleTeamsCommandToggle;
		private System.Windows.Forms.ToolStripMenuItem fileMenuCommandsItemKickPlayerCommandToggle;
		private System.Windows.Forms.ToolStripMenuItem fileMenuCommandsItemEndGameCommandToggle;
		private System.Windows.Forms.ComboBox comboBoxMaxPlayers;
		private System.Windows.Forms.Panel panelPlayerCount;
		private System.Windows.Forms.Label labelPlayersOf;
		private System.Windows.Forms.Label labelVariantTypeIcon;
		private System.Windows.Forms.PictureBox pictureBoxMapAndStatusOverlay;
		private System.Windows.Forms.Label labelMapVariantOn;
		private System.Windows.Forms.PictureBox pictureBoxGameVariantIcon;
		private System.Windows.Forms.Button buttonScrollLock;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
		private TextBoxAutoScroll textBoxAutoScrollConsoleText;
		private TextBoxAutoScroll textBoxAutoScrollChatText;
		private TextBoxAutoScroll textBoxAutoScrollPlayerLog;
		private TextBoxAutoScroll textBoxAutoScrollApplicationLog;
		public System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonClearLog;
		private System.Windows.Forms.ToolStripMenuItem fileMenuTranslationItem;
		private System.Windows.Forms.ToolStripMenuItem fileMenuTranslationItemTranslationEnabledButton;
		private System.Windows.Forms.ToolStripTextBox fileMenuTranslationItemTranslatedCharactersTextBox;
		private System.Windows.Forms.ToolStripMenuItem fileMenuTranslationItemSetCharacterCountAndBillingCycleButton;
		private System.Windows.Forms.ToolStripTextBox fileMenuTranslationItemTranslationBillingCycleTextBox;
		private System.Windows.Forms.ToolStripMenuItem fileMenuTranslationItemSetGoogleTranslateAPIKeyButton;
		private System.Windows.Forms.Panel panelMapAndVariantDisplay;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMatchInfoAndServerName;
		private System.Windows.Forms.TextBox textBoxServerNameEdit;
		private System.Windows.Forms.ToolStrip toolStripMapSelect;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonMap;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonGame;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonServerHook;
	}
}

