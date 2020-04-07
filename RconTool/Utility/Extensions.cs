using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	public static class Extensions
	{

		#region String Extensions

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
		/// Returns the string with <paramref name="count"/> characters removed from the beginning.
		/// </summary>
		public static string TrimStart(this string input, int count)
		{
			if (string.IsNullOrEmpty(input)) { return input; }
			else if (input.Length > count) { return input.Remove(0, 1); }
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

		#endregion

		/// <summary>
		/// Returns the center point of the given rectangle. Integers are used, so results can be imprecise compared to decimal values.
		/// </summary>
		public static System.Drawing.Point GetCenter(this System.Drawing.Rectangle rect) 
		{
			return new System.Drawing.Point((int)(rect.X + rect.Width / 2f), (int)(rect.Y + rect.Height / 2f));
		}

	}
}
