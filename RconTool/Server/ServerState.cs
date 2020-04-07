using System.Collections.Generic;
using System.ComponentModel;
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
        public int NumPlayers { get; set; } = 0;
        public int MaxPlayers { get; set; } = 0;
        public string Xnkid { get; set; } = "";
        public string Xnaddr { get; set; } = "";
        public bool Passworded { get; set; } = false;
        public List<int> TeamScores { get; set; } = new List<int>();
        public List<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();
        private List<PlayerInfo> RemovePlayers { get; set; } = new List<PlayerInfo>();
        public bool IsDedicated { get; set; } = false;
        public string GameVersion { get; set; } = "";
        public string EldewritoVersion { get; set; } = "";

        public readonly object ServerStateLock = new object();
        
        public bool IsValid  { get { return GameVersion != ""; } }

        public bool InLobby { get { return inLobby; } }
        private bool inLobby = false;

        public bool IsLoading { get { return isLoading; } }
        private bool isLoading = false;

        public void Update(ServerState newState, Connection connection)
        {
            
            lock (ServerStateLock)
            {

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

                inLobby = Status.ToLowerInvariant().StartsWith("inlobby");
                isLoading = Status.ToLowerInvariant().StartsWith("loading");

            }

            string date = System.DateTime.Now.ToString("[MM-dd-yyyy HH:mm:ss] ");

            // Detect Match Start and End
            if (newState.Status != Status)
            {
                // Game Started
                if (newState.Status == Connection.StatusStringInGame)
                {
                    connection.OnMatchBeginOrEnd(this, new Connection.MatchBeginEndArgs(true, connection));
                    connection.PrintToConsole(date + "Game Started - " + newState.Variant + ":" + newState.VariantType + " - " + newState.Map);
                }
                // Game Ended
                else if (newState.Status == Connection.StatusStringInLobby && Status == Connection.StatusStringInGame)
                {
                    connection.OnMatchBeginOrEnd(this, new Connection.MatchBeginEndArgs(false, connection));
                    connection.PrintToConsole(date + "Game Ended");
                }
            }

            // For each player in the new server state's player list
            foreach (PlayerInfo player in newState.Players)
            {
                
                if (player.Name.Length == 0) { continue; }

                if (!Teams) { player.Team = -1; }
                else if (newState.Status == Connection.StatusStringInLobby) { player.Team = -1; }
                else if (newState.Status == Connection.StatusStringLoading) { player.Team = -1; }

                // If the player list does not contain this player, this player has just joined the game
                PlayerInfo match = Players.Find(x => x.Uid == player.Uid);
                if (match == null)
                {
                    connection.PrintToJoinLeaveLog(player.Name + " - " + player.ServiceTag + " : " + player.Uid + " has Joined.");
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
                PlayerInfo match = newState.Players.Find(x => x.Uid == player.Uid);
                if (match == null)
                {
                    connection.PrintToJoinLeaveLog(player.Name + " - " + player.ServiceTag + " : " + player.Uid + " has Left.");
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

                // Sort by team and then by name
                List<IGrouping<int, PlayerInfo>> teams = Players.GroupBy(x => x.Team).OrderBy(x => x.Key).ToList();
                Players.Clear();
                foreach (IGrouping<int, PlayerInfo> team in teams)
                {
                    Players.AddRange(team.OrderBy(x => x.Name));
                }

                Status = newState.Status;

                // TeamScores correct for team oddball
                // 
                // not sure about this, testing now
                // TeamScores can contain completely wrong numbers for team games that aren't team slayer
                //if (Teams && TeamScores != null && TeamScores.Count >= 2)
                //{
                //    if (TeamScores[0] == 0)
                //    {
                //        int a = Players.Sum(x => ((x.Team == 0) ? x.Score : 0));
                //        if (TeamScores[0] != a) { TeamScores[0] = a; }
                //    }
                //    if (TeamScores[1] == 0)
                //    {
                //        int a = Players.Sum(x => ((x.Team == 1) ? x.Score : 0));
                //        if (TeamScores[1] != a) { TeamScores[1] = a; }
                //    }
                //}

            }
            
        }

    }
}
