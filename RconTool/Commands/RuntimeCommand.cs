using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RconTool
{
	public class RuntimeCommand
	{

		//TODO add some helper methods to further generalize the process of parsing commands/arguments, see below
		// create and ArgType enum for things like player name, map name, variant name, arg name, etc.
		// then a ParseArgs method could automatically parse args for any command
		// as long as it knows how many args it's looking for and their types

		public static void TryRunCommand(string commandString, PlayerInfo sendingPlayer, Connection connection)
		{

			if (sendingPlayer == null) { return; }

			if (commandString.Length == 0 || string.IsNullOrWhiteSpace(commandString)) { return; }

			bool commandStringHasParameters = (commandString.Trim().Contains(' '));

			foreach (RuntimeCommand command in RuntimeCommands)
			{
				if (commandString.StartsWith(command.Trigger))
				{
					if (!command.AcceptsParameters && commandStringHasParameters) { continue; }
					if (command.Command == null)
					{
						connection.Respond(sendingPlayer.Name, "That command has not been implemented yet.");
						return;
					}
					command.Command(connection, commandString, sendingPlayer, command);
					return;
				}
			}

		}

		public static List<RuntimeCommand> RuntimeCommands = new List<RuntimeCommand>()
		{

            #region not implemented
            //         //EnableTimedCommand
            //new RuntimeCommand()
            //         {
            //             Trigger = "!EnableTimedCommand",
            //             Name = "EnableTimedCommand",
            //             Blurb = " !EnableTimedCommand ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> { new Arg("commandName", "commandName: The name of the command to enable. \"Use Quotes\", Case-sensitive.") },
            //             Command = null
            //         },
            //         //EnableConditionalCommand
            //new RuntimeCommand()
            //         {
            //             Trigger = "!EnableConditionalCommand",
            //             Name = "EnableConditionalCommand",
            //             Blurb = " !EnableConditionalCommand ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> { new Arg("commandName", "commandName: The name of the command to enable. \"Use Quotes\", Case-sensitive.") },
            //             Command = null
            //         },
            //         //EnableCommandsByTag
            //new RuntimeCommand()
            //         {
            //             Trigger = "!EnableCommandsByTag",
            //             Name = "EnableCommandsByTag",
            //             Blurb = " !EnableCommandsByTag ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> { new Arg("tag", "tag: The tag that will be searched for. Commands tagged with this word will be enabled. \"Use Quotes\", Case-sensitive.") },
            //             Command = null
            //         },
            //         //DisableTimedCommand
            //new RuntimeCommand()
            //         {
            //             Trigger = "!DisableTimedCommand",
            //             Name = "DisableTimedCommand",
            //             Blurb = " !DisableTimedCommand ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> { new Arg("commandName", "commandName: The name of the command to disable. \"Use Quotes\", Case-sensitive.") },
            //             Command = null
            //         },
            //         //DisableConditionalCommand
            //new RuntimeCommand()
            //         {
            //             Trigger = "!DisableConditionalCommand",
            //             Name = "DisableConditionalCommand",
            //             Blurb = " !DisableConditionalCommand ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> { new Arg("commandName", "commandName: The name of the command to disable. \"Use Quotes\", Case-sensitive.") },
            //             Command = null
            //         },
            //         //DisableCommandsByTag
            //new RuntimeCommand()
            //         {
            //             Trigger = "!DisableCommandsByTag",
            //             Name = "DisableCommandsByTag",
            //             Blurb = " !DisableCommandsByTag ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> { new Arg("tag", "tag: The tag that will be searched for. Commands tagged with this word will be disabled. \"Use Quotes\", Case-sensitive.") },
            //             Command = null
            //         },
            //         //SetNextGame
            //new RuntimeCommand()
            //         {
            //             Trigger = "!SetNextGame",
            //             Name = "SetNextGame",
            //             Blurb = " !SetNextGame ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command.",
            //             Args = new List<Arg>
            //             {
            //                 new Arg("game", "game: The name of the game variant to use. \"Use Quotes\". Case-sensitive."),
            //                 new Arg("map", "map: The name of the default map or map variant to use. \"Use Quotes\". Case-sensitive.")
            //             },
            //             Command = null
            //         },
            //         //EnableDynamicVoteFiles
            //new RuntimeCommand()
            //         {
            //             Trigger = "!EnableDynamicVoteFiles",
            //             Name = "EnableDynamicVoteFiles",
            //             Blurb = " !EnableDynamicVoteFiles ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> {},
            //             Command = null
            //         },
            //         //DisableDynamicVoteFiles
            //new RuntimeCommand()
            //         {
            //             Trigger = "!DisableDynamicVoteFiles",
            //             Name = "DisableDynamicVoteFiles",
            //             Blurb = " !DisableDynamicVoteFiles ",
            //             HelpStrings = new List<string>() {},
            //             Description = "Admin Command. ",
            //             Args = new List<Arg> {},
            //             Command = null
            //         },
            #endregion

            #region StartGame
            new RuntimeCommand()
			{
				Trigger = "!!StartGame",
				Name = "StartGame",
				Blurb = " !!StartGame ",
				HelpStrings = new List<string>() {},
				Description = "Admin Command. ",
				Args = new List<Arg> {},
				Command = (connection, message, player, command) =>
				{
					connection.SendToRcon("Game.Start");
				}
			},
            #endregion
            #region EndGame
            new RuntimeCommand()
			{
				Trigger = "!!EndGame",
				Name = "EndGame",
				Blurb = " !!EndGame ",
				HelpStrings = new List<string>() {},
				Description = "Admin Command. ",
				Args = new List<Arg> {},
				Command = (connection, message, player, command) =>
				{
					connection.SendToRcon("Game.End");
				}
			},
            #endregion
            #region Help
            new RuntimeCommand()
			{
				Trigger = "!!help",
				Name = "help",
				Blurb = " !!help: shows help for items.",
				HelpStrings = new List<string>()
				{
					"'!commands' to see all commands.",
					"'!!help *command' for help with a command.",
					"'!!help list' helps with '!list'",
					"'!!help list maps' helps with '!list maps'",
					"*'s, like on '*baseMap' indicate options.",
					"Words like *baseMap are your choice.",
					"  CORRECT '!list maps Valhalla'",
					"INCORRECT '!list maps *baseMap'",
					"For help with a *parameter, do this:",
					"  CORRECT '!!help list maps baseMap'",
					"INCORRECT '!!help list maps *baseMap'",
				},
				Description = "!!help : Provides detailed information about the command, subcommand, or parameter, submitted.",
				Args = new List<Arg>(),
				AdminCommand = false,
				Command = (connection, message, player, command) =>
				{
                    
                    // Finds and sends help for the specified item

                    // Echo Command
                    // Echo help request back to player, because this happens no matter what
                    // it's the easiest and most effective way to give context for the help received
                    // it also gives the player a chance to notice typos in what they've sent                    
					
					List<string> result = new List<string>() { message };

					string trigger = command.Trigger;

					if (
						message.Trim() == trigger ||
						message.Trim().ToLowerInvariant() == trigger ||
						message.Trim().TrimStart(trigger.Length).Trim().Length == 0
					)
					{
						result.AddRange(command.HelpStrings);
						connection.Respond(player.Name, result);
						return;
					}

                    // Remove "!!help " from string
                    message = message.Trim().TrimStart(trigger.Length).Trim();

					foreach (RuntimeCommand item in RuntimeCommands)
					{
						if (message.StartsWith(item.Name)) {
							result.AddRange(item.GetHelp(message));
							connection.Respond(player.Name, result);
							return;
						}
					}

					result.Add("Oof, '!!help' command encountered an unknown error. Sorry!");
					connection.Respond(player.Name, result);
					return;

				}
			},
            #endregion
            #region List
            new RuntimeCommand()
			{
				Trigger = "!list",
				Name = "list",
				Blurb = " !list: lists available items.",
				HelpStrings = new List<string>()
				{
					"Lists available items of that type.",
					"'!!help list *subCommand' for specific help:",
					"'!!help list games' helps with '!list games'",
					"------------------------------------------",
					"'!list games'",
					"Lists all available game variants.",
					"'!list games *baseGame'->'!list games Slayer'",
					"Lists all variants of the base game.",
					"'!list maps'",
					"Lists all available map variants",
					"'!list maps *baseMap'->'!list maps ThePit'",
					"Lists all variants of the base map.",
					"'!list commands'",
					"Lists all available commands."
				},
				Description = "",
				Args = new List<Arg> {},
				AdminCommand = false,
				Command = (connection, message, player, command) =>
				{

                    // Echo Command in response
                    List<string> result = new List<string>() { Echo(message, player) };
					string trigger = command.Trigger;

					if (
						message.Trim() == command.Trigger ||
						message.Trim().ToLowerInvariant() == command.Trigger ||
						message.Trim().TrimStart(trigger.Length).Trim().Length == 0
					)
					{
						result.AddRange(command.HelpStrings);
						connection.Respond(player.Name, result);
						return;
					}

					message = message.Trim().TrimStart(trigger.Length).Trim();

					bool paramsProvided = (message.Split(new char[] {' '}).Length != 1);
					foreach (RuntimeCommand item in command.SubCommands)
					{
						if (!item.AcceptsParameters && paramsProvided) { continue; }
						if (message.StartsWith(item.Name)) {
							new Thread(new ThreadStart(() => {
								connection.Respond(player.Name, result);
								Thread.Sleep(Connection.ServerMessageDelay);
								item.Command(connection, message, player, item);
							})).Start();
							return;
						}
					}

					result.Add("Oof, '!list' command encountered an unknown error. Sorry!");
					connection.Respond(player.Name, result);
					return;

				},
				SubCommands = new List<RuntimeCommand>()
				{
                    // maps
                    new RuntimeCommand()
					{
						Trigger = "maps",
						Name = "maps",
						Blurb = " !maps ",
						HelpStrings = new List<string>() {
							"Options: All, HighGround, Guardian, Valhalla, Narrows, ThePit",
							"Sandtrap, Standoff, LastResort, Icebox, Reactor, Edge, Diamondback"
						},
						Description = "Lists available map variants. Ex: '!list maps All', or '!list maps HighGround'",
						Args = new List<Arg>()
						{
							new Arg("baseMap", "Name of the base map to list map variants for. Ex: Valhalla, HighGround, All.")
						},
						Command = (connection, message, player, command) =>
						{
							message = message.TrimStart(command.Name.Length).Trim();
							if (string.IsNullOrWhiteSpace(message) || message.ToLowerInvariant() == "all") {
								connection.SendMapDescriptions(MapVariant.BaseMap.All, player.Name);
							}
							else {
								connection.SendMapDescriptions(message, player);
							}
						}
					},
                    // games
                    new RuntimeCommand()
					{
						Trigger = "games",
						Name = "games",
						Blurb = " !games ",
						HelpStrings = new List<string>() {
							"Options: Slayer, Oddball, Assault, CaptureTheFlag",
							"Options: Infection, VIP, KingOfTheHill, Juggernaut"
						},
						Description = "Lists available game variants of the given base gametype. Ex: '!list games Oddball', '!list games CaptureTheFlag'",
						Args = new List<Arg>()
						{
							new Arg("baseGame", "baseGame: The gametype to list variants for. Ex: Slayer, Assault, CaptureTheFlag.")
						},
						Command = (connection, message, player, command) =>
						{
							message = message.TrimStart(command.Name.Length).Trim();
							if (string.IsNullOrWhiteSpace(message) || message.ToLowerInvariant() == "all") {
								connection.SendGameDescriptions(GameVariant.BaseGame.All, player.Name);
							}
							else {
								connection.SendGameDescriptions(message, player);
							}
						}
					},
                    // commands
                    new RuntimeCommand()
					{
						Trigger = "commands",
						Name = "commands",
						Blurb = " !commands ",
						Description = "Lists all available commands. Ex: '!list commands'",
						Command = (connection, message, player, command) =>
						{
							List<string> blurbs = new List<string>();
							bool auth = connection.Settings.AuthorizedUIDs.Contains(player.Uid);
							foreach (RuntimeCommand item in RuntimeCommands)
							{
								if ((!item.AdminCommand || auth) && item.IsValidForConnection(connection)) { blurbs.Add(item.Blurb); }
							}
							connection.Respond(player.Name, blurbs);
						}
					}

				}
			},
            #endregion
            #region Commands
            new RuntimeCommand()
			{
				Trigger = "!commands",
				Name = "commands",
				Blurb = " !commands: lists all commands.",
				HelpStrings = new List<string>() { "Lists all available commands. Use: '!commands'" },
				Description = "Lists all available commands.",
				Args = new List<Arg> {},
				AdminCommand = false,
				Command = (connection, message, player, command) =>
				{
					List<string> blurbs = new List<string>();
					bool auth = connection.Settings.AuthorizedUIDs.Contains(player.Uid);
					foreach (RuntimeCommand item in RuntimeCommands)
					{
						if ((!item.AdminCommand || auth) && item.IsValidForConnection(connection)) { blurbs.Add(item.Blurb); }
					}
					connection.Respond(player.Name, blurbs);
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
					"'!authorize 'BIRD COMMAND' asdfghjkl'",
				},
				Description = "Give a player authorization.",
				Args = new List<Arg> {
					new Arg("playerName", "Player to authorize. \"Quotes Required\"."),
					new Arg("password", "Server authorization password.")
				},
				Command = (connection, message, player, command) =>
				{

					List<string> result = new List<string>() { Echo(message, player) };

					message = message.TrimStart(command.Trigger.Length).Trim();
					string[] args = message.Split(new char[] {'"'}, StringSplitOptions.RemoveEmptyEntries);
					string name = "";
					string pw = "";

					if (args.Length < 2) {
						result.Add("Invalid authorization argument count. Requires a player name and password. Player name should be in \"quotes\".");
						connection.Respond(player.Name, result);
						return;
					}
					else if (args.Length > 2)
					{
						name = args[0];
						for (int i = 1; i < args.Length; i++)
						{
							if (string.IsNullOrWhiteSpace(args[i])) {
								continue;
							}
							else if (i == args.Length - 1)
							{
								pw = args[i];
							}
						}
					}
					else if (args.Length == 2)
					{
						name = args[0];
						pw = args[1];
					}

					if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pw))
					{
						result.Add("Invalid authorization argument count. Requires a player name and password. Player name should be in \"quotes\".");
						connection.Respond(player.Name, result);
						return;
					}

					pw = pw.Trim();

					if (connection.State.Players.Count == 0) { 
						// I don't see how this is possible
						// but I'm going to leave it here
						// because I don't remember why I put it here
						return; 
					}

					PlayerInfo match = connection.State.Players.Find(x => x.Name == name);
					if (match != null && pw == connection.Settings.RconPassword) {
						connection.Settings.AuthorizedUIDs.Add(match.Uid);
						result.Add("Successfully authorized " + match.Name + ".");
					}
					else
					{
						result.Add("Failed to authorize player \"" + name + "\".");
					}

					connection.Respond(player.Name, result);
					return;

				},
				AdminCommand = false
			},
            #endregion
            #region SetNextMatch
            new RuntimeCommand()
			{
				Attribute = CommandAttribute.DynamicVoteFileCommand,
				Trigger = "!setNextMatch",
				Name = "setNextMatch",
				Blurb = " !setNextMatch: set next game/map",
				HelpStrings = new List<string>() {
                  //-------------------------------------------
                    "Sets gametype and map for the next match.",
					"Use '!list' commands to find options."
				},
				Description = "Sets gametype and map for the next match.",
				Args = new List<Arg> {
					new Arg("game", "The gametype to use."),
					new Arg("map", "The map to use.")
				},
				Command = (connection, message, player, command) =>
				{

					List<string> result = new List<string>() { Echo(message, player) };
                    
					if (!connection.Settings.DynamicVotingFileManagement)
					{
						result.Add("Unable to set next game, dynamic voting file management is disabled.");
						connection.Respond(player.Name, result);
						return;
					}

					message = message.TrimStart(command.Trigger.Length).Trim();

					if (message.Length < 2)
					{
						result.Add("Invalid setNextMatch arguments.");
						connection.Respond(player.Name, result);
						return;
					}

					string[] args = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //connection.MessagePlayer(player.Name, "DEBUG: " + args[0] + " | " + args[1], 10);

                    if (args.Length == 2)
					{
						new Thread(new ThreadStart(() => {
							connection.Respond(player.Name, result);
							Thread.Sleep(Connection.ServerMessageDelay);
							connection.Command_SetNextGame(args[0], args[1], player.Name);
						})).Start();
					}
					else
					{
						result.Add("Invalid setNextMatch args");
						connection.Respond(player.Name, result);
					}

					return;

				}
			},
            #endregion
            #region VoteNextMatch
            new RuntimeCommand()
			{
				Attribute = CommandAttribute.DynamicVoteFileCommand,
				Trigger = "!voteAddGame",
				Name = "voteAddGame",
				Blurb = " !voteAddGame: vote to enqueue game/map",
				HelpStrings = new List<string>() {
                  //-------------------------------------------
                    "Vote to add game/map to match queue.",
					"Use '!list games' to find game options.",
					"Use '!list maps' to find map options."
				},
				Description = "Starts a vote for *game on *map to be added to the match queue.",
				Args = new List<Arg> {
					new Arg("game", "The game to play"),
					new Arg("map", "The map to play on")
				},
				AdminCommand = false,
				Command = (connection, message, player, command) =>
				{

					List<string> result = new List<string>() { Echo(message, player) };
                    
					if (!connection.Settings.DynamicVotingFileManagement)
					{
						result.Add("Unable to start a vote for a game, dynamic voting file management is disabled.");
						connection.Respond(player.Name, result);
						return;
					}

					message = message.TrimStart(command.Trigger.Length).Trim();

					if (message.Length < 2)
					{
						result.Add("Invalid parameters");
						connection.Respond(player.Name, result);
						return;
					}

					string[] args = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //connection.MessagePlayer(player.Name, "DEBUG: " + args[0] + " | " + args[1], 10);

                    if (args.Length == 2)
					{

						MatchInfo matchInfo = connection.GetMatchInfo(args[0], args[1]);

						if (matchInfo.IsValid)
						{
							new Thread(new ThreadStart(() => {

								connection.Respond(player.Name, result);

								Thread.Sleep(Connection.ServerMessageDelay);

								connection.Command_BeginVote(
									() => { connection.Command_AddGameToQueue(args[0], args[1], player.Name); },
									player,
									"add \"" + args[0] + "\" on \"" + args[1] + "\" to the match queue",
									new List<string>() { matchInfo.GameVariant.Name + ": " + matchInfo.GameVariant.Description },
									new List<string>() { "\"" + args[0] + "\" on \"" + args[1] + "\" has been added to the match queue." }
								);

							})).Start();
							
							return;
						}
						else
						{
							if (matchInfo.GameVariant == null) { result.Add("Game variant \"" + args[0] + "\" could not be found."); }
							if (matchInfo.MapVariant == null) { result.Add(  "Map variant \"" + args[1] + "\" could not be found."); }
							result.Add("Invalid match, unable to begin vote.");
							connection.Respond(player.Name, result);
							return;
						}
					}
					else
					{
						result.Add("Invalid parameters");
						connection.Respond(player.Name, result);
						return;
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
					"Lists all queued matches. Use: '!commands'",
					"Only works if match mode is Queue,"
				},
				Description = "Lists all matches in the match queue.",
				Args = new List<Arg> {},
				AdminCommand = false,
				Command = (connection, message, player, command) =>
				{
					
					List<string> result = new List<string>() { Echo(message, player) };

					if (connection.NextQueuedMatch == "")
					{
						result.Add("There are no matches in the match queue.");
						connection.Respond(player.Name, result);
						return;
					}
					else {
						Queue<MatchInfo> queueCopy = new Queue<MatchInfo>(connection.MatchQueue.ToArray().Reverse());
						if (queueCopy.Count != 0)
						{
							string num = "";
							int i = queueCopy.Count;
							foreach( MatchInfo item in queueCopy )
							{
								if (i == 1) { break; }
								else {num = i.ToString() + ": "; }
								result.Add(num + item.GameVariant.Name + " on " + item.MapVariant.Name);
								i--;
							}
						}
						result.Add("NEXT: " + connection.NextQueuedMatch);
						connection.Respond(player.Name, result);
						return;
					}

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
					"Ex: !setVoteFile voting"
				},
				Description = "Sets current voting JSON file.",
				Args = new List<Arg> {
					new Arg("voteFile", "The name of the voting file.")
				},
				Command = (connection, message, player, command) =>
				{

					List<string> result = new List<string>() { Echo(message, player) };

					message = message.TrimStart(command.Trigger.Length).Trim();

					if(string.IsNullOrEmpty(message))
					{
						result.Add("Invalid voteFile paramter.");
					}
					else
					{
						result.Add("VoteFile change request sent.");
						connection.SetActiveVotingJson(message);
					}

					connection.Respond(player.Name, result);
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
					" !pm: privately message a player",
					"Ex: '!pm Player Name message'",
					"This command requires 2 parameters"
				},
				AdminCommand = false,
				Description = "Privately messages a player",
				Args = new List<Arg> {
					new Arg("player", "name of the player to message."),
					new Arg("message", "the message to send the player.")
				},
				Command = (connection, message, player, command) =>
				{
					string playerName = player.Name;
					List<string> result = new List<string>();

					if (message.Trim() == command.Trigger)
					{
						result.AddRange(command.HelpStrings);
						connection.Respond(playerName, result);
						return;
					}

					string content = message.Trim().TrimStart(command.Trigger.Length).Trim();

					if (content.Length == 0)
					{
						result.AddRange(command.HelpStrings);
						connection.Respond(playerName, result);
						return;
					}

					else
					{
						
						string targetName = "";
						lock (connection.State.ServerStateLock)
						{
							targetName = connection.State.Players.Find(x => content.StartsWith(x.Name))?.Name;
						}

						if (targetName == null)
						{
							result.Add("'" + command.Trigger + " " +  ((content.Length >= 12) ? content.Substring(0, 12) + "...'" : content + "'"));
							result.Add("PM Failed: Player not found.");
							connection.Respond(playerName, result);
							return;
						}
						
						content = content.TrimStart(targetName.Length).Trim();

						if (content.Length == 0 || string.IsNullOrWhiteSpace(content))
						{
							result.Add("'" + command.Trigger + " " + targetName + "'");
							result.Add("PM Failed: Empty Message");
							connection.Respond(playerName, result);
							return;
						}

						result.Add("(@" + playerName  + ")>>[" + targetName + "]:");
						result.Add(content);

						connection.Respond(playerName, result);
						connection.Respond(targetName, new List<string>() {
							"[@" + targetName + "]<<(" + playerName + "):", content
						});

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
				Args = new List<Arg> {},
				Command = (connection, message, player, command) =>
				{
					connection.Command_DeactivateMatchQueue();
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
		public Action<Connection, string, PlayerInfo, RuntimeCommand> Command { get; set; }
		public List<RuntimeCommand> SubCommands { get; set; } = new List<RuntimeCommand>();
		public CommandAttribute Attribute { get; set; } = CommandAttribute.None;
		public bool AdminCommand { get; set; } = true;
		public bool AcceptsParameters { get; set; } = true;
		
		public bool IsValidForConnection(Connection connection)
		{

			if (HasFlag(CommandAttribute.DynamicVoteFileCommand) && !connection.Settings.DynamicVotingFileManagement) { return false; }

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
		/// Returns true if the help request is for one of this command's sub-commands.
		/// </summary>
		/// <param name="message">The help request, with the initial "!!help " substring removed.</param>
		private bool IsSubCommandHelpRequest(string message)
		{

			if (SubCommands.Count == 0) { return false; }

			foreach (RuntimeCommand command in SubCommands)
			{
				if (message.StartsWith(command.Name))
				{
					return true;
				}
			}

			return false;

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
		/// Returns a timestamped/ID'd copy of the given message.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="player"></param>
		/// <returns></returns>
		private static string Echo(string message, PlayerInfo player)
		{
			return "[" + DateTime.Now.ToShortTimeString() + "]" + player.Name + ": \"" + message + "\"";
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

			if (IsSubCommandHelpRequest(message))
			{
				foreach (RuntimeCommand item in SubCommands)
				{
					if (message.StartsWith(item.Name))
					{
						result.AddRange(item.GetHelp(message));
						return result;
					}
				}
			}
			else if (IsArgHelpRequest(message))
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
		
		public class Arg
		{
			public string Name { get; set; } = "";
			public string Description { get; set; } = "";
			public Arg(string Name, string Description)
			{
				this.Name = Name; this.Description = Description;
			}
		}
		
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

		[Flags]
		public enum CommandAttribute
		{
			None = 0,
			DynamicVoteFileCommand = 1
		}

	}

}
