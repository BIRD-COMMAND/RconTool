﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RconTool.App;

namespace RconTool
{
	public partial class ToolCommandEditor : Form
	{

        static ToolCommandEditor()
		{
            List<string> vals = new List<string>(ConsoleAutoCompleteStrings);
            vals.AddRange(RuntimeCommand.RuntimeCommandNames);
            autoCompleteTextBoxCommandsList = vals.ToArray();
        }

        bool isEditing = false;
        ToolCommand commandEditTarget;
        ListBox listBox;

        private ToolCommand.TriggerType conditionType = ToolCommand.TriggerType.PlayerJoined;
        private ToolCommand.Operator conditionOperator = ToolCommand.Operator.GreatherThan;

        public static Form commandEditor;
        private static string[] autoCompleteTextBoxCommandsList;

        private List<string> operators = new List<string> { ">", "<", "=", ">=", "<=" };
        private List<string> dailyTimeOptions = new List<string>()
        {
            "12 AM", "1 AM", "2 AM", "3 AM", "4 AM", "5 AM", "6 AM", "7 AM", "8 AM", "9 AM", "10 AM", "11 AM",
            "12 PM", "1 PM", "2 PM", "3 PM", "4 PM", "5 PM", "6 PM", "7 PM", "8 PM", "9 PM", "10 PM", "11 PM"
        };

        public ToolCommandEditor(ListBox listBox = null, ToolCommand command = null)
        {

            InitializeComponent();
            //autoCompleteTextBoxCommands.Values = autoCompleteTextBoxCommandsList;
			autoCompleteTextBoxCommands.HandleCreated += (o, e) =>
			{
                autoCompleteTextBoxCommands.Values = autoCompleteTextBoxCommandsList;
                //List<string> vals = new List<string>(ConsoleAutoCompleteStrings);
                //vals.AddRange((IEnumerable<string>)RuntimeCommand.RuntimeCommands);
                //autoCompleteTextBoxCommands.Values = vals.ToArray();
            };

			commandEditor = this;

            if (listBox != null)
            {
                this.listBox = listBox;
            }
            comboBoxConditionType.DataSource = Enum.GetValues(typeof(ToolCommand.TriggerType));

            if (command != null)
            {

                isEditing = true;
                commandEditTarget = command;

                // set controls to display existing properties

                textBoxName.Text = command.Name;
                conditionType = command.ConditionType;
                comboBoxConditionType.SelectedItem = (conditionType);
                conditionOperator = command.ConditionOperator;
                buttonOperatorSelection_SetText();
                numericUpDownThreshold.Value = command.ConditionThreshold;
                selectionRangeSliderPlayerCountRange.SelectedMin = command.PlayerCountRangeMin;
                selectionRangeSliderPlayerCountRange.SelectedMax = command.PlayerCountRangeMax;
                comboBoxDailyCommandTime.SelectedIndex = command.RunTime;
                textBoxCustomServerCommand.Text = command.CustomServerCommand;
                textBoxCommandTag.Text = command.Tag;

                switch (conditionType)
                {
                    case ToolCommand.TriggerType.EveryXMinutes:
                        numericUpDownRunIntervalMinutes.Value = command.RunTime;
                        checkBoxGlobalCommand.Enabled = true;
                        break;
                    case ToolCommand.TriggerType.Daily:
                        comboBoxDailyCommandTime.SelectedIndex = command.RunTime;
                        checkBoxGlobalCommand.Enabled = true;
                        break;
                    default: break;
                }
                
                if (command.IsGlobalToolCommand) 
                { 
                    checkBoxGlobalCommand.Checked = true; 
                    checkBoxGlobalCommand.CheckState = CheckState.Checked; 
                }

                foreach (string commandString in command.CommandStrings)
                {
                    autoCompleteTextBoxCommands.AppendText(commandString + System.Environment.NewLine);
                }

            }

            textBoxName.Select();
            textBoxName.Focus();

        }

