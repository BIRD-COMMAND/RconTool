using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static RconTool.App;
using static RconTool.ServerManager;

namespace RconTool
{
    public partial class ServerEditor : Form
    {

        private Connection connection;
        private ListBox listbox;

        private DirectoryInfo gameExecutableDirectoryInfo;
        private bool isEditing = false;
        private bool alreadyHasFilteredPhrases = false;


        // ServerEditor Window when adding a new server connection
        public ServerEditor(ListBox listbox)
        {
            InitializeComponent();
            this.listbox = listbox;
            comboBoxServerLanguage.SelectedIndex = 0;
            textBoxCommandsToSendUponConnection.Text = $"Server.SendChatToRconClients 1{Environment.NewLine}";
            pictureBoxMapsFolderFoundIndicator.Image = XMarkDisabledImage;
            pictureBoxGamesFolderFoundIndicator.Image = XMarkDisabledImage;
            pictureBoxVoteFilesFolderFoundIndicator.Image = XMarkDisabledImage;
            UpdateLocalFileUseInterfaceElements();
        }

        // ServerEditor Window when editing a server connection
        public ServerEditor(Connection connections, ListBox listbox)
        {
            this.listbox = listbox;
            connection = connections;
            ServerSettings info = connections.Settings;
            isEditing = true;
            InitializeComponent();
            textBoxServerIP.Text = info.Ip;
            textBoxServerPort.Text = info.InfoPort;
            textBoxServerPassword.Text = info.ServerPassword;
            textBoxRconPassword.Text = info.RconPassword;
            textBoxRconPort.Text = info.RconPort;
            textBoxName.Text = info.Name ?? "";
            checkBoxEnableAutoTranslate.Checked = info.AutoTranslateChatMessages;
            checkBoxIncludeDefaultFilteredPhrases.Checked = true;
            alreadyHasFilteredPhrases = true;
            foreach (string item in Translation.DefaultFilteredEnglishPhrases) {
                if (!info.AutoTranslateIgnoredPhrasesList.Contains(item)) {
                    checkBoxIncludeDefaultFilteredPhrases.Checked = false;
                    alreadyHasFilteredPhrases = false;
                    break;
                }
			}
            string commandlist = "";
            if (!(info.SendOnConnectCommands == null || info.SendOnConnectCommands.Count == 0))
            {
                foreach (string sc in info.SendOnConnectCommands)
                {
                    commandlist = commandlist + sc + System.Environment.NewLine;
                }
            }
            textBoxCommandsToSendUponConnection.Text = commandlist;
            if (!string.IsNullOrWhiteSpace(info.ServerLanguage) && Translation.EnglishLanguageNameStringsByLanguageCode.ContainsKey(info.ServerLanguage))
			{
                comboBoxServerLanguage.SelectedItem = Translation.EnglishLanguageNameStringsByLanguageCode[info.ServerLanguage];
            }
            else
			{
                info.ServerLanguage = "en"; comboBoxServerLanguage.SelectedIndex = 0;
			}

            checkBoxUseServerHook.Checked = info.UseServerHook;
            if (info.ServerExecutableDirectory?.Exists ?? false) {
                gameExecutableDirectoryInfo = info.ServerExecutableDirectory;
			}
            checkBoxUseLocalData.Checked = info.UseLocalFiles;
            UpdateLocalFileUseInterfaceElements();
            
        }

