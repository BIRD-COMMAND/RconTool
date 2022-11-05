using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RconTool
{
	public partial class ServerHookForm : Form
	{

		private Connection connection;
		private List<System.Diagnostics.Process> serverProcesses;

		public ServerHookForm(Connection connection)
		{			
			InitializeComponent();			
			this.connection = connection;			
			checkBoxServerHookAllowed_Set();
			serverProcesses = connection.GetServerProcesses().ToList();
			comboBoxServerProcessDropdown_Populate();
		}

		private void checkBoxServerHookAllowed_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxServerHookAllowed.Checked != (connection.Settings.UseServerHook)) {
				connection.Settings.UseServerHook = checkBoxServerHookAllowed.Checked;
				connection.SaveSettings();
			}
		}
		private void checkBoxServerHookAllowed_Set()
		{
			checkBoxServerHookAllowed.Checked = connection.Settings.UseServerHook;
			checkBoxServerHookAllowed.CheckState = (connection.Settings.UseServerHook) ? CheckState.Checked : CheckState.Unchecked;
		}

		private void buttonServerHookAttemptConnection_Click(object sender, EventArgs e)
		{
			if (!connection.Settings.UseServerHook) { return; }
			try {
				string[] processArgs = ((string)comboBoxServerProcessDropdown?.Text ?? "").Split("[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				if (processArgs.Length > 0 && int.TryParse(processArgs[0], out int pId)) {
					connection.AttemptServerHook(pId);
				}
				Close();
			}
			catch (Exception) { App.Log("ServerHook Failed"); }
		}

		private void comboBoxServerProcessDropdown_Populate()
		{
			comboBoxServerProcessDropdown.Items.Clear();
			foreach (System.Diagnostics.Process process in serverProcesses) {
				try {
					if (process != null) {
						comboBoxServerProcessDropdown.Items.Add($"[{process.Id}] {process.ProcessName}");
					}
				}
				catch (Exception e) { }
			}
		}

	}
}
