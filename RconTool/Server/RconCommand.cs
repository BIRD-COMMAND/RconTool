using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{

	public class RconCommand
	{


		#region Fields and Properties


		// COMMAND
		/// <summary>
		/// The text of the command. Defaults to empty string.
		/// </summary>
		public string command = "";
		/// <summary>
		/// True if the command is not an empty string.
		/// </summary>
		public bool IsValid { get { return !String.IsNullOrWhiteSpace(command); } }
				
		// LOG STRING
		/// <summary>
		/// If present, this text will be logged to the specified feed instead of the raw command text. Defaults to empty string.
		/// </summary>
		public string logString = "";
		/// <summary>
		/// True if the logString is not an empty string.
		/// </summary>
		public bool LogStringValid { get { return !String.IsNullOrWhiteSpace(logString); } }

		// PLAYER NAME
		/// <summary>
		/// If present, the player name may sometimes be included with the logString in the logged message.  Defaults to empty string.
		/// </summary>
		public string playerName = "";
		/// <summary>
		/// True if the playerName is not an empty string.
		/// </summary>
		public bool PlayerNameValid { get { return !String.IsNullOrWhiteSpace(playerName); } }

		// LOG TO
		/// <summary>
		/// Flag field indicating which feeds or logs this command will be displayed in. Defaults to None.
		/// </summary>
		public LogTo logTo = LogTo.None;


		#endregion


		#region Private Constructors

		/// <summary>
		/// Creates an RconCommand and specifies which Log (if any) the command will be logged/displayed in.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logTo">Flag denoting which Logs to log/display the command in.</param>
		private RconCommand(string command, LogTo logTo) : this(command, "", logTo) {}

		/// <summary>
		/// Creates an RconCommand and specifies which Log (if any) the command will be logged/displayed in.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">Text that (if present) will be logged to the specified feed instead of the raw command text.</param>
		/// <param name="logTo">Flag denoting which Logs to log/display the command in.</param>
		private RconCommand(string command, string logString, LogTo logTo) : this(command, logString, "", logTo) {}

		/// <summary>
		/// Creates an RconCommand and specifies which Log (if any) the command will be logged/displayed in.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">Text that (if present) will be logged to the specified feed instead of the raw command text.</param>
		/// <param name="command">Name of the player who triggered the command.</param>
		/// <param name="logTo">Flag denoting which Logs to log/display the command in.</param>
		private RconCommand(string command, string logString, string playerName, LogTo logTo)
		{
			this.command = command;
			this.logString = logString;
			this.playerName = playerName;
			this.logTo = logTo;			
		}

		#endregion


		#region Static RconCommand Constructor Methods


		#region ConsoleLog RconCommands

		/// <summary>
		/// Creates an RconCommand that will be logged in the Console Log.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		public static RconCommand ConsoleLogCommand(string command)
		{
			return ConsoleLogCommand(command, "", "");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Console Log under the Server's name - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged instead of the raw command text.</param>
		public static RconCommand ConsoleLogCommand(string command, string logString)
		{
			//return ConsoleLogCommand(command, logString, "SERVER");
			return ConsoleLogCommand(command, logString, "");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Console Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="commandMessage">Full text of message that triggered the command.</param>
		/// <param name="commandSender">PlayerInfo of player who sent the command.</param>
		public static RconCommand ConsoleLogCommand(string command, string commandMessage, PlayerInfo commandSender)
		{
			return ConsoleLogCommand(command, commandMessage, commandSender.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Console Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="parseResult">ParseResult of the chat message that triggered the command.<br>ParseResults store the player name and full message text.</br></param>
		public static RconCommand ConsoleLogCommand(string command, ParseResult parseResult)
		{
			return ConsoleLogCommand(command, parseResult?.ChatMessage?.Text, parseResult?.ChatMessage?.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Console Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged instead of the raw command text.</param>
		/// <param name="playerName">Name of the player that triggered this command.</param>
		public static RconCommand ConsoleLogCommand(string command, string logString = "", string playerName = "")
		{
			return new RconCommand(command, logString, playerName, LogTo.Console);
		}

		#endregion


		#region ChatLog RconCommands

		/// <summary>
		/// Creates an RconCommand that will be logged in the Chat Log.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		public static RconCommand ChatLogCommand(string command)
		{
			return ChatLogCommand(command, "", "");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Chat Log under the Server's name - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged to chat instead of the raw command text.</param>
		public static RconCommand ChatLogCommand(string command, string logString)
		{
			return ChatLogCommand(command, logString, "SERVER");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Chat Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="commandMessage">Full text of chat message that triggered the command.</param>
		/// <param name="commandSender">PlayerInfo of player who sent the command message in chat.</param>
		public static RconCommand ChatLogCommand(string command, string commandMessage, PlayerInfo commandSender) {
			return ChatLogCommand(command, commandMessage, commandSender?.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Chat Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="parseResult">ParseResult of the chat message that triggered the command.<br>ParseResults store the player name and full message text.</br></param>
		public static RconCommand ChatLogCommand(string command, ParseResult parseResult) {
			return ChatLogCommand(command, parseResult?.ChatMessage?.Text, parseResult?.ChatMessage?.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Chat Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged to chat instead of the raw command text.</param>
		/// <param name="playerName">Name of the player that triggered this command.</param>
		public static RconCommand ChatLogCommand(string command, string logString = "", string playerName = "") {
			return new RconCommand(command, logString, playerName, LogTo.Chat);
		}

		#endregion


		#region PlayerLog RconCommands

		/// <summary>
		/// Creates an RconCommand that will be logged in the Player Log.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		public static RconCommand PlayerLogCommand(string command)
		{
			return PlayerLogCommand(command, "", "");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Player Log under the Server's name - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged instead of the raw command text.</param>
		public static RconCommand PlayerLogCommand(string command, string logString)
		{
			return PlayerLogCommand(command, logString, "SERVER");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Player Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="commandMessage">Full text of chat message that triggered the command.</param>
		/// <param name="commandSender">PlayerInfo of player who sent the command message in chat.</param>
		public static RconCommand PlayerLogCommand(string command, string commandMessage, PlayerInfo commandSender)
		{
			return PlayerLogCommand(command, commandMessage, commandSender?.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Player Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="parseResult">ParseResult of the chat message that triggered the command.<br>ParseResults store the player name and full message text.</br></param>
		public static RconCommand PlayerLogCommand(string command, ParseResult parseResult)
		{
			return PlayerLogCommand(command, parseResult?.ChatMessage?.Text, parseResult?.ChatMessage?.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the Player Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged instead of the raw command text.</param>
		/// <param name="playerName">Name of the player that triggered this command.</param>
		public static RconCommand PlayerLogCommand(string command, string logString = "", string playerName = "")
		{
			return new RconCommand(command, logString, playerName, LogTo.PlayerLog);
		}

		#endregion


		#region AppLog RconCommands

		/// <summary>
		/// Creates an RconCommand that will be logged in the App Log.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		public static RconCommand AppLogCommand(string command)
		{
			return AppLogCommand(command, "", "");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the App Log under the Server's name - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged instead of the raw command text.</param>
		public static RconCommand AppLogCommand(string command, string logString)
		{
			return AppLogCommand(command, logString, "SERVER");
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the App Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="commandMessage">Full text of message that triggered the command.</param>
		/// <param name="commandSender">PlayerInfo of player who sent the command.</param>
		public static RconCommand AppLogCommand(string command, string commandMessage, PlayerInfo commandSender)
		{
			return AppLogCommand(command, commandMessage, commandSender?.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the App Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="parseResult">ParseResult of the chat message that triggered the command.<br>ParseResults store the player name and full message text.</br></param>
		public static RconCommand AppLogCommand(string command, ParseResult parseResult)
		{
			return AppLogCommand(command, parseResult?.ChatMessage?.Text, parseResult?.ChatMessage?.Name);
		}

		/// <summary>
		/// Creates an RconCommand that will be logged in the App Log - with additional parameters for more informative logging.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		/// <param name="logString">The text that will be logged instead of the raw command text.</param>
		/// <param name="playerName">Name of the player that triggered this command.</param>
		public static RconCommand AppLogCommand(string command, string logString = "", string playerName = "")
		{
			return new RconCommand(command, logString, playerName, LogTo.AppLog);
		}

		#endregion


		/// <summary>
		/// Creates an RconCommand that will not be logged anywhere.
		/// </summary>
		/// <param name="command">Text of RCON command.</param>
		public static RconCommand Command(string command)
		{
			return new RconCommand(command, "", "", LogTo.None);
		}


		#endregion


		public void Log(Connection connection)
		{
			switch (logTo) {
				case LogTo.None: break;
				case LogTo.Console:		connection.PrintToConsole($"{(PlayerNameValid ? playerName + ": " : "")}{(LogStringValid ? logString : (command ?? ""))}"); break;
				case LogTo.Chat:		connection.PrintToChat(PlayerNameValid ? playerName : "SERVER", LogStringValid ? logString : (command ?? "")); break;
				case LogTo.PlayerLog:	connection.PrintToPlayerLog($"{(PlayerNameValid ? playerName + ": " : "")}{(LogStringValid ? logString : (command ?? ""))}"); break;
				case LogTo.AppLog:		App.Log($"{(PlayerNameValid ? playerName + ": " : "")}{(LogStringValid ? logString : (command ?? ""))}", connection); break;
				default: break;
			}
		}


		/// <summary>
		/// Flags Enum specifying which logs or feeds (if any) the command will be logged/displayed on.
		/// </summary>
		[Flags] public enum LogTo
		{
			None = 0,
			Console = 1,
			Chat = 2,
			PlayerLog = 4,
			AppLog = 8
		}


	}

}