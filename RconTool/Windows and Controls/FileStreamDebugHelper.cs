﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RconTool
{

	/// <summary>
	/// A pseudo-hex editor I used to help me visualize operations for reading game and map variant files.
	/// </summary>
	public partial class FileStreamDebugHelper : Form
	{

		public OpenFileDialog openFileDialog;
		public string filePath;
		public bool readStarted = false;
		public bool autoRead = false;
		public Thread autoReadThread;
		public int offsetA = 0;
		public int offsetB = 1;
		public int position = 0;

		public FileStream fs;

		public List<string> offsets = new List<string>()
		{
			"00000000",
			"00000016",
			"00000032",
			"00000048",
			"00000064",
			"00000080",
			"00000096",
			"00000112",
		};
		
		public FileStreamDebugHelper()
		{

			InitializeComponent();
			openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = "C:\\";
			openFileDialog.Filter = "All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;
			filePath = textBoxFilePath.Text;

			// Add some offsets
			int offset = 112;
			for (int i = 0; i < 100; i++)
			{
				offset += 16;
				if (offset < 1000)
				{
					offsets.Add("00000" + offset.ToString());
				}
				else if(offset < 10000)
				{
					offsets.Add("0000" + offset.ToString());
				}
				else if (offset < 100000)
				{
					offsets.Add("000" + offset.ToString());
				}
				else if (offset < 1000000)
				{
					offsets.Add("00" + offset.ToString());
				}
			}

		}

		private void buttonBrowseFile_Click(object sender, EventArgs e)
		{
			DialogResult result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				filePath = openFileDialog.FileName;
				textBoxFilePath.Text = filePath;
			}
		}

		delegate string GetListBoxItemsCallback(ListBox listBox, int index);
		public string GetListBoxItem(ListBox listBox, int index)
		{
			if (listBox.InvokeRequired)
			{
				GetListBoxItemsCallback de = new GetListBoxItemsCallback(DoGetListBoxItem);
				return (string)listBox.Invoke(de, new object[] { listBox, index });
			}
			else
			{
				return DoGetListBoxItem(listBox, index);
			}
		}
		private string DoGetListBoxItem(ListBox listBox, int index)
		{
			return listBox.Items[index].ToString();
		}

		delegate void SetListBoxItemsCallback(ListBox listBox, int index, string item);
		public void SetListBoxItem(ListBox listBox, int index, string item)
		{
			if (listBox.InvokeRequired)
			{
				SetListBoxItemsCallback de = new SetListBoxItemsCallback(DoSetListBoxItem);
				listBox.Invoke(de, new object[] { listBox, index, item });
			}
			else
			{
				DoSetListBoxItem(listBox, index, item);
			}
		}
		private void DoSetListBoxItem(ListBox listBox, int index, string item)
		{
			listBox.Items[index] = item;
		}

		delegate void AddListBoxItemCallback(ListBox listBox, string item);
		public void AddListBoxItem(ListBox listBox, string item)
		{
			if (listBox.InvokeRequired)
			{
				AddListBoxItemCallback de = new AddListBoxItemCallback(DoAddListBoxItem);
				listBox.Invoke(de, new object[] { listBox, item });
			}
			else
			{
				DoAddListBoxItem(listBox, item);
			}
		}
		private void DoAddListBoxItem(ListBox listBox, string item)
		{
			listBox.Items.Add(item);
		}

		delegate int GetListBoxItemCountCallback(ListBox listBox);
		public int GetListBoxItemCount(ListBox listBox)
		{
			if (listBox.InvokeRequired)
			{
				GetListBoxItemCountCallback de = new GetListBoxItemCountCallback(DoGetListBoxItemCount);
				return (int)listBox.Invoke(de, new object[] { listBox});
			}
			else
			{
				return DoGetListBoxItemCount(listBox);
			}
		}
		private int DoGetListBoxItemCount(ListBox listBox)
		{
			return listBox.Items.Count;
		}

		private void ReadNextByte()
		{
			int listBoxItemCount = GetListBoxItemCount(listBoxRead);
			if ((listBoxItemCount / 2) <= (position / 16))
			{
				AddListBoxItem(listBoxRead, (offsets[position / 16] + "   "));
				AddListBoxItem(listBoxRead, (offsets[position / 16] + "   "));
			}

			fs = new FileStream(filePath, FileMode.Open);
			for (int i = 0; i < position; i++)
			{
				fs.ReadByte();
			}

			byte thisByte = (byte)fs.ReadByte();
			byte[] data = { thisByte };

			fs.Close();
			fs.Dispose();

			#region Add Hex Interpretation

			//string s = listBoxRead.Items[offsetA].ToString();
			string s = GetListBoxItem(listBoxRead, offsetA);			
			s += BitConverter.ToString(data) + " ";

			//listBoxRead.Items[offsetA] = s;
			SetListBoxItem(listBoxRead, offsetA, s);

			#endregion

			#region Add UTF Interpretation

			//s = listBoxRead.Items[offsetB].ToString();
			s = GetListBoxItem(listBoxRead, offsetB);
			if (thisByte == 0) { s += " . "; }
			else { s += " " + System.Text.Encoding.UTF8.GetString(data) + " "; }

			//listBoxRead.Items[offsetB] = s;
			SetListBoxItem(listBoxRead, offsetB, s);

			#endregion

			position++;
			offsetA = (position / 16) * 2;
			offsetB = offsetA + 1;
			//labelOffset.Text = "Offset (code) : " + offsetA;

			listBoxRead.Invoke(new Action( () => {
				listBoxRead.SelectedIndex = listBoxRead.Items.Count - 1;
				listBoxRead.SelectedIndex = -1;
				listBoxRead.Invalidate();
			}));

		}

		private void buttonRead_Click(object sender, EventArgs e)
		{
			if (autoRead || autoReadThread != null) { return; }
			ReadNextByte();
		}

		private void buttonAutoRead_Click(object sender, EventArgs e)
		{
			if (autoRead)
			{
				autoRead = false;
				autoReadThread.Abort();
				autoReadThread = null;
			}
			else
			{
				if (autoReadThread != null) { return; }
				autoRead = true;
				autoReadThread = new Thread(new ThreadStart(() => {
					while (autoRead)
					{
						Thread.Sleep(50);
						if (autoRead)
						{
							ReadNextByte();
						}
					}
				}));
				autoReadThread.Start();
			}
		}
	}

}
