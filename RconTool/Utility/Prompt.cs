using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RconTool
{
	public static class Prompt
	{

        //
        // based on / inspired by
        // https://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
        //
        //
        //   Steal things without remorse, 
        //  what are they gonna do about it?
        //
        //                    - BIRD COMMAND
        //                          
        //                          - Michael Scott
        //

        /// <summary>
        /// Prompts the user to enter a string and returns that value. Only the <paramref name="text"/> and <paramref name="title"/> parameters are required.
        /// </summary>
        /// <param name="text">The text to be displayed within the prompt. This text should explain what value is expected and ask the user to enter it.</param>
        /// <param name="title">The title of the prompt window.</param>
        /// <param name="existingFieldValue">A string which will be used to populate the generated input's entry field. Omit or pass null to use the default value (blank).</param>
        /// <param name="inputControlRect">A rectangle whose values will be applied to the generated input, overriding its default size and location.</param>
        public static string ShowStringDialog(string text, string title, string existingFieldValue = null, Rectangle? inputControlRect = null)
        {
            return (string)ShowDialog(text, title, PromptType.String, existingFieldValue, inputControlRect);
        }

        /// <summary>
        /// Prompts the user to enter an integer and returns that value. Only the <paramref name="text"/> and <paramref name="title"/> parameters are required.
        /// </summary>
        /// <param name="text">The text to be displayed within the prompt. This text should explain what value is expected and ask the user to enter it.</param>
        /// <param name="title">The title of the prompt window.</param>
        /// <param name="existingFieldValue">An integer which will be used to populate the generated input's entry field. Omit or pass null to use the default value (0).</param>
        /// <param name="inputControlRect">A rectangle whose values will be applied to the generated input, overriding its default size and location.</param>
        public static int ShowIntDialog(string text, string title, int? existingFieldValue = null, Rectangle? inputControlRect = null)
        {
            return (int)ShowDialog(text, title, PromptType.Int, existingFieldValue, inputControlRect);
        }

        /// <summary>
        /// Prompts the user to input a value and returns that value.
        /// </summary>
        /// <param name="text">The text to be displayed within the prompt. This text should explain what value is expected and ask the user to enter it.</param>
        /// <param name="title">The title of the prompt window.</param>
        /// <param name="type">The type of prompt to use.</param>
        /// <param name="existingFieldValue">An object whose value will be used to populate the generated input's entry field. Omit or pass null to use default values. If provided, must match the type indicated by the supplied <paramref name="type"/>.</param>
        /// <param name="inputControlRect">A rectangle whose values will be applied to the generated input, overriding its default size and location.</param>
        /// <returns></returns>
        private static object ShowDialog(string text, string title, PromptType type, object existingFieldValue = null, Rectangle? inputControlRect = null)
        {

            Form prompt = new Form()
            {
                Text = title,
                FormBorderStyle = FormBorderStyle.FixedDialog,                
                StartPosition = FormStartPosition.CenterScreen,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true,
            };

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true,
            };
            prompt.Controls.Add(flowLayoutPanel);

            //Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            //Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            Label textLabel = new Label() { Text = text, AutoSize = true };
            Button confirmation = new Button() { Text = "Ok", DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            TextBox textBox = null;
            NumericUpDown numericUpDown = null;

            
            // Create type-specific input control
            switch (type)
            {
                case PromptType.String: textBox = new TextBox() { Width = 400, TextAlign = HorizontalAlignment.Right }; break;
                case PromptType.Int: numericUpDown = new NumericUpDown() { Width = 80, TextAlign = HorizontalAlignment.Right }; break;
                default: break;
            }

            // Apply rect to control
            if (inputControlRect != null)
            {
                switch (type)
                {
                    case PromptType.String: SetInputRectValues(textBox, (Rectangle)inputControlRect); break;
                    case PromptType.Int: SetInputRectValues(numericUpDown , (Rectangle)inputControlRect); break;
                    default: break;
                }
            }

            // Populate input with existing field value
            if (existingFieldValue != null)
            {
                try
                {
                    switch (type)
                    {
                        case PromptType.String: textBox.Text = (string)existingFieldValue; break;
                        case PromptType.Int: numericUpDown.Value = (int)existingFieldValue; break;
                        default: break;
                    }
                }
                catch(Exception e)
                {
                    throw new Exception("Failed to set existing field value for the input control. Most likely an invalid type was supplied.", e);
                }
            }


            // Add controls to flow layout panel
            flowLayoutPanel.Controls.Add(textLabel);

            // Add type-specific input control to the form
            switch (type)
            {
                case PromptType.String: flowLayoutPanel.Controls.Add(textBox); break;
                case PromptType.Int: flowLayoutPanel.Controls.Add(numericUpDown); break;
                default: break;
            }

            flowLayoutPanel.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            // Fix spacing issue within flow layout panel
            SetControlTopMargin(textLabel, 8);
            switch (type)
            {
                case PromptType.String: SetControlTopMargin(textBox, 4); break;
                case PromptType.Int: SetControlTopMargin(numericUpDown, 4); break;
                default: break;
            }

            // Return prompt result
            switch (type)
            {
                case PromptType.String: return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
                case PromptType.Int: return prompt.ShowDialog() == DialogResult.OK ? (int)numericUpDown.Value : 0;
                default: return null;
            }

        }

        /// <summary>
        /// Sets the size and position of a control to match the supplied rectangle.
        /// </summary>
        /// <param name="control">Control whose size and location should be modified.</param>
        /// <param name="rect">Rectangle whose size and location should be applied to the control.</param>
        private static void SetInputRectValues(Control control, Rectangle rect)
        {
            if (control == null) { return; }
            control.Location = rect.Location;
            control.Size = rect.Size;
        }

        /// <summary>
        /// Updates the top margin of a control.
        /// </summary>
        /// <param name="control">Control whose margin will be updated.</param>
        /// <param name="margin">Value to apply to the top margin.</param>
        private static void SetControlTopMargin(Control control, int margin)
        {
            if (margin < 0) { return; }
            control.Margin = new Padding(control.Margin.Left, margin, control.Margin.Right, control.Margin.Bottom);
        }

        /// <summary>
        /// Indicates the type of value returned by a prompt.
        /// </summary>
        public enum PromptType
        {
            String,
            Int
        }

    }
}
