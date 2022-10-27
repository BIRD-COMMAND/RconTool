#define DEBUG

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace RconTool
{

	public class Connection
    {

        #region Properties and Variables

        public ServerState State { get; set; } = new ServerState();

        public ServerSettings Settings { get { return settings?.Value ?? null; } }
        private SavedSetting<ServerSettings> settings;

        public string ConnectionName { 
            get { return Settings?.DisplayName ?? "UNKNOWN"; }
        }
        public string LogPrefix { get { return $"{DateTime.Now.ToUniversalTime().ToFastLogString()} ({ConnectionName})"; } }

        public SavedRecord<string> ConsoleMessages { get; set; }
        private string consoleText = "";
        public SavedRecord<Message> ChatMessages { get; set; }
        private string chatText = "";
        public SavedRecord<string> PlayerLogMessages { get; set; }
        private string playerLogText = "";

        

        public bool IsCyclingConsoleHistory { get; set; } = false;
        public Stack<string> ConsoleHistory { get; set; } = new Stack<string>();
        public int ConsoleHistoryCurrentIndex { get; set; } = -1;

        public bool UpdateDisplay { get; set; } = true;

        public const string StatusStringInGame = "InGame";
        public const string StatusStringInLobby = "InLobby";
        public const string StatusStringLoading = "Loading";
        public const string DatabaseServerMessageSeparator = "·";

        public int PopulatedTeams { get; set; } = 1;
        public string PopulatedTeamsString { get { return PopulatedTeams.ToString(); } }

        public bool InLobby {
            get { return (State?.Status ?? "") == StatusStringInLobby; }
		}
        public bool InGame {
            get { return (State?.Status ?? "") == StatusStringInGame; }
        }

        public const int ServerMessageDelay = 25;

        public string ServerStateJson { get; set; } = "";

        public bool ServerStatusAvailable { 
            get { return !string.IsNullOrWhiteSpace(ServerStateJson); }
        }

        public bool IsDisplayedCurrently { get { return App.currentConnection == this; } }

        public bool HasPlayers { get; private set; }

        /// <summary>
        /// The MatchQueue used when the Settings.ServerMatchMode is set to MatchMode.Queue.
        /// <br>With MatchMode.Queue, when a match ends and returns to lobby, the next match in the MatchQueue will be loaded.</br>
        /// </summary>
        public System.Collections.Concurrent.ConcurrentQueue<MatchInfo> MatchQueue { get; set; } = new System.Collections.Concurrent.ConcurrentQueue<MatchInfo>();
        public string NextQueuedMatchString { 
            get {
                if (MatchQueue.IsEmpty) { return "No matches queued."; }
                else {
                    MatchQueue.TryPeek(out MatchInfo next);
                    return (next?.GameVariant?.Name + " on " + next?.MapVariant?.Name) ?? "Unknown Match";
                }
            }
        }

        /// <summary>
        /// Dictionary of <b>Player UIDs</b> and the <b>Name</b> of the player their Private Message Replies (!r command) will be sent to.
        /// </summary>
        public Dictionary<string, string> ReplyCommandPlayers = new Dictionary<string, string>();

        public List<MapVariant> MapVariants {
            get {
                if (Settings.UseLocalFiles == false)
                {
                    if (mapVariants.Count > 0) { mapVariants.Clear(); }
                }
                else if (ShouldQueryMapVariants) { LoadMapVariants(); }
                return mapVariants;
            }
        }
        private List<MapVariant> mapVariants = new List<MapVariant>();
        public List<string> MapVariantNames {
            get {
                if (Settings.UseLocalFiles == false)
                {
                    if (mapVariantNames.Count > 0) { mapVariantNames.Clear(); }
                }
                else if (ShouldQueryMapVariants) { LoadMapVariants(); }
                return mapVariantNames;
            }
        }
        private List<string> mapVariantNames = new List<string>();
        private System.Nullable<DateTime> lastMapVariantLoadTime = null;
        private bool ShouldQueryMapVariants {
            get {
                return lastMapVariantLoadTime == null 
                    || lastMapVariantLoadTime.HasValue == false 
                    || DateTime.Now - lastMapVariantLoadTime.Value > TimeSpan.FromMinutes(5);
            }
        }

        public List<GameVariant> GameVariants {
            get {
                if (Settings.UseLocalFiles == false)
                {
                    if (gameVariants.Count > 0) { gameVariants.Clear(); }
                }
                else if (ShouldQueryGameVariants) { LoadGameVariants(); }
                return gameVariants;
            }
        }
        private List<GameVariant> gameVariants = new List<GameVariant>();
        public List<string> GameVariantNames {
            get {
                if (Settings.UseLocalFiles == false)
                {
                    if (gameVariantNames.Count > 0) { gameVariantNames.Clear(); }
                }
                else if (ShouldQueryGameVariants) { LoadGameVariants(); }
                return gameVariantNames;
            }
        }
        private List<string> gameVariantNames = new List<string>();
        private System.Nullable<DateTime> lastGameVariantLoadTime = null;
        private bool ShouldQueryGameVariants { 
            get {
                return lastGameVariantLoadTime == null
                    || lastGameVariantLoadTime.HasValue == false
                    || DateTime.Now - lastGameVariantLoadTime.Value > TimeSpan.FromMinutes(5);
            }
        }

        /// <summary>
        /// If the ServerHook is enabled, returns a GameVariant constructed by reading application memory. Returns null if ServerHook is disabled or if the operation fails.
        /// </summary>
        public GameVariant LiveGameVariant { 
            get {
                if (ServerHookEnabled) {
                    if (DateTime.Now - lastLiveGameVariantQuery > TimeSpan.FromSeconds(15)) {
                        liveGameVariant = GetCurrentGameType();
                        lastLiveGameVariantQuery = DateTime.Now;
                    }
				}
                else {
                    liveGameVariant = null;
				}
                return liveGameVariant;
            }
        }
        private GameVariant liveGameVariant;
        private DateTime lastLiveGameVariantQuery = DateTime.Now - TimeSpan.FromSeconds(30);
        public void QueueLiveGameVariantUpdate(int millisecondsFromNow)
		{
            lastLiveGameVariantQuery = 
                DateTime.Now 
                - TimeSpan.FromSeconds(15) 
                + TimeSpan.FromMilliseconds(millisecondsFromNow
            );
		}

        public List<PlayerStatsRecord> LastMatchResults = new List<PlayerStatsRecord>();

        public void UpdateServerHook_PlayerTeamsByUid() { if (ServerHookEnabled) { GetPlayerTeams(); } }

        public DateTime TimeOfLastEmblemRequest { get; set; }
        public bool EmblemsNeeded { get; set; } = false;

        private Dictionary<string, int> serverHookPlayerTeamIndices = new Dictionary<string, int>() { {"null", -1} };

        private PlayerJoinLeaveEventArgs LastPlayerJoinEventArgs { get; set; } = null;
        public Dictionary<int, RankEmblemData> RankAndEmblemData { get; set; } = new Dictionary<int, RankEmblemData>();

        public System.Collections.Concurrent.ConcurrentQueue<RconCommand> RconCommandQueue { get; set; } = new System.Collections.Concurrent.ConcurrentQueue<RconCommand>();
        public Thread RconCommandQueueThread;
        public Thread RunTimedCommandsThread;
        public Thread MonitorServerStatusThread;
        public Thread ManageRanksAndEmblemsThread;

        #region Interface

        public bool ConsoleAutoScroll { get; set; } = true;
        public bool ChatAutoScroll { get; set; } = true;
        public bool PlayerLogAutoScroll { get; set; } = true;
        public bool ApplicationLogAutScroll { get; set; } = true;

        #endregion

        #region Rcon Variables

        /// <summary>
        /// The thread which manages the Rcon connection.
        /// </summary>
        public Thread RconConnectionThread;
        /// <summary>
        /// The WebSocket which facilitates the Rcon connection.
        /// </summary>
        public WebSocket RconWebSocket;
        public Mutex RconWebSocketMutex = new Mutex();
        
        /// <summary>
        /// Determines whether an Rcon connection to the current server will be created and maintained.
        /// </summary>
        public bool RconEnabled { get; set; } = true;
        /// <summary>
        /// Indicates whether the current RconWebSocket connection is active (connected).
        /// </summary>
        public bool RconConnected { get {
                return RconWebSocket?.IsAlive ?? false;
                //RconWebSocketMutex.WaitOne();
                //bool isAlive = RconWebSocket?.IsAlive ?? false;
                //RconWebSocketMutex.ReleaseMutex();
                //return isAlive;
            } 
        }

        /// <summary>
        /// Controls whether the Rcon connection will automatically attempt to reconnect periodically if the connection is interrupted for any reason.
        /// </summary>
        public bool RconAutoReconnectEnabled { get; set; } = true;
        /// <summary>
        /// This is the number of seconds until the next automatic reconnect attempt will be made if Rcon is disconnected and <see cref="RconAutoReconnectEnabled"/> is enabled.
        /// </summary>
        public int RconAutoAttemptReconnectInterval { get; set; } = 10;
        /// <summary>
        /// A flag which tells the Rcon WebSocket Manager to attempt a connection as soon as possible. Resets after firing.
        /// </summary>
        public bool RconReconnect { get; set; } = true;
        /// <summary>
        /// How often, in minutes, an Rcon Connection Error will be logged to the Network/Debug Log.
        /// </summary>
        public int RconDisconnectedLogInterval { get; set; } = 60;
        /// <summary>
        /// Tracks Rcon connection failure notification times so the log isn't spammed with notifications.
        /// </summary>
        private DateTime lastRconDisconnectedLogTime = (DateTime.Now - TimeSpan.FromMinutes(60));

		#endregion

        #region Vote Properties
                
        public bool VoteInProgress { get; set; } = false;
        public int VoteDuration { get; set; } = 45;
        public int VoteStatusUpdates { get; set; } = 2;
        public int VotesRequiredToPass { get; set; } = 0;                
        public bool DynamicVoteFileInUse { get; set; } = false;
        public bool NextGameAndMapLockedIn { get; set; } = false; 
        public string OriginalVotingFilePath { get; set; } = "";
        public string OriginalMapVotingTime { get; set; } = "12";
        public string OriginalNumberOfVotingOptions { get; set; } = "4";
        public string OriginalNumberOfRevotesAllowed { get; set; } = "3";
        public string OriginalVotingDuplicationLevel { get; set; } = "1";
        public string OriginalVotingJsonFileName { get; set; } = "voting.json";

        public System.Collections.Concurrent.ConcurrentDictionary<string, bool> UidVotePairs { get; set; } 
         = new System.Collections.Concurrent.ConcurrentDictionary<string, bool>(2, 32);

        #endregion

        #endregion

        /// <summary>
        /// Creates a new connection.
        /// <para>If the settings parameter is null, the ServerSettings will be loaded from the database with the provided connection string.</para>
        /// <para>If a ServerSettings object is passed, the database will be created or updated with the provided ServerSettings.</para>
        /// </summary>
        /// <param name="serverDatabaseConnectionString"></param>
        /// <param name="settings"></param>
        public Connection(string connectionId, ServerSettings settings = null)
        {

            // Validate connectionId
            if (string.IsNullOrWhiteSpace(connectionId)) { 
                throw new ArgumentException("Invalid connectionId - must be unique, non-null, and non-whitespace."); 
            }

            // Add/Save new Connection Id if needed
            if (!App.ConnectionIdsList.Value.Contains(connectionId)) { 
                App.ConnectionIdsList.Value.Add(connectionId);
                App.ConnectionIdsList.Save();
            }

            this.settings = new SavedSetting<ServerSettings>($"{connectionId}_Settings", null);
            if (settings != null) { this.settings.Value = settings; this.settings.Save(); }

            // Validate Settings
            if (Settings == null) { 
                throw new Exception("Failed to initialize ServerSettings for Connection."); 
            }

			Settings.LoadAllVoteFilesFromDisk();

            TimeOfLastEmblemRequest = DateTime.Now;
            
            if (!App.connectionList.Contains(this)) { App.connectionList.Add(this); }

            PlayerChangedTeams += OnPlayerTeamChanged;
            ChatMessageReceived += OnMessageRuntimeCommands;
            MatchBeganOrEnded += OnMatchBeginOrEnd;

            if (Settings.AuthorizedUIDs == null) { Settings.AuthorizedUIDs = new List<string>(); }

            // load saved chat messages
            try {
                ChatMessages = new SavedRecord<Message>($"{connectionId}_Messages", null);
                foreach (Message item in ChatMessages) { PrintToChat(item); }
            }
            catch (Exception e) { AppLog($"Failed to load or initialize saved Chat Message list: {e}"); }

            // load saved console messages
            try {
                ConsoleMessages = new SavedRecord<string>($"{connectionId}_ConsoleMessages", null);
                foreach (string item in ConsoleMessages) { PrintToConsole(item, false, false); }
            }
            catch (Exception e) { AppLog($"Failed to load or initialize saved Console Messages list: {e}"); }

            // load saved Player Log messages
            try {
                PlayerLogMessages = new SavedRecord<string>($"{connectionId}_PlayerLogMessages", null);
                foreach (string item in PlayerLogMessages) { PrintToPlayerLog(item, false, false); }
            }
            catch (Exception e) { AppLog($"Failed to load or initialize saved Player Log Messages list: {e}"); }

            PrintToConsole("Connecting to server...");


            RconConnectionThread = new Thread(new ThreadStart(ManageRconConnection)) { IsBackground = true };
            if (RconConnectionThread.Name == null) {
                RconConnectionThread.Name = "RconConnectionThread for " + Settings.DisplayName;
            }
            RconConnectionThread.Start();

            MonitorServerStatusThread = new Thread(new ThreadStart(MonitorServerStatus)) { IsBackground = true };
            if (MonitorServerStatusThread.Name == null) {
                MonitorServerStatusThread.Name = "MonitorServerStatusThread for " + Settings.DisplayName;
            }
            MonitorServerStatusThread.Start();

            RunTimedCommandsThread = new Thread(new ThreadStart(RunTimedCommands)) { IsBackground = true };
            if (RunTimedCommandsThread.Name == null) {
                RunTimedCommandsThread.Name = "RunTimedCommandsThread for " + Settings.DisplayName;
            }
            RunTimedCommandsThread.Start();

            RconCommandQueueThread = new Thread(new ThreadStart(ManageRconCommandQueue)) { IsBackground = true };
            if (RconCommandQueueThread.Name == null) {
                RconCommandQueueThread.Name = "RconCommandQueueThread for " + Settings.DisplayName;
            }
            RconCommandQueueThread.Start();

            //NOTE since halostats is now down, this thread is no longer used
            //ManageRanksAndEmblemsThread = new Thread(new ThreadStart(ManageRanksAndEmblems)) { IsBackground = true };
            //if (ManageRanksAndEmblemsThread.Name == null) {
            //    ManageRanksAndEmblemsThread.Name = "ManageRanksAndEmblemsThread for " + Settings.DisplayName;
            //}
            //ManageRanksAndEmblemsThread.Start();

            UpdateDisplay = true;

        }

        public void Close()
        {
            //try { Database.Dispose(); }
            //catch { Console.WriteLine($"Error disposing database for connection {Settings?.Name ?? "Unknown Connection"}"); }
            if (ServerMemory != null) {
                try { ServerMemory.Dispose(); } 
                catch { }
            }
        }

        public void AppLog(string message, [System.Runtime.CompilerServices.CallerMemberName] string calledFrom = "")
		{

            if (string.IsNullOrWhiteSpace(message)) { return; }

            App.Log($"{LogPrefix}.{calledFrom}", message);

#if DEBUG // Output more detailed info to the console when debugging
            Console.WriteLine($"{LogPrefix}.{calledFrom} {message}");
#endif

        }

        private void ManageRconConnection()
        {

            Thread.Sleep(2000);

            while (true ) {

                while ((RconAutoReconnectEnabled == false && RconReconnect == false) || RconEnabled == false) {
                    Thread.Sleep(250);
                }

                // If RconReconnect && it's been at least RconDisconnectedLogInterval minutes since last Attempting Connection notification, notify about connection attempt
                if (RconReconnect && (DateTime.Now - lastRconDisconnectedLogTime) >= TimeSpan.FromMinutes(RconDisconnectedLogInterval)) { 
                    AppLog($"Attempting RCON Connection for {ConnectionName}"); 
                }

                if (AttemptConnection()) {

                    // Connected - Monitor for any changes
                    while (true ) {

                        // Check for changes once per second
                        Thread.Sleep(1000);

                        // Check should reset
                        if (RconReconnect) { AppLog("Resetting RCON Connection"); break; }

                        // Check connection alive
                        RconWebSocketMutex.WaitOne();
                        bool isAlive = RconWebSocket.IsAlive;
                        RconWebSocketMutex.ReleaseMutex();

                        if (!isAlive) {
                            LogRconConnectionError(true);
                            if (RconAutoReconnectEnabled) {
                                Thread.Sleep(RconAutoAttemptReconnectInterval * 1000);        
                            }
                            break;
                        }

                    }

                }
                else {

                    LogRconConnectionError();
                    if (RconAutoReconnectEnabled) {
                        RconReconnect = true;
                        Thread.Sleep(RconAutoAttemptReconnectInterval * 1000);
                    }
                    else {
                        App.ShowMessageBox(
                            "RCON Connection Failed. RCON Auto-Reconnect is disabled, but you may attempt to " +
                            "restart the RCON connection by selecting Retry Connection from the Menu.",
                            "RCON Connection Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

            }

        }
        /// <summary>
        /// Attempts to establish the RconWebSocket connection. Returns true if connection was established, returns false otherwise.
        /// </summary>
        private bool AttemptConnection()
		{

            // Reset Reconnect Flag because we're attempting to reconnect now
            RconReconnect = false;

            // Handle invalid Rcon Address
            if (!Settings.RconWebSocketAddressIsValid) {
                RconEnabled = false;
                App.ShowMessageBox("An RCON connection was attempted with invalid connection settings. " +
                    "RCON Connection has been disabled. Please verify the Server IP and RCON Port, " +
                    "then re-enable RCON and select Retry Connection from the Menu.", "RCON Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return false;
            }

            // Acquire RconWebSocket Mutex
            RconWebSocketMutex.WaitOne();

            //Dispose RconWebSocket if not null
            if (RconWebSocket != null) {
                try { RconWebSocket.Close(); }
                catch (Exception e) {
                    AppLog($"Error while resetting RCON Websocket connection. RconWebSocket.Close() Error:\n{e}");
                }
            }

            // Create RconWebSocket
            RconWebSocket = new WebSocket(Settings.RconWebSocketAddress, ServerSettings.RconProtocolString);
            RconWebSocket.OnOpen += OnRconWebsocketOpen;
            RconWebSocket.OnMessage += OnRconWebsocketMessage;
            RconWebSocket.OnClose += OnRconWebsocketClose;
            RconWebSocket.OnError += OnRconWebsocketError;

            // Attempt connection
            if (RconWebSocket.ReadyState != WebSocketState.Open) {
                try {
                    RconWebSocket.Connect();
                }
                catch (Exception e) {
                    AppLog($"Error while attempting RCON WebSocket connection:\n{e}");
                    RconWebSocketMutex.ReleaseMutex();
                    return false;
                }
            }
            
            // If ready state isn't open, give it 5 seconds
            if (RconWebSocket.ReadyState != WebSocketState.Open) {
                Thread.Sleep(5000);
            }
            
            // get connection success status (open == success)
            bool connectionSuccessStatus = RconWebSocket.ReadyState == WebSocketState.Open;
            
            // Release Mutex before returning
            RconWebSocketMutex.ReleaseMutex();

            return connectionSuccessStatus;

        }
        public void ResetRconConnection()
        {
            RconReconnect = true;
        }
        private void LogRconConnectionError(bool forceLog = false)
        {

            if (RconEnabled == false) { return; }

            if ((DateTime.Now - lastRconDisconnectedLogTime) >= TimeSpan.FromMinutes(RconDisconnectedLogInterval) || forceLog)
            {
                if (RconAutoReconnectEnabled)
                {
                    AppLog($"RCON Connection Failed | Will automatically attempt to reconnect every {RconAutoAttemptReconnectInterval} seconds.");
                }
                else
                {
                    App.ShowMessageBox(
                        "RCON Connection Failed. RCON Auto-Reconnect is disabled, but you may attempt to " +
                        "restart the RCON connection by selecting Retry Connection from the Menu.",
                        "RCON Connection Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                lastRconDisconnectedLogTime = DateTime.Now;
            }

        }

        private void RunTimedCommands()
        {
            
            // Timed Commands Execution Tick

            while (true)
            {

                try
                {
                    foreach (ToolCommand command in Settings.Commands)
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
                                    if (RconConnected)
                                    {
                                        //PrintToConsole($"Timed command - {command.Name}: {commandString}");
                                        RconCommandQueue.Enqueue(
                                            RconCommand.ConsoleLogCommand(
                                                commandString, 
                                                $"Timed command - {command.Name}: {commandString}"
                                            )
                                        );
                                    }
                                }
                            }
                        }
                        else if (command.ConditionType == ToolCommand.TriggerType.Daily)
                        {
                            if (DateTime.Now.Hour == command.RunTime && command.Triggered == false)
                            {
                                command.Triggered = true;
                                if (RconConnected)
                                {                                    
                                    foreach (string commandString in command.CommandStrings)
                                    {
                                        //PrintToConsole($"Daily command - {command.Name}: {commandString}");
                                        RconCommandQueue.Enqueue(
                                            RconCommand.ConsoleLogCommand(
                                                commandString,
                                                $"Daily command - {command.Name}: {commandString}"
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
                }
                catch (Exception e) { AppLog($"Timed Command Error:\n{e}"); }

                //NOTE if there's any weird behavior, see if re-enabling this App.form.Invalidate() fixes it. I'm disabling it because it doesn't make sense
                //App.form.Invalidate();
                Thread.Sleep(200);

            }

        }

        private void ManageRconCommandQueue()
		{
            
            while (true) {

                if (RconCommandQueue.IsEmpty) { Thread.Sleep(25); continue; }

                RconWebSocketMutex.WaitOne();
                if (RconWebSocket == null) { 
                    RconWebSocketMutex.ReleaseMutex(); 
                    Thread.Sleep(25); 
                    continue; 
                }
                else { RconWebSocketMutex.ReleaseMutex(); }

                while (!RconCommandQueue.IsEmpty) {
                    try {
                        if (RconCommandQueue.TryDequeue(out RconCommand command)) {
                            if (command.IsValid) {
                                command.Log(this);
                                Command(command.command); // top tier code right here
                            }
                        }
                    } catch { break; }
                }

			}

		}

        public async void MonitorServerStatus()
        {

            using (HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) }) {
                
                string status = "";
                int serverHookReadInterval = 5, serverHookReadWait = 0;

                while (true) {

                    Thread.Sleep(1000);

                    serverHookReadWait++;
                    if (serverHookReadWait == serverHookReadInterval) {
                        serverHookReadWait = 0;
                        try { /*UpdateServerHook_PlayerTeamsByUid();*/ }
                        catch { }
					}

                    //TODO make sure the functionality of 'webClient.Encoding = Encoding.UTF8' is still supplied by the HTTPClient implementation
                    //TODO confirm that the HTTPClient implementation Auth Header works correctly with password protected servers

                    // Attempt to download server status string...
                    if (Settings.ServerInfoAddressIsValid) {

                        if (System.Net.Http.Headers.AuthenticationHeaderValue.TryParse(
                                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"dorito:{Settings.ServerPassword}" ?? ""))}",
                                out System.Net.Http.Headers.AuthenticationHeaderValue authHeader
                            )
                        ) { httpClient.DefaultRequestHeaders.Authorization = authHeader; }
                        else {
                            AppLog($"Failed to create Authentication Header for Server Status HTTP Request for {ConnectionName}");
                            throw new Exception($"Failed to create Authentication Header for Server Status HTTP Request for {ConnectionName}");
                        }

                        try { status = await httpClient.GetStringAsync(Settings.ServerInfoAddress); }
                        catch { status = ""; continue; }

                    }

                    ProcessServerStatus(status);

                }

            }

        }
        private void ProcessServerStatus(string status)
        {

            // Server Status Unchanged
            if (status == ServerStateJson) { return; }
            // Server Status Changed
            else { ServerStateJson = status; }

            if (ServerStateJson == null) { return; }

            //ITraceWriter traceWriter = new MemoryTraceWriter();
            //ServerState newState = JsonConvert.DeserializeObject<ServerState>(ServerStateJson, new JsonSerializerSettings() {TraceWriter = traceWriter });
            ServerState newState;
            try { newState = JsonConvert.DeserializeObject<ServerState>(ServerStateJson); }
            catch (Exception e)
            {
                AppLog($"Error decoding Server Status String:\n{e}");
                return;
            }

            if (!newState.IsValid)
            {
                try {
                    AppLog($"Invalid Server State:\n{JsonConvert.SerializeObject(newState)}");
                }
                catch (Exception jse) {
                    AppLog($"Invalid Server State: Failed to convert server state to JSON for logging:\n{jse}");
                }
                return;
            }

            State.Update(newState, this);
            if (!ServerHookEnabled && ShouldUseServerHook && !ServerHookAttempted && !string.IsNullOrWhiteSpace(State?.Name)) {
                AttemptServerHook();
                attemptingServerHook = false;
            }
            HasPlayers = (State?.Players?.Count ?? 0) > 0;
            if (IsDisplayedCurrently) {
                Scoreboard.RegenerateScoreboardImage = true;
            }

        }
        private async void ManageRanksAndEmblems()
        {

            //NOTE due to halostats shutting down, emblems can no longer be retrieved, so this method just exits now
            return;


            Dictionary<int, RankEmblemData> rankEmblemData = null;
            bool needEmblems = false;

            while (true) {

                Thread.Sleep(5000);

                // check if emblems needed
                lock (State.ServerStateLock) {
                    needEmblems = State.Players.Any(x => x.Emblem == null);
                }

                if (needEmblems) {

                    // in certain circumstances Server.PlayersInfo will return old data,
                    // including emblem and rank info for players who have already left
                    // this means that in these instances, the player emblems will be 
                    // unable to be loaded until this information is updated again,
                    // which seems to happen regularly during a match, but not in lobby
                    // 
                    // I have confirmed this behavior in the following circumstances:
                    //  one player alone in lobby

                    string rankAndEmblemJson = null;
                    while (rankAndEmblemJson == null) {
                        try { rankAndEmblemJson = await ServerMessenger.GetJsonResponse(this, "Server.PlayersInfo"); }
                        catch { continue; }
                    }
                    if (!string.IsNullOrEmpty(rankAndEmblemJson)) {

                        rankEmblemData = null;

                        try { rankEmblemData = JsonConvert.DeserializeObject<Dictionary<int, RankEmblemData>>(rankAndEmblemJson); }
                        catch (Exception e) { AppLog($"Failed to interpret rank and emblem data:\n{e}"); }

                        bool invalidResult = true;
                        lock (State.ServerStateLock) {
                            invalidResult = rankEmblemData.Count < State.Players.Count;
                        }
                        if (invalidResult) { rankEmblemData.Clear(); continue; }
                        else { RankAndEmblemData = rankEmblemData; }

                    }

                    
                    if (RankAndEmblemData.Count > 0) {
                        lock (State.ServerStateLock) { if (RankAndEmblemData.Count != State.Players.Count) { RankAndEmblemData.Clear(); continue; } }
                        bool updateScoreboard = false;
                        lock (State.ServerStateLock) {
                            for (int i = 0; i < RankAndEmblemData.Count; i++) {
                                if (!RankAndEmblemData.ContainsKey(i)) { continue; }
                                if (State.Players[i] != null && State.Players[i].Emblem == null) {
                                    State.Players[i].Rank = RankAndEmblemData[i].Rank;
                                    using (WebClient webClient = new WebClient()) {
                                        byte[] data = webClient.DownloadData(RankAndEmblemData[i].Emblem);
                                        using (MemoryStream mem = new MemoryStream(data)) {
                                            State.Players[i].Emblem =
                                                App.ResizeImage(new Bitmap(mem), Scoreboard.EmblemSize.X, Scoreboard.EmblemSize.Y)
                                            ;
                                        }
                                    }
                                    updateScoreboard = true;
                                }
                            }
                            RankAndEmblemData.Clear();
                        }
                        Scoreboard.RegenerateScoreboardImage = updateScoreboard;
                    }

                }

            }

        }

        public bool SaveSettings() {
            try {
                /*Database.SettingsTable.Update(Settings);*/
                settings.Save();
            }
            catch (Exception e) { throw e; }
            return true;
        }

		/// <summary>
		/// Updates the <paramref name="chatMessage"/> object with an appropriate translation (if translation succeeds).
		/// </summary>
		/// <param name="chatMessage">The ChatMessage object to be translated.</param>
		/// <param name="targetLanguageCode">The BCP-47 Language Code to translate the message to.</param>
		/// <param name="autoTranslating">True by default, indicating a failure during an automatic attempt to translate a chat message.
		/// <br>Only affects debug messages, making them more specific.</br></param>
		/// <returns>Returns the TranslateTextResponse object, or null if an exception was encountered.</returns>
		public string TranslateChatMessage(Message chatMessage, string targetLanguageCode = null, bool autoTranslating = true)
		{

            //return on null message
            if (string.IsNullOrWhiteSpace(chatMessage?.Text)) { return null; }

            //default target language is the server language configured in settings
            if (string.IsNullOrWhiteSpace(targetLanguageCode)) { targetLanguageCode = Settings.ServerLanguage; }

            // if AutoTranslating, apply filters to avoid unnecessary translation calls
            if (autoTranslating)
			{

                string lowerCaseMessage = chatMessage.Text.ToLowerInvariant();

                // if server language is english, filter out these patterns that are unlikely to be real words needing translation
                if (Settings.ServerLanguage == Translation.LanguageCodesByEnglishLanguageName[Translation.EnglishLanguageNames.English])
                {                    
                    
                    // single character messages
                    if (lowerCaseMessage.Length == 1) { return null; }

                    // all vowels
                    if (lowerCaseMessage.IsAllVowels_English()) { return null; }

                    // all consonants
                    if (lowerCaseMessage.IsAllConsonants_English()) { return null; }

                    // ('w' + 2 or more vowels) OR ('w' + vowel(s) + 'w')
                    if (lowerCaseMessage.Length > 2 && lowerCaseMessage[0] == 'w')
                    {
                        //('w' + vowel(s) + 'w') ex: woooow
                        if (lowerCaseMessage[lowerCaseMessage.Length - 1] == 'w'
                            && lowerCaseMessage.Substring(1, lowerCaseMessage.Length - 2).IsAllVowels_English())
                        {
                            return null;
                        }

                        //('w' + 2 or more vowels) ex: wooooooo waaaaaaa
                        if (lowerCaseMessage.Substring(1, lowerCaseMessage.Length - 1).IsAllVowels_English())
                        {
                            return null;
                        }
                    }

                    // (all a's o's e's and h's) ex: ahaha haha hah aahahahahhahahah hohoho ooh ooooohhhhhh ohhhhh heh hehe hehehehehe ehehehehe
                    // besides AAA (which is filtered already), there aren't any meaningful words to check for besides hoe - https://i.imgur.com/QuSdMhR.png
                    if (lowerCaseMessage.IsComposedOf("aoeh".ToCharArray()) && lowerCaseMessage.Trim() != "hoe") { return null; }


                    // messages starting with certain punctuation marks: " : ; ' & * # 
                    char f = lowerCaseMessage[0];
                    if (!char.IsWhiteSpace(f) && !char.IsLetterOrDigit(f)
                        && (f == '"' || f == ':' || f == ';' || f == '\'' || f == '&' || f == '*' || f == '#'))
                    { return null; }

                }

                // don't translate the message if it's included in the ignored phrases
                if (Settings.AutoTranslateIgnoredPhrasesList.Contains(lowerCaseMessage.Trim()))
                {
                    chatMessage.DetectedLanguage = Settings.ServerLanguage;
                    return null;
                }

            }

			// Get Translation
			Google.Cloud.Translation.V2.TranslationResult translationResult;
			try {
                App.TranslatedCharactersThisBillingCycle.Value += chatMessage.Text.Length;
                translationResult = App.TranslationClient.TranslateText(chatMessage.Text, targetLanguageCode); 
            }
			catch (Exception e) {
				if (autoTranslating) {
					AppLog($"Error while attempting auto-translation:\n{e}");
				}
				else {
					AppLog($"Message Translation Error:\n{e}");
				}
				chatMessage.DetectedLanguage = "";
				return null;
			}

			// If we got a translation, process it
			if (!string.IsNullOrWhiteSpace(translationResult.TranslatedText)) {
				// save the detected language
				if (string.IsNullOrWhiteSpace(chatMessage.DetectedLanguage) && !string.IsNullOrWhiteSpace(translationResult.DetectedSourceLanguage)) {
					chatMessage.DetectedLanguage = translationResult.DetectedSourceLanguage;
				}

				// if a translation was warranted, add it to the chatMessage object and update its status accordingly
				if ((translationResult.DetectedSourceLanguage ?? targetLanguageCode) != targetLanguageCode) {
					string translation = WebUtility.HtmlDecode(translationResult.TranslatedText);
					chatMessage.Translation.SetTranslation(targetLanguageCode, translation);
					//ServerSay($"Detected Language: {translation.DetectedLanguageCode}");
					//ServerSay($"Translation(en): {translation.TranslatedText}");
					//AppLog($"Detected Language: {chatMessage.DetectedLanguage}");
					if (targetLanguageCode == Settings.ServerLanguage) {
						chatMessage.ServerLanguageTranslation = translation;
						chatMessage.HasServerLanguageTranslation = true;
					}
				}

				// Record IsServerLanguage bool
				if (chatMessage.DetectedLanguage != Settings.ServerLanguage) {
					chatMessage.IsServerLanguage = false;
				}

			}
			else {
				chatMessage.DetectedLanguage = "";
			}

			return /*translateTextResponse*/ null;

        }

        #region Rcon WebSocket Events

        public void OnRconWebsocketClose(object sender, CloseEventArgs e)
        {
            //AppLog("Rcon WebSocket Closed");
        }

        public void OnRconWebsocketOpen(object sender, EventArgs e)
        {
            //AppLog("Rcon WebSocket Opened");
            //PrintToConsole("Sending RCON Password");
            RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    Settings.RconPassword, 
                    "Sending RCON Password..."
                )
            );
        }

        public void OnRconWebsocketMessage(object sender, MessageEventArgs e)
        {

            string message = e?.Data;
            if (message == null) { return; }

            //AppLog($"OnMessage TEXT:{e.IsText}|PING:{e.IsPing}|DATA:{e.IsBinary}|: [{App.DateTimeUTC()}]:\n{message}{(e.IsBinary ? "|" + Encoding.UTF8.GetString(e.RawData) : "")}");

            // By default the tool ignores JSON messages, identified by a first character of '{'
            if (message.StartsWith("{")) {
                if (App.FilterServerJson) { return; }
                else { PrintToConsole(message.FormatJson()); return; }
            }

            // I think this is a keep-alive message, sending the version number occasionally to keep the websocket connection open
            if (message.StartsWith("0.6")) { return; }

            // If the message starts with "accept" the WebSocket connection has been succesfully established
            else if (message.StartsWith("accept"))
            {

                // Indicate successful connection
                AppLog($"Successfully Connected to RCON");

                // Send 'OnConnect' commands to the server (if there are any for this server)
                if (Settings.SendOnConnectCommands != null && Settings.SendOnConnectCommands.Count > 0)
                {
                    
                    //PrintToConsole("Sending OnConnect Commands");

                    foreach (string command in Settings.SendOnConnectCommands)
                    {
                        RconCommandQueue.Enqueue(
                            RconCommand.ConsoleLogCommand(
                                command,
                                $"Sending OnConnectCommand: \"{command}\""
                            )
                        );
                    }

                }

            }

            // Otherwise, if not empty, parse this as a chat message
            else if (!string.IsNullOrWhiteSpace(message))
            {

                Message cm = ParseMessage(message);

                if (cm.IsValidChatMessage)
                {
                    
                    if (    !string.IsNullOrWhiteSpace(Settings.WebhookTrigger) 
                        &&  !string.IsNullOrWhiteSpace(Settings.Webhook) 
                        &&  cm.Text.StartsWith(Settings.WebhookTrigger))
                    {
                        SendToDiscord(message);
                    }

                    // Print Chat Message to Chat Log
                    PrintToChat(cm.Name, cm.Text);

                    // Dispatch chat event
                    PlayerInfo sendingPlayer = null;
                    lock (State.ServerStateLock)
                    {
                        sendingPlayer = State.Players.Find(x => x.Uid == cm.UID);
                    }

                    // Auto-translation
                    List<string> response = null;
                    if (CanTranslate && Settings.AutoTranslateChatMessages && sendingPlayer != null && !cm.Text.StartsWith("!")) {
                        TranslateChatMessage(cm);
                        if (cm.DetectedLanguage != Settings.ServerLanguage
                            && cm.HasServerLanguageTranslation
                            && !cm.Text.Trim().StartsWith("&")) {
                            response = ($"{cm.Name}[{cm.DetectedLanguage}▸{Settings.ServerLanguage}]: {cm.ServerLanguageTranslation}").Split(124);
                        }
                    }

                    // Save message to database
                    ChatMessages.Add(cm);

                    if (response != null && response.Count > 0) {
                        Respond(null, response, cm);
                    }

                    // Send OnMessage event
                    OnChatMessageReceived(
                        new ChatEventArgs(
                            cm.Text, 
                            sendingPlayer, 
                            cm.DateTimeString, 
                            this,
                            cm
                        )
                    );

                }
                else if (message.Contains(" -> ")) { PrintToConsole($"Setting Updated: {message}"); }
                else if (message.IsMultiLine()) { // EndsWith($"Map variant loaded successfully!")
					foreach (string part in message.SplitOnLineBreaks()) {
                        PrintToConsole($"Server: {part}");
                    }
                }
                else { PrintToConsole($"Server: {message}"); }
            }

        }

        public void OnRconWebsocketError(object sender, EventArgs e)
        {
            //AppLog("WebSocket Error\nError Message: \n" + eventArgs.Message + "\nException Message: " + (eventArgs.Exception?.Message ?? "none") + "\n");
        }

		#endregion

		#region Console Methods

		public string GetConsole()
        {
            return consoleText;
        }

        public void PrintToConsole(string line, bool saveRecord = true, bool timestamp = true)
        {

            string result = Regex.Replace(line, @"\r\n?|\n", Environment.NewLine);
            //if (result.StartsWith("SERVER: ") && result.Contains(Environment.NewLine)) { }
            //else { }

            if (timestamp) { result = $"[{App.DateTimeUTC()}]: {result}"; }

            if (saveRecord) { ConsoleMessages.Add(result); }

            result = $"{Environment.NewLine}{result}";
            if (IsDisplayedCurrently) { App.AppendConsole(result); }
            consoleText += result;
            Console.WriteLine($"{LogPrefix}: {result.Trim()}");
        }

        public void ClearConsole()
        {
            consoleText = "";
        }

        #endregion

        #region Chat Methods

        public string GetChat()
        {
            return chatText;
        }

        public void PrintToChat(Message chatMessage)
		{
            string result = Regex.Replace($"{chatMessage.Name}: {chatMessage.Text}", @"\r\n?|\n", Environment.NewLine);
            result = $"{Environment.NewLine}[{chatMessage.DateTimeString}] {result}";
            if (IsDisplayedCurrently) { App.AppendChat(result); }
            chatText += result;
            Console.WriteLine($"{LogPrefix}: {result.Trim()}");
        }
        public void PrintToChat(string playerName, string message)
        {
            
            string line = (playerName ?? "*UNKNOWN*" )+ ": " + (message ?? "" ) + "";

            string result = Regex.Replace(line, @"\r\n?|\n", Environment.NewLine);
            result = $"{Environment.NewLine}[{App.DateTimeUTC()}] {result}";
            if (IsDisplayedCurrently) { App.AppendChat(result); }
            chatText += result;
            Console.WriteLine($"{LogPrefix}: {result.Trim()}");
        }

        public void ClearChat()
        {
            chatText = "";
        }

        private const string MessageParseRegexString = "\\[([\\d\\/]*) ([\\d\\:]*)] <([^\\/]*)\\/([^\\/]*)\\/([^>]*)> ([^\n]*)";
        private static readonly Regex MessageParseRegex = new Regex(MessageParseRegexString, RegexOptions.Singleline);
        public static Message ParseMessage(string message)
        {
            Match m = MessageParseRegex.Match(message);
            if (m.Success && m.Groups.Count == 7)
            {

                DateTime dt;
                try { 
                    dt = DateTime.ParseExact(
                        m.Groups[1].ToString() + " " + m.Groups[2].ToString(),
                        App.ChatMessageDateTimeFormatString, 
                        System.Globalization.DateTimeFormatInfo.InvariantInfo
                    );
                }
                catch { dt = DateTime.UtcNow; }

                return new Message()
                {
                    IsValidChatMessage = true,
                    // DateTime examples from database '12/02/20 17:04:11' '12/03/20 03:08:58'
                    DateTimeString = m.Groups[1].ToString() + " " + m.Groups[2].ToString(),
                    Name = m.Groups[3].ToString(),
                    UID = m.Groups[4].ToString(),
                    IP = m.Groups[5].ToString(),
                    Text = m.Groups[6].ToString(),
                    DateTime = dt
                };
            }
            else
            {
                return new Message()
                {
                    IsValidChatMessage = false
                };
            }
        }

        #endregion

        #region PlayerLog Methods

        public string GetPlayerLog()
        {
            return playerLogText;
        }

        public void PrintToPlayerLog(string line, bool saveRecord = true, bool timestamp = true)
        {

            string result = Regex.Replace(line, @"\r\n?|\n", Environment.NewLine);

            if (timestamp) { result = $"[{App.DateTimeUTC()}]: {result}"; }
            if (saveRecord) { ConsoleMessages.Add(result); }

            result = $"{Environment.NewLine}{result}";

            if (IsDisplayedCurrently) { App.AppendPlayerLog(result); }
            playerLogText += result;

        }

        public void ClearPlayerLog()
        {
            playerLogText = "";
        }

        #endregion

        #region MatchQueue

        /// <summary>
        /// Sets the Server.MatchMode to MatchMode.Queue and disables in-lobby match voting so that the MatchQueue can run without interference.
        /// </summary>
        private void ActivateMatchQueue()
        {
            Settings.ServerMatchMode = ServerSettings.MatchMode.Queue;
            Command_ServerVotingDisable("Match Queue Activation");
		}

		/// <summary>
		/// Deactivates the server's MatchQueue (without clearing it), and (by default) re-enables voting by sending the <b>Server.VotingEnabled 1</b> command and (by default) sets the Server MatchMode to MatchMode.Voting.<br>
		/// You can specify false for the <paramref name="reenableVoting"/> parameter to prevent the <b>Server.VotingEnabled 1</b> command from being sent.</br><br>
		/// You can specify a different MatchMode for the <paramref name="newMatchMode"/> parameter to force a MatchMode other than MatchMode.Voting to be set.</br>
		/// </summary>
		/// <param name="reenableVoting">(True by default)<br>
		/// If set to true, <b>Server.VotingEnabled 1</b> will be sent to the server after deactivating the MatchQueue.</br><br>
		/// If set to false, no server command will be sent after deactivating the MatchQueue.</br></param>
		/// /// <param name="newMatchMode">The MatchMode that the server will be set to after deactivating the MatchQueue. By default, the server will be set to MatchMode.Voting.<br>
		/// If you set <paramref name="newMatchMode"/> to MatchMode.Queue, the only thing this method will do is re-enable voting (if <paramref name="reenableVoting"/> is set to true).</br></param>
		private void DeactivateMatchQueue(bool reenableVoting = true, ServerSettings.MatchMode newMatchMode = ServerSettings.MatchMode.Voting)
        {
            // re-enable voting
            if (reenableVoting) { Command_ServerVotingEnable("Match Queue Deactivation"); }
            // update server matchmode
            Settings.ServerMatchMode = newMatchMode;
        }

        /// <summary>
        /// Clears the MatchQueue and (by default) re-enables voting by sending the <b>Server.VotingEnabled 1</b> command and (by default) sets the Server MatchMode to MatchMode.Voting.<br>
        /// You can specify false for the <paramref name="reenableVoting"/> parameter to prevent the <b>Server.VotingEnabled 1</b> command from being sent.</br><br>
        /// You can specify a different MatchMode for the <paramref name="newMatchMode"/> parameter to force a specific MatchMode to be set after clearing the MatchQueue.</br>
        /// </summary>
        /// <param name="reenableVoting">(True by default)<br>
        /// If set to true, <b>Server.VotingEnabled 1</b> will be sent to the server after clearing the MatchQueue.</br><br>
        /// If set to false, no server command will be sent after clearing the MatchQueue.</br></param>
        /// /// <param name="newMatchMode">The MatchMode that the server will be set to after clearing the MatchQueue. By default, the server will be set to MatchMode.Voting.</param>
        /// <returns>The returned bool indicates whether the MatchQueue was successfully cleared.</returns>
        private bool ClearMatchQueue(bool reenableVoting = true, ServerSettings.MatchMode newMatchMode = ServerSettings.MatchMode.Voting)
        {
            bool retVal = true;
            try { MatchQueue = new System.Collections.Concurrent.ConcurrentQueue<MatchInfo>(); }
            catch (Exception e) {
                AppLog($"Failed to clear MatchQueue.\nException:\n{e}");
                retVal = false;
            }
            if (reenableVoting) { Command_ServerVotingEnable("Match Queue Cleared"); }
            Settings.ServerMatchMode = newMatchMode;
            return retVal;
        }

        /// <summary>
        /// Load the next match in the queue using the <b>Game.Map</b> and <b>Game.Gametype</b> server commands, and announce the loaded match in broadcast.<br>
        /// This method should only be called if the server is in lobby and Settings.ServerMatchMode == MatchMode.Queue</br>
        /// </summary>
        private void LoadNextMatchFromQueue()
        {

            MatchInfo matchInfo = null;
            while (!MatchQueue.IsEmpty && matchInfo == null) { MatchQueue.TryDequeue(out matchInfo); }
            if (matchInfo == null && MatchQueue.IsEmpty) {
                DeactivateMatchQueue();
                Broadcast("The match queue is now empty, voting will be re-enabled in the lobby.");
                return; 
            }

            RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    $"Game.Map \"{matchInfo.MapVariant.TypeNameForVotingFile}\"",
                    $"Game.Map \"{matchInfo.MapVariant.TypeNameForVotingFile}\"",
                    "Match Queue - Loading Map"
                )
            );
            Thread.Sleep(3000);
            RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    $"Game.Gametype \"{matchInfo.GameVariant.TypeNameForVotingFile}\"",
                    $"Game.Gametype \"{matchInfo.GameVariant.TypeNameForVotingFile}\"",
                    "Match Queue - Loading Gametype"
                )                
            );

            if (MatchQueue.IsEmpty) { DeactivateMatchQueue(); }

            AnnounceMapLoading(matchInfo.GameVariant, matchInfo.MapVariant);

            new Thread(new ThreadStart(() => {
                Thread.Sleep(15000);
                RconCommandQueue.Enqueue(
                    RconCommand.ConsoleLogCommand(
                        "Game.Start",
                        "Game.Start",
                        "Match Queue - Game Start"
                    )
                );
                if (MatchQueue.IsEmpty) {
                    Thread.Sleep(5000);
                    Broadcast("Match Queue empty: voting re-enabled.");
                }

            })).Start();
            
        }

        public void Command_ActivateMatchQueue()
		{
            ActivateMatchQueue();
		}
        public void Command_DeactivateMatchQueue(bool reenableVoting = true, ServerSettings.MatchMode newMatchMode = ServerSettings.MatchMode.Voting)
        {
            DeactivateMatchQueue(reenableVoting, newMatchMode);
        }
        public void Command_ClearMatchQueue(bool reenableVoting = true, ServerSettings.MatchMode newMatchMode = ServerSettings.MatchMode.Voting)
        {
            ClearMatchQueue(reenableVoting, newMatchMode);
        }
        public bool Command_SetNextGameInQueue(string game, string map, string respondPlayer = null, bool announce = true)
        {
            return AddGameToQueue(game, map, respondPlayer, true, announce);
        }
        public bool Command_AddGameToQueue(string game, string map, string respondPlayer = null, bool addToBeginning = false, bool announce = true)
        {
            return AddGameToQueue(game, map, respondPlayer, addToBeginning, announce);
        }
        public bool AddGameToQueue(string game, string map, string respondPlayer = null, bool addToBeginning = false, bool announce = true)
        {

            MatchInfo matchInfo = GetMatchInfo(game, map, respondPlayer);

            if (matchInfo.IsValid) {
                AddGameToQueue(matchInfo, respondPlayer, addToBeginning, announce);
                return true;
            }
            else { return false; }

        }
        private void AddGameToQueue(MatchInfo matchInfo, string respondPlayer, bool addToBeginning = false, bool announce = true)
        {

            // Add to queue
            if (addToBeginning) {                
                
                // Have to copy match queue into a list, because there's no simple way (that I know of) to insert an item
                // in the first slot of an existing threadsafe queue
                
                List<MatchInfo> queueBuffer = new List<MatchInfo>() { matchInfo };
				while (MatchQueue.TryDequeue(out MatchInfo temp)) {
					queueBuffer.Add(temp);
				}
				while (queueBuffer.Count > 0) {
                    MatchQueue.Enqueue(queueBuffer[0]);
                    queueBuffer.RemoveAt(0);
                }
            }
            else { MatchQueue.Enqueue(matchInfo); }

            // Announce match queue update
            if (addToBeginning) { AnnounceNextMatch(matchInfo.GameVariant, matchInfo.MapVariant, respondPlayer, announce); }
            else { AnnounceMatchAddedToQueue(matchInfo.GameVariant, matchInfo.MapVariant, respondPlayer, announce); }

        }

        public void AnnounceNextMatch(GameVariant game, MapVariant map, string respondPlayer = null, bool announce = true)
        {
            List<string> announcement = new List<string>();
            string matchAddedString = $"Next match set: {game.Name} ON {map.Name}!";
            if (matchAddedString.Length <= 120) {
                announcement.Add(matchAddedString);
            }
            else {
                announcement.Add("Next match has been set!");
                announcement.Add($"{game.Name} ({game.BaseGameTypeString})");
                announcement.Add($"ON {map.Name} ({map.BaseMapString})");
            }
            if (announce || respondPlayer == null) { Broadcast(announcement); }
            else { Respond(respondPlayer, announcement); }
        }
        public void AnnounceMatchAddedToQueue(GameVariant game, MapVariant map, string respondPlayer = null, bool announce = true)
        {

            List<string> announcement = new List<string>();
            string matchAddedString = $"{game.Name} ON {map.Name} added to match queue!";
            if (matchAddedString.Length <= 120) {
                announcement.Add(matchAddedString);
			}
            else {
                announcement.Add("Match added to match queue!");
                announcement.Add($"{game.Name} ({game.BaseGameTypeString})");
                announcement.Add($"ON {map.Name} ({map.BaseMapString})");
            }
            if (announce || respondPlayer == null) { Broadcast(announcement); }
            else { Respond(respondPlayer, announcement); }
        }
        public void AnnounceMapLoading(GameVariant game, MapVariant map, int secondsUntilStart = 15)
        {

            if (secondsUntilStart < 1) { secondsUntilStart = 15; }

            Broadcast(new List<string>() {
                $"LOADING {game.Name}", // ({game.BaseGameTypeString})",
                $"ON {map.Name}", // ({map.BaseMapString})",
                $"Game starts in {secondsUntilStart.ToString()} seconds"
            });

            DateTime matchStartTime = DateTime.Now + TimeSpan.FromSeconds(secondsUntilStart);

            new Thread(new ThreadStart(() => {
                Thread.Sleep(1000);
                while (DateTime.Now < matchStartTime) {
                    if ((matchStartTime - DateTime.Now).Seconds > 4) {
                        if ((matchStartTime - DateTime.Now).Seconds % 5 == 0) {
                            Broadcast($"Game starts in {(matchStartTime - DateTime.Now).Seconds} seconds");
                            if ((matchStartTime - DateTime.Now).Seconds == 5) { break; }
                        }
				    }
                    //else {
                    //    Thread.Sleep(1000);
                    //    Broadcast("Game starts in 3...");
                    //    Thread.Sleep(1000);
                    //    Broadcast("Game starts in 2...");
                    //    Thread.Sleep(1000);
                    //    Broadcast("Game starts in 1...");
                    //    return;
                    //}
                    Thread.Sleep(1000);
                }
            })).Start();
        }

		#endregion

		#region Dynamic Votefile

        public bool Command_SetDynamicVoteFileManagement(bool state)
		{
            Settings.UseLocalFiles = state;
            try { SaveSettings(); }
            catch (Exception e) {
                // Revert setting because it (most likely) failed to save 
                Settings.UseLocalFiles = !Settings.UseLocalFiles;
                throw e;
            }
            return true;
        }
		public void Command_EnableDynamicVoteFileManagement()
        {            
            Settings.UseLocalFiles = true;
            SaveSettings();
            LoadGameVariants();
            LoadMapVariants();
        }
        public void Command_DisableDynamicVoteFileManagement()
        {
            Settings.UseLocalFiles = false;
            SaveSettings();
        }
        public async Task<bool> SetActiveVotingJson(string jsonFileName)
        {
            if (jsonFileName.EndsWith(".json") == false)
            {
                jsonFileName += ".json";
            }
            await ServerMessenger.GetStringResponse(this, "Server.VotingJsonPath \"" + Settings.RelativeVotingPath + "\\" + jsonFileName + "\"");
            string response = await ServerMessenger.GetStringResponse(this, "Server.ReloadVotingJson");
            return response.StartsWith("Success");
        }

        #endregion

        #region Map + Game Variant Helpers

        public MatchInfo GetMatchInfo(string game, string map, string respondPlayer = null)
        {
            GameVariant gameVariant = GetGameVariant(game);
            MapVariant mapVariant = GetMapVariant(map);

            if (gameVariant == null || !gameVariant.IsValid || mapVariant == null || !mapVariant.IsValid) 
            {
                return new MatchInfo(gameVariant, mapVariant) { IsValid = false };
            }
            else
            {
                return new MatchInfo(gameVariant, mapVariant);
            }
        }

		public void LoadMapVariants()
        {
            if (Settings.MapVariantsDirectory != null)
            {
                
                mapVariants.Clear();
                foreach (DirectoryInfo directory in Settings.MapVariantsDirectory.GetDirectories())
                {
                    MapVariant newMV;
                    try { newMV = new MapVariant(directory); }
                    catch (Exception e) { AppLog($"Error Parsing MapVariant at '{directory.FullName}'\n{e}"); continue; }
                    if (!mapVariants.Contains(newMV)) { mapVariants.Add(newMV); }
                }

                mapVariantNames.Clear();
                foreach (MapVariant item in mapVariants)
                {
                    mapVariantNames.Add(item.Name);
                }

                lastMapVariantLoadTime = DateTime.Now;

            }
            else
            {
                AppLog("Failed to get map variants. MapVariants Directory could not be found.");
                return;
            }
        }
        public void LoadGameVariants()
        {
            if (Settings.GameVariantsDirectory != null)
            {

                foreach (DirectoryInfo directory in Settings.GameVariantsDirectory.GetDirectories())
                {
                    GameVariant newGV;
                    try { newGV = new GameVariant(directory); }
                    catch (Exception e) { AppLog($"Error Parsing GameVariant at '{directory.FullName}'\n{e}"); continue; }
                    if (!gameVariants.Contains(newGV)) { gameVariants.Add(newGV); }
                }

                gameVariantNames.Clear();
                foreach (GameVariant item in gameVariants)
                {
                    gameVariantNames.Add(item.Name);
                }

                lastGameVariantLoadTime = DateTime.Now;

            }
            else
            {
                AppLog("Failed to get game variants. GameVariants Directory could not be found.");
                return;
            }
        }

        public void SendMapDescriptions(MapVariant.BaseMap baseMap, Message chatMessage = null)
        {
            SendMapDescriptions(baseMap, "", chatMessage);
        }
        public void SendMapDescriptions(string baseMap, string playerName, Message chatMessage = null)
        {
            SendMapDescriptions(MapVariant.GetBaseMap(baseMap), playerName, chatMessage);
        }
        public void SendMapDescriptions(string baseMap, PlayerInfo player, Message chatMessage = null)
        {
            SendMapDescriptions(MapVariant.GetBaseMap(baseMap), player?.Name, chatMessage);
        }        
        public void SendMapDescriptions(MapVariant.BaseMap baseMap, PlayerInfo player, Message chatMessage = null)
        {
            SendMapDescriptions(baseMap, player?.Name, chatMessage);
        }
        public void SendMapDescriptions(MapVariant.BaseMap baseMap, string playerName, Message chatMessage = null)
        {

            if (baseMap == MapVariant.BaseMap.Unknown) { Respond(playerName, new string[] { "Invalid Base Map Received." }, chatMessage); return; }
            if (MapVariants.Count == 0) { Respond(playerName, new string[] { "There are no Map Variants to list." }, chatMessage); return; }

            List<string> descriptions = new List<string>();

            // Add descriptions for all base maps
            if (baseMap == MapVariant.BaseMap.All)
            {
				foreach (MapVariant variant in MapVariants) {
                    descriptions.Add(variant.Description_OneLine);
                }
                if (descriptions.Count == 0) { 
                    descriptions.Add("There are no Map Variants to list."); 
                }
            }
            // Add descriptions for maps with specific base map
            else {
                foreach (MapVariant variant in MapVariants)
                {
                    if (variant.BaseMapID == baseMap) {
                        descriptions.Add(variant.Description_OneLine);
                    }
                }
                if (descriptions.Count == 0) { 
                    descriptions.Add($"There are no {MapVariant.BaseMapDisplayNamesByBaseMap[baseMap]} Map Variants to list."); 
                }
            }

            Respond(playerName, descriptions, chatMessage);

        }

        public void SendMapDescription(MapVariant map, Message chatMessage = null)
        {
            SendMapDescription(map, (string)null, chatMessage);
        }
        public void SendMapDescription(MapVariant map, PlayerInfo player, Message chatMessage = null)
        {
            SendMapDescription(map, player?.Name, chatMessage);
        }
        public void SendMapDescription(MapVariant map, string playerName, Message chatMessage = null)
        {
            Respond(playerName, map.Description_Chunked, chatMessage);
        }

        public void SendGameDescriptions(GameVariant.BaseGame baseGame, Message chatMessage = null)
        {
            SendGameDescriptions(baseGame, "", chatMessage);
        }
        public void SendGameDescriptions(string baseGame, string playerName, Message chatMessage = null)
        {
            SendGameDescriptions(GameVariant.GetBaseGame(baseGame), playerName, chatMessage);
        }
        public void SendGameDescriptions(string baseGame, PlayerInfo player, Message chatMessage = null)
        {
            SendGameDescriptions(GameVariant.GetBaseGame(baseGame), player?.Name, chatMessage);
        }
        public void SendGameDescriptions(GameVariant.BaseGame baseGame, PlayerInfo player, Message chatMessage = null)
        {
            SendGameDescriptions(baseGame, player?.Name, chatMessage);
        }
        public void SendGameDescriptions(GameVariant.BaseGame baseGame, string playerName, Message chatMessage = null)
        {

            if (baseGame == GameVariant.BaseGame.Unknown) { Respond(playerName, new string[] { "Invalid Base Game Received." }, chatMessage); return; }
            if (GameVariants.Count == 0) { Respond(playerName, new string[] { "There are no Game Variants to list." }, chatMessage); return; }

            List<string> descriptions = new List<string>();

            // Add descriptions for all base gametypes
            if (baseGame == GameVariant.BaseGame.All) {
                foreach (GameVariant variant in GameVariants) {
                    descriptions.Add(variant.Description_OneLine);
                }
                if (descriptions.Count == 0) { 
                    descriptions.Add("There are no Game Variants to list."); 
                }
            }
            // Add descriptions for games with specific base gametype
            else {                
                foreach (GameVariant variant in GameVariants) {
                    if (variant.BaseGameID == baseGame) {
                        descriptions.Add(variant.Description_OneLine);
                    }
                }
                if (descriptions.Count == 0) { 
                    descriptions.Add($"There are no {GameVariant.BaseGameShortDisplayNamesByBaseGame[baseGame]} Game Variants to list."); 
                }
            }
            
            Respond(playerName, descriptions, chatMessage);

        }

        public void SendGameDescription(GameVariant game, Message chatMessage = null)
        {
            SendGameDescription(game, (string)null, chatMessage);
        }
        public void SendGameDescription(GameVariant game, PlayerInfo player, Message chatMessage = null)
        {
            SendGameDescription(game, player?.Name, chatMessage);
        }
        public void SendGameDescription(GameVariant game, string playerName, Message chatMessage = null)
        {
            Respond(playerName, game.Description_Chunked, chatMessage);
        }

        public GameVariant GetGameVariant(string game)
        {

            if (string.IsNullOrWhiteSpace(game)) { 
                return null; 
            }

            // If a built in game variant is matched, just skip to the return
            if (!GameVariant.TryGetBuiltInVariant(game, out GameVariant match)) { 
                // Try to find exact name match
                match = GameVariants.Find(x => x.Name == game);
                if (match != null) { return match; }
                // Fall back to case-invariant match
                match = GameVariants.Find(x => x.Name.ToLowerInvariant() == game.ToLowerInvariant());
            }
            return match;

        }
        public MapVariant GetMapVariant(string map)
        {

            if (string.IsNullOrWhiteSpace(map)) { 
                return null; 
            }

            // If a built in map variant is matched, just skip to the return
			if (!MapVariant.TryGetBuiltInMapVariant(map, out MapVariant match)) {
				// Try to find exact name match
				match = MapVariants.Find(x => x.Name == map);
				if (match != null) { return match; }
				// Fall back to case-invariant match
				match = MapVariants.Find(x => x.Name.ToLowerInvariant() == map.ToLowerInvariant());
			}
			return match;

        }

        public bool IsValidGameVariant(string name, out GameVariant game)
        {
            if (string.IsNullOrWhiteSpace(name)) { game = null; return false; }
            game = GameVariants.FirstOrDefault(x => x.Name == name);
            return game == null;
        }
        public bool IsValidMapVariant(string name, out MapVariant map)
        {
            if (string.IsNullOrWhiteSpace(name)) { map = null; return false; }
            map = MapVariants.FirstOrDefault(x => x.Name == name);
            return map == null;
        }
        public bool IsValidGameVariant(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return false; }
            return GameVariants.Any(x => x.Name == name);
        }
        public bool IsValidMapVariant(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return false; }
            return MapVariants.Any(x => x.Name == name);
        }

		#endregion

		#region Custom Chat Voting

        public void Command_BeginVote(Action action, PlayerInfo voteStarterPlayer, string voteBlurb, string voteStartMessage, string votePassedMessage, Message chatMessage = null)
        {
            BeginVote(action, voteStarterPlayer, voteBlurb, new List<string>() { voteStartMessage }, new List<string>() { votePassedMessage }, chatMessage);
        }
        public void Command_BeginVote(Action action, PlayerInfo voteStarterPlayer, string voteBlurb, List<string> voteStartMessages, List<string> votePassedMessages, Message chatMessage = null)
        {
            BeginVote(action, voteStarterPlayer, voteBlurb, voteStartMessages, votePassedMessages, chatMessage);
        }
        public void BeginVote(Action action, PlayerInfo voteStarterPlayer, string voteBlurb, List<string> voteStartMessages, List<string> votePassedMessages, Message chatMessage = null)
        {

            if (VoteInProgress) {
                Respond(voteStarterPlayer?.Name, new string[] { "Unable to start another vote while a vote is currently in progress." }, chatMessage);
                return;
            }

            // Pass a server-initiated vote if the server is empty
            if (State.Players.Count == 0) {
                Respond(null, new List<string> {
                    $"{voteStarterPlayer?.Name ?? "SERVER"} has started a vote to {voteBlurb}.",
                    "There are no players on the server, so congratulations, the vote has passed."
                }, chatMessage);
                action();
                return;
            }

            UidVotePairs.Clear();
            VoteInProgress = true;
            string voteStarterName = voteStarterPlayer?.Name ?? "SERVER";

            // Add vote statuses for all players currently in game
            foreach (PlayerInfo player in State.Players) {
                UidVotePairs.TryAdd(player.Uid, player == voteStarterPlayer);
                //while (!UidVotePairs.TryAdd(player.Uid, player == voteStarterPlayer));
            }
            // If vote was server-initiated, add a vote for the server (this will mess up the votes required to pass ratio but who cares)
            if (voteStarterPlayer == null) { while (!UidVotePairs.TryAdd("server", true)) { }; }

            // Calculate required votes
            if (State.Players.Count == 1) { VotesRequiredToPass = 1; }
            else { VotesRequiredToPass = (State.Players.Count / 2) + 1; }

            // Setup vote messages
            voteStartMessages.Add(voteStarterName + " has started a vote to " + voteBlurb + ".");
            voteStartMessages.Add("Type \"!yes\" to vote.");

            votePassedMessages.Insert(0, "Vote has passed!");

            // Send vote start messages
            Respond(null, voteStartMessages);

            DateTime voteStartTime = DateTime.Now;
            DateTime voteEndTime = voteStartTime.AddSeconds(VoteDuration);

            TimeSpan voteStatusUpdateInterval = TimeSpan.FromSeconds((int)(VoteDuration / (float)(VoteStatusUpdates + 1) * 1000f));

            bool votePassed = false;

            ChatMessageReceived += CheckVote;

            new Thread(new ThreadStart(() => {

                int voteCount = 0;
                int votesNeeded = VotesRequiredToPass;
                DateTime lastVoteUpdate = DateTime.Now;

                while (DateTime.Now < voteEndTime) {

                    voteCount = 0;
                    foreach (bool value in UidVotePairs.Values) {
                        if (value) { voteCount++; }
                    }

                    if (voteCount >= votesNeeded) {
                        votePassed = true;
                        break;
                    }
                    else if (DateTime.Now - lastVoteUpdate > voteStatusUpdateInterval) {                        
                        if (votesNeeded - voteCount == 1) {
                            Broadcast($"Voting to {voteBlurb}. 1 vote needed. !yes to vote.");
                        }
                        else {
                            Broadcast($"Voting to {voteBlurb}. {(votesNeeded - voteCount)} votes needed. !yes to vote.");
                        }
                        lastVoteUpdate = DateTime.Now;
                    }

                    Thread.Sleep(500);

                }

                ChatMessageReceived -= CheckVote;

                voteCount = 0;
                foreach (bool value in UidVotePairs.Values) { if (value) { voteCount++; } }
                votePassed = (voteCount >= votesNeeded);

                if (votePassed) {
                    Respond(null, votePassedMessages);
                    UidVotePairs.Clear();
                    VoteInProgress = false;
                    action();
                }
                else {
                    voteCount = 0; foreach (bool value in UidVotePairs.Values) { if (value) { voteCount++; } }
                    Broadcast($"Vote did not pass. Received {voteCount}/{votesNeeded} needed votes.");
                    UidVotePairs.Clear();
                    VoteInProgress = false;
                }
                

            })).Start();

        }
        void CheckVote(object sender, ChatEventArgs e)
        {
            if (e.Message.ToLowerInvariant().StartsWith("!yes") && UidVotePairs.Keys.Contains(e.SendingPlayer.Uid))
            {
                if (UidVotePairs[e.SendingPlayer.Uid]) {
                    Whisper(e.SendingPlayer.Name, new List<string>() { "You can only vote once." });
                }
                else
                {
                    UidVotePairs[e.SendingPlayer.Uid] = true;                        
                    int voteCount = 0; foreach (bool value in UidVotePairs.Values) { if (value) { voteCount++; } }
                    Broadcast($"{e.SendingPlayer.Name}: !yes {voteCount}/{VotesRequiredToPass}");
                }
            }
        }

		#endregion

		#region Server Communication

        public void SendToDiscord(string message)
        {

            if (string.IsNullOrWhiteSpace(Settings.WebhookRole)) { 
                MessageBox.Show(
                    "Discord integration is not fully set up. You must specify the 'role' that will receive messages.",
                    "Discord Integration Incomplete",
                    MessageBoxButtons.OK
                ); 
                return; 
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Settings.Webhook);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                
                string roleInfo = "<@&" + Settings.WebhookRole + "> ";
                string json = "{\"content\":\"" + roleInfo + "Player reported on " + State.Name + ": " + message + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
        private void Command(string cmd)
        {
            RconWebSocketMutex.WaitOne();
            if (RconWebSocket.IsAlive) {                    
                try {
                    if (App.Debug_RconCommandQueueLog) {
                        AppLog($"RCON SEND: \"{cmd}\"");
					}
                    RconWebSocket.Send(Encoding.UTF8.GetBytes(cmd));
                }
                catch (Exception e) {
                    AppLog($"WebSocket Command Transmission Error\nCommand issued: {cmd}\n{e}");
                }                    
            }
            RconWebSocketMutex.ReleaseMutex();
        }

        /// <summary>
        /// Disables voting for the next match in the lobby by sending the server the <b>Server.VotingEnabled 0</b> command.
        /// </summary>
        public void Command_ServerVotingDisable(string callingPlayerOrFunction = "")
		{
            RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.VotingEnabled 0",
                    "Server.VotingEnabled 0",
                    callingPlayerOrFunction
                )
            );
        }
        /// <summary>
        /// Enables voting for the next match in the lobby by sending the server the <b>Server.VotingEnabled 1</b> command.
        /// </summary>
        public void Command_ServerVotingEnable(string callingPlayerOrFunction = "")
        {
            RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    "Server.VotingEnabled 1",
                    "Server.VotingEnabled 1",
                    callingPlayerOrFunction
                )
            );
        }

        public void Command_SetCommandEnabledStateByTag(string tag, bool state, ParseResult parseResult = null)
		{            
            
            if (string.IsNullOrWhiteSpace(tag)) {
                if (parseResult != null) {
                    parseResult.Add("Command failed. The supplied tag was invalid.");
                }
                return;
            }

            int matches = 0;
            List<bool> previousStates = new List<bool>();
            foreach (ToolCommand item in Settings.Commands) {
                if (item.Tag.Contains(tag)) {
                    matches++;
                    previousStates.Add(item.Enabled);
                    if (item.Enabled != state) {
                        if (state) { item.Enable(false); }
                        else { item.Disable(false); }
                    }
                }
            }
            App.RepopulateConditionalCommandsDropdown = true;

            if (parseResult != null) {
                try {
                    if (SaveSettings()) {
                        parseResult.Add($"Successfully {(state ? "en" : "dis")}abled {matches} command{(matches > 1 ? "s" : "")}.");
                    }
                    else {
                        parseResult.Add($"An unknown error occured, command enabled states may not have been changed or saved.");
                        parseResult.Add("The server tool's state may be unstable, so please restart the application as soon as possible.");
                    }
                }
                catch (Exception e) {					
                    // Reset command enabled states since an exception occured while saving
                    for (int i = 0; i < Settings.Commands.Count; i++) {
                        if (Settings.Commands[i].Tag.Contains(tag)) {
                            if (Settings.Commands[i].Enabled != previousStates[i]) {
                                if (Settings.Commands[i].Enabled) { Settings.Commands[i].Disable(false); }
                                else { Settings.Commands[i].Enable(false); }
                            }
                        }
                    }
                    App.RepopulateConditionalCommandsDropdown = true;
                    parseResult.Add($"Failed to save command enabled states. States have been reverted to their previous values.");
                    parseResult.Add($"Error message: {e.Message}");
                    return;
                }

                if (matches > 0) {
                    parseResult.Add($"{matches} matching commands were found, and were successfully {(state ? "en" : "dis")}abled.");
                }
                else {
                    parseResult.Add($"Successfully processed the request, but no matching commands were found to {(state ? "en" : "dis")}able.");
                }

            }
            else {
                try { SaveSettings(); }
                catch {
                    // Reset command enabled states since an exception occured while saving
                    for (int i = 0; i < Settings.Commands.Count; i++) {
                        if (Settings.Commands[i].Tag.Contains(tag)) {
                            if (Settings.Commands[i].Enabled != previousStates[i]) {
                                if (Settings.Commands[i].Enabled) { Settings.Commands[i].Disable(false); }
                                else { Settings.Commands[i].Enable(false); }
                            }
                        }
                    }
                    App.RepopulateConditionalCommandsDropdown = true;
                }
            }
        }
        public void Command_SetCommandEnabledState(ToolCommand command, bool state, ParseResult parseResult = null)
		{
            bool prevCommandState = command.Enabled;
            if (state) { command.Enable(); }
            else { command.Disable(); }

            if (parseResult != null) {
                try {
                    if (SaveSettings()) {
                        parseResult.Add($"Successfully {(state ? "en" : "dis")}abled the command.");
                    }
                    else {
                        parseResult.Add($"Currently the command is {(command.Enabled ? "enabled" : "disabled")}, however, an unknown error occured. ");
                        parseResult.Add("The server tool's state may be unstable, so please restart the application as soon as possible.");
                    }
                }
                catch (Exception e) {
                    if (command.Enabled != prevCommandState) {
                        if (command.Enabled) { command.Disable(); }
                        else { command.Enable(); }
                    }
                    parseResult.Add($"Failed to save command state. Command will remain {(command.Enabled ? "ENABLED" : "DISABLED")}.");
                    parseResult.Add($"Error message: {e.Message}");
                }
            }
            else {
                try { SaveSettings(); }
                catch {
                    if (command.Enabled != prevCommandState) {
                        if (command.Enabled) { command.Disable(); }
                        else { command.Enable(); }
                    }
                }
            }
        }

        public void Respond(string playerName = null, string message = null, Message chatMessage = null)
        {
            if (string.IsNullOrWhiteSpace(message)) { throw new ArgumentException("Invalid argument:'message'. The message passed was either null or empty, unable to send response."); }
            Respond(playerName, new string[] { message }, chatMessage);
        }
        public void Respond(string playerName = null, IEnumerable<string> messages = null, Message chatMessage = null)
        {
            if (messages == null || messages.Count() == 0) { throw new ArgumentException("Invalid argument:'messages'. The list of messages passed was either null or empty, unable to send response."); }
            if (chatMessage != null) { chatMessage.ServerResponses.AddRange(messages); }
            if (!string.IsNullOrEmpty(playerName))
            {
                Whisper(playerName, messages);
            }
            else
            {
                Broadcast(messages);
            }
            if (chatMessage != null) {
                try { ChatMessages.Add(chatMessage); }
                catch { }
            }
        }
        
        /// <summary>
        /// Sends the message to the server chat - visible to everyone. <strong>This method should never be called directly!</strong>
        /// </summary>
        private void ServerSay(string message)
		{
            
            //ChatMessage chatMessage = new ChatMessage()
            //{
            //    DateTime = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss"),
            //    Name = "SERVER",
            //    UID = "0000000000000000",
            //    IP = "127.0.0.1",
            //    Message = message,
            //    DetectedLanguage = Settings.ServerLanguage,
            //    IsServerLanguage = true,
            //    ServerLanguageTranslation = "",
            //    HasServerLanguageTranslation = false,
            //    TranslationsJson = "{}"
            //};

            // Using default RconCommand.Command because Server.Say
            // will be picked up and displayed as a chat message
            RconCommandQueue.Enqueue(RconCommand.Command($"Server.Say \"{message}\""));
            //Database.MessagesTable.Add(chatMessage);

        }
        /// <summary>
        /// Sends the message(s) to the server chat - visible to everyone. <strong>This method should never be called directly!</strong>
        /// </summary>
        private void ServerSay(IEnumerable<string> messages)
		{

            //ChatMessage chatMessage = new ChatMessage()
            //{
            //    IP = "127.0.0.1",
            //    UID = "0000000000000000",
            //    DateTime = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss"),
            //    Name = "SERVER",
            //    Message = string.Join(DatabaseServerMessageSeparator, messages)
            //};

            foreach (string message in messages) {
                // Using default RconCommand.Command because Server.Say
                // will be picked up and displayed as a chat message
                RconCommandQueue.Enqueue(RconCommand.Command($"Server.Say \"{message}\""));
            }
            //Database.MessagesTable.Add(chatMessage);

		}        
        public void Broadcast(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Invalid argument:'message'. The message passed was either null or empty, unable to broadcast.");
            }
            if (message.Length > 124) {
                Broadcast(message.Split(124));
			}
            else {
                ServerSay(message);
            }
            
        }
        public void Broadcast(IEnumerable<string> messages)
        {
            if (messages == null || messages.Count() == 0) { 
                throw new ArgumentException("Invalid argument:'messages'. The list of messages passed was either null or empty, unable to broadcast."); 
            }
            ServerSay(messages);
        }

        /// <summary>
        /// Sends the message as a PM to the specified player. <strong>This method should never be called directly!</strong>
        /// </summary>
        private void ServerPM(string playerName, string message)
        {

            if (string.IsNullOrWhiteSpace(playerName)) { throw new ArgumentException("The playerName argument was not valid - unable to send PM."); }

            //ChatMessage chatMessage = new ChatMessage()
            //{
            //    IP = "127.0.0.1",
            //    UID = "0000000000000000",
            //    DateTime = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss"),
            //    Name = "SERVER",
            //    //Name = $"PM:{playerName}",
            //    Message = message
            //};

            // Using default RconCommand.Command because Server.PM
            // will be picked up and displayed as a chat message I think
            RconCommandQueue.Enqueue(RconCommand.Command($"Server.PM \"{playerName}\" \"{message}\""));

            //Database.MessagesTable.Add(chatMessage);

        }
        /// <summary>
        /// Sends the message(s) as a PM to the specified player. <strong>This method should never be called directly!</strong>
        /// </summary>
        private void ServerPM(string playerName, IEnumerable<string> messages)
		{
            
            if (string.IsNullOrWhiteSpace(playerName)) { throw new ArgumentException("The playerName argument was not valid - unable to send PM."); }

            //ChatMessage chatMessage = new ChatMessage()
            //{
            //    IP = "127.0.0.1",
            //    UID = "0000000000000000",
            //    DateTime = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss"),
            //    Name = "SERVER",
            //    //Name = $"PM:{playerName}",
            //    Message = string.Join(DatabaseServerMessageSeparator, messages)
            //};

            foreach (string message in messages) {
                // Using default RconCommand.Command because Server.PM
                // will be picked up and displayed as a chat message I think
                RconCommandQueue.Enqueue(RconCommand.Command($"Server.PM \"{playerName}\" \"{message}\""));
			}

            //Database.MessagesTable.Add(chatMessage);

        }
        public void Whisper(string playerName, string message)
        {
            if (message != null && message.Length > 124) {
                Whisper(playerName, message.Split(124));
			}
            else {
                ServerPM(playerName, message);
            }            
        }
        public void Whisper(string playerName, IEnumerable<string> messages)
        {
            ServerPM(playerName, messages);
        }

        #endregion

        #region Utility

        public bool CanTranslate { get { return App.CanTranslate && Settings.TranslationEnabled; } }

        
        public List<Tuple<PlayerInfo, int>> GenerateBalancedTeamList(int numTeams, bool useRecordedStats = false)
		{
            
            if ((State?.Players?.Count ?? 0) < 1) { return null; }

            List<int> activeTeams = new List<int>();

            if (InLobby) {
                // Handle invalid numTeams
                if (numTeams < 2) { numTeams = 2; }
                else if (numTeams > 8) { numTeams = 8; }
                // add indices until we have enough indices to match numTeams
                for (int i = 0; i < numTeams; i++) { activeTeams.Add(i); }
            }
            else {
                if (!State.Teams) { return null; }
                else {

                    // add active team indices first
                    foreach (PlayerInfo playerInfo in State.Players) {
                        if (!activeTeams.Contains(playerInfo.Team)) { activeTeams.Add(playerInfo.Team); }
                    }

                    // valid number of active teams
                    if (activeTeams.Count > 1 && activeTeams.Count < 9) {

                        // Handle invalid numTeams
                        if (numTeams < 2 || numTeams > 8) { numTeams = activeTeams.Count; }

                        // If we're balancing players into a smaller number of teams
                        // remove the teams with the lowest score from the active teams
                        while (activeTeams.Count > numTeams) {
                            int lowestScore = 255, lowestScoreTeamIndex = 0;
                            for (int i = 0; i < activeTeams.Count; i++) {
                                if ((State?.TeamScores?[activeTeams[i]] ?? 0) < lowestScore) {
                                    lowestScore = (State?.TeamScores?[activeTeams[i]] ?? 0);
                                    lowestScoreTeamIndex = i;
                                }
                            }
                            activeTeams.RemoveAt(lowestScoreTeamIndex);
                        }

                        // Add team indices if needed
                        if (activeTeams.Count < numTeams) {
                            for (int i = 0; i < 8; i++) {
                                if (!activeTeams.Contains(i)) { activeTeams.Add(i); }
                                if (activeTeams.Count == numTeams) { break; }
                            }
                        }

                    }

                    else {

                        // Handle invalid numTeams
                        if (numTeams < 2) { numTeams = 2; }
                        else if (numTeams > 8) { numTeams = 8; }

                        // We either have 1 team or more than 8
                        // if it's 8 just clear the list, if it's 1 we'll keep it
                        if (activeTeams.Count != 1) { activeTeams.Clear(); }

                        // add more if needed until we have enough indices to match numTeams
                        for (int i = 0; i < 8; i++) {
                            if (!activeTeams.Contains(i)) { activeTeams.Add(i); }
                            if (activeTeams.Count == numTeams) { break; }
                        }

                    }

                }
            }

            return GenerateBalancedTeamList(activeTeams, useRecordedStats);

		}

        public List<Tuple<PlayerInfo, int>> GenerateBalancedTeamList(List<int> teamIndices, bool useRecordedStats = false)
		{

            // Get a list of all active players and sort them by K/D Ratio, taking into account tracked K/D Ratio if available
            List<Tuple<PlayerInfo, double>> currentPlayerRatios = new List<Tuple<PlayerInfo, double>>();
            if (useRecordedStats) {
                PlayerStatsRecord match;
                if (InLobby) {
                    foreach (PlayerInfo player in State.Players) {
                        match = App.PlayerStatsRecords.Value.Find(x => x.Uid == player.Uid);
                        if (match == null) { currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, 0)); }
                        else { currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, match.KDRatio)); }
                    }
                }
                else {
                    foreach (PlayerInfo player in State.Players) {
                        match = App.PlayerStatsRecords.Value.Find(x => x.Uid == player.Uid);
                        if (match == null) { currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, player.KillDeathRatio)); }
                        else { currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, match.LiveKDRatio(player))); }
                    }
                }
            }
            else {
                if (InLobby) {
                    PlayerStatsRecord match;
                    foreach (PlayerInfo player in State.Players) {
                        match = LastMatchResults?.Find(x => x.Uid == player.Uid);
                        if (match != null) {
                            currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, match.KDRatio));
                        }
                        else {
                            currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, 0));
                        }
                    }
                }
                else {
                    PlayerStatsRecord match;
                    foreach (PlayerInfo player in State.Players) {
                        match = LastMatchResults?.Find(x => x.Uid == player.Uid);
                        if (match != null) {
                            currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, match.KDRatio + player.KillDeathRatio));
                        }
                        else {
                            currentPlayerRatios.Add(new Tuple<PlayerInfo, double>(player, player.KillDeathRatio));
                        }
                    }
                }
            }
            currentPlayerRatios = currentPlayerRatios.OrderBy(x => x.Item2).ToList();

            // Assign players to the active teams in order of K/D ratio
            // This is a very, very rough approach to balancing, and more robust logic will yield better results
            List<Tuple<PlayerInfo, int>> teamAssignments = new List<Tuple<PlayerInfo, int>>();
            for (int i = 0, j = 0; i < currentPlayerRatios.Count; i++, j++) {
                if (j == teamIndices.Count) { j = 0; }
                teamAssignments.Add(new Tuple<PlayerInfo, int>(currentPlayerRatios[i].Item1, teamIndices[j]));
            }

            return teamAssignments;

        }

        public void RecordMatchResults(ServerState serverState = null)
		{
            if (serverState == null) { serverState = State; }
            if ((serverState?.Players?.Count ?? 0) < 1) { return; }
            LastMatchResults.Clear();
            PlayerStatsRecord record;
			foreach (PlayerInfo player in State.Players) {
                record = new PlayerStatsRecord(player);
                if (record.IsValid) { LastMatchResults.Add(record); }
			}
		}

        public int GetTeamIndex(PlayerInfo player)
		{
            if (player == null) { return -1; }
            else { 
                if (ServerHookEnabled) {
                    return serverHookPlayerTeamIndices.TryGetValue(player.Uid ?? "null", out int teamIndex) 
                        ? teamIndex 
                        : player.Team;
                }
                else {
                    return player.Team;
				}
            }
		}

        //PLUS give these methods well-defined behavior in the result of a tie (which will be very common)

        public string GetMVPName()
		{
			switch (State?.GameVariantType ?? GameVariant.BaseGame.Unknown) {
				case GameVariant.BaseGame.Slayer: return GetHighestKDRName();
				case GameVariant.BaseGame.Oddball: return GetHighestScoreName();
				case GameVariant.BaseGame.KingOfTheHill: return GetHighestScoreName();
                case GameVariant.BaseGame.CaptureTheFlag: return GetHighestScoreName();
                case GameVariant.BaseGame.Assault: return GetHighestScoreName();
                case GameVariant.BaseGame.Juggernaut: return GetHighestScoreName();
                case GameVariant.BaseGame.Infection: return GetHighestScoreName();
                case GameVariant.BaseGame.VIP: return GetHighestScoreName();
                case GameVariant.BaseGame.Unknown: return GetHighestKDRName();
                case GameVariant.BaseGame.All: return GetHighestKDRName();
                default: return "";
			}
		}
        public string GetHighestKillsName()
		{
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderBy(x => x.Kills).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetLowestKillsName()
        {
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderByDescending(x => x.Kills).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetHighestScoreName()
        {
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderBy(x => x.Score).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetLowestScoreName()
		{
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderByDescending(x => x.Score).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetHighestDeathsName()
        {
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderBy(x => x.Deaths).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetLowestDeathsName()
        {
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderByDescending(x => x.Deaths).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetHighestKDRName()
		{
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderBy(x => x.KillDeathRatio).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetLowestKDRName()
        {
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderByDescending(x => x.KillDeathRatio).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetHighestBetrayalsName()
        {
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderBy(x => x.Betrayals).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        public string GetLowestBetrayalsName()
        {
            if ((State?.Players?.Count ?? 0) > 0) {
                return State.Players.OrderByDescending(x => x.Betrayals).ToArray()[0].Name ?? "";
            }
            else { return ""; }
        }
        /// <summary>
		/// Get a player's name based on their index in the player list.
		/// </summary>
		/// <returns>Returns the player's name if found, or an empty string.</returns>
		public string GetPlayerName(int index)
        {
            if (index >= 0 && index < (State?.Players?.Count ?? 0)) { return State.Players[index]?.Name ?? ""; }
            else { return ""; }
        }
        /// <summary>
        /// Get a player's name based on what "place" they are in. First place, second place, etc. (Not reliable in the event of ties)
        /// </summary>
        /// <returns>Returns the player's name if found, or an empty string.</returns>
        public string GetPlayerNameByPlace(int place)
        {
            if (place >= 1 && place <= (State?.Players?.Count ?? 0)) { return State.Players.OrderBy(x => x.Score).ToArray()[place - 1].Name ?? ""; }
            else { return ""; }
        }
        /// <summary>
        /// Get the name of the player in first place. (Not reliable in the event of ties)
        /// </summary>
        /// <returns>Returns the player's name if found, or an empty string.</returns>
        public string GetPlayerNameInFirstPlace()
        {
            if (State?.Players?.Count > 0) { return State.Players.OrderBy(x => x.Score).ToArray()[0].Name ?? ""; }
            else { return ""; }
        }
        /// <summary>
        /// Get the name of the player in last place. (Not reliable in the event of ties)
        /// </summary>
        /// <returns>Returns the player's name if found, or an empty string.</returns>
        public string GetPlayerNameInLastPlace()
        {
            if (State?.Players?.Count > 0) { return State.Players.OrderByDescending(x => x.Score).ToArray()[0].Name ?? ""; }
            else { return ""; }
        }

        #endregion

        #region ServerHook

        private Int32[] teamIndexAddresses = new Int32[16];
        public bool ShouldUseServerHook = true, ServerHookAttempted = false;
        public bool ServerHookEnabled = false; public bool attemptingServerHook = false;
        private ProcessMemory ServerMemory = null;
        private IntPtr MtnDewModuleBaseAddress, ServerProcessBaseAddress;
        private static readonly byte[] CustomShuffleMessageOriginalStringBytes = new byte[] { 0x6C, 0x69, 0x66, 0x65, 0x5F, 0x63, 0x79, 0x63, 0x6C, 0x65, 0x5F, 0x73, 0x74, 0x61, 0x74, 0x65, 0x5F, 0x68, 0x61, 0x6E, 0x64, 0x6C, 0x65, 0x72, 0x5F, 0x6D, 0x61, 0x74, 0x63, 0x68, 0x6D, 0x61, 0x6B, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x6E, 0x64, 0x5F, 0x61, 0x6E, 0x64, 0x5F, 0x61, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x65, 0x5F, 0x6D, 0x61, 0x74, 0x63, 0x68 };
        private const int CustomShuffleMessageMaxLength = 60;

        public void AttemptServerHook()
        {
            
            ServerHookEnabled = false;

            if (attemptingServerHook) { return; }
            attemptingServerHook = true;

            ServerHookEnabled = false;
			ServerHookAttempted = true;

            AppLog($"Attempting Server Hook...");

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses()/*("eldorado.exe (32 bit)")*/) {

                ServerProcessBaseAddress = IntPtr.Zero;
                MtnDewModuleBaseAddress = IntPtr.Zero;
                bool gotMtnDewModule = false;

                try {
                    
                    // Get Server Process                    
                    if (!(process?.MainWindowTitle ?? "").StartsWith("ElDewrito |")) { continue; }
                    
                    process.Refresh(); // for accuracy/stability of accessing modules

                    ServerProcessBaseAddress = process.MainModule.BaseAddress;

                    // ProcessMemory object streamlines working with the process memory
                    if (ServerMemory != null) { ServerMemory.Dispose(); ServerMemory = null; }
                    ServerMemory = new ProcessMemory(process.Id);
                    
                    // Get mtndew.dll Module                    
                    List<ProcessMemory.ModuleInfo> modules = ServerMemory.GetModuleInfos().ToList();
                    foreach (ProcessMemory.ModuleInfo module in modules) {
                        if (module.baseName == "mtndew.dll") {
                            MtnDewModuleBaseAddress = module.baseOfDll;
                            if (ServerProcessMatchesConnection(true)) {
                                AppLog($"Found Server Process");
                                gotMtnDewModule = true;
                                break;
                            }
                        }
                    }

                    if (gotMtnDewModule) {
                        try {
                            teamIndexAddresses[00] = 0x1A4ED58 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[01] = 0x1A503A0 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[02] = 0x1A519E8 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[03] = 0x1A53030 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[04] = 0x1A54678 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[05] = 0x1A55CC0 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[06] = 0x1A57308 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[07] = 0x1A58950 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[08] = 0x1A59F98 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[09] = 0x1A5B5E0 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[10] = 0x1A5CC28 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[11] = 0x1A5E270 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[12] = 0x1A5F8B8 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[13] = 0x1A60F00 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[14] = 0x1A62548 - (Int32)ServerProcessBaseAddress;
                            teamIndexAddresses[15] = 0x1A63B90 - (Int32)ServerProcessBaseAddress;
                        }
                        catch (Exception e) {
                            AppLog($"Failed to record player properties addresses.\n{e}");
                            return;
                        }


#if DEBUG               // One-time modification of shuffle teams function so that it works even if there's only one player
                        //ToggleShuffleTeamsPlayerCountRequirement();
#endif

                        // Modifies the shuffle teams function such that:
                        // if {0x90, 0x90} (nop, nop) is written at mtndew.dll+12390F
                        // the function skips random team assignment
                        // *but then corrects the bytes that were just nop'd back to their original values*
                        // so the *next* time you call the shuffle teams function, it works correctly

                        // originally this byte[] was hardcoded, but DWORD 2 is actually an address
                        // that can vary slightly, so that address needs to be calculated at runtime

                        // new byte[] { 0xEB, 0x0C, 0xC7, 0x05,
                        //          --> 0x0D, 0x39, 0xF7, 0x79, <-- address
                        //              0x85, 0xDB, 0xEB, 0x0D,
                        //              0xEB, 0x21, 0x90, 0x7E,
                        //              0x1E }

                        // The original bytecode for the shuffle teams function looks like this:
                        // https://i.imgur.com/iJa5hgZ.png
                        // The modified bytecode looks like this:
                        // https://i.imgur.com/co1KQQA.png

                        List<byte> shuffleModificationBytes = new List<byte>() { 0xEB, 0x0C, 0xC7, 0x05, 0x85, 0xDB, 0xEB, 0x0C, 0xEB, 0x21, 0x90, 0x7E, 0x1E };
                        shuffleModificationBytes.InsertRange(4, BitConverter.GetBytes((Int32)MtnDewModuleBaseAddress + 0x12390D));

                        if (true) {
                            AppLog("Skipping ShuffleTeams function modification.\n");
                        }
                        else {
                            try { ServerMemory.Write((Int32)MtnDewModuleBaseAddress + 0x12390F, shuffleModificationBytes.ToArray()); }
                            catch (Exception e) {
                                AppLog($"Failed to modify ShuffleTeams function.\n{e}");
                                return;
                            }
                        }

                        #region One-time modification of ShuffleTeams message from 'Teams have been shuffled.' to 'Teams updated.'
						
                        //byte[] teamShuffleMessagePointerBytes;

                        //// Record the bytes for the Teams-Shuffled-Message string pointer
                        //// mtndew.dll+0x123948: record pointer bytes
                        //try { teamShuffleMessagePointerBytes = ServerMemory.Read((Int32)MtnDewModuleBaseAddress + 0x123948, 4); }
                        //catch (Exception e) { 
                        //    AppLog($"Failed to acquire pointer to default teams-shuffled message string.\n{e}");
                        //    return; 
                        //}

                        //// Update protections on Teams shuffled string to ReadWrite
                        //try { ServerMemory.SetReadWriteProtection((IntPtr)BitConverter.ToInt32(teamShuffleMessagePointerBytes, 0), 25); }
                        //catch (Exception e) { 
                        //    AppLog($"Failed to set ReadWrite protections on memory region where teams-shuffled message string resides.\n{e}");
                        //    return; 
                        //}

                        //// modify string
                        //try { ServerMemory.Write(BitConverter.ToInt32(teamShuffleMessagePointerBytes, 0), Encoding.ASCII.GetBytes("Teams updated.           ")); }
                        //catch (Exception e) { 
                        //    AppLog($"Failed to modify default teams-shuffled string.\n{e}");
                        //    return; 
                        //}

                        //// Update protections on Teams shuffled string to ReadOnly
                        //try { ServerMemory.SetReadOnlyProtection((IntPtr)BitConverter.ToInt32(teamShuffleMessagePointerBytes, 0), 25); }
                        //catch (Exception e) { 
                        //    AppLog($"Failed to set ReadWrite protections on memory region where teams-shuffled message string resides.\n{e}");
                        //    return; 
                        //}

                        //// modify byte indicating the shuffle teams message length (2 places: 0x123946 and 0x1239C0)
                        //try { 
                        //    ServerMemory.Write((Int32)MtnDewModuleBaseAddress + 0x123946, new byte[] { 0x0E });
                        //    ServerMemory.Write((Int32)MtnDewModuleBaseAddress + 0x1239C0, new byte[] { 0x0E });
                        //}
                        //catch (Exception e) {
                        //    AppLog($"Failed to modify value dictating teams-shuffled message length.\n{e}");
                        //    return;
                        //}

                        #endregion

                        ServerHookEnabled = true;

                        AppLog("Found and modified process successfully");

                        break;
                    }
                    else {
                        try {
                            AppLog($"Found non-matching game instance");
                            ServerProcessBaseAddress = IntPtr.Zero;
                            MtnDewModuleBaseAddress = IntPtr.Zero;
                            if (ServerMemory != null) {
                                ServerMemory.Dispose();
                                ServerMemory = null;
                            }
                        }
                        catch (Exception e) {
                            AppLog($"Failed to reset variables needed to check next process.\n{e}");
                            return;
                        }
                        continue; 
                    }

                }
                catch (Exception e) {
                    AppLog($"Exception raised while checking processes:\n{e}");
                    continue; 
                }
            }

            if (ServerMemory == null) {
                AppLog("Failed: Unable to locate server process.\nThe app will continue running with ServerHook functionality disabled.");
                return;
            }
            else { AppLog($"Server Hook Successful"); }

        }

        public int ToggleShuffleTeamsPlayerCountRequirement()
		{

            if (!ServerHookEnabled) { return 2; }
            
            int playerCountRequiredToShuffleTeams = 2;

            // try to get current required player count value
            try { 
                byte[] result = ServerMemory.Read((Int32)MtnDewModuleBaseAddress + 0x1237BF, 1);
                if (result != null && result.Length == 1) { playerCountRequiredToShuffleTeams = result[0]; }
			}
            catch (Exception e) {
                AppLog($"Failed to read minimum player count value in ShuffleTeams function.\n{e}");
            }

            // toggle value from 1->2 or 2->1 or just set it to 2 if something went wrong
            if (playerCountRequiredToShuffleTeams == 1) { playerCountRequiredToShuffleTeams++; }
            else if (playerCountRequiredToShuffleTeams == 2) { playerCountRequiredToShuffleTeams--; }
            else { playerCountRequiredToShuffleTeams = 2; }

            SetShuffleTeamsPlayerCountRequirement(playerCountRequiredToShuffleTeams);

            return playerCountRequiredToShuffleTeams;

        }
        public void SetShuffleTeamsPlayerCountRequirement(int requiredPlayerCount)
		{

            if (!ServerHookEnabled) { return; }

            // ensure valid argument
            if (requiredPlayerCount < 1 || requiredPlayerCount > 16) { 
                requiredPlayerCount = 2; 
            }

            // write required player count value
            try { ServerMemory.Write((Int32)MtnDewModuleBaseAddress + 0x12390F, new byte[] { (byte)requiredPlayerCount }); }
            catch (Exception e) {
                AppLog($"Failed to modify shuffleTeams function to require at least {requiredPlayerCount} player(s) in the lobby.\n{e}");
            }

        }

        /// <summary>
        /// Disables the random team-reassignment of the shuffle teams function, allowing it to be used to manually assign players to specific teams by editing their team values in the server's application memory and then calling ShuffleTeams.
        /// </summary>
        /// <param name="autoreset">True by default. If true, the team randomization will be automatically re-enabled after 3 seconds.</param>
        public void DisableShuffleTeamsRandomization(bool autoreset = true)
		{
            if (!ServerHookEnabled) { return; }

            // if {0xEB, 0x0D} (jmp, offset) is written at mtndew.dll+12390F the function skips random team assignment
            try { ServerMemory.Write((Int32)MtnDewModuleBaseAddress + 0x12390F, new byte[] { 0xEB, 0x0D }); }
            catch (Exception e) {
                AppLog($"Failed to disable team-randomization in shuffleTeams function.\n{e}");
            }

            if (autoreset) {
                new Thread(new ThreadStart(() => {
                    Thread.Sleep(3000);
                    EnableShuffleTeamsRandomization();
                })).Start();
            }
        }
        public void EnableShuffleTeamsRandomization()
		{
            if (!ServerHookEnabled) { return; }

            // if {0x7E, 0x2D} (jle, offset) is written at mtndew.dll+12390F the function performs random team assignment normally
            try { ServerMemory.Write((Int32)MtnDewModuleBaseAddress + 0x12390F, new byte[] { 0x7E, 0x2D }); }
            catch (Exception e) {
                AppLog($"Failed to disable team-randomization in shuffleTeams function.\n{e}");
            }
        }

        public void GetPlayerTeams()
        {
            if (!ServerHookEnabled || State == null) { return; }
            //Console.WriteLine($"{LogPrefix}.GetPlayerTeams() Start");
            try {

                byte[] result; bool teamUpdate = false;
                StringBuilder sb = new StringBuilder();

                // Use the UID value from the PlayerInfo parameter to determine the relevant memory address
                for (int i = 0; i < 16; i++) {

                    sb.Clear();

                    // Read the UID value from the PlayerProperties in memory
                    result = ServerMemory.Read((Int32)ServerProcessBaseAddress + (teamIndexAddresses[i] + 0x10), 8);

                    // The hex bytes of the UID are reversed, so flip 'em
                    for (int j = result.Length - 1; j >= 0; j--) { sb.Append(result[j].ToString("X2")); }

                    // get the non-client team index from this PlayerProperties block
                    result = ServerMemory.Read((Int32)ServerProcessBaseAddress + teamIndexAddresses[i] + 0x3C, 1);

                    // Update serverHookPlayerTeamIndices for Scoreboard use
                    if (serverHookPlayerTeamIndices.TryGetValue(sb.ToString().ToLowerInvariant(), out int teamIndex)) {
                        if (teamIndex != result[0]) { teamUpdate = true; } // existing player is on different team
                    }
                    else { teamUpdate = true; } // new player joined
                    serverHookPlayerTeamIndices[sb.ToString().ToLowerInvariant()] = result[0];

                }

                if (teamUpdate) { Scoreboard.RegenerateScoreboardImage = true; }

            }
            catch (System.ComponentModel.Win32Exception e32) { ServerHookEnabled = false; AppLog($"Failed:\n{e32}"); }
            catch (Exception e) { ServerHookEnabled = false; AppLog($"Failed:\n{e}"); }
            //Console.WriteLine($"{LogPrefix}.ServerHookPlayerTeamIndices:\n{serverHookPlayerTeamIndices.ToEntriesString()}");
        }

        public void SetPlayerTeam(PlayerInfo player, int teamIndex)
        {
            SetPlayerTeam(null, new Tuple<PlayerInfo, int>(player, teamIndex));
        }
        
        public void SetPlayerTeam(string customMessage = null, params Tuple<PlayerInfo, int>[] args)
        {

            //Console.WriteLine($"{LogPrefix}.SetPlayerTeam() Start");

            // If ServerHook not enabled, return
            if (!ServerHookEnabled) { 
                AppLog($"Failed: ServerHook not active."); 
                return; 
            }

            // If there are no players on the server, return
            if (!HasPlayers) {
                AppLog($"Failed: No players present.");
                return;
            }

            try {

                Dictionary<int, Tuple<PlayerInfo, int>> playerUpdates = new Dictionary<int, Tuple<PlayerInfo, int>>();
                StringBuilder sb = new StringBuilder();
                byte[] result;

                // Use the UID value from the PlayerInfo parameter to determine the relevant memory address
                for (int i = 0; i < 16; i++) {

                    sb.Clear();

                    // Read the UID value from the PlayerProperties in memory
                    result = ServerMemory.Read((Int32)ServerProcessBaseAddress + (teamIndexAddresses[i] + 0x10), 8);

                    // The hex bytes of the UID are reversed, so flip 'em
                    for (int j = result.Length - 1; j >= 0; j--) { sb.Append(result[j].ToString("X2")); }

                    foreach (Tuple<PlayerInfo, int> arg in args) {
                        if (sb.ToString().ToLowerInvariant() == arg.Item1.Uid) {
                            // add update entry for this PlayerProperties index
                            playerUpdates.Add(i, arg); break;
                        }
                    }

                    // If there is one update entry per player, we've found all the addresses we need
                    if (playerUpdates.Count == args.Length) { break; }

                }

                // We should have one updateIndex per player arg, if not, fail
                if (playerUpdates.Count != args.Length) {
                    if (args.Length == 1) { PrintToConsole("SetPlayerTeam Failed: Unabled to determine PlayerProperties Index. Operation aborted."); }
                    else { PrintToConsole("SetPlayerTeams Failed: Unable to determine all PlayerProperties Indices. Operation aborted."); }
                    Console.WriteLine($"{LogPrefix}.SetPlayerTeam() Update/PlayerCount Mismatch Abort");
                    return;
                }

                // Modify each player's team index -
                // The modified ShuffleTeams function will
                // inform all clients that someone has changed teams

                foreach (int addressIndex in playerUpdates.Keys.ToList()) {
                    // modify the team index byte in the ClientPlayerProperties
                    ServerMemory.Write(
                        (Int32)ServerProcessBaseAddress + teamIndexAddresses[addressIndex],
                        new byte[] { (byte)playerUpdates[addressIndex].Item2 }
                    );
                }

                #region Obsolete broken flag method that utilized a "permanent" modification of the shuffle teams function
                // 'flag' the shuffleTeams bytecode so it skips assigning random team indices the next time it's called
                // the previously modified bytecode will revert the two 'flag' bytes back to their original values
                // before skipping the random assignment, so shuffleTeams functions normally the next time it's called

                // mtndew.dll+0x12390F: update from 7E 2D -> 90 90
                // // ServerMemory.Write((Int32)MtnDewModuleBaseAddress + 0x12390F, new byte[] { 0x90, 0x90 });
                #endregion

                DisableShuffleTeamsRandomization();

				// Call Server.ShuffleTeams
				RconCommandQueue.Enqueue(RconCommand.Command("Server.ShuffleTeams"));

            }
            catch (Exception e) { 
                AppLog($"Failed:\n{e}");
                ServerHookEnabled = false; 
            }

            //Console.WriteLine($"{LogPrefix}.SetPlayerTeam() End");

        }

		/// <summary>
		/// Attempts to read GameType information from active memory and construct a GameVariant. Returns null if the operation fails for any reason.
		/// </summary>
		public GameVariant GetCurrentGameType()
        {
            if (!ServerHookEnabled) { return null; }

            Console.WriteLine($"{LogPrefix}.GetCurrentGameType() Start");

            try {

                byte[] result;

                // Base GameType Enum Value: (int)ServerProcessBaseAddress + 0x181EE9C -> 4 bytes int32
                result = ServerMemory.Read((int)ServerProcessBaseAddress + 0x181EE9C, 4);

                GameVariant.BaseGame baseGame = GameVariant.BaseGame.Unknown;   //enum GameType : int32_t
                switch (BitConverter.ToInt32(result, 0)) {
                    // case 0: break;                                           //eGameTypeBase = 0,
                    case 1: baseGame = GameVariant.BaseGame.CaptureTheFlag; break;//eGameTypeCTF = 1,
                    case 2: baseGame = GameVariant.BaseGame.Slayer; break;      //eGameTypeSlayer = 2,
                    case 3: baseGame = GameVariant.BaseGame.Oddball; break;     //eGameTypeOddball = 3,
                    case 4: baseGame = GameVariant.BaseGame.KingOfTheHill; break;//eGameTypeKOTH = 4,
                                                                                 //eGameTypeForge = 5?,
                    case 6: baseGame = GameVariant.BaseGame.VIP; break;         //eGameTypeVIP = 6,
                    case 7: baseGame = GameVariant.BaseGame.Juggernaut; break;  //eGameTypeJuggernaut = 7,
                                                                                //eGameTypeTerritories = 8?,
                    case 9: baseGame = GameVariant.BaseGame.Assault; break;     //eGameTypeAssault = 9,
                    case 10: baseGame = GameVariant.BaseGame.Infection; break;  //eGameTypeInfection = 10 (0xA),
                    default: baseGame = GameVariant.BaseGame.Unknown; break;    //eGameTypeCount
                }

                // Variant Name: (int)ServerProcessBaseAddress + 0x181EEA8 -> 16 bytes ascii
                result = ServerMemory.Read((int)ServerProcessBaseAddress + 0x181EEA8, 16);
                string variantName = Encoding.ASCII.GetString(result).RemoveAll('\0').Trim();

                // Variant Display Name: (int)ServerProcessBaseAddress + 0x181EED0 -> 32 bytes unicode
                result = ServerMemory.Read((int)ServerProcessBaseAddress + 0x181EED0, 32);
                string variantDisplayName = Encoding.Unicode.GetString(result).RemoveAll('\0').Trim();

                // Variant Description: (int)ServerProcessBaseAddress + 0x181EEF0 -> 128 bytes ascii
                result = ServerMemory.Read((int)ServerProcessBaseAddress + 0x181EEF0, 128);
                string variantDescription = Encoding.ASCII.GetString(result).RemoveAll('\0').Trim();

                // Variant Author: (int)ServerProcessBaseAddress + 0x181EF70 -> 16 bytes ascii
                result = ServerMemory.Read((int)ServerProcessBaseAddress + 0x181EF70, 16);
                string variantAuthor = Encoding.ASCII.GetString(result).RemoveAll('\0').Trim();

                Console.WriteLine($"{LogPrefix}.GetCurrentGameType() Success [{baseGame}: {variantName} ({variantDisplayName})]");

                return new GameVariant(baseGame, variantName, variantDisplayName, variantDescription, variantAuthor);

            }
            catch (System.ComponentModel.Win32Exception) { 
                ServerHookEnabled = false;
            }
            catch (Exception e) {
                ServerHookEnabled = false;
                PrintToConsole("GetCurrentGameType Failed: Failed to retrieve gametype data from memory.\n" + e.ToString());
            }

            Console.WriteLine($"{LogPrefix}.GetCurrentGameType() Failed");

            return null;

        }

        public bool ServerProcessMatchesConnection(bool suppressDetailLogging = false)
        {
            try {

                if (!ServerHookEnabled && !attemptingServerHook) { return false; }

                string status = GetServerStatusPacketStringFromMemory();
                
                if (string.IsNullOrWhiteSpace(status)) { return false; }

                // trim the first bit of the packet json
                status = status.TrimStart("{\"name\":\"".Length); //{"name":"

                // get the name value, terminating at the first "
                status = status.Substring(0, status.IndexOf('\"'));

                // match
                if (!string.IsNullOrWhiteSpace(status) && status == State?.Name) { 
                    return true;
                }

                if (!suppressDetailLogging) {
                    PrintToConsole($"Connection Server Name: {State?.Name ?? ""}");
                    PrintToConsole($"ServerHook Server Name: {status ?? ""}");
                }

            }
            catch (Exception e) { PrintToConsole($"ServerProcessMatchesConnection Failed: Unable to complete verification.\n{e}"); }
            return false;
        }

        /// <summary>
        /// Attempts to read the server status packet string from the server application's memory.
        /// <br>If the operation fails, the returned string value will be null, unless <paramref name="returnExceptionString"/> is set to true.</br>
        /// </summary>
        /// <param name="returnExceptionString">If true, exception information will be returned as a string if the operation fails, otherwise the returned string value is null.</param>
        public string GetServerStatusPacketStringFromMemory(bool returnExceptionString = false)
        {

            //mtndew.dll+40C700 -> pointer(4 bytes), a pointer to the server status packet string
            //mtndew.dll+40C710 -> integer(4 bytes), the number of bytes in the server status packet string
            // the server status packet string is UTF8, so byte-length can differ from string length, e.g. 🦀
            if (!ServerHookEnabled && !attemptingServerHook) { return "GetServerStatusStringFromMemory Disabled."; }

            try {
                int statusPacketByteCount =
                    BitConverter.ToInt32(ServerMemory.Read((int)MtnDewModuleBaseAddress + 0x40C710, 4), 0);
                int statusPacketStringAddress = 
                    BitConverter.ToInt32(ServerMemory.Read((int)MtnDewModuleBaseAddress + 0x40C700, 4), 0);
                return Encoding.UTF8.GetString(
                    ServerMemory.Read(statusPacketStringAddress, statusPacketByteCount)
                );
            }
            catch (Exception e) {
                if (returnExceptionString) { return $"GetServerStatusStringFromMemory Failed.\n{e}"; }
            }
            return null;

        }

#endregion

        #region Events

        public event EventHandler<MatchBeginEndArgs> MatchBeganOrEnded;
        public event EventHandler<ChatEventArgs> ChatMessageReceived;
		public event EventHandler<PlayerJoinLeaveEventArgs> PlayerJoined;
        public event EventHandler<PlayerJoinLeaveEventArgs> PlayerLeft;
        public event EventHandler<PlayerJoinLeaveEventArgs> PlayerCountChanged;

        public event EventHandler<PlayerTeamChangeEventArgs> PlayerChangedTeams;

        public void InvokeMatchBeganOrEnded(MatchBeginEndArgs e)
		{
            MatchBeganOrEnded(this, e);
		}
        public void OnMatchBeginOrEnd(object sender, MatchBeginEndArgs e)
        {
            if (e.MatchBegan)
            {

#region Prune Reply-Tracking Dictionary

                // Copy all player UIDs we are tracking reply names for to a list
				List<string> replyUids = ReplyCommandPlayers.Keys.ToList();

                // If that player is still present in the match, remove their UID from the list
				for (int i = 0; i < replyUids.Count; i++) {
                    if (State.Players.Any(x => x.Uid == replyUids[i])) {
                        replyUids.RemoveAt(i); i--;
					}
				}

                // We now have a list of UIDs with no corresponding player in the match, so remove them from the tracking Dictionary
				foreach (string absentUid in replyUids) {
                    if (ReplyCommandPlayers.ContainsKey(absentUid ?? "")) { ReplyCommandPlayers.Remove(absentUid); }
				}

#endregion

			}
			else
            {
                // Manage match queue
                if (Settings.ServerMatchMode == ServerSettings.MatchMode.Queue) {
                    LoadNextMatchFromQueue();
                }
            }
        }
        public void OnMessageRuntimeCommands(object sender, ChatEventArgs e)
        {
            RuntimeCommand.TryRunCommand(e.Message, e.SendingPlayer, e.Connection, e.ChatMessage);
        }
        public void OnPlayerTeamChanged(object sender, PlayerTeamChangeEventArgs e)
        {
            if (IsDisplayedCurrently) { Scoreboard.RegenerateScoreboardImage = true; }
            if (State.Teams == false) { return; }
            if (State.Status == StatusStringInLobby) { return; }
            if (State.Status == StatusStringLoading) { return; }

            if (State.Status != StatusStringInGame) { AppLog($"TEAM CHANGE: {State.Status}"); }

            AppLog($"{e.PlayerState.Name} changed teams from {App.TeamNames[e.PlayerStatePrevious.Team]} team to { App.TeamNames[e.PlayerState.Team]} team.");

        }
        public void OnChatMessageReceived(ChatEventArgs e)
        {
            //AppLog($"{e.SendingPlayer?.Name ?? "SERVER"}: {e.ChatMessage.Text}");
            ChatMessageReceived?.Invoke(e.Connection, e);
        }
        public void OnPlayerJoined(PlayerJoinLeaveEventArgs e)
        {

            LastPlayerJoinEventArgs = e;
            
            PlayerJoined?.Invoke(e.Connection, e);
            PlayerCountChanged?.Invoke(e.Connection, e);

            if (IsDisplayedCurrently && App.settings_PlaySoundOnPlayerJoin.Value)
            {
                using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerJoinSound))
                {
                    //// Set sound that will be played
                    //if (App.settings_PlayerJoinSoundPath == "")
                    //{
                    //    // Use default sound
                    //    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerJoinSound);
                    //}
                    //else
                    //{
                    //    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerJoinSound);
                    //}
                    player.Play();
                }
            }

            // Only message joining players if RCON is already connected
            if (RconConnected) {
                
                // Tell joining players how to access custom commands
                Whisper(e.Player.Name, "Use \"!commands\" to see all commands");
                
                // Tell joining players about AutoTranslation
                if (CanTranslate && Settings.AutoTranslateChatMessages) {
                    Whisper(e.Player?.Name, "AutoTranslation of chat messages is enabled on this server.");
                    Whisper(e.Player?.Name, "To prevent auto-translation of a chat messages, type '&' as the first character in the message.");
                }

            }

        }
        public void OnPlayerLeft(PlayerJoinLeaveEventArgs e)
        {
            PlayerLeft?.Invoke(e.Connection, e);
            PlayerCountChanged?.Invoke(e.Connection, e);

            if (IsDisplayedCurrently && App.settings_PlaySoundOnPlayerLeave.Value)
            {
                using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerLeaveSound))
                {
                    // Set sound that will be played
                    //if (App.settings_PlayerLeaveSoundPath == "")
                    //{
                    //    // Use default sound
                    //    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerLeaveSound);
                    //}
                    //else
                    //{
                    //    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerLeaveSound);
                    //}
                    player.Play();
                }
            }

        }
        public void OnPlayerCountChanged(PlayerJoinLeaveEventArgs e)
        {
            PlayerCountChanged?.Invoke(e.Connection, e);
        }

        public class PlayerJoinLeaveEventArgs : EventArgs
        {

            public PlayerJoinLeaveEventArgs(PlayerInfo player, bool playerJoined, int newPlayerCount, string eventTime, Connection connection)
            {
                Player = player;
                PlayerJoined = playerJoined;
                NewPlayerCount = newPlayerCount;
                EventTime = eventTime;
                Connection = connection;
            }

            public PlayerInfo Player { get; set; }
            public bool PlayerJoined { get; set; }
            public int NewPlayerCount { get; set; }
            public string EventTime { get; set; }
            public Connection Connection { get; set; }

        }

        public class PlayerTeamChangeEventArgs : EventArgs
        {
            
            public PlayerTeamChangeEventArgs(PlayerInfo playerInfo, PlayerInfo previousPlayerInfo, Connection connection)
            {
                PlayerState = playerInfo;
                PlayerStatePrevious = previousPlayerInfo;
                EventTime = DateTime.Now;
                Connection = connection;
            }

            public DateTime EventTime { get; set; }
            public Connection Connection { get; set; }
            public PlayerInfo PlayerState { get; set; }
            public PlayerInfo PlayerStatePrevious { get; set; }
            public int TeamJoined   { get { return PlayerState.Team; } }
            public int TeamLeft     { get { return PlayerStatePrevious.Team; } }
            public int TeamLeftPlayerCount    { get { return Connection.State.Players.Count(x => x.Team == PlayerStatePrevious.Team); } }
            public int TeamJoinedPlayerCount  { get { return Connection.State.Players.Count(x => x.Team == PlayerState.Team); } }

        }

        public class ChatEventArgs : EventArgs
        {

            public ChatEventArgs(string message, PlayerInfo sender, string eventTime, Connection connection, Message chatMessage = null)
            {
                SendingPlayer = sender;
                Message = message;
                EventTime = eventTime;
                Connection = connection;
                ChatMessage = chatMessage;
            }

            public string Message { get; set; }
            public PlayerInfo SendingPlayer { get; set; }
            public string EventTime { get; set; }
            public Connection Connection { get; set; }
            public Message ChatMessage { get; set; }

        }

        public class MatchBeginEndArgs : EventArgs
        {

            public bool MatchBegan { get; set; }
            public DateTime EventTime { get; set; }
            public Connection Connection { get; set; }

            public MatchBeginEndArgs(bool matchIsBeginning, Connection connection)
            {
                MatchBegan = matchIsBeginning;
                Connection = connection;
                EventTime = DateTime.Now;
            }

        }

        public class RankEmblemData
        {

            //  "0":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=0&2=16&3=2&fi=39&bi=39&fl=0&m=1"
            // ,"1":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=120&0=5&1=2&2=1&3=3&fi=58&bi=35&fl=1&m=1"
            // , ...

            [JsonProperty("r")]
            public int Rank { get; set; } = 0;

            [JsonProperty("e")]
            public string Emblem { get; set; } = "";

        }


        #endregion
        

    }

}
