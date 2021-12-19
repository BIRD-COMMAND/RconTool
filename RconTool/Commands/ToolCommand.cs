using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RconTool
{
	[JsonObject(MemberSerialization.OptIn)]
    public class ToolCommand
    {

		#region Properties

		[JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public List<string> CommandStrings { get; set; }
        [JsonProperty]
        public bool Enabled { get; set; }
        [JsonProperty]
        public TriggerType ConditionType { get; set; }
        [JsonProperty]
        public Operator ConditionOperator { get; set; }
        [JsonProperty]
        public int ConditionThreshold { get; set; }
        [JsonProperty]
        public string CustomServerCommand { get; set; }
        [JsonProperty]
        public int PlayerCountRangeMin { get; set; }
        [JsonProperty]
        public int PlayerCountRangeMax { get; set; }
        [JsonProperty]
        public int RunTime { get; set; }        
        [JsonProperty]
        public string Tag { get; set; } = "";
        [JsonProperty]
        public bool IsGlobalToolCommand { get; set; } = false;

        public ServerSettings AssociatedServer { get; set; }
        public Connection Connection { get; set; }
        public DateTime NextRunTime { get; set; }

        public bool Triggered { get; set; } = false;
        private bool SubscribedToConnectionEvents { get; set; } = false;

        #endregion

        public ToolCommand(
            string name,
            bool enabled,
            List<string> runs,
            TriggerType trigger,
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
            this.ConditionType = trigger;
            this.ConditionOperator = conditionOperator;
            this.ConditionThreshold = conditionThreshold;
            this.CustomServerCommand = customServerCommand;
            this.PlayerCountRangeMin = playerCountRangeMin;
            this.PlayerCountRangeMax = playerCountRangeMax;
            this.RunTime = runTime;
            this.Tag = tag;
            NextRunTime = DateTime.Now;

        }

        public void Initialize(Connection connection)
        {

            Connection = connection;
            NextRunTime = DateTime.Now;

            if (Enabled)
            {
                switch (ConditionType)
                {
                    case TriggerType.PlayerJoined:
                        connection.PlayerJoined += CheckPlayerConditional;
                        break;
                    case TriggerType.PlayerLeft:
                        connection.PlayerLeft += CheckPlayerConditional;
                        break;
                    case TriggerType.PlayerCount:
                        connection.PlayerCountChanged += CheckPlayerConditional;
                        break;
                    case TriggerType.PlayerCountInRange:
                        connection.PlayerCountChanged += CheckPlayerConditional;
                        break;
                    case TriggerType.CustomServerCommand:
                        connection.ChatMessageReceived += CheckChatConditional;
                        break;
					case TriggerType.MatchBegan:
						connection.MatchBeganOrEnded += CheckMatchBeginEndConditional;
						break;
					case TriggerType.MatchEnded:
						connection.MatchBeganOrEnded += CheckMatchBeginEndConditional;
						break;
					default: break;
                }
                SubscribedToConnectionEvents = true;
            }

        }

        public bool CanRunCommandOnConnection(Connection connection)
        {
            if (!Enabled) { return false; }
            if (Connection == null)
            {
                if (AssociatedServer == null) { return false; }
                Connection = App.GetConnection(AssociatedServer);
                if (Connection == null) { return false; }
            }
            if (Connection != connection) { return false; }
            return true;
        }

        public void Enable(bool populateCommandsDropdown = true)
        {
            Enabled = true;
            if (!SubscribedToConnectionEvents) {
                Initialize(Connection);
            }
            if (populateCommandsDropdown) {
                App.RepopulateConditionalCommandsDropdown = true;
            }
        }

        public void Disable(bool populateCommandsDropdown = true)
        {

            if (!IsGlobalToolCommand && Connection == null)
            {
                //throw new Exception
                App.Log("Attempted to disable a connection-specific command with no specified connection.");
            }

            else if (!IsGlobalToolCommand)
            {

                Enabled = false;

                if (SubscribedToConnectionEvents) {
                    switch (ConditionType) {
                        case TriggerType.PlayerJoined:
                            Connection.PlayerJoined -= CheckPlayerConditional;
                            break;
                        case TriggerType.PlayerLeft:
                            Connection.PlayerLeft -= CheckPlayerConditional;
                            break;
                        case TriggerType.PlayerCount:
                            Connection.PlayerCountChanged -= CheckPlayerConditional;
                            break;
                        case TriggerType.PlayerCountInRange:
                            Connection.PlayerCountChanged -= CheckPlayerConditional;
                            break;
                        case TriggerType.CustomServerCommand:
                            Connection.ChatMessageReceived -= CheckChatConditional;
                            break;
                        case TriggerType.MatchBegan:
                            Connection.MatchBeganOrEnded -= CheckMatchBeginEndConditional;
                            break;
                        case TriggerType.MatchEnded:
                            Connection.MatchBeganOrEnded -= CheckMatchBeginEndConditional;
                            break;
                        default: break;
                    }
                    SubscribedToConnectionEvents = false;
                }

            }

            if (populateCommandsDropdown) {
                App.RepopulateConditionalCommandsDropdown = true;
            }

        }

        public void ToggleEnabled()
        {
            if (Enabled)
            {
                Disable();
            }
            else
            {
                if (ConditionType == TriggerType.EveryXMinutes)
                {
                    NextRunTime = DateTime.Now;
                }
                Enable();
            }
            App.RepopulateConditionalCommandsDropdown = true;
            App.SaveSettings();
        }

        public void CheckPlayerConditional(object sender, Connection.PlayerJoinLeaveEventArgs e)
        {

            if (!CanRunCommandOnConnection(e.Connection)) { return; }
            
            switch (ConditionType)
            {
                case TriggerType.PlayerJoined:
                    if (e.PlayerJoined) { RunCommands(); }
                    break;
                case TriggerType.PlayerLeft:
                    if (!e.PlayerJoined) { RunCommands(); }
                    break;
                case TriggerType.PlayerCount:
                    #region

                    bool conditionMet = false;

                    if (Triggered == false)
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
                            Triggered = true;
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
                            Triggered = false;
                        }

                    }
                    #endregion
                    break;
                case TriggerType.PlayerCountInRange:
                    #region

                    if (Triggered == false)
                    {
                        if (e.NewPlayerCount >= PlayerCountRangeMin && e.NewPlayerCount <= PlayerCountRangeMax)
                        {
                            Triggered = true;
                            RunCommands();
                        }
                    }
                    else
                    {
                        if (e.NewPlayerCount < PlayerCountRangeMin || e.NewPlayerCount > PlayerCountRangeMax)
                        {
                            Triggered = false;
                        }
                    }

                    #endregion
                    break;

                default: break;

            }

        }

        public void CheckChatConditional(object sender, Connection.ChatEventArgs e = null)
        {

            if (!CanRunCommandOnConnection(e.Connection)) { return; }

            switch (ConditionType)
            {

                case TriggerType.CustomServerCommand:
                    #region

                    if (e.Message.StartsWith(CustomServerCommand)) {
                        RunCommands(e);
                    }

                    #endregion
                    break;

                default: break;
            }

        }

        public void CheckMatchBeginEndConditional(object sender, Connection.MatchBeginEndArgs e = null)
		{
            if (ConditionType == TriggerType.MatchBegan && e.MatchBegan) { RunCommands(); }
            else if (ConditionType == TriggerType.MatchEnded && !e.MatchBegan) { RunCommands(); }
		}

        public void RunCommands(Connection.ChatEventArgs e = null)
        {
            foreach (string command in CommandStrings)
            {
                string formatted = command.Trim();
                formatted = ParseResult.ReplaceDynamicReferences(formatted, Connection);
                if (command.Trim().StartsWith("!") || command.Trim().StartsWith("%"))
                {

                    if (command.Trim().StartsWith("!!")) {
                        formatted = formatted.TrimStart("@".ToCharArray()); // I don't remember why this is necessary
					}
                    else {
                        // Remove the command-enclosing '%' characters
                        formatted = command.RemoveFirst('%', 2);
                    }
                    while (formatted.StartsWith("!")) { 
                        // Remove '!' characters
                        formatted = formatted.Remove(0, 1); 
                    }

                    if (RuntimeCommand.StringStartsWithCommandName(formatted)) {
                        RuntimeCommand.TryRunCommand($"!{formatted}", null, Connection);
                    }
                    else {
                        Connection.RconCommandQueue.Enqueue(
                            RconCommand.ConsoleLogCommand(
                                formatted, formatted, 
                                e?.SendingPlayer?.Name ?? "SERVER"
                            )
                        );
                    }

                }
				else {
                    if (formatted.StartsWith("!")) {
                        if (RuntimeCommand.StringStartsWithCommandName(formatted.TrimStart(1))) {
                            RuntimeCommand.TryRunCommand(formatted, null, Connection);
                        }
                        else {
                            Connection.RconCommandQueue.Enqueue(
                                RconCommand.ConsoleLogCommand(
                                    formatted, formatted,
                                    e?.SendingPlayer?.Name ?? "SERVER"
                                )
                            );
                        }
                    }
                    else {
                        if (RuntimeCommand.StringStartsWithCommandName(formatted)) {
                            RuntimeCommand.TryRunCommand($"!{formatted}", null, Connection);
                        }
                        else {
                            Connection.RconCommandQueue.Enqueue(
                                RconCommand.ConsoleLogCommand(
                                    formatted, formatted,
                                    e?.SendingPlayer?.Name ?? "SERVER"
                                )
                            );
                        }
                    }                    
                }
			}
        }

        public enum TriggerType
        {
            PlayerJoined,
            PlayerLeft,
            PlayerCount,
            PlayerCountInRange,
            CustomServerCommand,
            Daily,
            EveryXMinutes,
            MatchBegan,
            MatchEnded,
        }

        public enum Operator
        {
            GreatherThan,
            LessThan,
            EqualTo,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo
        }

    }
}