        private void SetHelpText (ToolCommand.TriggerType conditionType)
        {

            string help = "";

            switch (conditionType)
            {
                case ToolCommand.TriggerType.PlayerJoined:
                    labelConditionInformation.Text = "Condition Information: Player Joined";
                    help = "The command will activate each time a player joins the server.";
                    break;
                case ToolCommand.TriggerType.PlayerLeft:
                    labelConditionInformation.Text = "Condition Information: Player Left";
                    help = "The command will activate each time a player leaves the server.";
                    break;
                case ToolCommand.TriggerType.PlayerCount:
                    labelConditionInformation.Text = "Condition Information: Player Count";
                    help = "The command will activate once each time the player count changes and the condition becomes true.";
                    help += Environment.NewLine + Environment.NewLine;
                    help += "For instance, you can use Player Count conditions to load different server voting configuration files.";
                    help += Environment.NewLine;
                    help += "You could have one condition for \"Player Count > 8\" that loads a \"Big Team Battle\" voting file.";
                    help += Environment.NewLine;
                    help += "You could have another condition for \"Player Count < 9\" that loads a \"Small Lobby\" voting file.";
                    help += Environment.NewLine + Environment.NewLine;
                    help += "Setting up the conditions like this ensures that each time the Player Count becomes 9, the \"Big Team Battle\" ";
                    help += "voting file will be loaded, and each time the Player Count becomes 8, the \"Small Lobby\" voting file will be loaded.";
                    break;
                case ToolCommand.TriggerType.PlayerCountInRange:
                    labelConditionInformation.Text = "Condition Information: Player Count In Range";
                    help = "The command will activate once each time the player count enters the specified range. ";
                    help += Environment.NewLine;
                    help += "The range is inclusive of the numbers the sliders rest on.";
                    break;
                case ToolCommand.TriggerType.CustomServerCommand:
                    labelConditionInformation.Text = "Condition Information: Custom Server Command";
                    help = "The command will activate once each time the custom command text is entered in chat by any player.";
                    help += Environment.NewLine;
                    help += "Custom commands prefixed with \"!\" will not show up in chat, but will be accepted ";
                    help += "by the server and will be capable of triggering this conditional command." + Environment.NewLine;
                    help += "However, the \"!\" prefix is not required, and any text can be used to trigger a custom server command.";
                    //help += Environment.NewLine + Environment.NewLine;
                    //help += "Combining custom server commands with "
                    break;
                case ToolCommand.TriggerType.Daily:
                    labelConditionInformation.Text = "Condition Information: Daily";
                    help = "The command will activate at the specified time once per day.";
                    break;
                case ToolCommand.TriggerType.EveryXMinutes:
                    labelConditionInformation.Text = "Condition Information: Every X Minutes";
                    help = "The command will activate once every X minutes." + Environment.NewLine;
                    help += "X represents the number of minutes between activations.";
                    break;
                case ToolCommand.TriggerType.MatchBegan:
                    labelConditionInformation.Text = "Condition Information: Match Began";
                    help = "The command will activate each time a match begins.";
                    break;
                case ToolCommand.TriggerType.MatchEnded:
                    labelConditionInformation.Text = "Condition Information: Match Ended";
                    help = "The command will activate each time a match ends.";
                    break;
            }

            textBoxConditionInformation.Text = help;

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text == "")
            {
                MessageBox.Show("You must specify a name for the command.", "Name Required", MessageBoxButtons.OK);
                return;
            }

            ToolCommand triggerMatch = null;
            if (conditionType == ToolCommand.TriggerType.CustomServerCommand)
            {
                if (string.IsNullOrWhiteSpace(textBoxCustomServerCommand.Text)) {
                    MessageBox.Show("You must specify text for the custom server command.", "Custom Server Command Text Required", MessageBoxButtons.OK);
                    return;
                }
                if (RuntimeCommand.RuntimeCommandAutoCompleteNames.Contains(textBoxCustomServerCommand.Text)) {
                    MessageBox.Show(
                        "There is already a Runtime Command using that trigger, please choose a different trigger.", 
                        "Custom Server Command Trigger Conflict", MessageBoxButtons.OK
                    );
                    return;
                }
                if ((currentConnection?.Settings?.Commands?.Count ?? 0) > 0) {
                    triggerMatch = currentConnection.Settings.Commands.DefaultIfEmpty(null).FirstOrDefault(x => x.CustomServerCommand == textBoxCustomServerCommand.Text);
				}
            }

            ToolCommand nameMatch = null;
            if (GlobalToolCommands.Value.Count > 0)
            {
                nameMatch = GlobalToolCommands.Value.DefaultIfEmpty(null).FirstOrDefault(x => x.Name == textBoxName.Text);
            }
            if (nameMatch == null && currentConnection.Settings.Commands.Count > 0)
            {
                nameMatch = currentConnection.Settings.Commands.DefaultIfEmpty(null).FirstOrDefault(x => x.Name == textBoxName.Text);
            }

