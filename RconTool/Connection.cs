#undef DEBUG

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WebSocketSharp;
using Newtonsoft.Json.Serialization;
using System.Drawing;

namespace RconTool
{

    public class Connection
    {

		#region Properties and Variables

		public ServerState State { get; set; } = new ServerState();
        public ServerSettings Settings { get; set; } = null;

        //TODO Add option for persistent logs per-connection
        private string consoleText = "";
        private string chatText = "";
        private string joinLeaveLogText = "";

        public const string StatusStringInGame = "InGame";
        public const string StatusStringInLobby = "InLobby";
        public const string StatusStringLoading = "Loading";

        public const int ServerMessageDelay = 25;

        public string ServerStateJson { get; set; } = "";

        public bool AcceptJsonMessage { get; set; } = false;
        public bool ServerStatusAvailable { 
            get { return !string.IsNullOrWhiteSpace(ServerStateJson); }
        }

        public bool IsDisplayedCurrently { get { return App.currentConnection == this; } }

        /// <summary>
        /// When using the dynamic voting file, this is the queue for matches that will be played.
        /// <para>When the queue is empty, the server will switch to using the most recent voting file.</para>
        /// </summary>
        public Queue<MatchInfo> MatchQueue { get; set; } = new Queue<MatchInfo>();
        public readonly object MatchQueueLock = new object();
        public string NextQueuedMatch { get; set; } = "";

        public List<GameVariant> GameVariants {
            get {
                if (!GameVariantsLoaded) { LoadGameVariants(); }
                return gameVariants;
            }
            set { gameVariants = value; }
        }
        private List<GameVariant> gameVariants = new List<GameVariant>();
        public List<MapVariant> MapVariants {
            get {
                if (!MapVariantsLoaded) { LoadMapVariants(); }
                return mapVariants;
            }
            set { mapVariants = value; }
        }
        private List<MapVariant> mapVariants = new List<MapVariant>();
        public bool MapVariantsLoaded { get; set; } = false;
        public bool GameVariantsLoaded { get; set; } = false;
        public DateTime LastMapVariantLoadTime { get; set; }
        public DateTime LastGameVariantLoadTime { get; set; }

        private PlayerJoinLeaveEventArgs LastPlayerJoinEventArgs { get; set; } = null;
        public List<Tuple<int, string>> RankAndEmblemDataPairs { get; set; } = new List<Tuple<int, string>>();
        
        public Thread RunTimedCommandsThread;
        public Thread MonitorServerStatusThread;

        #region Rcon Variables

        /// <summary>
        /// The thread which manages the Rcon connection.
        /// </summary>
        public Thread RconConnectionThread;
        /// <summary>
        /// The WebSocket which facilitates the Rcon connection.
        /// </summary>
        public WebSocket RconWebSocket;
        
        /// <summary>
        /// Determines whether an Rcon connection to the current server will be created and maintained.
        /// </summary>
        public bool RconEnabled { get; set; } = true;
        /// <summary>
        /// Indicates whether the current RconWebSocket connection is active (connected).
        /// </summary>
        public bool RconConnected { get { return (RconWebSocket?.IsAlive ?? false); } }

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

        /// <summary>
        /// Add commands here to have them sent sequentially, waiting for server response messages in between commands.
        /// </summary>
        private Queue<string> commandQueue = new Queue<string>();
        /// <summary>
        /// Flag for automatic activation and deactivation of the command queue.
        /// </summary>
        private bool commandQueueProcessing = false;

        public object RconThreadLock = new object();

		#endregion

        #region Vote Properties
                
        public bool VoteInProgress { get; set; } = false;
        public int VoteDuration { get; set; } = 30;
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

        public List<Tuple<string, bool>> UidVotePairs { get; set; } = new List<Tuple<string, bool>>();
        private readonly object uidVotePairsLock = new object();

		#endregion

		#endregion

		public Connection(ServerSettings settings)
        {

            Settings = settings;
            
            if (!App.connectionList.Contains(this)) { App.connectionList.Add(this); }

            //TODO make all of these files persistent
            consoleText = "";
            chatText = "";
            joinLeaveLogText = "";

#if !DEBUG
            RconConnectionThread = new Thread(new ThreadStart(ManageRconConnection));
            RconConnectionThread.Start();

            MonitorServerStatusThread = new Thread(new ThreadStart(MonitorServerStatus));
            MonitorServerStatusThread.Start();
#endif

            PlayerChangedTeams += OnPlayerTeamChanged;
            ChatMessageReceived += OnMessageRuntimeCommands;
            MatchBeganOrEnded += OnMatchBeginOrEnd;

            if (Settings.AuthorizedUIDs == null) { settings.AuthorizedUIDs = new List<string>(); }

            RunTimedCommandsThread = new Thread(new ThreadStart(RunTimedCommands));
            RunTimedCommandsThread.Start();

            App.UpdateServerSelectDropdown();

        }

