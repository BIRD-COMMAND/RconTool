using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RconTool
{
    public class AutoCompleteTextBox : TextBox
    {

        //TODO minor. clean up or stop using this AutoCompleteTextBox implementation

        private ListBoxExtended _listBox;
        private String[] _values;
        private String _formerValue = String.Empty;
        private int _prevBreak;
        private int _nextBreak;
        private int _wordLen;
        private bool skipNextUpdate = false;

        public AutoCompleteTextBox()
        {
            this.HandleCreated += (o, e) => {
                
                _listBox = new ListBoxExtended(this, GetLocation);
                KeyDown += this_KeyDown;
                KeyUp += this_KeyUp;
                ResetListBox();
            };
        }
        public Point GetLocation()
        {
            Form f = this.FindForm();
            //return this.PointToScreen(new Point(this.Left + _listBox.Left, this.Top + _listBox.Top + this.Location.Y));
            //return this.PointToScreen(new Point(this.Location.X - this.Parent.Left, this.Parent.Location.Y + this.Location.Y + this.Height));
            return f.PointToScreen(new Point(this.Parent.Location.X + this.Location.X, this.Parent.Location.Y + this.Location.Y + this.Height));
            //return f.PointToScreen(new Point(f.Location.X + parent.Location.X + this.Location.X, f.Location.Y + parent.Location.Y + this.Location.Y + this.Height));
        }

        //private void InitializeComponent(Control parent)
        //{
        //    //KeyDown += this_KeyDown;
        //    //KeyUp += this_KeyUp;
        //}

        private void ShowListBox()
        {            
            _listBox.Visible = true;
            _listBox.BringToFront();
        }

        private void ResetListBox()
        {
            _listBox.Visible = false;
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            if (!skipNextUpdate) {
                UpdateListBox();
            }
            else
            {
                skipNextUpdate = false;
            }

        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {

            if (Text.Length == 0) { return; }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                case Keys.Space:
                {
                    if (_listBox.Visible)
                    {
                        if (Text.Length == 1)
                        {
                            Text = _listBox.SelectedItem.ToString();
                        }
                        else
                        {
                            Text = Text.Remove(_prevBreak == 0 ? 0 : _prevBreak + 1, _prevBreak == 0 ? _wordLen + 1 : _wordLen);
                            Text = Text.Insert(_prevBreak == 0 ? 0 : _prevBreak + 1, _listBox.SelectedItem.ToString());
                        }
                        ResetListBox();
                        _formerValue = Text;
                        Select(Text.Length, 0);
                        e.Handled = true;
                        
                    }
                    
                    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) {
                        e.SuppressKeyPress = true;
                    }

                    break;
                }
                case Keys.Down:
                {
                    if (Text.Length == 1 && Text == "%") { skipNextUpdate = true; }
                    if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                        _listBox.SelectedIndex++;
                    e.Handled = true;
                    break;
                }
                case Keys.Up:
                {
                    if (Text.Length == 1 && Text == "%") { skipNextUpdate = true; }
                    if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                        _listBox.SelectedIndex--;
                    e.Handled = true;
                    break;
                }
                case Keys.Escape:
                {
                    if ((_listBox.Visible)) { 
                        ResetListBox();
                        e.Handled = true;
                    }
                    break;
                }


            }
        }


        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    if (_listBox.Visible)
                        return true;
                    else
                        return false;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void UpdateListBox()
        {
            if (Text == _formerValue) return;
            if (Text.Length == 0)
            {
                _listBox.Visible = false;
                return;
            }
            string word = "";
            if (Text.Length == 1 && !string.IsNullOrWhiteSpace(Text)) { word = Text; }
            else {                
                _formerValue = Text;
                var separators = new[] { '|', '[', ']', '\r', '\n', ' ', '\t' };
                _prevBreak = Text.LastIndexOfAny(separators, CaretIndex > 0 ? CaretIndex - 1 : 0);
                if (_prevBreak < 1) _prevBreak = 0;
                _nextBreak = Text.IndexOfAny(separators, _prevBreak + 1);
                if (_nextBreak == -1) _nextBreak = CaretIndex;
                _wordLen = _nextBreak - _prevBreak - 1;
                if (_wordLen < 1) { return; }
                word = Text.Substring(_prevBreak + 1, _wordLen);
            }

            if (_values != null && word.Length > 0)
            {

                if (!word.StartsWith("%")) { return; }

                string[] matches = Array.FindAll(_values,
                    x => (x.ToLower().Contains(word.ToLower())));
                if (matches.Length > 0)
                {
                    ShowListBox();
                    _listBox.BeginUpdate();
                    _listBox.Items.Clear();
                    Array.ForEach(matches, x => _listBox.Items.Add(x));
                    _listBox.SelectedIndex = 0;
                    _listBox.Height = 0;
                    _listBox.Width = 0;
                    Focus();
                    using (Graphics graphics = _listBox.CreateGraphics())
                    {
                        for (int i = 0; i < _listBox.Items.Count; i++)
                        {
                            if (i < 20)
                                _listBox.Height += _listBox.GetItemHeight(i);
                            // it item width is larger than the current one
                            // set it to the new max item width
                            // GetItemRectangle does not work for me
                            // we add a little extra space by using '_'
                            int itemWidth = (int)graphics.MeasureString(((string)_listBox.Items[i]) + "_", _listBox.Font).Width;
                            _listBox.Width = (_listBox.Width < itemWidth) ? itemWidth : Width; ;
                        }
                    }
                    _listBox.EndUpdate();
                }
                else
                {
                    ResetListBox();
                }
            }
            else
            {
                ResetListBox();
            }
        }

        public int CaretIndex => SelectionStart;

        public String[] Values {
            get {
                return _values;
            }
            set {
                _values = value;
            }
        }
    }

}
