namespace HtmlTags.UI.AutoComplete
{
	using Helpers;

	public abstract class AutoCompleteInput
	{
		public AutoCompleteInput(string url)
		{
			Url = HtmlContentExtensions.GetPath(url);
		}

		public string Value { get; set; }
		public string Text { get; set; }
		public string Url { get; protected set; }
	}
}