        private void ManageRconConnection()
        {

            Thread.Sleep(2000);

            bool failed = false;

        ConnectWhenReady:

            while ((RconAutoReconnectEnabled == false && RconReconnect == false) || RconEnabled == false)
            {
                Thread.Sleep(1000);
            }
            
            if (RconReconnect) { App.Log("Attempting Rcon Connection...", this); }

            // Reset Reconnect Flag
            RconReconnect = false;

            // Attempt Connection
            lock (RconThreadLock)
            {

                #region Attempt Close WebSocket
                
                // Handle invalid Rcon Address
                if (!Settings.RconWebSocketAddressIsValid)
                {
                    RconEnabled = false;
                    App.ShowMessageBox("An Rcon connection was attempted with invalid connection settings. " +
                        "Rcon Connection has been disabled. Please verify the Server IP and Rcon Port, " +
                        "then re-enable Rcon and 'Reset Rcon Connection' from the file menu", "Rcon Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning
                    );
                    goto ConnectWhenReady;
                }

                if (RconWebSocket != null)
                try
                {
                    RconWebSocket.Close();
                }
                catch (AggregateException e)
                {
                    App.Log("Error while resetting Rcon Websocket connection. RconWebSocket.Close() Error: " + e.Message, this);
                }

                #endregion

                #region Attempt Connection

                RconWebSocket = new WebSocket(Settings.RconWebSocketAddress, ServerSettings.RconProtocolString);

                RconWebSocket.OnOpen += OnOpen;
                RconWebSocket.OnMessage += OnMessage;
                RconWebSocket.OnMessage += ManageCommandQueue;
                RconWebSocket.OnClose += OnClose;
                RconWebSocket.OnError += OnError;

                DateTime connectionAttemptStartTime = DateTime.Now;
                if (RconWebSocket.ReadyState != WebSocketState.Open)
                {
                    try
                    {
                        RconWebSocket.Connect();
                    }
                    catch (Exception e)
                    {
                        App.Log("Error while attempting Rcon WebSocket connection: " + e.Message, this);
                        failed = true;
                    }
                }
                if (failed == false && RconWebSocket.ReadyState != WebSocketState.Open)
                {
                    Thread.Sleep(5000);
                }
                if (failed == false && RconWebSocket.ReadyState != WebSocketState.Open)
                {
                    failed = true;
                }

                if (failed)
                {

                    LogRconConnectionError();

                    if (RconAutoReconnectEnabled)
                    {
                        failed = false;
                        Thread.Sleep(RconAutoAttemptReconnectInterval * 1000);
                    }
                    else
                    {
                        App.ShowMessageBox(
                            "Rcon Connection Failed. Rcon Auto-Reconnect is disabled, but you may attempt to " +
                            "restart the Rcon connection by selecting Reset Rcon Connection from the File menu.",
                            "Rcon Connection Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }

                    goto ConnectWhenReady;

                }

                #endregion

            }

            // Connected - Monitor for any changes            

            bool gotoConnectWhenReady = false, isAlive = false;
            Thread.Sleep(1000);

            while (true)
            {
                
                // Check should reset
                if (RconReconnect)
                {
                    App.Log("Resetting Rcon Connection", this);
                    gotoConnectWhenReady = true;
                }

                // Check connection alive
                lock (RconThreadLock) { isAlive = RconWebSocket.IsAlive; }
                
                if (!isAlive)
                {
                    LogRconConnectionError(true);
                    if (RconAutoReconnectEnabled)
                    {
                        Thread.Sleep(RconAutoAttemptReconnectInterval * 1000);
                    }
                    gotoConnectWhenReady = true;
                }

                if (gotoConnectWhenReady) { break; }

                Thread.Sleep(1000);

            }

            goto ConnectWhenReady;

        }
        public void ResetRconConnection()
        {
            RconReconnect = true;
        }
        public void DeleteConnection()
        {

            MonitorServerStatusThread.Abort();

            lock (RconThreadLock)
            {
                RconWebSocket.Close();
            }

            Thread.Sleep(50);

            RconConnectionThread.Abort();
            RunTimedCommandsThread.Abort();
            

        }
        private void LogRconConnectionError(bool forceLog = false)
        {

            if (RconEnabled == false) { return; }

            if ((DateTime.Now - lastRconDisconnectedLogTime) >= TimeSpan.FromMinutes(RconDisconnectedLogInterval) || forceLog)
            {
                if (RconAutoReconnectEnabled)
                {
                    App.Log("Rcon Connection Failed | Will automatically attempt to reconnect every " + RconAutoAttemptReconnectInterval + " seconds.", this);
                }
                else
                {
                    App.ShowMessageBox(
                        "Rcon Connection Failed. Rcon Auto-Reconnect is disabled, but you may attempt to " +
                        "restart the Rcon connection by selecting Reset Rcon Connection from the File menu.",
                        "Rcon Connection Failed",
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
                                    if (RconConnected)
                                    {
                                        SendToRcon(commandString);
                                    }
                                }
                            }
                        }
                        else if (command.ConditionType == ToolCommand.Type.Daily)
                        {
                            if (DateTime.Now.Hour == command.RunTime && command.triggered == false)
                            {
                                command.triggered = true;
                                if (RconConnected)
                                {                                    
                                    foreach (string commandString in command.CommandStrings)
                                    {
                                        SendToRcon(commandString);
                                    }
                                }
                            }
                            if (DateTime.Now.Hour != command.RunTime && command.triggered == true)
                            {
                                command.triggered = false;
                            }
                        }

                    }

                    App.form.Invalidate();
                    Thread.Sleep(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Timed Command Error: " + e.Message);
                }
            }

        }

        public async void MonitorServerStatus()
        {
            string status = "";
            while (true)
            {

                Thread.Sleep(1000);

                // Attempt to download server status string...
                if (Settings.ServerInfoAddressIsValid)
                {
                    using (WebClient webClient = new WebClient())
                    {
                        try
                        {
                            webClient.Encoding = Encoding.UTF8;
                            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Base64Encode("dorito:" + Settings.ServerPassword ?? ""));
                            status = await webClient.DownloadStringTaskAsync(Settings.ServerInfoAddress);
                        }
                        catch (Exception e)
                        {
                            //App.Log("Server Status Request Failed | " + e.Message, this);
                            continue;
                        }
                    }
                }

                ProcessServerStatus(status);

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
                App.Log("Error decoding Server Status String: " + e.Message);
                return;
            }

            if (!newState.IsValid)
            {
                try
                {
                    App.Log("Invalid Server State: " + JsonConvert.SerializeObject(newState));
                }
                catch (Exception jse)
                {
                    App.Log("Invalid Server State | Failed to convert server state to JSON for logging: " + jse.Message);
                }
                return;
            }

            // Retrieve player emblems as needed
            if (RankAndEmblemDataPairs.Count > 0 &&
                newState.Players.Count == State.Players.Count &&
                newState.Players.Count >= RankAndEmblemDataPairs.Count)
            {

                PlayerInfo match;
                for (int i = 0; i < RankAndEmblemDataPairs.Count; i++)
                {
                    match = newState.Players[i].FindMatchingPlayer(State.Players);
                    if (match != null && match.Emblem == null)
                    {
                        newState.Players[i].Rank = RankAndEmblemDataPairs[i].Item1;
                        using (WebClient webClient = new WebClient())
                        {
                            byte[] data = webClient.DownloadData(RankAndEmblemDataPairs[i].Item2);
                            using (MemoryStream mem = new MemoryStream(data))
                            {
                                newState.Players[i].Emblem = new Bitmap(mem);
                            }
                        }
                    }
                }

                RankAndEmblemDataPairs.Clear();

            }

            State.Update(newState, this);
            if (IsDisplayedCurrently) { Scoreboard.RegenerateScoreboardImage = true; }

        }