        private bool IsValidIPAddress(string input)
        {
            return IPAddress.TryParse(input, out IPAddress address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
        }

        private void buttonSaveServer_Click(object sender, EventArgs e)
        {
            
			bool isPortValid = int.TryParse(textBoxServerPort.Text, out _);

            if (string.IsNullOrWhiteSpace(textBoxServerIP.Text))
            {
                MessageBox.Show("Server IP cannot be blank!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBoxServerPort.Text))
            {
                MessageBox.Show("InfoServer Port cannot be blank!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsValidIPAddress(textBoxServerIP.Text))
            {
                MessageBox.Show("Server IP must be valid!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!isPortValid)
            {
                MessageBox.Show("InfoServer Port must be valid!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                
                textBoxCommandsToSendUponConnection.Text = textBoxCommandsToSendUponConnection.Text.StandardizeLineBreaks();
                List<string> onConnectCommands = new List<string>(textBoxCommandsToSendUponConnection.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));

                string serverLanguage;
                string selectedLanguage = (string)comboBoxServerLanguage.SelectedItem;
                if (Translation.LanguageCodesByEnglishLanguageNameString.ContainsKey(selectedLanguage)) {
                    serverLanguage = Translation.LanguageCodesByEnglishLanguageNameString[selectedLanguage];
                }
                else { serverLanguage = "en"; }

                if (isEditing)
                {

                    listbox.Items.Remove(listbox.SelectedItem);
                    connection.Settings.Ip = textBoxServerIP.Text;
                    connection.Settings.InfoPort = textBoxServerPort.Text;
                    connection.Settings.ServerPassword = textBoxServerPassword.Text;
                    connection.Settings.RconPassword = textBoxRconPassword.Text;
                    connection.Settings.RconPort = textBoxRconPort.Text;
                    connection.Settings.Name = textBoxName.Text;
                    connection.Settings.SendOnConnectCommands = onConnectCommands;
                    connection.Settings.ServerLanguage = serverLanguage;
                    connection.Settings.AutoTranslateChatMessages = checkBoxEnableAutoTranslate.Checked;
                    connection.Settings.UseLocalFiles = checkBoxUseLocalData.Checked;
                    connection.Settings.UseServerHook = checkBoxUseServerHook.Checked;

                    if (gameExecutableDirectoryInfo?.Exists ?? false) {
                        // Add Server Directory Path
                        connection.Settings.ServerExecutableDirectoryPath =
                            gameExecutableDirectoryInfo.FullName;
                        // Add Map Variants folder if found
                        if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\maps")) {
                            connection.Settings.MapVariantsDirectoryPath =
                                gameExecutableDirectoryInfo.FullName + "\\mods\\maps";
                        }
                        else { connection.Settings.MapVariantsDirectoryPath = ""; }
                        // Add Game Variants folder if found
                        if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\variants")) {
                            connection.Settings.GameVariantsDirectoryPath =
                                gameExecutableDirectoryInfo.FullName + "\\mods\\variants";
                        }
                        else { connection.Settings.GameVariantsDirectoryPath = ""; }
                        // Add Vote Files folder if found
                        if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\server")) {
                            connection.Settings.VoteFilesDirectoryPath =
                                gameExecutableDirectoryInfo.FullName + "\\mods\\server";
                        }
                        else { connection.Settings.VoteFilesDirectoryPath = ""; }
                    }
                    else {
                        connection.Settings.ServerExecutableDirectoryPath = "";
                        connection.Settings.MapVariantsDirectoryPath = "";
                        connection.Settings.GameVariantsDirectoryPath = "";
                        connection.Settings.VoteFilesDirectoryPath = "";
                    }

                    listbox.Items.Add(new ServerManagerListBoxItem
                    {
                        ServerDisplayName = connection.Settings.DisplayName,
                        Connection = connection
                    });
                    connection.ResetRconConnection();
                    if (checkBoxIncludeDefaultFilteredPhrases.Checked && !alreadyHasFilteredPhrases) {
						foreach (string item in Translation.DefaultFilteredEnglishPhrases) {
                            if (!connection.Settings.AutoTranslateIgnoredPhrasesList.Contains(item)) {
                                connection.Settings.AutoTranslateIgnoredPhrasesList.Add(item);
							}
						}
                        connection.SaveSettings();
					}

                    connection.UpdateDisplay = true;

                }
                else
                {

                    //ServerSettings newServerSettings = new ServerSettings(
                    //    GetNextConnectionId(),
                    //    textBoxServerIP.Text,
                    //    textBoxServerPort.Text,
                    //    textBoxServerPassword.Text,
                    //    textBoxRconPassword.Text,
                    //    textBoxRconPort.Text,
                    //    serverLanguage,
                    //    textBoxName.Text,
                    //    onConnectCommands
                    //);

                    ServerSettings newServerSettings = new ServerSettings()
                    {
                        Id = GetNextConnectionId(),
                        Ip = textBoxServerIP.Text,
                        InfoPort = textBoxServerPort.Text,
                        RconPort = textBoxRconPort.Text,
                        ServerPassword = textBoxServerPassword.Text,
                        RconPassword = textBoxRconPassword.Text,
                        Name = textBoxName.Text,
                        ServerLanguage = serverLanguage,
                        SendOnConnectCommands = onConnectCommands,
                        UseLocalFiles = checkBoxUseLocalData.Checked,
                        UseServerHook = checkBoxUseServerHook.Checked
                    };

                    if (newServerSettings.UseLocalFiles) {

                        if (gameExecutableDirectoryInfo?.Exists ?? false) {
                            
                            newServerSettings.ServerExecutableDirectoryPath = 
                                gameExecutableDirectoryInfo.FullName;

                            // Add Map Variants folder if found
                            if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\maps")) {
                                newServerSettings.MapVariantsDirectoryPath =
                                    gameExecutableDirectoryInfo.FullName + "\\mods\\maps";
                            }
                            // Add Game Variants folder if found
                            if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\variants")) {
                                newServerSettings.GameVariantsDirectoryPath = 
                                    gameExecutableDirectoryInfo.FullName + "\\mods\\variants";
                            }
                            // Add Vote Files folder if found
                            if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\server")) {
                                newServerSettings.VoteFilesDirectoryPath =
                                    gameExecutableDirectoryInfo.FullName + "\\mods\\server";
                            }

                        }
                    }

                    Connection newConnection = new Connection(newServerSettings.Id, newServerSettings);

                    listbox.Items.Add(new ServerManagerListBoxItem
                    {
                        ServerDisplayName = newServerSettings.DisplayName,
                        Connection = newConnection
                    });

                    newConnection.Settings.AutoTranslateChatMessages = checkBoxEnableAutoTranslate.Checked;
                    if (checkBoxIncludeDefaultFilteredPhrases.Checked) {                        
                        foreach (string item in Translation.DefaultFilteredEnglishPhrases) {
                            if (!newConnection.Settings.AutoTranslateIgnoredPhrasesList.Contains(item)) {
                                newConnection.Settings.AutoTranslateIgnoredPhrasesList.Add(item);
                            }
                        }
                    }

                    newConnection.SaveSettings();

                }

                SaveSettings();
                Close();

            }
        }

		private void checkBoxUseLocalData_CheckedChanged(object sender, EventArgs e)
		{
            
            bool checkValue = ((CheckBox)sender).Checked;
            
            buttonBrowseGameExecutable.Enabled = checkValue;
            labelGameExecutable.Enabled = checkValue;
            textBoxGameExecutable.Enabled = checkValue;

            labelFound.Enabled = checkValue;
            labelMapsFolderFound.Enabled = checkValue;
            pictureBoxMapsFolderFoundIndicator.Enabled = checkValue;
            labelGamesFolderFound.Enabled = checkValue;
            pictureBoxGamesFolderFoundIndicator.Enabled = checkValue;
            labelVoteFilesFolderFound.Enabled = checkValue;
            pictureBoxVoteFilesFolderFoundIndicator.Enabled = checkValue;

            // Make sure the folder-found indicator labels use the correct 
            // enabled/disabled version of the XMark or CheckMark images
            PictureBoxUseCorrectEnabledDisabledIndicatorVariant(checkValue, pictureBoxMapsFolderFoundIndicator);
            PictureBoxUseCorrectEnabledDisabledIndicatorVariant(checkValue, pictureBoxGamesFolderFoundIndicator);
            PictureBoxUseCorrectEnabledDisabledIndicatorVariant(checkValue, pictureBoxVoteFilesFolderFoundIndicator);

        }

        private void buttonBrowseGameExecutable_Click(object sender, EventArgs e)
        {
			
            OpenFileDialog openFileDialog_GetGameExecutable = new OpenFileDialog {
				InitialDirectory = 
                    !string.IsNullOrWhiteSpace(connection?.Settings?.ServerExecutableDirectoryPath) 
                        ? connection.Settings.ServerExecutableDirectoryPath 
                        : Application.StartupPath,
				Title = "Select Game Executable (eldorado.exe)"
			};

			DialogResult result = openFileDialog_GetGameExecutable.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = openFileDialog_GetGameExecutable.FileName;
                if (!string.IsNullOrWhiteSpace(file)) {

                    try { 
                        gameExecutableDirectoryInfo = new DirectoryInfo(file).Parent;
                        if (!gameExecutableDirectoryInfo.Exists) { gameExecutableDirectoryInfo = null; }
                    }
                    catch (System.Security.SecurityException secEx) {
                        App.Error("Access Error", 
                            $"{App.InternalAppName} lacks sufficient permission to access the directory.\n\n" +
                            "To resolve this you could:\n"+
                                $"\t- Run {App.InternalAppName} with administrative privileges.\n"+
                                "\t- Change the permissions for the directory.\n\n" +
                            "Local files cannot be used until this is resolved.\n" +
                            "Try again after resolving the permissions conflict."
                        );
                        gameExecutableDirectoryInfo = null; 
                    }
                    catch (Exception ex) {
                        App.ErrorAppFailedTo("save information about the game directory");
                        gameExecutableDirectoryInfo = null; 
                    }
                    
                }
            }

            UpdateLocalFileUseInterfaceElements();

        }

