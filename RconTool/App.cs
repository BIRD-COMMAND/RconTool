#undef DEBUG

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using WebSocketSharp;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace RconTool
{

    public partial class App : Form
    {
        
        //TODO add command history for up and down arrow use on console input
        //TODO add \c modifier to console commands, if a command string ends with \c, send the command and copy the result returned from the server to the clipboard

        #region Properties/Fields

        public static App form;

        private bool autoUpdateEnabled = true;
        private bool autoScroll = true;

        //TODO persist app log text
        public static string AppLogText = "";

        public static string toolversion = "3.50";
        public static string titleOption = "";
        public static string webhook = "";
        public static string webhookTrigger = "";
        public static string webhookRole = "";
        
        public static string DynamicVotingJsonPath = "mods/server/dynamic.json";
        public static string currentVoteFile = "mods/server/voting.json";

        public static Connection currentConnection = null;
        public static List<Connection> connectionList = new List<Connection>();
        public static List<ToolCommand> GlobalToolCommands = new List<ToolCommand>();
        public static BindingSource playerListBindingSource;

        public static Thread TimedCommandsThread;
        public static Thread InterfaceUpdateThread;

        private string selectedServerTag = "";

        public static Point mousePoint = new Point();
        public static PlayerInfo contextPlayer;
        public ContextMenuStrip scoreBoardContextMenu = null;
        public static Dictionary<int, Tuple<Color,Color>> TeamColors { get; set; } = new Dictionary<int, Tuple<Color,Color>>()
        {
           {-1,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },    //dark gray  {-1, System.Drawing.ColorTranslator.FromHtml("#0B0B0B")}, //'#0B0B0B' "#BDBDBD"
            {0,new Tuple<Color,Color>(Color.FromArgb(98,11,11),     Color.FromArgb(128,41,41)) },     //red        { 0, System.Drawing.ColorTranslator.FromHtml("#620B0B")}, //'#620B0B' "#BDBDBD"
            {1,new Tuple<Color,Color>(Color.FromArgb(11,35,98),     Color.FromArgb(41,65,128)) },     //blue       { 1, System.Drawing.ColorTranslator.FromHtml("#0B2362")}, //'#0B2362' "#ef5350"
            {2,new Tuple<Color,Color>(Color.FromArgb(31,54,2),      Color.FromArgb(61,84,32)) },      //green      { 2, System.Drawing.ColorTranslator.FromHtml("#1F3602")}, //'#1F3602' "#42A5F5"
            {3,new Tuple<Color,Color>(Color.FromArgb(188,77,0),     Color.FromArgb(218,107,30)) },    //orange     { 3, System.Drawing.ColorTranslator.FromHtml("#BC4D00")}, //'#BC4D00' "#66BB6A"
            {4,new Tuple<Color,Color>(Color.FromArgb(29,16,82),     Color.FromArgb(59,46,112)) },     //purple     { 4, System.Drawing.ColorTranslator.FromHtml("#1D1052")}, //'#1D1052' "#FF7043"
            {5,new Tuple<Color,Color>(Color.FromArgb(167,119,8),    Color.FromArgb(197,147,38)) },    //gold       { 5, System.Drawing.ColorTranslator.FromHtml("#A77708")}, //'#A77708' "#7E57C2"
            {6,new Tuple<Color,Color>(Color.FromArgb(28,13,2),      Color.FromArgb(58,43,32)) },      //brown      { 6, System.Drawing.ColorTranslator.FromHtml("#1C0D02")}, //'#1C0D02' "#FFCA28"
            {7,new Tuple<Color,Color>(Color.FromArgb(255,77,138),   Color.FromArgb(255,107,168)) },   //pink       { 7, System.Drawing.ColorTranslator.FromHtml("#FF4D8A")}, //'#FF4D8A' "#8D6E63"
            {8,new Tuple<Color,Color>(Color.FromArgb(216,216,216),  Color.FromArgb(246, 246, 246)) }, //light gray { 9, System.Drawing.ColorTranslator.FromHtml("#424242")}, //'#424242' "#727272"
            {9,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },    //dark gray  { 8, System.Drawing.ColorTranslator.FromHtml("#0B0B0B")}, //'#D8D8D8' "#EC407A"
           {10,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },
           {11,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },
           {12,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },
           {13,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },
           {14,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },
           {15,new Tuple<Color,Color>(Color.FromArgb(66,66,66),     Color.FromArgb(96, 96, 96)) },
        };
        public static Dictionary<int, string> TeamColorStrings { get; set; } = new Dictionary<int, string>()
        {
            {-1, "GREY"}, //grey
            { 0, "RED"}, //red
            { 1, "BLUE"}, //blue
            { 2, "GREEN"}, //green
            { 3, "ORANGE"}, //orange
            { 4, "PURPLE"}, //purple
            { 5, "GOLD"}, //gold
            { 6, "BROWN"}, //brown
            { 7, "PINK"}, //pink
            { 8, "GREY"}, //grey
            { 9, "BLACK"},  //black
            {10, "BLACK"}, //grey
            {11, "BLACK"}, //grey
            {12, "BLACK"}, //grey
            {13, "BLACK"}, //grey
            {14, "BLACK"}, //grey
            {15, "BLACK"}, //grey
        };
        //public Dictionary<int, Color> TeamColors { get; set; } = new Dictionary<int, Color>()
        //{
        //    { 0, Color.FromArgb(155, 51, 50)},
        //    { 1, Color.FromArgb(50, 89, 146)},
        //    { 2, Color.FromArgb(31, 54, 2)},
        //    { 3, Color.FromArgb(188, 77, 0)},
        //    { 4, Color.FromArgb(29, 16, 82)},
        //    { 5, Color.FromArgb(167, 119, 8)},
        //    { 6, Color.FromArgb(28, 13, 2)},
        //    { 7, Color.FromArgb(255, 77, 138)},
        //    { 8, Color.FromArgb(216, 216, 216)},
        //    { 9, Color.FromArgb(11, 11, 11)},
        //    { 10,Color.FromArgb(216, 216, 216)}
        //};
        public Color DefaultScoreboardPlayerColor = System.Drawing.ColorTranslator.FromHtml("#BDBDBD");

        public Image CheckMarkImage = Properties.Resources.Image_CheckMark32x32;
        public Image XMarkImage = Properties.Resources.Image_XMark32x32;

        private static Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

        public static bool ResizeRequired { get; set; } = false;
        public static bool settings_PlaySoundOnPlayerJoin = false;
        public static bool settings_PlaySoundOnPlayerLeave = false;
        public static bool settings_PlaySoundOnInvalidServerStateJSON = true;
        public static string settings_PlayerJoinSoundPath = "";
        public static string settings_PlayerLeaveSoundPath = "";

#if DEBUG
        const string Debug_UseServerStatusTestDataCommandTrigger = "Debug_UseServerStatusTestData";
        const string Debug_FindTestDataWithPlayerCount = "Debug_FindTestDataWithPlayerCount";
        const string Debug_FindNextTeams = "Debug_FindNextTeams";
        const string Debug_FindNextFFA = "Debug_FindNextFFA";
        const string Debug_QuickStart = "Debug_QuickStart";
        const string Debug_SkipData = "Debug_SkipData";
#endif

        #endregion

        #region Keys

        public static class SettingsKeys
        {
            public const string WebhookURL = "WebhookURL";
            public const string WebhookTrigger = "WebhookTrigger";
            public const string WebhookRole = "WebhookRole";

            public const string TitleOption = "TitleOption";

            public const string ServerPrefix = "Server.";
            public const string ServersEncoded = "ServersEncoded";

            public const string ScoreboardFontSize = "ScoreboardFontSize";

            public const string PlaySoundOnPlayerJoin = "PlaySoundOnPlayerJoin"; // bool
            public const string PlayerJoinSoundPath = "PlayerJoinSoundPath"; // file path
            public const string PlaySoundOnPlayerLeave = "PlaySoundOnPlayerLeave"; // bool
            public const string PlayerLeaveSoundPath = "PlayerLeaveSoundPath"; // file path

            public const string TimedCommandsEncoded = "TimedCommandsEncoded";
            public const string GlobalToolCommandsEncoded = "ConditionalCommandsEncoded";

            public const string VoteFilePrefix = "VoteFile";

        }
        public static class SettingsDefaults
        {
            public const string WebhookURL = ""; // idk
            public const string WebhookTrigger = ""; // idk
            public const string WebhookRole = ""; // idk
            public const string TitleOption = ""; // idk
            public const string ServerPrefix = "."; // idk
            public const string TimedCommandsEncoded = ""; // idk
            public const string ScoreboardFontSize = "14";

            //-------------------------------------------------------

            public const string PlaySoundOnPlayerJoin = "false"; // bool - false by default
            public const string PlayerJoinSoundPath = ""; // file path - empty by default - no sound path set
            public const string PlaySoundOnPlayerLeave = "false"; // bool - false by default
            public const string PlayerLeaveSoundPath = ""; // file path - empty by default - no sound path set

            public const string DynamicVotingJsonPath = "";

        }

        #endregion


		public App()
        {

            form = this;
            InitializeComponent();

            if (HasServerConfig())
            {
                LoadSettings();
            }
            
            if (connectionList.Count >= 1)
            {
                currentConnection = connectionList[0];
                SetTextBoxText(textBoxConsoleText, currentConnection.GetConsole());
                SetTextBoxText(textBoxChatText, currentConnection.GetChat());
                SetTextBoxText(textBoxJoinLeaveLog, currentConnection.GetJoinLeaveLog());
                SetTextBoxText(textBoxAppLog, AppLogText);

                foreach (Connection connection in connectionList)
                {
                    if (connection.Settings.Commands == null)
                    {
                        connection.Settings.Commands = new List<ToolCommand>();
                    }
                    connection.Settings.InitializeCommands(connection);
                }
            }

            if(currentConnection == null)
            {
                new ServerManager().ShowDialog();

                if (connectionList.Count < 1) 
                {
                    CloseProgram(); 
                }
                else
                {
                    currentConnection = connectionList[0];
                }
            }

            if (currentConnection == null)
            {
                CloseProgram();
            }

            SetStyle(ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;

            toolStripStatusLabelVersion.Text = "Version: " + toolversion;

            scoreBoardContextMenu = new ContextMenuStrip();
            scoreBoardContextMenu.Items.Add("Kick",null,                (sender, e) => { KickPlayer(sender, e); });
            scoreBoardContextMenu.Items.Add("Ban Temporarily", null,    (sender, e) => { BanPlayerTemporarily(sender, e); });
            scoreBoardContextMenu.Items.Add("Ban Permanently", null,    (sender, e) => { BanPlayerPermanently(sender, e); });
            scoreBoardContextMenu.Items.Add("Copy Name", null,          (sender, e) => { CopyName(sender, e); });
            scoreBoardContextMenu.Items.Add("Copy UID", null,           (sender, e) => { CopyUID(sender, e); });
            scoreBoardContextMenu.Items.Add("Copy Name and UID", null,    (sender, e) => { CopyNameAndUID(sender, e); });

            #region Setup Auto-Complete
            AutoCompleteStringCollection serverCommandsCollection = new AutoCompleteStringCollection();
            serverCommandsCollection.AddRange(
                Properties.Resources.TextFile_ServerCommandTriggers.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            );


#if DEBUG
            serverCommandsCollection.Add(Debug_UseServerStatusTestDataCommandTrigger);
            serverCommandsCollection.Add(Debug_FindTestDataWithPlayerCount);
            serverCommandsCollection.Add(Debug_FindNextTeams);
            serverCommandsCollection.Add(Debug_FindNextFFA);            
            serverCommandsCollection.Add(Debug_QuickStart);
            serverCommandsCollection.Add(Debug_SkipData);
            //textBoxConsoleTextEntry.Text = Debug_QuickStart + " 13";
            textBoxConsoleTextEntry.Text = Debug_UseServerStatusTestDataCommandTrigger;
            new Thread(new ThreadStart(() => {
                Thread.Sleep(500);
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(500);
                SendKeys.SendWait(Debug_FindNextFFA + "{ENTER}");
                //SendKeys.SendWait("Debug_FindTestDataWithPlayerCount 12");
                //SendKeys.SendWait("{ENTER}");

            })).Start();
#else
            TimedCommandsThread = new Thread(new ThreadStart(RunTimedCommands));
            TimedCommandsThread.Start();
#endif
            InterfaceUpdateThread = new Thread(new ThreadStart(UpdateInterface));
            InterfaceUpdateThread.Start();


            textBoxConsoleTextEntry.AutoCompleteCustomSource = serverCommandsCollection;
            textBoxConsoleTextEntry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxConsoleTextEntry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			#endregion
            
            // Enables menu items to be toggled without closing the dropdown
            menuItemSettings.DropDown.Closing += new ToolStripDropDownClosingEventHandler(DropDown_Closing);
            menuItemCommands.DropDown.Closing += new ToolStripDropDownClosingEventHandler(DropDown_Closing);

            Scoreboard.Position = new Point(4, 24);

            UpdateServerSelectDropdown();

        }
        
		public void CloseProgram()
        {
            if (InterfaceUpdateThread != null && InterfaceUpdateThread.IsAlive)
            {
                InterfaceUpdateThread.Abort();
            }
            if (TimedCommandsThread != null && TimedCommandsThread.IsAlive)
            {
                TimedCommandsThread.Abort();
            }
            Process.GetCurrentProcess().Kill();
        }
        public void RunTimedCommands()
        {

            // Timed Commands Execution Tick

            while (true)
            {
                try
                {
                    foreach (ToolCommand command in GlobalToolCommands)
                    {
                        
                        if (!command.Enabled) { continue; }
                        if (command.ConditionType != ToolCommand.Type.EveryXMinutes && command.ConditionType != ToolCommand.Type.Daily)
                        {
                            continue;
                        }

                        if (command.ConditionType == ToolCommand.Type.EveryXMinutes)
                        {
                            if (DateTime.Now >= command.nextRunTime)
                            {

                                command.nextRunTime = command.nextRunTime.AddMinutes(command.RunTime);

                                foreach (string commandString in command.CommandStrings)
                                {
                                    foreach (Connection connection in connectionList)
                                    {
                                        if (connection.RconConnected)
                                        {
                                            connection.SendToRcon(commandString);
                                        }
                                    }
                                }
                            }
                        }
                        else if (command.ConditionType == ToolCommand.Type.Daily)
                        {                                
                            if (DateTime.Now.Hour == command.RunTime && command.triggered == false)
                            {
                                command.triggered = true;
                                foreach (string commandString in command.CommandStrings)
                                {
                                    foreach (Connection connection in connectionList)
                                    {
                                        connection.SendToRcon(commandString);
                                    }
                                }
                            }
                            if (DateTime.Now.Hour != command.RunTime && command.triggered == true)
                            {
                                command.triggered = false;
                            }
                        }

                    }

                    Invalidate();

                    Thread.Sleep(200);

                }
                catch (Exception e)
                {
                    // "Thread was being aborted" happens while quitting,
                    // don't want to attempt to log something while app shuts down
                    if (e.Message != "Thread was being aborted.")
                    {
                        Log("Timed Command Error: " + e.Message);
                    }
                }
            }

        }
        public void UpdateInterface()
        {

            // Update Various Labels and Text Tick
            while (true)
            {
                try
                {
                    
                    // Set Server Connection Info Labels
                    bool state = currentConnection.State.IsValid;
                    SetLabelText(labelServerName, "Name: " + ((state) ? currentConnection.State.Name : ""));
                    SetLabelText(labelHost, "Host: " + ((state) ? currentConnection.State.HostPlayer : ""));
                    SetLabelText(labelSprintEnabled, "Sprint Enabled: " + ((state) ? currentConnection.State.SprintEnabled : ""));
                    SetLabelText(labelAssassinations, "Assassinations: " + ((state) ? currentConnection.State.AssassinationEnabled : ""));
                    SetLabelText(labelMap, "Map: " + ((state) ? currentConnection.State.Map : ""));
                    SetLabelText(labelVariant, "Variant: " + ((state) ? currentConnection.State.Variant : ""));
                    SetLabelText(labelVariantType, "Variant Type: " + ((state) ? currentConnection.State.VariantType : ""));
                    SetLabelText(labelStatus, "Status: " + ((state) ? currentConnection.State.Status : ""));
                    SetLabelText(labelPlayers, "Players: " + ((state) ? currentConnection.State.NumPlayers + "/" + currentConnection.State.MaxPlayers : ""));
                    SetLabelText(labelVersion, "Version: " + ((state) ? currentConnection.State.EldewritoVersion : ""));


                    #region Update Status Text and Icon for Rcon Connection and Stats Connection

                    // Update Stats Connection Image
                    UpdateToolStripStatusLabelImage(toolStripStatusLabelStatsConnection, ((currentConnection.ServerStatusAvailable) ? CheckMarkImage : XMarkImage));

                    // Update Rcon Connection Image
                    UpdateToolStripStatusLabelImage(toolStripStatusLabelRconConnection, ((currentConnection.RconConnected) ? CheckMarkImage : XMarkImage));

					#endregion


					// Set Window Title
					if (currentConnection.State.IsValid && !string.IsNullOrEmpty(titleOption))
                    {
                        if (titleOption.Equals("Server IP"))
                        {
                            SetTitle("Dedicated Rcon Tool - " + currentConnection.Settings.Ip + ":" + currentConnection.Settings.InfoPort);
                        }
                        else if (titleOption.Equals("Server Name"))
                        {
                            SetTitle("Dedicated Rcon Tool - " + currentConnection.State.Name);
                        }
                        else if (titleOption.Equals("Server Game"))
                        {
                            if (currentConnection.State.Status.Equals("InLobby"))
                            {
                                SetTitle("Dedicated Rcon Tool - In Lobby");
                            }
                            else
                            {
                                SetTitle("Dedicated Rcon Tool - " + currentConnection.State.Variant + " on " + currentConnection.State.Map);
                            }
                        }
                        else if (titleOption.Equals("None"))
                        {
                            SetTitle("Dedicated Rcon Tool");
                        }
                    }


                    // Set Toggle Button Text to Disable/Enable
                    if (currentConnection.State.IsValid)
                    {
                        SetButtonLabel(buttonEnableSprint, (currentConnection.State.SprintEnabled == "0" ? "Enable Sprint" : "Disable Sprint"));
                        SetButtonLabel(buttonEnableUnlimitedSprint, (currentConnection.State.SprintUnlimitedEnabled == "0" ? "Enable Unlimited Sprint" : "Disable Unlimited Sprint"));
                        SetButtonLabel(buttonEnableAssasinations, (currentConnection.State.AssassinationEnabled == "0" ? "Enable Assassinations" : "Disable Assassinations"));
                    }


                    Thread.Sleep(200);

                }
                catch (Exception e)
                {
                    Log("Error while updating interface:\n" + e.Message);
                }
            }
        }
        
        /// <summary>
        /// Creates a unique "log file" detailing the kick or ban action that was taken. Creates a new one every time there's a kick or ban. Not great.
        /// </summary>
        public static void LogMessage(ServerSettings serverinfo, string message)
        {
            StringBuilder sb = new StringBuilder();
            string date = DateTime.Now.ToString("[MM-dd-yyyy HH:mm:ss] ");
            string filename = DateTime.Now.ToString("MMddyyyyHHmm");

            sb.Append(date + "[" + serverinfo.Ip + ":" + serverinfo.InfoPort + "] " + message + System.Environment.NewLine);

            File.AppendAllText("log-" + filename + ".txt", sb.ToString());
            sb.Clear();
        }
        
        public static DialogResult ShowMessageBox(string text_mb, string title_mb, MessageBoxButtons buttons_mb, MessageBoxIcon icon_mb)
        {
            return (DialogResult)form.Invoke(
                new Func<string,string,MessageBoxButtons,MessageBoxIcon,DialogResult>(
                    (string text, string title, MessageBoxButtons buttons, MessageBoxIcon icon) =>
                    {
                        return (MessageBox.Show(text, title, buttons, icon));
                    }
                ), 
                new object[] { text_mb, title_mb, buttons_mb, icon_mb }
            );
        }
        
        public static Connection GetConnection(ServerSettings serverInfo)
        {
            foreach (Connection potentialConnection in connectionList)
            {
                if (potentialConnection.Settings.IsSameServerReference(serverInfo))
                {
                    return potentialConnection;
                }
            }
            return null;
        }

        public static FileInfo GetFileInfo(string path)
        {
            try
            {
                return new FileInfo(path);
            }
            catch
            {
                return null;
            }
        }

        protected override bool ProcessTabKey(bool forward)
        {
            Control control = this.ActiveControl;
            if (control != null && control is TextBox)
            {
                TextBox textBox = (TextBox)control;
                if (textBox == textBoxConsoleTextEntry && textBox.Text.Length > 0)
                {
                    return false;
                }
            }
            return base.ProcessTabKey(forward); // process TAB key as normal
        }

        private static void ResizeToFitScoreboard()
        {
            form.MinimumSize = new Size(Scoreboard.Width + 25, 370 + 5 + Scoreboard.Height);
            form.Size = form.MinimumSize;
            ResizeRequired = false;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;

        }

        #region Auto-Update

        public static void AppendConsole(string text)
        {
            form.FormAppendConsole(text);
        }
        private void FormAppendConsole(string text)
        {
            if (autoUpdateEnabled)
            {
                AppendText(textBoxConsoleText, text);
            }
        }

        public static void AppendChat(string text)
        {
            form.FormAppendChat(text);
        }
        private void FormAppendChat(string text)
        {
            if (autoUpdateEnabled)
            {
                AppendText(textBoxChatText, text);
            }
        }

        public static void AppendJoinLeave(string text)
        {
            form.FormAppendJoinLeave(text);
        }
        private void FormAppendJoinLeave(string text)
        {
            if (autoUpdateEnabled)
            {
                AppendText(textBoxJoinLeaveLog, text);
            }
        }

        public void AppendAppLog(string text)
        {
            if (autoUpdateEnabled)
            {
                AppendText(textBoxAppLog, text);
            }
        }

        public static void Log(string message, ServerSettings settings)
        {
            if (settings == null) { message = "[" + DateTime.Now.ToLongTimeString() + "] APP: " + message; }
            else
            {
                message = 
                    "[" + DateTime.Now.ToLongTimeString() + "] " +
                    (string.IsNullOrWhiteSpace(settings.Name) 
                        ? " (" + settings.Ip + ":" + settings.InfoPort + ")"
                        : settings.Name
                    )
                    + ": " + message;
            }
            
            message = System.Text.RegularExpressions.Regex.Replace(
                message, @"\r\n?|\n", System.Environment.NewLine
            ) + System.Environment.NewLine;

            AppLogText += message;
            form.AppendAppLog(message);
        }
        public static void Log(string message, Connection connection = null)
        {
            Log(message, connection?.Settings ?? null);
        }

        #endregion

		#region Loading, Saving, and Settings

        public static void LoadSettings()
        {

            webhook = LoadSetting(SettingsKeys.WebhookURL);
            webhookTrigger = LoadSetting(SettingsKeys.WebhookTrigger);
            webhookRole = LoadSetting(SettingsKeys.WebhookRole);

            titleOption = LoadSetting(SettingsKeys.TitleOption);

            LoadServers();
            LoadGlobalToolCommands();

            #region Load Join/Leave Sound Settings

            settings_PlaySoundOnPlayerJoin = bool.Parse(
                LoadSettingOrDefault(SettingsKeys.PlaySoundOnPlayerJoin, SettingsDefaults.PlaySoundOnPlayerJoin)
            );
            settings_PlaySoundOnPlayerLeave = bool.Parse(
                LoadSettingOrDefault(SettingsKeys.PlaySoundOnPlayerLeave, SettingsDefaults.PlaySoundOnPlayerLeave)
            );
            settings_PlayerJoinSoundPath = LoadSettingOrDefault(SettingsKeys.PlayerJoinSoundPath, SettingsDefaults.PlayerJoinSoundPath);
            settings_PlayerLeaveSoundPath = LoadSettingOrDefault(SettingsKeys.PlayerLeaveSoundPath, SettingsDefaults.PlayerLeaveSoundPath);

            form.SetToolStripMenuItemCheckedState(form.toolStripMenuItemPlaySoundOnPlayerJoin, settings_PlaySoundOnPlayerJoin);
            form.SetToolStripMenuItemCheckedState(form.toolStripMenuItemPlaySoundOnPlayerLeave, settings_PlaySoundOnPlayerLeave);

            #endregion

            // Load scoreboard settings
            int fontSize = Scoreboard.InitialFontSize;
            int.TryParse(LoadSettingOrDefault(SettingsKeys.ScoreboardFontSize, SettingsDefaults.ScoreboardFontSize), out fontSize);
            Scoreboard.FontSize = fontSize;
            Scoreboard.Layout();

        }

		public static string LoadSettingOrDefault(string key, string defaultValue)
        {
            string result = ConfigurationManager.AppSettings[key];
            if (result == null) { return defaultValue; }
            else { return result; }
        }

        public static string LoadSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void RemoveSetting(string key)
        {
            config.AppSettings.Settings.Remove(key);
        }

        public static bool HasServerConfig()
        {
            if (string.IsNullOrEmpty(LoadSetting(SettingsKeys.ServerPrefix + "0")))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Config Set");
                return true;
            }            
        }
                
        private static void LoadGlobalToolCommands()
        {
            if (LoadSetting(SettingsKeys.GlobalToolCommandsEncoded) != null)
            {
                string json = Base64Decode(LoadSetting(SettingsKeys.GlobalToolCommandsEncoded));
                try
                {
                    GlobalToolCommands = JsonConvert.DeserializeObject<List<ToolCommand>>(json);
                }
                catch
                {
                    GlobalToolCommands = new List<ToolCommand>();
                }
            }
            foreach (ToolCommand command in GlobalToolCommands)
            {
                if (command.IsGlobalToolCommand == false) { command.IsGlobalToolCommand = true; }
            }
            GlobalToolCommands.RemoveAll(x => x.ConditionType != ToolCommand.Type.Daily && x.ConditionType != ToolCommand.Type.EveryXMinutes);

            PopulateConditionalCommandsDropdown();

        }

        private delegate void PopulateConditionCommandsDropdownCallback();
        public static void PopulateConditionalCommandsDropdown()
        {
            if (form.menuBarApp.InvokeRequired)
            {
                PopulateConditionCommandsDropdownCallback d = new PopulateConditionCommandsDropdownCallback(DoPopulateConditionalCommandsDropdown);
                form.menuBarApp.Invoke(d, new object[] {});
            }
            else
            {
                DoPopulateConditionalCommandsDropdown();
            }
        }
        private static void DoPopulateConditionalCommandsDropdown()
        {
            ToolStripItemCollection dropdownItems = form.menuItemCommands.DropDownItems;

            ToolStripItem manageCommandsOption = dropdownItems[0];

            dropdownItems.Clear();

            dropdownItems.Add(manageCommandsOption);

            if (currentConnection != null && currentConnection.Settings.Commands.Count > 0)
            {

                dropdownItems.Add("- Current Server Commands -");

                foreach (ToolCommand command in currentConnection.Settings.Commands)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(command.Name)
                    {
                        CheckOnClick = true,
                        Checked = command.Enabled,
                        Name = command.Name
                    };
                    item.Click += new EventHandler((o, e) =>
                    {
                        command.ToggleEnabled();
                    });
                    dropdownItems.Add(item);
                }

            }

            if (GlobalToolCommands.Count > 0)
            {

                dropdownItems.Add("- Global Commands -");

                foreach (ToolCommand command in GlobalToolCommands)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(command.Name)
                    {
                        CheckOnClick = true,
                        Checked = command.Enabled,
                        Name = command.Name
                    };
                    item.Click += new EventHandler((o, e) =>
                    {
                        command.ToggleEnabled();
                    });
                    dropdownItems.Add(item);
                }

            }

        }
        public static void SaveConditionalCommands()
        {
            SaveSetting(SettingsKeys.GlobalToolCommandsEncoded, SerializeList(GlobalToolCommands));
        }
        private static string SerializeList<T>(List<T> list)
        {
            return
            Convert.ToBase64String(
                Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(list)
                )
            );
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static void LoadServers()
        {

            List<ServerSettings> serverSettings = null;
            try
            {
                string json = Base64Decode(LoadSetting(SettingsKeys.ServersEncoded));
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<ServerSettings>));
                    serverSettings = (List<ServerSettings>)deserializer.ReadObject(ms);
                }
            }
            catch (Exception e)
            {
                App.Log("Error Loading Saved Servers: " + e.Message);
            }

            if (serverSettings == null || serverSettings.Count == 0)
            {
                App.Log("No saved servers to load.");
                return;
            }

            foreach (ServerSettings settings in serverSettings)
            {
                try
                {
                    settings.LoadAllVoteFilesFromDisk();
                    new Connection(settings);
                }
                catch (Exception e)
                {
                    App.Log("Error creating connection from saved server settings: " + e.Message);
                }
            }

        }
        
        public static void SaveServers()
        {

            List<ServerSettings> serverSettings = new List<ServerSettings>();
            foreach (Connection connection in connectionList)
            {
                serverSettings.Add(connection.Settings);
            }
            string servers = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(serverSettings)));
            SaveSetting(SettingsKeys.ServersEncoded, servers);

        }

        public static void SaveSettings()
        {

            SaveSetting(SettingsKeys.ScoreboardFontSize, Scoreboard.FontSize.ToString());
            SaveSetting(SettingsKeys.WebhookTrigger, webhookTrigger);
            SaveSetting(SettingsKeys.WebhookRole, webhookRole);
            SaveSetting(SettingsKeys.WebhookURL, webhook);                        
            SaveSetting(SettingsKeys.TitleOption, titleOption);
            
            SaveServers();
            
            SaveConditionalCommands();

            UpdateServerSelectDropdown();

        }

        public static void SaveConfigToFile()
        {
            config.Save(ConfigurationSaveMode.Full);
        }

        public static void SaveSetting(string Key, string value)
        {
            // Remove does not throw an exception if the object is not found.
            config.AppSettings.Settings.Remove(Key);
            config.AppSettings.Settings.Add(Key, value);
            SaveConfigToFile();
        }

        #endregion

        #region Form/App Events

        private void App_Paint(object sender, PaintEventArgs e)
        {
            if (ResizeRequired) { ResizeToFitScoreboard(); }
            Scoreboard.DrawScoreboard(sender, e);
        }

        private void App_MouseMove(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
        }

        private void App_MouseDown(object sender, MouseEventArgs e)
        {

            if ((bool)scoreBoardContextMenu?.Visible) { scoreBoardContextMenu.Close(); }

            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    if (contextPlayer != null && !contextPlayer.Name.Equals(""))
                    {
                        scoreBoardContextMenu.Items[0].Text = "Kick " + contextPlayer.Name;
                        scoreBoardContextMenu.Items[1].Text = "Temp. Ban " + contextPlayer.Name;
                        scoreBoardContextMenu.Items[2].Text = "Perm. Ban " + contextPlayer.Name;
                        this.scoreBoardContextMenu.Show(this, new Point(e.X, e.Y));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generating Scoreboard Context Menu: " + ex.Message);
                }
            }

        }

        private void App_MouseLeave(object sender, EventArgs e)
        {
            mousePoint = new Point(-1, -1);
        }

        private void formApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseProgram();
        }

        private void formApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseProgram();
        }

        #endregion

        #region Callback Delegates

        // Controls in Windows Forms are bound to a specific thread and are not thread safe.
        // Therefore, if you are calling a control's method from a different thread, 
        // you must use one of the control's invoke methods to marshal the call to the proper thread.

        delegate void UpdateServerSelectDropdownCallback();
        public static void UpdateServerSelectDropdown()
        {
            form._UpdateServerSelectDropdown();
        }
        private void _UpdateServerSelectDropdown()
        {
            if (statusStripStatusInformation.InvokeRequired)
            {
                UpdateServerSelectDropdownCallback d = new UpdateServerSelectDropdownCallback(DoUpdateServerSelectDropdown);
                statusStripStatusInformation.Invoke(d, new object[] { });
            }
            else
            {
                DoUpdateServerSelectDropdown();
            }
        }
        private void DoUpdateServerSelectDropdown()
        {   

            // Clear dropdown
            toolStripSplitButtonServerSelect.DropDownItems.Clear();

            // Update Server Select Dropdown Options
            for (int i = 0; i < connectionList.Count; i++)
            {

                if (connectionList[i] != null)
                {

                    string id = connectionList[i].Settings.Identifier;

                    // Add a dropdown item for this server connection
                    toolStripSplitButtonServerSelect.DropDownItems.Add(new ToolStripMenuItem(id));
                    toolStripSplitButtonServerSelect.DropDownItems[i].Tag = i.ToString();

                    // Set dropdown text to selected server name
                    if (connectionList[i] == currentConnection)
                    {
                        toolStripSplitButtonServerSelect.Text = id;
                        //toolStripSplitButtonServerSelect.Image = Properties.Resources.Image_ServerIcon16x16;
                        //toolStripSplitButtonServerSelect.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                        //toolStripSplitButtonServerSelect.AutoSize = true;
                        ((ToolStripMenuItem)toolStripSplitButtonServerSelect.DropDownItems[i]).Checked = true;
                        ((ToolStripMenuItem)toolStripSplitButtonServerSelect.DropDownItems[i]).CheckState = CheckState.Checked;
                    }
                    else
                    {
                        toolStripSplitButtonServerSelect.DropDownItems[i].BackColor = DefaultBackColor;
                    }

                }

            }

            //toolStripSplitButtonServerSelect.Invalidate();

        }

        delegate void UpdateToolStripStatusLabelImageCallback(ToolStripStatusLabel label, Image image);
        private void UpdateToolStripStatusLabelImage(ToolStripStatusLabel label, Image image)
        {
            if (statusStripStatusInformation.InvokeRequired)
            {
                UpdateToolStripStatusLabelImageCallback de = new UpdateToolStripStatusLabelImageCallback(DoUpdateToolStripStatusLabelImage);
                statusStripStatusInformation.Invoke(de, new object[] { label, image});
            }
            else
            {
                DoUpdateToolStripStatusLabelImage(label, image);
            }
        }
        private void DoUpdateToolStripStatusLabelImage(ToolStripStatusLabel label, Image image)
        {
            label.Image = image;
        }

        delegate void AppendTextCallback(TextBox textBox, string text);
        private void AppendText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(DoAppendText);
                textBox.Invoke(d, new object[] { textBox, text});
            }
            else
            {
                DoAppendText(textBox, text);
            }
        }
        private void DoAppendText(TextBox textBox, string text)
        {
            int caretPos;
            if (autoScroll)
            {
                textBox.Text += text;
                caretPos = textBox.Text.Length;
            }
            else
            {
                caretPos = textBox.Text.Length;
                textBox.Text += text;
            }
            textBox.Select(caretPos, 0);
            textBox.ScrollToCaret();
        }

        delegate void SetTextBoxTextCallback(TextBox textbox, string text);
        public void SetTextBoxText(TextBox textbox, string text)
        {
            if (textbox.InvokeRequired)
            {
                SetTextBoxTextCallback de = new SetTextBoxTextCallback(DoSetTextBoxText);
                textbox.Invoke(de, new object[] { textbox, text });
            }
            else
            {
                DoSetTextBoxText(textbox, text);
            }
        }
        private void DoSetTextBoxText(TextBox textbox, string text)
        {
            textbox.Text = text;
        }

        delegate void SetLabelTextCallback(Label label, string text);
        private void SetLabelText(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                SetLabelTextCallback de = new SetLabelTextCallback(DoSetLabelText);
                label.Invoke(de, new object[] { label, text });
            }
            else
            {
                DoSetLabelText(label, text);
            }
        }
        private void DoSetLabelText(Label label, string text)
        {
            label.Text = text;
        }

        delegate void SetButtonCallback(Button label, string text);
        public void SetButtonLabel(Button button, string text)
        {
            if (button.InvokeRequired)
            {
                SetButtonCallback d = new SetButtonCallback(DoSetButtonLabel);
                button.Invoke(d, new object[] { button, text });
            }
            else
            {
                DoSetButtonLabel(button, text);
            }
        }
        private void DoSetButtonLabel(Button button, string text)
        {
            button.Text = text;
        }

        delegate void SetTitleCallback(string text);
        public void SetTitle(string text)
        {
            if (InvokeRequired)
            {
                SetTitleCallback d = new SetTitleCallback(DoSetTitle);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                DoSetTitle(text);
            }
        }
        private void DoSetTitle(string text)
        {
            Text = text;
        }

        delegate void SetScoreboardDataCallback(DataGridView dataGridView, BindingSource source);
        public void SetScoreboardData(DataGridView dataGridView, BindingSource source)
        {
            if (dataGridView.InvokeRequired)
            {
                SetScoreboardDataCallback d = new SetScoreboardDataCallback(DoSetScoreboardData);
                dataGridView.Invoke(d, new object[] { dataGridView, source });
            }
            else
            {
                DoSetScoreboardData(dataGridView, source);
            }
        }
        private void DoSetScoreboardData(DataGridView dataGridView, BindingSource source)
        {
            dataGridView.DataSource = source;
        }

        delegate void SetMenuItemCheckedStateCallback(MenuItem item, bool checkedState);
        public void SetMenuItemCheckedState(MenuItem item, bool checkedState)
        {
            if (InvokeRequired)
            {
                SetMenuItemCheckedStateCallback d = new SetMenuItemCheckedStateCallback(DoSetMenuItemCheckedState);
                this.Invoke(d, new object[] { item, checkedState });
            }
            else
            {
                DoSetMenuItemCheckedState(item, checkedState);
            }
        }
        private void DoSetMenuItemCheckedState(MenuItem item, bool checkedState)
        {
            item.Checked = checkedState;
        }


        delegate void SetToolStripMenuItemCheckedStateCallback(ToolStripMenuItem item, bool checkedState);
        public void SetToolStripMenuItemCheckedState(ToolStripMenuItem item, bool checkedState)
        {

            if (InvokeRequired)
            {
                SetToolStripMenuItemCheckedStateCallback d = new SetToolStripMenuItemCheckedStateCallback(DoSetToolStripMenuItemCheckedState);
                this.Invoke(d, new object[] { item, checkedState });
            }
            else
            {
                DoSetToolStripMenuItemCheckedState(item, checkedState);
            }
        }
        private void DoSetToolStripMenuItemCheckedState(ToolStripMenuItem item, bool checkedState)
        {
            item.Checked = checkedState;
            if (checkedState) { item.CheckState = CheckState.Checked; }
            else { item.CheckState = CheckState.Unchecked; }
        }

        protected void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }

        #endregion
        
        #region Menu Items

        //TODO Update mobile app and config stuff
        private void menuItemGenerateAppConfig_Click(object sender, EventArgs e)
        {
            //new ConfigExport().ShowDialog();
        }
        private void menuItemDownloadTheAndroidApp_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://play.google.com/store/apps/details?id=jaron.rcontool.com.rcontool");
        }
        
        private void menuItemManageCommands_Click(object sender, EventArgs e)
        {
            new ToolCommandManager().ShowDialog();
        }

        private void menuItemRetryConnection_Click(object sender, EventArgs e)
        {
            if (currentConnection != null)
            {
                currentConnection.ResetRconConnection();
            }
        }
        
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            CloseProgram();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void menuItemManageServers_Click(object sender, EventArgs e)
        {
            new ServerManager().ShowDialog();
        }
        
        private void toolStripMenuItemPlaySoundOnPlayerJoin_Click(object sender, EventArgs e)
        {
            settings_PlaySoundOnPlayerJoin = !settings_PlaySoundOnPlayerJoin;
            //SetToolStripMenuItemCheckedState(toolStripMenuItemPlaySoundOnPlayerJoin, settings_PlaySoundOnPlayerJoin);
            SaveSetting(SettingsKeys.PlaySoundOnPlayerJoin, settings_PlaySoundOnPlayerJoin.ToString());
        }

        private void toolStripMenuItemPlaySoundOnPlayerLeave_Click(object sender, EventArgs e)
        {
            settings_PlaySoundOnPlayerLeave = !settings_PlaySoundOnPlayerLeave;
            //SetToolStripMenuItemCheckedState(toolStripMenuItemPlaySoundOnPlayerLeave, settings_PlaySoundOnPlayerLeave);
            SaveSetting(SettingsKeys.PlaySoundOnPlayerLeave, settings_PlaySoundOnPlayerLeave.ToString());
        }

        private void toolStripMenuItemConfigureSettings_Click(object sender, EventArgs e)
        {
            new ServerSettingsEditor().ShowDialog();
        }

        private void toolStripMenuItemChangeScoreboardFontSize_Click(object sender, EventArgs e)
        {
            Scoreboard.FontSize = Prompt.ShowIntDialog(
                "Select a font size for the scoreboard text.",
                "Change Scoreboard Font Size",
                Scoreboard.FontSize
            );
            if (Scoreboard.FontSize < 4) { Scoreboard.FontSize = 4; }
            SaveSettings();
            Scoreboard.Layout();            
            ResizeToFitScoreboard();
        }

        #endregion

        #region Console Interface

        private void textBoxConsoleText_TextChanged(object sender, EventArgs e)
        {
            if (autoScroll)
            {
                textBoxConsoleText.SelectionStart = textBoxConsoleText.TextLength;
                textBoxConsoleText.ScrollToCaret();
            }
        }

        private void textBoxConsoleTextEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (textBoxConsoleTextEntry.Text.Length == 0)
                {
                    //textBoxConsoleTextEntry.
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                while (textBoxConsoleTextEntry.Text.Contains("\r"))
                {
                    textBoxConsoleTextEntry.Text = textBoxConsoleTextEntry.Text.Replace("\r", "");
                }

#if DEBUG
                HandleDebugCommands(sender, e);
#endif

                buttonConsoleTextSend_Click(this, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true;


            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (textBoxConsoleTextEntry.Text != "")
                {
                    ProcessTabKey(false);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }
        
        private void buttonConsoleTextSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxConsoleTextEntry.Text))
            {
                currentConnection.PrintToConsole(textBoxConsoleTextEntry.Text);
                currentConnection.SendToRcon(textBoxConsoleTextEntry.Text);
                textBoxConsoleTextEntry.Clear();
            }
        }

        private void buttonConsoleTextClear_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to clear?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.ClearConsole();
                SetTextBoxText(textBoxConsoleText, "");
            }
        }
