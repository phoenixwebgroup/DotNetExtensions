namespace HtmlTags.UI.Attributes
{
	using System;

	/// <summary>
	/// 	Renders as a text area.
	/// </summary>
	public class MultilineAttribute : Attribute
	{
		public int? NumberOfRows { get; set; }
		public int? NumberOfColumns { get; set; }
	}
}