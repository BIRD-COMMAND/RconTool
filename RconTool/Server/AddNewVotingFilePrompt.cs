using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RconTool
{

	public partial class AddNewVotingFilePrompt : Form
	{

		Regex r = new Regex("^[a-zA-Z0-9]*$");
		public static string name = null;
		public static List<string> takenNames = new List<string>();

		public AddNewVotingFilePrompt()
		{
			InitializeComponent();
			name = null;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			buttonSubmit.Enabled = r.IsMatch(textBoxName.Text);
		}

		private void SubmitName(object sender, EventArgs e)
		{
			string name = textBoxName.Text;
			if (string.IsNullOrEmpty(name) || !r.IsMatch(name)) { return; }
			else
			{
				if (takenNames.Contains(name + ".json"))
				{
					MessageBox.Show(
						"That name is already in use, please choose a filename that is not in use in the voting files directory.",
						"Alert: File Name Unavailable",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
					);
					return;
				}
				else
				{
					AddNewVotingFilePrompt.name = name;
					Close();
				}				
			}
		}

		private void buttonSubmit_Click(object sender, EventArgs e)
		{
			SubmitName(null, null);
		}

		private void textBoxName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SubmitName(null, null);
			}
		}
	}

}
