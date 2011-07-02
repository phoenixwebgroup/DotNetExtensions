namespace HtmlTags.UI
{
	using System;

	internal static class ExceptionHelpers
	{
		internal static void IgnoreExceptions(Action statement)
		{
			try
			{
				statement();
			}
			catch
			{
			}
		}
	}
}