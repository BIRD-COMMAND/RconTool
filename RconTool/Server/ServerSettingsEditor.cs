using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RconTool
{

	public partial class ServerSettingsEditor : Form
	{
		
		private int currentSelectionIndex = 0;
		private bool unsavedChanges = false;
		ServerSettings settings;

		//NOTE folder browser dialog returns a path like "C:\\Users\\Alex\\Games\\Halo Online"
		// all backslashes are escaped, and there is no ending backslash for a directory
		FolderBrowserDialog folderBrowserDialog;

		public ServerSettingsEditor()
		{

			settings = App.currentConnection.Settings;

			InitializeComponent();

			#region Set up Server Select ComboBox

			comboBoxSelectedServer.Items.Clear();

			string currentServerName;

			for (int i = 0; i < App.connectionList.Count; i++)
			{
				
				currentServerName = App.connectionList[i].Settings.Identifier;

				comboBoxSelectedServer.Items.Add( currentServerName );

				// Set dropdown text to selected server name
				if (App.connectionList[i] == App.currentConnection)
				{
					comboBoxSelectedServer.Text = currentServerName;
					currentSelectionIndex = i;
					comboBoxSelectedServer.SelectedIndex = i;
					comboBoxSelectedServer.SelectedItem = currentServerName;
					comboBoxSelectedServer.Invalidate();
				}

			}

			#endregion

			folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			folderBrowserDialog.Description = 
				"Select the folder where your voting .json files will be stored. " 
				+ "The directory must be a subdirectory of the folder where your game/server executable is located. " 
				+ "The default is \"mods\\server\\\", where the default \"voting.json\" file is located. "
				+ "If you are unsure what directory to use, it is acceptable to use this directory."
			;
			
			buttonBrowsePathToServerExecutable.Click += buttonBrowsePathToServerExecutable_Click;

			SetInputValues();

		}

		private void SetInputValues()
		{

			textBoxRelativeVotingPath.TextChanged -= CheckUnsavedChanges;
			textBoxPathToServerExecutable.TextChanged -= CheckUnsavedChanges;

			textBoxRelativeVotingPath.Text = settings.RelativeVotingPath ?? "";
			textBoxPathToServerExecutable.Text = settings.ServerExecutableDirectoryPath ?? "";

			panelDynamicJsonFiles.Enabled = settings.DynamicVotingFileManagement;

			if (!string.IsNullOrEmpty(settings.VoteFilesDirectoryPath) && !buttonToggleDynamicFileManagement.Enabled)
			{
				buttonToggleDynamicFileManagement.Enabled = true;
				toolTipEnableFileManagement.Active = false;
			}
			if (string.IsNullOrEmpty(settings.VoteFilesDirectoryPath) && buttonToggleDynamicFileManagement.Enabled)
			{
				buttonToggleDynamicFileManagement.Enabled = false;
				toolTipEnableFileManagement.Active = true;
			}

			textBoxRelativeVotingPath.TextChanged += CheckUnsavedChanges;
			textBoxPathToServerExecutable.TextChanged += CheckUnsavedChanges;

			if (settings.DynamicVotingFileManagement)
			{
				buttonToggleDynamicFileManagement.Text = "Disable";
				labelToggleDynamicFileManagement.Text = "Disable Dynamic Voting File Management";
			}
			else
			{
				buttonToggleDynamicFileManagement.Text = "Enable";
				labelToggleDynamicFileManagement.Text = "Enable Dynamic Voting File Management";
			}

			PopulateVotingFilesListView();

		}
		private void PopulateVotingFilesListView()
		{
			listViewDynamicVotingFiles.Items.Clear();
			foreach (VoteFile file in settings.voteFiles)
			{
				listViewDynamicVotingFiles.Items.Add(file.Name);
			}
		
		}

		private void buttonBrowsePathToServerExecutable_Click(object sender, EventArgs e)
		{

			folderBrowserDialog.Description = "Select the folder where your server executable is located. (eldorado.exe)";

			DialogResult result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
				{
					textBoxPathToServerExecutable.Text = folderBrowserDialog.SelectedPath;
				}
				CheckUnsavedChanges(null, null);
			}
		}

		private void comboBoxSelectedServer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxSelectedServer.SelectedIndex != currentSelectionIndex)
			{
				if (unsavedChanges)
				{
					DialogResult dialogResult = MessageBox.Show(
						"You have unsaved changes, would you like to save these changes before selecting another server to configure?" +
						Environment.NewLine
						+ "Unsaved changes will not persist when selecting a different server through the dropdown.",
						"Alert: Unsaved Changes",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Warning
					);

					switch (dialogResult)
					{
						case DialogResult.Cancel:
							comboBoxSelectedServer.SelectedIndex = currentSelectionIndex;
							comboBoxSelectedServer.SelectedItem = App.connectionList[currentSelectionIndex].Settings.Identifier;
							return;
						case DialogResult.Yes: SaveSettings(); break;
						case DialogResult.No: break;
					}

					currentSelectionIndex = comboBoxSelectedServer.SelectedIndex;
					settings = App.connectionList[currentSelectionIndex].Settings;
					SetInputValues();

				}
				else
				{
					currentSelectionIndex = comboBoxSelectedServer.SelectedIndex;
					settings = App.connectionList[currentSelectionIndex].Settings;
					SetInputValues();
				}
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveSettings();
		}
		private bool HasUnsavedChanges() {

			if (textBoxRelativeVotingPath.Text != settings.RelativeVotingPath) { return true; }
			if (textBoxPathToServerExecutable.Text != settings.VoteFilesDirectoryPath) { return true; }
			//if (textBoxPathToGameVariantsDirectory.Text != settings.GameVariantsDirectoryPath) { return true; }
			//if (textBoxPathToMapVariantsDirectory.Text != settings.MapVariantsDirectoryPath) { return true; }
			return false;
		}
		private void CheckUnsavedChanges(object sender, EventArgs e)
		{
			if (HasUnsavedChanges())
			{
				unsavedChanges = true;
				labelUnsavedChangesIndicator.Visible = true;
				pictureBoxUnsavedChangesWarningIcon.Visible = true;
				buttonSave.Enabled = true;
			}
		}
		private void SaveSettings()
		{

			#region Save Directories

			if (textBoxPathToServerExecutable.Text.Contains("/"))
			{
				textBoxPathToServerExecutable.Text = textBoxPathToServerExecutable.Text.Replace('/', '\\');
			}
			while (textBoxPathToServerExecutable.Text.EndsWith("\\"))
			{ 
				textBoxPathToServerExecutable.Text = textBoxPathToServerExecutable.Text.TrimLastCharacter(); 
			}
			string previousDirectory = settings.ServerExecutableDirectoryPath;
			settings.ServerExecutableDirectoryPath = textBoxPathToServerExecutable.Text;			
			if (settings.ServerExecutableDirectory == null)
			{
				settings.ServerExecutableDirectoryPath = previousDirectory;
				MessageBox.Show(
					"The server executable directory could not be located. Please enter the correct directory and try again.",
					"ERROR: Server Executable Directory Not Found",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			if (textBoxRelativeVotingPath.Text.Contains("/"))
			{
				textBoxRelativeVotingPath.Text = textBoxRelativeVotingPath.Text.Replace('/', '\\');
			}
			while(textBoxRelativeVotingPath.Text.StartsWith("\\"))
			{
				textBoxRelativeVotingPath.Text = textBoxRelativeVotingPath.Text.TrimFirstCharacter();
			}
			while (textBoxRelativeVotingPath.Text.EndsWith("\\"))
			{
				textBoxRelativeVotingPath.Text = textBoxRelativeVotingPath.Text.TrimLastCharacter();
			}
			previousDirectory = settings.VoteFilesDirectoryPath;
			settings.VoteFilesDirectoryPath = textBoxPathToServerExecutable.Text + "\\" + textBoxRelativeVotingPath.Text;
			if (settings.VoteFilesDirectory == null)
			{
				settings.VoteFilesDirectoryPath = previousDirectory;
				MessageBox.Show(
					"The voting file directory entered could not be located. Please enter the correct directory and try again.",
					"Alert: Invalid Voting Files Directory",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			settings.GameVariantsDirectoryPath = textBoxPathToServerExecutable.Text + "\\mods\\variants";
			if (settings.GameVariantsDirectory == null)
			{
				settings.GameVariantsDirectoryPath = previousDirectory;
				MessageBox.Show(
					"The game variants directory could not be located.",
					"ERROR: Game Variants Directory Not Found",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			settings.MapVariantsDirectoryPath = textBoxPathToServerExecutable.Text + "\\mods\\maps";
			if (settings.MapVariantsDirectory == null)
			{
				settings.MapVariantsDirectoryPath = previousDirectory;
				MessageBox.Show(
					"The map variants directory could not be located.",
					"ERROR: Map Variants Directory Not Found",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			#endregion

			// Save Relative Voting Path
			settings.RelativeVotingPath = textBoxRelativeVotingPath.Text;
			
			labelUnsavedChangesIndicator.Visible = false;
			pictureBoxUnsavedChangesWarningIcon.Visible = false;
			unsavedChanges = false;

			App.SaveSettings();

			if (!string.IsNullOrEmpty(settings.VoteFilesDirectoryPath) && !buttonToggleDynamicFileManagement.Enabled)
			{
				buttonToggleDynamicFileManagement.Enabled = true;
				toolTipEnableFileManagement.Active = false;
			}
			if (string.IsNullOrEmpty(settings.VoteFilesDirectoryPath) && buttonToggleDynamicFileManagement.Enabled)
			{
				buttonToggleDynamicFileManagement.Enabled = false;
				toolTipEnableFileManagement.Active = true;
			}

			buttonSave.Enabled = false;
			SetInputValues();

		}

		private void buttonToggleDynamicVotingFileManagement_Click(object sender, EventArgs e)
		{

			if (unsavedChanges)
			{
				MessageBox.Show(
					"You must save any unsaved changes before toggling this feature.",
					"Alert: Unsaved Changes",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			if (settings.VoteFilesDirectory == null)
			{
				MessageBox.Show(
					"The voting file directory could not be located. Please enter the correct directory and try again.",
					"Alert: Invalid Voting Files Directory",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			DialogResult saveOkay = MessageBox.Show(
					"Toggling this setting will save all unsaved changes. Click OK to save and continue.",
					"Alert: Changes Will Be Saved",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Warning
				);
			if (saveOkay == DialogResult.Cancel) { return; }

			settings.DynamicVotingFileManagement = !settings.DynamicVotingFileManagement;
			panelDynamicJsonFiles.Enabled = settings.DynamicVotingFileManagement;

			if (settings.DynamicVotingFileManagement)
			{
				buttonToggleDynamicFileManagement.Text = "Disable";
				labelToggleDynamicFileManagement.Text = "Disable Dynamic Voting File Management";
			}
			else
			{
				buttonToggleDynamicFileManagement.Text = "Enable";
				labelToggleDynamicFileManagement.Text = "Enable Dynamic Voting File Management";
			}

			SaveSettings();
			SetInputValues();

		}

		private void buttonAddNewFile_Click(object sender, EventArgs e)
		{

			if (unsavedChanges)
			{
				MessageBox.Show(
					"You must save any unsaved changes before adding new files.",
					"Alert: Unsaved Changes",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			if (settings.VoteFilesDirectory == null) {
				MessageBox.Show(
					"The voting file directory could not be located. Please enter the correct directory and try again.",
					"Alert: Invalid Voting Files Directory",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			AddNewVotingFilePrompt.takenNames = settings.GetVoteFileNames();
			new AddNewVotingFilePrompt().ShowDialog();
			if (AddNewVotingFilePrompt.name != null)
			{
				string name = AddNewVotingFilePrompt.name;
				AddNewVotingFilePrompt.takenNames.Clear();
				AddNewVotingFilePrompt.name = null;
				try { settings.voteFiles.Add(VoteFile.Create(settings.VoteFilesDirectory.FullName, name, settings)); }
				catch (Exception ex) { App.Log(ex.Message, settings); }
				App.SaveSettings();
				SetInputValues();
			}
		}

		private void buttonDeleteFile_Click(object sender, EventArgs e)
		{

			if (unsavedChanges)
			{
				MessageBox.Show(
					"You must save any unsaved changes before deleting files.",
					"Alert: Unsaved Changes",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			if (listViewDynamicVotingFiles.SelectedItems.Count == 0) { return; }

			string prompt = "Are you sure you want to permanently delete all of the following files from your hard drive?" + Environment.NewLine + Environment.NewLine;	
			foreach (ListViewItem item in listViewDynamicVotingFiles.SelectedItems)
			{
				prompt += settings.voteFiles.Find(x => x.Name == item.Text)?.Path + Environment.NewLine ?? "";
			}
			prompt += Environment.NewLine + "All of the above files will be deleted permanently.";

			DialogResult dialogResult = MessageBox.Show( 
				prompt,
				"Alert: Confirm File Deletion",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning
			);

			switch (dialogResult)
			{
				case DialogResult.Cancel: return;
				case DialogResult.OK:
					foreach (ListViewItem item in listViewDynamicVotingFiles.SelectedItems)
					{
						VoteFile current = settings.voteFiles.Find(x => x.Name == item.Text);
						if (current != null) {
							settings.DeleteVoteFile(current);
						}
					}
					break;
				default: return;
			}


			SaveSettings();
			listViewDynamicVotingFiles.Items.Clear();
			SetInputValues();

		}

		private void buttonEditFile_Click(object sender, EventArgs e)
		{
			//TODO FEATURE implement runtime votefile editor form
		}
	}
}
