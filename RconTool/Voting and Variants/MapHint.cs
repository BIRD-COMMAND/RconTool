using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	public class MapHint
	{

		/// <summary>
		/// The name of the map to broadcast hints for.
		/// </summary>
		public string mapName;
		/// <summary>
		/// The hint that will be broadcast to the server.
		/// </summary>
		public string mapHint;
		/// <summary>
		/// The number of seconds between broadcasts of this hint.
		/// </summary>
		public int frequencyInSeconds { 
			get { return (int)frequency.TotalSeconds; }
			set { if (value > 0) { frequency = TimeSpan.FromSeconds(value); } } 
		}
		private TimeSpan frequency = TimeSpan.FromSeconds(60);

		/// <summary>
		/// The last time the hint was broadcast.
		/// </summary>
		public DateTime lastIssued = DateTime.MinValue;

		/// <summary>
		/// Create a new MapHint. The specified hint will be broadcast to the server whenever that map is loaded and the server is in-game.
		/// <br>The hint will be broadcast every so often, specified by the hintFrequencyInSeconds param. The default frequency is every 60 seconds.</br>
		/// </summary>
		/// <param name="map">The name of the map to broadcast hints for.</param>
		/// <param name="hint">The hint that will be broadcast to the server.</param>
		/// <param name="hintFrequencyInSeconds">The number of seconds between broadcasts of this hint.</param>
		public MapHint(string map, string hint, int hintFrequencyInSeconds = 60)
		{
			
			if (string.IsNullOrWhiteSpace(map)) { throw new ArgumentException("Argument 'map' must not be null or blank."); }
			if (string.IsNullOrWhiteSpace(hint)) { throw new ArgumentException("Argument 'hint' must not be null or blank."); }
			if (hintFrequencyInSeconds < 1) { throw new ArgumentException("Argument 'hintFrequencyInSeconds' must be a positive integer greater than 0."); }

			mapName = map;
			mapHint = hint;
			frequencyInSeconds = hintFrequencyInSeconds;

		}

		public void TryHint(Connection connection)
		{

			// Return if map or hint are invalid
			if (string.IsNullOrWhiteSpace(mapName) || string.IsNullOrWhiteSpace(mapHint)) { return; }
			
			// Return if a different map is loaded
			if (connection.State.Map != mapName) { return; }

			// Return if we're not in-game
			if (connection.InLobby) { return; }

			// If the hint is due to be issued, broadcast it
			if (DateTime.Now - lastIssued > frequency) {
				connection.Broadcast(mapHint);
				lastIssued = DateTime.Now;
			}

		}

	}
}
