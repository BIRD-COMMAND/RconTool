namespace RconTool
{
	partial class ToolCommandManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolCommandManager));
			this.listBoxCommands = new System.Windows.Forms.ListBox();
			this.buttonAddConditionalCommand = new System.Windows.Forms.Button();
			this.buttonEditConditionalCommand = new System.Windows.Forms.Button();
			this.buttonDeleteConditionalCommand = new System.Windows.Forms.Button();
			this.panelConditionalCommandButtons = new System.Windows.Forms.Panel();
			this.panelConditionalCommandButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBoxCommands
			// 
			this.listBoxCommands.FormattingEnabled = true;
			this.listBoxCommands.Location = new System.Drawing.Point(6, 6);
			this.listBoxCommands.Name = "listBoxCommands";
			this.listBoxCommands.Size = new System.Drawing.Size(287, 134);
			this.listBoxCommands.TabIndex = 0;
			// 
			// buttonAddConditionalCommand
			// 
			this.buttonAddConditionalCommand.AutoSize = true;
			this.buttonAddConditionalCommand.Location = new System.Drawing.Point(3, 3);
			this.buttonAddConditionalCommand.Name = "buttonAddConditionalCommand";
			this.buttonAddConditionalCommand.Size = new System.Drawing.Size(86, 23);
			this.buttonAddConditionalCommand.TabIndex = 1;
			this.buttonAddConditionalCommand.Text = "Add Command";
			this.buttonAddConditionalCommand.UseVisualStyleBackColor = true;
			this.buttonAddConditionalCommand.Click += new System.EventHandler(this.buttonAddConditionalCommand_Click);
			// 
			// buttonEditConditionalCommand
			// 
			this.buttonEditConditionalCommand.AutoSize = true;
			this.buttonEditConditionalCommand.Location = new System.Drawing.Point(95, 3);
			this.buttonEditConditionalCommand.Name = "buttonEditConditionalCommand";
			this.buttonEditConditionalCommand.Size = new System.Drawing.Size(85, 23);
			this.buttonEditConditionalCommand.TabIndex = 2;
			this.buttonEditConditionalCommand.Text = "Edit Command";
			this.buttonEditConditionalCommand.UseVisualStyleBackColor = true;
			this.buttonEditConditionalCommand.Click += new System.EventHandler(this.buttonEditConditionalCommand_Click);
			// 
			// buttonDeleteConditionalCommand
			// 
			this.buttonDeleteConditionalCommand.AutoSize = true;
			this.buttonDeleteConditionalCommand.Location = new System.Drawing.Point(186, 3);
			this.buttonDeleteConditionalCommand.Name = "buttonDeleteConditionalCommand";
			this.buttonDeleteConditionalCommand.Size = new System.Drawing.Size(98, 23);
			this.buttonDeleteConditionalCommand.TabIndex = 3;
			this.buttonDeleteConditionalCommand.Text = "Delete Command";
			this.buttonDeleteConditionalCommand.UseVisualStyleBackColor = true;
			this.buttonDeleteConditionalCommand.Click += new System.EventHandler(this.buttonDeleteConditionalCommand_Click);
			// 
			// panelConditionalCommandButtons
			// 
			this.panelConditionalCommandButtons.AutoSize = true;
			this.panelConditionalCommandButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelConditionalCommandButtons.Controls.Add(this.buttonAddConditionalCommand);
			this.panelConditionalCommandButtons.Controls.Add(this.buttonEditConditionalCommand);
			this.panelConditionalCommandButtons.Controls.Add(this.buttonDeleteConditionalCommand);
			this.panelConditionalCommandButtons.Location = new System.Drawing.Point(6, 146);
			this.panelConditionalCommandButtons.Name = "panelConditionalCommandButtons";
			this.panelConditionalCommandButtons.Size = new System.Drawing.Size(287, 29);
			this.panelConditionalCommandButtons.TabIndex = 4;
			// 
			// ToolCommandManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(299, 181);
			this.Controls.Add(this.panelConditionalCommandButtons);
			this.Controls.Add(this.listBoxCommands);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ToolCommandManager";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Conditional Commands";
			this.panelConditionalCommandButtons.ResumeLayout(false);
			this.panelConditionalCommandButtons.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxCommands;
		private System.Windows.Forms.Button buttonAddConditionalCommand;
		private System.Windows.Forms.Button buttonEditConditionalCommand;
		private System.Windows.Forms.Button buttonDeleteConditionalCommand;
		private System.Windows.Forms.Panel panelConditionalCommandButtons;
	}
}