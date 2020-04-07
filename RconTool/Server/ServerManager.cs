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

            comboBoxWindowTitle.SelectedIndex = comboBoxWindowTitle.Items.IndexOf(App.titleOption == "" ? "None" : App.titleOption);
            listBoxServerList.DisplayMember = "Ip";
            listBoxServerList.ValueMember = "connection";

            textBoxWebHookURL.Text = App.webhook;
            textBoxReportCommand.Text = App.webhookTrigger;
            textBoxRoleID.Text = App.webhookRole;

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
                    Ip = App.connectionList[x].Settings.Ip + ":" + App.connectionList[x].Settings.InfoPort,
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

                    
                    ((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection.DeleteConnection();
                    connectionList.Remove(((ServerManagerListBoxItem)listBoxServerList.SelectedItem).Connection);
                    listBoxServerList.Items.Remove(listBoxServerList.SelectedItem);

                    SaveSettings();

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
                App.webhook = textBoxWebHookURL.Text;
                App.webhookTrigger = textBoxReportCommand.Text;
                App.webhookRole = textBoxRoleID.Text;
                App.titleOption = comboBoxWindowTitle.SelectedItem.ToString();
                this.Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.SaveSettings();
        }

        public class ServerManagerListBoxItem
        {
            public string Ip { get; set; }
            public Connection Connection { get; set; }
        }


    }
}