#if DEBUG
        private void HandleDebugCommands(object sender, KeyEventArgs e)
        {
            // Handle server status test data commands
            if (textBoxConsoleTextEntry.Text.StartsWith("Debug_"))
            {

                e.Handled = true;
                e.SuppressKeyPress = true;

                string[] args = textBoxConsoleTextEntry.Text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (args == null || args.Length < 1) { 
                    currentConnection.PrintToConsole("Debug command failed, unable to parse command or arguments."); 
                    textBoxConsoleTextEntry.Text = ""; 
                    return; 
                }

                switch (args[0])
                {
                    case Debug_UseServerStatusTestDataCommandTrigger:
                        foreach (Connection c in connectionList) { c.BeginUseServerStatusTestData(); }
                        break;
                    case Debug_FindTestDataWithPlayerCount:
                        FindTestDataWithPlayerCount(args);
                    break;
                    case Debug_QuickStart:
                        foreach (Connection c in connectionList) { c.BeginUseServerStatusTestData(); }
                        FindTestDataWithPlayerCount(args);
                        break;
                    case Debug_FindNextFFA:
                        FindNextTestDataWithFFA(currentConnection);
                        break;
                    case Debug_FindNextTeams:
                        FindNextTestDataWithTeams(currentConnection);
                        break;
                    case Debug_SkipData:
                        if (args.Length == 2)
                        {
                            int x = 0;
                            if (int.TryParse(args[1], out x) && x > 0)
                            {
                                if (x > currentConnection.ServerStatusTestData.Count) {
                                    currentConnection.ServerStatusTestData.Clear();
                                    currentConnection.LoadServerStatusTestData(); 
                                }
                                else
                                {
                                    for (int i = 0; i < x; i++)
                                    {
                                        currentConnection.ServerStatusTestData.Dequeue();
                                    }                                    
                                }
                            }
                        }
                        else
                        {
                            currentConnection.PrintToConsole("Debug command failed, unable to parse command or arguments.");
                            textBoxConsoleTextEntry.Text = "";
                        }
                        break;

                }

                textBoxConsoleTextEntry.Text = "";
                return;
            }
        }

        private void SkipData(Connection c, int count)
        {

        }

        private void FindTestDataWithPlayerCount(string[] args, bool allConnections = true)
        {
            // validate args
            bool validCommand = true;
            if (args.Length != 2) { validCommand = false; }
            for (int i = 0; i < args[1].Length; i++) { if (!char.IsDigit(args[1][i])) { validCommand = false; break; } }
            if (!validCommand) { currentConnection.PrintToConsole("Command failed, numeric argument required."); return; }

            if (allConnections)
            {
                foreach (Connection c in connectionList)
                {
                    FindTestDataWithPlayerCount(args[1], c);
                }
            }
            else
            {
                FindTestDataWithPlayerCount(args[1], currentConnection);
            }
        }
        private void FindTestDataWithPlayerCount(string playerCount, Connection c)
        {

            // For each connection, dequeue each test data list until the first string with the correct numPlayers argument is found, else log failure

            bool searching = true;
            int count = 0;
            while (searching && c.ServerStatusTestData.Count > 0)
            {
                if (!c.ServerStatusTestData.Peek().Contains("\"numPlayers\":" + playerCount))
                {
                    count++;
                    c.ServerStatusTestData.Dequeue();
                }
                else { searching = false; }
            }
            if (searching) { c.PrintToConsole("Unable to find test data with " + playerCount + " players."); }
            else { c.PrintToConsole("Found test data with " + playerCount + " players. Skipped " + count + " test data entries."); }
        }

        private void FindTestDataWithTeams(Connection c)
        {
            FindTestDataWithTeamMatchStatus(c, true);
        }
        private void FindTestDataWithFFA(Connection c)
        {
            FindTestDataWithTeamMatchStatus(c, false);
        }
        private void FindNextTestDataWithTeams(Connection c)
        {
            if (DataIsTeams(c.ServerStatusTestData.Peek())) { FindTestDataWithFFA(c); }
            FindTestDataWithTeams(c);
        }
        private void FindNextTestDataWithFFA(Connection c)
        {
            if (DataIsFFA(c.ServerStatusTestData.Peek()))  { FindTestDataWithTeams(c); }
            FindTestDataWithFFA(c);
        }
        private bool DataIsFFA(string data)
        {
            return data.Contains("\"teams\":false");
        }
        private bool DataIsTeams(string data)
        {
            return !DataIsFFA(data);
        }
        private void FindTestDataWithTeamMatchStatus(Connection c, bool teams)
        {

            if (c.ServerStatusTestData.Count == 0) { c.LoadServerStatusTestData(); }

            // For each connection, dequeue each test data list until the first string with FFA teams is found
            string target = ((teams) ? "teams" : " a FFA gametype");
            bool searching = true;
            int count = 0;
            while (searching && c.ServerStatusTestData.Count > 0)
            {
                if (DataIsTeams(c.ServerStatusTestData.Peek()) != teams)
                {
                    count++;
                    c.ServerStatusTestData.Dequeue();
                }
                else { searching = false; }
            }
            if (searching) { c.PrintToConsole("Unable to find test data with " + target + "."); }
            else { c.PrintToConsole("Found test data with " + target + ". Skipped " + count + " test data entries."); }

            if (c.ServerStatusTestData.Count == 0) { c.LoadServerStatusTestData(); }

        }