        private void UpdateLocalFileUseInterfaceElements()
		{
            
            if (gameExecutableDirectoryInfo?.Exists ?? false) {

                textBoxGameExecutable.Text = gameExecutableDirectoryInfo.FullName;

                // Add Map Variants folder if found
                if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\maps")) {
                    pictureBoxMapsFolderFoundIndicator.Image = CheckMarkImage;
                } else { pictureBoxMapsFolderFoundIndicator.Image = XMarkImage; }
                // Add Game Variants folder if found
                if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\variants")) {
                    pictureBoxGamesFolderFoundIndicator.Image = CheckMarkImage;
                } else { pictureBoxGamesFolderFoundIndicator.Image = XMarkImage; }
                // Add Vote Files folder if found
                if (Directory.Exists(gameExecutableDirectoryInfo.FullName + "\\mods\\server")) {
                    pictureBoxVoteFilesFolderFoundIndicator.Image = CheckMarkImage;
                } else { pictureBoxVoteFilesFolderFoundIndicator.Image = XMarkImage; }

            }

            checkBoxUseLocalData_CheckedChanged(checkBoxUseLocalData, EventArgs.Empty);

        }

        private void PictureBoxUseCorrectEnabledDisabledIndicatorVariant(bool enabled, PictureBox pictureBox)
		{
            try {
                if (enabled) {
                    if (pictureBox.Image == CheckMarkDisabledImage) { pictureBox.Image = CheckMarkImage; }
                    else if (pictureBox.Image == XMarkDisabledImage) { pictureBox.Image = XMarkImage; }
                }
                else {
                    if (pictureBox.Image == CheckMarkImage) { pictureBox.Image = CheckMarkDisabledImage; }
                    else if (pictureBox.Image == XMarkImage) { pictureBox.Image = XMarkDisabledImage; }
                }
            }
            catch { }
		}

	}
}
