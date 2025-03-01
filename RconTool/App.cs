using Google.Cloud.Translation.V2;
using RconTool.Properties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RconTool
{

	public partial class App : Form
    {

        //NOTE
        // Controls in Windows Forms are bound to a specific thread and are not thread safe.
        // Therefore, if you are calling a control's method from a different thread, 
        // you must use one of the control's invoke methods to marshal the call to the proper thread.

        //TODO update github readme with list of runtimecommands and other features

        #region Properties/Fields

        public static App form;

        public static Configuration SettingsFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

        public static bool Debug_RconCommandQueueLog = false;

        public static string AppLogText = "";

        public const string toolversion = "3.51";
        public const string InternalAppName = "RCON Tool";
        public const string ChatMessageIncomingDateTimeFormatString = "MM/dd/yy hh:mm:ss";
		public const string ChatMessageDateTimeFormatString = "MM/dd/yy hh:mm:ss tt";

        public const string DefaultDynamicVotingJsonPath = "mods/server/dynamic.json";
        public const string defaultVotingJsonPath = "mods/server/voting.json";

		/// <summary>
		/// By default the tool ignores JSON messages, identified by a first character of '{'<br/>
        /// If this is set to true, the <see cref="Connection.OnRconWebsocketMessage(object, WebSocketSharp.MessageEventArgs)"/> method will not process JSON messages in any way.
		/// </summary>
		public static bool FilterServerJson { get; set; } = true;

        public static TranslationClient TranslationClient { get; private set; }
        private static SavedSetting<string> TranslationAPIKey = new SavedSetting<string>(SettingsKeys.TranslationApiKey, "", true, false);
        public static SavedSetting<int> TranslatedCharactersThisBillingCycle = new SavedSetting<int>(SettingsKeys.TranslatedCharactersCount, 0);
        public static SavedSetting<DateTime> TranslateBillingCycleDateTime = new SavedSetting<DateTime>(SettingsKeys.TranslationBillingCycleDateTime, DateTime.Now);
        public static bool CanTranslate { get { return !string.IsNullOrWhiteSpace(TranslationAPIKey.Value); } }

        public static SavedSetting<List<string>> ConnectionIdsList = new SavedSetting<List<string>>("ConnectionIdsList", new List<string>());
        public static Connection currentConnection = null;
        public static List<Connection> connectionList = new List<Connection>();
        public static SavedSetting<List<ToolCommand>> GlobalToolCommands = 
            new SavedSetting<List<ToolCommand>>(SettingsKeys.GlobalToolCommandsEncoded, new List<ToolCommand>());

        private static ConcurrentQueue<string> ConsoleLogQueue = new ConcurrentQueue<string>();
        private static ConcurrentQueue<string> ChatLogQueue = new ConcurrentQueue<string>();
        private static ConcurrentQueue<string> PlayerLogQueue = new ConcurrentQueue<string>();
        private static ConcurrentQueue<string> AppLogQueue = new ConcurrentQueue<string>();

        public static Thread TimedCommandsThread;
        public static bool RepopulateConditionalCommandsDropdown = true;

        private string selectedServerTag = "";

        public static List<string> ConsoleAutoCompleteStrings = new List<string>();
        public static List<string> ConsoleCommandQueryMatches = new List<string>();
        public static bool IsQueryingConsoleCommandMatch = false;
        public static string ConsoleCommandQueryString = "";
        public static int ConsoleCommandQueryMatchIndex = 0;
        /// <summary>
        /// Returns true if the current connection exists and has its server hook enabled.
        /// </summary>
        public static bool CurrentConnectionHooked { get { return currentConnection?.ServerHookActive ?? false; } }

        public static SavedSetting<List<PlayerStatsRecord>> PlayerStatsRecords = new SavedSetting<List<PlayerStatsRecord>>(SettingsKeys.PlayerStatsRecords, new List<PlayerStatsRecord>());

        public static Point mousePoint = new Point();
        public static PlayerInfo contextPlayer;
        public ToolStripItem sendToTeamItem = null;
        public ContextMenuStrip scoreBoardContextMenu = null, teamListContextMenuStrip = null;
        public ContextMenuStrip mapsCM = null, variantsCM = null;
        public static List<string> TeamColorsHexStrings = new List<string>()
        {
            "#620B0B",
            "#0B2362",
            "#1F3602",
            "#BC4D00",
            "#1D1052",
            "#A77708",
            "#1C0D02",
            "#FF4D8A"
        };
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
        public static Dictionary<int, Tuple<Color, Color>> TeamColorsAlpha { get; set; } = new Dictionary<int, Tuple<Color, Color>>()
        {
           {-1,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },    //dark gray  {-1, System.Drawing.ColorTranslator.FromHtml("#0B0B0B")}, //'#0B0B0B' "#BDBDBD"
            {0,new Tuple<Color,Color>(Color.FromArgb(230,98,11,11),     Color.FromArgb(230,128,41,41)) },     //red        { 0, System.Drawing.ColorTranslator.FromHtml("#620B0B")}, //'#620B0B' "#BDBDBD"
            {1,new Tuple<Color,Color>(Color.FromArgb(230,11,35,98),     Color.FromArgb(230,41,65,128)) },     //blue       { 1, System.Drawing.ColorTranslator.FromHtml("#0B2362")}, //'#0B2362' "#ef5350"
            {2,new Tuple<Color,Color>(Color.FromArgb(230,31,54,2),      Color.FromArgb(230,61,84,32)) },      //green      { 2, System.Drawing.ColorTranslator.FromHtml("#1F3602")}, //'#1F3602' "#42A5F5"
            {3,new Tuple<Color,Color>(Color.FromArgb(230,188,77,0),     Color.FromArgb(230,218,107,30)) },    //orange     { 3, System.Drawing.ColorTranslator.FromHtml("#BC4D00")}, //'#BC4D00' "#66BB6A"
            {4,new Tuple<Color,Color>(Color.FromArgb(230,29,16,82),     Color.FromArgb(230,59,46,112)) },     //purple     { 4, System.Drawing.ColorTranslator.FromHtml("#1D1052")}, //'#1D1052' "#FF7043"
            {5,new Tuple<Color,Color>(Color.FromArgb(230,167,119,8),    Color.FromArgb(230,197,147,38)) },    //gold       { 5, System.Drawing.ColorTranslator.FromHtml("#A77708")}, //'#A77708' "#7E57C2"
            {6,new Tuple<Color,Color>(Color.FromArgb(230,28,13,2),      Color.FromArgb(230,58,43,32)) },      //brown      { 6, System.Drawing.ColorTranslator.FromHtml("#1C0D02")}, //'#1C0D02' "#FFCA28"
            {7,new Tuple<Color,Color>(Color.FromArgb(230,255,77,138),   Color.FromArgb(230,255,107,168)) },   //pink       { 7, System.Drawing.ColorTranslator.FromHtml("#FF4D8A")}, //'#FF4D8A' "#8D6E63"
            {8,new Tuple<Color,Color>(Color.FromArgb(230,216,216,216),  Color.FromArgb(230,246, 246, 246)) }, //light gray { 9, System.Drawing.ColorTranslator.FromHtml("#424242")}, //'#424242' "#727272"
            {9,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },    //dark gray  { 8, System.Drawing.ColorTranslator.FromHtml("#0B0B0B")}, //'#D8D8D8' "#EC407A"
           {10,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },
           {11,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },
           {12,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },
           {13,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },
           {14,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },
           {15,new Tuple<Color,Color>(Color.FromArgb(230,66,66,66),     Color.FromArgb(230,96, 96, 96)) },
        };
        public static Dictionary<int, string> TeamNames { get; set; } = new Dictionary<int, string>()
        {
            {-1, "GREY"},   //grey
            { 0, "RED"},    //red
            { 1, "BLUE"},   //blue
            { 2, "GREEN"},  //green
            { 3, "ORANGE"}, //orange
            { 4, "PURPLE"}, //purple
            { 5, "GOLD"},   //gold
            { 6, "BROWN"},  //brown
            { 7, "PINK"},   //pink
            { 8, "GREY"},   //grey
            { 9, "BLACK"},  //black
            {10, "BLACK"}, //grey
            {11, "BLACK"}, //grey
            {12, "BLACK"}, //grey
            {13, "BLACK"}, //grey
            {14, "BLACK"}, //grey
            {15, "BLACK"}, //grey
        };
        public static Dictionary<int, string> TeamDisplayNames { get; set; } = new Dictionary<int, string>()
        {
            {-1, "Grey"},   //grey
            { 0, "Red"},    //red
            { 1, "Blue"},   //blue
            { 2, "Green"},  //green
            { 3, "Orange"}, //orange
            { 4, "Purple"}, //purple
            { 5, "Gold"},   //gold
            { 6, "Brown"},  //brown
            { 7, "Pink"},   //pink
            { 8, "Grey"},   //grey
            { 9, "Black"},  //black
            {10, "Black"},  //grey
            {11, "Black"},  //grey
            {12, "Black"},  //grey
            {13, "Black"},  //grey
            {14, "Black"},  //grey
            {15, "Black"},  //grey
        };
        public static List<string> TeamColorsRGBA { get; set; } = new List<string>() {
            "rgba(128, 41, 41, 230)",
            "rgba(41, 65, 128, 230)",
            "rgba(61, 84, 32, 230)",
            "rgba(218, 107, 30, 230)",
            "rgba(59, 46, 112, 230)",
            "rgba(197, 147, 38, 230)",
            "rgba(58, 43, 32, 230)",
            "rgba(255, 107, 168, 230)",
            "rgba(246, 246, 246, 230)",
            "rgba(41, 41, 41, 230)"
        };
        public static List<string> TeamColorsDarkRGBA { get; set; } = new List<string>() {
            "rgba(98,11,11,230)",
            "rgba(11,35,98,230)",
            "rgba(31,54,2,230)",
            "rgba(188,77,0,230)",
            "rgba(29,16,82,230)",
            "rgba(167,119,8,230)",
            "rgba(28,13,2,230)",
            "rgba(255,77,138,230)",
            "rgba(216,216,216,230)",
            "rgba(11, 11, 11, 230)"
        };
        public static List<string> TeamColorsHex { get; set; } = new List<string>() {
            "#802929",
            "#294180",
            "#3D5420",
            "#DA6B1E",
            "#3B2E70",
            "#C59326",
            "#3A2B20",
            "#FF6BA8",
            "#D8D8D8",
            "#0B0B0B"
        };
        public Color DefaultScoreboardPlayerColor = System.Drawing.ColorTranslator.FromHtml("#BDBDBD");

        public static Image CheckMarkImage = Resources.Image_CheckMark32x32;
        public static Image CheckMarkDisabledImage = Resources.Image_CheckMarkDisabled32x32;
        public static Image XMarkImage = Resources.Image_XMark32x32;
        public static Image XMarkDisabledImage = Resources.Image_XMarkDisabled32x32;

        public static bool ResizeRequired { get; set; } = false;
        public static SavedSetting<bool> settings_PlaySoundOnPlayerJoin = new SavedSetting<bool>(SettingsKeys.PlaySoundOnPlayerJoin, true);
        public static SavedSetting<bool> settings_PlaySoundOnPlayerLeave = new SavedSetting<bool>(SettingsKeys.PlaySoundOnPlayerLeave, true);
        public static SavedSetting<string> settings_PlayerJoinSoundPath = new SavedSetting<string>(SettingsKeys.PlayerJoinSoundPath, "");
        public static SavedSetting<string> settings_PlayerLeaveSoundPath = new SavedSetting<string>(SettingsKeys.PlayerLeaveSoundPath, "");
        public static bool settings_PlaySoundOnInvalidServerStateJSON = true;

        public static readonly Dictionary<GameVariant.BaseGame, Image> GameVariantIconsByBaseGame = new Dictionary<GameVariant.BaseGame, Image>()
        {
            {GameVariant.BaseGame.Slayer,           Resources.gt_icon_slayer_black_20x20},
            {GameVariant.BaseGame.Oddball,          Resources.gt_icon_oddball_black_20x20},
            {GameVariant.BaseGame.KingOfTheHill,    Resources.gt_icon_koth_black_20x20},
            {GameVariant.BaseGame.CaptureTheFlag,   Resources.gt_icon_ctf_black_20x20},
            {GameVariant.BaseGame.Assault,          Resources.gt_icon_assault_black_20x20},
            {GameVariant.BaseGame.Juggernaut,       Resources.gt_icon_juggernaut_black_20x20},
            {GameVariant.BaseGame.Infection,        Resources.gt_icon_infection_black_20x20},
            {GameVariant.BaseGame.VIP,              Resources.gt_icon_vip_black_20x20},
            {GameVariant.BaseGame.Unknown,          Resources.gt_icon_territories_black_20x20},
            {GameVariant.BaseGame.All,              Resources.gt_icon_forge_black_20x20},
        };

        public static readonly Dictionary<string, Image> MapIconsByName = new Dictionary<string, Image>()
        {
            {"guardian",       Resources.map_icon_Guardian},
            {"riverworld",     Resources.map_icon_Valhalla},
            {"s3d_avalanche",  Resources.map_icon_Diamondback},
            {"s3d_edge",       Resources.map_icon_Edge},
            {"s3d_reactor",    Resources.map_icon_Reactor},
            {"s3d_turf",       Resources.map_icon_Icebox},
            {"cyberdyne",      Resources.map_icon_ThePit},
            {"chill",          Resources.map_icon_Narrows},
            {"deadlock",       Resources.map_icon_HighGround},
            {"bunkerworld",    Resources.map_icon_Standoff},
            {"shrine",         Resources.map_icon_Sandtrap},
            {"zanzibar",       Resources.map_icon_LastResort},
        };

        private System.Windows.Forms.Timer InterfaceUpdateTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer UpdateTranslationUsageTrackingDataTimer = new System.Windows.Forms.Timer();

        private readonly struct SettingsKeys
        {

            public const string TranslationApiKey = "TranslationApiKey";
            public const string TranslatedCharactersCount = "TranslatedCharactersCount";
            public const string TranslationBillingCycleDateTime = "TranslationBillingCycleDateTime";

            public const string PlayerStatsRecords = "PlayerStatsRecords";

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

        #endregion


        public App()
        {

            form = this;
            
            // Set up UpdateInterface Timer
            HandleCreated += (object s, EventArgs e) => {
                InterfaceUpdateTimer.Tick += UpdateInterface;
                InterfaceUpdateTimer.Interval = 100;
                InterfaceUpdateTimer.Start();
            };

            // Framework
            InitializeComponent();

            // Ensure ConfigurationManager.AppSettings Exists
            if (ConfigurationManager.AppSettings == null) {
                throw new Exception(
                    "ConfigurationManager.AppSettings does not exist, needs to be initialized to an empty collection."
                );
            }

            LoadSettings();

            if (!string.IsNullOrWhiteSpace(TranslationAPIKey.Value)) {
                TranslationClient = TranslationClient.CreateFromApiKey(TranslationAPIKey.Value);
            }

            if (connectionList.Count >= 1)
            {
                
                currentConnection = connectionList[0];
                textBoxAutoScrollConsoleText.Text = currentConnection.GetConsole();
                textBoxAutoScrollChatText.Text = currentConnection.GetChat();
                textBoxAutoScrollPlayerLog.Text = currentConnection.GetPlayerLog();
                textBoxAutoScrollApplicationLog.Text = AppLogText;

                foreach (Connection connection in connectionList)
                {
                    if (connection.Settings.Commands == null)
                    {
                        connection.Settings.Commands = new List<ToolCommand>();
                    }
                    connection.Settings.InitializeCommands(connection);
                }

            }
            else
            {
                // No servers were loaded, force a new one to be added
                new ServerManager().ShowDialog();

                //ConnectionIdsList.Value.Add("1");
                //ConnectionIdsList.Save();

                if (connectionList.Count < 1) {
                    MessageBox.Show(
                        "Error: No connections were loaded.\nIf you have not configured any connections, please configure at least one the next time you run the application.\nIf connections have been configured and are not being loaded as you would expect, please report this error if it happens again.\nThe application will now exit.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning
                    );
                }
                else { currentConnection = connectionList[0]; }
            }

            if (currentConnection == null) {
                Application.Exit();
                form.Close();                
                //Application.ExitThread();
                return;
            }

            // Set Translation data-check timer
			UpdateTranslationUsageTrackingDataTimer.Tick += (o, e) => {
				UpdateTranslationUsageTrackingData();
            };
            UpdateTranslationUsageTrackingDataTimer.Interval = (int)TimeSpan.FromHours(1).TotalMilliseconds;
            UpdateTranslationUsageTrackingDataTimer.Start();

            // Set Translation Enabled/Disabled button text
            if (currentConnection.Settings.TranslationEnabled) {
                fileMenuTranslationItemTranslationEnabledButton.Text = "Translation Enabled";
            }
            else {
                fileMenuTranslationItemTranslationEnabledButton.Text = "Translation Disabled";
            }

            SetStyle(ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;

            toolStripStatusLabelVersion.Text = "Version: " + toolversion;

			#region Set up Scoreboard Right-click Context Menu
			
            scoreBoardContextMenu = new ContextMenuStrip();
            
            /*0*/scoreBoardContextMenu.Items.Add("Kick",null,                (sender, e) => { KickPlayer(sender, e); });
            /*1*/scoreBoardContextMenu.Items.Add("Ban Temporarily", null,    (sender, e) => { BanPlayerTemporarily(sender, e); });
            /*2*/scoreBoardContextMenu.Items.Add("Ban Permanently", null,    (sender, e) => { BanPlayerPermanently(sender, e); });
            /*3*/scoreBoardContextMenu.Items.Add("Copy Name", null,          (sender, e) => { CopyName(sender, e); });
            /*4*/scoreBoardContextMenu.Items.Add("Copy UID", null,           (sender, e) => { CopyUID(sender, e); });
            /*5*/scoreBoardContextMenu.Items.Add("Copy Name and UID", null,  (sender, e) => { CopyNameAndUID(sender, e); });
            /*6*/scoreBoardContextMenu.Items.Add("AUTHORIZE AS ADMINISTRATOR",  null, (sender, e) => { AuthorizePlayerAsAdmin(sender, e); });
            /*7*/scoreBoardContextMenu.Items.Add("DEAUTHORIZE ADMINISTRATOR", null, (sender, e) => { RemovePlayerAdminPrivileges(sender, e); });
            /*8*/scoreBoardContextMenu.Items.Add("Send to Team"); sendToTeamItem = scoreBoardContextMenu.Items.LastItem();

			#endregion

			#region Set up SendToTeam Context Menu

			teamListContextMenuStrip = new ContextMenuStrip();

            teamListContextMenuStrip.Text = "Send to Team";
            teamListContextMenuStrip.ShowImageMargin = false; teamListContextMenuStrip.ShowCheckMargin = false;
            teamListContextMenuStrip.Items.Add("Red Team", null, (sender, e) => { SendContextPlayerToTeam(0); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[0].Item2; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            teamListContextMenuStrip.Items.Add("Blue Team", null, (sender, e) => { SendContextPlayerToTeam(1); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[1].Item2; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            teamListContextMenuStrip.Items.Add("Green Team", null, (sender, e) => { SendContextPlayerToTeam(2); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[2].Item2; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            teamListContextMenuStrip.Items.Add("Orange Team", null, (sender, e) => { SendContextPlayerToTeam(3); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[3].Item2; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            teamListContextMenuStrip.Items.Add("Purple Team", null, (sender, e) => { SendContextPlayerToTeam(4); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[4].Item2; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            teamListContextMenuStrip.Items.Add("Gold Team", null, (sender, e) => { SendContextPlayerToTeam(5); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[5].Item2; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            teamListContextMenuStrip.Items.Add("Brown Team", null, (sender, e) => { SendContextPlayerToTeam(6); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[6].Item1; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            teamListContextMenuStrip.Items.Add("Pink Team", null, (sender, e) => { SendContextPlayerToTeam(7); });
            teamListContextMenuStrip.Items.LastItem().BackColor = TeamColors[7].Item1; teamListContextMenuStrip.Items.LastItem().ForeColor = Color.White;
            (sendToTeamItem as ToolStripMenuItem).DropDown = teamListContextMenuStrip;

			#endregion

			#region Set up Maps Context Menu - Add built-in maps

			mapsCM = new ContextMenuStrip();
            
            mapsCM.Items.Add("Diamondback",   null, (sender, e) => { ContextLoadMap("Game.Map s3d_avalanche", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Diamondback]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Diamondback];
            mapsCM.Items.Add("Edge",          null, (sender, e) => { ContextLoadMap("Game.Map s3d_edge", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Edge]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Edge];
            mapsCM.Items.Add("Guardian",      null, (sender, e) => { ContextLoadMap("Game.Map guardian", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Guardian]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Guardian];
            mapsCM.Items.Add("High Ground",   null, (sender, e) => { ContextLoadMap("Game.Map deadlock", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.HighGround]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.HighGround];
            mapsCM.Items.Add("Icebox",        null, (sender, e) => { ContextLoadMap("Game.Map s3d_turf", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Icebox]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Icebox];
            mapsCM.Items.Add("Last Resort",   null, (sender, e) => { ContextLoadMap("Game.Map zanzibar", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.LastResort]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.LastResort];
            mapsCM.Items.Add("Narrows",       null, (sender, e) => { ContextLoadMap("Game.Map chill", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Narrows]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Narrows];
            mapsCM.Items.Add("Reactor",       null, (sender, e) => { ContextLoadMap("Game.Map s3d_reactor", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Reactor]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Reactor];
            mapsCM.Items.Add("Sandtrap",      null, (sender, e) => { ContextLoadMap("Game.Map shrine", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Sandtrap]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Sandtrap];
            mapsCM.Items.Add("Standoff",      null, (sender, e) => { ContextLoadMap("Game.Map bunkerworld", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Standoff]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Standoff];
            mapsCM.Items.Add("The Pit",       null, (sender, e) => { ContextLoadMap("Game.Map cyberdyne", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.ThePit]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.ThePit];
            mapsCM.Items.Add("Valhalla",      null, (sender, e) => { ContextLoadMap("Game.Map riverworld", sender); toolStripDropDownButtonMap.ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Valhalla]; });
            mapsCM.Items.LastItem().ToolTipText = MapVariant.BaseMapDescriptionsByBaseMap[MapVariant.BaseMap.Valhalla];

            toolStripDropDownButtonMap.DropDown = mapsCM;
            toolStripDropDownButtonMap.DropDownDirection = ToolStripDropDownDirection.AboveRight;

            #endregion

            #region Set up Game Variants Context Menu - Add default game variants

            variantsCM = new ContextMenuStrip();

            variantsCM.Items.Add("Slayer", null, null);
            variantsCM.Items[0].DropDownItems().Add("Slayer", null, (sender, e) => { ContextLoadGame("Game.GameType slayer", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["slayer"]; });
            variantsCM.Items[0].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["slayer"];
            variantsCM.Items[0].DropDownItems().Add("Team Slayer", null, (sender, e) => { ContextLoadGame("Game.GameType team slayer", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["team slayer"]; });
            variantsCM.Items[0].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["team slayer"];
            variantsCM.Items[0].DropDownItems().Add("Rockets", null, (sender, e) => { ContextLoadGame("Game.GameType rockets", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["rockets"]; });
            variantsCM.Items[0].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["rockets"];
            variantsCM.Items[0].DropDownItems().Add("Elimination", null, (sender, e) => { ContextLoadGame("Game.GameType elimination", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["elimination"]; });
            variantsCM.Items[0].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["elimination"];
            variantsCM.Items[0].DropDownItems().Add("Duel", null, (sender, e) => { ContextLoadGame("Game.GameType duel", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["duel"]; });
            variantsCM.Items[0].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["duel"];
            variantsCM.Items.Add("Oddball", null, null);
            variantsCM.Items[1].DropDownItems().Add("Oddball", null, (sender, e) => { ContextLoadGame("Game.GameType oddball", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["oddball"]; });
            variantsCM.Items[1].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["oddball"];
            variantsCM.Items[1].DropDownItems().Add("Team Oddball", null, (sender, e) => { ContextLoadGame("Game.GameType team oddball", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["team oddball"]; });
            variantsCM.Items[1].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["team oddball"];
            variantsCM.Items[1].DropDownItems().Add("Lowball", null, (sender, e) => { ContextLoadGame("Game.GameType lowball", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["lowball"]; });
            variantsCM.Items[1].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["lowball"];
            variantsCM.Items[1].DropDownItems().Add("Ninjaball", null, (sender, e) => { ContextLoadGame("Game.GameType ninjaball", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["ninjaball"]; });
            variantsCM.Items[1].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["ninjaball"];
            variantsCM.Items[1].DropDownItems().Add("Rocketball", null, (sender, e) => { ContextLoadGame("Game.GameType rocketball", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["rocketball"]; });
            variantsCM.Items[1].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["rocketball"];
            variantsCM.Items.Add("Capture The Flag", null, null);
            variantsCM.Items[2].DropDownItems().Add("Multi Flag", null, (sender, e) => { ContextLoadGame("Game.GameType multi flag", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["multi flag"]; });
            variantsCM.Items[2].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["multi flag"];
            variantsCM.Items[2].DropDownItems().Add("One Flag", null, (sender, e) => { ContextLoadGame("Game.GameType one flag", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["one flag"]; });
            variantsCM.Items[2].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["one flag"];
            variantsCM.Items[2].DropDownItems().Add("Tank Flag", null, (sender, e) => { ContextLoadGame("Game.GameType tank flag", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["tank flag"]; });
            variantsCM.Items[2].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["tank flag"];
            variantsCM.Items[2].DropDownItems().Add("Attrition CTF", null, (sender, e) => { ContextLoadGame("Game.GameType attrition ctf", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["attrition ctf"]; });
            variantsCM.Items[2].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["attrition ctf"];
            variantsCM.Items.Add("Assault", null, null);
            variantsCM.Items[3].DropDownItems().Add("Assault", null, (sender, e) => { ContextLoadGame("Game.GameType assault", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["assault"]; });
            variantsCM.Items[3].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["assault"];
            variantsCM.Items[3].DropDownItems().Add("Neutral Bomb", null, (sender, e) => { ContextLoadGame("Game.GameType neutral assault", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["neutral assault"]; });
            variantsCM.Items[3].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["neutral assault"];
            variantsCM.Items[3].DropDownItems().Add("One Bomb", null, (sender, e) => { ContextLoadGame("Game.GameType one bomb", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["one bomb"]; });
            variantsCM.Items[3].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["one bomb"];
            variantsCM.Items[3].DropDownItems().Add("Attrition Bomb", null, (sender, e) => { ContextLoadGame("Game.GameType attrition assault", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["attrition assault"]; });
            variantsCM.Items[3].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["attrition assault"];
            variantsCM.Items.Add("Infection", null, null);
            variantsCM.Items[4].DropDownItems().Add("Infection", null, (sender, e) => { ContextLoadGame("Game.GameType infection", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["infection"]; });
            variantsCM.Items[4].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["infection"];
            variantsCM.Items[4].DropDownItems().Add("Save One Bullet", null, (sender, e) => { ContextLoadGame("Game.GameType save one bullet", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["save one bullet"]; });
            variantsCM.Items[4].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["save one bullet"];
            variantsCM.Items[4].DropDownItems().Add("Alpha Zombie", null, (sender, e) => { ContextLoadGame("Game.GameType alpha zombie", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["alpha zombie"]; });
            variantsCM.Items[4].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["alpha zombie"];
            variantsCM.Items[4].DropDownItems().Add("Hide and Seek", null, (sender, e) => { ContextLoadGame("Game.GameType hide and seek", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["hide and seek"]; });
            variantsCM.Items[4].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["hide and seek"];
            variantsCM.Items.Add("King Of The Hill", null, null);
            variantsCM.Items[5].DropDownItems().Add("Crazy King", null, (sender, e) => { ContextLoadGame("Game.GameType crazy king", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["crazy king"]; });
            variantsCM.Items[5].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["crazy king"];
            variantsCM.Items[5].DropDownItems().Add("Team King", null, (sender, e) => { ContextLoadGame("Game.GameType team king", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["team king"]; });
            variantsCM.Items[5].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["team king"];
            variantsCM.Items[5].DropDownItems().Add("Mosh Pit", null, (sender, e) => { ContextLoadGame("Game.GameType mosh pit", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["mosh pit"]; });
            variantsCM.Items[5].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["mosh pit"];
            variantsCM.Items.Add("Juggernaut", null, null);
            variantsCM.Items[6].DropDownItems().Add("Juggernaut", null, (sender, e) => { ContextLoadGame("Game.GameType juggernaut", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["juggernaut"]; });
            variantsCM.Items[6].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["juggernaut"];
            variantsCM.Items[6].DropDownItems().Add("Mad Dash", null, (sender, e) => { ContextLoadGame("Game.GameType mad dash", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["mad dash"]; });
            variantsCM.Items[6].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["mad dash"];
            variantsCM.Items[6].DropDownItems().Add("Ninjanaut", null, (sender, e) => { ContextLoadGame("Game.GameType ninjanaut", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["ninjanaut"]; });
            variantsCM.Items[6].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["ninjanaut"];
            variantsCM.Items.Add("VIP", null, null);
            variantsCM.Items[7].DropDownItems().Add("VIP", null, (sender, e) => { ContextLoadGame("Game.GameType vip", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["vip"]; });
            variantsCM.Items[7].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["vip"];
            variantsCM.Items[7].DropDownItems().Add("One-Sided VIP", null, (sender, e) => { ContextLoadGame("Game.GameType one sided vip", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["one sided vip"]; });
            variantsCM.Items[7].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["one sided vip"];
            variantsCM.Items[7].DropDownItems().Add("Escort", null, (sender, e) => { ContextLoadGame("Game.GameType escort", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["escort"]; });
            variantsCM.Items[7].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["escort"];
            variantsCM.Items[7].DropDownItems().Add("Influential VIP", null, (sender, e) => { ContextLoadGame("Game.GameType influential vip", sender); toolStripDropDownButtonGame.ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["influential vip"]; });
            variantsCM.Items[7].DropDownItems().LastItem().ToolTipText = GameVariant.BuiltInGameVariantDescriptionsByName["influential vip"];

            toolStripDropDownButtonGame.DropDown = variantsCM;
            toolStripDropDownButtonGame.DropDownDirection = ToolStripDropDownDirection.AboveRight;

            #endregion

            #region Setup Auto-Complete
            AutoCompleteStringCollection serverCommandsCollection = new AutoCompleteStringCollection();
            ConsoleAutoCompleteStrings = Resources.TextFile_ServerCommandTriggers.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            ConsoleAutoCompleteStrings.Add("Server.ListPlayersJson");
            ConsoleAutoCompleteStrings.Add("Server.BalanceTeams");
            ConsoleAutoCompleteStrings.Add("Server.StatusPacket");
            ConsoleAutoCompleteStrings.Add("Server.ToggleShuffleTeamsPlayerCountRequirement");
            ConsoleAutoCompleteStrings.Add("Application.VerifyServerHook");
            ConsoleAutoCompleteStrings.Add("Application.ShowServerJson");
            ConsoleAutoCompleteStrings.Add("Application.HideServerJson");
            serverCommandsCollection.AddRange(ConsoleAutoCompleteStrings.ToArray());

            TimedCommandsThread = new Thread(new ThreadStart(RunTimedCommands)) { IsBackground = true };
            if (TimedCommandsThread.Name == null) {
                TimedCommandsThread.Name = InternalAppName + ".RunTimedCommandsThread";
            }
            TimedCommandsThread.Start();

            textBoxConsoleTextEntry.AutoCompleteCustomSource = serverCommandsCollection;
            textBoxConsoleTextEntry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxConsoleTextEntry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

			#endregion
            
            // Enables menu items to be toggled without closing the dropdown
            fileMenuSettingsItem.DropDown.Closing += new ToolStripDropDownClosingEventHandler(DropDown_Closing);
            fileMenuCommandsItem.DropDown.Closing += new ToolStripDropDownClosingEventHandler(DropDown_Closing);

            Scoreboard.Position = new Point(4, 4);

            // removes the blinking caret, and focus, from read-only text boxes if there is no text selected
            textBoxAutoScrollChatText.MouseUp += DisplayOnlyTextBox_MouseUp;
            textBoxAutoScrollConsoleText.MouseUp += DisplayOnlyTextBox_MouseUp;
            textBoxAutoScrollPlayerLog.MouseUp += DisplayOnlyTextBox_MouseUp;
            textBoxAutoScrollApplicationLog.MouseUp += DisplayOnlyTextBox_MouseUp;

            // Auto scroll
            textBoxAutoScrollConsoleText.TabSelected_UpdateAutoScroll(tabControlServerInterfaces, null);

            // Disable Server Hook Button
            toolStripSplitButtonServerHook.Enabled = false;
            toolStripSplitButtonServerHook.Visible = false;

		}

        private void RunTimedCommands()
        {

            // Timed Commands Execution Tick

            while (true )
            {
                try
                {
                    foreach (ToolCommand command in GlobalToolCommands.Value)
                    {
                        
                        if (!command.Enabled) { continue; }
                        if (command.ConditionType != ToolCommand.TriggerType.EveryXMinutes && command.ConditionType != ToolCommand.TriggerType.Daily)
                        {
                            continue;
                        }

                        if (command.ConditionType == ToolCommand.TriggerType.EveryXMinutes)
                        {
                            if (DateTime.Now >= command.NextRunTime)
                            {

                                command.NextRunTime = command.NextRunTime.AddMinutes(command.RunTime);

                                foreach (string commandString in command.CommandStrings)
                                {
                                    foreach (Connection connection in connectionList)
                                    {
                                        if (connection.RconConnected)
                                        {
                                            connection.RconCommandQueue.Enqueue(
                                                RconCommand.ConsoleLogCommand(
                                                    commandString,
                                                    commandString,
                                                    "Timed Command"
                                                )
                                            );
                                        }
                                    }
                                }
                            }
                        }
                        else if (command.ConditionType == ToolCommand.TriggerType.Daily)
                        {                                
                            if (DateTime.Now.Hour == command.RunTime && command.Triggered == false)
                            {
                                command.Triggered = true;
                                foreach (string commandString in command.CommandStrings)
                                {
                                    foreach (Connection connection in connectionList)
                                    {
                                        connection.RconCommandQueue.Enqueue(
                                            RconCommand.ConsoleLogCommand(
                                                commandString,
                                                commandString,
                                                "Daily Command"
                                            )
                                        );
                                    }
                                }
                            }
                            if (DateTime.Now.Hour != command.RunTime && command.Triggered == true)
                            {
                                command.Triggered = false;
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
        private void UpdateInterface(object sender, EventArgs e) {
            InterfaceUpdateTimer.Stop();
            UpdateInterface();
            InterfaceUpdateTimer.Enabled = true;
        }
        private void UpdateInterface()
        {
            
            if (currentConnection == null) { return; }
            
            // Update Labels, Images, Etc.
            try {

				#region Update logs with messages
				while (ConsoleLogQueue.Count > 0) {
                    if (ConsoleLogQueue.TryDequeue(out string msg)) {
                        textBoxAutoScrollConsoleText.AppendText(msg);
                    }
                }
                while (ChatLogQueue.Count > 0) {
                    if (ChatLogQueue.TryDequeue(out string msg)) {
                        textBoxAutoScrollChatText.AppendText(msg);
                    }
                }
                while (PlayerLogQueue.Count > 0) {
                    if (PlayerLogQueue.TryDequeue(out string msg)) {
                        textBoxAutoScrollPlayerLog.AppendText(msg);
                    }
                }
                while (AppLogQueue.Count > 0) {
                    if (AppLogQueue.TryDequeue(out string msg)) {
                        AppLogText += msg;
                        textBoxAutoScrollApplicationLog.AppendText(msg);
                    }
                }
				#endregion

				bool validState = currentConnection?.State?.IsValid ?? false;

                if (RepopulateConditionalCommandsDropdown) {
                    RepopulateConditionalCommandsDropdown = false;
                    PopulateConditionalCommandsDropdown(); 
                    SaveSettings(); 
                }

                // Server Name Edit Box needs manual checks due to inconsistent OnMouseLeave event firing
                // If Server Name TextBox is not being edited...
                if (!textBoxServerNameEdit_editing) {

                    // If mouse is within edit area...
                    if (textBoxServerNameEdit.ClientRectangle.Contains(textBoxServerNameEdit.PointToClient(Control.MousePosition))) {
                        // Turn on edit mode view if it's off
                        if (!textBoxServerNameEdit_editModeView) {
                            textBoxServerNameEdit.ReadOnly = false;
                            textBoxServerNameEdit.BackColor = SystemColors.Window;
                            textBoxServerNameEdit_editModeView = true;
                        }
                    }

                    // Mouse is NOT within edit area...
                    else {
                        // Turn off edit mode view if it's on
                        if (textBoxServerNameEdit_editModeView) {
                            textBoxServerNameEdit.ReadOnly = true;
                            textBoxServerNameEdit.BackColor = SystemColors.Control;
                            textBoxServerNameEdit_editModeView = false;
                        }
                    }

                }

                // Set Server Connection Info Labels
                if (validState) {

                    if (!comboBoxMaxPlayers.Enabled) { comboBoxMaxPlayers.Enabled = true; }
                    //if ((labelServerName.Text ?? "") != currentConnection.State.Name) { 
                    //    labelServerName.Text = currentConnection.State.Name; 
                    //}
                    if (!textBoxServerNameEdit_editing) { 
                        if ((textBoxServerNameEdit.Text ?? "") != currentConnection.State.name) {
                            textBoxServerNameEdit.Text = currentConnection.State.name;
                        }
                    }

                    // Set Lobby + Map Indicator images
                    if (CurrentConnectionHooked) {
                        if (pictureBoxMapAndStatusOverlay.Image != currentConnection.State.LobbyStateOverlay) {
                            pictureBoxMapAndStatusOverlay.Image = currentConnection.State.LobbyStateOverlay;
                        }
                        if (pictureBoxMapAndStatusOverlay.BackgroundImage != currentConnection.State.LobbyMapBackgroundImage) {
                            pictureBoxMapAndStatusOverlay.BackgroundImage = currentConnection.State.LobbyMapBackgroundImage;
                        }
                    }
                    else {
                        if (currentConnection.State.InLobby) {
                            if (pictureBoxMapAndStatusOverlay.Image != ServerState.statusOverlayInLobby) {
                                pictureBoxMapAndStatusOverlay.Image = ServerState.statusOverlayInLobby;
                            }
                            if (pictureBoxMapAndStatusOverlay.BackgroundImage != ServerState.mapIconLobby) {
                                pictureBoxMapAndStatusOverlay.BackgroundImage = ServerState.mapIconLobby;
                            }
                        }
                        else {
                            if (pictureBoxMapAndStatusOverlay.Image != currentConnection.State.LobbyStateOverlay) {
                                pictureBoxMapAndStatusOverlay.Image = currentConnection.State.LobbyStateOverlay;
                            }
                            if (pictureBoxMapAndStatusOverlay.BackgroundImage != currentConnection.State.LobbyMapBackgroundImage) {
                                pictureBoxMapAndStatusOverlay.BackgroundImage = currentConnection.State.LobbyMapBackgroundImage;
                            }
                        }
                    }
                    
                    // Set Map/Variant Icons and Labels
                    if (CurrentConnectionHooked) {
                        if (toolStripDropDownButtonGame.Text != (currentConnection?.LiveGameVariant?.Name ?? "Deciding Gametype")) {
                            toolStripDropDownButtonGame.Text = currentConnection?.LiveGameVariant?.Name ?? "Deciding Gametype";
                        }
                        if (pictureBoxGameVariantIcon.Image != GameVariantIconsByBaseGame[currentConnection?.LiveGameVariant?.BaseGameID ?? GameVariant.BaseGame.Unknown]) {
                            pictureBoxGameVariantIcon.Image = GameVariantIconsByBaseGame[currentConnection?.LiveGameVariant?.BaseGameID ?? GameVariant.BaseGame.Unknown];
                        }
                    }
                    else {
                        if (toolStripDropDownButtonGame.Text != (currentConnection.State.variant == "none" ? "Deciding Gametype" : currentConnection.State.variant)) {
                            toolStripDropDownButtonGame.Text = currentConnection.State.variant == "none" ? "Deciding Gametype" : currentConnection.State.variant;
                        }
                        if (pictureBoxGameVariantIcon.Image != GameVariantIconsByBaseGame[currentConnection.State.GameVariantType]) {
                            pictureBoxGameVariantIcon.Image = GameVariantIconsByBaseGame[currentConnection.State.GameVariantType];
                        }
                    }

                    if (toolStripDropDownButtonMap.Text != currentConnection.State.map) { toolStripDropDownButtonMap.Text = currentConnection.State.map; }
                    if (labelPlayers.Text != currentConnection.State.numPlayers.ToString()) { labelPlayers.Text = currentConnection.State.numPlayers.ToString(); }

                    // Set chat commands menu items toggle check state
                    if (currentConnection.Settings != null) {
                        if (fileMenuCommandsItemShuffleTeamsCommandToggle.Checked != currentConnection.Settings.ChatCommandShuffleTeamsEnabled) {
                            fileMenuCommandsItemShuffleTeamsCommandToggle.Checked =
                                currentConnection.Settings.ChatCommandShuffleTeamsEnabled;
                            fileMenuCommandsItemShuffleTeamsCommandToggle.CheckState =
                                currentConnection.Settings.ChatCommandShuffleTeamsEnabled ? CheckState.Checked : CheckState.Unchecked;
                        }
                        if (fileMenuCommandsItemKickPlayerCommandToggle.Checked != currentConnection.Settings.ChatCommandKickPlayerEnabled) {
                            fileMenuCommandsItemKickPlayerCommandToggle.Checked =
                                currentConnection.Settings.ChatCommandKickPlayerEnabled;
                            fileMenuCommandsItemKickPlayerCommandToggle.CheckState =
                                currentConnection.Settings.ChatCommandKickPlayerEnabled ? CheckState.Checked : CheckState.Unchecked;
                        }
                        if (fileMenuCommandsItemEndGameCommandToggle.Checked != currentConnection.Settings.ChatCommandEndGameEnabled) {
                            fileMenuCommandsItemEndGameCommandToggle.Checked =
                                currentConnection.Settings.ChatCommandEndGameEnabled;
                            fileMenuCommandsItemEndGameCommandToggle.CheckState =
                                currentConnection.Settings.ChatCommandEndGameEnabled ? CheckState.Checked : CheckState.Unchecked;
                        }
                    }

                    // Update max players dropdown selector
                    if (currentConnection.State.maxPlayers != comboBoxMaxPlayers.SelectedIndex + 16) {
                        // Don't update the combo box if it's focused, because the selection will keep resetting and it will be unusable
                        if (!comboBoxMaxPlayers.Focused) { comboBoxMaxPlayers.SelectedIndex = 16 - currentConnection.State.maxPlayers; }
                    }

                    // Update Server Select Dropdown
                    if (connectionList.Any(c => c.UpdateDisplay)) {

                        // Clear dropdown
                        toolStripSplitButtonServerSelect.DropDownItems.Clear();

                        // Update Server Select Dropdown Options
                        for (int i = 0; i < connectionList.Count; i++) {
                            if (connectionList[i] != null) {

                                string id = connectionList[i].Settings.DisplayName;

                                // Add a dropdown item for this server connection
                                toolStripSplitButtonServerSelect.DropDownItems.Add(new ToolStripMenuItem(id));
                                toolStripSplitButtonServerSelect.DropDownItems[i].Tag = i.ToString();

                                // Set dropdown text to selected server name
                                if (connectionList[i] == currentConnection) {
                                    toolStripSplitButtonServerSelect.Text = id;
                                    //toolStripSplitButtonServerSelect.Image = Properties.Resources.Image_ServerIcon16x16;
                                    //toolStripSplitButtonServerSelect.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                                    //toolStripSplitButtonServerSelect.AutoSize = true;
                                    ((ToolStripMenuItem)toolStripSplitButtonServerSelect.DropDownItems[i]).Checked = true;
                                    ((ToolStripMenuItem)toolStripSplitButtonServerSelect.DropDownItems[i]).CheckState = CheckState.Checked;
                                }
                                else {
                                    toolStripSplitButtonServerSelect.DropDownItems[i].BackColor = DefaultBackColor;
                                }

                                connectionList[i].UpdateDisplay = false;
                            }
                        }

                    }

                }
                else {

                    if (comboBoxMaxPlayers.SelectedIndex != 0) { comboBoxMaxPlayers.SelectedIndex = 0; }
                    if (comboBoxMaxPlayers.Enabled) { comboBoxMaxPlayers.Enabled = false; }

                    // Set Map/Variant Icons and Labels
                    //if (labelServerName.Text != "No Connection") {  labelServerName.Text = "No Connection"; }
                    if (textBoxServerNameEdit.Text != "No Connection") { textBoxServerNameEdit.Text = "No Connection"; }
                    if (labelMap.Text != "...") {                   labelMap.Text = "...";                  }
                    if (labelVariant.Text != "...") {               labelVariant.Text = "...";              }
                    if (labelPlayers.Text != "0") {                 labelPlayers.Text = "0";                }
                    if (pictureBoxGameVariantIcon.Image != Resources.gt_icon_territories_black_20x20) { pictureBoxGameVariantIcon.Image = Resources.gt_icon_territories_black_20x20; }
                    if (pictureBoxMapAndStatusOverlay.Image != null) { pictureBoxMapAndStatusOverlay.Image = null; }
                    if (pictureBoxMapAndStatusOverlay.BackgroundImage != Resources.map_icon_Lobby) { pictureBoxMapAndStatusOverlay.BackgroundImage = Resources.map_icon_Lobby; }

                    // Set chat commands menu items toggle check state
                    if (fileMenuCommandsItemShuffleTeamsCommandToggle.Checked || fileMenuCommandsItemShuffleTeamsCommandToggle.CheckState != CheckState.Unchecked) {
                        fileMenuCommandsItemShuffleTeamsCommandToggle.Checked = false;
                        fileMenuCommandsItemShuffleTeamsCommandToggle.CheckState = CheckState.Unchecked;
                    }
                    if (fileMenuCommandsItemKickPlayerCommandToggle.Checked || fileMenuCommandsItemKickPlayerCommandToggle.CheckState != CheckState.Unchecked) {
                        fileMenuCommandsItemKickPlayerCommandToggle.Checked = false;
                        fileMenuCommandsItemKickPlayerCommandToggle.CheckState = CheckState.Unchecked;
                    }
                    if (fileMenuCommandsItemEndGameCommandToggle.Checked || fileMenuCommandsItemEndGameCommandToggle.CheckState != CheckState.Unchecked) {
                        fileMenuCommandsItemEndGameCommandToggle.Checked = false;
                        fileMenuCommandsItemEndGameCommandToggle.CheckState = CheckState.Unchecked;
                    }

                }

                #region Update Status Text and Icon for Rcon Connection and Stats Connection

                // Update Stats Connection Image
                if (toolStripStatusLabelStatsConnection.Image != (currentConnection.ServerStatusAvailable ? CheckMarkImage : XMarkImage)) {
                    toolStripStatusLabelStatsConnection.Image = currentConnection.ServerStatusAvailable ? CheckMarkImage : XMarkImage;
                }
                // Update Rcon Connection Image
                if (toolStripStatusLabelRconConnection.Image != (currentConnection.RconConnected ? CheckMarkImage : XMarkImage)) {
                    toolStripStatusLabelRconConnection.Image = currentConnection.RconConnected ? CheckMarkImage : XMarkImage;
                }
                // Update ServerHook Status Image
                if (toolStripSplitButtonServerHook.Image != (CurrentConnectionHooked ? CheckMarkImage : XMarkImage)) {
                    toolStripSplitButtonServerHook.Image = CurrentConnectionHooked ? CheckMarkImage : XMarkImage;
                    toolStripSplitButtonServerHook.ToolTipText = CurrentConnectionHooked 
                        ? "ServerHook Active"
                        : "Click to attempt to establish the ServerHook.\nIn order to establish the ServerHook:\n\tThe server process must be running on this computer.\n\tThis application must have administrator privileges."
                    ;
                }

                #endregion

                // Set Window Title
                if (validState && currentConnection.Settings != null) {
                    string title = "";
                    switch (currentConnection.Settings.TitleDisplayOption) {
                        case ServerSettings.TitleOption.Name: title = ("RCON Tool - " + currentConnection?.State?.name ?? "Unknown Server"); break;
                        case ServerSettings.TitleOption.Game:
                            if ((currentConnection?.State?.status ?? "") == Connection.StatusStringInLobby) { title = ("RCON Tool - In Lobby"); }
                            else { title =  "RCON Tool - " + currentConnection?.State?.variant ?? "Unknown Gametype" + " on " + currentConnection?.State?.map ?? "Unknown Map"; }
                            break;
                        case ServerSettings.TitleOption.Ip: title = "RCON Tool - " + currentConnection?.Settings?.Ip ?? "Unknown IP " + ":" + currentConnection?.Settings?.InfoPort ?? "Unknown Port"; break;
                        case ServerSettings.TitleOption.None: title = ("RCON Tool"); break;
                        default: break;
                    }
                    if (Text != title) { Text = title; }
                }

            }
            catch (Exception e) {
                Log("Error while updating interface:\n" + e.Message);
            }

        }
        

        #region Text Logs

        public static void AppendConsole(string text) { 
            if (string.IsNullOrWhiteSpace(text)) { return; } 
            ConsoleLogQueue.Enqueue(text); 
        }

        public static void AppendChat(string text) { 
            if (string.IsNullOrWhiteSpace(text)) { return; } 
            ChatLogQueue.Enqueue(text); 
        }

        public static void AppendPlayerLog(string text) { 
            if (string.IsNullOrWhiteSpace(text)) { return; } 
            PlayerLogQueue.Enqueue(text); 
        }

        public static void Log(string message, Connection connection)
        {
            if (connection == null) { 
                Log(message); 
            }
            else { 
                Log($"[{DateTimeUTC()}] ({connection.ConnectionName})", message); 
            }
        }
        public static void Log(string message) { 
            Log($"[{DateTimeUTC()}] (APP)", message); 
        }
        public static void Log(string prefix, string message)
		{

            if (string.IsNullOrWhiteSpace(message)) { return; }

            prefix = Environment.NewLine + System.Text.RegularExpressions.Regex.Replace(
                prefix, @"\r\n?|\n", Environment.NewLine
            );

            message = System.Text.RegularExpressions.Regex.Replace(
                message, @"\r\n?|\n", Environment.NewLine
            );

            AppLogQueue.Enqueue($"{prefix}: {message}");

        }

        /// <summary>
        /// Adds an entry in the Administrative Actions log file detailing the kick or ban action that was taken.
        /// </summary>
        public static void ExternalLog(ServerSettings serverinfo, string message)
        {
            try {
                File.AppendAllText(
                    $"log_administrative_actions.txt",
                    $"[{DateTimeUTC()}] [{serverinfo?.DisplayName ?? "Unknown Connection"}] {message}{System.Environment.NewLine}"
                );
            }
            catch (Exception e) { 
                Log("Error logging info to administrative actions log - most likely a permissions issue creating the file. Please make sure you have write permissions for the directory where this app's executable is located.\nError: " + e.ToString()); 
            }
            Log($"[{DateTimeUTC()}] [{serverinfo?.DisplayName ?? "Unknown Connection"}] {message}");
        }

        #endregion

        #region Loading, Saving, and Settings

        /// <summary>
        /// Finds the 'mods' directory for ElDewrito by checking running processes. Returns the DirectoryInfo for the directory or null if none is found.
        /// </summary>
        public static DirectoryInfo FindModsDirectory()
        {
            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses())
            {
                if (!string.IsNullOrWhiteSpace(process.MainWindowTitle) && process.MainWindowTitle.StartsWith("ElDewrito | Version:"))
                {
                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(process.MainModule.FileName);
                        if (directoryInfo.Exists && directoryInfo.GetDirectories().Any(x => x.Name == "mods"))
                        {
                            DirectoryInfo modsDirectoryInfo = directoryInfo.GetDirectories().First(x => x.Name == "mods");
                            DialogResult isCorrectDirectory = MessageBox.Show(
                                "The following 'mods' directory was found:\n\n" 
                                + modsDirectoryInfo.FullName + 
                                "\n\nIs this the correct 'mods' directory?", 
                                "Confirm Directory", 
                                MessageBoxButtons.YesNo
                            );
                            if (isCorrectDirectory == DialogResult.Yes) { return modsDirectoryInfo; }
                        }
                    }
                    catch/*(Exception e)*/ {}
                }
            }
            return null;
        }

        public static void LoadSettings()
        {

            LoadServerDatabases();

            foreach (ToolCommand command in GlobalToolCommands.Value)
            {
                if (command.IsGlobalToolCommand == false) { command.IsGlobalToolCommand = true; }
            }
            
            GlobalToolCommands.Value.RemoveAll(x => x.ConditionType != ToolCommand.TriggerType.Daily && x.ConditionType != ToolCommand.TriggerType.EveryXMinutes);

            RepopulateConditionalCommandsDropdown = true;

            form.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle.Checked = settings_PlaySoundOnPlayerJoin.Value;
            form.fileMenuSettingsItemPlaySoundOnPlayerJoinToggle.CheckState = settings_PlaySoundOnPlayerJoin.Value ? CheckState.Checked : CheckState.Unchecked;
            form.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle.Checked = settings_PlaySoundOnPlayerLeave.Value;
            form.fileMenuSettingsItemPlaySoundOnPlayerLeaveToggle.CheckState = settings_PlaySoundOnPlayerLeave.Value ? CheckState.Checked : CheckState.Unchecked;

            // Load scoreboard settings
            Scoreboard.Layout();
            ResizeRequired = true;

        }

        public static void LoadServerDatabases() 
        {

            // Make sure connection Ids list exists
            if (ConnectionIdsList?.Value == null) {
                ConnectionIdsList = new SavedSetting<List<string>>("ConnectionIdsList", new List<string>());
            }

            // If it's empty, add a connection id
            if (ConnectionIdsList.Value.Count > 0) {
                // Create Connection for each existing connectionId
                foreach (string connectionId in ConnectionIdsList.Value) {
                    new Connection(connectionId);
                }
            }


   //         // Create Servers folder if it doesn't exist
   //         if (!Directory.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Servers"))
			//{
   //             try { Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Servers"); }
   //             catch (Exception e) { 
   //                 MessageBox.Show(
   //                     "Unable to create required directory to store server database files.\n\n"
   //                     + "Please run this application as an administrator, or edit the permissions for this application's directory to enable reading and writing of contained files and subdirectories.\n\n"
   //                     + "In case the error was not permissions related, here is the exception information for troubleshooting purposes:\n\n"
   //                     + e.ToString(), 
   //                     "Server Database Directory Creation Failed", 
   //                     MessageBoxButtons.OK
   //                 );
   //                 Application.Exit();
   //             }
			//}

   //         // Gather files in databases folder
   //         FileInfo[] files = new DirectoryInfo(
   //             Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
   //             + "\\Servers"
   //         ).GetFiles();

   //         // Generate connection strings for each .db file found
   //         List<string> connectionStrings = new List<string>();
   //         if (files.Any(file => file.Name.EndsWith(".db")))
   //         {
   //             foreach (FileInfo file in files)
   //             {
   //                 if (file.Name.EndsWith(".db")) {
   //                     //connectionStrings.Add(string.Format("Data Source={0};Version=3;MultipleActiveResultSets=true;", file.FullName));
   //                     connectionStrings.Add(string.Format("Data Source={0};", file.FullName));
   //                 }
   //             }
   //         }

			//// Create Connection for each found database
			//foreach (string connectionString in connectionStrings) { 
   //             new Connection(connectionString); 
   //         }

        }

        public static string GetNextConnectionId()
        {
            //int dbNumber = 0;
            //string dbName = "Server";
            //string dbExtension = ".db";
            //string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Servers\\";
            //while (true)
            //{
            //    if (File.Exists(path + dbName + dbNumber.ToString() + dbExtension)) { dbNumber++; continue; }
            //    return string.Format("Data Source={0};", path + dbName + dbNumber.ToString() + dbExtension);
            //    //return string.Format("Data Source={0};Version=3;MultipleActiveResultSets=true;", path + dbName + dbNumber.ToString() + dbExtension);
            //}

            try { return (ConnectionIdsList.Value.Count + 1).ToString(); }
            catch { return "1"; }

        }

        private static void DeleteSelectedDatabase(object sender = null, EventArgs a = null)
        {
            //if ( ActiveEmployeeService != null && File.Exists(ActiveEmployeeService.DBFilePath))
            //if (ActiveDB.EmployeesTable != null)
            //{

            //    DialogResult confirmResult = MessageBox.Show(
            //        "Are you sure you want to delete the database '" + ActiveDB.DBFilePath + "'?\n" +
            //        "If you click \"OK\" the file '" + ActiveDB.DBFileName + "' will be deleted permanently.",
            //        "Confirm Database Deletion", MessageBoxButtons.OKCancel
            //    );
            //    if (confirmResult != DialogResult.OK) { return; }

            //    if (Databases.Contains(ActiveDB))
            //    {
            //        Databases.Remove(ActiveDB);
            //    }

            //    comboBoxDatabaseSelection.Items.Clear();
            //    listBoxEmployees.Items.Clear();

            //    ActiveDB.Dispose();
            //    try { File.Delete(ActiveDB.DBFilePath); }
            //    catch (Exception ex) { throw new Exception("Database Deletion Exception: " + ex.ToString(), ex); }

            //    MessageBox.Show(
            //            "Database '" + ActiveDB.DBFilePath + "' has been deleted successfully.",
            //            "Database Deletion Successful", MessageBoxButtons.OK
            //    );

            //    ReadDatabases();

            //}
            //else
            //{
            //    if (ActiveDB.EmployeesTable == null) // No active employee table - therefore no active database
            //    {
            //        MessageBox.Show(
            //            "No database database is loaded as the active employee database," +
            //            " so database deletion was unable to be performed.",
            //            "Database Deletion Failed", MessageBoxButtons.OK
            //        );
            //    }
            //    else if (!File.Exists(ActiveDB.EmployeesTable.DatabaseService.DBFilePath)) // Specified database file does not exist
            //    {
            //        MessageBox.Show(
            //            "Attempted to locate database file '" + ActiveDB.EmployeesTable.DatabaseService.DBFilePath + "'," +
            //            "however, that database file has been moved, renamed, or deleted since the application initially loaded it," +
            //            " so database deletion was unable to be performed.",
            //            "Database Deletion Failed", MessageBoxButtons.OK
            //        );
            //    }
            //}
        }
                
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        
        public static void SaveSettings()
        {
            TranslationAPIKey.Save();
            TranslatedCharactersThisBillingCycle.Save();
            TranslateBillingCycleDateTime.Save();
            GlobalToolCommands.Save();            
            foreach (Connection connection in connectionList) { connection.SaveSettings(); }
        }

        /// <summary>
        /// This method will check the Google Translate billing cycle date against the current date and reset the translated character count if needed.
        /// <br>If this server does not have Translation features enabled, this method will do nothing.</br>
        /// </summary>
        public void UpdateTranslationUsageTrackingData(bool forceUpdate = false)
        {
            if (CanTranslate || forceUpdate) {
                // Billing cycle ended
                if (DateTime.Now.Month > TranslateBillingCycleDateTime.Value.Month &&
                    DateTime.Now.Day >= TranslateBillingCycleDateTime.Value.Day) {
                    string output =
                        "Updating translation usage data.\n" +
                        $"For the billing period of {TranslateBillingCycleDateTime.Value.ToShortDateString()} to {DateTime.Now.ToShortDateString()}, " +
                        $"this application tracked {TranslatedCharactersThisBillingCycle.Value} characters sent for translation.\n" +
                        (TranslatedCharactersThisBillingCycle.Value > 500000
                            ? "You may have been charged for going over the 500,000-character free translation limit.\n"
                            : "You have stayed under the 500,000-character free translation limit, so you have probably not been charged anything.\n") +
                        "To verify the official service usage and charges determined by Google, please check your Google Cloud Billing Console for details.";
                    Log(output);
                    TranslateBillingCycleDateTime.Value = DateTime.Now;
                    TranslatedCharactersThisBillingCycle.Value = 0;
                    SaveSettings();
                }
                else if (DateTime.Now < TranslateBillingCycleDateTime.Value) {
                    TranslateBillingCycleDateTime.Value = DateTime.Now;
                    Log("The Translation usage billing cycle has been updated.\n" +
                        "If you are using the translation feature, please verify the character count and billing cycle in the Translation Menu " +
                        "match the Google Translate usage and billing information in your Google Cloud Billing Console."
                    );
                    SaveSettings();
                }
            }
        }

        #endregion

        #region Form/App Events & Utilities

        private void App_Paint(object sender, PaintEventArgs e)
        {
            if (currentConnection == null) { return; }
            if (ResizeRequired) {  ResizeToFitScoreboard(); }
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
                        
                        // Enable/Disable relevant admin authorization commands
                        if (currentConnection?.Settings?.AuthorizedUIDs != null) {
                            if (currentConnection.Settings.AuthorizedUIDs.Contains(contextPlayer.Uid)) {
                                scoreBoardContextMenu.Items[6].Enabled = false;
                                scoreBoardContextMenu.Items[7].Enabled = true;
                            }
                            else {
                                scoreBoardContextMenu.Items[6].Enabled = true;
                                scoreBoardContextMenu.Items[7].Enabled = false;
                            }
                        }
                        else {
                            scoreBoardContextMenu.Items[6].Enabled = false;
                            scoreBoardContextMenu.Items[7].Enabled = false;
                        }

                        if (CurrentConnectionHooked) {
                            currentConnection.GetPlayerTeams();
                            int teamIndex = currentConnection.GetTeamIndex(contextPlayer);
                            if (teamIndex > -1 && teamIndex < 8) {
                                sendToTeamItem.Enabled = true;
                                sendToTeamItem.Visible = true;
                                foreach (ToolStripItem item in sendToTeamItem.DropDownItems()) { 
                                    item.Enabled = true; 
                                }
                                sendToTeamItem.DropDownItems()[teamIndex].Enabled = false;
                            }
                            else {
                                sendToTeamItem.Enabled = false;
                                sendToTeamItem.Visible = false;
                            }
                        }
                        else {
                            sendToTeamItem.Enabled = false;
                            sendToTeamItem.Visible = false;
						}

                        this.scoreBoardContextMenu.Show(this, new Point(e.X, e.Y));
                    }
                }
                catch (Exception ex)
                {
                    App.Log("Error Generating Scoreboard Context Menu: " + ex.Message);
                    Console.WriteLine("Error Generating Scoreboard Context Menu: " + ex.Message);                    
                }
            }

        }

        private void SendContextPlayerToTeam(int teamIndex)
		{
            if (contextPlayer != null && 
                contextPlayer.Team != teamIndex && 
                (currentConnection?.State?.teamGame ?? false)) 
            {
                currentConnection.SetPlayerTeam(null, new Tuple<PlayerInfo, int>(contextPlayer, teamIndex));
			}
		}

        private void App_MouseLeave(object sender, EventArgs e)
        {
            mousePoint = new Point(-1, -1);
        }

        private void formApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Connection c in connectionList) { c.Close(); }
        }

        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked) {
                e.Cancel = true;
            }
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void DisplayOnlyTextBox_MouseUp(object sender, MouseEventArgs e)
		{
            // Don't allow the cursor to remain in a display-only text box if there is no text selected, it looks bad
            if (((TextBoxAutoScroll)sender).SelectionLength == 0) { ((TextBoxAutoScroll)sender).Parent.Focus(); }
        }

        protected override bool ProcessTabKey(bool forward)
        {
            Control control = this.ActiveControl;
            if (control != null && control is TextBox) {
                TextBox textBox = (TextBox)control;
                if (textBox == textBoxConsoleTextEntry && textBox.Text.Length > 0) {
                    return false;
                }
            }
            return base.ProcessTabKey(forward); // process TAB key as normal
        }

        private static void PopulateConditionalCommandsDropdown()
        {
            ToolStripItemCollection dropdownItems = form.fileMenuCommandsItem.DropDownItems;

            ToolStripItem manageCommandsOption = dropdownItems[0];

            dropdownItems.Clear();

            dropdownItems.Add(manageCommandsOption);

            if (currentConnection != null && currentConnection.Settings.Commands.Count > 0) {

                dropdownItems.Add("- Current Server Commands -");

                foreach (ToolCommand command in currentConnection.Settings.Commands) {
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

            if (GlobalToolCommands.Value.Count > 0) {

                dropdownItems.Add("- Global Commands -");

                foreach (ToolCommand command in GlobalToolCommands.Value) {
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

        public static DialogResult ShowMessageBox(string text_mb, string title_mb, MessageBoxButtons buttons_mb, MessageBoxIcon icon_mb)
        {
            return (DialogResult)form.Invoke(
                new Func<string, string, MessageBoxButtons, MessageBoxIcon, DialogResult>(
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
            foreach (Connection potentialConnection in connectionList) {
                if (potentialConnection.Settings.IsSameServerReference(serverInfo)) {
                    return potentialConnection;
                }
            }
            return null;
        }

        public static FileInfo GetFileInfo(string path)
        {
            try {
                return new FileInfo(path);
            }
            catch {
                return null;
            }
        }

        private static void ResizeToFitScoreboard()
        {

            form.tabControlServerInterfaces.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            form.UpdateBounds();
            //form.MinimumSize = new Size(Scoreboard.Width + 25, 370 + 5 + Scoreboard.Height);
            form.MinimumSize = new Size(
                Scoreboard.Width + 25,
                15       // padding
                + 32    // window title bar height
                + 180   // tabControlServerInterfaces default height
                + form.pictureBoxMapAndStatusOverlay.Height
                + form.statusStripVersion.Height
                + Scoreboard.Height
            );

            form.Size = form.MinimumSize;
            form.UpdateBounds();
            form.tabControlServerInterfaces.Location = new Point(0, 5 + Scoreboard.Height);
            form.tabControlServerInterfaces.Height = 180;
            form.UpdateBounds();
            form.tabControlServerInterfaces.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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

            using (var graphics = Graphics.FromImage(destImage)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;

        }

        public static string DateTimeDisplayString(DateTime? dateTime = null)
        {
            return (dateTime.HasValue && dateTime.Value != null)
                ? dateTime?.ToUniversalTime().ToString(ChatMessageDateTimeFormatString)
                : DateTime.Now.ToUniversalTime().ToString(ChatMessageDateTimeFormatString);
        }
        public static string DateTimeUTC()
        {
            return DateTimeDisplayString();
        }

        public static void ErrorAppFailedTo(string appFailedTo, bool includeGenericHelp = true)
		{
            if (includeGenericHelp) {
                string genericHelp = "The following steps may help, but are not guaranteed solutions:\n" +
                                     "\t- Run the application with adminstrative privileges.\n" +
                                     "\t- Restart the application and try again.";
                MessageBox.Show($"{InternalAppName} failed to {appFailedTo}.\n\n{genericHelp}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                MessageBox.Show($"{InternalAppName} failed to {appFailedTo}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void Error(string title, string message)
		{
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

        #endregion

        #region Menu Items

        //private void menuItemGenerateAppConfig_Click(object sender, EventArgs e)
        //{
        //    new ConfigExport().ShowDialog();
        //}
        //private void menuItemDownloadTheAndroidApp_Click(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Process.Start("https://play.google.com/store/apps/details?id=jaron.rcontool.com.rcontool");
        //}

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
            Application.Exit();
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
            settings_PlaySoundOnPlayerJoin.Value = !settings_PlaySoundOnPlayerJoin.Value;
            settings_PlaySoundOnPlayerJoin.Save();
        }

        private void toolStripMenuItemPlaySoundOnPlayerLeave_Click(object sender, EventArgs e)
        {
            settings_PlaySoundOnPlayerLeave.Value = !settings_PlaySoundOnPlayerLeave.Value;
            settings_PlaySoundOnPlayerLeave.Save();
        }

        private void toolStripMenuItemChangeScoreboardFontSize_Click(object sender, EventArgs e)
        {
            int promptResult = Prompt.ShowIntDialog(
                "Select a font size for the scoreboard text.",
                "Change Scoreboard Font Size",
                Scoreboard.FontSize.Value
            );
            if (promptResult == -1) { return; }
            else if (promptResult < 4) { 
                MessageBox.Show(
                    "The minimum font size is 4, the font size cannot be set to " + promptResult.ToString(), 
                    "Invalid Font Size", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                ); 
            }
            else {
                Scoreboard.FontSize.Value = promptResult;
                ResizeToFitScoreboard();
            }
        }

        #endregion

        #region Console Interface

        private void textBoxConsoleText_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxConsoleText_TextChanged(sender, null);
        }

        private void textBoxConsoleText_TextChanged(object sender, EventArgs e)
        {

            //Trims first character of textbox text IF it's a whitespace character
            //if (!string.IsNullOrWhiteSpace(textBoxConsoleText.Text) 
            //    && textBoxConsoleText.Text.Length > 0
            //    && char.IsWhiteSpace(textBoxConsoleText.Text[0])) 
            //{
            //    textBoxConsoleText.Text = textBoxConsoleText.Text.Trim();
            //}

            if (!string.IsNullOrWhiteSpace(textBoxAutoScrollConsoleText.Text)
                && textBoxAutoScrollConsoleText.Text.Length > 0
                && char.IsWhiteSpace(textBoxAutoScrollConsoleText.Text[0])) {
                textBoxAutoScrollConsoleText.Text = textBoxAutoScrollConsoleText.Text.Trim();
            }

        }

        private void textBoxConsoleTextEntry_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {

                // Trigger Console History Cycling
                if (currentConnection != null 
                    && (currentConnection.IsCyclingConsoleHistory || textBoxConsoleTextEntry.Text.Length == 0) 
                    && currentConnection.ConsoleHistory.Count > 0)
                {                    

                    if ( !currentConnection.IsCyclingConsoleHistory) { currentConnection.IsCyclingConsoleHistory = true; }                    
                    
                    // Up Key
                    if (e.KeyCode == Keys.Up) { 
                        // if there are older items in the history, cycle to the next oldest one
                        if (currentConnection.ConsoleHistoryCurrentIndex < currentConnection.ConsoleHistory.Count - 1) { 
                            currentConnection.ConsoleHistoryCurrentIndex++;
                            textBoxConsoleTextEntry.Text = 
                                currentConnection.ConsoleHistory.ElementAt(currentConnection.ConsoleHistoryCurrentIndex);
                            textBoxConsoleTextEntry.SelectionStart = textBoxConsoleTextEntry.TextLength;
                            textBoxConsoleTextEntry.SelectionLength = 0;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                    }
                    // Down Key
                    else {
                        // if there are more recent items in the history, cycle to the next most recent one
                        if (currentConnection.ConsoleHistoryCurrentIndex >= -1 && currentConnection.ConsoleHistoryCurrentIndex <= currentConnection.ConsoleHistory.Count)
                        {
                            if (currentConnection.ConsoleHistoryCurrentIndex > -1)
                            {
                                currentConnection.ConsoleHistoryCurrentIndex--;
                                if (currentConnection.ConsoleHistoryCurrentIndex == -1)
                                {
                                    textBoxConsoleTextEntry.Text = "";
                                }
                                else
                                {
                                    textBoxConsoleTextEntry.Text =
                                        currentConnection.ConsoleHistory.ElementAt(currentConnection.ConsoleHistoryCurrentIndex);
                                    textBoxConsoleTextEntry.SelectionStart = textBoxConsoleTextEntry.TextLength;
                                    textBoxConsoleTextEntry.SelectionLength = 0;
                                }
                                // Only handle/suppress the event if cycling occured
                                // that way the autocomplete options can be triggered by using down-key on the empty text entry
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                while (textBoxConsoleTextEntry.Text.Contains("\r"))
                {
                    textBoxConsoleTextEntry.Text = textBoxConsoleTextEntry.Text.Replace("\r", "");
                }

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

        private void textBoxConsoleTextEntry_KeyUp(object sender, KeyEventArgs e)
        {
            
            //TODO explain that you can use the TAB key in the console text box to cycle through all commands containing your currently entered text
            if (IsQueryingConsoleCommandMatch)
			{
                
                //Don't allow modifier key presses to exit the query cycle
                if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey 
                    || e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey
                    || e.KeyCode == Keys.Alt) { return; }

                if (e.KeyCode != Keys.Tab) {
                    IsQueryingConsoleCommandMatch = false;
                    ConsoleCommandQueryMatchIndex = 0;
                    ConsoleCommandQueryMatches.Clear();
                    ConsoleCommandQueryString = "";
                }
                else
				{
                    if (e.Modifiers.HasFlag(Keys.Shift))
                    {
                        ConsoleCommandQueryMatchIndex--;
                        if (ConsoleCommandQueryMatchIndex < 0) { ConsoleCommandQueryMatchIndex = Math.Max(0, ConsoleCommandQueryMatches.Count - 1); }
                    }
                    else
					{
                        ConsoleCommandQueryMatchIndex++;
                        if (ConsoleCommandQueryMatchIndex >= ConsoleCommandQueryMatches.Count) { ConsoleCommandQueryMatchIndex = 0; }
                    }
                    
                    if (ConsoleCommandQueryMatches.Count > 0)
					{
                        textBoxConsoleTextEntry.Text = ConsoleCommandQueryMatches[ConsoleCommandQueryMatchIndex];
                        textBoxConsoleTextEntry.SelectionStart =
                            textBoxConsoleTextEntry.Text.IndexOf(ConsoleCommandQueryString)
                            + ConsoleCommandQueryString.Length;
                        textBoxConsoleTextEntry.SelectionLength =
                            textBoxConsoleTextEntry.Text.Length - textBoxConsoleTextEntry.SelectionStart - 1;
                    }
                    e.SuppressKeyPress = true;
                    e.Handled = true;
				}
            }
            else
			{
                if (e.KeyCode != Keys.Tab) { return; }
                if (!string.IsNullOrWhiteSpace(textBoxConsoleTextEntry.Text)
                && !ConsoleAutoCompleteStrings.Any(x => x.StartsWith(textBoxConsoleTextEntry.Text)))
                {
                    ConsoleCommandQueryString = textBoxConsoleTextEntry.Text;
                    ConsoleCommandQueryMatches = ConsoleAutoCompleteStrings.Where(x => x.Contains(ConsoleCommandQueryString)).ToList();
                    if (ConsoleCommandQueryMatches.Count > 0) {
                        ConsoleCommandQueryMatchIndex = 0;
                        textBoxConsoleTextEntry.Text = ConsoleCommandQueryMatches[ConsoleCommandQueryMatchIndex];
                        IsQueryingConsoleCommandMatch = true;
                        textBoxConsoleTextEntry.SelectionStart =
                            textBoxConsoleTextEntry.Text.IndexOf(ConsoleCommandQueryString) 
                            + ConsoleCommandQueryString.Length;
                        textBoxConsoleTextEntry.SelectionLength =
                            textBoxConsoleTextEntry.Text.Length - textBoxConsoleTextEntry.SelectionStart - 1;
                    }
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
        }

        private void buttonConsoleTextSend_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBoxConsoleTextEntry.Text) && currentConnection != null)
            {
                // If some kind of text was entered, reset console history cycling status and add the entered text to the console history
                currentConnection.IsCyclingConsoleHistory = false;
                currentConnection.ConsoleHistoryCurrentIndex = -1;
                currentConnection.ConsoleHistory.Push(textBoxConsoleTextEntry.Text);

                string logString = textBoxConsoleTextEntry.Text;

                // Handle unintuitive Game.Map command quirk when trying to load built-in maps
                if (textBoxConsoleTextEntry.Text.StartsWith("Game.Map ")) {
                    string baseMapName = MapVariant.TryGetBaseMapInternalNameFromDisplayName(
                        textBoxConsoleTextEntry.Text.TrimStart("Game.Map ".Length).Trim().RemoveAll('"')
                    );
                    if (!string.IsNullOrWhiteSpace(baseMapName)) {
                        if (baseMapName.Contains(' ')) { baseMapName = $"\"{baseMapName}\""; }
                        logString = textBoxConsoleTextEntry.Text + " -> " + baseMapName;
                        textBoxConsoleTextEntry.Text = "Game.Map " + baseMapName;
                    }
				}

                // Handle unintuitive Game.GameType command quirk when trying to load built-in variants
                else if (textBoxConsoleTextEntry.Text.StartsWith("Game.GameType ")) {
                    string baseVariantName = GameVariant.TryGetBaseGameInternalNameFromDisplayName(
                        textBoxConsoleTextEntry.Text.TrimStart("Game.GameType ".Length).Trim().RemoveAll('"')
                    );
                    if (!string.IsNullOrWhiteSpace(baseVariantName)) {
                        if (baseVariantName.Contains(' ')) { baseVariantName = $"\"{baseVariantName}\""; }
                        logString = textBoxConsoleTextEntry.Text + " -> " + baseVariantName;
                        textBoxConsoleTextEntry.Text = "Game.GameType " + baseVariantName;
                    }
                }

                if (TryDirectConsoleCommand(textBoxConsoleTextEntry.Text)) {
                    currentConnection.PrintToConsole(textBoxConsoleTextEntry.Text);
                }
                else {
                    // Print to console, and send the command to RCON
                    //currentConnection.PrintToConsole(logString);
                    currentConnection.RconCommandQueue.Enqueue(
                        RconCommand.ConsoleLogCommand(
                            textBoxConsoleTextEntry.Text,
                            logString,
                            "Console"
                        )
                    );
                }
                textBoxConsoleTextEntry.Clear();
                textBoxConsoleTextEntry.AutoCompleteMode = AutoCompleteMode.None;
                textBoxConsoleTextEntry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                
                // Reset command querying status
                IsQueryingConsoleCommandMatch = false;
                ConsoleCommandQueryMatches.Clear();
                ConsoleCommandQueryString = "";
                ConsoleCommandQueryMatchIndex = 0;

            }
        }

        private bool TryDirectConsoleCommand(string command)
		{
            if (command.ToLowerInvariant().Trim().StartsWith("server.balanceteams")) {
                if (CurrentConnectionHooked) {
                    try {
                        List<Tuple<PlayerInfo, int>> teamAssignments;
                        string numberOfTeamsParameter = command.TrimStart("server.balanceteams ".Length);
                        if (!string.IsNullOrWhiteSpace(numberOfTeamsParameter) && int.TryParse(numberOfTeamsParameter, out int numTeams)) {
                            teamAssignments = currentConnection?.GenerateBalancedTeamList(numTeams);
                        }
                        else {
                            teamAssignments = currentConnection?.GenerateBalancedTeamList(-1);
                        }
                        if (teamAssignments != null) { currentConnection.SetPlayerTeam("Teams have been balanced.", teamAssignments.ToArray()); }
                        else { textBoxConsoleTextEntry.Text += "\nServer.BalanceTeams Failed: Failed to generate balanced team list."; }
                    }
                    catch { }
                }
                else {
                    textBoxConsoleTextEntry.Text += "\nServer.BalanceTeams Failed: ServerHook is not enabled.";
                }
                return true;
            }
            else if (command.ToLowerInvariant().Trim().StartsWith("server.addmaphint")) {
                try {
                    List<string> mapHintArgs = command.TrimStart("server.addmaphint".Length).Trim().Split(',').ToList();
                    if (mapHintArgs.Count != 3) { textBoxConsoleTextEntry.Text += "\nServer.AddMapHint Failed.\nUsage: 'Server.AddMapHint mapName, mapHintMessage, hintFrequencyInSeconds'\nExample: 'Server.AddMapHint guardian, Don't fall off the map, 60'"; return true; }
                    else {
                        string mapName = mapHintArgs[0].Trim();
                        string mapHint = mapHintArgs[1].Trim();
                        if (string.IsNullOrWhiteSpace(mapName) || string.IsNullOrWhiteSpace(mapHint) || !int.TryParse(mapHintArgs[2], out int hintFrequency) || hintFrequency < 1) {
                            textBoxConsoleTextEntry.Text += "\nServer.AddMapHint Failed.\nUsage: 'Server.AddMapHint mapName, mapHintMessage, hintFrequencyInSeconds'\nExample: 'Server.AddMapHint guardian, Don't fall off the map, 60'"; return true;
                        }
                        else { currentConnection.mapHints.Add(new MapHint(mapName, mapHint, hintFrequency)); }
					}
                }
                catch { }
                return true;
			}
            else if (command.ToLowerInvariant().Trim().StartsWith("server.enableshuffleteamsrandomization")) {
                if (CurrentConnectionHooked) {
                    try { currentConnection.EnableShuffleTeamsRandomization(); }
                    catch { }
                }
                else {
                    textBoxConsoleTextEntry.Text += "\nServer.EnableShuffleTeamsRandomization Failed: ServerHook is not enabled.";
                }
                return true;
            }
            else if (command.ToLowerInvariant().Trim().StartsWith("server.disableshuffleteamsrandomization")) {
                if (CurrentConnectionHooked) {
                    try { currentConnection.DisableShuffleTeamsRandomization(); }
                    catch { }
                }
                else {
                    textBoxConsoleTextEntry.Text += "\nServer.DisableShuffleTeamsRandomization Failed: ServerHook is not enabled.";
                }
                return true;
            }
            // Server.StatusPacket - print server status packet to the console
            else if (command.Trim().StartsWith("Server.StatusPacket")) {
                if (CurrentConnectionHooked) {
                    textBoxConsoleTextEntry.Text += "\n" + (currentConnection.GetServerStatusPacketStringFromMemory(true) ?? "NULL Status Packet");
                }
                else {
                    textBoxConsoleTextEntry.Text += "\nServer.StatusPacket Failed: ServerHook is not enabled.";
                }
                return true;
            }
            // Application.VerifyServerHook - print details on verification that ServerHook server matches RCON connection server
            else if (command.Trim().StartsWith("Application.VerifyServerHook")) {
                if (CurrentConnectionHooked) {
                    //TODO remove this call, ServerProcessMatchesConnection should not be called from outside of Connection.AttemptServerHook
                    if (currentConnection.ServerProcessMatchesConnection()) {
                        textBoxConsoleTextEntry.Text = command.Trim() + ": ServerHook is valid.";
                    }
                    else {
                        textBoxConsoleTextEntry.Text = command.Trim() + ": ServerHook is not valid.";
                    }
                }
                else {
                    textBoxConsoleTextEntry.Text = command.Trim() + ": ServerHook is not enabled.";
                }
                return true;
            }
            else if (command.Trim().StartsWith("Application.ShowServerJson")) {
                textBoxConsoleTextEntry.Text += ": JSON Filter Disabled";
                FilterServerJson = false; return true;
            }
            else if (command.Trim().StartsWith("Application.HideServerJson")) {
                textBoxConsoleTextEntry.Text += ": JSON Filter Enabled";
                FilterServerJson = true; return true;
            }
            else if (command.Trim().StartsWith("Server.ToggleShuffleTeamsPlayerCountRequirement")) {
                if (CurrentConnectionHooked) {
                    textBoxConsoleTextEntry.Text += 
                        $"-> {currentConnection.ToggleShuffleTeamsPlayerCountRequirement()} player(s) required.";
                }
                else { 
                    textBoxConsoleTextEntry.Text += 
                        $"\nCommand failed, ServerHook not active."; 
                }
                return true;
			}
            return false;
		}

        #endregion

        #region Chat Interface

        private void textBoxChatText_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxChatText_TextChanged(sender, null);
        }

        private void textBoxChatText_TextChanged(object sender, EventArgs e)
        {
            //Trims first character of textbox text IF it's a whitespace character
            //if (!string.IsNullOrWhiteSpace(textBoxChatText.Text)
            //    && textBoxChatText.Text.Length > 0
            //    && char.IsWhiteSpace(textBoxChatText.Text[0])) {
            //    textBoxChatText.Text = textBoxChatText.Text.Trim();
            //}

            if (!string.IsNullOrWhiteSpace(textBoxAutoScrollChatText.Text)
                && textBoxAutoScrollChatText.Text.Length > 0
                && char.IsWhiteSpace(textBoxAutoScrollChatText.Text[0])) {
                textBoxAutoScrollChatText.Text = textBoxAutoScrollChatText.Text.Trim();
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

        #endregion

        #region Player Log Interface

        private void textBoxPlayerLog_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxPlayerLog_TextChanged(sender, null);
        }

        private void textBoxPlayerLog_TextChanged(object sender, EventArgs e)
        {
            //Trims first character of textbox text IF it's a whitespace character
            if (!string.IsNullOrWhiteSpace(textBoxAutoScrollPlayerLog.Text)
                && textBoxAutoScrollPlayerLog.Text.Length > 0
                && char.IsWhiteSpace(textBoxAutoScrollPlayerLog.Text[0])) {
                textBoxAutoScrollPlayerLog.Text = textBoxAutoScrollPlayerLog.Text.Trim();
            }
        }

        #endregion

        #region App Log Interface

        private void textBoxAppLog_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxAppLog_TextChanged(sender, null);
        }

        private void textBoxAppLog_TextChanged(object sender, EventArgs e)
        {
            //Trims first character of textbox text IF it's a whitespace character
   //         if (!string.IsNullOrWhiteSpace(textBoxAppLog.Text)
			//		&& textBoxAppLog.Text.Length > 0
			//		&& char.IsWhiteSpace(textBoxAppLog.Text[0])) {
   //             textBoxAppLog.Text = textBoxAppLog.Text.Trim();
			//}

            if (!string.IsNullOrWhiteSpace(textBoxAutoScrollApplicationLog.Text)
                    && textBoxAutoScrollApplicationLog.Text.Length > 0
                    && char.IsWhiteSpace(textBoxAutoScrollApplicationLog.Text[0])) {
                textBoxAutoScrollApplicationLog.Text = textBoxAutoScrollApplicationLog.Text.Trim();
            }
        }

        #endregion

        #region Server Controls Interface

        private void label_MouseRightClickCopyContents(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                string labelText = ((Label)sender)?.Text ?? "";
                Clipboard.SetText(labelText);
                App.Log($"\"{labelText}\" copied to the clipboard.");
            }
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to Start the game?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Game.Start",
                        "Game.Start", 
                        "Game Start Button"
                    )
                );
            }
        }

        private void buttonStopGame_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to Stop the game?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Game.stop",
                        "Game.stop",
                        "Game Stop Button"
                    )
                );
            }
        }

        private void buttonToggleAssasinations_Click(object sender, EventArgs e)
        {
            bool originallyEnabled = statusButtonAssassinationToggle.ToolTipText.StartsWith("Assassinations Enabled");
            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    $"Server.AssassinationEnabled {(originallyEnabled ? "0" : "1")}",
                    $"Server.AssassinationEnabled {(originallyEnabled ? "0" : "1")}",
                    "Assassination Toggle Button"
                )
            );

            statusButtonAssassinationToggle.Visible = false;
            if (originallyEnabled) {
                statusButtonAssassinationToggle.ToolTipText = "Assassinations Disabled: Click to Toggle";
                statusButtonAssassinationToggle.Image = Resources.AssassinationTiny_Disabled;
            }
            else {
                statusButtonAssassinationToggle.ToolTipText = "Assassinations Enabled: Click to Toggle";
                statusButtonAssassinationToggle.Image = Resources.AssassinationTiny;
            }
            statusButtonAssassinationToggle.Visible = true;
        }

        private void buttonToggleSprint_Click(object sender, EventArgs e)
        {
            statusButtonSprintToggle.Visible = false;
            if (statusButtonSprintToggle.ToolTipText.StartsWith("Sprint Disabled")) {
                // turn on sprint
                currentConnection.RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Server.SprintEnabled 1",
                        "Server.SprintEnabled 1",
                        "Sprint Toggle Button (Enable)"
                    )
                );
                statusButtonSprintToggle.ToolTipText = "Sprint Enabled: Click to Toggle";
                statusButtonSprintToggle.Image = Resources.SprintTiny;
            }
            else if (statusButtonSprintToggle.ToolTipText.StartsWith("Sprint Enabled")) {
                // turn on unlimited sprint
                currentConnection.RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Server.UnlimitedSprint 1",
                        "Server.UnlimitedSprint 1",
                        "Sprint Toggle Button (Unlimited)"
                    )
                );
                statusButtonSprintToggle.ToolTipText = "Unlimited Sprint Enabled: Click to Toggle";
                statusButtonSprintToggle.Image = Resources.SprintTiny_Unlimited;
            }
            else {
                // turn off sprint + unlimited sprint
                currentConnection.RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Server.SprintEnabled 0",
                        "Server.SprintEnabled 0",
                        "Sprint Toggle Button (Disable)"
                    )
                );
                currentConnection.RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Server.UnlimitedSprint 0",
                        "Server.UnlimitedSprint 0",
                        "Sprint Toggle Button (Disable)"
                    )
                );
                statusButtonSprintToggle.ToolTipText = "Sprint Disabled: Click to Toggle";
                statusButtonSprintToggle.Image = Resources.SprintTiny_Disabled;
            }
            statusButtonSprintToggle.Visible = true;
        }

        private void fileMenuCommandsItem_DropDownOpening(object sender, EventArgs e)
        {
            fileMenuCommandsItemShuffleTeamsCommandToggle.Checked =
                currentConnection?.Settings?.ChatCommandShuffleTeamsEnabled ?? false;
            fileMenuCommandsItemShuffleTeamsCommandToggle.CheckState = 
                fileMenuCommandsItemShuffleTeamsCommandToggle.Checked ? CheckState.Checked : CheckState.Unchecked;

            fileMenuCommandsItemKickPlayerCommandToggle.Checked =
                currentConnection?.Settings?.ChatCommandKickPlayerEnabled ?? false;
            fileMenuCommandsItemKickPlayerCommandToggle.CheckState =
                fileMenuCommandsItemKickPlayerCommandToggle.Checked ? CheckState.Checked : CheckState.Unchecked;

            fileMenuCommandsItemEndGameCommandToggle.Checked =
                currentConnection?.Settings?.ChatCommandEndGameEnabled ?? false;
            fileMenuCommandsItemEndGameCommandToggle.CheckState =
                fileMenuCommandsItemEndGameCommandToggle.Checked ? CheckState.Checked : CheckState.Unchecked;
        }

        private void buttonToggleShuffleTeamsCommand_Click(object sender, EventArgs e)
        {
            //Server.ChatCommandShuffleTeamsEnabled "1"
            currentConnection.Settings.ChatCommandShuffleTeamsEnabled = !currentConnection.Settings.ChatCommandShuffleTeamsEnabled;
            fileMenuCommandsItemShuffleTeamsCommandToggle.Checked = 
                currentConnection.Settings.ChatCommandShuffleTeamsEnabled;
            fileMenuCommandsItemShuffleTeamsCommandToggle.CheckState =
                fileMenuCommandsItemShuffleTeamsCommandToggle.Checked ? CheckState.Checked : CheckState.Unchecked;
            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.ChatCommandShuffleTeamsEnabled " + (currentConnection.Settings.ChatCommandShuffleTeamsEnabled ? "1" : "0"),
                    "Server.ChatCommandShuffleTeamsEnabled " + (currentConnection.Settings.ChatCommandShuffleTeamsEnabled ? "1" : "0"),
                    "Toggle Command '!shuffleTeams'"
                )
            );
            currentConnection.SaveSettings();
        }

        private void buttonToggleKickPlayerCommand_Click(object sender, EventArgs e)
        {
            //Server.ChatCommandKickPlayerEnabled "1"
            currentConnection.Settings.ChatCommandKickPlayerEnabled = !currentConnection.Settings.ChatCommandKickPlayerEnabled;
            fileMenuCommandsItemKickPlayerCommandToggle.Checked =
                currentConnection?.Settings?.ChatCommandKickPlayerEnabled ?? false;
            fileMenuCommandsItemKickPlayerCommandToggle.CheckState =
                fileMenuCommandsItemKickPlayerCommandToggle.Checked ? CheckState.Checked : CheckState.Unchecked;
            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.ChatCommandKickPlayerEnabled " + (currentConnection.Settings.ChatCommandKickPlayerEnabled ? "1" : "0"),
                    "Server.ChatCommandKickPlayerEnabled " + (currentConnection.Settings.ChatCommandKickPlayerEnabled ? "1" : "0"),
                    "Toggle Command '!kickPlayer'"
                )
            );
            currentConnection.SaveSettings();
        }

        private void buttonToggleEndGameCommand_Click(object sender, EventArgs e)
        {
            //Server.ChatCommandEndGameEnabled "1"
            currentConnection.Settings.ChatCommandEndGameEnabled = !currentConnection.Settings.ChatCommandEndGameEnabled;
            fileMenuCommandsItemEndGameCommandToggle.Checked =
                currentConnection?.Settings?.ChatCommandEndGameEnabled ?? false;
            fileMenuCommandsItemEndGameCommandToggle.CheckState =
                fileMenuCommandsItemEndGameCommandToggle.Checked ? CheckState.Checked : CheckState.Unchecked;
            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.ChatCommandEndGameEnabled " + (currentConnection.Settings.ChatCommandEndGameEnabled ? "1" : "0"),
                    "Server.ChatCommandEndGameEnabled " + (currentConnection.Settings.ChatCommandEndGameEnabled ? "1" : "0"),
                    "Toggle Command '!endGame'"
                )
            );
            currentConnection.SaveSettings();
        }

        private void buttonReloadVotingAndVetoJson_Click(object sender, EventArgs e)
		{
            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.ReloadVotingJson",
                    "Server.ReloadVotingJson",
                    "Reload Vote / Veto Json Button"
                )
            );
            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.ReloadVetoJson",
                    "Server.ReloadVetoJson",
                    "Reload Vote / Veto Json Button"
                )
            );
        }

        private void buttonShuffleTeams_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to shuffle the teams?", "Warning", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                currentConnection.RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Server.ShuffleTeams",
                        "Server.ShuffleTeams",
                        "Shuffle Teams Button"
                    )
                );
            }
        }

        private void comboBoxMaxPlayers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.MaxPlayers " + (string)comboBoxMaxPlayers.SelectedItem,
                    "Server.MaxPlayers " + (string)comboBoxMaxPlayers.SelectedItem,
                    "Max Players Updated"
                )
            );
        }

        private void fileMenuTranslationItem_DropDownOpening(object sender, EventArgs e)
        {
            fileMenuTranslationItemTranslationEnabledButton.Checked =
                CanTranslate && (currentConnection?.Settings?.TranslationEnabled ?? false);
            fileMenuTranslationItemTranslationEnabledButton.CheckState =
                fileMenuTranslationItemTranslationEnabledButton.Checked ? CheckState.Checked : CheckState.Unchecked;

            fileMenuTranslationItemSetCharacterCountAndBillingCycleButton_Click(null, null);
        }

        private void fileMenuTranslationItemTranslationEnabledButton_Click(object sender, EventArgs e)
        {
            currentConnection.Settings.TranslationEnabled = !currentConnection.Settings.TranslationEnabled;
            fileMenuTranslationItemTranslationEnabledButton.Checked = 
                currentConnection.Settings.TranslationEnabled;
            fileMenuTranslationItemTranslationEnabledButton.CheckState =
                currentConnection.Settings.TranslationEnabled ? CheckState.Checked : CheckState.Unchecked;
            currentConnection.SaveSettings();
            if (currentConnection.Settings.TranslationEnabled) {
                fileMenuTranslationItemTranslationEnabledButton.Text = "Translation Enabled";
            }
            else {
                fileMenuTranslationItemTranslationEnabledButton.Text = "Translation Disabled";
            }
            
        }

        private void fileMenuTranslationItemSetGoogleTranslateApiKey_Click(object sender, EventArgs e)
        {
            string promptResult = Prompt.ShowStringDialog(
                "Enter your Google Translate API Key",
                "API Key Entry",
                (TranslationAPIKey.Value ?? "")
            );
            if (string.IsNullOrWhiteSpace(promptResult) || promptResult.Length < 10) {
                TranslationAPIKey.Value = TranslationAPIKey.DefaultValue;
                MessageBox.Show(
                    $"The entered API Key, \"{promptResult ?? ""}\" was not valid.\nFor help, read the API Key section at https://github.com/BIRD-COMMAND/RconTool/",
                    "Invalid API Key",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            else {

                TranslationAPIKey.Value = promptResult;
                SaveSettings();
                if (TranslationClient != null) { TranslationClient.Dispose(); }
                TranslationClient = TranslationClient.CreateFromApiKey(TranslationAPIKey.Value);
                MessageBox.Show(
                    $"Your API Key has been successfully updated to \"{TranslationAPIKey.Value}\".",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void fileMenuTranslationItemSetCharacterCountAndBillingCycleButton_Click(object sender, EventArgs e)
        {
            
            if (sender != null) { new SetTranslationUsageTrackingData().ShowDialog(); }

            fileMenuTranslationItemTranslatedCharactersTextBox.Text =
                $"Translated Characters: {TranslatedCharactersThisBillingCycle.Value}";

            fileMenuTranslationItemTranslationBillingCycleTextBox.Text =
                $@"Billing Cycle: {TranslateBillingCycleDateTime.Value.ToShortDateString()} - {
                    (TranslateBillingCycleDateTime.Value.Month < 12
                        ? new DateTime(TranslateBillingCycleDateTime.Value.Year, TranslateBillingCycleDateTime.Value.Month + 1, TranslateBillingCycleDateTime.Value.Day).ToShortDateString()
                        : new DateTime(TranslateBillingCycleDateTime.Value.Year + 1, 1, TranslateBillingCycleDateTime.Value.Day).ToShortDateString()
                    )
                }";

        }


        private static int[] builtInVariantsPerBaseGame = new int[] { 5, 5, 4, 4, 4, 3, 3, 4 };
        private static readonly Dictionary<GameVariant.BaseGame, int> gametypeContextMenuIndicesByBaseGame = new Dictionary<GameVariant.BaseGame, int>()
        {
            {GameVariant.BaseGame.Slayer, 0},           //[0]"Slayer",
            {GameVariant.BaseGame.Oddball, 1},          //[1]"Oddball",
            {GameVariant.BaseGame.CaptureTheFlag, 2},   //[2]"Capture The Flag",
            {GameVariant.BaseGame.Assault, 3},          //[3]"Assault",
            {GameVariant.BaseGame.Infection, 4},        //[4]"Infection",
            {GameVariant.BaseGame.KingOfTheHill, 5},    //[5]"King Of The Hill",
            {GameVariant.BaseGame.Juggernaut, 6},       //[6]"Juggernaut",
            {GameVariant.BaseGame.VIP, 7},              //[7]"VIP",
        };
        private static string[] contextMaps = new string[] { "Diamondback", "Edge", "Guardian", "High Ground", "Icebox", "Last Resort", "Narrows", "Reactor", "Sandtrap", "Standoff", "The Pit", "Valhalla" };
        private static readonly Dictionary<MapVariant.BaseMap, int> mapContextMenuIndicesByBaseMap = new Dictionary<MapVariant.BaseMap, int>()
        {
            {MapVariant.BaseMap.Diamondback, 00 },  //mapsCM.Items[00] = "Diamondback" /*"s3d_avalanche"*/
            {MapVariant.BaseMap.Edge,        01 },  //mapsCM.Items[01] = "Edge"        /*"s3d_edge"*/
            {MapVariant.BaseMap.Guardian,    02 },  //mapsCM.Items[02] = "Guardian"    /*"guardian"*/
            {MapVariant.BaseMap.HighGround,  03 },  //mapsCM.Items[03] = "High Ground" /*"deadlock"*/
            {MapVariant.BaseMap.Icebox,      04 },  //mapsCM.Items[04] = "Icebox"      /*"s3d_turf"*/
            {MapVariant.BaseMap.LastResort,  05 },  //mapsCM.Items[05] = "Last Resort" /*"zanzibar"*/
            {MapVariant.BaseMap.Narrows,     06 },  //mapsCM.Items[06] = "Narrows"     /*"chill"*/
            {MapVariant.BaseMap.Reactor,     07 },  //mapsCM.Items[07] = "Reactor"     /*"s3d_reactor"*/
            {MapVariant.BaseMap.Sandtrap,    08 },  //mapsCM.Items[08] = "Sandtrap"    /*"shrine"*/
            {MapVariant.BaseMap.Standoff,    09 },  //mapsCM.Items[09] = "Standoff"    /*"bunkerworld"*/
            {MapVariant.BaseMap.ThePit,      10 },  //mapsCM.Items[10] = "The Pit"     /*"cyberdyne"*/
            {MapVariant.BaseMap.Valhalla,    11 },  //mapsCM.Items[11] = "Valhalla"    /*"riverworld"*/
        };
                

        private void toolStripDropDownButtonGame_DropDownOpening(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(currentConnection?.State?.variant)) {

                //[0]"Slayer",
                //[1]"Oddball",
                //[2]"Capture The Flag",
                //[3]"Assault",
                //[4]"Infection",
                //[5]"King Of The Hill",
                //[6]"Juggernaut",
                //[7]"VIP",

                string variantName = currentConnection.State.variant ?? "";

                if (CurrentConnectionHooked) {
                    variantName = currentConnection.GetCurrentGameType()?.Name ?? "";
                }

                // Remove all custom variants from the dropdowns and reset color and check states
                // If a built-in variant is active, set the color and check state
                for (int i = 0; i < builtInVariantsPerBaseGame.Length; i++) {

                    variantsCM.Items[i].BackColor = SystemColors.Control;

                    for (int j = variantsCM.Items[i].DropDownItems().Count - 1; j >= builtInVariantsPerBaseGame[i]; j--) {
                        variantsCM.Items[i].DropDownItems().RemoveAt(j);
                    }

                    foreach (ToolStripItem item in variantsCM.Items[i].DropDownItems()) {
                        if (variantName == item.Text) {
                            variantsCM.Items[i].BackColor = SystemColors.GradientActiveCaption;
                            item.BackColor = SystemColors.GradientActiveCaption;
                            item.Checked(true);
                            break;
                        }
                        else {
                            item.BackColor = SystemColors.Control;
                            item.Checked(false);
                        }
                    }

                }

                // Go through map variants, add each one as a drop down menu item for their corresponding base map
                // If that particular map variant is currently loaded, set its check state to true, and update the background color for its base map's context menu item
                int ind = -1; ToolStripItem currentItem;
                foreach (GameVariant variant in currentConnection.GameVariants) {
                    
                    if (variant == null || 
                        variant.BaseGameID == GameVariant.BaseGame.Unknown || 
                        variant.BaseGameID == GameVariant.BaseGame.All || 
                        string.IsNullOrWhiteSpace(variant.Name)
                    ) 
                    { continue; }

                    ind = gametypeContextMenuIndicesByBaseGame[variant.BaseGameID];

                    currentItem = variantsCM.Items[ind].DropDownItems().Add(
                        variant.Name, null, (s, ev) => {
                            ContextLoadMap($"Game.GameType \"{variant.TypeNameForVotingFile}\"", s);
                            toolStripDropDownButtonGame.ToolTipText = variant.Description;
                        }
                    );
                    
                    currentItem.ToolTipText = variant.Description;

                    if (variant.Name == variantName) {
                        variantsCM.Items[ind].BackColor = SystemColors.GradientActiveCaption;
                        variantsCM.Items[ind].Checked(false);
                        currentItem.BackColor = SystemColors.GradientActiveCaption;
                        currentItem.Checked(true); 
                    }

                }

            }

        }

        private void ContextLoadGame(string loadGameCommand, object sender)
        {
            variantsCM.Hide();

            if (!(currentConnection?.InLobby ?? false)) {
                DialogResult confirmLoadMap = MessageBox.Show(
                    $"Are you sure you want to load '{loadGameCommand.TrimStart("Game.GameType ".Length).RemoveAll('"')}' while a game is in progress?\n",
                    "Confirm GameType Load", MessageBoxButtons.OKCancel
                );
                if (confirmLoadMap != DialogResult.OK) { return; }
            }

            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    loadGameCommand,
                    loadGameCommand,
                    "GameType Menu"
                )
            );
            foreach (ToolStripMenuItem item in variantsCM.Items) {
                item.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;

            // This will help the gametype label to update more consistently after loading a new gametype
            currentConnection?.QueueLiveGameVariantUpdate(2000);

        }

        private void toolStripDropDownButtonMap_DropDownOpening(object sender, EventArgs e)
        {
            UpdateMapsContextMenu();
        }

        private void pictureBoxMapAndStatusOverlay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !string.IsNullOrWhiteSpace(currentConnection?.State?.mapFile)) {

                UpdateMapsContextMenu();

                // Show the context menu, adjust positioning so that the bottom-left corner of the menu will be at the click location
                Point showLocation = pictureBoxMapAndStatusOverlay.PointToScreen(e.Location);
                this.mapsCM.Show(new Point(showLocation.X, showLocation.Y - mapsCM.Height));

            }
        }

        private void UpdateMapsContextMenu()
		{
            
            if (!string.IsNullOrWhiteSpace(currentConnection?.State?.mapFile)) {

                //mapsCM.Items[00] = "Diamondback" /*"s3d_avalanche"*/
                //mapsCM.Items[01] = "Edge"        /*"s3d_edge"*/
                //mapsCM.Items[02] = "Guardian"    /*"guardian"*/
                //mapsCM.Items[03] = "High Ground" /*"deadlock"*/
                //mapsCM.Items[04] = "Icebox"      /*"s3d_turf"*/
                //mapsCM.Items[05] = "Last Resort" /*"zanzibar"*/
                //mapsCM.Items[06] = "Narrows"     /*"chill"*/
                //mapsCM.Items[07] = "Reactor"     /*"s3d_reactor"*/
                //mapsCM.Items[08] = "Sandtrap"    /*"shrine"*/
                //mapsCM.Items[09] = "Standoff"    /*"bunkerworld"*/
                //mapsCM.Items[10] = "The Pit"     /*"cyberdyne"*/
                //mapsCM.Items[11] = "Valhalla"    /*"riverworld"*/

                string mapName = currentConnection.State.map ?? "";

                // Set Base Maps colors and check state
                for (int i = 0; i < 12; i++) {
                    mapsCM.Items[i].DropDownItems().Clear();
                    mapsCM.Items[i].Checked(false);
                    mapsCM.Items[i].BackColor = SystemColors.Control;
                    if (mapName == contextMaps[i]) {
                        mapsCM.Items[i].Checked(true);
                        mapsCM.Items[i].BackColor = SystemColors.HotTrack;
                    }
                }

                // Go through map variants, add each one as a drop down menu item for their corresponding base map
                // If that particular map variant is currently loaded, set its check state to true, and update the background color for its base map's context menu item
                int ind = -1; ToolStripItem currentItem;
                foreach (MapVariant map in currentConnection.MapVariants) {
                    
                    if (map == null || 
                        string.IsNullOrWhiteSpace(map.Name) || 
                        map.BaseMapID == MapVariant.BaseMap.All ||
                        map.BaseMapID == MapVariant.BaseMap.Unknown
                    ) 
                    { continue; }

                    ind = mapContextMenuIndicesByBaseMap[map.BaseMapID];

                    currentItem = mapsCM.Items[ind].DropDownItems().Add(
                        map.Name, null, (s, ev) => {
                            ContextLoadMap($"Game.Map \"{map.Name}\"", s);
                            toolStripDropDownButtonMap.ToolTipText = map.Description;
                        }
                    );
                    currentItem.ToolTipText = map.Description;

                    if (map.Name == mapName) {
                        mapsCM.Items[ind].BackColor = SystemColors.GradientActiveCaption;
                        mapsCM.Items[ind].Checked(false);
                        currentItem.BackColor = SystemColors.GradientActiveCaption;
                        currentItem.Checked(true);
                    }

                }

            }
        }

        private void ContextLoadMap(string loadMapCommand, object sender)
        {

            mapsCM.Hide();

            if (!(currentConnection?.InLobby ?? false)) {
                DialogResult confirmLoadMap = MessageBox.Show(
                    $"Are you sure you want to load '{loadMapCommand.TrimStart("Game.Map ".Length).RemoveAll('"')}' while a game is in progress?\n",
                    "Confirm Map Load", MessageBoxButtons.OKCancel
                );
                if (confirmLoadMap != DialogResult.OK) { return; }
            }

            currentConnection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    loadMapCommand,
                    loadMapCommand,
                    "Map Menu"
                )
            );
            foreach (ToolStripMenuItem item in mapsCM.Items) {
                item.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;

            // This will help the gametype label to update more consistently after loading a new gametype
            currentConnection?.QueueLiveGameVariantUpdate(2000);

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
                    currentConnection.RconCommandQueue.Enqueue(
                        RconCommand.ConsoleLogCommand(
                            $"Server.KickUid {contextPlayer.Uid}",
                            $"Kicking: {contextPlayer.Name} / {contextPlayer.Uid}",
                            "Context Menu Item"
                        )
                    );
                    ExternalLog(currentConnection.Settings, "Server.KickUid " + contextPlayer.Uid + " - " + contextPlayer.Name);
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
                    currentConnection.RconCommandQueue.Enqueue(                        
                        RconCommand.ConsoleLogCommand(
                            $"Server.KickBanUid {contextPlayer.Uid}",
                            $"Permanently Banning: {contextPlayer.Name} / {contextPlayer.Uid}",
                            "Context Menu Item"
                        )
                    );
                    ExternalLog(currentConnection.Settings, "Server.KickBanUid " + contextPlayer.Uid + " - " + contextPlayer.Name);
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
                    
                    currentConnection.RconCommandQueue.Enqueue(
                        RconCommand.ConsoleLogCommand(
                            $"Server.KickTempBanUid {contextPlayer.Uid}",
                            $"Temporarily Banning: {contextPlayer.Name} / {contextPlayer.Uid}",
                            "Context Menu Item"
                        )
                    );
                    ExternalLog(currentConnection.Settings, "Server.KickTempBanUid " + contextPlayer.Uid + " - " + contextPlayer.Name);
                }
            }
        }

        private void AuthorizePlayerAsAdmin(object sender, System.EventArgs e)
		{
            if (currentConnection.RconConnected && contextPlayer.IsValid) {
                var confirmResult = MessageBox.Show("Are you sure you want to authorize the following player as an ADMINISTRATOR?\nThey will be able to run arbitrary commands on the server.\nPlayer: " + contextPlayer.Name + ":" + contextPlayer.Uid + ".", "Confirm Administrator Authorization", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes) {
                    if (currentConnection.Settings?.AuthorizedUIDs == null) {
                        App.Log($"Failed to authorize {contextPlayer.ScoreboardName} (UID:{contextPlayer.Uid}).\nThere was an error accessing the settings file or AuthorizedUIDs List.");
                        return;
                    }
                    if (!currentConnection.Settings.AuthorizedUIDs.Contains(contextPlayer.Uid)) {
                        currentConnection.Settings.AuthorizedUIDs.Add(contextPlayer.Uid);
                        currentConnection.SaveSettings();
                    }                    
                    currentConnection.PrintToConsole("Successfully authorized player as admin: " + contextPlayer.Name + "/" + contextPlayer.Uid + ".");
                    ExternalLog(currentConnection.Settings, "Server.AdminAuthorization " + contextPlayer.Uid + " - " + contextPlayer.Name);
                    currentConnection.Whisper(contextPlayer.Name, new List<string>() { "ADMIN AUTHORIZATION GRANTED", "ADMIN commands unlocked", "\"!commands\" to view commands" });
                }
                else {
                    currentConnection.PrintToConsole($"Cancelled admin authorization of player {contextPlayer.ScoreboardName}.");
                }
            }
            else {
                App.Log(
                    "AUTHORIZATION COMMAND FAILED: " + 
                    (currentConnection.RconConnected 
                        ? "Invalid Context Player." 
                        : "RCON not connected.")
                );
			}
        }

        private void RemovePlayerAdminPrivileges(object sender, System.EventArgs e)
        {
            if (currentConnection.RconConnected && contextPlayer.IsValid) {
                if ((currentConnection.Settings?.AuthorizedUIDs?.Contains(contextPlayer.Uid) ?? false)) {
                    var confirmResult = MessageBox.Show("Are you sure you want to REMOVE the following player's administrator privileges?\nThey will no longer be able to run arbitrary commands on the server.\nPlayer: " + contextPlayer.Name + ":" + contextPlayer.Uid + ".", "Confirm Administrator Deauthorization", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes) {
                        currentConnection.Settings.AuthorizedUIDs.Remove(contextPlayer.Uid);
                        currentConnection.SaveSettings();
                        currentConnection.PrintToConsole("Successfully removed player admin privileges: " + contextPlayer.Name + "/" + contextPlayer.Uid + ".");
                        ExternalLog(currentConnection.Settings, "Server.AdminDeauthorization " + contextPlayer.Uid + " - " + contextPlayer.Name);
                        currentConnection.Whisper(contextPlayer.Name, new List<string>() { "ADMIN AUTHORIZATION REVOKED", "ADMIN commands now locked", "\"!commands\" to view commands" });
                    }
                    else {
                        currentConnection.PrintToConsole($"CANCELLED Deauthorization of player {contextPlayer.ScoreboardName}. They will RETAIN their administrative privileges.");
                    }
                }
                else {
                    currentConnection.PrintToConsole($"{contextPlayer.ScoreboardName} does not have administrative privileges.");
                }
            }
            else {
                App.Log(
                    "DEAUTHORIZATION COMMAND FAILED: " +
                    (currentConnection.RconConnected
                        ? "Invalid Context Player."
                        : "RCON not connected.")
                );
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
                textBoxAutoScrollConsoleText.Text = currentConnection.GetConsole();
                textBoxAutoScrollChatText.Text = currentConnection.GetChat();
                textBoxAutoScrollPlayerLog.Text = currentConnection.GetPlayerLog();
                textBoxAutoScrollApplicationLog.Text = AppLogText;
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

        private void toolStripSplitButtonAutoUpdate_Click(object sender, EventArgs e)
        {
            toolStripSplitButtonAutoUpdate.ShowDropDown();
        }

        private void toolStripSplitButtonAutoUpdate_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.Contains("Enabled"))
            {
                toolStripSplitButtonAutoUpdate.Image = Resources.Image_CheckMark32x32;
                toolStripMenuItemAutoUpdateEnabled.Checked = true;
                toolStripMenuItemAutoUpdateEnabled.CheckState = CheckState.Checked;
                toolStripMenuItemAutoUpdateDisabled.Checked = false;
                toolStripMenuItemAutoUpdateDisabled.CheckState = CheckState.Unchecked;
                textBoxAutoScrollConsoleText.Text = currentConnection.GetConsole();
                textBoxAutoScrollChatText.Text = currentConnection.GetChat();
                textBoxAutoScrollPlayerLog.Text = currentConnection.GetPlayerLog();
            }
            else
            {
                toolStripSplitButtonAutoUpdate.Image = Resources.Image_XMark32x32;
                toolStripMenuItemAutoUpdateEnabled.Checked = false;
                toolStripMenuItemAutoUpdateEnabled.CheckState = CheckState.Unchecked;
                toolStripMenuItemAutoUpdateDisabled.Checked = true;
                toolStripMenuItemAutoUpdateDisabled.CheckState = CheckState.Checked;
            }
        }

        private void toolStripResizeToFitScoreboard_ButtonClick(object sender, EventArgs e)
        {
            ResizeToFitScoreboard();
        }

        private void toolStripFontSizeIncrease_ButtonClick(object sender, EventArgs e)
        {
            Scoreboard.FontSize.Value += 1;
            ResizeToFitScoreboard();
        }

        private void toolStripFontSizeDecrease_ButtonClick(object sender, EventArgs e)
        {
            if (Scoreboard.FontSize.Value < 4) { return; }
            Scoreboard.FontSize.Value -= 1;
            ResizeToFitScoreboard();
        }

        private void toolStripSplitButtonServerHook_Click(object sender, EventArgs e)
        {
            new ServerHookForm(currentConnection).ShowDialog();
            // Attempt ServerHook
            //if (CurrentConnectionHooked) { return; }
            //else { currentConnection.AttemptServerHook(); }
        }

        #endregion

        #region Above-Log Buttons

        // Move the above-log buttons relative to the tabcontrol
        private void UpdateAboveLogButtonPositions()
        {
            buttonScrollLock.Location = new Point(
                tabControlServerInterfaces.Location.X + tabControlServerInterfaces.Width - buttonScrollLock.Width - 5,
                tabControlServerInterfaces.Location.Y + 4
            );
            buttonClearLog.Location = new Point(
                tabControlServerInterfaces.Location.X + tabControlServerInterfaces.Width - buttonScrollLock.Width - buttonClearLog.Width - 5,
                tabControlServerInterfaces.Location.Y + 4
            );
            //buttonScrollLock.Invalidate();
        }
        private void tabControlServerInterfaces_LocationChanged(object sender, EventArgs e)
        {
            UpdateAboveLogButtonPositions();
        }
        private void tabControlServerInterfaces_SizeChanged(object sender, EventArgs e)
        {
            UpdateAboveLogButtonPositions();
        }

        // buttonClearLog behaviors
        private void tabControlServerInterfaces_Selected(object sender, TabControlEventArgs e)
        {
            
            // get tab index, validate range
			int index = tabControlServerInterfaces?.SelectedIndex ?? -1;
            if (index < 0 || index > 3) { return; }

            // assign log name
            string logName = "";
            switch (index) {
                case 0: logName = "Console "; break;
                case 1: logName = "Chat "; break;
                case 2: logName = "Player "; break;
                case 3: logName = "Application "; break;
                default: break;
            }

            //Update buttonClearLog tooltip
            toolTip1.SetToolTip(buttonClearLog, $"Click to clear {logName}Log text.");

		}
		private void buttonClearLog_Click(object sender, EventArgs e)
        {
            // get tab index, validate range
            int index = tabControlServerInterfaces?.SelectedIndex ?? -1;
            if (index < 0 || index > 3) { return; }

            // assign log name
            string logName = "";
            switch (index) {
                case 0: logName = "Console "; break;
                case 1: logName = "Chat "; break;
                case 2: logName = "Player "; break;
                case 3: logName = "Application "; break;
                default: break;
            }

            // confirm clearing log
            DialogResult confirmation = MessageBox.Show(
                $"Are you sure you want to clear the {logName}Log text?",
                $"Clear {logName}Log Text?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.None
            );

            // clear log
            if (confirmation == DialogResult.Yes && index > -1 && index < 4) {
                ClearLog(index);
			}
		}
        private void ClearLog(int tabIndex)
		{
            if (tabIndex == 3) {
                AppLogText = "";
                textBoxAutoScrollApplicationLog.Text = "";
            }
            else if (currentConnection != null) {
                if (tabIndex == 0) {
                        currentConnection.ClearConsole();
                        textBoxAutoScrollConsoleText.Text = "";
                }
                else if (tabIndex == 1) {
                        currentConnection.ClearChat();
                        textBoxAutoScrollChatText.Text = "";
                }
                else if (tabIndex == 2) {
                        currentConnection.ClearPlayerLog();
                        textBoxAutoScrollPlayerLog.Text = "";
                }
            }
        }


        #endregion

        #region Server Name Display / Edit Box

        /// <summary>
        /// Indicates that the Server Name Text Box is currently in "edit mode view" (looks like a text box)
        /// </summary>
        private bool textBoxServerNameEdit_editModeView = false;
        /// <summary>
        /// Indicates that the Server Name Text Box currently has focus and is being edited
        /// </summary>
		private bool textBoxServerNameEdit_editing = false;

        // If Server Name edit box is clicked, Set editing to true and activate Edit View Mode if needed
        private void textBoxServerNameEdit_Click(object sender, EventArgs e)
        {
            if (!textBoxServerNameEdit_editModeView) {                
                textBoxServerNameEdit.ReadOnly = false;
                textBoxServerNameEdit.BackColor = SystemColors.Window;
                textBoxServerNameEdit_editModeView = true;
            }
            textBoxServerNameEdit_editing = true;
        }

		// If focus leaves the Server Name edit box, editing is set to false
		private void textBoxServerNameEdit_Leave(object sender, EventArgs e)
        {
            textBoxServerNameEdit_editing = false;
        }

        // Enter key submits name change, escape key cancels
        private void textBoxServerNameEdit_KeyDown(object sender, KeyEventArgs e)
		{

            // ENTER KEY
            if (e.KeyCode == Keys.Enter) {

                // Handle and Suppress key event
                e.Handled = true;
                e.SuppressKeyPress = true;

                // if name is unchanged or invalid, reset text to current server name
                if (String.IsNullOrWhiteSpace(textBoxServerNameEdit.Text) || textBoxServerNameEdit.Text == (currentConnection?.State?.name ?? "Server Name Error")) {
                    textBoxServerNameEdit.Text = currentConnection?.State?.name ?? "Server Name Error";
                }

                // get confirmation and then submit the Server Name change command if confirmed
                else {
                    if (MessageBox.Show($"Are you sure you want to change the server name to:\n{textBoxServerNameEdit.Text}?",
                        "Confirm Server Name Change", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                        currentConnection.RconCommandQueue.Enqueue(
                            RconCommand.ConsoleLogCommand(
                                $"Server.Name \"{textBoxServerNameEdit.Text}\"",
                                $"Server.Name \"{textBoxServerNameEdit.Text}\"",
                                "Server Name Update"
                            )
                        );
                    }
                }

                // either way, remove focus, and change back to label-like display
                this.ActiveControl = null;
                textBoxServerNameEdit_editing = false;
                if (textBoxServerNameEdit_editModeView) {
                    textBoxServerNameEdit.ReadOnly = true;
                    textBoxServerNameEdit.BackColor = SystemColors.Control;
                    textBoxServerNameEdit_editModeView = false;
                }

            }

            // ESCAPE KEY
            else if (e.KeyCode == Keys.Escape) {

                // Handle and Suppress key event
                e.Handled = true;
                e.SuppressKeyPress = true;

                // reset text to current server name
                textBoxServerNameEdit.Text = currentConnection?.State?.name ?? "Server Name Error";

                // remove focus, and change back to label-like display
                this.ActiveControl = null;
                textBoxServerNameEdit_editing = false;
                if (textBoxServerNameEdit_editModeView) {
                    textBoxServerNameEdit.ReadOnly = true;
                    textBoxServerNameEdit.BackColor = SystemColors.Control;
                    textBoxServerNameEdit_editModeView = false;
                }

            }

        }

        #endregion

    }

}
