namespace RconTool
{
	partial class ToolCommandEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolCommandEditor));
			this.labelName = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.comboBoxConditionType = new System.Windows.Forms.ComboBox();
			this.labelConditionType = new System.Windows.Forms.Label();
			this.panelConditionInformation = new System.Windows.Forms.Panel();
			this.textBoxConditionInformation = new System.Windows.Forms.TextBox();
			this.labelConditionInformation = new System.Windows.Forms.Label();
			this.panelCondition_PlayerCount = new System.Windows.Forms.Panel();
			this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
			this.buttonCondition_PlayerCount_OperatorSelection = new System.Windows.Forms.Button();
			this.labelCondition_PlayerCount_PlayerCount = new System.Windows.Forms.Label();
			this.labelCommandEntry = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.panelCommandEntry = new System.Windows.Forms.Panel();
			this.checkBoxGlobalCommand = new System.Windows.Forms.CheckBox();
			this.autoCompleteTextBoxCommands = new RconTool.AutoCompleteTextBox();
			this.textBoxCommandTag = new System.Windows.Forms.TextBox();
			this.labelCommandTag = new System.Windows.Forms.Label();
			this.panelCommandName = new System.Windows.Forms.Panel();
			this.panelCommandType = new System.Windows.Forms.Panel();
			this.flowLayoutPanelCommandNameAndType = new System.Windows.Forms.FlowLayoutPanel();
			this.panelCustomServerCommand = new System.Windows.Forms.Panel();
			this.textBoxCustomServerCommand = new System.Windows.Forms.TextBox();
			this.labelCustomServerCommand = new System.Windows.Forms.Label();
			this.panelCondition_PlayerCountRange = new System.Windows.Forms.Panel();
			this.selectionRangeSliderPlayerCountRange = new RconTool.SelectionRangeSlider();
			this.labelPlayerCountRangeSelector = new System.Windows.Forms.Label();
			this.labelRange0 = new System.Windows.Forms.Label();
			this.labelRange1 = new System.Windows.Forms.Label();
			this.labelRange2 = new System.Windows.Forms.Label();
			this.labelRange3 = new System.Windows.Forms.Label();
			this.labelRange4 = new System.Windows.Forms.Label();
			this.labelRange5 = new System.Windows.Forms.Label();
			this.labelRange6 = new System.Windows.Forms.Label();
			this.labelRange7 = new System.Windows.Forms.Label();
			this.labelRange8 = new System.Windows.Forms.Label();
			this.labelRange9 = new System.Windows.Forms.Label();
			this.labelRange10 = new System.Windows.Forms.Label();
			this.labelRange11 = new System.Windows.Forms.Label();
			this.labelRange12 = new System.Windows.Forms.Label();
			this.labelRange13 = new System.Windows.Forms.Label();
			this.labelRange14 = new System.Windows.Forms.Label();
			this.labelRange15 = new System.Windows.Forms.Label();
			this.labelRange16 = new System.Windows.Forms.Label();
			this.panelConditionConfiguration = new System.Windows.Forms.Panel();
			this.labelConditionConfiguration = new System.Windows.Forms.Label();
			this.panelEveryXMinutesInterval = new System.Windows.Forms.Panel();
			this.labelEveryXMinutesInterval_A = new System.Windows.Forms.Label();
			this.labelEveryXMinutesInterval_B = new System.Windows.Forms.Label();
			this.numericUpDownRunIntervalMinutes = new System.Windows.Forms.NumericUpDown();
			this.panelDailyCommandTime = new System.Windows.Forms.Panel();
			this.labelDailyCommandTime_A = new System.Windows.Forms.Label();
			this.labelDailyCommandTime_B = new System.Windows.Forms.Label();
			this.comboBoxDailyCommandTime = new System.Windows.Forms.ComboBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.toolTipGlobalCommandBlurb = new System.Windows.Forms.ToolTip(this.components);
			this.panelConditionInformation.SuspendLayout();
			this.panelCondition_PlayerCount.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
			this.panelCommandEntry.SuspendLayout();
			this.panelCommandName.SuspendLayout();
			this.panelCommandType.SuspendLayout();
			this.flowLayoutPanelCommandNameAndType.SuspendLayout();
			this.panelCustomServerCommand.SuspendLayout();
			this.panelCondition_PlayerCountRange.SuspendLayout();
			this.panelConditionConfiguration.SuspendLayout();
			this.panelEveryXMinutesInterval.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunIntervalMinutes)).BeginInit();
			this.panelDailyCommandTime.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelName
			// 
			this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(3, 3);
			this.labelName.Margin = new System.Windows.Forms.Padding(3);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(35, 13);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "Name";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(44, 0);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(149, 20);
			this.textBoxName.TabIndex = 1;
			// 
			// comboBoxConditionType
			// 
			this.comboBoxConditionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxConditionType.FormattingEnabled = true;
			this.comboBoxConditionType.Location = new System.Drawing.Point(40, 0);
			this.comboBoxConditionType.Name = "comboBoxConditionType";
			this.comboBoxConditionType.Size = new System.Drawing.Size(153, 21);
			this.comboBoxConditionType.TabIndex = 2;
			this.comboBoxConditionType.SelectedIndexChanged += new System.EventHandler(this.comboBoxConditionType_SelectedIndexChanged);
			// 
			// labelConditionType
			// 
			this.labelConditionType.AutoSize = true;
			this.labelConditionType.Location = new System.Drawing.Point(3, 3);
			this.labelConditionType.Margin = new System.Windows.Forms.Padding(3);
			this.labelConditionType.Name = "labelConditionType";
			this.labelConditionType.Size = new System.Drawing.Size(31, 13);
			this.labelConditionType.TabIndex = 3;
			this.labelConditionType.Text = "Type";
			// 
			// panelConditionInformation
			// 
			this.panelConditionInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelConditionInformation.Controls.Add(this.textBoxConditionInformation);
			this.panelConditionInformation.Controls.Add(this.labelConditionInformation);
			this.panelConditionInformation.Location = new System.Drawing.Point(3, 125);
			this.panelConditionInformation.Name = "panelConditionInformation";
			this.panelConditionInformation.Size = new System.Drawing.Size(411, 96);
			this.panelConditionInformation.TabIndex = 4;
			// 
			// textBoxConditionInformation
			// 
			this.textBoxConditionInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxConditionInformation.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxConditionInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxConditionInformation.Location = new System.Drawing.Point(3, 16);
			this.textBoxConditionInformation.Multiline = true;
			this.textBoxConditionInformation.Name = "textBoxConditionInformation";
			this.textBoxConditionInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxConditionInformation.Size = new System.Drawing.Size(405, 77);
			this.textBoxConditionInformation.TabIndex = 0;
			// 
			// labelConditionInformation
			// 
			this.labelConditionInformation.AutoSize = true;
			this.labelConditionInformation.Location = new System.Drawing.Point(3, 0);
			this.labelConditionInformation.Name = "labelConditionInformation";
			this.labelConditionInformation.Size = new System.Drawing.Size(106, 13);
			this.labelConditionInformation.TabIndex = 5;
			this.labelConditionInformation.Text = "Condition Information";
			// 
			// panelCondition_PlayerCount
			// 
			this.panelCondition_PlayerCount.AutoSize = true;
			this.panelCondition_PlayerCount.Controls.Add(this.numericUpDownThreshold);
			this.panelCondition_PlayerCount.Controls.Add(this.buttonCondition_PlayerCount_OperatorSelection);
			this.panelCondition_PlayerCount.Controls.Add(this.labelCondition_PlayerCount_PlayerCount);
			this.panelCondition_PlayerCount.Enabled = false;
			this.panelCondition_PlayerCount.Location = new System.Drawing.Point(118, 16);
			this.panelCondition_PlayerCount.Name = "panelCondition_PlayerCount";
			this.panelCondition_PlayerCount.Size = new System.Drawing.Size(170, 31);
			this.panelCondition_PlayerCount.TabIndex = 6;
			this.panelCondition_PlayerCount.Visible = false;
			// 
			// numericUpDownThreshold
			// 
			this.numericUpDownThreshold.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.numericUpDownThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numericUpDownThreshold.Location = new System.Drawing.Point(108, 5);
			this.numericUpDownThreshold.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numericUpDownThreshold.Name = "numericUpDownThreshold";
			this.numericUpDownThreshold.Size = new System.Drawing.Size(44, 20);
			this.numericUpDownThreshold.TabIndex = 2;
			this.numericUpDownThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// buttonCondition_PlayerCount_OperatorSelection
			// 
			this.buttonCondition_PlayerCount_OperatorSelection.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonCondition_PlayerCount_OperatorSelection.Location = new System.Drawing.Point(72, 3);
			this.buttonCondition_PlayerCount_OperatorSelection.Name = "buttonCondition_PlayerCount_OperatorSelection";
			this.buttonCondition_PlayerCount_OperatorSelection.Size = new System.Drawing.Size(29, 23);
			this.buttonCondition_PlayerCount_OperatorSelection.TabIndex = 1;
			this.buttonCondition_PlayerCount_OperatorSelection.Text = ">";
			this.buttonCondition_PlayerCount_OperatorSelection.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonCondition_PlayerCount_OperatorSelection.UseVisualStyleBackColor = true;
			this.buttonCondition_PlayerCount_OperatorSelection.Click += new System.EventHandler(this.buttonCondition_PlayerCount_OperatorSelection_Click);
			// 
			// labelCondition_PlayerCount_PlayerCount
			// 
			this.labelCondition_PlayerCount_PlayerCount.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.labelCondition_PlayerCount_PlayerCount.Location = new System.Drawing.Point(3, 7);
			this.labelCondition_PlayerCount_PlayerCount.Name = "labelCondition_PlayerCount_PlayerCount";
			this.labelCondition_PlayerCount_PlayerCount.Size = new System.Drawing.Size(67, 13);
			this.labelCondition_PlayerCount_PlayerCount.TabIndex = 0;
			this.labelCondition_PlayerCount_PlayerCount.Text = "Player Count";
			// 
			// labelCommandEntry
			// 
			this.labelCommandEntry.AutoSize = true;
			this.labelCommandEntry.Location = new System.Drawing.Point(3, 0);
			this.labelCommandEntry.Name = "labelCommandEntry";
			this.labelCommandEntry.Size = new System.Drawing.Size(394, 13);
			this.labelCommandEntry.TabIndex = 8;
			this.labelCommandEntry.Text = "Command Entry : Enter one command per line. Example: Server.Say \"Hello World\"";
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.AutoSize = true;
			this.buttonSave.Location = new System.Drawing.Point(333, 190);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 9;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// panelCommandEntry
			// 
			this.panelCommandEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelCommandEntry.Controls.Add(this.checkBoxGlobalCommand);
			this.panelCommandEntry.Controls.Add(this.autoCompleteTextBoxCommands);
			this.panelCommandEntry.Controls.Add(this.textBoxCommandTag);
			this.panelCommandEntry.Controls.Add(this.labelCommandTag);
			this.panelCommandEntry.Controls.Add(this.buttonSave);
			this.panelCommandEntry.Controls.Add(this.labelCommandEntry);
			this.panelCommandEntry.Location = new System.Drawing.Point(3, 227);
			this.panelCommandEntry.Name = "panelCommandEntry";
			this.panelCommandEntry.Size = new System.Drawing.Size(411, 216);
			this.panelCommandEntry.TabIndex = 10;
			// 
			// checkBoxGlobalCommand
			// 
			this.checkBoxGlobalCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxGlobalCommand.AutoSize = true;
			this.checkBoxGlobalCommand.Enabled = false;
			this.checkBoxGlobalCommand.Location = new System.Drawing.Point(213, 192);
			this.checkBoxGlobalCommand.Name = "checkBoxGlobalCommand";
			this.checkBoxGlobalCommand.Size = new System.Drawing.Size(106, 17);
			this.checkBoxGlobalCommand.TabIndex = 20;
			this.checkBoxGlobalCommand.Text = "Global Command";
			this.toolTipGlobalCommandBlurb.SetToolTip(this.checkBoxGlobalCommand, "Global commands are sent to all connected servers when triggered, and only suppor" +
        "t \'Daily\' and \'Every X Minutes\' command triggers.");
			this.checkBoxGlobalCommand.UseVisualStyleBackColor = true;
			// 
			// autoCompleteTextBoxCommands
			// 
			this.autoCompleteTextBoxCommands.AcceptsTab = true;
			this.autoCompleteTextBoxCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.autoCompleteTextBoxCommands.Location = new System.Drawing.Point(3, 16);
			this.autoCompleteTextBoxCommands.Multiline = true;
			this.autoCompleteTextBoxCommands.Name = "autoCompleteTextBoxCommands";
			this.autoCompleteTextBoxCommands.Size = new System.Drawing.Size(405, 168);
			this.autoCompleteTextBoxCommands.TabIndex = 12;
			this.autoCompleteTextBoxCommands.Values = new string[] {
        "%DisableTimedCommand%",
        "%DisableConditionalCommand%",
        "%DisableCommandsByTag%",
        "%EnableTimedCommand%",
        "%EnableConditionalCommand%",
        "%EnableCommandsByTag%",
        "%player%",
        "%SetNextGame%",
        "%EnableDynamicVoteFileCommands%"};
			// 
			// textBoxCommandTag
			// 
			this.textBoxCommandTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxCommandTag.Location = new System.Drawing.Point(85, 190);
			this.textBoxCommandTag.Name = "textBoxCommandTag";
			this.textBoxCommandTag.Size = new System.Drawing.Size(114, 20);
			this.textBoxCommandTag.TabIndex = 11;
			// 
			// labelCommandTag
			// 
			this.labelCommandTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelCommandTag.AutoSize = true;
			this.labelCommandTag.Location = new System.Drawing.Point(3, 193);
			this.labelCommandTag.Name = "labelCommandTag";
			this.labelCommandTag.Size = new System.Drawing.Size(76, 13);
			this.labelCommandTag.TabIndex = 10;
			this.labelCommandTag.Text = "Command Tag";
			// 
			// panelCommandName
			// 
			this.panelCommandName.Controls.Add(this.textBoxName);
			this.panelCommandName.Controls.Add(this.labelName);
			this.panelCommandName.Location = new System.Drawing.Point(0, 3);
			this.panelCommandName.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.panelCommandName.Name = "panelCommandName";
			this.panelCommandName.Size = new System.Drawing.Size(196, 21);
			this.panelCommandName.TabIndex = 12;
			// 
			// panelCommandType
			// 
			this.panelCommandType.Controls.Add(this.comboBoxConditionType);
			this.panelCommandType.Controls.Add(this.labelConditionType);
			this.panelCommandType.Location = new System.Drawing.Point(199, 3);
			this.panelCommandType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.panelCommandType.Name = "panelCommandType";
			this.panelCommandType.Size = new System.Drawing.Size(196, 21);
			this.panelCommandType.TabIndex = 13;
			// 
			// flowLayoutPanelCommandNameAndType
			// 
			this.flowLayoutPanelCommandNameAndType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelCommandNameAndType.Controls.Add(this.panelCommandName);
			this.flowLayoutPanelCommandNameAndType.Controls.Add(this.panelCommandType);
			this.flowLayoutPanelCommandNameAndType.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanelCommandNameAndType.Name = "flowLayoutPanelCommandNameAndType";
			this.flowLayoutPanelCommandNameAndType.Size = new System.Drawing.Size(411, 27);
			this.flowLayoutPanelCommandNameAndType.TabIndex = 14;
			this.flowLayoutPanelCommandNameAndType.WrapContents = false;
			// 
			// panelCustomServerCommand
			// 
			this.panelCustomServerCommand.Controls.Add(this.textBoxCustomServerCommand);
			this.panelCustomServerCommand.Controls.Add(this.labelCustomServerCommand);
			this.panelCustomServerCommand.Enabled = false;
			this.panelCustomServerCommand.Location = new System.Drawing.Point(2, 16);
			this.panelCustomServerCommand.Name = "panelCustomServerCommand";
			this.panelCustomServerCommand.Size = new System.Drawing.Size(398, 20);
			this.panelCustomServerCommand.TabIndex = 16;
			this.panelCustomServerCommand.Visible = false;
			// 
			// textBoxCustomServerCommand
			// 
			this.textBoxCustomServerCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCustomServerCommand.Location = new System.Drawing.Point(132, 0);
			this.textBoxCustomServerCommand.Name = "textBoxCustomServerCommand";
			this.textBoxCustomServerCommand.Size = new System.Drawing.Size(266, 20);
			this.textBoxCustomServerCommand.TabIndex = 1;
			// 
			// labelCustomServerCommand
			// 
			this.labelCustomServerCommand.AutoSize = true;
			this.labelCustomServerCommand.Location = new System.Drawing.Point(3, 3);
			this.labelCustomServerCommand.Margin = new System.Windows.Forms.Padding(3);
			this.labelCustomServerCommand.Name = "labelCustomServerCommand";
			this.labelCustomServerCommand.Size = new System.Drawing.Size(126, 13);
			this.labelCustomServerCommand.TabIndex = 0;
			this.labelCustomServerCommand.Text = "Custom Server Command";
			// 
			// panelCondition_PlayerCountRange
			// 
			this.panelCondition_PlayerCountRange.Controls.Add(this.selectionRangeSliderPlayerCountRange);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelPlayerCountRangeSelector);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange0);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange1);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange2);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange3);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange4);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange5);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange6);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange7);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange8);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange9);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange10);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange11);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange12);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange13);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange14);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange15);
			this.panelCondition_PlayerCountRange.Controls.Add(this.labelRange16);
			this.panelCondition_PlayerCountRange.Enabled = false;
			this.panelCondition_PlayerCountRange.Location = new System.Drawing.Point(8, 16);
			this.panelCondition_PlayerCountRange.Name = "panelCondition_PlayerCountRange";
			this.panelCondition_PlayerCountRange.Size = new System.Drawing.Size(392, 62);
			this.panelCondition_PlayerCountRange.TabIndex = 17;
			this.panelCondition_PlayerCountRange.Visible = false;
			// 
			// selectionRangeSliderPlayerCountRange
			// 
			this.selectionRangeSliderPlayerCountRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selectionRangeSliderPlayerCountRange.Enabled = false;
			this.selectionRangeSliderPlayerCountRange.Location = new System.Drawing.Point(11, 20);
			this.selectionRangeSliderPlayerCountRange.Max = 16;
			this.selectionRangeSliderPlayerCountRange.Min = 0;
			this.selectionRangeSliderPlayerCountRange.Name = "selectionRangeSliderPlayerCountRange";
			this.selectionRangeSliderPlayerCountRange.SelectedMax = 12;
			this.selectionRangeSliderPlayerCountRange.SelectedMin = 4;
			this.selectionRangeSliderPlayerCountRange.Size = new System.Drawing.Size(372, 23);
			this.selectionRangeSliderPlayerCountRange.TabIndex = 15;
			// 
			// labelPlayerCountRangeSelector
			// 
			this.labelPlayerCountRangeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelPlayerCountRangeSelector.AutoSize = true;
			this.labelPlayerCountRangeSelector.Location = new System.Drawing.Point(125, 4);
			this.labelPlayerCountRangeSelector.Name = "labelPlayerCountRangeSelector";
			this.labelPlayerCountRangeSelector.Size = new System.Drawing.Size(144, 13);
			this.labelPlayerCountRangeSelector.TabIndex = 18;
			this.labelPlayerCountRangeSelector.Text = "Player Count Range Selector";
			// 
			// labelRange0
			// 
			this.labelRange0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange0.AutoSize = true;
			this.labelRange0.Location = new System.Drawing.Point(5, 46);
			this.labelRange0.Name = "labelRange0";
			this.labelRange0.Size = new System.Drawing.Size(13, 13);
			this.labelRange0.TabIndex = 16;
			this.labelRange0.Text = "0";
			// 
			// labelRange1
			// 
			this.labelRange1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange1.AutoSize = true;
			this.labelRange1.Location = new System.Drawing.Point(28, 46);
			this.labelRange1.Name = "labelRange1";
			this.labelRange1.Size = new System.Drawing.Size(13, 13);
			this.labelRange1.TabIndex = 17;
			this.labelRange1.Text = "1";
			// 
			// labelRange2
			// 
			this.labelRange2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange2.AutoSize = true;
			this.labelRange2.Location = new System.Drawing.Point(52, 46);
			this.labelRange2.Name = "labelRange2";
			this.labelRange2.Size = new System.Drawing.Size(13, 13);
			this.labelRange2.TabIndex = 18;
			this.labelRange2.Text = "2";
			// 
			// labelRange3
			// 
			this.labelRange3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange3.AutoSize = true;
			this.labelRange3.Location = new System.Drawing.Point(75, 46);
			this.labelRange3.Name = "labelRange3";
			this.labelRange3.Size = new System.Drawing.Size(13, 13);
			this.labelRange3.TabIndex = 19;
			this.labelRange3.Text = "3";
			// 
			// labelRange4
			// 
			this.labelRange4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange4.AutoSize = true;
			this.labelRange4.Location = new System.Drawing.Point(97, 46);
			this.labelRange4.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelRange4.Name = "labelRange4";
			this.labelRange4.Size = new System.Drawing.Size(13, 13);
			this.labelRange4.TabIndex = 20;
			this.labelRange4.Text = "4";
			// 
			// labelRange5
			// 
			this.labelRange5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange5.AutoSize = true;
			this.labelRange5.Location = new System.Drawing.Point(121, 46);
			this.labelRange5.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
			this.labelRange5.Name = "labelRange5";
			this.labelRange5.Size = new System.Drawing.Size(13, 13);
			this.labelRange5.TabIndex = 21;
			this.labelRange5.Text = "5";
			// 
			// labelRange6
			// 
			this.labelRange6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange6.AutoSize = true;
			this.labelRange6.Location = new System.Drawing.Point(144, 46);
			this.labelRange6.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
			this.labelRange6.Name = "labelRange6";
			this.labelRange6.Size = new System.Drawing.Size(13, 13);
			this.labelRange6.TabIndex = 22;
			this.labelRange6.Text = "6";
			// 
			// labelRange7
			// 
			this.labelRange7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange7.AutoSize = true;
			this.labelRange7.Location = new System.Drawing.Point(167, 46);
			this.labelRange7.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
			this.labelRange7.Name = "labelRange7";
			this.labelRange7.Size = new System.Drawing.Size(13, 13);
			this.labelRange7.TabIndex = 23;
			this.labelRange7.Text = "7";
			// 
			// labelRange8
			// 
			this.labelRange8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange8.AutoSize = true;
			this.labelRange8.Location = new System.Drawing.Point(190, 46);
			this.labelRange8.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
			this.labelRange8.Name = "labelRange8";
			this.labelRange8.Size = new System.Drawing.Size(13, 13);
			this.labelRange8.TabIndex = 24;
			this.labelRange8.Text = "8";
			// 
			// labelRange9
			// 
			this.labelRange9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange9.AutoSize = true;
			this.labelRange9.Location = new System.Drawing.Point(213, 46);
			this.labelRange9.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
			this.labelRange9.Name = "labelRange9";
			this.labelRange9.Size = new System.Drawing.Size(13, 13);
			this.labelRange9.TabIndex = 25;
			this.labelRange9.Text = "9";
			// 
			// labelRange10
			// 
			this.labelRange10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange10.AutoSize = true;
			this.labelRange10.Location = new System.Drawing.Point(233, 46);
			this.labelRange10.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
			this.labelRange10.Name = "labelRange10";
			this.labelRange10.Size = new System.Drawing.Size(19, 13);
			this.labelRange10.TabIndex = 26;
			this.labelRange10.Text = "10";
			// 
			// labelRange11
			// 
			this.labelRange11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange11.AutoSize = true;
			this.labelRange11.Location = new System.Drawing.Point(256, 46);
			this.labelRange11.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
			this.labelRange11.Name = "labelRange11";
			this.labelRange11.Size = new System.Drawing.Size(19, 13);
			this.labelRange11.TabIndex = 27;
			this.labelRange11.Text = "11";
			// 
			// labelRange12
			// 
			this.labelRange12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange12.AutoSize = true;
			this.labelRange12.Location = new System.Drawing.Point(280, 46);
			this.labelRange12.Margin = new System.Windows.Forms.Padding(2, 0, 3, 0);
			this.labelRange12.Name = "labelRange12";
			this.labelRange12.Size = new System.Drawing.Size(19, 13);
			this.labelRange12.TabIndex = 28;
			this.labelRange12.Text = "12";
			// 
			// labelRange13
			// 
			this.labelRange13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange13.AutoSize = true;
			this.labelRange13.Location = new System.Drawing.Point(303, 46);
			this.labelRange13.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
			this.labelRange13.Name = "labelRange13";
			this.labelRange13.Size = new System.Drawing.Size(19, 13);
			this.labelRange13.TabIndex = 29;
			this.labelRange13.Text = "13";
			// 
			// labelRange14
			// 
			this.labelRange14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange14.AutoSize = true;
			this.labelRange14.Location = new System.Drawing.Point(326, 46);
			this.labelRange14.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
			this.labelRange14.Name = "labelRange14";
			this.labelRange14.Size = new System.Drawing.Size(19, 13);
			this.labelRange14.TabIndex = 30;
			this.labelRange14.Text = "14";
			// 
			// labelRange15
			// 
			this.labelRange15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange15.AutoSize = true;
			this.labelRange15.Location = new System.Drawing.Point(349, 46);
			this.labelRange15.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
			this.labelRange15.Name = "labelRange15";
			this.labelRange15.Size = new System.Drawing.Size(19, 13);
			this.labelRange15.TabIndex = 31;
			this.labelRange15.Text = "15";
			// 
			// labelRange16
			// 
			this.labelRange16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRange16.AutoSize = true;
			this.labelRange16.Location = new System.Drawing.Point(372, 46);
			this.labelRange16.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
			this.labelRange16.Name = "labelRange16";
			this.labelRange16.Size = new System.Drawing.Size(19, 13);
			this.labelRange16.TabIndex = 32;
			this.labelRange16.Text = "16";
			// 
			// panelConditionConfiguration
			// 
			this.panelConditionConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelConditionConfiguration.AutoSize = true;
			this.panelConditionConfiguration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelConditionConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelConditionConfiguration.Controls.Add(this.labelConditionConfiguration);
			this.panelConditionConfiguration.Controls.Add(this.panelEveryXMinutesInterval);
			this.panelConditionConfiguration.Controls.Add(this.panelDailyCommandTime);
			this.panelConditionConfiguration.Controls.Add(this.panelCondition_PlayerCountRange);
			this.panelConditionConfiguration.Controls.Add(this.panelCondition_PlayerCount);
			this.panelConditionConfiguration.Controls.Add(this.panelCustomServerCommand);
			this.panelConditionConfiguration.Location = new System.Drawing.Point(3, 36);
			this.panelConditionConfiguration.Name = "panelConditionConfiguration";
			this.panelConditionConfiguration.Size = new System.Drawing.Size(411, 83);
			this.panelConditionConfiguration.TabIndex = 18;
			// 
			// labelConditionConfiguration
			// 
			this.labelConditionConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelConditionConfiguration.Location = new System.Drawing.Point(0, 0);
			this.labelConditionConfiguration.Name = "labelConditionConfiguration";
			this.labelConditionConfiguration.Size = new System.Drawing.Size(406, 13);
			this.labelConditionConfiguration.TabIndex = 19;
			this.labelConditionConfiguration.Text = "Condition Configuration";
			this.labelConditionConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelEveryXMinutesInterval
			// 
			this.panelEveryXMinutesInterval.AutoSize = true;
			this.panelEveryXMinutesInterval.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelEveryXMinutesInterval.Controls.Add(this.labelEveryXMinutesInterval_A);
			this.panelEveryXMinutesInterval.Controls.Add(this.labelEveryXMinutesInterval_B);
			this.panelEveryXMinutesInterval.Controls.Add(this.numericUpDownRunIntervalMinutes);
			this.panelEveryXMinutesInterval.Enabled = false;
			this.panelEveryXMinutesInterval.Location = new System.Drawing.Point(96, 16);
			this.panelEveryXMinutesInterval.Name = "panelEveryXMinutesInterval";
			this.panelEveryXMinutesInterval.Size = new System.Drawing.Size(217, 25);
			this.panelEveryXMinutesInterval.TabIndex = 26;
			this.panelEveryXMinutesInterval.Visible = false;
			// 
			// labelEveryXMinutesInterval_A
			// 
			this.labelEveryXMinutesInterval_A.AutoSize = true;
			this.labelEveryXMinutesInterval_A.Location = new System.Drawing.Point(-3, 4);
			this.labelEveryXMinutesInterval_A.Name = "labelEveryXMinutesInterval_A";
			this.labelEveryXMinutesInterval_A.Size = new System.Drawing.Size(107, 13);
			this.labelEveryXMinutesInterval_A.TabIndex = 21;
			this.labelEveryXMinutesInterval_A.Text = "Run Command Every";
			// 
			// labelEveryXMinutesInterval_B
			// 
			this.labelEveryXMinutesInterval_B.AutoSize = true;
			this.labelEveryXMinutesInterval_B.Location = new System.Drawing.Point(170, 4);
			this.labelEveryXMinutesInterval_B.Name = "labelEveryXMinutesInterval_B";
			this.labelEveryXMinutesInterval_B.Size = new System.Drawing.Size(44, 13);
			this.labelEveryXMinutesInterval_B.TabIndex = 23;
			this.labelEveryXMinutesInterval_B.Text = "Minutes";
			// 
			// numericUpDownRunIntervalMinutes
			// 
			this.numericUpDownRunIntervalMinutes.Location = new System.Drawing.Point(110, 2);
			this.numericUpDownRunIntervalMinutes.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
			this.numericUpDownRunIntervalMinutes.Name = "numericUpDownRunIntervalMinutes";
			this.numericUpDownRunIntervalMinutes.Size = new System.Drawing.Size(54, 20);
			this.numericUpDownRunIntervalMinutes.TabIndex = 20;
			this.numericUpDownRunIntervalMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDownRunIntervalMinutes.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// panelDailyCommandTime
			// 
			this.panelDailyCommandTime.AutoSize = true;
			this.panelDailyCommandTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelDailyCommandTime.Controls.Add(this.labelDailyCommandTime_A);
			this.panelDailyCommandTime.Controls.Add(this.labelDailyCommandTime_B);
			this.panelDailyCommandTime.Controls.Add(this.comboBoxDailyCommandTime);
			this.panelDailyCommandTime.Enabled = false;
			this.panelDailyCommandTime.Location = new System.Drawing.Point(96, 16);
			this.panelDailyCommandTime.Name = "panelDailyCommandTime";
			this.panelDailyCommandTime.Size = new System.Drawing.Size(220, 30);
			this.panelDailyCommandTime.TabIndex = 25;
			this.panelDailyCommandTime.Visible = false;
			// 
			// labelDailyCommandTime_A
			// 
			this.labelDailyCommandTime_A.AutoSize = true;
			this.labelDailyCommandTime_A.Location = new System.Drawing.Point(-2, 9);
			this.labelDailyCommandTime_A.Name = "labelDailyCommandTime_A";
			this.labelDailyCommandTime_A.Size = new System.Drawing.Size(90, 13);
			this.labelDailyCommandTime_A.TabIndex = 22;
			this.labelDailyCommandTime_A.Text = "Run Command At";
			// 
			// labelDailyCommandTime_B
			// 
			this.labelDailyCommandTime_B.AutoSize = true;
			this.labelDailyCommandTime_B.Location = new System.Drawing.Point(161, 9);
			this.labelDailyCommandTime_B.Name = "labelDailyCommandTime_B";
			this.labelDailyCommandTime_B.Size = new System.Drawing.Size(56, 13);
			this.labelDailyCommandTime_B.TabIndex = 24;
			this.labelDailyCommandTime_B.Text = "Every Day";
			// 
			// comboBoxDailyCommandTime
			// 
			this.comboBoxDailyCommandTime.FormattingEnabled = true;
			this.comboBoxDailyCommandTime.Items.AddRange(new object[] {
            "12 AM",
            "1 AM",
            "2 AM",
            "3 AM",
            "4 AM",
            "5 AM",
            "6 AM",
            "7 AM",
            "8 AM",
            "9 AM",
            "10 AM",
            "11 AM",
            "12 PM",
            "1 PM",
            "2 PM",
            "3 PM",
            "4 PM",
            "5 PM",
            "6 PM",
            "7 PM",
            "8 PM",
            "9 PM",
            "10 PM",
            "11 PM"});
			this.comboBoxDailyCommandTime.Location = new System.Drawing.Point(94, 6);
			this.comboBoxDailyCommandTime.Name = "comboBoxDailyCommandTime";
			this.comboBoxDailyCommandTime.Size = new System.Drawing.Size(60, 21);
			this.comboBoxDailyCommandTime.TabIndex = 19;
			this.comboBoxDailyCommandTime.Text = "12 AM";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanelCommandNameAndType);
			this.flowLayoutPanel1.Controls.Add(this.panelConditionConfiguration);
			this.flowLayoutPanel1.Controls.Add(this.panelConditionInformation);
			this.flowLayoutPanel1.Controls.Add(this.panelCommandEntry);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 2);
			this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(412, 352);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(417, 446);
			this.flowLayoutPanel1.TabIndex = 20;
			// 
			// toolTipGlobalCommandBlurb
			// 
			this.toolTipGlobalCommandBlurb.ToolTipTitle = "Global Command";
			// 
			// ToolCommandEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(418, 449);
			this.Controls.Add(this.flowLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(440, 490);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(20, 39);
			this.Name = "ToolCommandEditor";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Command Editor";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConditionalCommandEditor_FormClosing);
			this.panelConditionInformation.ResumeLayout(false);
			this.panelConditionInformation.PerformLayout();
			this.panelCondition_PlayerCount.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
			this.panelCommandEntry.ResumeLayout(false);
			this.panelCommandEntry.PerformLayout();
			this.panelCommandName.ResumeLayout(false);
			this.panelCommandName.PerformLayout();
			this.panelCommandType.ResumeLayout(false);
			this.panelCommandType.PerformLayout();
			this.flowLayoutPanelCommandNameAndType.ResumeLayout(false);
			this.panelCustomServerCommand.ResumeLayout(false);
			this.panelCustomServerCommand.PerformLayout();
			this.panelCondition_PlayerCountRange.ResumeLayout(false);
			this.panelCondition_PlayerCountRange.PerformLayout();
			this.panelConditionConfiguration.ResumeLayout(false);
			this.panelConditionConfiguration.PerformLayout();
			this.panelEveryXMinutesInterval.ResumeLayout(false);
			this.panelEveryXMinutesInterval.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunIntervalMinutes)).EndInit();
			this.panelDailyCommandTime.ResumeLayout(false);
			this.panelDailyCommandTime.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.ComboBox comboBoxConditionType;
		private System.Windows.Forms.Label labelConditionType;
		private System.Windows.Forms.Panel panelConditionInformation;
		private System.Windows.Forms.TextBox textBoxConditionInformation;
		private System.Windows.Forms.Label labelConditionInformation;
		private System.Windows.Forms.Panel panelCondition_PlayerCount;
		private System.Windows.Forms.Label labelCommandEntry;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelCondition_PlayerCount_PlayerCount;
		private System.Windows.Forms.Button buttonCondition_PlayerCount_OperatorSelection;
		private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
		private System.Windows.Forms.Panel panelCommandEntry;
		private System.Windows.Forms.Panel panelCommandName;
		private System.Windows.Forms.Panel panelCommandType;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCommandNameAndType;
		private RconTool.SelectionRangeSlider selectionRangeSliderPlayerCountRange;
		private System.Windows.Forms.Panel panelCustomServerCommand;
		private System.Windows.Forms.TextBox textBoxCustomServerCommand;
		private System.Windows.Forms.Label labelCustomServerCommand;
		private System.Windows.Forms.Panel panelCondition_PlayerCountRange;
		private System.Windows.Forms.Label labelRange0;
		private System.Windows.Forms.Label labelRange1;
		private System.Windows.Forms.Label labelRange2;
		private System.Windows.Forms.Label labelRange3;
		private System.Windows.Forms.Label labelRange4;
		private System.Windows.Forms.Label labelRange5;
		private System.Windows.Forms.Label labelRange6;
		private System.Windows.Forms.Label labelRange7;
		private System.Windows.Forms.Label labelRange8;
		private System.Windows.Forms.Label labelRange9;
		private System.Windows.Forms.Label labelRange10;
		private System.Windows.Forms.Label labelRange11;
		private System.Windows.Forms.Label labelRange12;
		private System.Windows.Forms.Label labelRange13;
		private System.Windows.Forms.Label labelRange14;
		private System.Windows.Forms.Label labelRange15;
		private System.Windows.Forms.Label labelRange16;
		private System.Windows.Forms.Panel panelConditionConfiguration;
		private System.Windows.Forms.Label labelConditionConfiguration;
		private System.Windows.Forms.Label labelPlayerCountRangeSelector;
		private System.Windows.Forms.TextBox textBoxCommandTag;
		private System.Windows.Forms.Label labelCommandTag;
		private System.Windows.Forms.ComboBox comboBoxDailyCommandTime;
		private System.Windows.Forms.Label labelEveryXMinutesInterval_A;
		private System.Windows.Forms.Label labelEveryXMinutesInterval_B;
		private System.Windows.Forms.Label labelDailyCommandTime_A;
		private System.Windows.Forms.NumericUpDown numericUpDownRunIntervalMinutes;
		private System.Windows.Forms.Label labelDailyCommandTime_B;
		private System.Windows.Forms.Panel panelEveryXMinutesInterval;
		private System.Windows.Forms.Panel panelDailyCommandTime;
		private AutoCompleteTextBox autoCompleteTextBoxCommands;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.CheckBox checkBoxGlobalCommand;
		private System.Windows.Forms.ToolTip toolTipGlobalCommandBlurb;
	}
}