            if (isEditing)
            {

                if (nameMatch != null && nameMatch != commandEditTarget)
                {
                    MessageBox.Show("The selected name is used by another conditional command, please choose a unique name.",
                        "Unique Name Required", MessageBoxButtons.OK
                    );
                    return;
                }
                else
                {
                    nameMatch = null;
                }

                if (conditionType == ToolCommand.TriggerType.CustomServerCommand && triggerMatch != null && triggerMatch != commandEditTarget) {
                    MessageBox.Show("The selected trigger is used by another command, please choose a unique trigger.",
                        "Unique Trigger Required", MessageBoxButtons.OK
                    );
                    return;
                }
                else {
                    triggerMatch = null;
				}
                
                if (GlobalToolCommands.Value.Contains(commandEditTarget))
                {
                    GlobalToolCommands.Value.Remove(commandEditTarget);
                }
                else if (currentConnection.Settings.Commands.Contains(commandEditTarget))
                {
                    currentConnection.Settings.Commands.Remove(commandEditTarget);
                }

                commandEditTarget.Disable();
                
                if (listBox != null)
                {
                    listBox.Items.Remove(listBox.SelectedItem);
                }

            }

            if (nameMatch != null)
            {
                MessageBox.Show("The selected name is used by another conditional command, please choose a unique name.", "Unique Name Required", MessageBoxButtons.OK);
                return;
            }

            if (conditionType == ToolCommand.TriggerType.CustomServerCommand && triggerMatch != null) {
                MessageBox.Show("The selected trigger is used by another command, please choose a unique trigger.",
                    "Unique Trigger Required", MessageBoxButtons.OK
                );
                return;
            }

            #region Get Player Count Operator Value
            Enum.TryParse(comboBoxConditionType.SelectedValue.ToString(), out conditionType);

            switch (buttonCondition_PlayerCount_OperatorSelection.Text)
            {
                case ">": conditionOperator = ToolCommand.Operator.GreatherThan; break;
                case "<": conditionOperator = ToolCommand.Operator.LessThan; break;
                case "=": conditionOperator = ToolCommand.Operator.EqualTo; break;
                case ">=": conditionOperator = ToolCommand.Operator.GreaterThanOrEqualTo; break;
                case "<=": conditionOperator = ToolCommand.Operator.LessThanOrEqualTo; break;
            }
            #endregion

            List<string> commands = new List<string>(
                autoCompleteTextBoxCommands.Text.Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries)
            );
            
            int runTime;
            switch (conditionType)
            {
                case ToolCommand.TriggerType.EveryXMinutes:
                    runTime = (int)numericUpDownRunIntervalMinutes.Value;
                    break;
                case ToolCommand.TriggerType.Daily:
                    runTime = comboBoxDailyCommandTime.SelectedIndex;
                    break;
                default: runTime = 0; break;
            }

            ToolCommand command =
                new ToolCommand(
                    textBoxName.Text,
                    true,
                    commands,
                    conditionType,
                    conditionOperator,
                    (int)numericUpDownThreshold.Value,
                    currentConnection.Settings,
                    textBoxCustomServerCommand.Text,
                    selectionRangeSliderPlayerCountRange.SelectedMin,
                    selectionRangeSliderPlayerCountRange.SelectedMax,
                    runTime,
                    textBoxCommandTag.Text
                );

            if (command.IsGlobalToolCommand)
            {
                command.IsGlobalToolCommand = true;
                GlobalToolCommands.Value.Add(command);
            }
            else
            {
                currentConnection.Settings.Commands.Add(command);
                command.Initialize(currentConnection);
            }

            if (listBox != null)
            {
                listBox.Items.Add(
                    new Tuple<string, ToolCommand>(command.Name, command)
                );
            }

            SaveSettings();
                        
