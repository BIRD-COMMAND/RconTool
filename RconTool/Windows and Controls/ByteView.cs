using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RconTool
{

	/// <summary>
	/// A simple byte viewer for debugging raw memory edits
	/// </summary>
	public partial class ByteView : Form
	{

		public int ByteCount { get; set; }
		public IntPtr Address { get; set; }
		public ProcessMemory ProcessMemory { get; set; }
		public bool LiveView { get; set; } = true;

		private int trackBarMemoryStatesLastValue = 0;

		private System.Windows.Forms.Timer Timer_CheckMemoryStateTimelineSliderValue;
		private Label hoveredLabel;

		public List<DateTime> ReadTimes { get; set; } = new List<DateTime>();
		public List<byte[]> MemoryStates { get; set; } = new List<byte[]>();

		public Label[] offsetLabels = new Label[16];
		public Label[,] byteLabels = new Label[16, 16];

		public ByteView(ProcessMemory processMemory, IntPtr address, int byteCount )
		{
			
			if (processMemory == null) { throw new ArgumentNullException("processMemory"); }
			if (address == null) { throw new ArgumentNullException("address"); }
			if (byteCount < 1 || byteCount > 255) { throw new ArgumentOutOfRangeException("byteCount"); }

			ProcessMemory = processMemory;
			Address = address;
			ByteCount = byteCount;

			InitializeComponent();

			// Create/Add Offset Labels
			for (int i = 0; i < 16; i++) {
				offsetLabels[i] = new Label() { Text = "00000000", Font = tableByteLabels.Font, TextAlign = ContentAlignment.MiddleCenter };
				tableByteLabels.Controls.Add(offsetLabels[i], 0, i);
			}

			// Create/Add Byte Labels
			for (int r = 0; r < 16; r++) {
				for (int c = 0; c < 16; c++) {				
					byteLabels[c, r] = new Label() { Text = "00", Font = tableByteLabels.Font, TextAlign = ContentAlignment.MiddleCenter };
					tableByteLabels.Controls.Add(byteLabels[c, r], (c + 1), r);
					byteLabels[c, r].MouseHover += SetHoveredLabel;
					byteLabels[c, r].MouseEnter += SetHoveredLabel;
				}
			}

			// Set up slider
			trackBarMemoryStates.TickFrequency = 1;

			// Initial Display Call
			DisplayBytes(Address, ByteCount);

			Timer_CheckMemoryStateTimelineSliderValue = new System.Windows.Forms.Timer() { Interval = 50 };
			Timer_CheckMemoryStateTimelineSliderValue.Tick += CheckSliderValue;
			Timer_CheckMemoryStateTimelineSliderValue.Start();

		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) { return; }
			try {
				base.OnFormClosing(e);
				Timer_CheckMemoryStateTimelineSliderValue.Stop();
				Timer_CheckMemoryStateTimelineSliderValue.Dispose();
			}
			catch { return; }
		}

		public void DisplayBytes(IntPtr address, int byteCount)
		{

			if (address == null) { throw new ArgumentNullException("address"); }
			if (byteCount < 1 || byteCount > 255) { throw new ArgumentOutOfRangeException("byteCount"); }

			// Update Parameters
			Address = address;
			ByteCount = byteCount;

			// Reset Memory States Slider
			trackBarMemoryStates.Value = 0;
			trackBarMemoryStates.Minimum = 0;
			trackBarMemoryStates.Maximum = 0;
			trackBarMemoryStates.Enabled = false;
			trackBarMemoryStatesLastValue = trackBarMemoryStates.Value;

			// Update offset labels
			for (int i = 0; i < 16; i++) {
				offsetLabels[i].Text = (address.ToInt32() + (i * 16)).ToString("X");
			}

			// Reset tags
			for (int r = 0; r < 16; r++) {
				for (int c = 0; c < 16; c++) {				
					byteLabels[c, r].Tag = 0;
				}
			}

			// Setup Memory States
			MemoryStates.Clear();
			ReadTimes.Clear();
			LiveView = true;

			// Reset HoveredLabel
			if (hoveredLabel != null) { hoveredLabel.BackColor = SystemColors.Control; }
			labelHoverInfo.Text = "XX [0x00000000]";

			// Display
			UpdateBytes();

		}

		public void RefreshBytes() { UpdateBytes(); }
		public void UpdateBytes(bool skipRead = false)
		{


			if (skipRead == false) {
				try { 
					MemoryStates.Add(ProcessMemory.Read((int)Address, ByteCount));
					ReadTimes.Add(DateTime.Now);
				}
				catch { throw; }
			}

			if (trackBarMemoryStates.Maximum != MemoryStates.Count - 1) { trackBarMemoryStates.Maximum = MemoryStates.Count - 1; }
			if (!trackBarMemoryStates.Enabled) { trackBarMemoryStates.Enabled = true; }

			if (LiveView) { 
				trackBarMemoryStates.Value = trackBarMemoryStates.Maximum;
				trackBarMemoryStatesLastValue = trackBarMemoryStates.Value;
			}

			// Update info label with base address and number of bytes read
			labelMemoryStateInfo.Text = $"[{trackBarMemoryStates.Value}]{ReadTimes[trackBarMemoryStates.Value].ToString("mm:ss:fff")} - 0x{((int)Address).ToString("X8")} [{ByteCount}]";

			int b = 0; string byteString; byte currentByte, originalByte;
			for (int r = 0; r < 16; r++) {
				for (int c = 0; c < 16; c++, b++) {
					if (b < ByteCount) {

						originalByte = MemoryStates[0][b];
						currentByte = MemoryStates[trackBarMemoryStates.Value][b];


						byteString = currentByte.ToString("X2");
						
						toolTip.SetToolTip(byteLabels[c, r], GenerateTooltip(b));

						// Byte same
						if (currentByte == originalByte) {
							if ((int)byteLabels[c, r].Tag == 1) {
								// Bytes that have changed but now match their original value are green
								byteLabels[c, r].ForeColor = Color.DarkGreen;
							}
							else {
								// Bytes that have maintained their original value stay black
								byteLabels[c, r].ForeColor = SystemColors.ControlText;
							}
						}
						// Byte changed
						else {							
							byteLabels[c, r].ForeColor = Color.Red;
							// Denotes label value has changed at some point
							byteLabels[c, r].Tag = 1; 
						}

						byteLabels[c, r].Text = byteString;

					}
					else {
						toolTip.SetToolTip(byteLabels[c, r], "");
						byteLabels[c, r].ForeColor = SystemColors.ControlText;
						byteLabels[c, r].Text = "XX"; 
					}
				}
			}

		}

		private StringBuilder tooltipSB = new StringBuilder();
		/// <summary>
		/// Generate a tooltip integrating all the information from the available memory states.
		/// </summary>
		private string GenerateTooltip(int byteIndex)
		{
			// Exceptions
			if ((MemoryStates?.Count ?? 0) == 0) { 
				throw new Exception("ByteView.GenerateTooltip Failed: No Recorded Memory States"); 
			}
			if (byteIndex >= ByteCount || byteIndex < 0) { 
				throw new ArgumentException("GenerateTooltip Failed: Invalid byteIndex."); 
			}

			tooltipSB.Clear();

			for (int i = 0; i < MemoryStates.Count; i++) {
				if (i > 0) { tooltipSB.Append("->"); }
				if (trackBarMemoryStates.Value == i) {
					tooltipSB.Append($"[{MemoryStates[i][byteIndex].ToString("X2")}]");
				}
				else { tooltipSB.Append(MemoryStates[i][byteIndex].ToString("X2"));}
			}

			return tooltipSB.ToString();

		}

		private void SetHoveredLabel(object sender, EventArgs e)
		{
			if (hoveredLabel == (Label)sender) { return; }
			else if (hoveredLabel != null) { hoveredLabel.BackColor = SystemColors.Control; }
			hoveredLabel = (Label)sender;
			hoveredLabel.BackColor = SystemColors.GradientActiveCaption;
			UpdateHoverInfoLabel(tableByteLabels.GetCellPosition(hoveredLabel), hoveredLabel);
		}
		private void UpdateHoverInfoLabel(TableLayoutPanelCellPosition cell, Label label)
		{
			labelHoverInfo.Text = $"{label.Text} [0x{(Address + (cell.Row * 16) + (cell.Column - 1)).ToString("X8")}]";
		}


		private void buttonRead_Click(object sender, EventArgs e) { UpdateBytes(); }

		private void CheckSliderValue(object sender, EventArgs e)
		{
			//if (trackBarMemoryStates.Capture) { return; }
			if (trackBarMemoryStates.Value != trackBarMemoryStatesLastValue) {
				if (trackBarMemoryStates.Capture) { LiveView = false; }
				else { LiveView = (trackBarMemoryStates.Value == trackBarMemoryStates.Maximum); }
				trackBarMemoryStatesLastValue = trackBarMemoryStates.Value;
				UpdateBytes(true);
				LiveView = (trackBarMemoryStates.Value == trackBarMemoryStates.Maximum);
			}
		}

		//UTF Interpretation
		//System.Text.Encoding.UTF8.GetString(bytes)

	}

}
