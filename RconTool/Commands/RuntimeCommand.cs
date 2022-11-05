using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebSocketSharp;

namespace RconTool
{
	/// <summary>
	/// Base class for all custom server chat commands, usable by sending an in-game chat message in this format: '!triggerWord and arguments'.
	/// </summary>
	public class RuntimeCommand
	{
		
		static RuntimeCommand()
		{
			RuntimeCommandNames = RuntimeCommands.Select(x => x.Name).ToList();
			RuntimeCommandAutoCompleteNames = RuntimeCommands.Select(x => $"%{x.Name}%").ToList();
		}

		public static List<string> RuntimeCommandNames;
		public static List<string> RuntimeCommandAutoCompleteNames;

		private static readonly string[] HelpCodes = new string[6] { "-h", "/h", "-help", "/help", "-?", "/?" };
		private static bool IsHelpQuery(string query)
		{
			return (
				query.EndsWith(HelpCodes[0]) ||
				query.EndsWith(HelpCodes[1]) ||
				query.EndsWith(HelpCodes[2]) ||
				query.EndsWith(HelpCodes[3]) ||
				query.EndsWith(HelpCodes[4]) ||
				query.EndsWith(HelpCodes[5])
			);
		}

		public static bool StringStartsWithCommandName(string query)
		{
			if (string.IsNullOrWhiteSpace(query)) { return false; }
			foreach (string item in RuntimeCommandNames) {
				if (item.StartsWith(query)) { return true; }
			}
			return false;
		}
		public static void TryRunCommand(string commandString, PlayerInfo sendingPlayer, Connection connection, Message chatMessage = null)
		{

			//if (sendingPlayer == null) { return; }

			if (string.IsNullOrWhiteSpace(commandString)) { return; }

			commandString = commandString.Trim();

			bool commandStringHasParameters = (commandString.Contains(' '));

			foreach (RuntimeCommand command in RuntimeCommands)
			{
				if (commandString.StartsWith(command.Trigger))
				{

					// Fix translate command being triggered by any other command or custom command that starts with '!t'
					if (command.Trigger == "!t") {
						if (commandString != "!t" && !commandString.StartsWith("!t ")) { continue; }
					}

					if (!command.AcceptsParameters && commandStringHasParameters) { continue; }
					if (command.Command == null)
					{
						connection.Respond(sendingPlayer?.Name ?? null, new List<string>() { "That command has not been implemented yet." }, chatMessage);
						return;
					}
					
					// Send help response
					if (IsHelpQuery(commandString) || (command.Args != null && command.Args.Any(x => x.IsRequired) && commandString.Trim() == command.Trigger)) {
						List<string> response = new List<string>() { commandString };
						response.AddRange(command.HelpStrings);
						connection.Respond(sendingPlayer?.Name ?? null, response, chatMessage);
						return;
					}

					// Trigger command
					else
					{
						command.Command(
											connection, commandString, sendingPlayer, command,
							new ParseResult(connection, commandString, sendingPlayer, command, chatMessage)
						);
					}

					return;
				}
			}

		}

