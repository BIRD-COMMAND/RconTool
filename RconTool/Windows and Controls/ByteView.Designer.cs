namespace RconTool
{
	partial class ByteView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ByteView));
			this.labelPositions = new System.Windows.Forms.Label();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.tableByteLabels = new System.Windows.Forms.TableLayoutPanel();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.labelMemoryStateInfo = new System.Windows.Forms.Label();
			this.trackBarMemoryStates = new System.Windows.Forms.TrackBar();
			this.labelHoverInfo = new System.Windows.Forms.Label();
			this.panelLabelHoverInfo = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.trackBarMemoryStates)).BeginInit();
			this.panelLabelHoverInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelPositions
			// 
			this.labelPositions.AutoSize = true;
			this.labelPositions.Location = new System.Drawing.Point(22, 9);
			this.labelPositions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelPositions.Name = "labelPositions";
			this.labelPositions.Size = new System.Drawing.Size(522, 19);
			this.labelPositions.TabIndex = 0;
			this.labelPositions.Text = " OFFSET   00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F";
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.AutoSize = true;
			this.buttonRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonRefresh.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonRefresh.Location = new System.Drawing.Point(13, 502);
			this.buttonRefresh.Margin = new System.Windows.Forms.Padding(4);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(45, 25);
			this.buttonRefresh.TabIndex = 1;
			this.buttonRefresh.Text = "READ";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// tableByteLabels
			// 
			this.tableByteLabels.ColumnCount = 17;
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableByteLabels.Font = new System.Drawing.Font("Consolas", 8F);
			this.tableByteLabels.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableByteLabels.Location = new System.Drawing.Point(13, 31);
			this.tableByteLabels.Name = "tableByteLabels";
			this.tableByteLabels.RowCount = 16;
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableByteLabels.Size = new System.Drawing.Size(531, 433);
			this.tableByteLabels.TabIndex = 0;
			// 
			// labelMemoryStateInfo
			// 
			this.labelMemoryStateInfo.AutoSize = true;
			this.labelMemoryStateInfo.Location = new System.Drawing.Point(13, 476);
			this.labelMemoryStateInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelMemoryStateInfo.Name = "labelMemoryStateInfo";
			this.labelMemoryStateInfo.Size = new System.Drawing.Size(90, 19);
			this.labelMemoryStateInfo.TabIndex = 0;
			this.labelMemoryStateInfo.Text = "Read Info";
			// 
			// trackBarMemoryStates
			// 
			this.trackBarMemoryStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarMemoryStates.AutoSize = false;
			this.trackBarMemoryStates.LargeChange = 1;
			this.trackBarMemoryStates.Location = new System.Drawing.Point(64, 498);
			this.trackBarMemoryStates.Maximum = 0;
			this.trackBarMemoryStates.Name = "trackBarMemoryStates";
			this.trackBarMemoryStates.Size = new System.Drawing.Size(480, 32);
			this.trackBarMemoryStates.TabIndex = 2;
			this.trackBarMemoryStates.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			// 
			// labelHoverInfo
			// 
			this.labelHoverInfo.AutoSize = true;
			this.labelHoverInfo.Dock = System.Windows.Forms.DockStyle.Right;
			this.labelHoverInfo.Location = new System.Drawing.Point(46, 2);
			this.labelHoverInfo.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.labelHoverInfo.Name = "labelHoverInfo";
			this.labelHoverInfo.Size = new System.Drawing.Size(144, 19);
			this.labelHoverInfo.TabIndex = 0;
			this.labelHoverInfo.Text = "XX [0x00000000]";
			// 
			// panelLabelHoverInfo
			// 
			this.panelLabelHoverInfo.Controls.Add(this.labelHoverInfo);
			this.panelLabelHoverInfo.Location = new System.Drawing.Point(354, 476);
			this.panelLabelHoverInfo.Margin = new System.Windows.Forms.Padding(0);
			this.panelLabelHoverInfo.Name = "panelLabelHoverInfo";
			this.panelLabelHoverInfo.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.panelLabelHoverInfo.Size = new System.Drawing.Size(190, 25);
			this.panelLabelHoverInfo.TabIndex = 3;
			// 
			// ByteView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 533);
			this.Controls.Add(this.panelLabelHoverInfo);
			this.Controls.Add(this.trackBarMemoryStates);
			this.Controls.Add(this.tableByteLabels);
			this.Controls.Add(this.labelMemoryStateInfo);
			this.Controls.Add(this.labelPositions);
			this.Controls.Add(this.buttonRefresh);
			this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "ByteView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Byte View";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.trackBarMemoryStates)).EndInit();
			this.panelLabelHoverInfo.ResumeLayout(false);
			this.panelLabelHoverInfo.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label labelPositions;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.TableLayoutPanel tableByteLabels;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label labelMemoryStateInfo;
		private System.Windows.Forms.TrackBar trackBarMemoryStates;
		private System.Windows.Forms.Label labelHoverInfo;
		private System.Windows.Forms.Panel panelLabelHoverInfo;
	}
}