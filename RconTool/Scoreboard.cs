using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RconTool
{
    public static class Scoreboard
    {

        /// <summary>
        /// Current drawing context.
        /// </summary>
        public static Graphics G {
            get {
                if (g == null) { throw new Exception("Scoreboard attempted to use an invalid graphics context for something."); }
                else { return g; }
            }
            set { g = value; }
        }
        private static Graphics g;

        public static Point Position {
            get { return position; }
            set {
                position = value;
                Layout();
            }
        }
        private static Point position = new Point(0, 0);

        public static List<ScoreboardColumn> Columns { get; set; } = new List<ScoreboardColumn>();

        private static Color HeaderBackgroundColor = Color.FromArgb(33, 33, 33);
        private static Color DarkerGray = Color.FromArgb(66, 66, 66);
        private static Color DarkGray = Color.FromArgb(96, 96, 96);
        private static Color Gray = Color.FromArgb(126, 126, 126);
        private static Color Gold = Color.FromArgb(204, 174, 44);

        public const int InitialFontSize = 14;

        public static bool HighlightAllRows { get; set; } = true;
        public static Bitmap EmblemDead { get; set; }
        public static Bitmap EmblemGeneric { get; set; }
        private static Bitmap RowHighlightRectangle { get; set; }
        public static bool RegenerateScoreboardImage { get; set; } = false;
        public static Bitmap ScoreboardImage { get; set; }
        public static bool RegenerateHeaderRowImage { get; set; } = true;
        public static Bitmap HeaderRowImage { get; set; } = null;
        public static Rectangle HeaderRowRect { get; set; }
        public static List<Rectangle> RowRects { get; set; } = new List<Rectangle>(16);
        public static bool RegenerateNamesColumnImage { get; set; } = true;
        public static Bitmap NamesColumnImage { get; set; } = null;


        public static CustomFont DefaultScoreboardFont = CustomFont.UbuntuMonoBird;

        public static bool useMargins = true;
        public static SizeF Margins { get; set; } = new SizeF(16f, 8f);
        public static Size ImageColumnMargins { get; set; } = new Size(8, 8);
        public static Size ImageColumnPadding { get; set; } = new Size(8, 8);
        public static StringFormat HeaderSF { get; set; } = null;
        public static StringFormat RightJustifySF { get; set; } = new StringFormat()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center
        };
        public static StringFormat CenterJustifySF { get; set; } = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        public static StringFormat LeftJustifySF { get; set; } = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };
        public static Alignment headerAlignment = Alignment.Center;
        public static Alignment scoreboardCellTextAlignment = Alignment.Right;
        public static Pen outlinePen = new Pen(Color.Black, 3f) { LineJoin = LineJoin.Bevel };
        public static Font fontHeader = FontUtility.GetMonospaceFont(DefaultScoreboardFont, (InitialFontSize > 4) ? InitialFontSize - 4 : InitialFontSize);
        public static Font fontRegular = FontUtility.GetMonospaceFont(DefaultScoreboardFont, InitialFontSize);
        public static Font fontDeathIndicator = FontUtility.GetMonospaceFont(DefaultScoreboardFont, (InitialFontSize > 2) ? InitialFontSize - 2 : InitialFontSize);
        public static int FontSize { 
            get { return fontSize; }
            set 
            {
                //if (value <= 8) { Margins = new SizeF(8,6); }
                //else if (value <= 12) { Margins = new SizeF(12, 8); }
                //else { Margins = new SizeF(16, 8); }
                fontSize = value;
                Layout();
            }
        }
        private static int fontSize = InitialFontSize;
        public static float fontEmSize = 0;
        public static int rowHeight = 0;
        public static int Width { get; set; } = 0;
        public static int Height { get; set; } = 0;
        public static Size isAliveIndicatorSize = new Size();
        public static Point EmblemSize { 
            get { return new Point(rowHeight - ImageColumnPadding.Width, rowHeight - ImageColumnPadding.Width); } 
        }


        static Scoreboard()
        {
            HeaderSF = CenterJustifySF;
            App.ResizeRequired = true;
        }

        public static void Layout()
        {

            RegenerateHeaderRowImage = true;
            RegenerateScoreboardImage = true;

            Columns.Clear();
            RowRects.Clear();

            #region Add Columns

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {

                G = graphics;

                fontHeader = FontUtility.GetMonospaceFont(DefaultScoreboardFont, (fontSize > 4) ? fontSize - 4 : fontSize);
                fontRegular = FontUtility.GetMonospaceFont(DefaultScoreboardFont, fontSize);
                fontDeathIndicator = FontUtility.GetMonospaceFont(DefaultScoreboardFont, (fontSize > 2) ? fontSize - 2 : fontSize);

                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                SizeF temp = GetMonospaceTextRenderSize(1, graphics, fontDeathIndicator, RightJustifySF, Margins);
                isAliveIndicatorSize = new Size((int)temp.Width, (int)temp.Height);

                // buffer: text outline is 4, above and below text (TOTAL  8)
                // buffer: 8 more for margins of 4 above and below (TOTAL 16)
                int buffer = 16;
                rowHeight = (int)(GetMonospaceTextRenderSize(3, graphics, fontRegular, RightJustifySF, Margins).Height + buffer);
                Height = rowHeight /*Header Row*/ + rowHeight * 16;

                int defaultWidth = (int)(GetMonospaceTextRenderSize(5, graphics, fontRegular, CenterJustifySF, Margins).Width + buffer); ;

                // Emblem
                AddColumn(ColumnID.Emblem, "", -1, Alignment.Center, false, BorderType.BlendRight, BorderType.BlendRight);

                //"NAME"
                int nameCharLimit = 16 /* max player name length */;
                AddColumn(ColumnID.Name, "Players", nameCharLimit, Alignment.Left, false, BorderType.Standard, BorderType.Standard, Alignment.Left);
                
                //TAG
                AddColumn(ColumnID.Tag, "", 5, Alignment.Center, false, BorderType.BlendLeft);

                //"SCORE"
                //AddColumn(ColumnID.Score, "Score", 5, Alignment.Center);
                AddColumn(ColumnID.Score, "Score", defaultWidth);

                //"KILLS"
                //AddColumn(ColumnID.Kills, "Kills", 5, Alignment.Center);
                AddColumn(ColumnID.Kills, "Kills", defaultWidth);

                //"DEATHS"
                //AddColumn(ColumnID.Deaths, "Deaths", 5, Alignment.Center);
                AddColumn(ColumnID.Deaths, "Deaths", defaultWidth);

                //"K/D"
                //AddColumn(ColumnID.KD, "K/D", 6, Alignment.Center);
                AddColumn(ColumnID.KD, "K/D", defaultWidth);

                //"ASSISTS"
                //AddColumn(ColumnID.Assists, "Assist", 5, Alignment.Center);
                AddColumn(ColumnID.Assists, "Assist", defaultWidth);

                //"BETRAYALS"
                //AddColumn(ColumnID.Betrayals, "Betray", 5, Alignment.Center);
                AddColumn(ColumnID.Betrayals, "Betray", defaultWidth);

                //"SUICIDES"
                //AddColumn(ColumnID.Suicides, "Suicide", 5, Alignment.Center);
                AddColumn(ColumnID.Suicides, "Suicide", defaultWidth);

                //"STREAK"
                //AddColumn(ColumnID.BestStreak, "Streak", 5, Alignment.Center);
                AddColumn(ColumnID.BestStreak, "Streak", defaultWidth);

                //"IN GAME" - previously "TIME ALIVE"
                //AddColumn(ColumnID.TimeInGame, "In Game", 7, Alignment.Center);
                AddColumn(ColumnID.TimeInGame, "In Game", defaultWidth);

            }

            #endregion

            #region Layout Columns

            Point pos = new Point(0, 0);
            foreach (ScoreboardColumn column in Columns)
            {
                column.Position = pos;
                pos = new Point(pos.X + column.Width, pos.Y);
            }
            ScoreboardColumn last = Columns[Columns.Count - 1];
            Width = (last.Position.X + last.Width);

            #endregion
            
            #region Generate Row-Highlight Effect Image

            // I tried a lot of different methods of drawing the highlight-row effect image,
            // but they were all really inconsistent and resulted in strange render artifacts or
            // incomplete coverage depending on font size. This unfortunate hardcoded 'magic number'
            // spacing is the only spacing that's consistent across all font sizes.

            int o1 = rowHeight % 2;
            RowHighlightRectangle = new Bitmap(Width, rowHeight + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics x = System.Drawing.Graphics.FromImage(RowHighlightRectangle))
            {

                x.Clear(Color.Transparent);

                Rectangle fillA = new Rectangle(0, 0, Width, (rowHeight / 2) - 2 - o1);
                Rectangle fillB = new Rectangle(0, 0, Width, (rowHeight / 2) + 1);

                Rectangle a = new Rectangle(0, 0, Width, (rowHeight / 2) - 2 - o1);
                Rectangle b = new Rectangle(0, (rowHeight / 2) + 2 + o1, Width, (rowHeight / 2) + 1 + o1);

                LinearGradientBrush TopBrush = new LinearGradientBrush(
                    fillA,
                    Color.FromArgb(99, 22, 22, 22),
                    Color.Transparent,
                    LinearGradientMode.Vertical
                );
                x.FillRectangle(TopBrush, a);

                LinearGradientBrush BottomBrush = new LinearGradientBrush(
                    fillB,
                    Color.Transparent,
                    Color.FromArgb(99, 22, 22, 22),
                    LinearGradientMode.Vertical
                );
                x.FillRectangle(BottomBrush, b);
            }

            #endregion

            // Move Players Header to a better spot
            // updates its position so that it will be drawn over
            // the textless EMBLEM column header and
            // the textless TAG column header and 
            // add a bit of spacing
            GetColumn(ColumnID.Name).AbsorbHeadersLeft(1);
            GetColumn(ColumnID.Name).AbsorbHeadersRight(1);
            GetColumn(ColumnID.Name).BumpHeaderRight(8);

            // Calculate rects for each row - used for determining mouse-hover scoreboard context player			
            for (int i = 1; i <= 17; i++) { RowRects.Add(new Rectangle(Position.X, (i * rowHeight) + Position.Y, Width, rowHeight)); }

			// Resize Generic Emblem and Death Emblem
			EmblemDead = App.ResizeImage(Properties.Resources.Image_EmblemDead, EmblemSize.X, EmblemSize.Y);
            EmblemGeneric = App.ResizeImage(Properties.Resources.Image_EmblemGeneric, EmblemSize.X, EmblemSize.Y);

        }

        private static ScoreboardColumn GetColumn(ColumnID id)
        {
            if (Columns.Any(x => x.ID == id)) { return Columns.First(x => x.ID == id); }
            else { return null; }
        }
        private static void AddColumn(ColumnID id, string name, int characterLimit, Alignment contentAlignment, bool useNameWidth = false, BorderType headerBorderType = BorderType.Standard, BorderType cellBorderType = BorderType.Standard, Alignment headerAlignment = Alignment.Center)
        {
            Columns.Add(new ScoreboardColumn(id, name, characterLimit, contentAlignment, useNameWidth, headerBorderType, cellBorderType, headerAlignment));
        }
        private static void AddColumn(ColumnID id, string name, int width)
        {
            Columns.Add(new ScoreboardColumn(id, name, width));
        }

        public static void DrawScoreboard(object sender, PaintEventArgs e)
        {
            
            // Generate Header Row Image As Needed
            if (RegenerateHeaderRowImage || HeaderRowImage == null) { GenerateHeaderRowImage(); RegenerateHeaderRowImage = false; }

            // Draw Header Row Image
            G = e.Graphics;
            G.DrawImage(HeaderRowImage, Position);

            // Generate Scoreboard Image As Needed
            if (RegenerateScoreboardImage || ScoreboardImage == null) { GenerateScoreboardImage(); RegenerateScoreboardImage = false; }

            // Draw Scoreboard Image
            G = e.Graphics;
            G.DrawImage(ScoreboardImage, new Point(Position.X, Position.Y + rowHeight));
            //DirectDrawScoreboard();

            #region Highlight Hovered Row
            G = e.Graphics;
            if (!App.form.scoreBoardContextMenu.Visible)
            {

                // Track Hovered Row
                for (int i = 0; i < RowRects.Count; i++)
                {
                    if (RowRects[i].Contains(App.mousePoint))
                    {
                        if (App.currentConnection.State.Players.Count > i)
                        {
                            App.contextPlayer = App.currentConnection.State.Players[i];
                            G.DrawImage(RowHighlightRectangle, RowRects[i].Location);

                            //G.DrawRectangle(new Pen(Color.FromArgb(244, 10, 10, 10)), RowRects[i]);
                            //G.FillRectangle(new SolidBrush(Color.FromArgb(244, 10, 10, 10)), RowRects[i]);

                            //G.DrawImage(Properties.Resources.ServerIcon16x16, new Point(RowRects[i].X + 4, RowRects[i].Y + 6));
                        }
                        else
                        {
                            App.contextPlayer = null;
                        }
                    }
                }

            }
            else
            {

                int i = App.currentConnection.State.Players.IndexOf(App.contextPlayer);
                if (i != -1)
                {
                    G.DrawImage(RowHighlightRectangle, RowRects[i].Location);

                    //G.DrawRectangle(new Pen(Color.FromArgb(222, 66, 66, 66)), RowRects[i]);
                    //G.FillRectangle(new SolidBrush(Color.FromArgb(166, 66, 66, 66)), RowRects[i]);

                    //G.DrawImage(Properties.Resources.ServerIcon16x16, RowRects[i].X + 4, RowRects[i].Y + 6); 
                }

            }
            #endregion


        }

        private static void GenerateHeaderRowImage()
        {

            if (HeaderRowImage != null) { HeaderRowImage.Dispose(); }

            HeaderRowImage = new Bitmap(Width, rowHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = System.Drawing.Graphics.FromImage(HeaderRowImage))
            {

                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                graphics.Clear(Color.Transparent);

                // Draw Headers
                Point origin = new Point(0, 0);
                foreach (ScoreboardColumn column in Columns)
                {
                    if (column.DrawHeader)
                    {
                        graphics.FillRectangle(new SolidBrush(HeaderBackgroundColor), column.Header);
                        if (HighlightAllRows) { graphics.DrawImage(RowHighlightRectangle, column.Header, 0, 0, column.Header.Width, column.Header.Height, GraphicsUnit.Pixel); }
                        if (!string.IsNullOrEmpty(column.Name)) { DrawString(graphics, column.Header, column.HeaderContentOrigin, column.HeaderStringFormat, column.Name, HeaderBackgroundColor, Color.White, column.HeaderFont, Color.Black); }
                    }
                }

            }

        }        
        private static void GenerateScoreboardImage()
        {

            if (ScoreboardImage != null) { ScoreboardImage.Dispose(); }

            Connection currentConnection = App.currentConnection;
            ScoreboardImage = new Bitmap(Width, rowHeight * 16, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = System.Drawing.Graphics.FromImage(ScoreboardImage))
            {

                G = graphics;

                // Settings that improve scoreboard text appearance
                fontEmSize = G.DpiY * fontSize / 72;
                G.InterpolationMode = InterpolationMode.High;
                //G.InterpolationMode = InterpolationMode.HighQualityBilinear;
                G.SmoothingMode = SmoothingMode.HighQuality;
                G.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                G.CompositingQuality = CompositingQuality.HighQuality;
                //G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                


                // Draw Rows
                lock (currentConnection.State.ServerStateLock)
                {
                    int rowIndex = 0;
                    foreach (PlayerInfo player in currentConnection.State.Players)
                    {
                        DrawScoreboardRow(rowIndex, currentConnection, player, Color.Gray, Color.White);
                        rowIndex++;
                    }
                    while (rowIndex < 16) { DrawEmptyScoreboardRow(rowIndex, Gray); rowIndex++; }
                }

            }

        }

        private static void DrawScoreboardRow(int rowIndex, Connection currentConnection, PlayerInfo player, Color rectColor, Color textColor)
        {
            Color TagColor;
            if (currentConnection.State.InLobby)
            { 
                rectColor = DarkGray; 
                TagColor = DarkerGray; 
            }
            else if (currentConnection.State.Teams && player.Team != -1) 
            { 
                rectColor = App.TeamColors[player.Team].Item2; 
                TagColor = App.TeamColors[player.Team].Item1; 
            }
            else 
            { 
                rectColor = player.ScoreboardColor; 
                TagColor = player.ScoreboardColorDark; 
            }

            int c = 0;

            #region Draw Emblem + Name Rect

            Rectangle dest = 
                new Rectangle(Columns[0].Cells[rowIndex].X, Columns[0].Cells[rowIndex].Y, Columns[0].Cells[rowIndex].Width + Columns[1].Cells[rowIndex].Width - 1, Columns[c].Cells[rowIndex].Height);
            G.FillRectangle(new SolidBrush(rectColor), dest);
            if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, dest, 0, 0, dest.Width, dest.Height, GraphicsUnit.Pixel); }
            #endregion

            #region Draw Emblem



            //G.FillRectangle(new SolidBrush(rectColor), Columns[c].Cells[rowIndex]);
            //if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, dest, 0, 0, Columns[c].Cells[rowIndex].Width, Columns[c].Cells[rowIndex].Height, GraphicsUnit.Pixel); }

            //DrawScoreboardCell(Columns[c], rowIndex, "", rectColor, textColor);

            //G.DrawImage(EmblemDead, Columns[c].Cells[rowIndex].Location);
            //System.Drawing.Rectangle deadX = new System.Drawing.Rectangle(new Point(Columns[c].Position.X + 4, Columns[c].Cells[rowIndex].Y), new Size(isAliveIndicatorSize.Width, Columns[c].Cells[rowIndex].Height));

            if (currentConnection.State.InLobby || currentConnection.State.IsLoading)
            {
                if (player.Emblem != null)
                {
                    G.DrawImage(player.Emblem, Columns[c].CellContentOrigins[rowIndex]);
                }
                else
                {
                    G.DrawImage(EmblemGeneric, Columns[c].CellContentOrigins[rowIndex]);
                }
            }
            else if (!player.IsAlive)
            {
                G.DrawImage(EmblemDead, Columns[c].CellContentOrigins[rowIndex]);
            }
            else if (player.Emblem != null)
            {
                G.DrawImage(player.Emblem, Columns[c].CellContentOrigins[rowIndex]);
            }
            else
            {
                G.DrawImage(EmblemGeneric, Columns[c].CellContentOrigins[rowIndex]);
            }

            c++;

            #endregion

            #region Draw NAME
            
            //G.FillRectangle(new SolidBrush(rectColor), Columns[c].Cells[rowIndex]);            
            //dest = new Rectangle(Columns[c].Cells[rowIndex].X, Columns[c].Cells[rowIndex].Y, Columns[c].Cells[rowIndex].Width, Columns[c].Cells[rowIndex].Height);
            //if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, dest, 0, 0, Columns[c].Cells[rowIndex].Width, Columns[c].Cells[rowIndex].Height, GraphicsUnit.Pixel); }

            DrawString(Columns[c].Cells[rowIndex], Columns[c].CellContentOrigins[rowIndex], Columns[c].CellStringFormat, player.Name.Length > 0 ? player.Name : "Loading...", rectColor, textColor);

            //DrawScoreboardCell(Columns[c], rowIndex, player.Name.Length > 0 ? player.Name : "Loading...", rectColor, textColor);
            c++;
            #endregion

            #region Draw TAG
            DrawScoreboardCell(Columns[c], rowIndex, player.ScoreboardServiceTag ?? "", TagColor, textColor);
            c++;
            #endregion

            #region Draw SCORE
            DrawScoreboardCell(Columns[c], rowIndex, player.Score.ToString(), rectColor, textColor);
            c++;
            #endregion

            #region Draw KILLS
            DrawScoreboardCell(Columns[c], rowIndex, player.Kills.ToString(), rectColor, textColor);
            c++;
            #endregion

            #region Draw DEATHS
            DrawScoreboardCell(Columns[c], rowIndex, player.Deaths.ToString(), rectColor, textColor);
            c++;
            #endregion

            #region Draw K/D Ratio
            DrawScoreboardCell(Columns[c], rowIndex, player.ScoreboardKillDeathRatio, rectColor, (player.KillDeathRatio >= 6) ? Gold : textColor);
            c++;
            #endregion

            #region Draw ASSISTS
            DrawScoreboardCell(Columns[c], rowIndex, player.Assists.ToString(), rectColor, textColor);
            c++;
            #endregion

            #region Draw BETRAYALS
            DrawScoreboardCell(Columns[c], rowIndex, player.Betrayals.ToString(), rectColor, (player.Betrayals >= 1) ? Gold : textColor);
            c++;
            #endregion

            #region Draw SUICIDES
            DrawScoreboardCell(Columns[c], rowIndex, player.Suicides.ToString(), rectColor, textColor);
            c++;
            #endregion

            #region Draw BEST STREAK
            DrawScoreboardCell(Columns[c], rowIndex, player.BestStreak.ToString(), rectColor, textColor);
            c++;
            #endregion

            #region Draw IN GAME - previously TIME ALIVE
            DrawScoreboardCell(Columns[c], rowIndex, player.TimeConnected, rectColor, textColor);
            c++;
            #endregion


        }
        private static void DrawEmptyScoreboardRow(int rowIndex, Color rectColor)
        {
            int c = 0;

            #region Draw Emblem + Name Rect

            Rectangle dest =
                new Rectangle(Columns[0].Cells[rowIndex].X, Columns[0].Cells[rowIndex].Y, Columns[0].Cells[rowIndex].Width + Columns[1].Cells[rowIndex].Width - 1, Columns[c].Cells[rowIndex].Height);
            G.FillRectangle(new SolidBrush(rectColor), dest);
            if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, dest, 0, 0, dest.Width, dest.Height, GraphicsUnit.Pixel); }

            #endregion

            //#region Draw Emblem

            //G.FillRectangle(new SolidBrush(rectColor), Columns[c].Cells[rowIndex]);

            //Rectangle dest = new Rectangle(Columns[c].Cells[rowIndex].X, Columns[c].Cells[rowIndex].Y, Columns[c].Cells[rowIndex].Width - 1, Columns[c].Cells[rowIndex].Height);
            //if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, dest, 0, 0, Columns[c].Cells[rowIndex].Width, Columns[c].Cells[rowIndex].Height, GraphicsUnit.Pixel); }
            
            //c++;

            //#endregion

            //#region Draw NAME

            //G.FillRectangle(new SolidBrush(rectColor), Columns[c].Cells[rowIndex]);

            //dest = new Rectangle(Columns[c].Cells[rowIndex].X, Columns[c].Cells[rowIndex].Y, Columns[c].Cells[rowIndex].Width, Columns[c].Cells[rowIndex].Height);
            //if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, dest, 0, 0, Columns[c].Cells[rowIndex].Width, Columns[c].Cells[rowIndex].Height, GraphicsUnit.Pixel); }

            //c++;
            //#endregion

            SolidBrush rectBrush = new SolidBrush(rectColor);
            for (int i = 2; i < Columns.Count; i++)
            {
                G.FillRectangle(rectBrush, Columns[i].Cells[rowIndex]);
                if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, Columns[i].Cells[rowIndex], 0, 0, Columns[i].Cells[rowIndex].Width, Columns[i].Cells[rowIndex].Height, GraphicsUnit.Pixel); }
            }
            //// Draw NAME
            //G.FillRectangle(rectBrush, Columns[0].Cells[rowIndex]);
            //// Draw SCORE
            //G.FillRectangle(rectBrush, Columns[1].Cells[rowIndex]);
            //// Draw KILLS
            //G.FillRectangle(rectBrush, Columns[2].Cells[rowIndex]);
            //// Draw DEATHS
            //G.FillRectangle(rectBrush, Columns[3].Cells[rowIndex]);
            //// Draw K/D Ratio
            //G.FillRectangle(rectBrush, Columns[4].Cells[rowIndex]);
            //// Draw ASSISTS
            //G.FillRectangle(rectBrush, Columns[5].Cells[rowIndex]);
            //// Draw BETRAYALS
            //G.FillRectangle(rectBrush, Columns[6].Cells[rowIndex]);
            //// Draw SUICIDES
            //G.FillRectangle(rectBrush, Columns[7].Cells[rowIndex]);
            //// Draw TIME ALIVE
            //G.FillRectangle(rectBrush, Columns[8].Cells[rowIndex]);
            //// Draw BEST STREAK
            //G.FillRectangle(rectBrush, Columns[9].Cells[rowIndex]);
        }
        private static void DrawScoreboardCell(ScoreboardColumn column, int rowIndex, string text, Color rectColor, Color textColor)
        {
            if (rectColor != Color.Transparent)
            {   
                // Background rect
                G.FillRectangle(new SolidBrush(rectColor), column.Cells[rowIndex]);
                if (HighlightAllRows) { G.DrawImage(RowHighlightRectangle, column.Cells[rowIndex], 0, 0, column.Cells[rowIndex].Width, column.Cells[rowIndex].Height, GraphicsUnit.Pixel); }
            }
            DrawString(column.Cells[rowIndex], column.CellContentOrigins[rowIndex], column.CellStringFormat, text, rectColor, textColor);
        }

        /// <summary>
        /// Draw a string with an outline and an optional colored background rectangle.
        /// </summary>
        /// <param name="g">Graphics context.</param>
        /// <param name="rectangle">Location to draw string (will be drawn at rectangle's center).</param>
        /// <param name="text">Text to draw.</param>
        /// <param name="rectColor">Color of background rectangle. Use Color.Transparent for no background rectangle.</param>
        /// <param name="textColor">Text fill color.</param>
        /// <param name="font">Font to use.</param>
        private static void DrawString(Graphics g, Rectangle rectangle, Point stringOrigin, StringFormat stringFormat, string text, Color rectColor, Color textColor, Font font = null, Color? outlineColor = null)
        {

            //https://stackoverflow.com/questions/4200843/outline-text-with-system-drawing
            //https://stackoverflow.com/questions/25421219/draw-outline-around-string-without-artifacts-in-net

            if (font == null) { font = fontRegular; }
            
            fontEmSize = g.DpiY * font.Size / 72;

            // Path for outlining and filling string
            GraphicsPath p = new GraphicsPath();
            p.AddString(
                text,                       // text to draw
                font.FontFamily,            // font family
                (int)FontStyle.Regular,     // font style (bold, italic, etc.)
                fontEmSize,       // em size
                stringOrigin,      // location where to draw text
                stringFormat      // set options here (e.g. center alignment)
            );

            if (outlineColor != null) { outlinePen.Color = (Color)outlineColor; }

            // Text outline + fill
            g.DrawPath(outlinePen, p);
            g.FillPath(new SolidBrush(textColor), p);
            
            if (outlineColor != null) { outlinePen.Color = Color.Black; }

        }
        private static void DrawString(Rectangle rectangle, Point stringOrigin, StringFormat stringFormat, string text, Color rectColor, Color textColor, Font font = null, Color? outlineColor = null)
        {
            DrawString(G, rectangle, stringOrigin, stringFormat, text, rectColor, textColor, font, outlineColor);
        }
        

        /// <summary>
        /// Returns the size in pixels of the supplied text rendered with the supplied font.
        /// </summary>
        /// <param name="text">The text to calculate a size for.</param>
        /// <param name="graphics">The graphics context that will render the string.</param>
        /// <param name="font">The font that will be used to render the string.</param>
        /// <param name="margins">(Optional) The desired spacing pixels (margins) around the string that should be included in the calculation.</param>
        private static SizeF GetTextRenderSize(string text, Graphics graphics, Font font, StringFormat stringFormat, SizeF? margins = null)
        {

            fontEmSize = graphics.DpiY * font.Size / 72;

            // Path for outlining and filling string
            GraphicsPath p = new GraphicsPath();
            p.AddString(
                text,                       // text to draw
                font.FontFamily,            // font family
                (int)FontStyle.Regular,     // font style (bold, italic, etc.)
                fontEmSize,       // em size
                new Point(0,0),      // location where to draw text
                stringFormat      // set options here (e.g. center alignment)
            );

            if (margins == null)
            {
                //return graphics.MeasureString(text, font, 0, stringFormat);
                return p.GetBounds().Size;
            }
            else
            {
                //return graphics.MeasureString(text, font, 0, stringFormat) + ((SizeF)margins);
                return (p.GetBounds().Size + (SizeF)margins);
            }
        }
        /// <summary>
        /// Returns the size in pixels of text rendered with the supplied monospace font.<para>This method should only be used with monospaced fonts.</para>
        /// </summary>
        /// <param name="stringLength">The length (character count) of the string.</param>
        /// <param name="graphics">The graphics context that will render the string.</param>
        /// <param name="font">The font that will be used to render the string.</param>
        /// <param name="margins">(Optional) The desired spacing pixels (margins) around the string that should be included in the calculation.</param>
        /// <returns></returns>
        private static SizeF GetMonospaceTextRenderSize(int stringLength, Graphics graphics, Font font, StringFormat stringFormat, SizeF? margins = null)
        {
            return GetTextRenderSize(new string('x', stringLength), graphics, font, stringFormat, margins);
        }


        public class ScoreboardColumn
        {

            public ColumnID ID { get; set; }

            public string Name { get; set; }
            public int Width { get; set; }
            public Font HeaderFont { get; set; }
            public Image HeaderImage { get; set; }
            public Alignment ContentAlignment { get; set; }
            public Alignment HeaderAlignment { get; set; }
            public BorderType HeaderBorderType { get; set; }
            public BorderType CellBorderType { get; set; }
            public StringFormat HeaderStringFormat { get; set; }
            public StringFormat CellStringFormat { get; set; }

            public bool DrawHeader { get; set; } = true;
            private bool ImageColumn { get; set; } = false;

            public Point HeaderContentOrigin { get; set; }
            public Point[] CellContentOrigins { get; set; } = new Point[16];

            public Point Position {
                get { return position; }
                set 
                {
                    if (HeaderBorderType == BorderType.BlendLeft) {
                        value = new Point(Math.Max(value.X - 1, 0), value.Y);
                    }
                    position = value;
                    CalculatePositioning();
                }
            }
            private Point position;

            public Rectangle Header { get; set; } = new Rectangle();
            public Rectangle[] Cells { get; set; } = new Rectangle[16];

            public ScoreboardColumn(ColumnID id, string name, int width)
            {
                
                ID = id;

                Name = string.IsNullOrEmpty(name) ? "" : name;

                HeaderBorderType = BorderType.Standard;
                CellBorderType = BorderType.Standard;

                HeaderAlignment = Alignment.Center;
                ContentAlignment = Alignment.Center;

                HeaderStringFormat = Scoreboard.CenterJustifySF;
                CellStringFormat = Scoreboard.CenterJustifySF;

                HeaderFont = fontRegular;

                Width = width;



            }

            public ScoreboardColumn(ColumnID id, string name, int characterLimit, Alignment contentAlignment, bool useNameWidth = false, BorderType headerBorderType = BorderType.Standard, BorderType cellBorderType = BorderType.Standard, Alignment headerAlignment = Alignment.Center)
            {

                ID = id;

                Name = string.IsNullOrEmpty(name) ? "" : name;

                HeaderFont = fontRegular;

                HeaderBorderType = headerBorderType;
                CellBorderType = cellBorderType;

                HeaderAlignment = headerAlignment;
                switch (HeaderAlignment)
                {
                    case Alignment.Left: HeaderStringFormat = Scoreboard.LeftJustifySF; break;
                    case Alignment.Center: HeaderStringFormat = Scoreboard.CenterJustifySF; break;
                    case Alignment.Right: HeaderStringFormat = Scoreboard.RightJustifySF; break;
                    default: break;
                }

                ContentAlignment = contentAlignment;
                switch (ContentAlignment)
                {
                    case Alignment.Left: CellStringFormat = Scoreboard.LeftJustifySF; break;
                    case Alignment.Center: CellStringFormat = Scoreboard.CenterJustifySF; break;
                    case Alignment.Right: CellStringFormat = Scoreboard.RightJustifySF; break;
                    default: break;
                }
                
                #region Determine column width and header size

                
                if (characterLimit == -1)
                {
                    ImageColumn = true;
                    Width = rowHeight + ImageColumnMargins.Width;
                }
                else
                {

                    float nameWidth = GetMonospaceTextRenderSize(Name.Length, G, fontRegular, RightJustifySF, Margins).Width;
                    float charWidth = GetMonospaceTextRenderSize(characterLimit, G, fontRegular, RightJustifySF, Margins).Width;

                    Width = (int)charWidth;

                    if (useNameWidth) { Width = (int)nameWidth; }
                    else
                    {
                        float decrement = 0.5f;
                        while ((int)nameWidth > Width)
                        {
                            HeaderFont = FontUtility.GetMonospaceFont(DefaultScoreboardFont, HeaderFont.Size - decrement);
                            nameWidth = GetMonospaceTextRenderSize(Name.Length, G, HeaderFont, RightJustifySF, Margins).Width;
                            decrement += 0.5f;
                        }
                    }

                }

				#endregion

            }

            public void CalculatePositioning()
            {

                int widthOffset = HeaderBorderType == BorderType.Standard? 0 : 1;
                int cellX = HeaderBorderType == BorderType.BlendLeft ? Position.X + 1 : Position.X;
                //switch (HeaderBorderType)
                //{
                //    case BorderType.BlendLeft: cellX = Position.X + 1; widthOffset = 1; break;
                //    case BorderType.BlendRight: widthOffset = 1; break;
                //    default: break;
                //}

                Header = new Rectangle(Position.X, Position.Y, Width + widthOffset, Scoreboard.rowHeight);
                switch (HeaderAlignment)
                {
                    case Alignment.Left: HeaderContentOrigin = new Point(Header.X, Header.Y + Header.Height / 2); break;
                    case Alignment.Center: HeaderContentOrigin = Header.GetCenter(); break;
                    case Alignment.Right: HeaderContentOrigin = new Point(Header.X + Header.Width, Header.Y + Header.Height / 2); break;
                }

                if (ImageColumn)
                {

                    int cellXOffset = CellBorderType == BorderType.BlendLeft ? -1 : 0;
                    int cellWidthOffset = CellBorderType == BorderType.Standard ? 0 : 1;

                    switch (CellBorderType)
                    {
                        case BorderType.Standard: cellXOffset = 0; cellWidthOffset = 0; break;
                        case BorderType.BlendLeft: cellXOffset = -1; cellWidthOffset = 1; break;
                        case BorderType.BlendRight: cellXOffset = 0; cellWidthOffset = 1; break;
                        default: break;
                    }

                    for (int i = 0; i < 16; i++)
                    {
                        Cells[i] = new Rectangle(cellX + cellXOffset, (i * rowHeight), Width + cellWidthOffset, rowHeight);
                        CellContentOrigins[i] = new Point(
                            Cells[i].X + (ImageColumnMargins.Width / 2) + (ImageColumnPadding.Width / 2), 
                            Cells[i].Y + (ImageColumnPadding.Height / 2)
                        );
                    }
                }
                else
                {

                    int cellXOffset = 0, cellWidthOffset = 0;

                    switch (CellBorderType)
                    {
                        case BorderType.Standard: cellXOffset = 0; cellWidthOffset = 0; break;
                        case BorderType.BlendLeft: cellXOffset = -1; cellWidthOffset = 1; break;
                        case BorderType.BlendRight: cellXOffset = 0; cellWidthOffset = 1; break;
                        default: break;
                    }

                    for (int i = 0; i < 16; i++)
                    {

                        Cells[i] = new Rectangle(cellX + cellXOffset, (i * Scoreboard.rowHeight), Width + cellWidthOffset, Scoreboard.rowHeight);
                        switch (ContentAlignment)
                        {
                            case Alignment.Left: CellContentOrigins[i] = new Point(Cells[i].Left, Cells[i].Top + Cells[i].Height / 2); break;
                            case Alignment.Center: CellContentOrigins[i] = Cells[i].GetCenter(); break;
                            case Alignment.Right: CellContentOrigins[i] = new Point(Cells[i].Right, Cells[i].Top + Cells[i].Height / 2); break;
                        }
                    }

                }

            }

            /// <summary>
            /// Adjust the column's header so that it is drawn as though it also occupies the space of some other column header(s) to the left.
            /// </summary>
            /// <param name="count">How many columns to 'absorb'.</param>
            public void AbsorbHeadersLeft(int count, bool hideAbsorbedHeaders = true) { AbsorbHeaders(count, true, hideAbsorbedHeaders); }
            /// <summary>
            /// Adjust the column's header so that it is drawn as though it also occupies the space of some other column header(s) to the right.
            /// </summary>
            /// <param name="count">How many columns to 'absorb'.</param>
            public void AbsorbHeadersRight(int count, bool hideAbsorbedHeaders = true) { AbsorbHeaders(count, false, hideAbsorbedHeaders); }
            private void AbsorbHeaders(int count, bool left, bool hideAbsorbedHeaders = true)
            {
                
                int i = Columns.IndexOf(this);
                if (i == -1) { throw new Exception("Column not found in Scoreboard columns list."); }

                int bumpIndex = (left) ? i - count: i + count;
                if (bumpIndex < 0 || bumpIndex > Columns.Count - 1) { throw new Exception("Invalid column absorb count or position."); }

                int bump = (left) 
                    ? Header.X - Columns[bumpIndex].Header.X 
                    : Columns[bumpIndex].Header.X + Columns[bumpIndex].Header.Width - Header.X - Header.Width
                ;

                Header = (left)
                    ? new Rectangle(Math.Max(0, Header.X - bump), Header.Y, Header.Width + bump, Header.Height)
                    : new Rectangle(Header.X, Header.Y, Header.Width + bump, Header.Height)
                ;

                switch (HeaderAlignment)
                {
                    case Alignment.Left: HeaderContentOrigin = new Point(Header.X, Header.Y + Header.Height / 2); break;
                    case Alignment.Center: HeaderContentOrigin = Header.GetCenter(); break;
                    case Alignment.Right: HeaderContentOrigin = new Point(Header.X + Header.Width, Header.Y + Header.Height / 2); break;
                }

                if (hideAbsorbedHeaders)
                {
                    if (left) { for (int x = i - 1; x >= (i - count); x--) { Columns[x].DrawHeader = false; } }
                    else {      for (int x = i + 1; x <= (i + count); x++) { Columns[x].DrawHeader = false; } }
                }

            }

            public void BumpHeaderLeft(int amount)   { BumpHeaderOrContent(true, true, amount);   }
            public void BumpHeaderRight(int amount)  { BumpHeaderOrContent(true, false, amount);  }
            public void BumpContentLeft(int amount)  { BumpHeaderOrContent(false, true, amount);  }
            public void BumpContentRight(int amount) { BumpHeaderOrContent(false, false, amount); }
            private void BumpHeaderOrContent(bool header, bool left, int amount)
            {
                if (header)
                {
                    HeaderContentOrigin = new Point(HeaderContentOrigin.X + ((left) ? -1 * amount : amount), HeaderContentOrigin.Y);
                    if (HeaderContentOrigin.X < 0) { HeaderContentOrigin = new Point(0, HeaderContentOrigin.Y); }
                }
                else
                {
                    for (int i = 0; i < CellContentOrigins.Length; i++)
                    {
                        CellContentOrigins[i] = new Point(CellContentOrigins[i].X + ((left) ? -1 * amount : amount), CellContentOrigins[i].Y);
                        if (CellContentOrigins[i].X < 0) { CellContentOrigins[i] = new Point(0, CellContentOrigins[i].Y); }
                    }
                }
            }

        }

        public enum BorderType
        {
            Standard,
            BlendLeft,
            BlendRight
        }
        public enum Alignment
        {
            Left,
            Center,
            Right
        }
        public enum ColumnID
        {
            Emblem,
            Name,
            Tag,
            Score,
            Kills,
            Deaths,
            KD,
            Assists,
            Betrayals,
            Suicides,
            BestStreak,
            TimeInGame
        }

    }
}
