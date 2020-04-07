using System;
using System.Windows.Forms;
using static RconTool.App;

namespace RconTool
{
    /// <summary>
    /// Form for adding, and removing Timed Command Sets, as well as launching the editor for editing an individual Timed Command Set.
    /// </summary>
    public partial class ToolCommandManager : Form
    {

        public ToolCommandManager()
        {

            if (currentConnection == null)
            {
                MessageBox.Show(
                    "Must have at least one server connection configured to add conditional commands.", 
                    "Server Configuration Required", 
                    MessageBoxButtons.OK
                );
                InitializeComponent();
                Close();
                return;
            }

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            InitializeComponent();

            listBoxCommands.DisplayMember = "Command";
            listBoxCommands.ValueMember = "cmd";

            foreach (ToolCommand command in GlobalToolCommands)
            {
                listBoxCommands.Items.Add(
                    new Tuple<string, ToolCommand>("Global: " + command.Name, command)
                );
            }

            if (currentConnection != null && currentConnection.Settings.Commands.Count > 0)
            {
                foreach (ToolCommand command in currentConnection.Settings.Commands)
                {
                    listBoxCommands.Items.Add(
                        new Tuple<string, ToolCommand>(command.Name, command)
                    );
                }
            }

            listBoxCommands.SelectedIndex = listBoxCommands.Items.Count - 1;
        }

        private void buttonDeleteConditionalCommand_Click(object sender, EventArgs e)
        {
            if (listBoxCommands.SelectedItem != null && ((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2 != null)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete the command?", "Warning", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GlobalToolCommands.Contains(((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2))
                    {
                        GlobalToolCommands.Remove(((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2);
                    }
                    else if (currentConnection != null && currentConnection.Settings.Commands.Contains(((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2))
                    {
                        ((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2.Disable();
                        currentConnection.Settings.Commands.Remove(((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2);
                    }
                        listBoxCommands.Items.Remove(listBoxCommands.SelectedItem);
                }
            }
            SaveSettings();
        }

        private void buttonEditConditionalCommand_Click(object sender, EventArgs e)
        {
            if (listBoxCommands.SelectedItem != null && ((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2 != null)
            {
                new ToolCommandEditor(
                    listBoxCommands,
                    ((Tuple<string, ToolCommand>)listBoxCommands.SelectedItem).Item2                    
                ).ShowDialog();
            }
        }

        private void buttonAddConditionalCommand_Click(object sender, EventArgs e)
        {
            new ToolCommandEditor(listBoxCommands, null).ShowDialog();
        }
    }
}
