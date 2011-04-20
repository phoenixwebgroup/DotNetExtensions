namespace BclExtensionMethods.Email
{
	using System.Text.RegularExpressions;

	public static class EmailExtensions
	{
		public static string EmailValidationRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

		public static bool IsValidEmailAddress(this string source)
		{
			var regex = new Regex(EmailValidationRegex);
			return regex.IsMatch(source);
		}
	}
}