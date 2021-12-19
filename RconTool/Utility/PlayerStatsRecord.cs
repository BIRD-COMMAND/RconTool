using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RconTool
{
	public class PlayerStatsRecord
	{

		public bool IsValid { get; set; } = false;
		public string Uid { get; set; }
		public string Name { get; set; }
		public int Team { get; set; }
		public int Score { get; set; }
		public int Kills { get; set; }
		public int Deaths { get; set; }
		public int Assists { get; set; }
		public int BestStreak { get; set; }
		public int Suicides { get; set; }
		public int Betrayals { get; set; }
		public double KDRatio { get { return CalculateKDRatio(Kills, Deaths); } }

		public PlayerStatsRecord(PlayerInfo player)
		{
			if (player == null) { return; }
			Uid = player.Uid;
			Name = player.Name;
			Team = player.Team;
			Score = player.Score;
			Kills = player.Kills;
			Deaths = player.Deaths;
			Assists = player.Assists;
			BestStreak = player.BestStreak;
			Suicides = player.Suicides;
			Betrayals = player.Betrayals;
			IsValid = true;
		}

		private double CalculateKDRatio(int kills, int deaths)
		{
			if (deaths == 0 && kills == 0) { return 0; }
			else if (deaths == 0) { return kills; }
			else if (kills == 0) { return deaths * -1; }
			else if (kills < 0) { return kills; }
			else { return kills / deaths; }
		}

		public double LiveKDRatio(PlayerInfo player) { 
			return CalculateKDRatio(Kills + player.Kills, Deaths + player.Deaths); 
		}

	}
}