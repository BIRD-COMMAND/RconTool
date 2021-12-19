
namespace RconTool
{
	partial class SetTranslationUsageTrackingData
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
			if (disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetTranslationUsageTrackingData));
			this.numericUpDownTranslatedCharacterCount = new System.Windows.Forms.NumericUpDown();
			this.labelTranslatedCharacterCount = new System.Windows.Forms.Label();
			this.dateTimePickerBillingCycleStartDate = new System.Windows.Forms.DateTimePicker();
			this.labelBillingCycleStartDate = new System.Windows.Forms.Label();
			this.buttonOkay = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTranslatedCharacterCount)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDownTranslatedCharacterCount
			// 
			this.numericUpDownTranslatedCharacterCount.Location = new System.Drawing.Point(165, 8);
			this.numericUpDownTranslatedCharacterCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.numericUpDownTranslatedCharacterCount.Name = "numericUpDownTranslatedCharacterCount";
			this.numericUpDownTranslatedCharacterCount.Size = new System.Drawing.Size(83, 23);
			this.numericUpDownTranslatedCharacterCount.TabIndex = 0;
			this.numericUpDownTranslatedCharacterCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDownTranslatedCharacterCount.ThousandsSeparator = true;
			// 
			// labelTranslatedCharacterCount
			// 
			this.labelTranslatedCharacterCount.AutoSize = true;
			this.labelTranslatedCharacterCount.Location = new System.Drawing.Point(9, 10);
			this.labelTranslatedCharacterCount.Name = "labelTranslatedCharacterCount";
			this.labelTranslatedCharacterCount.Size = new System.Drawing.Size(150, 15);
			this.labelTranslatedCharacterCount.TabIndex = 1;
			this.labelTranslatedCharacterCount.Text = "Translated Character Count";
			// 
			// dateTimePickerBillingCycleStartDate
			// 
			this.dateTimePickerBillingCycleStartDate.Location = new System.Drawing.Point(12, 52);
			this.dateTimePickerBillingCycleStartDate.MinDate = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
			this.dateTimePickerBillingCycleStartDate.Name = "dateTimePickerBillingCycleStartDate";
			this.dateTimePickerBillingCycleStartDate.Size = new System.Drawing.Size(236, 23);
			this.dateTimePickerBillingCycleStartDate.TabIndex = 2;
			this.dateTimePickerBillingCycleStartDate.Value = new System.DateTime(2021, 1, 1, 18, 0, 0, 0);
			// 
			// labelBillingCycleStartDate
			// 
			this.labelBillingCycleStartDate.AutoSize = true;
			this.labelBillingCycleStartDate.Location = new System.Drawing.Point(9, 34);
			this.labelBillingCycleStartDate.Name = "labelBillingCycleStartDate";
			this.labelBillingCycleStartDate.Size = new System.Drawing.Size(126, 15);
			this.labelBillingCycleStartDate.TabIndex = 3;
			this.labelBillingCycleStartDate.Text = "Billing Cycle Start Date";
			// 
			// buttonOkay
			// 
			this.buttonOkay.Location = new System.Drawing.Point(183, 81);
			this.buttonOkay.Name = "buttonOkay";
			this.buttonOkay.Size = new System.Drawing.Size(65, 23);
			this.buttonOkay.TabIndex = 4;
			this.buttonOkay.Text = "Submit";
			this.buttonOkay.UseVisualStyleBackColor = true;
			this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
			// 
			// SetTranslationUsageTrackingData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(260, 111);
			this.Controls.Add(this.buttonOkay);
			this.Controls.Add(this.labelTranslatedCharacterCount);
			this.Controls.Add(this.numericUpDownTranslatedCharacterCount);
			this.Controls.Add(this.labelBillingCycleStartDate);
			this.Controls.Add(this.dateTimePickerBillingCycleStartDate);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SetTranslationUsageTrackingData";
			this.Text = "Set Character Count and Billing Cycle";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTranslatedCharacterCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDownTranslatedCharacterCount;
		private System.Windows.Forms.Label labelTranslatedCharacterCount;
		private System.Windows.Forms.DateTimePicker dateTimePickerBillingCycleStartDate;
		private System.Windows.Forms.Label labelBillingCycleStartDate;
		private System.Windows.Forms.Button buttonOkay;
	}
}