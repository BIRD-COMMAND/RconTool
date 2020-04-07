using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Threading;

namespace RconTool
{
    [DataContract]
    public class ToolCommand
    {

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<string> CommandStrings { get; set; }
        [DataMember]
        public bool Enabled { get; set; }
        [DataMember]
        public Type ConditionType { get; set; }
        [DataMember]
        public Operator ConditionOperator { get; set; }
        [DataMember]
        public int ConditionThreshold { get; set; }
        [IgnoreDataMember]
        public ServerSettings AssociatedServer { get; set; }
        [DataMember]
        public string CustomServerCommand { get; set; }
        [DataMember]
        public int PlayerCountRangeMin { get; set; }
        [DataMember]
        public int PlayerCountRangeMax { get; set; }
        [DataMember]
        public int RunTime { get; set; }        
        [DataMember]
        public string Tag { get; set; } = "";
        [DataMember]
        public bool IsGlobalToolCommand { get; set; } = false;
        public DateTime nextRunTime;
        public Connection connection;

        public ToolCommand(
            string name,
            bool enabled,
            List<string> runs,
            Type conditionType,
            Operator conditionOperator,
            int conditionThreshold,
            ServerSettings associatedServer,
            string customServerCommand,
            int playerCountRangeMin,
            int playerCountRangeMax,
            int runTime,
            string tag
        )
        {

            this.Name = name;
            this.CommandStrings = runs;
            this.Enabled = enabled;
            this.AssociatedServer = associatedServer;
            this.ConditionType = conditionType;
            this.ConditionOperator = conditionOperator;
            this.ConditionThreshold = conditionThreshold;
            this.CustomServerCommand = customServerCommand;
            this.PlayerCountRangeMin = playerCountRangeMin;
            this.PlayerCountRangeMax = playerCountRangeMax;
            this.RunTime = runTime;
            this.Tag = tag;
            nextRunTime = DateTime.Now;

        }

        public bool triggered = false;

        public void Initialize(Connection connection)
        {

            nextRunTime = DateTime.Now;

            if (Enabled)
            {
                switch (ConditionType)
                {
                    case Type.PlayerJoined:
                        connection.PlayerJoined += CheckPlayerConditional;
                        break;
                    case Type.PlayerLeft:
                        connection.PlayerLeft += CheckPlayerConditional;
                        break;
                    case Type.PlayerCount:
                        connection.PlayerCountChanged += CheckPlayerConditional;
                        break;
                    case Type.PlayerCountInRange:
                        connection.PlayerCountChanged += CheckPlayerConditional;
                        break;
                    case Type.CustomServerCommand:
                        connection.ChatMessageReceived += CheckChatConditional;
                        break;
                    default: break;
                }
            }

        }

        public bool CanRunCommand(Connection connection)
        {
            if (!Enabled) { return false; }
            if (this.connection == null)
            {
                this.connection = App.GetConnection(AssociatedServer);
                if (this.connection == null) { return false; }
            }
            if (this.connection != connection) { return false; }
            return true;
        }

        public void Enable()
        {
            Enabled = true;
            Initialize(connection);
            App.PopulateConditionalCommandsDropdown();
        }

        public void Disable()
        {

            if (!IsGlobalToolCommand && connection == null)
            {
                //throw new Exception
                App.Log("Attempted to disable a connection-specific command with no specified connection.");
            }

            else if (!IsGlobalToolCommand)
            {

                Enabled = false;

                switch (ConditionType)
                {
                    case Type.PlayerJoined:
                        connection.PlayerJoined -= CheckPlayerConditional;
                        break;
                    case Type.PlayerLeft:
                        connection.PlayerLeft -= CheckPlayerConditional;
                        break;
                    case Type.PlayerCount:
                        connection.PlayerCountChanged -= CheckPlayerConditional;
                        break;
                    case Type.PlayerCountInRange:
                        connection.PlayerCountChanged -= CheckPlayerConditional;
                        break;
                    case Type.CustomServerCommand:
                        connection.ChatMessageReceived -= CheckChatConditional;
                        break;
                    default: break;
                }

            }

            App.PopulateConditionalCommandsDropdown();

        }

