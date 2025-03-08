using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

using static RconTool.App;

namespace RconTool
{
    public class ServerState
    {

        public string name { get; set; } = string.Empty;
        public int port { get; set; } = 0;
        public string hostPlayer { get; set; } = string.Empty;
        public bool isDedicated { get; set; } = false;
        public SprintStyle SprintStyle {
            get {
                if (string.IsNullOrWhiteSpace(sprintState)) { return SprintStyle.Disabled; }
                else if (sprintState == "0") { return SprintStyle.Disabled; }
                else if (sprintState == "1") { return SprintStyle.Enabled; }
                else if (sprintState == "2") { return SprintStyle.SetByGametype; }
                else { return SprintStyle.Disabled; }
            }
        }
        public string sprintState { get; set; } = string.Empty;
        public bool SprintUnlimitedEnabledBool { get { return ( sprintUnlimitedEnabled ?? "0" ) == "1"; } }
		public string sprintUnlimitedEnabled { get; set; } = string.Empty;
        public bool DualWieldingEnabledBool { get { return ( dualWielding ?? "0" ) == "1"; } }
		public string dualWielding { get; set; } = string.Empty;
        public bool AssassinationEnabledBool { get { return (assassinationEnabled ?? "0") == "1"; } }
		public string assassinationEnabled { get; set; } = string.Empty;
        public string VotingEnabled { get; set; } = string.Empty;
        public VotingStyle VotingStyle {
			get {
				if (string.IsNullOrWhiteSpace(voteSystemType)) { return VotingStyle.Disabled; }
				else if (voteSystemType == "0") { return VotingStyle.Disabled; }
				else if (voteSystemType == "1") { return VotingStyle.Vote; }
				else if (voteSystemType == "2") { return VotingStyle.Veto; }
				else { return VotingStyle.Disabled; }
			}
		}
		public string voteSystemType { get; set; } = string.Empty;
		public bool voip { get; set; } = false;
		public bool teams { get; set; } = false;
        public int redScore { get; set; } = 0;
		public int blueScore { get; set; } = 0;
        public string map { get; set; } = string.Empty;
		public string mapFile { get; set; } = string.Empty;
        public string variant { get; set; } = string.Empty;
        public string variantType { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string PreviousStatus { get; set; } = "_";
        public int numPlayers { get; set; } = 0;
        public int maxPlayers { get; set; } = 0;
		public int modCount { get; set; } = 0;
		public string modPackageName { get; set; } = string.Empty;
		public string modPackageAuthor { get; set; } = string.Empty;
		public string modPackageHash { get; set; } = string.Empty;
		public string modPackageVersion {  get; set; } = string.Empty;
		public string Xnkid { get; set; } = string.Empty;
        public string Xnaddr { get; set; } = string.Empty;
        public bool Passworded { get; set; } = false;
        public List<int> TeamScores { get; set; } = new List<int>();
        public List<Tuple<int, int>> OrderedTeamScores { get; set; } = new List<Tuple<int, int>>();
        // { {Team Index, Team Score}, {Team Players} },
        // { {Team Index, Team Score}, {Team Players} },
        // { {Team Index, Team Score}, {Team Players} }...
        public List<Tuple<Tuple<int, int>, List<PlayerInfo>>> OrderedTeams { get; set; } = new List<Tuple<Tuple<int, int>, List<PlayerInfo>>>();
        public List<Tuple<int, PlayerInfo>> RankedPlayers { get; set; } = new List<Tuple<int, PlayerInfo>>();
        public List<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();
        public Dictionary<string, PlayerInfo> PlayersByUid { get; set; } = new Dictionary<string, PlayerInfo>();
        private List<PlayerInfo> RemovePlayers { get; set; } = new List<PlayerInfo>();
        public string GameVersion { get; set; } = string.Empty;
        public string eldewritoVersion { get; set; } = string.Empty;
        public GameVariant.BaseGame GameVariantType { get; set; }

        public readonly object ServerStateLock = new object();
        
        public bool IsValid  { get { return GameVersion != string.Empty; } }

        public bool InLobby { get { return inLobby; } }
        private bool inLobby = false;

        /// <summary>
        /// Returns the TimeSpan representing how long the server status has been InLobby.
        /// <br>Returns 0 if the lobby timer is not active for any reason.</br>
        /// </summary>
        public TimeSpan TimeInLobby { get {
                if (lobbyTimerActive) { return DateTime.Now - lobbyStartTime; }
                else { return TimeSpan.Zero; }
			}
        }
        private DateTime lobbyStartTime;
        private bool lobbyTimerActive = false;


        public bool IsLoading { get { return isLoading; } }
        private bool isLoading = false;

        public static Image statusOverlayInLobby = Properties.Resources.Image_StatusOverlay_InLobby_126x40;
        public static Image statusOverlayInGame = Properties.Resources.Image_StatusOverlay_InGame_126x40;

        public static Image mapIconLobby = Properties.Resources.map_icon_Lobby;
        public static Image mapIconGuardian = Properties.Resources.map_icon_Guardian;
        public static Image mapIconValhalla = Properties.Resources.map_icon_Valhalla;
        public static Image mapIconDiamondback = Properties.Resources.map_icon_Diamondback;
        public static Image mapIconEdge = Properties.Resources.map_icon_Edge;
        public static Image mapIconReactor = Properties.Resources.map_icon_Reactor;
        public static Image mapIconIcebox = Properties.Resources.map_icon_Icebox;
        public static Image mapIconThePit = Properties.Resources.map_icon_ThePit;
        public static Image mapIconNarrows = Properties.Resources.map_icon_Narrows;
        public static Image mapIconHighGround = Properties.Resources.map_icon_HighGround;
        public static Image mapIconStandoff = Properties.Resources.map_icon_Standoff;
        public static Image mapIconSandtrap = Properties.Resources.map_icon_Sandtrap;
        public static Image mapIconLastResort = Properties.Resources.map_icon_LastResort;

        public Image LobbyStateOverlay { 
            get {
                if (status == "InLobby") { return statusOverlayInLobby; }
                else { return statusOverlayInGame; }
                //else if (Status == "loading") { return Properties.Resources.Image_StatusOverlay_Loading_126x40; }
            }
        }
        public Image LobbyMapBackgroundImage {
            get {
				switch (mapFile) {
                    case "guardian":       return mapIconGuardian;
                    case "riverworld":     return mapIconValhalla;
                    case "s3d_avalanche":  return mapIconDiamondback;
                    case "s3d_edge":       return mapIconEdge;
                    case "s3d_reactor":    return mapIconReactor;
                    case "s3d_turf":       return mapIconIcebox;
                    case "cyberdyne":      return mapIconThePit;
                    case "chill":          return mapIconNarrows;
                    case "deadlock":       return mapIconHighGround;
                    case "bunkerworld":    return mapIconStandoff;
                    case "shrine":         return mapIconSandtrap;
                    case "zanzibar":       return mapIconLastResort;
                    default:               return mapIconLobby;
                }				
			}
        }

        public void Update(ServerState newState, Connection connection)
        {
            
            lock (ServerStateLock)
            {

                if (status != newState.status) { connection.RecordMatchResults(this); }
                if (connection.ServerHookActive && teams != newState.teams) { Scoreboard.RegenerateScoreboardImage = true; }

                name = newState.name;
                port = newState.port;
                hostPlayer = newState.hostPlayer;
                sprintState = newState.sprintState;
                sprintUnlimitedEnabled = newState.sprintUnlimitedEnabled;
                dualWielding = newState.dualWielding;
                assassinationEnabled = newState.assassinationEnabled;
                VotingEnabled = newState.VotingEnabled;
                teams = newState.teams;
                map = newState.map;
                mapFile = newState.mapFile;
                variant = newState.variant;
                variantType = newState.variantType;
                numPlayers = newState.numPlayers;
                maxPlayers = newState.maxPlayers;
                TeamScores = newState.TeamScores;
                Passworded = newState.Passworded;
                Xnkid = newState.Xnkid;
                Xnaddr = newState.Xnaddr;
                isDedicated = newState.isDedicated;
                GameVersion = newState.GameVersion;
                eldewritoVersion = newState.eldewritoVersion;

                PreviousStatus = status;
                status = newState.status;

                inLobby = status == "InLobby";
                isLoading = status == "Loading";

                GameVariantType = GameVariant.GetBaseGameID(variantType);

            }

            //string date = System.DateTime.Now.ToString("[MM-dd-yyyy HH:mm:ss] ");
            string date = $"[{DateTimeUTC()}] ";

            // Detect Match Start and End
            if (status != PreviousStatus)
            {
                // Game Started
                if (status == Connection.StatusStringInGame)
                {
                    connection.InvokeMatchBeganOrEnded(new Connection.MatchBeginEndArgs(true, connection));
                    //connection.OnMatchBeginOrEnd(this, new Connection.MatchBeginEndArgs(true, connection));
                    connection.PrintToConsole("Game Started: " + newState.variant + " ON " + newState.map);
                }
                // Game Ended
                else if (status == Connection.StatusStringInLobby && PreviousStatus == Connection.StatusStringInGame)
                {
                    connection.InvokeMatchBeganOrEnded(new Connection.MatchBeginEndArgs(false, connection));
                    //connection.OnMatchBeginOrEnd(this, new Connection.MatchBeginEndArgs(false, connection));
                    connection.PrintToConsole("Game Ended");
                    PlayerStatsRecord match;
					foreach (PlayerInfo player in Players) {
                        match = App.PlayerStatsRecords.Value.Find(x => x.Uid == player.Uid);
                        if (match != null) { match.Kills += player.Kills; match.Deaths += player.Deaths; }
                        else {
                            match = new PlayerStatsRecord(player);
                            if (match.IsValid) { App.PlayerStatsRecords.Value.Add(match); }
                        }
					}
                    App.PlayerStatsRecords.Save();
                }

                // Start Lobby Timer
                if (InLobby && !lobbyTimerActive) {
                    lobbyStartTime = DateTime.Now;
                    lobbyTimerActive = true;
				}
                // Reset Lobby Timer
                if (!InLobby && lobbyTimerActive) {
                    lobbyTimerActive = false;
				}

            }

            // For each player in the new server state's player list
            foreach (PlayerInfo player in newState.Players)
            {
                
                if (player.Name.Length == 0) { continue; }

                if (!teams) { player.Team = -1; }
                else if (status == Connection.StatusStringInLobby) { player.Team = -1; }
                else if (status == Connection.StatusStringLoading) { player.Team = -1; }

                // If the player list does not contain this player, this player has just joined the game
                PlayerInfo match = Players.Find(x => x.Uid == player.Uid);
                if (match == null)
                {
                    connection.PrintToPlayerLog(player.Name + " - " + player.ServiceTag + " : " + player.Uid + " has Joined.");
                    connection.OnPlayerJoined(new Connection.PlayerJoinLeaveEventArgs(player, true, newState.Players.Count, date, connection));
                    lock (ServerStateLock)
                    {
                        // add new player to the player list
                        Players.Add(player);
                    }
                }

            }
                            
            // For each player in the player list
            foreach (PlayerInfo player in Players)
            {

                // If the new server state's player list does not contain this player, this player has just left the game
                //NOTE added '&& x.Name == player.Name', so if any bugs crop up check here
                PlayerInfo match = newState.Players.Find(x => x.Uid == player.Uid && x.Name == player.Name);
                if (match == null)
                {
                    connection.PrintToPlayerLog(player.Name + " - " + player.ServiceTag + " : " + player.Uid + " has Left.");
                    connection.OnPlayerLeft(new Connection.PlayerJoinLeaveEventArgs(player, false, newState.Players.Count, date, connection));
                    // Mark player for removal from players list
                    RemovePlayers.Add(player);
                }
                else {                        
                    if (player.Team != match.Team && player.Team != -1 && match.Team != -1)
                    {
                        // Player has switched teams
                        connection.OnPlayerTeamChanged(this, new Connection.PlayerTeamChangeEventArgs(match, player, connection));
                    }
                    lock (ServerStateLock)
                    {
                        // Update the player's stats
                        player.Update(match);
                    }
                }
            }

            lock (ServerStateLock)
            {

                // Remove players that left from the player list
                foreach (PlayerInfo removePlayer in RemovePlayers)
                {
                    Players.Remove(removePlayer);
                }
                RemovePlayers.Clear();


                // Sort by team and then by score
                List<IGrouping<int, PlayerInfo>> teams = Players.GroupBy(x => x.Team).OrderBy(x => x.Key).ToList();
                Players.Clear();
                foreach (IGrouping<int, PlayerInfo> team in teams)
                {
                    Players.AddRange(team.OrderByDescending(x => x.RoundScore));
                }

                OrderedTeamScores.Clear();
                OrderedTeams.Clear();
                RankedPlayers.Clear();

                // Sort team scores for scoreboard display
                if (this.teams)
                {

					OrderedTeamScores = TeamScores.Select(
                        (x, i) =>
                            ( Players.Any(p => p.Team == i))
                                ? new Tuple<int, int>(i, x)
                                : new Tuple<int, int>(-1, x)
                    )                                                   // Mark all empty teams with -1
                    .Where(x => x.Item1 > -1)                           // Grab all non-empty teams
                    .OrderByDescending(x => x.Item2).ToList();          // Order teams by score (descending)

                    connection.PopulatedTeams = OrderedTeamScores?.Count ?? 1;

                    for (int i = 0; i < OrderedTeamScores.Count; i++)
                    {
						OrderedTeams.Add(
                            new Tuple<Tuple<int, int>, List<PlayerInfo>>(
								OrderedTeamScores[i],
								Players.Where(x => x.Team == OrderedTeamScores[i].Item1).ToList()
                            )
                        );
                    }

                }


                // Update PlayersByUid Dict
                PlayersByUid.Clear();
				foreach (PlayerInfo player in newState.Players) { PlayersByUid.Add(player.Uid, player); }
                newState.PlayersByUid = PlayersByUid;

            }
            
        }

    }

    public enum SprintStyle
	{
		/// <summary>
		/// Sprint is globally disabled.
		/// </summary>
		Disabled = 0,
		/// <summary>
		/// Sprint is globally enabled.
		/// </summary>
		Enabled = 1,
		/// <summary>
		/// Sprint enabled status is set by the active gametype.
		/// </summary>
		SetByGametype = 2
	}

    public enum VotingStyle {
		/// <summary>
		/// Voting is disabled.
		/// </summary>
		Disabled = 0,
		/// <summary>
		/// Voting is enabled.
		/// </summary>
		Vote = 1,
		/// <summary>
		/// Vetoing is enabled.
		/// </summary>
		Veto = 2
	}

}