		public static List<RuntimeCommand> RuntimeCommands = new List<RuntimeCommand>()
		{

			#region Help
            new RuntimeCommand()
			{
				Trigger = "!help",
				Name = "help",
				Blurb = "!help: Display help.",
				HelpStrings = new List<string>() {
					"Displays helpful information."
				},
				Description = "Display help.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					// Give the server time to send the default help messages
					Thread.Sleep(1000);
					
					// Commands help
					parseResult.Add("To view custom commands, use \"!commands\".");
					parseResult.Add("For help with a custom command, type the command followed by '-h', '/h', '-help', '/help', '-?', or '/?'.");
					parseResult.Add("Examples: '!pm /h', or '!listMaps -?'");

					// AutoTranslate Help (if enabled)
					if (connection.Settings.AutoTranslateChatMessages)
					{
						if (Translation.EnglishLanguageNameStringsByLanguageCode.ContainsKey(connection.Settings.ServerLanguage ?? "")) {
							string serverLanguageName = Translation.EnglishLanguageNameStringsByLanguageCode[connection.Settings.ServerLanguage];
							parseResult.Add($"AutoTranslate to {serverLanguageName} is ON, messages in other languages will be automatically translated to {serverLanguageName}.");
						}
						else {
							parseResult.Add("AutoTranslate is ON, foreign language chat messages will be automatically translated to the server language.");
						}
						parseResult.Add("To prevent a chat message from being AutoTranslated, start the message with a '&'. Ex:'&hello team'.");
					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);

				}
			},
            #endregion
			#region ToggleAutoTranslate
            new RuntimeCommand()
			{
				Trigger = "!toggleAutoTranslate",
				Name = "toggleAutoTranslate",
				Blurb = "!toggleAutoTranslate: Toggle AutoTranslation",
				HelpStrings = new List<string>() {
					"Toggle AutoTranslation"
				},
				Description = "Toggle AutoTranslation",
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid)
					{
						connection.Settings.AutoTranslateChatMessages = !connection.Settings.AutoTranslateChatMessages;
						parseResult.Add("AutoTranslate is now " + (connection.Settings.AutoTranslateChatMessages ? "ON." : "OFF."));
						try { connection.SaveSettings(); }
						catch (Exception e) {
							parseResult.Add("Exception occured while updating AutoTranslate setting in the server settings database.");
							parseResult.Add("The AutoTranslate setting may not have saved, and may reset the next time this application launches.");
							parseResult.Add("Exception message: " + e.Message);
						}
					}
					else { parseResult.Add("Failed to parse command."); }
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
            #region GameStart
            new RuntimeCommand()
			{
				Trigger = "!gameStart",
				Name = "gameStart",
				Blurb = "!gameStart: Admin Game.Start command.",
				HelpStrings = new List<string>() {
					"Admin Game.Start command."
				},
				Description = "Admin Game.Start command.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					connection.RconCommandQueue.Enqueue(RconCommand.ConsoleLogCommand("Game.Start", parseResult));
					parseResult.Add("Command sent.");
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
            #region GameEnd
            new RuntimeCommand()
			{
				Trigger = "!gameEnd",
				Name = "gameEnd",
				Blurb = "!gameEnd: Admin Game.End command.",
				HelpStrings = new List<string>() {
					"Admin Game.End command."
				},
				Description = "Admin Game.End command.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					connection.RconCommandQueue.Enqueue(RconCommand.ConsoleLogCommand("Game.End", parseResult));
					parseResult.Add("Command sent.");
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region SetMap
			new RuntimeCommand()
			{
				Trigger = "!setMap",
				Name = "setMap",
				Blurb = "!setMap: load a map.",
				HelpStrings = new List<string>() {
					"Admin Game.map command.",
					"Ex: '!setMap chill'."
				},
				Description = "Admin Game.map command.",
				Args = new List<Arg>() {
					new Arg("map", "map name to load", Arg.Type.String)
				},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid && parseResult.Parameters.Count == 1 && parseResult.Parameters[0] != null) {
						string baseMapName = MapVariant.TryGetBaseMapInternalNameFromDisplayName(parseResult.Parameters[0]);
						if (!string.IsNullOrWhiteSpace(baseMapName)) {
							connection.RconCommandQueue.Enqueue(
								RconCommand.ConsoleLogCommand(
								$"Game.map \"{baseMapName}\"",
								$"Game.map \"{baseMapName}\"",
								player?.Name ?? "SERVER"
								)
							);
						}
						else {
							if (parseResult.Parameters[0].Contains('"')) {
								connection.RconCommandQueue.Enqueue(
									RconCommand.ConsoleLogCommand(
										$"Game.map {parseResult.Parameters[0]}",
										$"Game.map {parseResult.Parameters[0]}",
										player?.Name ?? "SERVER"
									)
								);
							}
							else {
								connection.RconCommandQueue.Enqueue(
									RconCommand.ConsoleLogCommand(
										$"Game.map \"{parseResult.Parameters[0]}\"",
										$"Game.map \"{parseResult.Parameters[0]}\"",
										player?.Name ?? "SERVER"
									)
								);
							}
						}						
						parseResult.Add("Command sent.");
					}
					else
					{
						parseResult.Add("Command failed.");
					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region SetGame
			new RuntimeCommand()
			{
				Trigger = "!setGame",
				Name = "setGame",
				Blurb = "!setGame: load a game.",
				HelpStrings = new List<string>() {
					"Admin Game.game command.",
					"Ex: '!setGame Slayer'."
				},
				Description = "Admin Game.gametype command.",
				Args = new List<Arg>() {
					new Arg("game", "gametype name to load", Arg.Type.String)
				},
				Command = (connection, message, player, command, parseResult) =>
				{

					if (parseResult.IsValid && parseResult.Parameters.Count == 1 && parseResult.Parameters[0] != null) {
						string builtInVariant = GameVariant.TryGetBaseGameInternalNameFromDisplayName(parseResult.Parameters[0]);
						if (!string.IsNullOrWhiteSpace(builtInVariant)) {
							connection.RconCommandQueue.Enqueue(
								RconCommand.ConsoleLogCommand(
									$"Game.gametype \"{builtInVariant}\"",
									$"Game.gametype \"{builtInVariant}\"",
									player?.Name ?? "SERVER"
								)
							);
						}
						else {
							if (parseResult.Parameters[0].Contains('"')) {
								connection.RconCommandQueue.Enqueue(
									RconCommand.ConsoleLogCommand(
										$"Game.gametype {parseResult.Parameters[0]}",
										$"Game.gametype {parseResult.Parameters[0]}",
										player?.Name ?? "SERVER"
									)
								);
							}
							else {
								connection.RconCommandQueue.Enqueue(
									RconCommand.ConsoleLogCommand(
										$"Game.gametype \"{parseResult.Parameters[0]}\"",
										$"Game.gametype \"{parseResult.Parameters[0]}\"",
										player?.Name ?? "SERVER"
									)
								);								
							}
						}
						parseResult.Add("Command sent.");
					}
					else
					{
						parseResult.Add("Command failed.");
					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);

				}
			},
			#endregion
			#region ListMaps
			new RuntimeCommand()
			{
				Trigger = "!listMaps",
				Name = "listMaps",
				Blurb = "!listMaps baseMap(optional): list maps",
				HelpStrings = new List<string>() {
					"'!listMaps' - List all maps",
					"'!listMaps Guardian' - List Guardian maps",
					"Options: All, HighGround, Guardian, Valhalla, Narrows, ThePit",
					"Sandtrap, Standoff, LastResort, Icebox, Reactor, Edge, Diamondback"
				},
				Description = "List map variants - '!listMaps', '!listMaps HighGround'",
				AdminCommand = false,
				Args = new List<Arg>()
				{
					new Arg("baseMap", "Name of the base map to list map variants for. Ex: Valhalla, HighGround", Arg.Type.BaseMap, true)
				},
				Command = (connection, message, player, command, parseResult) =>
				{

					Action result = () => { return; };
					if (connection.Settings.CanQueryMaps == false)
					{
						parseResult.Add("The server owner has not specified the path to the map variants directory, so no maps can be listed.");
					}
					else if (parseResult.IsValid)
					{
						if (parseResult.Parameters.Count == 0 || (parseResult.Parameters.Count == 1 && parseResult.Parameters[0] == null))
						{
							result = () => { connection.SendMapDescriptions(MapVariant.BaseMap.All, player?.Name, parseResult.ChatMessage); };
						}
						else if (parseResult.Parameters.Count == 1 && !string.IsNullOrWhiteSpace(parseResult.Parameters[0]))
						{
							result = () => { connection.SendMapDescriptions(parseResult.Parameters[0], player, parseResult.ChatMessage); };
						}
						else
						{
							parseResult.Add("Command Failed: Parse error.");
						}
					}

					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
					result();

				}
			},
			#endregion
			#region ListGames
            new RuntimeCommand()
			{
				Trigger = "!listGames",
				Name = "listGames",
				Blurb = "!listGames baseGame(optional): list games",
				HelpStrings = new List<string>() {
					"'!listGames' - List all games",
					"'!listGames Slayer' - List slayer games",
					"Options: Slayer, Oddball, Assault, CaptureTheFlag",
					"Options: Infection, VIP, KingOfTheHill, Juggernaut"
				},
				Description = "List game variants - '!listGames', '!listGames Slayer'",
				AdminCommand = false,
				Args = new List<Arg>()
				{
					new Arg("baseGame", "Name of the base game to list game variants for. Ex: Slayer, KingOfTheHill", Arg.Type.BaseVariant, true)
				},
				Command = (connection, message, player, command, parseResult) =>
				{

					Action result = () => { return; };

					if (connection.Settings.CanQueryGametypes == false)
					{
						parseResult.Add("The server owner has not specified the path to the game variants directory, so no gametypes can be listed.");
					}
					else if (parseResult.IsValid)
					{
						if (parseResult.Parameters.Count == 0 || (parseResult.Parameters.Count == 1 && parseResult.Parameters[0] == null))
						{
							result = () => { connection.SendGameDescriptions(GameVariant.BaseGame.All, player?.Name, parseResult.ChatMessage); }; 
						}
						else if (parseResult.Parameters.Count == 1 && !string.IsNullOrWhiteSpace(parseResult.Parameters[0]))
						{
							result = () => { connection.SendGameDescriptions(parseResult.Parameters[0], player, parseResult.ChatMessage); };
						}
						else
						{
							parseResult.Add("Command Failed: Parse error.");
						}
					}

					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
					result();

				}
			},
			#endregion
			#region ListStandardGames
            new RuntimeCommand()
			{
				Trigger = "!listStandardGames",
				Name = "listStandardGames",
				Blurb = "!listStandardGames baseGame(optional): list games",
				HelpStrings = new List<string>() {
					"'!listStandardGames' - List all  games",
					"'!listStandardGames baseGame' - List those games",
					"Options: Slayer, Oddball, Assault, CaptureTheFlag",
					"Options: Infection, VIP, KingOfTheHill, Juggernaut"
				},
				Description = "List standard game variants - '!listStandardGames', '!listStandardGames Slayer'",
				AdminCommand = false,
				Args = new List<Arg>()
				{
					new Arg("baseGame", "Name of the base game to list game variants for. Ex: Slayer, KingOfTheHill", Arg.Type.BaseVariant, true)
				},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid)
					{
						// If no parameter or invalid parameter...
						if (parseResult.Parameters.Count == 0 || (parseResult.Parameters.Count == 1 && string.IsNullOrWhiteSpace(parseResult.Parameters[0])))
						{
							parseResult.Add(GameVariant.BuiltInGameVariantDescriptionsWithNames);
						}
						// If has parameter
						else if (parseResult.Parameters.Count > 0 && !string.IsNullOrWhiteSpace(parseResult.Parameters[0]))
						{
							if (GameVariant.BaseGamesByNameLower.TryGetValue(parseResult.Parameters?[0]?.Replace(" ", "").ToLowerInvariant() ?? "", out GameVariant.BaseGame baseGame))
							{
								foreach (GameVariant.BuiltInVariant builtInVariant in GameVariant.BuiltInVariantsByBaseGame[baseGame])
								{
									parseResult.Add(GameVariant.BuiltInVariantDescriptionsWithNamesByBuiltInVariant[builtInVariant]);
								}
							}
							else {
								parseResult.Add("Command Failed: Unrecognized base game.");
							}
						}
						// Else error
						else
						{
							parseResult.Add("Command Failed: Parse error.");
						}
					}
					else
					{
						parseResult.Add("Command Failed: Parse error.");
					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region ReloadMapsAndGames
			new RuntimeCommand()
			{
				Trigger = "!reloadMapsAndGames",
				Name = "reloadMapsAndGames",
				Blurb = "!reloadMapsAndGames: reload variants",
				HelpStrings = new List<string>() {
					"Reload map and game variants."
				},
				Description = "Reload map and game variants.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {
						try { connection.LoadMapVariants(); connection.LoadGameVariants(); }
						catch (Exception e) {
							parseResult.IsValid = false;
							parseResult.Add("Error reloading maps and games.");
							parseResult.Add("Error message: " + e.Message);
						}
						if (parseResult.IsValid) {
							parseResult.Add("Command sent. Maps and Games reloaded successfully.");
						}
					}
					else
					{
						parseResult.Add("Command failed.");
					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region Commands
			new RuntimeCommand()
			{
				Trigger = "!commands",
				Name = "commands",
				Blurb = " !commands: lists commands.",
				HelpStrings = new List<string>() { "Lists commands. Ex: '!commands'" },
				Description = "Lists available commands.",
				Args = new List<Arg>() {},
				AdminCommand = false,
				Command = (connection, message, player, command, parseResult) =>
				{					
					List<string> blurbs = new List<string>() { parseResult.Response[0] };
					bool auth = connection.Settings.AuthorizedUIDs.Contains(player?.Uid ?? "");
					foreach (RuntimeCommand item in RuntimeCommands)
					{
						if ((!item.AdminCommand || auth) && item.IsValidForConnection(connection)) { blurbs.Add(item.Blurb); }
					}
					connection.Respond(player?.Name, blurbs, parseResult.ChatMessage);
				}
			},
            #endregion
            #region Authorize
            new RuntimeCommand()
			{
				Trigger = "!authorize",
				Name = "authorize",
				Blurb = " !authorize: makes player admin.",
				HelpStrings = new List<string>() {
					"Give a player admin authorization.",
					"You'll need the server admin password.",
					"'!authorize BIRD COMMAND password1'",
				},
				Description = "Give a player authorization.",
				Args = new List<Arg>() {
					new Arg("playerName", "Player to authorize.", Arg.Type.PlayerName),
					new Arg("password", "Server admin password.", Arg.Type.String)
				},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid)
					{
						lock (connection.State.ServerStateLock) {
							PlayerInfo match = connection.State.Players.Find(x => x.Name == parseResult.Parameters[0]);
							if (match != null) {
								if (parseResult.Parameters[1] == connection.Settings.RconPassword) {
									connection.Settings.AuthorizedUIDs.Add(match.Uid);
									connection.SaveSettings();
									parseResult.Add("Successfully authorized " + parseResult.Parameters[0] + ".");
								}
								else
								{
									parseResult.Add("Authorization Failed: Incorrect password.");
								}
							}
							else
							{
								parseResult.Add("Failed to authorize " + parseResult.Parameters[0] + ", player not be found.");
							}
						}
					}
					else {parseResult.Add("Authorization failed."); }

					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);					

				},
				AdminCommand = false
			},
            #endregion
            #region SetNextGame
            new RuntimeCommand()
			{
				Attribute = CommandAttribute.DynamicVoteFileCommand,
				Trigger = "!setNextGame",
				Name = "setNextGame",
				Blurb = " !setNextGame: set next game/map",
				HelpStrings = new List<string>() {
                  //-------------------------------------------
                    "Sets the game/map for the next match.",
					"Use '!list-games' to find game options.",
					"Use '!list-maps' to find map options.",
					"Don't use quotation marks."
				},
				Description = "Sets gametype and map for the next match.",
				Args = new List<Arg>() {
					new Arg("game", "The gametype to use.", Arg.Type.VariantName),
					new Arg("map", "The map to use.", Arg.Type.MapName)
				},
				Command = (connection, message, player, command, parseResult) =>
				{

					if (parseResult.IsValid) {

						MatchInfo matchInfo = connection.GetMatchInfo(parseResult.Parameters[0], parseResult.Parameters[1]);

						if (matchInfo.IsValid)
						{
							if (connection.Settings.ServerMatchMode != ServerSettings.MatchMode.Queue) { connection.Command_ActivateMatchQueue(); }
							if (connection.Command_SetNextGameInQueue(parseResult.Parameters[0], parseResult.Parameters[1], player?.Name)) {
								parseResult.Add("Match added to queue.");
							}
							else {
								parseResult.Add("Unable to add game to queue, either the game or map was invalid.");
							}
							connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
							return;
						}
						else
						{
							if (matchInfo.GameVariant == null) { parseResult.Add("Game variant \"" + parseResult.Parameters[0] + "\" could not be found."); }
							if (matchInfo.MapVariant == null) { parseResult.Add(  "Map variant \"" + parseResult.Parameters[1] + "\" could not be found."); }
							parseResult.Add("Invalid match, unable to begin vote.");
							connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						}
					}
					else
					{
						connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
					}

				}
			},
            #endregion
            #region VoteAddGame
            new RuntimeCommand()
			{
				Attribute = CommandAttribute.DynamicVoteFileCommand,
				Trigger = "!voteAddGame",
				Name = "voteAddGame",
				Blurb = " !voteAddGame: vote to enqueue game/map",
				HelpStrings = new List<string>() {
                    "Vote to add game/map to match queue.",
					"Use '!list-games' to find game options.",
					"Use '!list-maps' to find map options.",
					"Don't use quotation marks."
				},
				Description = "Starts a vote for *game on *map to be added to the match queue.",
				Args = new List<Arg>() {
					new Arg("game", "The game to play", Arg.Type.VariantName),
					new Arg("map", "The map to play on", Arg.Type.MapName)
				},
				AdminCommand = false,
				Command = (connection, message, player, command, parseResult) =>
				{

					//if (!connection.Settings.DynamicVotingFileManagement)
					//{
					//	parseResult.Response = new List<string>() { parseResult.Response[0] };
					//	parseResult.AddR("Command Failed: dynamic votefile and related features, including this command, are disabled.");
					//	connection.Respond(player?.Name, parseResult.Response);
					//	return;
					//}

					if (parseResult.IsValid) {

						if (connection.InLobby) {
							parseResult.Add("voteAddGame cannot be used in lobby.");
							connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
							return;
						}

						MatchInfo matchInfo = connection.GetMatchInfo(parseResult.Parameters[0], parseResult.Parameters[1]);

						if (matchInfo.IsValid)
						{

							connection.Broadcast($"{player?.Name ?? "CMD"}: {parseResult.ChatMessage.Text}");
							//connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);

							Thread.Sleep(Connection.ServerMessageDelay);

							connection.Command_BeginVote(
								() => {
									if (connection.Settings.ServerMatchMode != ServerSettings.MatchMode.Queue) { connection.Command_ActivateMatchQueue(); }
									connection.Command_AddGameToQueue(
										parseResult.Parameters[0], 
										parseResult.Parameters[1], 
										player?.Name
								);},
								player,
								"add \"" + parseResult.Parameters[0] + "\" on \"" + parseResult.Parameters[1] + "\" to the match queue",
								new List<string>() /*{ matchInfo.GameVariant.Name + ": " + matchInfo.GameVariant.Description }*/,
								new List<string>() /*{ "\"" + parseResult.Parameters[0] + "\" on \"" + parseResult.Parameters[1] + "\" has been added to the match queue." }*/,
								parseResult.ChatMessage
							);

							return;
						}
						else
						{
							if (matchInfo.GameVariant == null) { parseResult.Add("Game variant \"" + parseResult.Parameters[0] + "\" could not be found."); }
							if (matchInfo.MapVariant == null) { parseResult.Add(  "Map variant \"" + parseResult.Parameters[1] + "\" could not be found."); }
							parseResult.Add("Invalid match, unable to begin vote.");
							connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						}
					}
					else
					{
						connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
					}

				}
			},
            #endregion
            #region MatchQueue
            new RuntimeCommand()
			{
				Attribute = CommandAttribute.DynamicVoteFileCommand,
				Trigger = "!matchQueue",
				Name = "matchQueue",
				Blurb = " !matchQueue: lists queued matches",
				HelpStrings = new List<string>() {
					"Lists all queued matches. Use: '!commands'"
				},
				Description = "Lists all matches in the match queue.",
				Args = new List<Arg>() {},
				AdminCommand = false,
				Command = (connection, message, player, command, parseResult) =>
				{
					
					if (connection.MatchQueue.IsEmpty)
					{
						parseResult.Add(connection.NextQueuedMatchString);
						connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						return;
					}
					else {
						MatchInfo[] reversedMatchQueue = connection.MatchQueue.ToArray().Reverse().ToArray();
						if (reversedMatchQueue.Length != 0)
						{
							int i = reversedMatchQueue.Length;
							foreach( MatchInfo item in reversedMatchQueue )
							{
								if (i == 1) { break; }
								parseResult.Add($"{i}: {item.GameVariant.Name} on {item.MapVariant.Name}");
								i--;
							}
						}
						parseResult.Add($"NEXT: {connection.NextQueuedMatchString}");
						connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						return;
					}

				}
			},
            #endregion
			#region ClearMatchQueue
			new RuntimeCommand()
			{
				Trigger = "!clearMatchQueue",
				Name = "clearMatchQueue",
				Blurb = " !clearMatchQueue: Clears match queue.",
				HelpStrings = new List<string>() {
					"Clears the match queue. Reactivates previously active vote file.",
					
				},
				Description = "Clears match queue.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					connection.Command_ClearMatchQueue();
					parseResult.Add("Match queue has been cleared.");
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region DeactivateMatchQueue
			new RuntimeCommand()
			{
				Trigger = "!deactivateMatchQueue",
				Name = "deactivateMatchQueue",
				Blurb = " !deactivateMatchQueue: Deactivates match queue.",
				HelpStrings = new List<string>() {
					"Deactivates the match queue. Reactivates previously active vote file.",

				},
				Description = "Deactivates match queue.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					connection.Command_DeactivateMatchQueue();
					parseResult.Add("Match queue has been deactivated.");
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region ToggleVotefileEditing
			new RuntimeCommand()
			{
				Trigger = "!toggleVotefileEditing",
				Name = "toggleVotefileEditing",
				Blurb = " !toggleVotefileEditing: En/Disable votefile editing.",
				HelpStrings = new List<string>() { "Grants or denies permission for the server tool to edit votefiles while the game is running." },
				Description = "En/Disable votefile editing.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {

						try {
							if (connection.Command_SetDynamicVoteFileManagement(!connection.Settings.UseLocalFiles)) { 
								parseResult.Add(
									"Successfully toggled votefile editing. Votefile editing is now " 
									+ (connection.Settings.UseLocalFiles ? "ON." : "OFF.")
								);
							}
							else { parseResult.Add("Command error. Failed to toggle votefile editing."); }
						}
						catch(Exception e) {
							parseResult.Add("Error toggling votefile editing, the setting failed to save to the server settings database.");
							parseResult.Add("Votefile editing will remain " + (connection.Settings.UseLocalFiles ? "ON." : "OFF."));
							parseResult.Add("Error message:");
							parseResult.Add(e.Message.Split(124));
						}

					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region EnableVotefileEditing
			new RuntimeCommand()
			{
				Trigger = "!enableVotefileEditing",
				Name = "enableVotefileEditing",
				Blurb = " !enableVotefileEditing: Enable votefile editing.",
				HelpStrings = new List<string>() { "Grants permission for the server tool to edit votefiles while the game is running." },
				Description = "Enable votefile editing.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {

						if (connection.Settings.UseLocalFiles) { 
							parseResult.Add("Votefile editing is enabled."); 
							return; 
						}

						try {
							if (connection.Command_SetDynamicVoteFileManagement(true)) { parseResult.Add("Successfully enabled votefile editing. Votefile editing is now ON."); }
							else { parseResult.Add("Command error. Failed to enable votefile editing."); }
						}
						catch(Exception e) {
							parseResult.Add("Error enabling votefile editing, the setting failed to save to the server settings database.");
							parseResult.Add("Votefile editing will remain " + (connection.Settings.UseLocalFiles ? "ON." : "OFF."));
							parseResult.Add("Error message:");
							parseResult.Add(e.Message.Split(124));
						}

					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region DisableVotefileEditing
			new RuntimeCommand()
			{
				Trigger = "!disableVotefileEditing",
				Name = "disableVotefileEditing",
				Blurb = " !disableVotefileEditing: Disable votefile editing.",
				HelpStrings = new List<string>() { "Prevents the server tool from editing votefiles while the game is running." },
				Description = "Disable votefile editing.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {

						if (!connection.Settings.UseLocalFiles) { 
							parseResult.Add("Votefile editing is disabled."); 
							return; 
						}

						try {
							if (connection.Command_SetDynamicVoteFileManagement(true)) { parseResult.Add("Successfully disabled votefile editing. Votefile editing is now OFF."); }
							else { parseResult.Add("Command error. Failed to disable votefile editing."); }
						}
						catch(Exception e) {
							parseResult.Add("Error disabling votefile editing, the setting failed to save to the server settings database.");
							parseResult.Add("Votefile editing will remain " + (connection.Settings.UseLocalFiles ? "ON." : "OFF."));
							parseResult.Add("Error message:");
							parseResult.Add(e.Message.Split(124));
						}

					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region EnableCommand
			new RuntimeCommand()
			{
				Trigger = "!enableCommand",
				Name = "enableCommand",
				Blurb = " !enableCommand: Enable a command.",
				HelpStrings = new List<string>() { "Sets a saved server command's state to enabled." },
				Description = "Enable a command.",
				Args = new List<Arg>() {new Arg("commandName", "the name of the command to be enabled.", Arg.Type.CommandName, false)},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {
						ToolCommand match = connection.Settings.Commands.First(x => x.Name.StartsWith(parseResult.Parameters[0]));
						if (match != null) {
							connection.Command_SetCommandEnabledState(match, true, parseResult);
						}
						else {
							parseResult.Add("The specified command could not be found.");
						}
					}
					else { parseResult.Add("Command failed."); }
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region DisableCommand
			new RuntimeCommand()
			{
				Trigger = "!disableCommand",
				Name = "disableCommand",
				Blurb = " !disableCommand: Disable a command.",
				HelpStrings = new List<string>() { "Sets a saved server command's state to disabled." },
				Description = "Disable a command.",
				Args = new List<Arg>() {new Arg("commandName", "the name of the command to be disabled.", Arg.Type.CommandName, false)},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {
						ToolCommand match = connection.Settings.Commands.First(x => x.Name.StartsWith(parseResult.Parameters[0]));
						if (match != null) {
							connection.Command_SetCommandEnabledState(match, false, parseResult);
						}
						else {
							parseResult.Add("The specified command could not be found.");
						}
					}
					else { parseResult.Add("Command failed."); }
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region EnableCommandsByTag
			new RuntimeCommand()
			{
				Trigger = "!enableCommandsByTag",
				Name = "enableCommandsByTag",
				Blurb = " !enableCommandsByTag: Enable certain commands",
				HelpStrings = new List<string>() { "Enables all commands matching the specified tag." },
				Description = "Enable certain commands.",
				Args = new List<Arg>() {new Arg("tag", "the tag name of the commands to be enabled.", Arg.Type.CommandName, false)},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {
						ToolCommand match = connection.Settings.Commands.First(x => x.Name.StartsWith(parseResult.Parameters[0]));
						if (match != null) {
							connection.Command_SetCommandEnabledStateByTag(parseResult.Parameters[0], true, parseResult);
						}
						else {
							parseResult.Add("The specified command could not be found.");
						}
					}
					else { parseResult.Add("Command failed."); }
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region DisableCommandsByTag
			new RuntimeCommand()
			{
				Trigger = "!disableCommandsByTag",
				Name = "disableCommandsByTag",
				Blurb = " !disableCommandsByTag: Disable certain commands",
				HelpStrings = new List<string>() { "Disables all commands matching the specified tag." },
				Description = "Disable certain commands.",
				Args = new List<Arg>() {new Arg("tag", "the tag name of the commands to be disabled.", Arg.Type.CommandName, false)},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid) {
						ToolCommand match = connection.Settings.Commands.First(x => x.Name.StartsWith(parseResult.Parameters[0]));
						if (match != null) {
							connection.Command_SetCommandEnabledStateByTag(parseResult.Parameters[0], false, parseResult);
						}
						else {
							parseResult.Add("The specified command could not be found.");
						}
					}
					else { parseResult.Add("Command failed."); }
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region SetVoteFile
			new RuntimeCommand()
			{
				Attribute = CommandAttribute.DynamicVoteFileCommand,
				Trigger = "!setVoteFile",
				Name = "setVoteFile",
				Blurb = " !setVoteFile: sets JSON vote file",
				HelpStrings = new List<string>() {
					"Sets the vote file for the server.",
					"Ex: '!setVoteFile voting.json'"
				},
				Description = "Sets current voting JSON file.",
				Args = new List<Arg>() {
					new Arg("voteFile", "The name of the voting file.", Arg.Type.FileNameJSON)
				},
				Command = async (connection, message, player, command, parseResult) =>
				{

					if (parseResult.IsValid) {
						parseResult.Add("VoteFile change request sent.");
						if (await connection.SetActiveVotingJson(message)) {
							parseResult.Add("VoteFile successfully loaded.");
						}
						else {
							parseResult.Add("Failed to load VoteFile. The file may not exist, or another error may have occured.");
						}
					}
					else {
						parseResult.Add("Command failed - invalid syntax or arguments.");
					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
					return;

				}
			},
            #endregion
            #region Message
            new RuntimeCommand()
			{
				Trigger = "!pm",
				Name = "pm",
				Blurb = " !pm: privately message a player",
				HelpStrings = new List<string>() {
					"!pm: privately message a player",
					"Ex: '!pm BIRD COMMAND what's up?'"
				},
				AdminCommand = false,
				Description = "Privately messages a player",
				Args = new List<Arg>() {
					new Arg("player", "name of the player to message.", Arg.Type.PlayerName),
					new Arg("message", "the message to send the player.", Arg.Type.String)
				},
				Command = (connection, message, player, command, parseResult) =>
				{

					if (parseResult.IsValid)
					{

						try {
							PlayerInfo messagedPlayer = connection.State.Players.Find(x => x.Name == (parseResult?.Parameters[0]));
							connection.ReplyCommandPlayers[messagedPlayer?.Uid ?? ""] = player?.Name ?? "";
							//if (messagedPlayer != null) { messagedPlayer.ReplyPlayer = player?.Name ?? ""; }
							//else { connection.Broadcast("Failed to find MessagedPlayer, so can't set ReplyPlayer"); }
						} catch { /*connection.Broadcast("Failed to set ReplyPlayer");*/ }

						string playerName = player?.Name ?? "SERVER";
						parseResult.Response.Clear();
						parseResult.Add("(@" + playerName  + ")>>[" + parseResult.Parameters[0] + "]:");
						parseResult.Add(parseResult.Parameters[1]);
						connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						connection.Respond(
							parseResult.Parameters[0], 
							new List<string>() {"[@" + parseResult.Parameters[0] + "]<<(" + playerName + "):", parseResult.Parameters[1]},
							parseResult.ChatMessage
						);

					}
					else { connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage); }

				}
			},
			#endregion
			#region Reply
            new RuntimeCommand()
			{
				Trigger = "!r",
				Name = "r",
				Blurb = " !r: reply to private message",
				HelpStrings = new List<string>() {
					"!r: reply to private message",
					"Ex: '!r please stop camping'"
				},
				AdminCommand = false,
				Description = "Reply to private message",
				Args = new List<Arg>() {
					new Arg("message", "the message to send the player.", Arg.Type.String)
				},
				Command = (connection, message, player, command, parseResult) =>
				{
					//connection.Broadcast($"reply command triggered|{player?.ReplyPlayer??"_"}");
					if (parseResult.IsValid && connection.ReplyCommandPlayers.ContainsKey(player?.Uid ?? ""))
					{

						//connection.Respond(player?.Name, $"|{player?.ReplyPlayer??"_"}|{parseResult?.Parameters[0] ?? "_"}");
						
						string playerName = player?.Name ?? "SERVER";

						string replyUid = connection.State?.Players?.Find(x => x.Name == connection.ReplyCommandPlayers[player?.Uid ?? ""])?.Uid ?? "";
						connection.ReplyCommandPlayers[replyUid] = playerName;

						parseResult.Response.Clear();
						parseResult.Add("(@" + playerName  + ")>>[" + connection.ReplyCommandPlayers[player?.Uid ?? ""] + "]:");
						parseResult.Add(parseResult.Parameters[0]);
						connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						connection.Respond(
							connection.ReplyCommandPlayers[player?.Uid ?? ""],
							new List<string>() {"[@" + connection.ReplyCommandPlayers[player?.Uid ?? ""] + "]<<(" + playerName + "):",
								parseResult.Parameters[0]},
							parseResult.ChatMessage
						);
					}
					else { connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage); }

				}
			},
			#endregion
			#region Translate
			new RuntimeCommand()
			{
				Trigger = "!t",
				Name = "translate",
				Blurb = " !t, !t-aa: translation",
				HelpStrings = new List<string>() {
					"Translate a message.",
					"'!t' translate to server language.",
					"'!t-aa' translate to language aa.",
					//"'!t-aa-bb' translate from aa to bb.",
					"ISO 639-1: en, es, fr, de, ..."
				},
				Description = "Translate a message.",
				AdminCommand = false,
				Args = new List<Arg>() {
					new Arg("language code", "Translation language specifier", Arg.Type.LanguageCode, true),
					new Arg("message", "The message to translate", Arg.Type.String)
				},
				Command = (connection, message, player, command, parseResult) =>
				{

					if (parseResult.IsValid) {

						if (connection.CanTranslate) {

							Message chatMessage = parseResult.ChatMessage;

							string languageCode = connection.Settings.ServerLanguage;

							// Verify language code parameter
							if (parseResult.Parameters[0] != null)
							{
								if (Translation.IsValidLanguageCode(parseResult.Parameters[0]))
								{
									languageCode = parseResult.Parameters[0];
								}
								else
								{
									parseResult.Add("Translation Failed: Invalid language code.");
									connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
									return;
								}
							}

							// Verify message parameter
							if (string.IsNullOrWhiteSpace(parseResult.Parameters[1]))
							{
								parseResult.Add("Translation Failed: No message.");
								connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
								return;
							}

							string originalMessageText = chatMessage.Text;
							string textToTranslate = parseResult.Parameters[1];
							chatMessage.Text = textToTranslate;

							// Get translation
							connection.TranslateChatMessage(chatMessage, languageCode, false);

							// If translation exists, create server-message response
							List<string> broadcast = new List<string>();
							string translation = chatMessage.Translation.GetTranslation(languageCode);
							if (!string.IsNullOrWhiteSpace(translation))
							{

								if ($"{player?.Name ?? "SERVER"} :[!t-{languageCode}]({chatMessage.DetectedLanguage}) {textToTranslate}".Length < 124 )
								{
									broadcast.Add($"{player?.Name ?? "SERVER"} :[!t-{languageCode}]({chatMessage.DetectedLanguage}) {textToTranslate}");
								}
								else
								{
									broadcast.AddRange($"{player?.Name ?? "SERVER"} :[!t-{languageCode}]({chatMessage.DetectedLanguage}) {textToTranslate}".Split(124));
								}

								if ($"{player?.Name ?? "SERVER"} :[!t-{languageCode}] {translation}".Length < 124)
								{
									broadcast.Add($"{player?.Name ?? "SERVER"} :[!t-{languageCode}] {translation}");
								}
								else
								{
									broadcast.AddRange($"{player?.Name ?? "SERVER"} :[!t-{languageCode}] {translation}".Split(124));
								}

								parseResult.Response.Clear();

							}
						
							// Else, respond translation failed
							else
							{
								parseResult.Add("Translation Failed: Empty translation returned.");
							}

							//parseResult.ChatMessage.Message = originalMessageText;
							chatMessage.Text = originalMessageText;

							// Send responses to player first
							if ((parseResult.Response?.Count ?? 0) > 0) {
								connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
								//Thread.Sleep(100);
							}
							// Send responses to server second
							if (broadcast.Count > 0) { 
								connection.Respond(null, broadcast, parseResult.ChatMessage); 
							}

						}
						else {
							parseResult.Add("Server must provide an API Key to enable Google Translate support. Info at https://github.com/BIRD-COMMAND/RconTool/");
							connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						}

					}

				}
			},
			#endregion
			#region TranslationUsage
            new RuntimeCommand()
			{
				Trigger = "!translationUsage",
				Name = "translationUsage",
				Blurb = "!translationUsage: Translation character count.",
				HelpStrings = new List<string>() {
					"Checks the Google Translation character count (as tracked by the app)."
				},
				Description = "Translation character count.",
				Args = new List<Arg>() {},
				Command = (connection, message, player, command, parseResult) =>
				{
					parseResult.Add($"Note: Translation is {(connection.CanTranslate ? "" : "not")} currently enabled.");
					parseResult.Add($"Translated character count: {App.TranslatedCharactersThisBillingCycle}");
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
            #endregion
			#region DebugOnMessageEvents
			//new RuntimeCommand()
			//{
			//	Trigger = "!debugOnMessageEvents",
			//	Name = "debugOnMessageEvents",
			//	Blurb = "!debugOnMessageEvents",
			//	HelpStrings = new List<string>() {
			//		"Admin - debug command."
			//	},
			//	Description = "Admin debug command.",
			//	Args = new List<Arg>() {},
			//	Command = (connection, message, player, command, parseResult) =>
			//	{
			//		connection.Settings.DebugOnMessageEvents = !connection.Settings.DebugOnMessageEvents;
			//		parseResult.AddR($"DebugOnMessageEvents->{connection.Settings.DebugOnMessageEvents}");
			//		connection.Respond(player?.Name, parseResult.Response);
			//	}
			//},
			#endregion
			#region ServerCommand
			new RuntimeCommand()
			{
				Trigger = "!c",
				Name = "c",
				Blurb = "!c: Issue a raw server command",
				HelpStrings = new List<string>() {
					"Server command interface. Example: '!c Game.Stop'"
				},
				Description = "Issue a raw server command",
				Args = new List<Arg>() {new Arg("command", "Raw server command to be executed", Arg.Type.String, false)},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid)
					{
						if ((parseResult.Parameters?.Count ?? 0) > 0 && !string.IsNullOrWhiteSpace(parseResult.Parameters[0])) {
							connection.RconCommandQueue.Enqueue(RconCommand.ChatLogCommand(parseResult.Parameters[0], parseResult));
							parseResult.Add("Command sent.");
						}
						else
						{
							parseResult.Add("Invalid or missing command parameter, unable to issue command.");
						}
					}
					else
					{
						parseResult.Add("Failed to parse command, make sure you're issuing a valid server command.");
					}
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region AddAutoTranslateIgnoredPhrase
            new RuntimeCommand()
			{
				Trigger = "!ignorePhrase",
				Name = "ignorePhrase",
				Blurb = "!ignorePhrase: Add a translation-ignored phrase",
				HelpStrings = new List<string>() {
					"Add a phrase that will be ignored by AutoTranslation"
				},
				Description = "Add a translation-ignored phrase",
				Args = new List<Arg>() {new Arg("phrase","the phrase which will be converted to lower case and added to the AutoTranslateIgnoredPhrases list", Arg.Type.String, false)},
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid && (parseResult.Parameters?.Count ?? 0) > 0 && !string.IsNullOrWhiteSpace(parseResult.Parameters[0]))
					{
						connection.Settings.AutoTranslateIgnoredPhrasesList.Add(parseResult.Parameters[0]);
						bool errorAdding = false;
						try { connection.SaveSettings(); }
						catch (Exception e) { 
							parseResult.Add("Exception occured while adding phrase to the database - the phrase may not have been added:");
							parseResult.Add(e.Message.Split(124));
							errorAdding = true;
						}
						if (errorAdding == false)
						{
							parseResult.Add("Successfully added the phrase to the ignored phrases list");
						}
					}
					else { parseResult.Add("Failed to parse command."); }
					connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
				}
			},
			#endregion
			#region SetTeam
            new RuntimeCommand()
			{
				Trigger = "!setTeam",
				Name = "setTeam",
				Blurb = " !setTeam: set a player's team",
				HelpStrings = new List<string>() {
					"!setTeam: set a player's team",
					"Ex: '!setTeam BIRD COMMAND 0'",
					"Ex: '!setTeam BIRD COMMAND red'"
				},
				AdminCommand = true,
				Description = "Sets a player's team.",
				Args = new List<Arg>() {
					new Arg("player", "name of the player to message.", Arg.Type.PlayerName),
					new Arg("team", "index or color of team", Arg.Type.String)
				},
				Command = (connection, message, player, command, parseResult) =>
				{

					if (parseResult.IsValid && connection.ServerHookActive)
					{

						bool canSetTeam = false; int teamIndex = -1;
						PlayerInfo setPlayer = connection.State.Players.Find(x => x.Name == (parseResult?.Parameters[0]));
						if (setPlayer == null) { parseResult.Add("SetTeam Failed: invalid player."); }
						else {

							// try to get team passed as index
							if (int.TryParse(parseResult.Parameters[1] ?? "", out teamIndex)) {
								if		(teamIndex < 0) { teamIndex = 0; } 
								else if (teamIndex > 7) { teamIndex = 7; }
								canSetTeam = true;
							}
							// try to get team passed as color
							else {
								if (!string.IsNullOrWhiteSpace(parseResult.Parameters[1]) && parseResult.Parameters[1].Length > 2) {
									string argLower = parseResult.Parameters[1].ToLowerInvariant();
									if (argLower.StartsWith("red"))			{ teamIndex = 0; canSetTeam = true; }
									else if (argLower.StartsWith("blue"))	{ teamIndex = 1; canSetTeam = true; }
									else if (argLower.StartsWith("green"))	{ teamIndex = 2; canSetTeam = true; }
									else if (argLower.StartsWith("orange")) { teamIndex = 3; canSetTeam = true; }
									else if (argLower.StartsWith("purple")) { teamIndex = 4; canSetTeam = true; }
									else if (argLower.StartsWith("gold"))	{ teamIndex = 5; canSetTeam = true; }
									else if (argLower.StartsWith("brown"))	{ teamIndex = 6; canSetTeam = true; }
									else if (argLower.StartsWith("pink"))	{ teamIndex = 7; canSetTeam = true; }
								}
							}

							// no valid team index found
							if (!canSetTeam) { parseResult.Add("SetTeam Failed: invalid team."); }

						}

						connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);

						if (canSetTeam) { connection.SetPlayerTeam(setPlayer, teamIndex); }

					}
					else { connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage); }

				}
			},
			#endregion
			#region ForceBalanceTeams
            new RuntimeCommand()
			{
				Trigger = "!forceBalanceTeams",
				Name = "forceBalanceTeams",
				Blurb = " !forceBalanceTeams: try to balance teams",
				HelpStrings = new List<string>() {
					"!forceBalanceTeams: try to balance teams",
					"adjusts teams using k/d ratios"
				},
				AdminCommand = true,
				Description = "Adjusts teams using k/d ratios.",
				Args = new List<Arg>() { new Arg("teamCount", "number of teams", Arg.Type.String, true) },
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid && connection.ServerHookActive && connection.HasPlayers)
					{

						List<Tuple<PlayerInfo,int>> teamAssignments;

						int numTeams = -1; // Handle optional numTeams arg
						if (parseResult.Parameters.Count > 0 && int.TryParse(parseResult.Parameters[0]?.Trim() ?? connection.PopulatedTeamsString, out numTeams) && numTeams > 1 && numTeams < 8) {
							teamAssignments = connection.GenerateBalancedTeamList(numTeams);
						}
						else {
							teamAssignments = connection.GenerateBalancedTeamList(-1);
						}

						if (teamAssignments == null) {
							parseResult.Add("!forceBalanceTeams Failed: unable to generate balanced team list.");
							connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
						}
						else {
							connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage);
							connection.SetPlayerTeam("Teams have been balanced.", teamAssignments.ToArray());
						}
					}
					else { connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage); }
				}
			},
			#endregion
			#region BalanceTeams
            new RuntimeCommand()
			{
				Trigger = "!balanceTeams",
				Name = "balanceTeams",
				Blurb = " !balanceTeams: vote to balance teams",
				HelpStrings = new List<string>() {
					"!balanceTeams: vote to balance teams",
					"adjusts teams using k/d ratios"
				},
				AdminCommand = false,
				Description = "vote to balance using k/d ratios.",
				Args = new List<Arg>() { new Arg("teamCount", "number of teams", Arg.Type.String, true) },
				Command = (connection, message, player, command, parseResult) =>
				{
					if (parseResult.IsValid && connection.ServerHookActive && connection.HasPlayers)
					{

						List<Tuple<PlayerInfo,int>> teamAssignments;

						int numTeams = -1; // Handle optional numTeams arg
						if (parseResult.Parameters.Count > 0 && int.TryParse(parseResult.Parameters[0]?.Trim() ?? connection.PopulatedTeamsString, out numTeams) && numTeams > 1 && numTeams < 8) {
							teamAssignments = connection.GenerateBalancedTeamList(numTeams);
						}
						else {
							teamAssignments = connection.GenerateBalancedTeamList(-1);
						}

						if (teamAssignments != null) {
							// if we're balancing into a different number of teams
							if ((connection.State?.Players?.Select(x => x.Team).Distinct().ToList().Count ?? 0) != 
									    teamAssignments.Select(x => x.Item2).Distinct().ToList().Count)
							{
								connection.Broadcast($"{player?.Name ?? "CMD"}: {parseResult.ChatMessage.Text}");
								Thread.Sleep(Connection.ServerMessageDelay);

								connection.Command_BeginVote(
									() => { connection.SetPlayerTeam("Teams have been balanced.", teamAssignments.ToArray()); },
									player, $"balance the teams into {numTeams} teams.", new List<string>(), new List<string>(), parseResult.ChatMessage
								);
							}
							// if we're balancing into the same number of teams
							else {
								connection.Broadcast($"{player?.Name ?? "CMD"}: {parseResult.ChatMessage.Text}");
								Thread.Sleep(Connection.ServerMessageDelay);

								connection.Command_BeginVote(
									() => { connection.SetPlayerTeam("Teams have been balanced.", teamAssignments.ToArray()); },
									player, "balance the teams.", new List<string>(), new List<string>(), parseResult.ChatMessage
								);
							}
						}
						else { connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage); }

					}
					else { connection.Respond(player?.Name, parseResult.Response, parseResult.ChatMessage); }
				}
			},
			#endregion

		};

		public string Name { get; set; } = "";
		public string Trigger { get; set; } = "";
		public List<string> HelpStrings { get; set; } = new List<string>();
		public string Description { get; set; } = "";
		public string Blurb { get; set; } = "";
		public List<Arg> Args { get; set; } = new List<Arg>();
		public Action<Connection, string, PlayerInfo, RuntimeCommand, ParseResult> Command { get; set; }
		public CommandAttribute Attribute { get; set; } = CommandAttribute.None;
		public bool AdminCommand { get; set; } = true;
		public bool AcceptsParameters { get; set; } = true;
		
		/// <summary>
		/// Returns true if the command's attributes are permitted on the specified connection.
		/// </summary>
		public bool IsValidForConnection(Connection connection)
		{

			if (HasFlag(CommandAttribute.DynamicVoteFileCommand) && !connection.Settings.UseLocalFiles) { return false; }

			return true;

		}

		/// <summary>
		/// Returns true if the help request is for this command and not any sub-commands or arguments.
		/// </summary>
		/// <param name="message">The help request, with the initial "!!help " substring removed.</param>
		private bool IsDefaultHelpRequest(string message)
		{
			return (message.Trim() == Name);
		}
		
		/// <summary>
		/// Returns true if the help request is for one of this command's arguments.
		/// </summary>
		/// <param name="message">The help request, with the initial "!!help " substring removed.</param>
		private bool IsArgHelpRequest(string message)
		{

			if (Args.Count == 0) { return false; }

			foreach (Arg arg in Args)
			{
				if (message == arg.Name)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Retrieves help strings for this command based on the arguments (if any) in the message string.
		/// <para>The calling RuntimeCommand should remove its Trigger/Name from the message string before it's passed to this method.</para>
		/// </summary>
		public List<string> GetHelp(string message)
		{

			List<string> result = new List<string>();

			bool isDefault = IsDefaultHelpRequest(message);

			message = message.TrimStart(Name.Length).Trim();
			if (message.Length == 0) { isDefault = true; }

			if (isDefault)
			{
				// Add Description
				result.Add(Description);

				// Add Args
				foreach (Arg item in Args)
				{
					result.Add(item.Name + ": " + item.Description);
				}

				// Add Help Strings
				result.AddRange(HelpStrings);

				return result;
			}

			if (IsArgHelpRequest(message))
			{
				Arg match = Args.Find(x => x.Name == message);
				if (match != null)
				{
					result.Add(match.Name + ": " + match.Description);
					return result;
				}
			}
			else
			{
				result.Add("Error finding parameter '" + message + "' within the '" + Name + "' command. Sending help for the '" + Name + "' command.");
				foreach (Arg item in Args) { result.Add(item.Name + ": " + item.Description); }
				result.AddRange(HelpStrings);
				return result;
			}

			result.Add("There was an error processing the command.");
			return result;

		}

		#region Flags Helpers

		public void SetFlag(CommandAttribute attr)
		{
			Attribute |= attr;
		}
		public void UnsetFlag(CommandAttribute attr)
		{
			Attribute &= (~attr);
		}
		public void ToogleFlag(CommandAttribute attr)
		{
			Attribute ^= attr;
		}
		public bool HasFlag(CommandAttribute attr)
		{
			// Works with "None" as well
			return (Attribute & attr) == attr;
		}

		#endregion

		[Flags]
		public enum CommandAttribute
		{
			None = 0,
			DynamicVoteFileCommand = 1
		}

	}

}
