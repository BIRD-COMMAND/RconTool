using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RconTool
{
	public static class Extensions
	{

		#region String Extensions

		/// <summary>
		/// Returns the input string with any linebreaks converted to '\n' linebreak characters.
		/// <br>Note that this method will remove all multi-line linebreaks and replace them with single-line linebreaks.</br>
		/// <br>For example: "Line 1\r\n\nLine 3" will become "Line 1\nLine 3".</br>
		/// </summary>
		public static string StandardizeLineBreaks(this string input)
		{
			input = input.Replace(new char[] { '\r', '\v', '\f' }, '\n');
			while (input.Contains("\n\n")) { input = input.Replace("\n\n", "\n"); }
			return input;
		}

		/// <summary>
		/// Replaces any of the <paramref name="oldValues"/> characters with the <paramref name="newValue"/> character and returns the resulting string.
		/// </summary>
		/// <param name="oldValues">One or more characters that will be replaced with the <paramref name="newValue"/> character if they are present in the input string.</param>
		/// <param name="newValue">The character that will be used to replace any <paramref name="oldValues"/> characters found in the input string.</param>
		public static string Replace(this string input, IEnumerable<char> oldValues, char newValue)
		{
			
			if (string.IsNullOrWhiteSpace(input) || oldValues == null) { return input; }
			char[] matches = oldValues.ToArray();
			int matchesCount = matches.Length;
			if (matchesCount == 0) { return input; }
						
			int o;
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < input.Length; i++) {
				for (o = 0; o < matchesCount; o++) {
					if (input[i] == matches[o]) { sb.Append(newValue); break; }
				}
				sb.Append(input[i]);
			}

			return sb.ToString();

		}

		/// <summary>
		/// Removes all instances of the specified character from the string, and returns the resulting string.
		/// <br>If every character is removed from the string, an empty string will be returned.</br>
		/// </summary>
		/// <param name="c">The character to be purged from the string.</param>
		public static string RemoveAll(this string input, char c)
		{
			if (string.IsNullOrEmpty(input)) { return input; }
			StringBuilder sb = new StringBuilder();
			for (int i = 0, len = input.Length; i < len; i++) {
				if (input[i] != c) { sb.Append(input[i]); }
			}
			return sb.ToString();
		}

		/// <summary>
		/// Returns true if all characters in the string are digit characters. (char.IsDigit)
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsNumeric(this string input)
		{
			foreach (char c in input)
			{
				if (!char.IsDigit(c)) { return false; }
			}
			return true;
		}

		/// <summary>
		/// Returns true if the string has any line breaks
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsMultiLine(this string input)
		{
			if (string.IsNullOrEmpty(input)) { return false; }
			try { return input.Contains('\n'); }
			catch { return false; }
		}

		/// <summary>
		/// Returns the first character in the string. (string[0])
		/// </summary>
		public static char FirstChar(this string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new InvalidOperationException("Attempted to retrieve the first character in a null or empty string.");
			}
			return input[0];
		}

		/// <summary>
		/// Returns the last character in the string. (string[string.Length - 1])
		/// </summary>
		public static char LastChar(this string input)
		{
			if (string.IsNullOrEmpty(input)) { 
				throw new InvalidOperationException("Attempted to retrieve the last character in a null or empty string."); 
			}
			return input[input.Length - 1];
		}
		

		/// <summary>
		/// Returns the string with the first character removed. ( string.Remove(0,1) )
		/// </summary>
		public static string TrimFirstCharacter(this string input)
		{
			if (string.IsNullOrEmpty(input)) { return input; }
			else if (input.Length > 0) { return input.Remove(0, 1); }
			else { return input; }
		}

		/// <summary>
		/// Returns the string with the last character removed. ( string.Remove(string.Length - 1, 1) )
		/// </summary>
		public static string TrimLastCharacter(this string input)
		{
			if (string.IsNullOrEmpty(input)) { return input; }
			else { return input.TrimEnd(input[input.Length - 1]); }
		}

		/// <summary>
		/// Removes the first instance(s) of the specified <paramref name="character"/> from the string (if found) and returns the resulting string.
		/// <br>The <paramref name="occurences"/> parameter specifies the number of instances of the <paramref name="character"/> that will be searched for and removed if found.</br>
		/// <br>If the specified <paramref name="character"/> is not found in the string, the original string will be returned unmodified.</br>
		/// </summary>
		/// <param name="character">The character that will be removed from the string if found.</param>
		/// <param name="occurences">The number of instances of the character that will be removed from the string if found. 1 by default.</param>
		public static string RemoveFirst(this string str, char character, int occurences = 1)
		{
			if (string.IsNullOrWhiteSpace(str) || occurences < 1) { return str; }
			for (int o = 0, i; o < occurences && str.Length > 0; o++) {
				i = str.IndexOf(character);
				if (i > -1) { str = str.Remove(i, 1); }
			}
			return str;
		}

		/// <summary>
		/// Returns the string with <paramref name="count"/> characters removed from the beginning.
		/// </summary>
		public static string TrimStart(this string input, int count)
		{
			if (string.IsNullOrEmpty(input)) { return input; }
			else if (input.Length > count) { return input.Remove(0, count); }
			else { return ""; }
		}

		/// <summary>
		/// Returns the string with <paramref name="count"/> characters removed from the end.
		/// </summary>
		public static string TrimEnd(this string input, int count)
		{
			if (string.IsNullOrEmpty(input)) { return input; }
			else if (input.Length > count) { return input.Remove(input.Length - count, count); }
			else { return ""; }
		}

		/// <summary>
		/// Splits the string into a List of substrings, where each substring's maximum size is <paramref name="maxChunkSize"/>.
		/// <br>By default, each chunk is generated by removing a substring from the first remaining character to the last whitespace character before the <paramref name="maxChunkSize"/>.</br>
		/// <br>If no whitespace characters are found within the current <paramref name="maxChunkSize"/> then the entire maxChunkSize will be removed for the next chunk.</br>
		/// <para>If you set <paramref name="breakStringAtWhitespace"/> to false, every chunk will be split at the <paramref name="maxChunkSize"/>, with the final chunk containing all remaining characters.</para>
		/// </summary>
		/// <param name="maxChunkSize">The maximum number of characters each substring may contain.</param>
		/// <param name="breakStringAtWhitespace">True by default.
		/// <br>When set to true, chunks will be generated at the nearest whitespace character to the maxChunkSize.</br>
		/// <br>When set to false, every chunk will be split at the <paramref name="maxChunkSize"/>, with the final chunk containing all remaining characters.</br></param>
		/// <returns></returns>
		public static List<string> Split(this string str, int maxChunkSize, bool breakStringAtWhitespace = true)
		{
			
			if (maxChunkSize < 2) { throw new ArgumentException("maxChunkSize must be >= 2."); }

			if (str.Length <= maxChunkSize) { return new List<string>() { str }; }
			
			List<string> chunks = new List<string>();

			if (breakStringAtWhitespace) {
				
				int substringIndex;
				StringBuilder remaining = new StringBuilder(str);

				while (remaining.Length > maxChunkSize && remaining.Length > 0) {
					substringIndex = -1;
					for (int i = maxChunkSize - 1; i > -1; i--) {
						if (char.IsWhiteSpace(remaining[i])) { substringIndex = i; break; }
					}
					// found a space to break at
					if (substringIndex > -1) {
						//Add everything up to the space
						chunks.Add(remaining.ToString(0, substringIndex));
						//Remove everything up to and including the space
						remaining.Remove(0, substringIndex + 1);
					}
					// found no spaces to break at, will have to add the chunk in its entirety
					else {
						chunks.Add(remaining.ToString(0, maxChunkSize));
						remaining.Remove(0, maxChunkSize);
					}
				}

				if (remaining.Length > 0) { chunks.Add(remaining.ToString()); }

			}
			else {
				for (int i = 0; i < str.Length; i += maxChunkSize) {
					chunks.Add(str.Substring(i, Math.Min(maxChunkSize, str.Length - i)));
				}
			}

			return chunks;

		}

		/// <summary>
		/// Splits the string at all line breaks (Environment.NewLine) and returns the list of strings. By default, empty entries are discarded. Returns null if the operation fails.
		/// </summary>
		public static List<string> SplitOnLineBreaks(this string str, bool removeEmptyEntries = true)
		{
			if (string.IsNullOrEmpty(str)) { return null; }
			return removeEmptyEntries ? str.SplitOnLineBreaks(StringSplitOptions.RemoveEmptyEntries) : str.SplitOnLineBreaks(StringSplitOptions.None);
		}

		/// <summary>
		/// Splits the string at all line breaks (Environment.NewLine) and returns the list of strings. Returns null if the operation fails.
		/// </summary>
		/// <param name="options">StringSplitOptions: remove empty entries?</param>
		private static List<string> SplitOnLineBreaks(this string str, StringSplitOptions options)
		{
			try { return str.Split(Environment.NewLine.ToCharArray(), options).ToList(); }
			catch { return null; }
		}


		/// <summary>
		/// Returns true if the string is composed only of one or more instances of the characters in <paramref name="chars"/> array.
		/// <br>At least one of the characters in <paramref name="chars"/> must be present in the string at least once for this method to return true.</br>
		/// <br>If <paramref name="ignoreWhitespace"/> is true, this method will return true as long as the only non-<paramref name="chars"/> characters are whitespace.</br>
		/// </summary>
		/// <param name="ignoreWhitespace">Specifies whether whitespace characters should disqualify a match. If true, whitespace characters will be ignored and will not count towards determining string composition.</param>
		public static bool IsComposedOf(this string str, char[] chars, bool ignoreWhitespace = true)
		{
			
			// string must have at least one character
			if (string.IsNullOrEmpty(str))
			{
				throw new InvalidOperationException(
					"Tried to determine if a null or empty string was composed of certain characters. " 
					+ "An empty string, by definition, cannot be composed of anything. " 
					+ "This method can only be called on a string with a length greather than or equal to 1."
				);
			}

			// chars array must have at least one character
			if (chars == null || chars.Length == 0) { 
				throw new ArgumentException(
					"Tried to determine whether a string was composed of certain characters," 
					+ " but the array of characters passed was either null or empty - "
					+ "the 'chars' array passed to this method must contain at least one valid character."
				);
			}

			if (ignoreWhitespace)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (char.IsWhiteSpace(str[i])) { continue; }
					if (!chars.Contains(str[i])) { return false; }
				}
				return true;
			}
			else
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (!chars.Contains(str[i])) { return false; }
				}
				return true;
			}
		}

		/// <summary>
		/// Returns true if the string is comprised solely of english vowels (A,E,I,O,U), upper or lower case.
		/// </summary>
		/// <param name="ignoreWhitespace">If ignoreWhitespace is set to true, this method will return true even if the string includes whitespace or line breaks.</param>
		public static bool IsAllVowels_English(this string str, bool ignoreWhitespace = true)
		{
			
			// If string is null or whitespace, return false
			if (string.IsNullOrWhiteSpace(str)) { return false; }
			// If string is one character, return whether that character is a vowel
			if (str.Length == 1) { return str[0].IsVowel_English(); }

			if (ignoreWhitespace)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i].IsVowel_English() || char.IsWhiteSpace(str[i])) { continue; }
					else { return false; }
				}
				return true;
			}
			else
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i].IsVowel_English()) { continue; }
					else { return false; }
				}
				return true;
			}

		}

		/// <summary>
		/// Returns true if the string is comprised solely of english consonants
		/// </summary>
		/// <param name="ignoreWhitespace">If ignoreWhitespace is set to true, this method will return true even if the string includes whitespace or line breaks.</param>
		public static bool IsAllConsonants_English(this string str, bool ignoreWhitespace = true)
		{
			// If string is null or whitespace, return false
			if (string.IsNullOrWhiteSpace(str)) { return false; }
			// If string is one character, return whether that character is a vowel
			if (str.Length == 1) { return str[0].IsConsonant_English(); }

			if (ignoreWhitespace)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i].IsConsonant_English() || char.IsWhiteSpace(str[i])) { continue; }
					else { return false; }
				}
				return true;
			}
			else
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i].IsConsonant_English()) { continue; }
					else { return false; }
				}
				return true;
			}
		}

		/// <summary>
		/// Returns true if the string consists solely of one or more instances of any single non-whitespace character.
		/// </summary>
		/// <param name="ignoreWhitespace">If ignoreWhitespace is set to true, this method will return true if the string includes whitespace or line breaks, as long as there is otherwise only one non-whitespace character in or repeated throughout the string.</param>
		public static bool IsComposedOfOnlyOneTypeOfCharacter(this string str, bool ignoreWhitespace = true)
		{
			
			// If string is null or whitespace, return false
			if (string.IsNullOrWhiteSpace(str)) { return false; }

			// If string is one non-whitespace character, return true
			if (str.Length == 1) { return !char.IsWhiteSpace(str[0]); }

			if (ignoreWhitespace)
			{
				int i = 0;
				char toCheck = ' ';
				for (; i < str.Length; i++)
				{
					if (char.IsWhiteSpace(str[i])) { continue; }
					else { toCheck = str[i]; i++; break; }
				}
				if (toCheck == ' ') { return false; }

				for (; i < str.Length; i++)
				{
					if (str[i] != toCheck) { return false; }
				}
				return true;
			}

			else
			{
				char toCheck = ' ';
				if (char.IsWhiteSpace(str[0])) { return false; }
				else { toCheck = str[0]; }

				int i = 1;
				for (; i < str.Length; i++)
				{
					if (str[i] != toCheck) { return false; }
				}
				return true;
			}

		}

		/// <summary>
		/// Returns true if the character is an english vowel (A,E,I,O,U), either upper or lower case.
		/// </summary>
		public static bool IsVowel_English(this char chr)
		{
			return char.IsLetter(chr) && (chr == 'e' || chr == 'a' || chr == 'i' || chr == 'o' || chr == 'u'
				|| chr == 'E' || chr == 'A' || chr == 'I' || chr == 'O' || chr == 'U');
		}

		/// <summary>
		/// Returns true if the character is an english consonant (any letter that is not an english vowel (A,E,I,O,U)), either upper or lower case.
		/// </summary>
		public static bool IsConsonant_English(this char chr)
		{
			return char.IsLetter(chr) && !chr.IsVowel_English();
		}

		/// <summary>
		/// Attempts to format the string as properly indented JSON.
		/// <br>Returns the original string if the operation fails for any reason.</br>
		/// </summary>
		public static string FormatJson(this string str)
		{
			if (string.IsNullOrWhiteSpace(str)) { return str; }
			try {
				return Newtonsoft.Json.Linq.JToken.Parse(str).ToString(Newtonsoft.Json.Formatting.Indented);
				//dynamic parsedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(str);
				//return Newtonsoft.Json.JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
			}
			catch { return str; }
		}

		#endregion

		/// <summary>
		/// Returns the center point of the given rectangle. Integers are used, so results can be imprecise compared to decimal values.
		/// </summary>
		public static System.Drawing.Point GetCenter(this System.Drawing.Rectangle rect) 
		{
			return new System.Drawing.Point((int)(rect.X + rect.Width / 2f), (int)(rect.Y + rect.Height / 2f));
		}

		#region IntPtr Extensions

		//WPARAM - A message parameter.
		//	This type is declared in WinDef.h as follows:
		//	typedef UINT_PTR WPARAM;

		//LPARAM - A message parameter.
		//	This type is declared in WinDef.h as follows:
		//	typedef LONG_PTR LPARAM;
		public static int LoWord (this IntPtr intPtr)
		{
			return unchecked((short)(long)intPtr);			
		}
		public static int HiWord(this IntPtr intPtr)
		{
			return unchecked((short)((long)intPtr >> 16));
		}

		#endregion

		#region TextBoxBase Extensions

		/// <summary>
		/// Moves the input caret to the end of the TextBox's current text (no text is actually selected).
		/// </summary>
		public static void SelectEnd(this System.Windows.Forms.TextBoxBase textBox)
		{
			textBox.Select(textBox.Text?.Length ?? 0, 0);
		}

		#endregion

		#region ToolStripItem Extensions

		/// <summary>
		/// Casts this ToolStripItem to ToolStripMenuItem and returns its DropDownItems. If that operation fails for any reason, the returned value will be null.
		/// </summary>
		public static ToolStripItemCollection DropDownItems(this ToolStripItem item)
		{
			try { return (item as ToolStripMenuItem).DropDownItems; }
			catch { return null; }
		}

		/// <summary>
		/// Casts this ToolStripItem to ToolStripMenuItem and returns the value of its Checked property. If that operation fails for any reason, the returned value will be false.
		/// </summary>
		public static bool Checked(this ToolStripItem item)
		{
			try { return (item as ToolStripMenuItem).Checked; }
			catch { return false; }
		}

		/// <summary>
		/// Casts this ToolStripItem to ToolStripMenuItem and sets the value of its Checked property.
		/// <br>If the Checked property is successfully set, this method returns true. If the operation fails for any reason, this method will return false.</br>
		/// </summary>
		public static bool Checked(this ToolStripItem item, bool newCheckedValue)
		{
			try { (item as ToolStripMenuItem).Checked = newCheckedValue; return true; }
			catch { return false; }
		}

		#endregion

		#region ToolStripItemCollection Extensions

		/// <summary>
		/// Returns the last item in the ToolStripItemCollection as a ToolStripMenuItem. Returns null if the collection is empty.
		/// </summary>
		public static ToolStripMenuItem LastMenuItem(this ToolStripItemCollection items)
		{
			try { return items[items.Count - 1] as ToolStripMenuItem; }
			catch { return null; }
		}

		/// <summary>
		/// Returns the last item in the ToolStripItemCollection. Returns null if the collection is empty.
		/// </summary>
		public static ToolStripItem LastItem(this ToolStripItemCollection items)
		{
			try { return items[items.Count - 1]; }
			catch { return null; }
		}

		#endregion

	}
}