        public void ToggleEnabled()
        {
            if (Enabled)
            {
                Disable();
            }
            else
            {
                if (ConditionType == Type.EveryXMinutes)
                {
                    nextRunTime = DateTime.Now;
                }
                Enable();
            }
            App.PopulateConditionalCommandsDropdown();
            App.SaveConditionalCommands();
        }

        public void CheckPlayerConditional(object sender, Connection.PlayerJoinLeaveEventArgs e)
        {

            if (!CanRunCommand(e.Connection)) { return; }
            
            switch (ConditionType)
            {
                case Type.PlayerJoined:
                    RunCommands();
                    break;
                case Type.PlayerLeft:
                    RunCommands();
                    break;
                case Type.PlayerCount:
                    #region

                    bool conditionMet = false;

                    if (triggered == false)
                    {
                        switch (ConditionOperator)
                        {
                            case Operator.GreatherThan:
                                conditionMet = (e.NewPlayerCount > ConditionThreshold);
                                break;
                            case Operator.LessThan:
                                conditionMet = (e.NewPlayerCount < ConditionThreshold);
                                break;
                            case Operator.EqualTo:
                                conditionMet = (e.NewPlayerCount == ConditionThreshold);
                                break;
                            case Operator.GreaterThanOrEqualTo:
                                conditionMet = (e.NewPlayerCount >= ConditionThreshold);
                                break;
                            case Operator.LessThanOrEqualTo:
                                conditionMet = (e.NewPlayerCount <= ConditionThreshold);
                                break;
                        }
                        if (conditionMet)
                        {
                            RunCommands();
                            triggered = true;
                        }
                    }
                    else
                    {

                        switch (ConditionOperator)
                        {
                            case Operator.GreatherThan:
                                conditionMet = (e.NewPlayerCount <= ConditionThreshold);
                                break;
                            case Operator.LessThan:
                                conditionMet = (e.NewPlayerCount >= ConditionThreshold);
                                break;
                            case Operator.EqualTo:
                                conditionMet = (e.NewPlayerCount != ConditionThreshold);
                                break;
                            case Operator.GreaterThanOrEqualTo:
                                conditionMet = (e.NewPlayerCount < ConditionThreshold);
                                break;
                            case Operator.LessThanOrEqualTo:
                                conditionMet = (e.NewPlayerCount > ConditionThreshold);
                                break;
                        }
                        if (conditionMet)
                        {
                            triggered = false;
                        }

                    }
                    #endregion
                    break;
                case Type.PlayerCountInRange:
                    #region

                    if (triggered == false)
                    {
                        if (e.NewPlayerCount >= PlayerCountRangeMin && e.NewPlayerCount <= PlayerCountRangeMax)
                        {
                            triggered = true;
                            RunCommands();
                        }
                    }
                    else
                    {
                        if (e.NewPlayerCount < PlayerCountRangeMin || e.NewPlayerCount > PlayerCountRangeMax)
                        {
                            triggered = false;
                        }
                    }

                    #endregion
                    break;

                default: break;

            }

        }

        public void CheckChatConditional(object sender, Connection.ChatEventArgs e = null)
        {

            if (!CanRunCommand(e.Connection)) { return; }

            switch (ConditionType)
            {

                case Type.CustomServerCommand:
                    #region

                    if (e.Message.StartsWith(CustomServerCommand))
                    {
                        RunCommands(e);
                    }

                    #endregion
                    break;

                default: break;
            }

        }

