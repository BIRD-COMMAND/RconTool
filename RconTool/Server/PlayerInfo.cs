using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RconTool
{
    public class PlayerInfo : IEquatable<PlayerInfo>, INotifyPropertyChanged
    {

        public string ScoreboardName { get; set; } = "";
        public string ScoreboardColorRGBA { get; set; } = "";
        public string ScoreboardColorDarkRGBA { get; set; } = "";
        public System.Drawing.Color ScoreboardColor { 
            get { return scoreboardColor; }
            set { 
                scoreboardColor = value;
                ScoreboardColorDark =  Color.FromArgb(
                    Math.Max(0, value.R - 30), 
                    Math.Max(0, value.G - 30), 
                    Math.Max(0, value.B - 30)
                );
            }
        } 
        private System.Drawing.Color scoreboardColor = System.Drawing.Color.Gray;
        public System.Drawing.Color ScoreboardColorDark { get; set; } = System.Drawing.Color.Gray;
        public string ScoreboardKillDeathRatio { get; set; } = "00.00";

        public Bitmap RankImage { get; set; } = null;
        public int Rank { get; set; } = -1;

        public Bitmap Emblem { get; set; } = null;
        public string EmblemURL { get; set; } = null;

        public string Name {
            get { return name; }
            set { this.name = value; /*NotifyPropertyChanged();*/ }
        }
        private string name;
        public string ServiceTag {
            get { return serviceTag; }
            set { 
                this.serviceTag = value; 
                if (serviceTag != null && serviceTag.Length < 4)
                {
                    ScoreboardServiceTag = serviceTag + new string(' ', 4 - serviceTag.Length);
                }
                else
                {
                    ScoreboardServiceTag = serviceTag ?? "";
                }
                /*NotifyPropertyChanged();*/ 
            }
        }
        private string serviceTag;
        public string ScoreboardServiceTag { get; set; }
        public int Team {
            get { return team; }
            set 
            { 
                if (value < -1 || value > 15)
                {
                    this.team = -1;
                }
                else
                {
                    this.team = value;
                }
                
                /*NotifyPropertyChanged();*/ 
            }
        }
        private int team;
        public string Uid {
            get { return uid; }
            set { this.uid = value; /*NotifyPropertyChanged();*/ }
        }
        private string uid;
        public string PrimaryColor {
            get { return primaryColor; }
            set { this.primaryColor = value; /*NotifyPropertyChanged();*/ }
        }
        private string primaryColor;
        public bool IsAlive {
            get { return isAlive; }
            set { this.isAlive = value; /*NotifyPropertyChanged();*/ }
        }
        private bool isAlive;
        public int Score {
            get { return score; }
            set { this.score = value; /*NotifyPropertyChanged();*/ }
        }
        private int score;
        public int Kills {
            get { return kills; }
            set { this.kills = value; /*NotifyPropertyChanged();*/ }
        }
        private int kills;
        public int Assists {
            get { return assists; }
            set { this.assists = value; /*NotifyPropertyChanged();*/ }
        }
        private int assists;
        public int Deaths {
            get { return deaths; }
            set { this.deaths = value; /*NotifyPropertyChanged();*/ }
        }
        private int deaths;
        public int Betrayals {
            get { return betrayals; }
            set { this.betrayals = value; /*NotifyPropertyChanged();*/ }
        }
        private int betrayals;
        public int TimeSpentAlive {
            get { return timeSpentAlive; }
            set { 
                this.timeSpentAlive = value; /*NotifyPropertyChanged();*/
                if (timeSpentAlive < 3600) { TimeConnected = TimeSpan.FromSeconds(timeSpentAlive).ToString(@"mm\:ss"); }
                else { TimeConnected = TimeSpan.FromSeconds(timeSpentAlive).ToString(@"hh\:mm\:ss"); }                
            }
        }
        private int timeSpentAlive;
        public int Suicides {
            get { return suicides; }
            set { this.suicides = value; /*NotifyPropertyChanged();*/ }
        }
        private int suicides;
        public int BestStreak {
            get { return bestStreak; }
            set { this.bestStreak = value; /*NotifyPropertyChanged();*/ }
        }
        private int bestStreak;
        public double KillDeathRatio {
            get { return killDeathRatio; }
            set { this.killDeathRatio = value; /*NotifyPropertyChanged();*/ }
        }
        private double killDeathRatio;

        public string TimeConnected { get; set; } = "0:00";

        public string ReplyPlayer { get; set; } = "";


        //public string TeamString { 
        //    get 
        //    {
        //        switch (Team)
        //        {
        //            case 0: return "RED";
        //            case 1: return "BLUE";
        //            case 2: return "GREEN";
        //            case 3: return "ORANGE";
        //            case 4: return "PURPLE";
        //            case 5: return "GOLD";
        //            case 6: return "BROWN";
        //            case 7: return "PINK";
        //            default: return "NO TEAM";
        //        }
        //    }
        //}

        //public string IsAliveString { get { return (IsAlive) ? "": "X"; } }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        public void Update(PlayerInfo player)
        {

            Name = player.Name;
            ServiceTag = player.ServiceTag;
            Kills = player.Kills;
            Deaths = player.Deaths;
            Team = player.Team;
            IsAlive = player.IsAlive;
            Score = player.Score;
            Assists = player.Assists;
            Betrayals = player.Betrayals;            
            Suicides = player.Suicides;
            BestStreak = player.BestStreak;
            PrimaryColor = player.PrimaryColor;

            //Kill/Death Ratio            
            if (Kills < 0) { KillDeathRatio = Kills; }
            else if (Kills == 0) {
                if (Deaths == 0) { KillDeathRatio = 0; }
                else if (Deaths > 0) { KillDeathRatio = Deaths * -1; }
                else { KillDeathRatio = Kills; }
            }
            else if (Kills > 0) {
                if (Deaths == 0) { KillDeathRatio = Kills; }
                else { KillDeathRatio = Kills / Deaths; }
            }
            else { KillDeathRatio = Kills; }

            ScoreboardKillDeathRatio = string.Format("{0:00.00}", KillDeathRatio);
            ScoreboardColor = System.Drawing.ColorTranslator.FromHtml(PrimaryColor);
            ScoreboardColorRGBA = "rgba(" + ScoreboardColor.R + "," + ScoreboardColor.G + "," + ScoreboardColor.B+ ",230)";
            ScoreboardColorDarkRGBA = "rgba(" + ScoreboardColorDark.R + "," + ScoreboardColorDark.G + "," + ScoreboardColorDark.B + ",230)";
            ScoreboardName = Name + " - " + new string(' ', 4 - ServiceTag.Length) + ServiceTag;

            ReplyPlayer = player.ReplyPlayer;

            if (TimeSpentAlive != player.TimeSpentAlive) { TimeSpentAlive = player.TimeSpentAlive; }

            if (player.Rank > -1) { Rank = player.Rank; }
            if (player.Emblem != null) { Emblem = player.Emblem; }

        }

        public PlayerInfo FindMatchingPlayer(List<PlayerInfo> players)
        {
            foreach (PlayerInfo player in players)
            {
                if (player.Name == Name && player.Uid == Uid) { return player; }
            }
            return null;
        }

        public override bool Equals(object a)   
        {
            return this.Equals(a as PlayerInfo);
        }
        public bool Equals(PlayerInfo a)
        {
            // If parameter is null, return false.
            if (a is null) { return false; }
            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, a)) { return true; }
            // If run-time types are not exactly the same, return false.
            if (this.GetType() != a.GetType()) { return false; }
            // Return true if the relevant fields match.
            return (Uid == a.Uid);
        }
        public static bool operator ==(PlayerInfo a, PlayerInfo b)
        {

            // Check for null on left side.
            if (a is null)
            {
                if (b is null)
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return a.Equals(b);

        }
        public static bool operator !=(PlayerInfo a, PlayerInfo b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }

        public bool IsValid { get { return !String.IsNullOrWhiteSpace(this.Uid) && this.Uid != "-1"; } }
        public static PlayerInfo Blank()
        {
            return new PlayerInfo()
            {
                Name = "",
                ServiceTag = "",
                Team = -1,
                Uid = "-1",
                PrimaryColor = "",
                IsAlive = false,
                Score = 0,
                Kills = 0,
                Assists = 0,
                Deaths = 0,
                Betrayals = 0,
                TimeSpentAlive = 0,
                Suicides = 0,
                BestStreak = 0
            };
        }

        public static Dictionary<Item, string> ItemStrings = new Dictionary<Item, string>()
        {
            {Item.Name, "Name"},
            {Item.ServiceTag, "ServiceTag"},
            {Item.Team, "Team"},
            {Item.Uid, "Uid"},
            {Item.PrimaryColor, "PrimaryColor"},
            {Item.IsAlive, "IsAlive"},
            {Item.Score, "Score"},
            {Item.Kills, "Kills"},
            {Item.Assists, "Assists"},
            {Item.Deaths, "Deaths"},
            {Item.Betrayals, "Betrayals"},
            {Item.TimeSpentAlive, "TimeSpentAlive"},
            {Item.Suicides, "Suicides"},
            {Item.BestStreak, "BestStreak" },
            {Item.KillDeathRatio, "KillDeathRatio" }
        };
        public static Dictionary<Item, string> ItemTitles = new Dictionary<Item, string>()
        {
            {Item.Name, "NAME"},
            {Item.ServiceTag, "TAG"},
            {Item.Team, "TEAM"},
            {Item.Uid, "UID"},
            {Item.PrimaryColor, "COLOR"},
            {Item.IsAlive, "ALIVE"},
            {Item.Score, "SCORE"},
            {Item.Kills, "KILLS"},
            {Item.Assists, "ASSISTS"},
            {Item.Deaths, "DEATHS"},
            {Item.Betrayals, "BETRAYALS"},
            {Item.TimeSpentAlive, "TIME ALIVE"},
            {Item.Suicides, "SUICIDES"},
            {Item.BestStreak, "STREAK" },
            {Item.KillDeathRatio, "K/D" }
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public enum Item
        {
            Name,
            ServiceTag,
            Team,
            Uid,
            PrimaryColor,
            IsAlive,
            Score,
            Kills,
            Assists,
            Deaths,
            Betrayals,
            TimeSpentAlive,
            Suicides,
            BestStreak,
            KillDeathRatio
        }

    }
}
