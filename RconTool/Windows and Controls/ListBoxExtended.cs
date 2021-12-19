using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace RconTool
{
    

    public class ListBoxExtended : ListBox
    {

        private Control mParent;
        private Point mPos;
        private bool mInitialized;
        private Func<Point> GetPosition;

        public ListBoxExtended(Control parent, Func<Point> GetPosition)
        {
            mParent = parent;
            mInitialized = true;
            this.GetPosition = GetPosition;
            this.SetTopLevel(true);
            parent.FindForm().LocationChanged += new EventHandler(parent_LocationChanged);
            mPos = mParent.Location;            
        }

        public new Point Location {
            get { return mParent.PointToClient(this.Location); }
            set {
                Point zero = mParent.PointToScreen(Point.Empty);
                base.Location = new Point(zero.X + value.X, zero.Y + value.Y);
            }
        }

        protected override Size DefaultSize {
            get {
                return mInitialized ? base.DefaultSize : Size.Empty;
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (this.mInitialized)
                base.SetBoundsCore(x, y, width, height, specified);
        }

        void parent_LocationChanged(object sender, EventArgs e)
        {
            base.Location = GetPosition();            
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                if (mParent != null && !DesignMode)
                {

                    cp.Style = (int)(((long)cp.Style & 0xffff) | 0x90200000);
                    cp.Parent = mParent.Handle;

                    Point pos = GetPosition();
                    cp.X = pos.X;
                    cp.Y = pos.Y;
                    cp.Width = base.DefaultSize.Width;
                    cp.Height = base.DefaultSize.Height;
                }
                return cp;
            }
        }
    }
}