#endif
#endregion

        #region Chat Interface

        private void textBoxChatText_TextChanged(object sender, EventArgs e)
        {
            if (autoScroll)
            {
                textBoxChatText.SelectionStart = textBoxChatText.TextLength;
                textBoxChatText.ScrollToCaret();
            }
        }

        private void textBoxChatTextEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(textBoxChatTextEntry.Text))
                {
                    currentConnection.Broadcast(textBoxChatTextEntry.Text);
                    textBoxChatTextEntry.Clear();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void buttonChatTextSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxConsoleTextEntry.Text))
            {
                currentConnection.Broadcast(textBoxChatTextEntry.Text);
                textBoxChatTextEntry.Clear();
            }
        }

        private void buttonChatTextClear_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to clear?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.ClearChat();
                SetTextBoxText(textBoxChatText, "");
            }
        }

        #endregion

        #region Server Controls Interface

        private void buttonEnableSprint_Click(object sender, EventArgs e)
        {
            string ln = (currentConnection.State.SprintEnabled == "1" ? "0" : "1");
            currentConnection.SendToRcon("Server.SprintEnabled " + ln);
            currentConnection.PrintToConsole("Server.SprintEnabled " + ln);
        }

        private void buttonEnableUnlimitedSprint_Click(object sender, EventArgs e)
        {
            string ln = (currentConnection.State.SprintUnlimitedEnabled == "1" ? "0" : "1");
            currentConnection.SendToRcon("Server.UnlimitedSprint " + ln);
            currentConnection.PrintToConsole("Server.UnlimitedSprint " + ln);
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to Start the game?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.SendToRcon("Game.Start");
                currentConnection.PrintToConsole("Game.Start");
            }
        }

        private void buttonStopGame_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to Stop the game?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.SendToRcon("Game.stop");
                currentConnection.PrintToConsole("Game.stop");
            }
        }

        private void buttonEnableAssasinations_Click(object sender, EventArgs e)
        {
            string ln = (currentConnection.State.AssassinationEnabled == "1" ? "0" : "1");
            currentConnection.SendToRcon("Server.AssassinationEnabled " + ln);
            currentConnection.PrintToConsole("Server.AssassinationEnabled " + ln);
        }

        private void buttonSetMaxPlayers_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to set the max amount of players?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.SendToRcon("Server.MaxPlayers " + textBoxMaxPlayersCount.Text);
                currentConnection.PrintToConsole("Server.MaxPlayers " + textBoxMaxPlayersCount.Text);
            }
        }

        private void buttonEnableTeamShuffle_Click(object sender, EventArgs e)
        {
            string ln = "1";
            currentConnection.SendToRcon("Server.TeamShuffleEnabled " + ln);
            currentConnection.PrintToConsole("Server.TeamShuffleEnabled " + ln);
        }

        private void buttonDisableTeamShuffle_Click(object sender, EventArgs e)
        {
            string ln = "0";
            currentConnection.SendToRcon("Server.TeamShuffleEnabled " + ln);
            currentConnection.PrintToConsole("Server.TeamShuffleEnabled " + ln);
        }
        
        private void buttonReloadVotingJson_Click(object sender, EventArgs e)
        {
            currentConnection.SendToRcon("Server.ReloadVotingJson");
            currentConnection.PrintToConsole("Server.ReloadVotingJson");
        }

        private void buttonShuffleTeams_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to shuffle the teams?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.SendToRcon("Server.ShuffleTeams");
                currentConnection.PrintToConsole("Server.ShuffleTeams");
            }
        }

        private void buttonReloadVetoJson_Click(object sender, EventArgs e)
        {
            currentConnection.SendToRcon("Server.ReloadVetoJson");
            currentConnection.PrintToConsole("Server.ReloadVetoJson");
        }

        #endregion

        #region Join Leave Log Interface

        private void buttonJoinLeaveLogTextClear_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to clear?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.ClearJoinLeave();
                SetTextBoxText(textBoxJoinLeaveLog, "");
            }
        }

        #endregion

        #region Scoreboard Context Menu Options

        private void CopyUID(object sender, EventArgs e)
        {
            Clipboard.SetText(contextPlayer.Uid);
            currentConnection.PrintToConsole("Copied " + contextPlayer.Uid + " to Clipboard");
        }

        private void CopyName(object sender, EventArgs e)
        {
            Clipboard.SetText(contextPlayer.Name);
            currentConnection.PrintToConsole("Copied " + contextPlayer.Name + " to Clipboard");
        }

        private void CopyNameAndUID(object sender, EventArgs e)
        {
            Clipboard.SetText(contextPlayer.Name + ":" + contextPlayer.Uid);
            currentConnection.PrintToConsole("Copied " + contextPlayer.Name + ":" + contextPlayer.Uid + " to Clipboard");
        }

        private void KickPlayer(object sender, System.EventArgs e)
        {
            if (currentConnection.RconConnected && contextPlayer.IsValid)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to kick Player: " + contextPlayer.Name + ":" + contextPlayer.Uid, "Kick Player", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    currentConnection.PrintToConsole("Kicking: " + contextPlayer.Name + "/" + contextPlayer.Uid + "");
                    currentConnection.SendToRcon("Server.KickUid " + contextPlayer.Uid);
                    LogMessage(currentConnection.Settings, "Server.KickUid " + contextPlayer.Uid + " - " + contextPlayer.Name);
                }
            }
        }

        private void BanPlayerPermanently(object sender, System.EventArgs e)
        {
            if (currentConnection.RconConnected && contextPlayer.IsValid)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to permanently ban Player: " + contextPlayer.Name + ":" + contextPlayer.Uid + "?", "Permanently Ban Player", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    currentConnection.PrintToConsole("Permanently Banning: " + contextPlayer.Name + "/" + contextPlayer.Uid + "");
                    currentConnection.SendToRcon("Server.KickBanUid " + contextPlayer.Uid);
                    LogMessage(currentConnection.Settings, "Server.KickBanUid " + contextPlayer.Uid + " - " + contextPlayer.Name);
                }
            }
        }

        private void BanPlayerTemporarily(object sender, System.EventArgs e)
        {
            if (currentConnection.RconConnected && contextPlayer.IsValid)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to temporarily ban Player: " + contextPlayer.Name + ":" + contextPlayer.Uid + "?", "Temporarily Ban Player", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    currentConnection.PrintToConsole("Temporarily Banning: " + contextPlayer.Name + "/" + contextPlayer.Uid + "");
                    currentConnection.SendToRcon("Server.KickTempBanUid " + contextPlayer.Uid);
                    LogMessage(currentConnection.Settings, "Server.KickTempBanUid " + contextPlayer.Uid + " - " + contextPlayer.Name);
                }
            }
        }

        #endregion

        #region Status Bar Interface

        private void toolStripSplitButtonServerSelect_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Tag.ToString() != selectedServerTag)
            {

                int selectedServerIndex = int.Parse(e.ClickedItem.Tag.ToString());
                if (selectedServerIndex <= connectionList.Count)
                {
                    lock (currentConnection.State.ServerStateLock)
                    {
                        currentConnection = connectionList[selectedServerIndex];
                    }
                }
                SetTextBoxText(textBoxConsoleText, currentConnection.GetConsole());
                SetTextBoxText(textBoxChatText, currentConnection.GetChat());
                SetTextBoxText(textBoxJoinLeaveLog, currentConnection.GetJoinLeaveLog());
                SetTextBoxText(textBoxAppLog, AppLogText);
                toolStripSplitButtonServerSelect.Text = e.ClickedItem.Text;
                selectedServerTag = e.ClickedItem.Tag.ToString();

            }

            foreach (ToolStripMenuItem item in toolStripSplitButtonServerSelect.DropDownItems)
            {
                if (item.Tag.ToString() != selectedServerTag)
                {
                    item.Checked = false;
                    item.CheckState = CheckState.Unchecked;
                }
                else
                {
                    item.Checked = true;
                    item.CheckState = CheckState.Checked;
                }
            }

        }

        private void toolStripSplitButtonServerSelect_Click(object sender, EventArgs e)
        {
            toolStripSplitButtonServerSelect.ShowDropDown();
        }

        private void toolStripSplitButtonAutoUpdate_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.Contains("Enabled"))
            {
                toolStripSplitButtonAutoUpdate.Image = Properties.Resources.Image_CheckMark32x32;
                toolStripMenuItemAutoUpdateEnabled.Checked = true;
                toolStripMenuItemAutoUpdateEnabled.CheckState = CheckState.Checked;
                toolStripMenuItemAutoUpdateDisabled.Checked = false;
                toolStripMenuItemAutoUpdateDisabled.CheckState = CheckState.Unchecked;
                autoUpdateEnabled = true;
                autoScroll = true;
                SetTextBoxText(textBoxConsoleText, currentConnection.GetConsole());
                SetTextBoxText(textBoxChatText, currentConnection.GetChat());
            }
            else
            {
                toolStripSplitButtonAutoUpdate.Image = Properties.Resources.Image_XMark32x32;
                toolStripMenuItemAutoUpdateEnabled.Checked = false;
                toolStripMenuItemAutoUpdateEnabled.CheckState = CheckState.Unchecked;
                toolStripMenuItemAutoUpdateDisabled.Checked = true;
                toolStripMenuItemAutoUpdateDisabled.CheckState = CheckState.Checked;
                autoUpdateEnabled = false;
                autoScroll = false;
            }
        }

        private void toolStripSplitButtonAutoUpdate_Click(object sender, EventArgs e)
        {
            toolStripSplitButtonAutoUpdate.ShowDropDown();
        }

        private void toolStripResizeToFitScoreboard_ButtonClick(object sender, EventArgs e)
        {
            ResizeToFitScoreboard();
        }

        private void toolStripFontSizeIncrease_ButtonClick(object sender, EventArgs e)
        {
            Scoreboard.FontSize += 1;
            SaveSettings();
            Scoreboard.Layout();
            ResizeToFitScoreboard();
        }

        private void toolStripFontSizeDecrease_ButtonClick(object sender, EventArgs e)
        {
            if (Scoreboard.FontSize < 4) { return; }
            Scoreboard.FontSize -= 1;
            SaveSettings();
            Scoreboard.Layout();
            ResizeToFitScoreboard();
        }

        #endregion


    }

}
