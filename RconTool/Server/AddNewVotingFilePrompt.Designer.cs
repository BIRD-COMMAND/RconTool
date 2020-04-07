namespace RconTool
{
	partial class AddNewVotingFilePrompt
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewVotingFilePrompt));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.buttonSubmit = new System.Windows.Forms.Button();
			this.labelValidCharacters = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter a name for the new voting JSON file.";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(15, 25);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(205, 20);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
			// 
			// buttonSubmit
			// 
			this.buttonSubmit.Location = new System.Drawing.Point(228, 24);
			this.buttonSubmit.Name = "buttonSubmit";
			this.buttonSubmit.Size = new System.Drawing.Size(75, 22);
			this.buttonSubmit.TabIndex = 2;
			this.buttonSubmit.Text = "Submit";
			this.buttonSubmit.UseVisualStyleBackColor = true;
			this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
			// 
			// labelValidCharacters
			// 
			this.labelValidCharacters.AutoSize = true;
			this.labelValidCharacters.Location = new System.Drawing.Point(25, 52);
			this.labelValidCharacters.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
			this.labelValidCharacters.Name = "labelValidCharacters";
			this.labelValidCharacters.Size = new System.Drawing.Size(263, 13);
			this.labelValidCharacters.TabIndex = 3;
			this.labelValidCharacters.Text = "Valid characters are letters, a-z, A-Z, and numbers 0-9.";
			// 
			// AddNewVotingFilePrompt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(316, 75);
			this.Controls.Add(this.labelValidCharacters);
			this.Controls.Add(this.buttonSubmit);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddNewVotingFilePrompt";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Enter File Name";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Button buttonSubmit;
		private System.Windows.Forms.Label labelValidCharacters;
	}
}