        public void RunCommands(Connection.ChatEventArgs e = null)
        {

            foreach (string command in CommandStrings)
            {
                if (command.StartsWith("!!"))
                {
                    string s = command.TrimStart(new char[] { '@' });

                    foreach (string item in Directives.List)
                    {
                        if (s.StartsWith(item)) { }
                    }

                }
                else if (command.Trim().StartsWith("%"))
                {
                    string formatted = command.TrimStart(new char[] { '%' });
                    if (formatted.StartsWith(Directives.DisableConditionalCommand))
                    {
                        AssociatedServer.SetCommandEnabledState(
                            formatted.Remove(0, Directives.DisableConditionalCommand.Length + 1).Trim(),
                            false
                        );
                    }
                    else if (formatted.StartsWith(Directives.DisableTimedCommand))
                    {
                        AssociatedServer.SetCommandEnabledState(
                            formatted.Remove(0, Directives.DisableTimedCommand.Length + 1).Trim(),
                            false
                        );
                    }
                    else if (formatted.StartsWith(Directives.DisableCommandsByTag))
                    {
                        AssociatedServer.SetCommandEnabledState("", false, formatted.Remove(0, Directives.DisableCommandsByTag.Length + 1).Trim());
                    }
                    else if (formatted.StartsWith(Directives.EnableConditionalCommand))
                    {
                        AssociatedServer.SetCommandEnabledState(
                            formatted.Remove(0, Directives.EnableConditionalCommand.Length + 1).Trim(),
                            true
                        );
                    }
                    else if (formatted.StartsWith(Directives.EnableTimedCommand))
                    {
                        AssociatedServer.SetCommandEnabledState(
                            formatted.Remove(0, Directives.EnableTimedCommand.Length + 1).Trim(),
                            true
                        );
                    }
                    else if (formatted.StartsWith(Directives.EnableCommandsByTag))
                    {
                        AssociatedServer.SetCommandEnabledState("", true, formatted.Remove(0, Directives.EnableCommandsByTag.Length + 1).Trim());
                    }
                    else if (formatted.StartsWith(Directives.EnableDynamicVoteFiles))
                    {
                        connection.Command_EnableDynamicVoteFileManagement();
                    }
                    else if (formatted.StartsWith(Directives.DisableDynamicVoteFiles))
                    {
                        connection.Command_DisableDynamicVoteFileManagement();
                    }
                    else if (formatted.StartsWith(Directives.ListGameVariants))
                    {
                        connection.SendGameDescriptions(GameVariant.BaseGame.All);
                    }
                    else if (formatted.StartsWith(Directives.ListMapVariants))
                    {
                        connection.SendMapDescriptions(MapVariant.BaseMap.All);
                    }
                    else if (formatted.StartsWith(Directives.EndGame))
                    {
                        //if (formatted.Contains(connection.Settings.CommandAuthorization)) {
                            connection.SendToRcon("Game.End");
                        //}
                    }
                    else if (formatted.StartsWith(Directives.StartGame))
                    {
                        //if (formatted.Contains(connection.Settings.CommandAuthorization)) {
                            connection.SendToRcon("Game.Start");
                        //}
                    }
                    else if (formatted.StartsWith(Directives.ReloadAll))
                    {
                        //if (formatted.Contains(connection.Settings.CommandAuthorization)) {
                        connection.LoadGameVariants();
                        connection.LoadMapVariants();
                        //}
                    } //ServerSay
                    else if (formatted.StartsWith(Directives.ServerSay))
                    {
                        // Remove "%ServerSay% "" + "
                        if (e.Message.Length < Directives.ServerSay.Length + 6) { return; }
                        connection.Broadcast(e.Message.Remove(0, Directives.ServerSay.Length + 4).TrimLastCharacter());
                    }
                    else if (formatted.StartsWith(Directives.SetNextGame))
                    {
                        
                        formatted = formatted.Remove(0, Directives.SetNextGame.Length + 1).Trim();
                        string[] args = e.Message.Remove(0, Directives.SetNextGame.Length + 1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        connection.Broadcast("DEBUG: " + args[0] + " | " + args[1]);
                        if (args.Length == 2)
                        {
                            connection.Command_SetNextGame(args[0], args[1], e.SendingPlayer.Name);
                        }
                        else { connection.Whisper(e.SendingPlayer.Name, "Unable to parse SetNextGame args"); }
                        
                    }

                }
                else if (command.Trim().Contains("%") && ConditionType == Type.CustomServerCommand)
                {
                    //TODO FEATURE add mid-string directives
                }
                else
                {
                    connection.SendToRcon(command);
                }
            }
        }


        public enum Type
        {
            PlayerJoined,
            PlayerLeft,
            PlayerCount,
            PlayerCountInRange,
            CustomServerCommand,
            Daily,
            EveryXMinutes,
        }

        public enum Operator
        {
            GreatherThan,
            LessThan,
            EqualTo,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo
        }

        // vote for next gametype
        // vote for next map

        // vote for next gametype and then automatically vote for next map

        // -- maybe later
        // 
        // guided vote file construction
        //     select a game and then the maps for it
        //     keep going until vote to end or predetermined count
        //
        // chat-based on-the-fly game variant creation
        //     probably not worth it. definitely not worth it right *now*
        //     it would take a good bit of time to deconstruct the game variant encoding
        //
        // 
        // 

        public static class Directives
        {

            public const string DisableTimedCommand = "DisableTimedCommand";
            public const string DisableConditionalCommand = "DisableConditionalCommand";
            public const string DisableCommandsByTag = "DisableCommandsByTag";
            public const string EnableTimedCommand = "EnableTimedCommand";
            public const string EnableConditionalCommand = "EnableConditionalCommand";
            public const string EnableCommandsByTag = "EnableCommandsByTag";

            public const string SetNextGame = "SetNextGame";

            public const string EndGame = "EndGame";
            public const string StartGame = "StartGame";

            public const string EnableDynamicVoteFiles = "EnableDynamicVoteFiles";
            public const string DisableDynamicVoteFiles = "DisableDynamicVoteFiles";

            public const string ListGameVariants = "ListGameVariants";
            public const string ListMapVariants = "ListMapVariants";

            public const string ServerSay = "ServerSay";

            public const string ReloadAll = "ReloadAll";

            public static List<string> ListAsCommands {
                get {
                    if (_listAsCommands == null)
                    {
                        _listAsCommands = new List<string>();
                        FieldInfo[] fieldInfos = typeof(Directives).GetFields(
                            // Gets all public and static fields
                            BindingFlags.Public | BindingFlags.Static |
                            // This tells it to get the fields from all base types as well
                            BindingFlags.FlattenHierarchy);

                        // Go through the list and only pick out the constants
                        foreach (FieldInfo fi in fieldInfos)
                        {
                            // IsLiteral determines if its value is written at 
                            //   compile time and not changeable
                            // IsInitOnly determines if the field can be set 
                            //   in the body of the constructor
                            // for C# a field which is readonly keyword would have both true 
                            //   but a const field would have only IsLiteral equal to true
                            if (fi.IsLiteral && !fi.IsInitOnly)
                            {
                                _listAsCommands.Add("%" + (string)fi.GetValue(null) + "%");
                            }
                        }
                    }
                    return _listAsCommands;
                }
            }
            private static List<string> _listAsCommands;
            public static List<string> List { 
                get {
                    if (_list == null)
                    {
                        _list = new List<string>();
                        FieldInfo[] fieldInfos = typeof(Directives).GetFields(
                            // Gets all public and static fields
                            BindingFlags.Public | BindingFlags.Static |
                            // This tells it to get the fields from all base types as well
                            BindingFlags.FlattenHierarchy);

                        // Go through the list and only pick out the constants
                        foreach (FieldInfo fi in fieldInfos)
                        {
                            // IsLiteral determines if its value is written at 
                            //   compile time and not changeable
                            // IsInitOnly determines if the field can be set 
                            //   in the body of the constructor
                            // for C# a field which is readonly keyword would have both true 
                            //   but a const field would have only IsLiteral equal to true
                            if (fi.IsLiteral && !fi.IsInitOnly)
                            {
                                _list.Add((string)fi.GetValue(null));
                            }
                        }
                    }
                    return _list;
                }
            }
            private static List<string> _list;
            

        }

    }
}
