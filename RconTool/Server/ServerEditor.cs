using System;
using System.Collections.Generic;
using System.Net;
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

        private bool isEditing = false;


        // ServerEditor Window when adding a new server connection
        public ServerEditor(ListBox listbox)
        {
            this.listbox = listbox;
            InitializeComponent();
            string commandlist = "Server.SendChatToRconClients 1" + Environment.NewLine;
            textBoxCommandsToSendUponConnection.Text = commandlist;
        }

        // ServerEditor Window when editing a server connection
        public ServerEditor(Connection connections, ListBox lsbox)
        {
            listbox = lsbox;
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
            string commandlist = "";
            if (!(info.sendOnConnect == null || info.sendOnConnect.Count == 0))
            {
                foreach (string sc in info.sendOnConnect)
                {
                    commandlist = commandlist + sc + System.Environment.NewLine;
                }
            }
            textBoxCommandsToSendUponConnection.Text = commandlist;
        }

        private bool IsValidIPAddress(string input)
        {
            IPAddress address;
            if (IPAddress.TryParse(input, out address))
            {
                switch (address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        return true;

                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        return false;

                    default:
                        return false;

                }
            }
            return false;
        }

        private void buttonSaveServer_Click(object sender, EventArgs e)
        {
            var isPortValid = int.TryParse(textBoxServerPort.Text, out int n);

            if (textBoxServerIP.Text.Equals(""))
            {
                MessageBox.Show("Server IP cannot be blank!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (textBoxServerPort.Text.Equals(""))
            {
                MessageBox.Show("InfoServer Port cannot be blank!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (IsValidIPAddress(textBoxServerIP.Text) == false)
            {
                MessageBox.Show("Server IP must be valid!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (isPortValid == false)
            {
                MessageBox.Show("InfoServer Port must be valid!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {

                List<string> list = new List<string>(textBoxCommandsToSendUponConnection.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
                                
                if (isEditing)
                {

                    listbox.Items.Remove(listbox.SelectedItem);
                    connection.Settings.Ip = textBoxServerIP.Text;
                    connection.Settings.InfoPort = textBoxServerPort.Text;
                    connection.Settings.ServerPassword = textBoxServerPassword.Text;
                    connection.Settings.RconPassword = textBoxRconPassword.Text;
                    connection.Settings.RconPort = textBoxRconPort.Text;
                    connection.Settings.Name = textBoxName.Text;
                    connection.Settings.sendOnConnect = list;
                    listbox.Items.Add(new ServerManagerListBoxItem
                    {
                        Ip = connection.Settings.Ip + ":" + connection.Settings.InfoPort,
                        Connection = connection
                    });
                    connection.ResetRconConnection();

                }
                else
                {
                    
                    ServerSettings newServer = new ServerSettings(
                        textBoxServerIP.Text,
                        textBoxServerPort.Text, 
                        textBoxServerPassword.Text,
                        textBoxRconPassword.Text,
                        textBoxRconPort.Text, 
                        textBoxName.Text, list
                    );

                    Connection cm = new Connection(newServer);

                    listbox.Items.Add(new ServerManagerListBoxItem
                    {
                        Ip = newServer.Ip + ":" + newServer.InfoPort,
                        Connection = cm
                    });

                }

                App.SaveSettings();
                Close();

            }
        }
    
    }
}
