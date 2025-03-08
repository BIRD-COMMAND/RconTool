using System;
using System.Windows.Forms;
using static RconTool.App;

namespace RconTool
{
    public partial class ServerManager : Form
    {
        
        public ServerManager()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            InitializeComponent();

            comboBoxWindowTitle.SelectedIndex = 0;/*comboBoxWindowTitle.Items.IndexOf(App.titleOption == "" ? "None" : App.titleOption);*/
            listBoxServerList.DisplayMember = "ServerDisplayName";
            listBoxServerList.ValueMember = "connection";

            AddItems();

            listBoxServerList.SelectedIndex = listBoxServerList.Items.Count - 1;
        }

        public void AddItems()
        {
            listBoxServerList.Items.Clear();

            for (int x = 0; x < App.connectionList.Count; x++)
            {
                listBoxServerList.Items.Add(new ServerManagerListBoxItem
                {
                    //if (App.connectionList[x].Settings.Na)
                    ServerDisplayName = App.connectionList[x].Settings.DisplayName,
                    Connection = App.connectionList[x]
                });
            }
        }
        
        private void ButtonAddNewServer_Click(object sender, EventArgs e)
        {
            new ServerEditor(listBoxServerList).ShowDialog();
        }

        private void ButtonEditServer_Click(object sender, EventArgs e)
        {
            if (listBoxServerList.SelectedItem != null && ((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection != null)
            {
                new ServerEditor(((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection, listBoxServerList).ShowDialog();
                //this.Close();
            }
        }

        private void ButtonDeleteServer_Click(object sender, EventArgs e)
        {

            if (listBoxServerList.SelectedItem != null && ((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection != null)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete the server?", "Warning", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {

                    ServerSettings settings = ((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection?.Settings;
                    if (settings != null)
                    {
                        string deletionMessage =
                        "The following server connection is being deleted:\n" +
                        "┌\n" +
                        "│ Name: " + settings.Name ?? "(Null)" + "\n" +
                        "│ IP: " + settings.Ip ?? "(Null)" + "\n" +
                        "│ Info Port: " + settings.InfoPort ?? "(Null)" + "\n" +
                        "│ Rcon Port: " + settings.RconPort ?? "(Null)" + "\n" +
                        "└\n" +
                        "This server connection has been deleted.";
                        Log(deletionMessage);
                    }

                    App.DeleteServer(((ServerManagerListBoxItem)listBoxServerList.SelectedItem ).Connection);

					//((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection.Close();
					//connectionList.Remove(((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection);
					//listBoxServerList.Items.Remove(listBoxServerList.SelectedItem);
					//foreach (Connection connection in connectionList) { connection.UpdateDisplay = true; }
					//SaveSettings();

				}
            }

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (App.connectionList.Count == 0)
            {
                MessageBox.Show("Must have at least 1 server!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Connection selectedConnection = ((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection;
                selectedConnection.Settings.Webhook = textBoxWebHookURL.Text ?? "";
                selectedConnection.Settings.WebhookTrigger = textBoxReportCommand.Text ?? "";
                selectedConnection.Settings.WebhookRole = textBoxRoleID.Text ?? "";
                if (comboBoxWindowTitle.SelectedIndex == 0) { selectedConnection.Settings.TitleDisplayOption = ServerSettings.TitleOption.None; }
                else if (comboBoxWindowTitle.SelectedIndex == 1) { selectedConnection.Settings.TitleDisplayOption = ServerSettings.TitleOption.Name; }
                else if (comboBoxWindowTitle.SelectedIndex == 2) { selectedConnection.Settings.TitleDisplayOption = ServerSettings.TitleOption.Game; }
                else if (comboBoxWindowTitle.SelectedIndex == 3) { selectedConnection.Settings.TitleDisplayOption = ServerSettings.TitleOption.Ip; }
                try { selectedConnection.SaveSettings(); }
                catch { throw new Exception("Error saving server settings to the local server database."); }
                Close();
            }
        }

        private void PopulateWebIntegrationFields()
		{
            if (listBoxServerList.SelectedIndex == -1)
            {
                textBoxWebHookURL.Text = "";
                textBoxReportCommand.Text = "";
                textBoxRoleID.Text = "";
                comboBoxWindowTitle.SelectedIndex = 0;
            }
            else
            {
                Connection selectedConnection = ((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection;
                textBoxWebHookURL.Text = selectedConnection.Settings.Webhook ?? "";
                textBoxReportCommand.Text = selectedConnection.Settings.WebhookTrigger ?? "";
                textBoxRoleID.Text = selectedConnection.Settings.WebhookRole ?? "";
                switch (selectedConnection.Settings.TitleDisplayOption)
                {
                    case ServerSettings.TitleOption.Name: comboBoxWindowTitle.SelectedIndex = 1; break;
                    case ServerSettings.TitleOption.Game: comboBoxWindowTitle.SelectedIndex = 2; break;
                    case ServerSettings.TitleOption.Ip: comboBoxWindowTitle.SelectedIndex = 3; break;
                    case ServerSettings.TitleOption.None: comboBoxWindowTitle.SelectedIndex = 0; break;
                    default: comboBoxWindowTitle.SelectedIndex = 0; break;
                }
            }
		}

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.SaveSettings();
        }

        public class ServerManagerListBoxItem
        {
            public string ServerDisplayName { get; set; }
            public Connection Connection { get; set; }
        }

		private void listBoxServerList_SelectedIndexChanged(object sender, EventArgs e)
		{
            PopulateWebIntegrationFields();
		}
	}
}
