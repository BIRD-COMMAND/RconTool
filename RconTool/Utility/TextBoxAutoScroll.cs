using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace RconTool
{
	class TextBoxAutoScroll : RichTextBox
	{

        [Browsable(true)]
        public bool AutoScroll { get; set; }

        [Browsable(true)]
        public TabPage TabPage {
            get { return m_TabPage; }
            set { 
                m_TabPage = value;
                if (!DesignMode && value != null) { ((TabControl)m_TabPage.Parent).Selected += TabSelected_UpdateAutoScroll; }
            }
        }
        private TabPage m_TabPage;

        [Browsable(true)]
        public Button ButtonAutoScrollToggle { 
            get { return m_ButtonAutoScrollToggle; } 
            set { 
                m_ButtonAutoScrollToggle = value;
                if (!DesignMode && value != null) { m_ButtonAutoScrollToggle.Click += ButtonAutoScrollToggle_UpdateAutoScroll; }
            }
        }
        private Button m_ButtonAutoScrollToggle;

        private Mutex ScrollingMutex { get; set; } = new Mutex();
        private bool ScrollInProgress {
            get {
                ScrollingMutex.WaitOne();
                bool result = scrollInProgress;
                ScrollingMutex.ReleaseMutex();
                return result;
            }
            set {
                ScrollingMutex.WaitOne();
                scrollInProgress = value;
                ScrollingMutex.ReleaseMutex();
            }
        }
        private bool scrollInProgress = false;

        public TextBoxAutoScroll()
		{
            if (!DesignMode) { 
                TextChanged += TextChanged_UpdateAutoScroll;
				HandleCreated += OnHandleCreated;
            }
        }

		private void OnHandleCreated(object sender, EventArgs e)
		{
            if (IsHandleCreated) {

				// SetEventMask for Scroll Events

				// EM_SETEVENTMASK 	(WM_USER(0x0400) + 69)
				// EM_GETEVENTMASK 	(WM_USER(0x0400) + 59)
				// ENM_SCROLL		0x00000004
				int EM_GETEVENTMASK = 0x0400 + 59;
                int EM_SETEVENTMASK = 0x0400 + 69;
                long originalMask = SendMessage(Handle, EM_GETEVENTMASK, 0, IntPtr.Zero).ToInt64();
                long newMask = originalMask | 0x00000004 | 0x00000008;
                SendMessage(Handle, EM_SETEVENTMASK, 0, new IntPtr(newMask));

            }
            else { 
                throw new Exception(
                    "OnHandleCreated was called on an AutoScrollTextBox before the Handle was created.\n"
                    + "This should be impossible, and I'm very upset at Microsoft.\n"
                    + "Please report this bug if it happens more than once."
                );
            }
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_VSCROLL && IsHandleCreated) {
                // This setup picks up direct interactions with the scroll bar, clicks, drags, etc.
                // int messageInt = Low16(m.WParam);
                // string sType = WM_VSCROLL_MESSAGES.ContainsKey(messageInt) ? WM_VSCROLL_MESSAGES[messageInt] : "";
                // Console.WriteLine($"WM_VSCROLL: {sType}");
                Scrollbar_UpdateAutoScroll();
            }
            else if (m.Msg == WM_REFLECT + WM_COMMAND && High16(m.WParam) == EN_VSCROLL && IsHandleCreated) {
                // This setup correctly picks up mouse scroll events. The WParam HIWORD is 1538(0x602):EN_VSCROLL
                // Console.WriteLine($"WPLow:{Low16(m.WParam)}|WPHigh:{High16(m.WParam)}");
                Scrollbar_UpdateAutoScroll();
            }
			base.WndProc(ref m);
        }

        private void ButtonAutoScrollToggle_UpdateAutoScroll(object sender, EventArgs e)
        {
            if ( ((TabControl)TabPage.Parent).SelectedTab == TabPage ) {
                AutoScroll = !AutoScroll;
                if (AutoScroll) { ScrollToBottom(); }
                UpdateAutoScrollButton();
            }
        }
        public void TabSelected_UpdateAutoScroll(object sender, TabControlEventArgs e)
		{
            if (((TabControl)sender).SelectedTab == TabPage) {
                while (Text.StartsWith("\n") || Text.StartsWith("\r\n")) { Text = Text.Remove(0, 1); }
                if (AutoScroll) { ScrollToBottom(); }
                Scrollbar_UpdateAutoScroll();
                UpdateAutoScrollButton(); 
            }
		}
        private void TextChanged_UpdateAutoScroll(object sender, EventArgs e)
		{
            if (AutoScroll) { ScrollToBottom(); }
        }
        private void Scrollbar_UpdateAutoScroll()
		{
            if (ScrollInProgress) { return; }
			si = new SCROLLINFO { fMask = SIF_ALL };
			si.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(si);
            if (GetScrollInfo(Handle, SB_VERT, ref si)) {
                //Console.WriteLine($"min:{si.nMin}|max:{si.nMax}|pos:{si.nPos + si.nPage}");
                if (AutoScroll) {
                    if (si.nPos + si.nPage < (si.nMax - 1)) {
                        //Console.WriteLine($"AutoScroll OFF for {Name}");
                        AutoScroll = false;
                        UpdateAutoScrollButton();
                    }
                }
                else {
                    if (si.nPos + si.nPage >= (si.nMax - 1)) {
                        //Console.WriteLine($"AutoScroll ON for {Name}");
                        AutoScroll = true;
                        UpdateAutoScrollButton();
                    }
                }
            }
        }

        private void ScrollToBottom()
		{

            // Scroll to the bottom first, and then send a Scroll-One-Line-Down message to clear any incorrect spacing.
            //
            // Using ScrollToCaret to scroll to the end of a text box can result in an incorrect scroll position
            // if the text box height (+ any spacing) don't add up to a valid multiple of the text box's line height.

            ScrollInProgress = true;

            // Scroll to bottom using ScrollToCaret
            SelectionStart = TextLength;
            SelectionLength = 0;
            ScrollToCaret();

            // Scroll down one line (clears any incorrect spacing) |  EM_SCROLL (0x00B5)  SB_LINEDOWN (1)
            SendMessage(Handle, 0x00B5, 1, IntPtr.Zero);

            ScrollInProgress = false;

        }

        private void UpdateAutoScrollButton()
		{
            if (AutoScroll) { 
                if (ButtonAutoScrollToggle.Image != Properties.Resources.autoScrollButtonFade16x16) {
                    ButtonAutoScrollToggle.BackgroundImage = Properties.Resources.autoScrollButtonFade16x16;
                    ButtonAutoScrollToggle.Invalidate();
                    App.form.toolTip1.SetToolTip(ButtonAutoScrollToggle, "Auto-Scroll is currently enabled.\nClick to disable Auto-Scroll.");
                } 
            }
            else { 
                if (ButtonAutoScrollToggle.Image != Properties.Resources.autoScrollButtonNonfade16x16) {
                    ButtonAutoScrollToggle.BackgroundImage = Properties.Resources.autoScrollButtonNonfade16x16;
                    ButtonAutoScrollToggle.Invalidate();
                    App.form.toolTip1.SetToolTip(ButtonAutoScrollToggle, "Auto-Scroll is currently disabled.\nClick to enable Auto-Scroll.");
                } 
            }
        }


        #region ScrollInfo

        [StructLayout(LayoutKind.Sequential)]
        private struct SCROLLINFO
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        [DllImport("user32.dll", EntryPoint = "GetScrollInfo")]
        private static extern bool GetScrollInfo([In] IntPtr hwnd, [In] int fnBar, [In, Out] ref SCROLLINFO lpsi);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);

        private SCROLLINFO si = new SCROLLINFO();

        private Dictionary<int, string> WM_VSCROLL_MESSAGES = new Dictionary<int, string>()
        {
            { 0,"SB_LINEUP"},
            { 1,"SB_LINEDOWN"},
            { 2,"SB_PAGEUP"},
            { 3,"SB_PAGEDOWN"},
            { 4,"SB_THUMBPOSITION"},
            { 5,"SB_THUMBTRACK"},
            { 6,"SB_TOP"},
            { 7,"SB_BOTTOM "},
            { 8,"SB_ENDSCROLL"}
        };
        private Dictionary<int, string> WM_HSCROLL_MESSAGES = new Dictionary<int, string>()
        {
            { 0,"SB_LINELEFT"},
            { 1,"SB_LINERIGHT"},
            { 2,"SB_PAGELEFT"},
            { 3,"SB_PAGERIGHT"},
            { 4,"SB_THUMBPOSITION"},
            { 5,"SB_THUMBTRACK"},
            { 6,"SB_LEFT"},
            { 7,"SB_RIGHT"},
            { 8,"SB_ENDSCROLL"}
        };

        //private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;        
        private const int SIF_PAGE = 0x2;
        private const int SIF_POS = 0x4;
        private const int SIF_RANGE = 0x1;
        private const int SIF_TRACKPOS = 0x10;
        private const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);
        private const int EN_VSCROLL = 0x0602;
        //private const int WM_NOTIFY = 0x004e;
        private const int WM_COMMAND = 0x0111;
        private const int WM_REFLECT = 0x2000;
        //private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        private int GetIntUnchecked(IntPtr value)
        {
            return IntPtr.Size == 8 ? unchecked((int)value.ToInt64()) : value.ToInt32();
        }
        private int Low16(IntPtr value)
        {
            return unchecked((short)GetIntUnchecked(value));
        }
        private int High16(IntPtr value)
        {
            return unchecked((short)(((uint)GetIntUnchecked(value)) >> 16));
        }

        #endregion

    }
}
