using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RconTool
{

    /// <summary>
    /// Very basic slider control with selection range.
    /// </summary>
    [Description("Slider control with selection range.")]
    public partial class SelectionRangeSlider : UserControl
    {
        /// <summary>
        /// Minimum value of the slider.
        /// </summary>
        [Description("Minimum value of the slider.")]
        public int Min {
            get { return min; }
            set { min = value; Invalidate(); }
        }
        int min = 0;
        /// <summary>
        /// Maximum value of the slider.
        /// </summary>
        [Description("Maximum value of the slider.")]
        public int Max {
            get { return max; }
            set { max = value; Invalidate(); }
        }
        int max = 100;
        /// <summary>
        /// Minimum value of the selection range.
        /// </summary>
        [Description("Minimum value of the selection range.")]
        public int SelectedMin {
            get { return selectedMin; }
            set {
                selectedMin = value;
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
                Invalidate();
            }
        }
        int selectedMin = 0;
        /// <summary>
        /// Maximum value of the selection range.
        /// </summary>
        [Description("Maximum value of the selection range.")]
        public int SelectedMax {
            get { return selectedMax; }
            set {
                selectedMax = value;
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
                Invalidate();
            }
        }
        int selectedMax = 100;
        /// <summary>
        /// Fired when SelectedMin or SelectedMax changes.
        /// </summary>
        [Description("Fired when SelectedMin or SelectedMax changes.")]
        public event EventHandler SelectionChanged;

        public SelectionRangeSlider()
        {
            InitializeComponent();
            //avoid flickering
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Paint += new PaintEventHandler(SelectionRangeSlider_Paint);
            MouseDown += new MouseEventHandler(SelectionRangeSlider_MouseDown);
            MouseMove += new MouseEventHandler(SelectionRangeSlider_MouseMove);
        }

        void SelectionRangeSlider_Paint(object sender, PaintEventArgs e)
        {

            Brush backgroundBrush = SystemBrushes.Control; ;
            Brush selectionBackground;
            Pen border;
            Brush thumb;
            if (Enabled)
            {
                selectionBackground = SystemBrushes.GradientActiveCaption;
                border = Pens.Black;
                thumb = SystemBrushes.ControlDarkDark;
            }
            else
            {
                selectionBackground = SystemBrushes.ControlLight;
                border = Pens.DarkGray;
                thumb = SystemBrushes.ControlDark;
            }


            //paint background Control color
            e.Graphics.FillRectangle(backgroundBrush, ClientRectangle);

            //paint selection range in blue
            Rectangle selectionRect = new Rectangle(
                (selectedMin - Min) * Width / (Max - Min),
                0,
                (selectedMax - selectedMin) * Width / (Max - Min),
                Height);
            e.Graphics.FillRectangle(selectionBackground, selectionRect);

            # region Draw ticks
			SelectionRangeSliderRenderer.DrawHorizontalTicks(
                e.Graphics,
                new Rectangle(0, Height - 5, Width, 5),
                17,
                EdgeStyle.Bump
            );
            #endregion

            #region Draw left thumb
            //Left thumb position will be at
            // ( (selectedMin - Min) * Width / (Max - Min) ) - (half thumb's width)
            e.Graphics.FillRectangle(thumb,
                //SelectionRangeSliderRenderer.DrawHorizontalThumb(
                //e.Graphics,
                new Rectangle(
                    ((selectedMin - Min) * Width / (Max - Min)) - 4,
                    0,
                    8,
                    Height
                )
                //TrackBarThumbState.Normal
            //);
            );
            #endregion

            #region Draw right thumb
            //Right thumb position will be at
            // 
            e.Graphics.FillRectangle(thumb,
                //SelectionRangeSliderRenderer.DrawHorizontalThumb(
                //e.Graphics,
                new Rectangle(
                    ((selectedMin - Min) * Width / (Max - Min)) + ((selectedMax - selectedMin) * Width / (Max - Min)) - 4,
                    0,
                    8,
                    Height
                )
            //TrackBarThumbState.Normal
            //);
            );
			#endregion

			//draw a black frame around our control
			e.Graphics.DrawRectangle(border, 0, 0, Width - 1, Height - 1);

        }

        void SelectionRangeSlider_MouseDown(object sender, MouseEventArgs e)
        {
            //check where the user clicked so we can decide which thumb to move
            int pointedValue = Min + e.X * (Max - Min) / Width;
            //int distValue = Math.Abs(pointedValue - Value);
            int distMin = Math.Abs(pointedValue - SelectedMin);
            int distMax = Math.Abs(pointedValue - SelectedMax);
            int minDist = Math.Min(distMin, distMax);
            if (minDist == distMin)
                movingMode = MovingMode.MovingMin;
            else
                movingMode = MovingMode.MovingMax;
            //call this to refresh the position of the selected thumb
            SelectionRangeSlider_MouseMove(sender, e);
        }

        void SelectionRangeSlider_MouseMove(object sender, MouseEventArgs e)
        {
            //if the left button is pushed, move the selected thumb
            if (e.Button != MouseButtons.Left)
                return;
            int pointedValue = (Min + (e.X + ((Width / (Max - Min)) / 2)) * (Max - Min) / Width);
            //if (movingMode == MovingMode.MovingValue)
            //    Value = pointedValue;
            if (movingMode == MovingMode.MovingMin) { 
                if (SelectedMin > SelectedMax) {
                    // swap sliders, move the max paddle instead
                    SelectedMin = SelectedMax;
                    movingMode = MovingMode.MovingMax;
				}
                else { SelectedMin = pointedValue; }
            }
            else if (movingMode == MovingMode.MovingMax) { 
                if (SelectedMax < SelectedMin) {
                    // swap sliders, move the min paddle instead
                    SelectedMax = SelectedMin;
                    movingMode = MovingMode.MovingMin;
                }
                else { SelectedMax = pointedValue; }
            }

            // Clamp Values
            if (SelectedMin < Min) { SelectedMin = Min; }
            if (SelectedMax > Max) { SelectedMax = Max; }

        }

        /// <summary>
        /// To know which thumb is moving
        /// </summary>
        enum MovingMode { MovingMin, MovingMax }
        MovingMode movingMode;

        /// <summary>
        ///  This is a rendering class for the TrackBar control.
        /// </summary>
        public static class SelectionRangeSliderRenderer
        {
            //Make this per-thread, so that different threads can safely use these methods.
            [ThreadStatic]
            private static VisualStyleRenderer visualStyleRenderer = null;
            const int lineWidth = 2;

            /// <summary>
            ///  Returns true if this class is supported for the current OS and user/application settings,
            ///  otherwise returns false.
            /// </summary>
            public static bool IsSupported => VisualStyleRenderer.IsSupported; // no downlevel support

            /// <summary>
            ///  Renders a horizontal track.
            /// </summary>
            public static void DrawHorizontalTrack(Graphics g, Rectangle bounds)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.Track.Normal, 1);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a vertical track.
            /// </summary>
            public static void DrawVerticalTrack(Graphics g, Rectangle bounds)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.TrackVertical.Normal, 1);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a horizontal thumb.
            /// </summary>
            public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.Thumb.Normal, (int)state);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a vertical thumb.
            /// </summary>
            public static void DrawVerticalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbVertical.Normal, (int)state);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a constant size left pointing thumb centered in the given bounds.
            /// </summary>
            public static void DrawLeftPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal, (int)state);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a constant size right pointing thumb centered in the given bounds.
            /// </summary>
            public static void DrawRightPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal, (int)state);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a constant size top pointing thumb centered in the given bounds.
            /// </summary>
            public static void DrawTopPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal, (int)state);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a constant size bottom pointing thumb centered in the given bounds.
            /// </summary>
            public static void DrawBottomPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal, (int)state);

                visualStyleRenderer.DrawBackground(g, bounds);
            }

            /// <summary>
            ///  Renders a horizontal tick.
            /// </summary>
            public static void DrawHorizontalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
            {
                if (numTicks <= 0 || bounds.Height <= 0 || bounds.Width <= 0 || g == null)
                {
                    return;
                }

                InitializeRenderer(VisualStyleElement.TrackBar.Ticks.Normal, 1);

                //trivial case -- avoid calcs
                if (numTicks == 1)
                {
                    visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, bounds.Y, lineWidth, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
                    return;
                }

                float inc = ((float)bounds.Width - lineWidth) / ((float)numTicks - 1);

                while (numTicks > 0)
                {
                    //draw the nth tick
                    float x = bounds.X + ((float)(numTicks - 1)) * inc;
                    visualStyleRenderer.DrawEdge(g, new Rectangle((int)Math.Round(x), bounds.Y, lineWidth, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
                    numTicks--;
                }
            }

            /// <summary>
            ///  Renders a vertical tick.
            /// </summary>
            public static void DrawVerticalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
            {
                if (numTicks <= 0 || bounds.Height <= 0 || bounds.Width <= 0 || g == null)
                {
                    return;
                }

                InitializeRenderer(VisualStyleElement.TrackBar.TicksVertical.Normal, 1);

                //trivial case
                if (numTicks == 1)
                {
                    visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, bounds.Y, bounds.Width, lineWidth), Edges.Top, edgeStyle, EdgeEffects.None);
                    return;
                }

                float inc = ((float)bounds.Height - lineWidth) / ((float)numTicks - 1);

                while (numTicks > 0)
                {
                    //draw the nth tick
                    float y = bounds.Y + ((float)(numTicks - 1)) * inc;
                    visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.X, (int)Math.Round(y), bounds.Width, lineWidth), Edges.Top, edgeStyle, EdgeEffects.None);
                    numTicks--;
                }
            }

            /// <summary>
            ///  Returns the size of a left pointing thumb.
            /// </summary>
            public static Size GetLeftPointingThumbSize(Graphics g, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal, (int)state);

                return (visualStyleRenderer.GetPartSize(g, ThemeSizeType.True));
            }

            /// <summary>
            ///  Returns the size of a right pointing thumb.
            /// </summary>
            public static Size GetRightPointingThumbSize(Graphics g, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal, (int)state);

                return (visualStyleRenderer.GetPartSize(g, ThemeSizeType.True));
            }

            /// <summary>
            ///  Returns the size of a top pointing thumb.
            /// </summary>
            public static Size GetTopPointingThumbSize(Graphics g, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal, (int)state);

                return (visualStyleRenderer.GetPartSize(g, ThemeSizeType.True));
            }

            /// <summary>
            ///  Returns the size of a bottom pointing thumb.
            /// </summary>
            public static Size GetBottomPointingThumbSize(Graphics g, TrackBarThumbState state)
            {
                InitializeRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal, (int)state);

                return (visualStyleRenderer.GetPartSize(g, ThemeSizeType.True));
            }

            private static void InitializeRenderer(VisualStyleElement element, int state)
            {
                if (visualStyleRenderer == null)
                {
                    visualStyleRenderer = new VisualStyleRenderer(element.ClassName, element.Part, state);
                }
                else
                {
                    visualStyleRenderer.SetParameters(element.ClassName, element.Part, state);
                }
            }

        }

    }
}
