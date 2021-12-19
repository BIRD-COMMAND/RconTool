namespace RconTool
{
	partial class FileStreamDebugHelper
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileStreamDebugHelper));
			this.textBoxFilePath = new System.Windows.Forms.TextBox();
			this.labelFileToRead = new System.Windows.Forms.Label();
			this.buttonBrowseFile = new System.Windows.Forms.Button();
			this.labelPositions = new System.Windows.Forms.Label();
			this.buttonRead = new System.Windows.Forms.Button();
			this.listBoxRead = new System.Windows.Forms.ListBox();
			this.labelOffset = new System.Windows.Forms.Label();
			this.buttonAutoRead = new System.Windows.Forms.Button();
			this.tableByteLabels = new System.Windows.Forms.TableLayoutPanel();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// textBoxFilePath
			// 
			this.textBoxFilePath.Location = new System.Drawing.Point(139, 18);
			this.textBoxFilePath.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxFilePath.Name = "textBoxFilePath";
			this.textBoxFilePath.Size = new System.Drawing.Size(326, 26);
			this.textBoxFilePath.TabIndex = 0;
			this.textBoxFilePath.Text = "C:\\Users\\USERNAME\\Games\\Halo Online\\mods\\variants\\LPCVIP\\variant.vip";
			// 
			// labelFileToRead
			// 
			this.labelFileToRead.AutoSize = true;
			this.labelFileToRead.Location = new System.Drawing.Point(13, 21);
			this.labelFileToRead.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelFileToRead.Name = "labelFileToRead";
			this.labelFileToRead.Size = new System.Drawing.Size(117, 19);
			this.labelFileToRead.TabIndex = 1;
			this.labelFileToRead.Text = "File To Read";
			// 
			// buttonBrowseFile
			// 
			this.buttonBrowseFile.Location = new System.Drawing.Point(473, 13);
			this.buttonBrowseFile.Margin = new System.Windows.Forms.Padding(4);
			this.buttonBrowseFile.Name = "buttonBrowseFile";
			this.buttonBrowseFile.Size = new System.Drawing.Size(112, 34);
			this.buttonBrowseFile.TabIndex = 2;
			this.buttonBrowseFile.Text = "Browse";
			this.buttonBrowseFile.UseVisualStyleBackColor = true;
			this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
			// 
			// labelPositions
			// 
			this.labelPositions.AutoSize = true;
			this.labelPositions.Location = new System.Drawing.Point(21, 81);
			this.labelPositions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelPositions.Name = "labelPositions";
			this.labelPositions.Size = new System.Drawing.Size(522, 19);
			this.labelPositions.TabIndex = 6;
			this.labelPositions.Text = "OFFSET    00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F";
			// 
			// buttonRead
			// 
			this.buttonRead.AutoSize = true;
			this.buttonRead.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonRead.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonRead.Location = new System.Drawing.Point(105, 52);
			this.buttonRead.Margin = new System.Windows.Forms.Padding(4);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(45, 25);
			this.buttonRead.TabIndex = 2;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// listBoxRead
			// 
			this.listBoxRead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxRead.Enabled = false;
			this.listBoxRead.FormattingEnabled = true;
			this.listBoxRead.ItemHeight = 19;
			this.listBoxRead.Location = new System.Drawing.Point(12, 103);
			this.listBoxRead.Name = "listBoxRead";
			this.listBoxRead.Size = new System.Drawing.Size(573, 422);
			this.listBoxRead.TabIndex = 7;
			this.listBoxRead.Visible = false;
			// 
			// labelOffset
			// 
			this.labelOffset.AutoSize = true;
			this.labelOffset.Location = new System.Drawing.Point(157, 54);
			this.labelOffset.Name = "labelOffset";
			this.labelOffset.Size = new System.Drawing.Size(162, 19);
			this.labelOffset.TabIndex = 8;
			this.labelOffset.Text = "Offset (code) : 0";
			this.labelOffset.Visible = false;
			// 
			// buttonAutoRead
			// 
			this.buttonAutoRead.AutoSize = true;
			this.buttonAutoRead.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonAutoRead.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonAutoRead.Location = new System.Drawing.Point(17, 52);
			this.buttonAutoRead.Margin = new System.Windows.Forms.Padding(4);
			this.buttonAutoRead.Name = "buttonAutoRead";
			this.buttonAutoRead.Size = new System.Drawing.Size(80, 25);
			this.buttonAutoRead.TabIndex = 2;
			this.buttonAutoRead.Text = "Auto-Read";
			this.buttonAutoRead.UseVisualStyleBackColor = true;
			this.buttonAutoRead.Click += new System.EventHandler(this.buttonAutoRead_Click);
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
			this.tableByteLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tableByteLabels.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableByteLabels.Location = new System.Drawing.Point(12, 103);
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
			this.tableByteLabels.TabIndex = 10;
			// 
			// FileStreamDebugHelper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 545);
			this.Controls.Add(this.tableByteLabels);
			this.Controls.Add(this.labelOffset);
			this.Controls.Add(this.listBoxRead);
			this.Controls.Add(this.labelPositions);
			this.Controls.Add(this.buttonAutoRead);
			this.Controls.Add(this.buttonRead);
			this.Controls.Add(this.buttonBrowseFile);
			this.Controls.Add(this.labelFileToRead);
			this.Controls.Add(this.textBoxFilePath);
			this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FileStreamDebugHelper";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FileStream Debug Helper";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxFilePath;
		private System.Windows.Forms.Label labelFileToRead;
		private System.Windows.Forms.Button buttonBrowseFile;
		private System.Windows.Forms.Label labelPositions;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.ListBox listBoxRead;
		private System.Windows.Forms.Label labelOffset;
		private System.Windows.Forms.Button buttonAutoRead;
		private System.Windows.Forms.TableLayoutPanel tableByteLabels;
		private System.Windows.Forms.ToolTip toolTip;
	}
}