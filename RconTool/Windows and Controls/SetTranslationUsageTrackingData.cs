using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RconTool
{
	public partial class SetTranslationUsageTrackingData : Form
	{
		public SetTranslationUsageTrackingData()
		{
			InitializeComponent();
			numericUpDownTranslatedCharacterCount.Value = App.TranslatedCharactersThisBillingCycle.Value;
			dateTimePickerBillingCycleStartDate.Value = App.TranslateBillingCycleDateTime.Value;
		}

		private void buttonOkay_Click(object sender, EventArgs e)
		{
			App.form.UpdateTranslationUsageTrackingData(true);
			Close();
		}
	}
}
