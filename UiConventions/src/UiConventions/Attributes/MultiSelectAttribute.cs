namespace HtmlTags.UI.Attributes
{
	using System;

	public class MultiSelectAttribute : Attribute
	{
		public static int DefaultNumberOfRows = 12;

		public MultiSelectAttribute()
		{
			Rows = DefaultNumberOfRows;
		}

		public int Rows { get; set; }
	}
}