        private List<Tuple<int, string>> ParseRankAndEmblemJson(string rankAndEmblemsJson)
        {

            List<string> parts = rankAndEmblemsJson.Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            /*
             * {"0":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=0&2=16&3=2&fi=39&bi=39&fl=0&m=1"
             * ,"1":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=120&0=5&1=2&2=1&3=3&fi=58&bi=35&fl=1&m=1"
             * ,"2":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=0&2=9&3=10&fi=88&bi=0&fl=0&m=1"
             * ,"3":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=2&2=3&3=2&fi=16&bi=1&fl=1&m=1"
             * ,"4":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=9&2=9&3=9&fi=0&bi=1&fl=1&m=1"
             * ,"5":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=5&1=1&2=1&3=1&fi=54&bi=16&fl=1&m=1"
             */

            List<Tuple<int, string>> data = new List<Tuple<int, string>>();

            int pIndex = 0, rIndex = 0;

            foreach (string part in parts)
            {

                string p = part.Substring(2, part.Length - 2);
                if (char.IsDigit(p[0]))
                {
                    // players 10 - 15 (0 based array so never 16)
                    if (char.IsDigit(p[1]))
                    {
                        int.TryParse(p.Substring(0, 2), out pIndex);
                        p = p.Substring(1, p.Length - 1);
                    }
                    // players 0 - 9
                    else
                    {
                        int.TryParse(p.Substring(0, 1), out pIndex);
                    }
                }
                else { /*failed*/ return data; }

                // at this point p looks like
                //0":{"r":0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=0&2=16&3=2&fi=39&bi=39&fl=0&m=1"
                p = p.Substring(8, p.Length - 8);

                // now p looks like 
                //0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=0&2=16&3=2&fi=39&bi=39&fl=0&m=1"
                if (char.IsDigit(p[0]))
                {
                    // rank 10 - 99 (idk how many ranks there are but whatever)
                    if (char.IsDigit(p[1]))
                    {
                        int.TryParse(p.Substring(0, 2), out rIndex);
                        p = p.Substring(1, p.Length - 1);
                    }
                    // ranks 0 - 9
                    else
                    {
                        int.TryParse(p.Substring(0, 1), out rIndex);
                    }
                }
                else { /*failed*/ return data; }

                // at this point p looks like
                //0,"e":"http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=0&2=16&3=2&fi=39&bi=39&fl=0&m=1"
                p = p.Substring(7, p.Length - 8);

                // now p looks like
                //http://new.halostats.click/emblem/emblem.php?s=100&0=0&1=0&2=16&3=2&fi=39&bi=39&fl=0&m=1

                data.Add(new Tuple<int, string>(rIndex, p));

            }

            return data;

        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        #region Rcon WebSocket Events

        public void OnClose(object sender, CloseEventArgs e)
        {
            //App.Log("Rcon WebSocket Closed", this);
        }

        public void OnMessage(object sender, MessageEventArgs e)
        {

            string message = e.Data;

            //App.Log("OnMessage() | " + message, this);

            if (!AcceptJsonMessage && message.StartsWith("{")) { return; }

            if (message.StartsWith("0.6")) { return; }

            else if (message.StartsWith("accept"))
            {

                App.Log("Successfully Connected to Rcon", this);
                PrintToConsole("Successfully Connected to Rcon");
                PrintToConsole("Sending OnConnect Commands");

                if (Settings.sendOnConnect.Count > 0)
                {
                    foreach (string command in Settings.sendOnConnect)
                    {
                        commandQueue.Enqueue(command);
                    }
                }

            }

            else if (!message.Equals(""))
            {
                if (IsChat(message))
                {
                    ChatMessage cm = ParseChat(message);

                    if (App.webhookTrigger != "" && App.webhook != "" && cm.Message.StartsWith(App.webhookTrigger))
                    {
                        SendToDiscord(message);
                    }

                    // Print Chat Message to Chat Log
                    PrintToChat("[" + cm.Date + " " + cm.Time + "] " + cm.Name + ": " + cm.Message + "");

                    // Dispatch chat event
                    PlayerInfo sendingPlayer = null;
                    lock (State.ServerStateLock)
                    {
                        sendingPlayer = State.Players.Find(x => x.Uid == cm.UID);
                    }
                    OnChatMessageReceived(
                        new ChatEventArgs(
                            cm.Message, 
                            sendingPlayer, 
                            cm.Date + " " + cm.Time, 
                            this
                        )
                    );

                }
                else
                {
                    PrintToConsole(message);
                }
            }
        }

        public void OnOpen(object sender, EventArgs e)
        {
            //App.Log("Rcon WebSocket Opened", this);
            PrintToConsole("Sending Rcon Password");
            lock (RconThreadLock)
            {
                RconWebSocket.Send(Encoding.UTF8.GetBytes(Settings.RconPassword));
            }
        }

        public void OnError(object sender, EventArgs e)
        {
            //App.Log("WebSocket Error\nError Message: \n" + eventArgs.Message + "\nException Message: " + (eventArgs.Exception?.Message ?? "none") + "\n", this);
        }

		#endregion

		#region Console Methods

		public string GetConsole()
        {
            return this.consoleText;
        }

        public void PrintToConsole(string line)
        {

            string result = Regex.Replace(line, @"\r\n?|\n", System.Environment.NewLine);
            if (IsDisplayedCurrently) { App.AppendConsole((result + System.Environment.NewLine)); }
            consoleText = consoleText + (result + System.Environment.NewLine);
            Console.WriteLine(Settings.Ip + ": " + result);
        }

        public void ClearConsole()
        {
            this.consoleText = "";
        }

        #endregion

        #region Chat Methods

        public string GetChat()
        {
            return this.chatText;
        }

        public void PrintToChat(string line)
        {
            string result = Regex.Replace(line, @"\r\n?|\n", System.Environment.NewLine);
            if (IsDisplayedCurrently) { App.AppendChat((result + System.Environment.NewLine)); }
            chatText = chatText + (result + System.Environment.NewLine);
            Console.WriteLine(Settings.Ip + ": " + result);
        }

        public void ClearChat()
        {
            this.chatText = "";
        }

        public static bool IsChat(string input)
        {
            ChatMessage message = ParseChat(input);
            if (message != null)
            {
                if (message.Name != null && message.IP != null &&
                   message.Message != null && message.UID != null)
                {
                    if (!message.Name.Equals("") && !message.IP.Equals("") &&
                       !message.Message.Equals("") && !message.UID.Equals(""))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static ChatMessage ParseChat(string chat)
        {
            Regex r = new Regex(ChatMessageRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(chat);
            if (m.Success)
            {
                return new ChatMessage()
                {
                    Message = m.Groups[15].ToString(),
                    Name = m.Groups[8].ToString(),
                    Date = m.Groups[2].ToString(),
                    Time = m.Groups[4].ToString(),
                    UID = m.Groups[10].ToString(),
                    IP = m.Groups[12].ToString()
                };
            }
            return null;
        }

        private const string ChatMessageRegex =
            "(\\[)((?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))" + 
            "[-:\\/.](?:(?:\\d{1}\\d{1})))(?![\\d])( )((?:(?:[0-1][0-9])|(?:[2][0-3])" +
            "|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)(\\])" +
            "( )(<)((?:.*))(\\/)((?:.*))(\\/)((?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)" +
            "\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))(?![\\d])(>)( )((?:.*))"
        ;

        // "I didn't write this" - BIRD COMMAND
        // https://i.imgur.com/TNhqNfn.png

        #endregion

        #region JoinLeaveLog Methods

        public string GetJoinLeaveLog()
        {
            return this.joinLeaveLogText;
        }

        public void PrintToJoinLeaveLog(string line)
        {

            string result = Regex.Replace(line, @"\r\n?|\n", System.Environment.NewLine);
            if (IsDisplayedCurrently) { App.AppendJoinLeave((result + System.Environment.NewLine)); }
            joinLeaveLogText = joinLeaveLogText + (result + System.Environment.NewLine);
        }

        public void ClearJoinLeave()
        {
            this.joinLeaveLogText = "";
        }

        #endregion
        
        #region Dynamic Votefile

        // "Server.VotingJsonPath"
        // "Server.ReloadVotingJson"
        // "Server.NumberOfRevotesAllowed"
        // "Server.NumberOfVotingOptions"
        // "Server.VotingDuplicationLevel" 

        //
        // How to force the server to load a specific map and gametype for the next match
        //
        // Set the voting file to have 2 identical copies of the same (game)type
        // give each type 2 copies of the same map
        // Set voting duplication level to 2
        // set voting choices to 1
        // optionally, lower voting time to 0 and revotes to 0 as well
        // or (optionally) disable voting

        public void Command_EnableDynamicVoteFileManagement()
        {
            Settings.DynamicVotingFileManagement = true;
            new Thread(new ThreadStart(LoadGameVariants)).Start();
            new Thread(new ThreadStart(LoadMapVariants)).Start();
        }
        public void Command_DisableDynamicVoteFileManagement()
        {
            Settings.DynamicVotingFileManagement = false;
        }
        
        public void Command_SetActiveVotingJson(string jsonFileName, string respondPlayer = null)
        {
            new Thread(new ThreadStart(async () => 
            { 
                if (await SetActiveVotingJson(jsonFileName))
                {
                    Respond(respondPlayer, "Successfully changed voting json.");
                } 
                else
                {
                    Respond(respondPlayer, "Failed to change voting json.");
                }            
            })).Start();
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

        private async Task<bool> ActivateMatchQueue(VoteFile dynamic)
        {
            
            // get current vote file
            string currentVotePath = await ServerMessenger.GetJsonResponse(this, "Server.VotingJsonPath");
            if (string.IsNullOrEmpty(currentVotePath)) {
                App.Log("Failed to retrieve Server.VotingJsonPath from the server.", this);
                return false; 
            }

            string[] filePathParts;
            if (currentVotePath.Contains('\\')) {
                filePathParts = currentVotePath.Trim().Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            else {
                filePathParts = currentVotePath.Trim().Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            OriginalVotingFilePath = Settings.VoteFilesDirectoryPath + "\\" + filePathParts[filePathParts.Length - 1];
            OriginalVotingJsonFileName = filePathParts[filePathParts.Length - 1];

            // get current vote option duplication level
            string currentVDL = await ServerMessenger.GetNumericResponse(this, "Server.VotingDuplicationLevel");
            if (string.IsNullOrEmpty(currentVDL))
            {
                App.Log("Failed to retrieve Server.VotingDuplicationLevel from the server.", this);
                return false;
            }
            OriginalVotingDuplicationLevel = currentVDL;

            // get current number of voting options
            string currentNVO = await ServerMessenger.GetNumericResponse(this, "Server.NumberOfVotingOptions");
            if (string.IsNullOrEmpty(currentNVO))
            {
                App.Log("Failed to retrieve Server.NumberOfVotingOptions from the server.", this);
                return false;
            }
            OriginalNumberOfVotingOptions = currentNVO;

            // get current number of revotes allowed
            string currentNRVA = await ServerMessenger.GetNumericResponse(this, "Server.NumberOfRevotesAllowed");
            if (string.IsNullOrEmpty(currentNRVA))
            {
                App.Log("Failed to retrieve Server.NumberOfRevotesAllowed from the server.", this);
                return false;
            }
            OriginalNumberOfRevotesAllowed = currentNRVA;

            // get current map voting time
            string currentMVT = await ServerMessenger.GetNumericResponse(this, "Server.MapVotingTime");
            if (string.IsNullOrEmpty(currentMVT))
            {
                App.Log("Failed to retrieve Server.MapVotingTime from the server.", this);
                return false;
            }
            OriginalMapVotingTime = currentMVT;

            await ServerMessenger.GetStringResponse(this, "Server.VotingDuplicationLevel 2");
            await ServerMessenger.GetStringResponse(this, "Server.NumberOfVotingOptions 1");
            await ServerMessenger.GetStringResponse(this, "Server.NumberOfRevotesAllowed 0");
            await ServerMessenger.GetStringResponse(this, "Server.MapVotingTime 15");

            if (await SetActiveVotingJson(dynamic.Name))
            {
                // update server matchmode
                Settings.ServerMatchMode = ServerSettings.MatchMode.Queue;
                return true;
            }
            else
            {
                App.Log("Failed to set active voting json while activating match queue.", this);
                return false;
            }

        }
        private async void DeactivateMatchQueue()
        {

            if (Settings.ServerMatchMode != ServerSettings.MatchMode.Queue) {
                //App.Log("Attempted to DeactiveMatchQueue() while server was not in Match Queue Mode.", this);
                return; 
            }

            MatchQueue.Clear();
            NextQueuedMatch = "";

            // reset active voting file
            if (!await SetActiveVotingJson(OriginalVotingJsonFileName))
            {
                App.Log("Failed to set active voting json. The server will use whatever the default is.", this);
            }

            // reset vote option duplication level
            string result = await ServerMessenger.GetStringResponse(this, "Server.VotingDuplicationLevel " + OriginalVotingDuplicationLevel);
            if (string.IsNullOrEmpty(result) || result.StartsWith("Value"))
            {
                App.Log("Failed to reset Server.VotingDuplicationLevel.", this);
            }

            // reset number of voting options
            result = await ServerMessenger.GetStringResponse(this, "Server.NumberOfVotingOptions " + OriginalNumberOfVotingOptions);
            if (string.IsNullOrEmpty(result) || result.StartsWith("Value"))
            {
                App.Log("Failed to reset Server.NumberOfVotingOptions.", this);
            }

            // reset number of revotes allowed
            result = await ServerMessenger.GetStringResponse(this, "Server.NumberOfRevotesAllowed " + OriginalNumberOfRevotesAllowed);
            if (string.IsNullOrEmpty(result) || result.StartsWith("Value"))
            {
                App.Log("Failed to reset Server.NumberOfRevotesAllowed.", this);
            }

            // reset map voting time
            result = await ServerMessenger.GetStringResponse(this, "Server.MapVotingTime " + OriginalMapVotingTime);
            if (string.IsNullOrEmpty(result) || result.StartsWith("Value"))
            {
                App.Log("Failed to reset Server.MapVotingTime.", this);
            }

            // update server matchmode
            Settings.ServerMatchMode = ServerSettings.MatchMode.Voting;

        }
        private async void SetNextMatchFromQueue()
        {

            if (MatchQueue.Count == 0)
            {
                DeactivateMatchQueue();
            }

            VoteFile dynamic = null;

            try { dynamic = Settings.GetDynamicVoteFileObject(); }
            catch (Exception e) { App.Log(e.Message, this); }

            if (dynamic == null || !dynamic.IsValid)
            {
                DeactivateMatchQueue();
                return;
            }

            bool nextMatchSet = false;
            MatchInfo matchInfo = null;

            while (MatchQueue.Count > 0 && nextMatchSet == false) {       
                
                lock (MatchQueueLock) { matchInfo = MatchQueue.Dequeue(); }

                if (matchInfo == null) { continue; }

                try { dynamic.MakeSingleOptionVotingFile(matchInfo.GameVariant, matchInfo.MapVariant); }
                catch (Exception e) { App.Log(e.Message, this); }

                if (dynamic == null || !dynamic.IsValid) { continue; }
                else
                {
                    string response = await ServerMessenger.GetStringResponse(this, "Server.ReloadVotingJson");
                    if (response.StartsWith("Success"))
                    {
                        nextMatchSet = true;
                        if (MatchQueue.Count > 0)
                        {
                            MatchInfo next = MatchQueue.Peek();
                            if (next != null)
                            {
                                NextQueuedMatch = next.GameVariant.Name + " on " + next.MapVariant.Name;
                            }
                        }
                    }
                    else
                    {
                        DeactivateMatchQueue();
                        return;
                    }
                }
            }

        }

        public void Command_DeactivateMatchQueue()
        {
            DeactivateMatchQueue();
        }
        public void Command_SetNextGame(string game, string map, string respondPlayer = null)
        {
            new Thread(new ThreadStart(() => { AddGameToQueue(game, map, respondPlayer, true); })).Start();
        }
        public void Command_AddGameToQueue(string game, string map, string respondPlayer = null, bool addToBeginning = false)
        {
            new Thread(new ThreadStart(() => { AddGameToQueue(game, map, respondPlayer, addToBeginning); })).Start();
        }
        public void AddGameToQueue(string game, string map, string respondPlayer = null, bool addToBeginning = false)
        {

            MatchInfo matchInfo = GetMatchInfo(game, map, respondPlayer);

            if (matchInfo.IsValid)
            {
                AddGameToQueue(matchInfo, respondPlayer, addToBeginning);
            }
            else if (respondPlayer != null)
            {
                Respond(respondPlayer, "Unable to add game to queue, either the game or map was invalid." );
            }

        }
        private async void AddGameToQueue(MatchInfo matchInfo, string respondPlayer, bool addToBeginning = false)
        {

            if (!Settings.VoteFilesLoaded || Settings.LastVoteFileLoadTime.AddMinutes(30) < DateTime.Now)
            {
                Settings.LoadAllVoteFilesFromDisk();
            }

            VoteFile dynamic = null;

            try { dynamic = Settings.GetDynamicVoteFileObject(); }
            catch (Exception e) { App.Log(e.Message, this); }
            
            if (dynamic == null) { 
                Respond(respondPlayer, "setNextMatch command failed. Unable to create dynamic voting file."); 
                return;
            }

            try { dynamic.MakeSingleOptionVotingFile(matchInfo.GameVariant, matchInfo.MapVariant); }
            catch (Exception e) { App.Log(e.Message, this); }

            if (dynamic != null && dynamic.IsValid)
            {

                switch (Settings.ServerMatchMode)
                {
                    case ServerSettings.MatchMode.Voting:
                        
                        // Activates Queue
                        if ((await ActivateMatchQueue(dynamic)) == false)
                        {
                            Respond(respondPlayer, "setNextMatch command failed. Failed to activate the match queue feature.");
                            return;
                        }
                        else
                        {
                            NextQueuedMatch = matchInfo.GameVariant.Name + " on " + matchInfo.MapVariant.Name;
                        }

                        break;

                    case ServerSettings.MatchMode.SetList:
                        // Nope
                        Respond(respondPlayer, "Adding match to match queue failed. The server is using a Set List Rotation for matches.");
                        return;

                    case ServerSettings.MatchMode.Queue:
                        MatchQueue.Enqueue(matchInfo);
                        break;

                    default: break;
                }

                AnnounceNextMatch(matchInfo.GameVariant, matchInfo.MapVariant);

            }
            else
            {
                Respond(respondPlayer, "setNextMatch command failed. Invalid dynamic voting file.");
            }

        }

        public void AnnounceNextMatch(GameVariant game, MapVariant map)
        {
            Broadcast(new List<string>() {
                "Next match has been set!",
                game.Name + " (" + game.BaseGameTypeString + ")",
                "ON " + map.Name + " (" + map.BaseMapString + ")"
            });
        }

		#endregion

		#region Map + Game Variant Helpers

        public MatchInfo GetMatchInfo(string game, string map, string respondPlayer = null)
        {            
            if (!GameVariantsLoaded) { LoadGameVariants(); }
            if (!MapVariantsLoaded) { LoadMapVariants(); }

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
            MapVariantsLoaded = true;
            if (Settings.MapVariantsDirectory != null)
            {
                foreach (DirectoryInfo directory in Settings.MapVariantsDirectory.GetDirectories())
                {
                    MapVariant newMV;
                    try { newMV = new MapVariant(directory); }
                    catch (Exception e) { App.Log("Error Parsing MapVariant at " + directory.FullName + " | " + e.Message, this); continue; }
                    if (!MapVariants.Contains(newMV)) { MapVariants.Add(newMV); }
                }
                
                LastMapVariantLoadTime = DateTime.Now;
            }
            else
            {
                MapVariantsLoaded = false;
                App.Log("Failed to get map variants. MapVariants Directory could not be found.", this);
                return;
            }
        }
        public void LoadGameVariants()
        {
            GameVariantsLoaded = true;
            if (Settings.GameVariantsDirectory != null)
            {

                foreach (DirectoryInfo directory in Settings.GameVariantsDirectory.GetDirectories())
                {
                    GameVariant newGV;
                    try { newGV = new GameVariant(directory); }
                    catch (Exception e) { App.Log("Error Parsing GameVariant at " + directory.FullName + " | " + e.Message, this); continue; }
                    if (!GameVariants.Contains(newGV)) { GameVariants.Add(newGV); }
                }

                LastGameVariantLoadTime = DateTime.Now;

            }
            else
            {
                GameVariantsLoaded = false;
                App.Log("Failed to get game variants. GameVariants Directory could not be found.", this);
                return;
            }
        }

        public void SendMapDescriptions(MapVariant.BaseMap baseMap)
        {
            SendMapDescriptions(baseMap, "");
        }
        public void SendMapDescriptions(string baseMap, string playerName)
        {
            SendMapDescriptions(MapVariant.GetBaseMap(baseMap), playerName);
        }
        public void SendMapDescriptions(string baseMap, PlayerInfo player)
        {
            SendMapDescriptions(MapVariant.GetBaseMap(baseMap), player.Name);
        }        
        public void SendMapDescriptions(MapVariant.BaseMap baseMap, PlayerInfo player)
        {
            SendMapDescriptions(baseMap, player.Name);
        }
        public void SendMapDescriptions(MapVariant.BaseMap baseMap, string playerName)
        {

            if (baseMap == MapVariant.BaseMap.Unknown) { Respond(playerName, "Invalid Base Map Received."); return; }
            if (MapVariants.Count == 0) { Respond(playerName, "There are no Map Variants to list."); return; }

            List<MapVariant> variants;
            if (baseMap == MapVariant.BaseMap.All)
            {
                variants = MapVariants;
            }
            else
            {
                variants = new List<MapVariant>();
                foreach (MapVariant variant in MapVariants)
                {
                    if (variant.BaseMapID == baseMap)
                    {
                        variants.Add(variant);
                    }
                }
            }

            List<string> descriptions = new List<string>();
            foreach (MapVariant map in variants)
            {
                descriptions.Add("Map Name: " + map.Name + " | " + "Author: " + map.Author + " | Base Map: " + map.BaseMapString + " | Description: ");
                descriptions.Add(map.Description);
            }
            Respond(playerName, descriptions);

        }        

        public void SendMapDescription(MapVariant map)
        {
            SendMapDescription(map, "");
        }
        public void SendMapDescription(MapVariant map, PlayerInfo player)
        {
            SendMapDescription(map, player.Name);
        }
        public void SendMapDescription(MapVariant map, string playerName)
        {
            Respond(playerName, new List<string>() { 
                "Map Name: " + map.Name + " | " + "Author: " + map.Author + " | Base Map: " + map.BaseMapString + " | Description: ", 
                map.Description 
            });
        }

        public void SendGameDescriptions(GameVariant.BaseGame baseGame)
        {
            SendGameDescriptions(baseGame, "");
        }
        public void SendGameDescriptions(string baseGame, string playerName)
        {
            SendGameDescriptions(GameVariant.GetBaseGame(baseGame), playerName);
        }
        public void SendGameDescriptions(string baseGame, PlayerInfo player)
        {
            SendGameDescriptions(GameVariant.GetBaseGame(baseGame), player.Name);
        }
        public void SendGameDescriptions(GameVariant.BaseGame baseGame, PlayerInfo player)
        {
            SendGameDescriptions(baseGame, player.Name);
        }
        public void SendGameDescriptions(GameVariant.BaseGame baseGame, string playerName)
        {

            if (baseGame == GameVariant.BaseGame.Unknown) { Respond(playerName, "Invalid Base Game Received."); return; }
            if (GameVariants.Count == 0) { Respond(playerName, "There are no Game Variants to list."); return; }

            List<GameVariant> variants;
            if (baseGame == GameVariant.BaseGame.All)
            {
                variants = GameVariants;
            }
            else
            {
                variants = new List<GameVariant>();
                foreach (GameVariant variant in GameVariants)
                {
                    if (variant.BaseGameID == baseGame)
                    {
                        variants.Add(variant);
                    }
                }
            }

            List<string> descriptions = new List<string>();
            foreach (GameVariant Game in variants)
            {
                descriptions.Add("Game Name: " + Game.Name + " | " + "Author: " + Game.Author + " | Base Game: " + Game.BaseGameTypeString + " | Description: ");
                descriptions.Add(Game.Description);
            }
            Respond(playerName, descriptions);

        }

        public void SendGameDescription(GameVariant Game)
        {
            SendGameDescription(Game, "");
        }
        public void SendGameDescription(GameVariant Game, PlayerInfo player)
        {
            SendGameDescription(Game, player.Name);
        }
        public void SendGameDescription(GameVariant Game, string playerName)
        {
            Respond(playerName, new List<string>() {
                "Game Name: " + Game.Name + " | " + "Author: " + Game.Author + " | Base Game: " + Game.BaseGameTypeString + " | Description: ",
                Game.Description
            });
        }


        public GameVariant GetGameVariant(string game)
        {
            if (string.IsNullOrEmpty(game)) { return null; }
            return GameVariants.Find(x => x.Name == game);
        }
        public MapVariant GetMapVariant(string map)
        {
            if (string.IsNullOrEmpty(map)) { return null; }
            MapVariant m = MapVariant.DetermineBaseMap(map);
            if (m != null) { return m; }
            return MapVariants.Find(x => x.Name == map);
        }

        public bool IsValidGameVariant(string name, out GameVariant game)
        {
            foreach (GameVariant item in GameVariants)
            {
                if (item.Name == name)
                {
                    game = item;
                    return true;
                }
            }
            game = null;
            return false;
        }
        public bool IsValidMapVariant(string name, out MapVariant map)
        {
            foreach (MapVariant item in MapVariants)
            {
                if (item.Name == name)
                {
                    map = item;
                    return true;
                }
            }
            map = null;
            return false;
        }
        public bool IsValidGameVariant(string name)
        {
            foreach (GameVariant item in GameVariants)
            {
                if (item.Name == name) { return true; }
            }
            return false;
        }
        public bool IsValidMapVariant(string name)
        {
            foreach (MapVariant item in MapVariants)
            {
                if (item.Name == name) { return true; }
            }
            return false;
        }

		#endregion

		#region Custom Chat Voting

        
        public void Command_BeginVote(Action action, PlayerInfo voteStarterPlayer, string voteBlurb, string voteStartMessage, string votePassedMessage)
        {
            new Thread(new ThreadStart(() => { BeginVote(action, voteStarterPlayer, voteBlurb, new List<string>() { voteStartMessage }, new List<string>() { votePassedMessage }); })).Start();
        }
        public void Command_BeginVote(Action action, PlayerInfo voteStarterPlayer, string voteBlurb, List<string> voteStartMessages, List<string> votePassedMessages)
        {
            new Thread(new ThreadStart(() => { BeginVote(action, voteStarterPlayer, voteBlurb, voteStartMessages, votePassedMessages); })).Start();
        }
        public async void BeginVote(Action action, PlayerInfo voteStarterPlayer, string voteBlurb, List<string> voteStartMessages, List<string> votePassedMessages)
        {

            if (VoteInProgress)
            {
                Respond(voteStarterPlayer.Name, "Unable to start another vote while a vote is currently in progress.");
                return;
            }

            VoteInProgress = true;
            UidVotePairs.Clear();

            foreach (PlayerInfo player in State.Players)
            {
                if (player == voteStarterPlayer)
                {
                    UidVotePairs.Add(new Tuple<string, bool>(player.Uid, true));
                }
                else
                {
                    UidVotePairs.Add(new Tuple<string, bool>(player.Uid, false));
                }
            }

            if (State.Players.Count == 1) { VotesRequiredToPass = 1; }
            else { VotesRequiredToPass = ((State.Players.Count / 2) + 1); }
            
            string voteStarterName = voteStarterPlayer.Name;

            votePassedMessages.Insert(0, "Vote to " + voteBlurb + " has passed.");
            voteStartMessages.Insert(0, voteStarterName + " has started a vote to " + voteBlurb + ". Type \"!yes\" to vote.");

            await RespondAsync(null, voteStartMessages);

            DateTime voteStartTime = DateTime.Now;
            DateTime voteEndTime = voteStartTime.AddSeconds(VoteDuration);

            int statusInterval = (int)(VoteDuration / (float)(VoteStatusUpdates + 1) * 1000f);
            List<DateTime> updateTimes = new List<DateTime>();
            for (int i = 1; i < (VoteStatusUpdates + 1); i++)
            {
                updateTimes.Add(voteStartTime.AddMilliseconds(statusInterval * i));
            }

            ChatMessageReceived += CheckVote;

            while (DateTime.Now < voteEndTime)
            {

                lock (uidVotePairsLock)
                {

                    int voteCount = UidVotePairs.FindAll(x => x.Item2).Count;

                    if (voteCount >= VotesRequiredToPass)
                    {
                        goto VotePassed;
                    }

                    if (updateTimes.Count > 0 && DateTime.Now > updateTimes[0])
                    {
                        Broadcast("Vote in progress to " + voteBlurb + ". " + (VotesRequiredToPass - voteCount) + " more votes required to pass. Type \"!yes\" to vote.");
                        updateTimes.RemoveAt(0);
                    }

                }

                Thread.Sleep(1000);
            }

            // Vote Failed
            Broadcast("Vote to " + voteBlurb + " did not pass. Received " + UidVotePairs.FindAll(x => x.Item2).Count + "/" + VotesRequiredToPass + " needed votes.");
            lock (uidVotePairsLock) { UidVotePairs.Clear(); }
            ChatMessageReceived -= CheckVote;
            VoteInProgress = false;            
            return;

            // Vote Passed
            VotePassed:
            lock (uidVotePairsLock) { UidVotePairs.Clear(); }
            await RespondAsync(null, votePassedMessages);
            ChatMessageReceived -= CheckVote;
            VoteInProgress = false;
            action();

        }   
        void CheckVote(object sender, ChatEventArgs e)
        {
            if (e.Message.ToLowerInvariant() == "!yes")
            {
                lock (uidVotePairsLock)
                {
                    Tuple<string, bool> voter = UidVotePairs.Find(x => x.Item1 == e.SendingPlayer.Uid);
                    if (voter != null)
                    {
                        if (voter.Item2)
                        {
                            Whisper(e.SendingPlayer.Name, "You can only vote once.");
                        }
                        else
                        {
                            UidVotePairs.Remove(voter);
                            UidVotePairs.Add(new Tuple<string, bool>(e.SendingPlayer.Uid, true));
                            int voteCount = UidVotePairs.FindAll(x => x.Item2).Count;
                            if (voteCount < VotesRequiredToPass)
                            {
                                Broadcast(e.SendingPlayer.Name + " has voted yes. " + (VotesRequiredToPass - voteCount) + " more votes needed to pass.");
                            }
                            else
                            {
                                Broadcast(e.SendingPlayer.Name + " has voted yes.");
                            }
                        }
                    }
                }
            }
        }

		#endregion

		#region Server Communication

		public void Command(string cmd)
        {
            lock (RconThreadLock)
            {
                if (RconWebSocket.IsAlive)
                {
                    try
                    {
                        RconWebSocket.Send(Encoding.UTF8.GetBytes(cmd));
                    }
                    catch (Exception e)
                    {
                        App.Log("WebSocket Command Transmission Error: " + e.Message, this);
                    }
                }
            }
        }

        public void SendToDiscord(string message)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(App.webhook);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string roleInfo = "";
                if (!App.webhookRole.Equals(""))
                {
                    roleInfo = "<@&" + App.webhookRole + "> ";
                }
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
        public void SendToRcon(string cmd)
        {
            new Thread(delegate () {
                Command(cmd);
            }).Start();
        }

        public void Respond(string playerName = null, string message = null)
        {            
            if (!string.IsNullOrEmpty(playerName))
            {
                Whisper(playerName, message ?? "");
            }
            else
            {
                Broadcast(message ?? "");
            }
        }
        public void Respond(string playerName = null, List<string> messages = null)
        {
            if (!string.IsNullOrEmpty(playerName))
            {
                Whisper(playerName, messages);
            }
            else
            {
                Broadcast(messages);
            }
        }
        public async Task RespondAsync(string playerName = null, List<string> messages = null)
        {
            if (!string.IsNullOrEmpty(playerName))
            {
                foreach (string message in messages)
                {
                    Whisper(playerName, message);
                    await Task.Delay(Connection.ServerMessageDelay);
                }
            }
            else
            {
                foreach (string message in messages)
                {
                    Broadcast(message);
                    await Task.Delay(Connection.ServerMessageDelay);
                }
            }
        }
        

        public void Broadcast(string message)
        {
            SendToRcon("Server.Say \"" + message + "\"");
        }
        public void Whisper(string playerName, string message)
        {
            SendToRcon("Server.PM \"" + playerName + "\" \"" + message + "\"");
        }
        public void Broadcast(List<string> messages)
        {
            new Thread(new ThreadStart(() => 
            { 
                foreach (string message in messages)
                {
                    Broadcast(message);
                    Thread.Sleep(Connection.ServerMessageDelay);
                }
            })).Start();
        }
        public void Whisper(string playerName, List<string> messages)
        {
            new Thread(new ThreadStart(() =>
            {
                foreach (string message in messages)
                {
                    Whisper(playerName, message);
                    Thread.Sleep(Connection.ServerMessageDelay);
                }
            })).Start();
        }


        private void ManageCommandQueue(object sender, EventArgs e)
        {
            if (commandQueue.Count > 0 && commandQueueProcessing == false)
            {
                commandQueueProcessing = true;
                RconWebSocket.OnMessage += CommandQueueNext;
                CommandQueueNext(sender, e);
            }
            else if (commandQueue.Count == 0 && commandQueueProcessing == true)
            {
                commandQueueProcessing = false;
                RconWebSocket.OnMessage -= CommandQueueNext;
                PrintToConsole("All queued commands have been sent");
            }
        }
        private void CommandQueueNext(object sender, EventArgs e)
        {
            if (commandQueue.Count > 0)
            {
                string command = commandQueue.Dequeue();
                PrintToConsole(command);
                lock (RconThreadLock)
                {
                    RconWebSocket.Send(Encoding.UTF8.GetBytes(command));
                }
            }
        }

        private void RequestRankAndEmblemInfo(PlayerJoinLeaveEventArgs e)
        {
            new Thread(new ThreadStart(async () =>
            {
                string rankAndEmblemJson = null;
                while (rankAndEmblemJson == null)
                {
                    try { rankAndEmblemJson = await ServerMessenger.GetJsonResponse(this, "Server.PlayersInfo"); }
                    catch { continue; }
                }
                if (!string.IsNullOrEmpty(rankAndEmblemJson))
                {
                    List<Tuple<int,string>> rankAndEmblemDataPairs = ParseRankAndEmblemJson(rankAndEmblemJson);
                    if (rankAndEmblemDataPairs.Count < LastPlayerJoinEventArgs.NewPlayerCount) { return; }
                    else { RankAndEmblemDataPairs = rankAndEmblemDataPairs; }
                }

            })).Start();
        }

		#endregion

		#region Events

		public event EventHandler<MatchBeginEndArgs> MatchBeganOrEnded;
        public event EventHandler<ChatEventArgs> ChatMessageReceived;
		public event EventHandler<PlayerJoinLeaveEventArgs> PlayerJoined;
        public event EventHandler<PlayerJoinLeaveEventArgs> PlayerLeft;
        public event EventHandler<PlayerJoinLeaveEventArgs> PlayerCountChanged;

        public event EventHandler<PlayerTeamChangeEventArgs> PlayerChangedTeams;
        //public event EventHandler OnMatchBegin;
        //public event EventHandler OnMatchEnd;
        public void OnMatchBeginOrEnd(object sender, MatchBeginEndArgs e)
        {
            if (e.MatchBegan)
            {
                
                //lock (RconThreadLock)
                //{
                //    if (RconThread != null && RconThread.IsAlive)
                //    {
                //        Respond(null, "Join the ranks of BIRD COMMAND on Discord! https://discord.gg/upuphgd", 0);
                //    }
                //}
                

                // Manage match queue
                if (Settings.ServerMatchMode == ServerSettings.MatchMode.Queue)
                {
                    new Thread(new ThreadStart(() => { SetNextMatchFromQueue(); })).Start();
                }
                
            }
            else
            {

            }
        }
        public void OnMessageRuntimeCommands(object sender, ChatEventArgs e)
        {
            RuntimeCommand.TryRunCommand(e.Message, e.SendingPlayer, e.Connection);
        }
        public void OnPlayerTeamChanged(object sender, PlayerTeamChangeEventArgs e)
        {
            if (State.Teams == false) { return; }
            if (State.Status == StatusStringInLobby) { return; }
            if (State.Status == StatusStringLoading) { return; }

            if (State.Status != StatusStringInGame) { App.Log(State.Status + " | TEAM CHANGE"); }

            App.Log(
                e.PlayerState.Name + " changed teams from " + 
                App.TeamColorStrings[e.PlayerStatePrevious.Team] + " team to " + 
                App.TeamColorStrings[e.PlayerState.Team] + " team."
                , this
            );
        }
        public void OnChatMessageReceived(ChatEventArgs e)
        {
            ChatMessageReceived?.Invoke(e.Connection, e);
        }
        public void OnPlayerJoined(PlayerJoinLeaveEventArgs e)
        {

            LastPlayerJoinEventArgs = e;
            
            PlayerJoined?.Invoke(e.Connection, e);
            PlayerCountChanged?.Invoke(e.Connection, e);

            if (IsDisplayedCurrently && App.settings_PlaySoundOnPlayerJoin)
            {
                System.Media.SoundPlayer player;
                // Set sound that will be played
                if (App.settings_PlayerJoinSoundPath == "")
                {
                    // Use default sound
                    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerJoinSound);
                }
                else
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerJoinSound);
                }
                player.Play();
            }

            RequestRankAndEmblemInfo(e);

        }
        public void OnPlayerLeft(PlayerJoinLeaveEventArgs e)
        {
            PlayerLeft?.Invoke(e.Connection, e);
            PlayerCountChanged?.Invoke(e.Connection, e);

            if (IsDisplayedCurrently && App.settings_PlaySoundOnPlayerLeave)
            {
                System.Media.SoundPlayer player;
                // Set sound that will be played
                if (App.settings_PlayerLeaveSoundPath == "")
                {
                    // Use default sound
                    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerLeaveSound);
                }
                else
                {
                    player = new System.Media.SoundPlayer(Properties.Resources.Audio_PlayerLeaveSound);
                }
                player.Play();
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
                EventTime = System.DateTime.Now.ToShortTimeString();
                Connection = connection;
            }

            public string EventTime { get; set; }
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

            public ChatEventArgs(string message, PlayerInfo sender, string eventTime, Connection connection)
            {
                SendingPlayer = sender;
                Message = message;
                EventTime = eventTime;
                Connection = connection;
            }

            public string Message { get; set; }
            public PlayerInfo SendingPlayer { get; set; }
            public string EventTime { get; set; }
            public Connection Connection { get; set; }

        }

        public class MatchBeginEndArgs : EventArgs
        {

            public bool MatchBegan { get; set; }
            public DateTime EventTime { get; set; }
            public Connection Connection { get; set; }

            public MatchBeginEndArgs(bool matchIsBeginning, Connection connection)
            {
                this.MatchBegan = matchIsBeginning;
                this.Connection = connection;
                EventTime = DateTime.Now;
            }

        }

        #endregion

#if DEBUG

        public Thread ServerStatusTestDataThread;
        public Queue<string> ServerStatusTestData = new Queue<string>();

        public void LoadServerStatusTestData()
        {
            if (File.Exists(Settings.Ip + "." + Settings.InfoPort + ".txt"))
            {
                ServerStatusTestData = new Queue<string>(File.ReadAllLines(Settings.Ip + "." + Settings.InfoPort + ".txt"));
            }
            else { PrintToConsole("Server Status Test Data not found"); }
        }
        public void BeginUseServerStatusTestData()
        {
            LoadServerStatusTestData();
            if (ServerStatusTestData.Count == 0) { PrintToConsole("No server status test data found for this connection."); return; }
            else { PrintToConsole("Server status test data loaded. Loaded " + ServerStatusTestData.Count + " entries."); }
            ServerStatusTestDataThread = new Thread(new ThreadStart(UseServerStatusTestData));
            ServerStatusTestDataThread.Start();
        }
        private void UseServerStatusTestData()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (ServerStatusTestData.Count == 0) {
                    LoadServerStatusTestData();
                    PrintToConsole("Server Status Test Data Entries Exhausted - Looping Back to First Entry");
                }
                ProcessServerStatus(ServerStatusTestData.Dequeue());
            }
        }

#endif

    }

    public class ChatMessage
    {
        public string Message { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
        public string UID { get; set; }
        public string IP { get; set; }

    }

}
