using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{

	/// <summary>
	/// Helper class for parsing a RuntimeCommand, identifying its arguments (if applicable), and validating the RuntimeCommand as a whole.
	/// </summary>
	public class ParseResult
	{

		/// <summary>
		/// Conveys the validity of the parsed command.
		/// <br>If the RuntimeCommand is parsed successfully, IsValid will be true.</br>
		/// <br>If parsing fails, or required arguments cannot be resolved, IsValid will be false.</br>
		/// </summary>
		public bool IsValid { get; set; } = true;
		/// <summary>
		/// Contains any arguments that were identified within the command string. If none are found, the list will be empty (not null).</string>
		/// <br>Sometimes a command has no parameters. Sometimes no valid Parameters can be determined.</br>
		/// </summary>
		public List<string> Parameters { get; set; } = new List<string>();
		/// <summary>
		/// Response strings that will be sent to the appropriate party (usually the player that issued a command).
		/// </summary>
		public List<string> Response { get; set; } = new List<string>();
		/// <summary>
		/// The ChatMessage associated with the message/RuntimeCommand being parsed.
		/// </summary>
		public Message ChatMessage { get; set; } = null;

		// A DynamicReference is a special string which, when surrounded with the DynamicReferenceDelimiterChar ('%' by default), will be evaluated as a dynamic reference by the <see cref="ReplaceDynamicReferences(string, Connection)"/> method.
		// <br>This dictionary contains all DynamicReference key strings and the methods which return the dynamic value used to replace the DynamicReference string key.</br>
		// <br>Example string with DynamicReference: "The player currently in first place is %firstPlace%!"</br>

		//PLUS give certain DynamicReferences well-defined behavior in the result of a tie (which will be very common for some)

		/*%player1%-%player16%     // player 1 - player 16 (based on index)
		* %place1%-%place16%       // 1st place - 16th place (based on scoreboard or match results)
		* %firstPlace%             // first place
		* %lastPlace%              // last place
		* %winningTeam%            // every player on winning team probably
		* %losingTeam%             // every player on losing team probably
		* %mvp%                    // best KDR or most points (depending on gametype)
		* %most/leastPoints%       // player with most or least points
		* %most/leastKills%        // player with most or least kills
		* %most/leastDeaths%       // player with most or least deaths
		* %most/leastBetrayals%    // player with most or least betrayals
		* %random%                 // random player
		* %gametype%               // CTF, Assault, King of the Hill, Slayer, etc.
		* %objectiveDescriptor%    // Flag Captures, Bomb Detonations, Time on the Hill, Kills, etc.
		* %gameName%               // name of the game variant
		* %mapName%                // name of the map variant*/
		/// <summary>
		/// A DynamicReference is a special string which, when surrounded with the DynamicReferenceDelimiterChar ('%' by default), will be evaluated as a dynamic reference by the <see cref="ReplaceDynamicReferences(string, Connection)"/> method.
		/// <br>This dictionary contains all DynamicReference key strings and the methods which return the dynamic value used to replace the DynamicReference string key.</br>
		/// <br>Example string with DynamicReference: "The player currently in first place is %firstPlace%!"</br>
		/// </summary>
		public static Dictionary<string, Func<Connection, string>> DynamicCommandReferences = new Dictionary<string, Func<Connection, string>>()
		{
			// first place
			{"firstPlace", (c => {return c.GetPlayerNameInFirstPlace();;})},             
			// last place
			{"lastPlace", (c => {return c.GetPlayerNameInLastPlace();})},
			{"winningTeam", (c => {return "";})},								// every player on winning team probably
			{"losingTeam", (c => {return "";})},								// every player on losing team probably
			{"mvp", (c => {return c.GetMVPName();})},							// best KDR or most points (depending on gametype)
			{"mostPoints", (c => {return c.GetHighestScoreName();})},			// player with most points
			{"leastPoints", (c => {return c.GetLowestScoreName();})},			// player with least points
			{"mostKills", (c => {return c.GetHighestKillsName();})},			// player with most kills
			{"leastKills", (c => {return c.GetLowestKillsName();})},			// player with least kills
			{"mostDeaths", (c => {return c.GetHighestDeathsName();})},		// player with most deaths
			{"leastDeaths", (c => {return c.GetLowestDeathsName();})},		// player with least deaths
			{"mostBetrayals", (c => {return c.GetHighestBetrayalsName();})},	// player with most betrayals
			{"leastBetrayals", (c => {return c.GetLowestBetrayalsName();})},	// player with least betrayals
			// random player
			{"random", (c => {return (c?.State?.Players?[new Random().Next(0, c?.State?.Players?.Count - 1 ?? 0)]?.Name ?? "");})},
			// CTF, Assault, King of the Hill, Slayer, etc.
			{"gametype", (c => {return GameVariant.BaseGameDisplayNamesByBaseGame[c?.State?.GameVariantType ?? GameVariant.BaseGame.Unknown];})},
			{"objectiveDescriptor", (c => {
				switch (c?.State?.GameVariantType ?? GameVariant.BaseGame.Unknown)
				{
					case GameVariant.BaseGame.Slayer: return "kills";
					case GameVariant.BaseGame.Oddball: return "time holding balls";
					case GameVariant.BaseGame.KingOfTheHill: return "time on the hill";
					case GameVariant.BaseGame.CaptureTheFlag: return "flag captures";
					case GameVariant.BaseGame.Assault: return "bomb detonations";
					case GameVariant.BaseGame.Juggernaut: return "Juggernaut points";
					case GameVariant.BaseGame.Infection: return "Infection points";
					case GameVariant.BaseGame.VIP: return "VIP points";
					case GameVariant.BaseGame.Unknown: return "";
					case GameVariant.BaseGame.All: return "";
					default: return "";
			}})},						// Flag Captures, Bomb Detonations, Time on the Hill, Kills, etc.			    
			{"gameName", (c => {return c?.State?.Variant ?? "";})},  // name of the game variant
			{"mapName", (c => {return c?.State?.Map ?? "";})},       // name of the map variant
			{"player1",  (c => {return c.GetPlayerName(0); })},      // player 1
			{"player2",  (c => {return c.GetPlayerName(1); })},      // player 2
			{"player3",  (c => {return c.GetPlayerName(2); })},      // player 3
			{"player4",  (c => {return c.GetPlayerName(3); })},      // player 4
			{"player5",  (c => {return c.GetPlayerName(4); })},      // player 5
			{"player6",  (c => {return c.GetPlayerName(5); })},      // player 6
			{"player7",  (c => {return c.GetPlayerName(6); })},      // player 7
			{"player8",  (c => {return c.GetPlayerName(7); })},      // player 8
			{"player9",  (c => {return c.GetPlayerName(8); })},      // player 9
			{"player10", (c => {return c.GetPlayerName(9); })},      // player 10
			{"player11", (c => {return c.GetPlayerName(10); })},     // player 11
			{"player12", (c => {return c.GetPlayerName(11); })},     // player 12
			{"player13", (c => {return c.GetPlayerName(12); })},     // player 13
			{"player14", (c => {return c.GetPlayerName(13); })},     // player 14
			{"player15", (c => {return c.GetPlayerName(14); })},     // player 15
			{"player16", (c => {return c.GetPlayerName(15); })},     // player 16
			{"place1",  (c => {return c.GetPlayerNameByPlace(1); })},      // place 1
			{"place2",  (c => {return c.GetPlayerNameByPlace(2); })},      // place 2
			{"place3",  (c => {return c.GetPlayerNameByPlace(3); })},      // place 3
			{"place4",  (c => {return c.GetPlayerNameByPlace(4); })},      // place 4
			{"place5",  (c => {return c.GetPlayerNameByPlace(5); })},      // place 5
			{"place6",  (c => {return c.GetPlayerNameByPlace(6); })},      // place 6
			{"place7",  (c => {return c.GetPlayerNameByPlace(7); })},      // place 7
			{"place8",  (c => {return c.GetPlayerNameByPlace(8); })},      // place 8
			{"place9",  (c => {return c.GetPlayerNameByPlace(9); })},      // place 9
			{"place10", (c => {return c.GetPlayerNameByPlace(10); })},     // place 10
			{"place11", (c => {return c.GetPlayerNameByPlace(11); })},     // place 11
			{"place12", (c => {return c.GetPlayerNameByPlace(12); })},     // place 12
			{"place13", (c => {return c.GetPlayerNameByPlace(13); })},     // place 13
			{"place14", (c => {return c.GetPlayerNameByPlace(14); })},     // place 14
			{"place15", (c => {return c.GetPlayerNameByPlace(15); })},     // place 15
			{"place16", (c => {return c.GetPlayerNameByPlace(16); })},     // place 16
		};
		private static readonly char[] DynamicReferenceDelimiterCharArray = new char[]{'%'};
		
		/// <summary>
		/// Replace certain keywords in the <paramref name="message"/> surrounded with %s with dynamic values related to the current game or lobby for the <paramref name="connection"/>.
		/// </summary>
		/// <param name="message">The message string in which to find and replace any dynamic reference tokens.</param>
		/// <param name="connection">The connection to gather dynamic reference values from.</param>
		/// <returns></returns>
		public static string ReplaceDynamicReferences(string message, Connection connection)
		{

			if (string.IsNullOrWhiteSpace(message) || !message.Contains('%')) { return message; }

			for (int i = 0, s = -1, e = -1; i < message.Length; i++) {
				if (message[i] == '%') {
					
					if (s == -1){ s = i; }
					else		{ e = i;  }

					if (e > s) {
						string commandKey = message.Substring(s + 1, e - s - 1);
						if (DynamicCommandReferences.ContainsKey(commandKey)) {
							int length = message.Length;
							message = 
								  message.Substring(0, s)
								+ DynamicCommandReferences[commandKey](connection)
								+ message.Substring(e + 1);
							i += message.Length - length;
						}
						else { i = e - 1; } // jump back one in case 2nd % is the 1st % in a new pair
						s = -1; e = -1;
					}
				}
			}

			return message;

		}

		/// <summary>
		/// Generate a <see cref="ParseResult"/> for the specified <see cref="RuntimeCommand"/> (<paramref name="command"/>) by parsing the command string (<paramref name="message"/>) sent by <see cref="PlayerInfo"/> (<paramref name="player"/>) (or RCON connected admin client) for the specified <see cref="Connection"/> (<paramref name="connection"/>), and store the related <see cref="Message"/> (<paramref name="chatMessage"/>) if applicable.
		/// </summary>
		/// <param name="connection">The relevant connection used to Parse the <paramref name="command"/>, respond to the <paramref name="player"/> if applicable, and store the <paramref name="chatMessage"/> if applicable.</param>
		/// <param name="message">The string that will be parsed for args and DynamicReferences for the <see cref="RuntimeCommand"/> being evaluated</param>
		/// <param name="player">The player whose message is being parsed.</param>
		/// <param name="command">The RuntimeCommand being parsed.</param>
		/// <param name="chatMessage">The ChatMessage containing the RuntimeCommand being parsed.</param>
		public ParseResult(Connection connection, string message, PlayerInfo player, RuntimeCommand command, Message chatMessage)
		{
			
			if (connection == null) { IsValid = false; }

			ChatMessage = chatMessage;

			ReplaceDynamicReferences(message, connection);

			// Add a copy of the command submitted by the player or server to the list of responses

			// To player
			if (player != null)
			{
				if (message.Length + player.Name.Length + 3 > 128)
				{
					Response.Add("@" + player.Name + ":");
					// Response.Add(" " + message); <- Previously there was a space here, but I think that would mess up, becuase it would be ' !whatever...', which would be flagged as a command I'm pretty sure
					Response.Add("@" + message);
				}
				else
				{
					Response.Add("@" + player.Name + ": " + message);
				}
			}
			// To server
			else {
				if (message.Length + 9 > 128) {
					Response.Add("@SERVER:");
					// Response.Add(" " + message); <- Previously there was a space here, but I think that would mess up, becuase it would be ' !whatever...', which would be flagged as a command I'm pretty sure
					Response.Add("@" + message);
				}
				else {
					Response.Add($"@SERVER: {message}");
				}
			}

			// Confirm authorization
			if (command.AdminCommand && player != null) {
				if (!connection.Settings.AuthorizedUIDs.Contains(player.Uid ?? "")) {
					IsValid = false;
					Add("You are not authorized to use this command.");
					return;
				}
			}

			// Process Args
			if (command.Args.Count > 0)
			{
				
				//TODO '!pm Player "message in quotes"' only sends 'message' - i.e. only the first word of the quoted message
				//TODO '!pm Player message with "quoted content" inside' only sends 'message with quoted' - so it cuts off everything after the first word after quotation mark

				// message with command trigger trimmed
				string content = message.TrimStart(command.Trigger.Length).Trim();

				foreach (Arg arg in command.Args)
				{

					switch (arg.ArgType)
					{

						case Arg.Type.PlayerName:
							List<string> players = null;
							lock (connection.State.ServerStateLock)
							{
								players = connection.State.Players.Select(x => x.Name).ToList();
							}
							Parameters.Add(BestMatch(players, content, arg, false));
							break;

						case Arg.Type.BaseMap:
							Parameters.Add(BestMatch(MapVariant.BaseMapIDsByNameLower.Keys.ToList(), content, arg));
							break;

						case Arg.Type.MapName:
							Parameters.Add(BestMatch(connection.MapVariantNames, content, arg, true));
							break;

						case Arg.Type.BaseVariant:
							Parameters.Add(BestMatch(GameVariant.BaseGamesByNameLower.Keys.ToList(), content, arg));
							break;

						case Arg.Type.VariantName:
							Parameters.Add(BestMatch(connection.GameVariantNames, content, arg, true));
							break;

						case Arg.Type.String:
							Parameters.Add(string.IsNullOrWhiteSpace(content) ? null : content);
							break;

						case Arg.Type.FileNameJSON:
							if (!string.IsNullOrWhiteSpace(content) && content.Contains(".json")) { 
								Parameters.Add(content.Substring(0, content.IndexOf(".json") + 5)); 
							}
							else { Parameters.Add(null); }
							break;

						case Arg.Type.LanguageCode:
							if (!string.IsNullOrWhiteSpace(content) && content.Length >= 4 && content.StartsWith("-") && content.Contains(' '))
							{
								// format is "-ab *whatever*" | ab = any language code
								content = content.TrimStart(1);
								Parameters.Add(content.Substring(0, content.IndexOf(' ')));
							}
							else { Parameters.Add(null); }
							break;

						case Arg.Type.CommandName:
							if (!string.IsNullOrWhiteSpace(content)) {
								
								RuntimeCommand runtimeCommand = RuntimeCommand.RuntimeCommands.First(x => x.Name.StartsWith(content) || x.Trigger.StartsWith(content));
								if (runtimeCommand != null) { Parameters.Add(content); break; }

								ToolCommand toolCommand = connection.Settings.Commands.First(x => x.Name.StartsWith(content));
								if (toolCommand != null) { Parameters.Add(content); break; }
								
								Parameters.Add(null);

							}
							else { Parameters.Add(null); }
							break;

						default: break;

					}

					if (Parameters[Parameters.Count - 1] != null)
					{
						// update content string
						content = content.Substring(Parameters[Parameters.Count - 1].Length, (content.Length - Parameters[Parameters.Count - 1].Length)).Trim();
					}
					else if (arg.IsRequired) {
						IsValid = false; Response.Add("Command Failed: Unable to parse " + arg.Name + " parameter."); 
					}

					// Add response and return if invalid
					if (!IsValid) {
						switch (arg.ArgType)
						{
							case Arg.Type.PlayerName:	Response.Add("Player names must be unquoted and correctly capitalized."); return;
							case Arg.Type.BaseMap:		Response.Add("Base map names must be unquoted, with any spaces removed."); return;
							case Arg.Type.MapName:		Response.Add("Map names must be unquoted and correctly capitalized."); return;
							case Arg.Type.BaseVariant:	Response.Add("Base gametype names must be unquoted, with any spaces removed."); return;
							case Arg.Type.VariantName:	Response.Add("Game variant names must be unquoted and correctly capitalized."); return;
							case Arg.Type.FileNameJSON: Response.Add("JSON file names must end in '.json'."); return;
							case Arg.Type.LanguageCode: Response.Add("Language code format is either '-aa' or '-aa-bb'."); return;
							case Arg.Type.CommandName:  Response.Add("Command names must be unquoted and correctly capitalized."); return;
							default: return;
						}
					}

				}

			}

		}

		/// <summary>
		/// Check each matchItem to see if the matchString starts with that item. Returns the best (longest) match, or null.
		/// </summary>
		/// <param name="matchItems">List of items to compare against the matchString.</param>
		/// <param name="matchString">The string that will be compared against the matchItems.</param>
		/// <param name="arg">The argument being matched.</param>
		/// <param name="matchLowerInvariants">If true, matchItems and matchString will be compared all in lower-case form, increasing forgiveness for capitalization errors.</param>
		private string BestMatch(List<string> matchItems, string matchString, Arg arg, bool matchLowerInvariants = true)
		{

			List<string> matches = Match(matchItems, matchString, arg, matchLowerInvariants);

			if (matches != null && matches.Count > 0)
			{
				// Detect if two identical matches were found
				if (matches.Count > 1 && (matches[0] ?? "") == (matches[1] ?? ""))
				{
					// Failed - indeterminate match
					if (arg.IsRequired)
					{
						IsValid = false;
						Response.Add("Command Failed: There was more than one match for the " + arg.Name + " parameter.");
					}
					return null;
				}
				// Otherwise the result is the first (longest) match
				else { return matches[0]; }
			}
			else
			{
				// Failed: Match not found
				if (arg.IsRequired)
				{
					IsValid = false;
					Response.Add("Command Failed: Unable to find " + arg.Name + ".");
				}
				return null;
			}

		}


		/// <summary>
		/// Looks through <paramref name="matchItems"/> for any strings matching <paramref name="matchString"/>.
		/// <br>Returns the best (longest) match, or an empty List if none are found.</br>
		/// <br><paramref name="matchLowerInvariants"/> can be set to true to enable matching strings that only differ in capitalization.</br>
		/// </summary>
		/// <param name="matchItems">List of items to compare against the matchString.</param>
		/// <param name="matchString">The string that will be compared against the matchItems.</param>
		/// <param name="arg">The argument being matched.</param>
		/// <param name="matchLowerInvariants">If true, matchItems and matchString will be compared all in lower-case form, increasing forgiveness for capitalization errors.</param>
		/// <returns></returns>
		public static List<string> Match(List<string> matchItems, string matchString, Arg arg, bool matchLowerInvariants = true) {

			if (string.IsNullOrWhiteSpace(matchString)) { return new List<string>(); }

			// Copy list so we don't mess with the original list
			List<Tuple<string, string>> items = matchItems.Select(x => new Tuple<string, string>(x, x.ToLowerInvariant())).ToList();
			string matchStringLower = matchString.ToLowerInvariant();
			
			List<Tuple<string, string>> matches;

			// Match lower case versions of strings if allowed - more forgiving for capitalization errors
			if (matchLowerInvariants) { 
				matches = items.FindAll(x => matchStringLower.StartsWith(x.Item2)).ToList(); 
			}
			else {
				matches = items.FindAll(x => matchString.StartsWith(x.Item1)).ToList(); 
			}

			// Order by result length, the longest match is the most correct match
			matches = matches.OrderByDescending(x => x.Item1.Length).ToList();

			// Return original matchItems, ordered by best (longest) match first
			return matches.Select(x => x.Item1).ToList();

		}

		/// <summary>
		/// Add a response string to the stored response list.
		/// </summary>
		public void Add(string response)
		{
			Response.Add(response);
		}
		/// <summary>
		/// Add response strings to the stored response list.
		/// </summary>
		public void Add(List<string> responses)
		{
			Response.AddRange(responses);
		}

	}

}
