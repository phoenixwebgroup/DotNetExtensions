namespace BclExtensionMethods
{
	using System;
	using System.Linq;

	public static class StringExtensions
	{
		/// <summary>
		/// 	Truncates the string to a specified length and replace the truncated to a ...
		/// </summary>
		public static string Truncate(this string text, int maxLength)
		{
			const string suffix = "...";
			if (maxLength <= 0 || string.IsNullOrEmpty(text))
			{
				return text;
			}

			var truncatedStringLength = maxLength - suffix.Length;
			if (truncatedStringLength <= 0 || text.Length <= maxLength)
			{
				return text;
			}

			var truncatedString = text.Substring(0, truncatedStringLength).TrimEnd();
			return truncatedString + suffix;
		}

		public static bool IsNullOrEmpty(this string input)
		{
			return String.IsNullOrEmpty(input);
		}

		public static bool IsNullOrWhiteSpace(this string input)
		{
			return String.IsNullOrWhiteSpace(input);
		}

		public static bool IsNotNullOrEmpty(this string input)
		{
			return !String.IsNullOrEmpty(input);
		}

		public static bool IsNotNullOrWhiteSpace(this string input)
		{
			return !String.IsNullOrWhiteSpace(input);
		}

		public static string Remove(this string input, string stringToRemove)
		{
			return input.RemoveAll(stringToRemove);
		}

		public static string RemoveAll(this string input, params string[] stringsToRemove)
		{
			return stringsToRemove.Aggregate(input, (current, c) => current.Replace(c, string.Empty));
		}
	}
}