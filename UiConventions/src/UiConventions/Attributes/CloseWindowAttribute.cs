namespace HtmlTags.UI.Attributes
{
	using System;

	public class CloseWindowAttribute : Attribute
	{
		public CloseWindowAttribute (int milliseconds)
		{
			MillisecondsToClose = milliseconds;
		}

		public int MillisecondsToClose { get; set; }
	}
}
