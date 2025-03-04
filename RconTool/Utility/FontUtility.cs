﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace RconTool
{

    //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.fontfamily.genericmonospace?view=netframework-4.8
    //https://stackoverflow.com/questions/2288246/how-can-i-bundle-a-font-with-my-net-winforms-application
    //https://www.dotnetperls.com/font

    // >>
    //https://web.archive.org/web/20120802033826/redwerb.com/post/Embedding-Fonts-in-your-Net-Application.aspx

    public class FontUtility
    {

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

        public static Font GetFont(CustomFont font, float emSize)
        {
            Font result;
            try { result = Create(font, emSize); }
            catch (Exception e)
            {
                App.Log("Failed to load custom scoreboard font: " + e.Message);
                result = new Font(FontFamily.GenericMonospace, emSize);
            }
            return result;
        }

        private static PrivateFontCollection sFonts;
        static FontUtility()
        {
            sFonts = new PrivateFontCollection();
            AddFont(Properties.Resources.Font_Cascadia);
            AddFont(Properties.Resources.Font_Consolas);
            AddFont(Properties.Resources.Font_Conduit);
            AddFont(Properties.Resources.Font_EnvyCodeR);
            AddFont(Properties.Resources.Font_Go);
            AddFont(Properties.Resources.Font_Inconsolata);
            AddFont(Properties.Resources.Font_LiberationMono);
            AddFont(Properties.Resources.Font_SourceCodeProMedium);
            AddFont(Properties.Resources.Font_UbuntuMonoBird);
        }
        private static void AddFont(byte[] font)
        {
            var buffer = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, buffer, font.Length);
            sFonts.AddMemoryFont(buffer, font.Length);
        }
        public static Font Create(
            CustomFont family,
            float emSize,
            FontStyle style = FontStyle.Regular,
            GraphicsUnit unit = GraphicsUnit.Pixel)
        {

            var fam = sFonts.Families[(int)family];
            return new Font(fam, emSize, style, unit);
        }
    }
    public enum CustomFont
    {
        Cascadia = 0,
        Conduit = 1,
        Consolas = 2,
        EnvyCodeR = 3,
        Go = 4,
        Inconsolata = 5,
        LiberationMono = 6,
        SourceCodeProMedium = 7,
        UbuntuMonoBird = 8,
    }

}