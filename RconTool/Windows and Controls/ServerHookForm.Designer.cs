
namespace RconTool
{
	partial class ServerHookForm
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
			if (disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerHookForm));
			this.checkBoxServerHookAllowed = new System.Windows.Forms.CheckBox();
			this.buttonServerHookAttemptConnection = new System.Windows.Forms.Button();
			this.comboBoxServerProcessDropdown = new System.Windows.Forms.ComboBox();
			this.labelServerProcessIdDropdown = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// checkBoxServerHookAllowed
			// 
			this.checkBoxServerHookAllowed.AutoSize = true;
			this.checkBoxServerHookAllowed.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.checkBoxServerHookAllowed.Location = new System.Drawing.Point(12, 16);
			this.checkBoxServerHookAllowed.Name = "checkBoxServerHookAllowed";
			this.checkBoxServerHookAllowed.Size = new System.Drawing.Size(136, 19);
			this.checkBoxServerHookAllowed.TabIndex = 5;
			this.checkBoxServerHookAllowed.Text = "Server Hook Allowed";
			this.checkBoxServerHookAllowed.UseVisualStyleBackColor = true;
			this.checkBoxServerHookAllowed.CheckedChanged += new System.EventHandler(this.checkBoxServerHookAllowed_CheckedChanged);
			// 
			// buttonServerHookAttemptConnection
			// 
			this.buttonServerHookAttemptConnection.AutoSize = true;
			this.buttonServerHookAttemptConnection.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.buttonServerHookAttemptConnection.Location = new System.Drawing.Point(261, 66);
			this.buttonServerHookAttemptConnection.Name = "buttonServerHookAttemptConnection";
			this.buttonServerHookAttemptConnection.Size = new System.Drawing.Size(75, 25);
			this.buttonServerHookAttemptConnection.TabIndex = 6;
			this.buttonServerHookAttemptConnection.Text = "Connect";
			this.buttonServerHookAttemptConnection.UseVisualStyleBackColor = true;
			this.buttonServerHookAttemptConnection.Click += new System.EventHandler(this.buttonServerHookAttemptConnection_Click);
			// 
			// comboBoxServerProcessDropdown
			// 
			this.comboBoxServerProcessDropdown.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.comboBoxServerProcessDropdown.FormattingEnabled = true;
			this.comboBoxServerProcessDropdown.Location = new System.Drawing.Point(12, 67);
			this.comboBoxServerProcessDropdown.Name = "comboBoxServerProcessDropdown";
			this.comboBoxServerProcessDropdown.Size = new System.Drawing.Size(243, 23);
			this.comboBoxServerProcessDropdown.TabIndex = 7;
			this.comboBoxServerProcessDropdown.Text = "Select server process or enter process ID...";
			// 
			// labelServerProcessIdDropdown
			// 
			this.labelServerProcessIdDropdown.AutoSize = true;
			this.labelServerProcessIdDropdown.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.labelServerProcessIdDropdown.Location = new System.Drawing.Point(12, 49);
			this.labelServerProcessIdDropdown.Name = "labelServerProcessIdDropdown";
			this.labelServerProcessIdDropdown.Size = new System.Drawing.Size(96, 15);
			this.labelServerProcessIdDropdown.TabIndex = 9;
			this.labelServerProcessIdDropdown.Text = "Server Process ID";
			// 
			// ServerHookForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(345, 102);
			this.Controls.Add(this.labelServerProcessIdDropdown);
			this.Controls.Add(this.comboBoxServerProcessDropdown);
			this.Controls.Add(this.buttonServerHookAttemptConnection);
			this.Controls.Add(this.checkBoxServerHookAllowed);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ServerHookForm";
			this.Text = "Server Hook";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxServerHookAllowed;
		private System.Windows.Forms.Button buttonServerHookAttemptConnection;
		private System.Windows.Forms.ComboBox comboBoxServerProcessDropdown;
		private System.Windows.Forms.Label labelServerProcessIdDropdown;
	}
}