            Close();
            
        }

        private void EnableCorrectInput(ToolCommand.TriggerType type)
        {
            TogglePlayerCountInput(type == ToolCommand.TriggerType.PlayerCount);
            TogglePlayerCountInRangeInput(type == ToolCommand.TriggerType.PlayerCountInRange);
            ToggleCustomServerCommandInput(type == ToolCommand.TriggerType.CustomServerCommand);
            ToggleDailyCommandInput(type == ToolCommand.TriggerType.Daily);
            ToggleEveryXMinutesInput(type == ToolCommand.TriggerType.EveryXMinutes);

        }
        private void TogglePlayerCountInput(bool state)
        {
            panelCondition_PlayerCount.Enabled = state;
            panelCondition_PlayerCount.Visible = state;
            //labelCondition_PlayerCount_PlayerCount.Enabled = state;
            //numericUpDownThreshold.Enabled = state;
            //buttonCondition_PlayerCount_OperatorSelection.Enabled = state;
        }
        private void TogglePlayerCountInRangeInput(bool state)
        {
            panelCondition_PlayerCountRange.Enabled = state;
            panelCondition_PlayerCountRange.Visible = state;
            labelPlayerCountRangeSelector.Enabled = state;
            labelCurrentRange.Enabled = state;
            selectionRangeSliderPlayerCountRange.Enabled = state;
            // Update current range label
            selectionRangeSliderPlayerCountRange_SelectionChanged(null, null);
            //labelRange0.Enabled = state;
            //labelRange1.Enabled = state;
            //labelRange2.Enabled = state;
            //labelRange3.Enabled = state;
            //labelRange4.Enabled = state;
            //labelRange5.Enabled = state;
            //labelRange6.Enabled = state;
            //labelRange7.Enabled = state;
            //labelRange8.Enabled = state;
            //labelRange9.Enabled = state;
            //labelRange10.Enabled = state;
            //labelRange11.Enabled = state;
            //labelRange12.Enabled = state;
            //labelRange13.Enabled = state;
            //labelRange14.Enabled = state;
            //labelRange15.Enabled = state;
            //labelRange16.Enabled = state;
        }
        private void ToggleCustomServerCommandInput(bool state)
        {
            panelCustomServerCommand.Enabled = state;
            panelCustomServerCommand.Visible = state;
            //labelCustomServerCommand.Enabled = state;
            //textBoxCustomServerCommand.Enabled = state;
        }
        private void ToggleDailyCommandInput(bool state)
        {
            panelDailyCommandTime.Enabled = state;
            panelDailyCommandTime.Visible = state;
            //labelDailyCommandTime_A.Enabled = state;
            //labelDailyCommandTime_B.Enabled = state;
            //comboBoxDailyCommandTime.Enabled = state;
        }
        private void ToggleEveryXMinutesInput(bool state)
        {
            panelEveryXMinutesInterval.Enabled = state;
            panelEveryXMinutesInterval.Visible = state;
            //labelEveryXMinutesInterval_A.Enabled = state;
            //labelEveryXMinutesInterval_B.Enabled = state;
            //numericUpDownRunIntervalMinutes.Enabled = state;            
        }

        private void buttonCondition_PlayerCount_OperatorSelection_Click(object sender, EventArgs e)
        {
            int i = operators.IndexOf(((Button)sender).Text);
            i += 1;
            if (i > 4) { i = 0; }
            conditionOperator = (ToolCommand.Operator)i;
            buttonOperatorSelection_SetText();
        }
        private void buttonOperatorSelection_SetText()
        {
            buttonCondition_PlayerCount_OperatorSelection.Text = operators[(int)conditionOperator];
        }

        private void comboBoxConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(comboBoxConditionType.SelectedValue.ToString(), out conditionType);
            SetHelpText(conditionType);
            EnableCorrectInput(conditionType);
            if (conditionType == ToolCommand.TriggerType.Daily || conditionType == ToolCommand.TriggerType.EveryXMinutes)
            {
                checkBoxGlobalCommand.Enabled = true;
            }
            else
            {
                checkBoxGlobalCommand.Checked = false;
                checkBoxGlobalCommand.CheckState = CheckState.Unchecked;
                checkBoxGlobalCommand.Enabled = false;
            }
            if (conditionType == ToolCommand.TriggerType.PlayerJoined || conditionType == ToolCommand.TriggerType.PlayerLeft
             || conditionType == ToolCommand.TriggerType.MatchBegan   || conditionType == ToolCommand.TriggerType.MatchEnded)
            {
                panelConditionConfiguration.Enabled = false;
                panelConditionConfiguration.Visible = false;
            }
            else
            {
                panelConditionConfiguration.Enabled = true;
                panelConditionConfiguration.Visible = true;
            }
        }

        private void ConditionalCommandEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            RepopulateConditionalCommandsDropdown = true;
        }

		private void selectionRangeSliderPlayerCountRange_SelectionChanged(object sender, EventArgs e)
		{
            if (selectionRangeSliderPlayerCountRange == null || labelCurrentRange == null) { return; }
            else {
                if (selectionRangeSliderPlayerCountRange.SelectedMin == selectionRangeSliderPlayerCountRange.SelectedMax) {
                    labelCurrentRange.Text = $"[{selectionRangeSliderPlayerCountRange.SelectedMin}]";
				}
                else {
                    labelCurrentRange.Text = $"[{selectionRangeSliderPlayerCountRange.SelectedMin} - {selectionRangeSliderPlayerCountRange.SelectedMax}]";
                }
			}
		}
	}

}
