using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RconTool.Utility
{
	
	public static class EmblemGenerator
	{

		public static readonly Color[] ColorValues = new Color[30] {
			ColorTranslator.FromHtml("#585758"), // "Steel"
			ColorTranslator.FromHtml("#a7a6a7"), // "Silver"
			ColorTranslator.FromHtml("#d7d8d7"), // "White"
			ColorTranslator.FromHtml("#893139"), // "Red"
			ColorTranslator.FromHtml("#d26163"), // "Mauve"
			ColorTranslator.FromHtml("#ea7d7d"), // "Salmon"
			ColorTranslator.FromHtml("#d28800"), // "Orange"
			ColorTranslator.FromHtml("#f6a34b"), // "Coral"
			ColorTranslator.FromHtml("#fbbe93"), // "Peach"
			ColorTranslator.FromHtml("#bfa12a"), // "Gold"
			ColorTranslator.FromHtml("#e2b02a"), // "Yellow"
			ColorTranslator.FromHtml("#fbd375"), // "Pale"
			ColorTranslator.FromHtml("#4b702b"), // "Sage"
			ColorTranslator.FromHtml("#839252"), // "Green"
			ColorTranslator.FromHtml("#cdea93"), // "Olive"
			ColorTranslator.FromHtml("#2b7475"), // "Teal"
			ColorTranslator.FromHtml("#4bafb2"), // "Aqua"
			ColorTranslator.FromHtml("#8ce8e5"), // "Cyan"
			ColorTranslator.FromHtml("#3d518c"), // "Blue"
			ColorTranslator.FromHtml("#4b84d2"), // "Cobalt"
			ColorTranslator.FromHtml("#93a9f3"), // "Sapphire"
			ColorTranslator.FromHtml("#4b3d8c"), // "Violet"
			ColorTranslator.FromHtml("#8c70e1"), // "Orchid"
			ColorTranslator.FromHtml("#bdacfb"), // "Lavender"
			ColorTranslator.FromHtml("#7d003d"), // "Crimson"
			ColorTranslator.FromHtml("#c83d7d"), // "Rubine"
			ColorTranslator.FromHtml("#fb88ad"), // "Pink"
			ColorTranslator.FromHtml("#4b352b"), // "Brown"
			ColorTranslator.FromHtml("#a0876b"), // "Tan"
			ColorTranslator.FromHtml("#d7af9b"), // "Khaki"
		};

		public static readonly string[] ColorNames = new string[30] {
			"Steel",    // "#585758"
			"Silver",   // "#a7a6a7"
			"White",    // "#d7d8d7"
			"Red",      // "#893139"
			"Mauve",    // "#d26163"
			"Salmon",   // "#ea7d7d"
			"Orange",   // "#d28800"
			"Coral",    // "#f6a34b"
			"Peach",    // "#fbbe93"
			"Gold",     // "#bfa12a"
			"Yellow",   // "#e2b02a"
			"Pale",     // "#fbd375"
			"Sage",     // "#4b702b"
			"Green",    // "#839252"
			"Olive",    // "#cdea93"
			"Teal",     // "#2b7475"
			"Aqua",     // "#4bafb2"
			"Cyan",     // "#8ce8e5"
			"Blue",     // "#3d518c"
			"Cobalt",   // "#8c70e1"
			"Sapphire", // "#bdacfb"
			"Violet",   // "#4b3d8c"
			"Orchid",   // "#8c70e1"
			"Lavender", // "#bdacfb"
			"Crimson",  // "#7d003d"
			"Rubine",   // "#c83d7d"
			"Pink",     // "#fb88ad"
			"Brown",    // "#4b352b"
			"Tan",      // "#a0876b"
			"Khaki",    // "#d7af9b"
		};

		private static readonly Bitmap[] foregrounds = new Bitmap[] {
			Properties.Resources.emblem_foregrounds_000,
			Properties.Resources.emblem_foregrounds_001,
			Properties.Resources.emblem_foregrounds_002,
			Properties.Resources.emblem_foregrounds_003,
			Properties.Resources.emblem_foregrounds_004,
			Properties.Resources.emblem_foregrounds_005,
			Properties.Resources.emblem_foregrounds_006,
			Properties.Resources.emblem_foregrounds_007,
			Properties.Resources.emblem_foregrounds_008,
			Properties.Resources.emblem_foregrounds_009,
			Properties.Resources.emblem_foregrounds_010,
			Properties.Resources.emblem_foregrounds_011,
			Properties.Resources.emblem_foregrounds_012,
			Properties.Resources.emblem_foregrounds_013,
			Properties.Resources.emblem_foregrounds_014,
			Properties.Resources.emblem_foregrounds_015,
			Properties.Resources.emblem_foregrounds_016,
			Properties.Resources.emblem_foregrounds_017,
			Properties.Resources.emblem_foregrounds_018,
			Properties.Resources.emblem_foregrounds_019,
			Properties.Resources.emblem_foregrounds_020,
			Properties.Resources.emblem_foregrounds_021,
			Properties.Resources.emblem_foregrounds_022,
			Properties.Resources.emblem_foregrounds_023,
			Properties.Resources.emblem_foregrounds_024,
			Properties.Resources.emblem_foregrounds_025,
			Properties.Resources.emblem_foregrounds_026,
			Properties.Resources.emblem_foregrounds_027,
			Properties.Resources.emblem_foregrounds_028,
			Properties.Resources.emblem_foregrounds_029,
			Properties.Resources.emblem_foregrounds_030,
			Properties.Resources.emblem_foregrounds_031,
			Properties.Resources.emblem_foregrounds_032,
			Properties.Resources.emblem_foregrounds_033,
			Properties.Resources.emblem_foregrounds_034,
			Properties.Resources.emblem_foregrounds_035,
			Properties.Resources.emblem_foregrounds_036,
			Properties.Resources.emblem_foregrounds_037,
			Properties.Resources.emblem_foregrounds_038,
			Properties.Resources.emblem_foregrounds_039,
			Properties.Resources.emblem_foregrounds_040,
			Properties.Resources.emblem_foregrounds_041,
			Properties.Resources.emblem_foregrounds_042,
			Properties.Resources.emblem_foregrounds_043,
			Properties.Resources.emblem_foregrounds_044,
			Properties.Resources.emblem_foregrounds_045,
			Properties.Resources.emblem_foregrounds_046,
			Properties.Resources.emblem_foregrounds_047,
			Properties.Resources.emblem_foregrounds_048,
			Properties.Resources.emblem_foregrounds_049,
			Properties.Resources.emblem_foregrounds_050,
			Properties.Resources.emblem_foregrounds_051,
			Properties.Resources.emblem_foregrounds_052,
			Properties.Resources.emblem_foregrounds_053,
			Properties.Resources.emblem_foregrounds_054,
			Properties.Resources.emblem_foregrounds_055,
			Properties.Resources.emblem_foregrounds_056,
			Properties.Resources.emblem_foregrounds_057,
			Properties.Resources.emblem_foregrounds_058,
			Properties.Resources.emblem_foregrounds_059,
			Properties.Resources.emblem_foregrounds_060,
			Properties.Resources.emblem_foregrounds_061,
			Properties.Resources.emblem_foregrounds_062,
			Properties.Resources.emblem_foregrounds_063,
			Properties.Resources.emblem_foregrounds_064,
			Properties.Resources.emblem_foregrounds_065,
			Properties.Resources.emblem_foregrounds_066,
			Properties.Resources.emblem_foregrounds_067,
			Properties.Resources.emblem_foregrounds_068,
			Properties.Resources.emblem_foregrounds_069,
			Properties.Resources.emblem_foregrounds_070,
			Properties.Resources.emblem_foregrounds_071,
			Properties.Resources.emblem_foregrounds_072,
			Properties.Resources.emblem_foregrounds_073,
			Properties.Resources.emblem_foregrounds_074,
			Properties.Resources.emblem_foregrounds_075,
			Properties.Resources.emblem_foregrounds_076,
			Properties.Resources.emblem_foregrounds_077,
			Properties.Resources.emblem_foregrounds_078,
			Properties.Resources.emblem_foregrounds_079,
			Properties.Resources.emblem_foregrounds_080,
			Properties.Resources.emblem_foregrounds_081,
			Properties.Resources.emblem_foregrounds_082,
			Properties.Resources.emblem_foregrounds_083,
			Properties.Resources.emblem_foregrounds_084,
			Properties.Resources.emblem_foregrounds_085,
			Properties.Resources.emblem_foregrounds_086,
			Properties.Resources.emblem_foregrounds_087,
			Properties.Resources.emblem_foregrounds_088,
			Properties.Resources.emblem_foregrounds_089,
			Properties.Resources.emblem_foregrounds_090,
			Properties.Resources.emblem_foregrounds_091,
			Properties.Resources.emblem_foregrounds_092,
			Properties.Resources.emblem_foregrounds_093,
			Properties.Resources.emblem_foregrounds_094,
			Properties.Resources.emblem_foregrounds_095,
			Properties.Resources.emblem_foregrounds_096,
			Properties.Resources.emblem_foregrounds_097,
			Properties.Resources.emblem_foregrounds_098,
			Properties.Resources.emblem_foregrounds_099,
			Properties.Resources.emblem_foregrounds_100,
			Properties.Resources.emblem_foregrounds_101,
			Properties.Resources.emblem_foregrounds_102,
			Properties.Resources.emblem_foregrounds_103,
			Properties.Resources.emblem_foregrounds_104,
			Properties.Resources.emblem_foregrounds_105,
			Properties.Resources.emblem_foregrounds_106,
			Properties.Resources.emblem_foregrounds_107,
			Properties.Resources.emblem_foregrounds_108,
			Properties.Resources.emblem_foregrounds_109,
			Properties.Resources.emblem_foregrounds_110,
			Properties.Resources.emblem_foregrounds_111,
			Properties.Resources.emblem_foregrounds_112,
			Properties.Resources.emblem_foregrounds_113,
			Properties.Resources.emblem_foregrounds_114,
			Properties.Resources.emblem_foregrounds_115,
			Properties.Resources.emblem_foregrounds_116,
			Properties.Resources.emblem_foregrounds_117,
			Properties.Resources.emblem_foregrounds_118,
			Properties.Resources.emblem_foregrounds_119,
			Properties.Resources.emblem_foregrounds_120,
			Properties.Resources.emblem_foregrounds_121,
			Properties.Resources.emblem_foregrounds_122,
			Properties.Resources.emblem_foregrounds_123,
			Properties.Resources.emblem_foregrounds_124,
			Properties.Resources.emblem_foregrounds_125,
			Properties.Resources.emblem_foregrounds_126,
			Properties.Resources.emblem_foregrounds_127,
			Properties.Resources.emblem_foregrounds_128,
			Properties.Resources.emblem_foregrounds_129,
			Properties.Resources.emblem_foregrounds_130,
			Properties.Resources.emblem_foregrounds_131,
			Properties.Resources.emblem_foregrounds_132,
			Properties.Resources.emblem_foregrounds_133,
			Properties.Resources.emblem_foregrounds_134,
			Properties.Resources.emblem_foregrounds_135,
			Properties.Resources.emblem_foregrounds_136,
			Properties.Resources.emblem_foregrounds_137,
			Properties.Resources.emblem_foregrounds_138,
			Properties.Resources.emblem_foregrounds_139,
			Properties.Resources.emblem_foregrounds_140,
			Properties.Resources.emblem_foregrounds_141,
			Properties.Resources.emblem_foregrounds_142,
			Properties.Resources.emblem_foregrounds_143,
			Properties.Resources.emblem_foregrounds_144,
			Properties.Resources.emblem_foregrounds_145,
			Properties.Resources.emblem_foregrounds_146,
			Properties.Resources.emblem_foregrounds_147,
			Properties.Resources.emblem_foregrounds_148,
			Properties.Resources.emblem_foregrounds_149,
			Properties.Resources.emblem_foregrounds_150,
			Properties.Resources.emblem_foregrounds_151,
			Properties.Resources.emblem_foregrounds_152,
			Properties.Resources.emblem_foregrounds_153,
			Properties.Resources.emblem_foregrounds_154,
			Properties.Resources.emblem_foregrounds_155,
			Properties.Resources.emblem_foregrounds_156,
			Properties.Resources.emblem_foregrounds_157,
			Properties.Resources.emblem_foregrounds_158,
			Properties.Resources.emblem_foregrounds_159,
			Properties.Resources.emblem_foregrounds_160,
			Properties.Resources.emblem_foregrounds_161,
			Properties.Resources.emblem_foregrounds_162,
			Properties.Resources.emblem_foregrounds_163,
			Properties.Resources.emblem_foregrounds_164,
			Properties.Resources.emblem_foregrounds_165,
			Properties.Resources.emblem_foregrounds_166,
			Properties.Resources.emblem_foregrounds_167,
			Properties.Resources.emblem_foregrounds_168,
			Properties.Resources.emblem_foregrounds_169,
			Properties.Resources.emblem_foregrounds_170,
			Properties.Resources.emblem_foregrounds_171,
			Properties.Resources.emblem_foregrounds_172,
			Properties.Resources.emblem_foregrounds_173,
			Properties.Resources.emblem_foregrounds_174,
			Properties.Resources.emblem_foregrounds_175,
			Properties.Resources.emblem_foregrounds_176,
			Properties.Resources.emblem_foregrounds_177,
			Properties.Resources.emblem_foregrounds_178,
			Properties.Resources.emblem_foregrounds_179,
			Properties.Resources.emblem_foregrounds_180,
			Properties.Resources.emblem_foregrounds_181,
			Properties.Resources.emblem_foregrounds_182,
			Properties.Resources.emblem_foregrounds_183,
			Properties.Resources.emblem_foregrounds_184,
			Properties.Resources.emblem_foregrounds_185,
		};

		private static readonly Bitmap[] backgrounds = new Bitmap[] {
			Properties.Resources.emblem_backgrounds_00,
			Properties.Resources.emblem_backgrounds_01,
			Properties.Resources.emblem_backgrounds_02,
			Properties.Resources.emblem_backgrounds_03,
			Properties.Resources.emblem_backgrounds_04,
			Properties.Resources.emblem_backgrounds_05,
			Properties.Resources.emblem_backgrounds_06,
			Properties.Resources.emblem_backgrounds_07,
			Properties.Resources.emblem_backgrounds_08,
			Properties.Resources.emblem_backgrounds_09,
			Properties.Resources.emblem_backgrounds_10,
			Properties.Resources.emblem_backgrounds_11,
			Properties.Resources.emblem_backgrounds_12,
			Properties.Resources.emblem_backgrounds_13,
			Properties.Resources.emblem_backgrounds_14,
			Properties.Resources.emblem_backgrounds_15,
			Properties.Resources.emblem_backgrounds_16,
			Properties.Resources.emblem_backgrounds_17,
			Properties.Resources.emblem_backgrounds_18,
			Properties.Resources.emblem_backgrounds_19,
			Properties.Resources.emblem_backgrounds_20,
			Properties.Resources.emblem_backgrounds_21,
			Properties.Resources.emblem_backgrounds_22,
			Properties.Resources.emblem_backgrounds_23,
			Properties.Resources.emblem_backgrounds_24,
			Properties.Resources.emblem_backgrounds_25,
			Properties.Resources.emblem_backgrounds_26,
			Properties.Resources.emblem_backgrounds_27,
			Properties.Resources.emblem_backgrounds_28,
			Properties.Resources.emblem_backgrounds_29,
			Properties.Resources.emblem_backgrounds_30,
			Properties.Resources.emblem_backgrounds_31,
			Properties.Resources.emblem_backgrounds_32,
			Properties.Resources.emblem_backgrounds_33,
			Properties.Resources.emblem_backgrounds_34,
			Properties.Resources.emblem_backgrounds_35,
			Properties.Resources.emblem_backgrounds_36,
			Properties.Resources.emblem_backgrounds_37,
			Properties.Resources.emblem_backgrounds_38,
			Properties.Resources.emblem_backgrounds_39,
			Properties.Resources.emblem_backgrounds_40,
			Properties.Resources.emblem_backgrounds_41,
			Properties.Resources.emblem_backgrounds_42,
			Properties.Resources.emblem_backgrounds_43,
			Properties.Resources.emblem_backgrounds_44,
			Properties.Resources.emblem_backgrounds_45,
			Properties.Resources.emblem_backgrounds_46,
			Properties.Resources.emblem_backgrounds_47,
			Properties.Resources.emblem_backgrounds_48,
			Properties.Resources.emblem_backgrounds_49,
			Properties.Resources.emblem_backgrounds_50,
			Properties.Resources.emblem_backgrounds_51,
			Properties.Resources.emblem_backgrounds_52,
		};

		private static readonly char[] k_AndArray = new char[] { '&' };

		//private const string k_EmblemUrl = @"https://api.eldewrito.org/emblem/emblem.php?";
		private const string k_EmblemUrl = @"?";

		// https://api.eldewrito.org/emblem/emblem.php?1=ffffff&2=6&3=16&fi=89&bi=50&fl=1
		// "?1=6&2=6&3=16&fi=89&bi=50&fl=1"
		//
		// First parameter (1) is the "background color" - integer or hex - max green channel value 101
		// Second parameter (2) is the "secondary color" - integer or hex - max blue channel value 236
		// Third parameter (3) is the "primary color" - integer or hex - max red channel value 177
		//
		// Fourth parameter ("fi") is the foreground index - integer index 0.png-185.png
		// 	dew://assets/emblems/player/preview/foreground
		// Fifth parameter ("bi") is the background index - integer index 0.png-52.png
		// 	dew://assets/emblems/player/preview/background
		// Sixth parameter ("fl") is a bool (0,1) and toggles the secondary icon element
		//
		// secondary icon element (toggleable part) is this blue shade, "#92ecec"

		private enum ColorPolicy { KeepEven, KeepBlue }

		private struct EmblemData
		{
			public Color BackgroundColor;
			public Color SecondaryColor;
			public Color PrimaryColor;
			public int ForegroundIndex;
			public int BackgroundIndex;
			public bool ToggleSecondary;
		}

		private static EmblemData ExtractEmblemData(string emblem) {
			emblem = emblem.Replace(k_EmblemUrl, "");
			EmblemData data = new EmblemData();
			string[] parts = emblem.Split(k_AndArray, StringSplitOptions.RemoveEmptyEntries);
			foreach (string part in parts) {
				string[] pair = part.Split('=');
				if (pair.Length != 2) continue;
				switch (pair[0]) {
					case "1":
						data.BackgroundColor = GetEmblemColor(pair[1]);
						break;
					case "2":
						data.SecondaryColor = GetEmblemColor(pair[1]);
						break;
					case "3":
						data.PrimaryColor = GetEmblemColor(pair[1]);
						break;
					case "fi":
						if (int.TryParse(pair[1], out int fi)) { data.ForegroundIndex = fi; }
						else { data.ForegroundIndex = 0; }
						break;
					case "bi":
						if (int.TryParse(pair[1], out int bi)) { data.BackgroundIndex = bi; }
						else { data.BackgroundIndex = 0; }
						break;
					case "fl":
						if (int.TryParse(pair[1], out int fl)) { data.ToggleSecondary = fl == 1; }
						else { data.ToggleSecondary = false; }
						break;
				}
			}
			return data;
		}

		private static Color GetEmblemColor(string colorValue) {
			if (string.IsNullOrWhiteSpace(colorValue)) { return ColorValues[0]; }
			if (colorValue.Length == 6) {
				// check if it's a valid hex string
				if (Regex.IsMatch(colorValue, @"^[0-9a-fA-F]+$")) {
					return ColorTranslator.FromHtml($"#{colorValue}");
				}
			}
			else if (int.TryParse(colorValue, out int colorIndex)) {
				if (colorIndex >= 0 && colorIndex < ColorValues.Length) {
					return ColorValues[colorIndex];
				}
			}
			return ColorValues[0];
		}

		public static Bitmap GenerateEmblem(string emblem) {
			EmblemData emblemData = ExtractEmblemData(emblem);

			Bitmap emblemBitmap = new Bitmap(128, 128);
			Bitmap scaledEmblem = new Bitmap(64, 64);
			using (Graphics g = Graphics.FromImage(emblemBitmap)) {
				// start with a blank canvas
				g.Clear(Color.Transparent);
				// draw background
				if (emblemData.BackgroundIndex != 0) {
					using (Bitmap bg = new Bitmap(backgrounds[emblemData.BackgroundIndex])) {
						ColorPixels(bg, g, emblemData.BackgroundColor, ColorPolicy.KeepEven, 101);
					}
				}
				// draw foreground
				using (Bitmap fg = new Bitmap(foregrounds[emblemData.ForegroundIndex])) {
					// draw primary emblem element
					ColorPixels(fg, g, emblemData.PrimaryColor, ColorPolicy.KeepEven, 177);
				}
				if (emblemData.ToggleSecondary) {
					using (Bitmap fg = new Bitmap(foregrounds[emblemData.ForegroundIndex])) {
						// draw secondary emblem element
						ColorPixels(fg, g, emblemData.SecondaryColor, ColorPolicy.KeepBlue, 236);
					}
				}

			}
			// downscale it to 32x32
			using (Graphics sg = Graphics.FromImage(scaledEmblem)) {
				sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				sg.DrawImage(emblemBitmap, 0, 0, 28, 28);
			}
			emblemBitmap.Dispose();
			return scaledEmblem;
		}

		private static void ColorPixels(Bitmap source, Graphics bmpGraphics, Color userColor, ColorPolicy sampling, float maxChannelValue, bool alphaBlend = true) {

			// each Bitmap is a 128x128 .png image with 32-bit RGBA pixel format
			// sample the specified ColorChannel of each pixel from the source image
			// use the maxChannelValue as a multiplier for the userColor value
			// fill in each pixel of the bmpGraphics based on the maxChannelValue, the alpha channel value, and the userColor value

			// Lock the bitmap's bits.
			Rectangle rect = new Rectangle(0, 0, source.Width, source.Height);
			System.Drawing.Imaging.BitmapData bmpData = 
				source.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, source.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * source.Height;
			byte[] rgbValues = new byte[bytes];

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			// Manipulate the bitmap
			for (int counter = 0; counter < rgbValues.Length; counter += 4) {
				// sample the specified channel
				float channelValue = 0;
				Color pixelColor = Color.FromArgb(rgbValues[counter + 3], rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]);
				bool blueDominant = (pixelColor.B - pixelColor.R > 10);
				bool evenColors = (pixelColor.B - pixelColor.R < 10) && (pixelColor.G - pixelColor.B < 10);
				switch (sampling) {
					case ColorPolicy.KeepEven:
						if (blueDominant) { channelValue = 0; }
						else { channelValue = pixelColor.R / maxChannelValue; }
							break;
					case ColorPolicy.KeepBlue:
						if (evenColors) { channelValue = 0; }
						else { channelValue = pixelColor.B / maxChannelValue; }
						break;
					default:
						break;
				}
				channelValue = Math.Min(1, channelValue);
				channelValue = Math.Max(0, channelValue);
				// apply the user color
				rgbValues[counter] = (byte)( userColor.B * channelValue );
				rgbValues[counter + 1] = (byte)( userColor.G * channelValue );
				rgbValues[counter + 2] = (byte)( userColor.R * channelValue );
				// apply the alpha channel
				if (alphaBlend) {
					rgbValues[counter + 3] = (byte)( 255 * channelValue );
				}
				else {
					rgbValues[counter + 3] = 255;
				}
			}

			// Copy the RGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			source.UnlockBits(bmpData);

			// Draw the modified image
			bmpGraphics.DrawImage(source, 0, 0);

			// Clean up
			source.Dispose();

		}

	}

}
