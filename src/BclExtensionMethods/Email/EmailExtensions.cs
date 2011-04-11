namespace BclExtensionMethods.Email
{
	using System.Text.RegularExpressions;

	public static class EmailExtensions
	{
		public static bool IsValidEmailAddress(this string s)
		{
			var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
			return regex.IsMatch(s);
		}
	}
}