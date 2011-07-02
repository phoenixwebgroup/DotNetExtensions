namespace HtmlTags.UI.Exports
{
	using System;

	public static class ActionExtensions
	{
		public static Action NoOp
		{
			get { return () => { }; }
		}
	}
}