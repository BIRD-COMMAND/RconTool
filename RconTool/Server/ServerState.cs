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

        public string Name { get; set; } = "";
        public int Port { get; set; } = 0;
        public string HostPlayer { get; set; } = "";
        public string SprintEnabled { get; set; } = "";
        public string SprintUnlimitedEnabled { get; set; } = "";
        public string DualWielding { get; set; } = "";
        public string AssassinationEnabled { get; set; } = "";
        public string VotingEnabled { get; set; } = "";
        public bool Teams { get; set; } = false;
        public string Map { get; set; } = "";
        public string MapFile { get; set; } = "";
        public string Variant { get; set; } = "";
        public string VariantType { get; set; } = "";
        public string Status { get; set; } = "";
        public string PreviousStatus { get; set; } = "_";
        public int NumPlayers { get; set; } = 0;
        public int MaxPlayers { get; set; } = 0;
        public string Xnkid { get; set; } = "";
        public string Xnaddr { get; set; } = "";
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
        public bool IsDedicated { get; set; } = false;
        public string GameVersion { get; set; } = "";
        public string EldewritoVersion { get; set; } = "";
        public GameVariant.BaseGame GameVariantType { get; set; }

        public readonly object ServerStateLock = new object();
        
        public bool IsValid  { get { return GameVersion != ""; } }

        public bool InLobby { get { return inLobby; } }
        private bool inLobby = false;

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
                if (Status == "InLobby") { return statusOverlayInLobby; }
                else { return statusOverlayInGame; }
                //else if (Status == "loading") { return Properties.Resources.Image_StatusOverlay_Loading_126x40; }
            }
        }
        public Image LobbyMapBackgroundImage {
            get {
				switch (MapFile) {
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

                if (Status != newState.Status) { connection.RecordMatchResults(this); }
                if (connection.ServerHookEnabled && Teams != newState.Teams) { Scoreboard.RegenerateScoreboardImage = true; }

                Name = newState.Name;
                Port = newState.Port;
                HostPlayer = newState.HostPlayer;
                SprintEnabled = newState.SprintEnabled;
                SprintUnlimitedEnabled = newState.SprintUnlimitedEnabled;
                DualWielding = newState.DualWielding;
                AssassinationEnabled = newState.AssassinationEnabled;
                VotingEnabled = newState.VotingEnabled;
                Teams = newState.Teams;
                Map = newState.Map;
                MapFile = newState.MapFile;
                Variant = newState.Variant;
                VariantType = newState.VariantType;
                NumPlayers = newState.NumPlayers;
                MaxPlayers = newState.MaxPlayers;
                TeamScores = newState.TeamScores;
                Passworded = newState.Passworded;
                Xnkid = newState.Xnkid;
                Xnaddr = newState.Xnaddr;
                IsDedicated = newState.IsDedicated;
                GameVersion = newState.GameVersion;
                EldewritoVersion = newState.EldewritoVersion;

                PreviousStatus = Status;
                Status = newState.Status;

                inLobby = Status == "InLobby";
                isLoading = Status == "Loading";

                GameVariantType = GameVariant.GetBaseGameID(VariantType);

            }

            //string date = System.DateTime.Now.ToString("[MM-dd-yyyy HH:mm:ss] ");
            string date = $"[{DateTimeUTC()}] ";

            // Detect Match Start and End
            if (Status != PreviousStatus)
            {
                // Game Started
                if (Status == Connection.StatusStringInGame)
                {
                    connection.InvokeMatchBeganOrEnded(new Connection.MatchBeginEndArgs(true, connection));
                    //connection.OnMatchBeginOrEnd(this, new Connection.MatchBeginEndArgs(true, connection));
                    connection.PrintToConsole("Game Started: " + newState.Variant + " ON " + newState.Map);
                }
                // Game Ended
                else if (Status == Connection.StatusStringInLobby && PreviousStatus == Connection.StatusStringInGame)
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
            }

            // For each player in the new server state's player list
            foreach (PlayerInfo player in newState.Players)
            {
                
                if (player.Name.Length == 0) { continue; }

                if (!Teams) { player.Team = -1; }
                else if (Status == Connection.StatusStringInLobby) { player.Team = -1; }
                else if (Status == Connection.StatusStringLoading) { player.Team = -1; }

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
                    Players.AddRange(team.OrderByDescending(x => x.Score));
                }

                OrderedTeamScores.Clear();
                OrderedTeams.Clear();
                RankedPlayers.Clear();

                // Sort team scores for scoreboard display
                if (Teams)
                {
                    
                    OrderedTeamScores = TeamScores.Select(
                        (x, i) =>
                            (Players.Any(p => p.Team == i))
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
}
