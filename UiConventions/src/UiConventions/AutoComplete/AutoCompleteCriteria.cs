namespace HtmlTags.UI.AutoComplete
{
	public class AutoCompleteCriteria
	{
		private const int DefaultLimit = 20;

		private int _Limit;

		/// <summary>
		/// Number of results to send back to the client, defaults to 20
		/// </summary>
		public int Limit
		{
			get { return _Limit < 1 ? DefaultLimit : _Limit; }
			set { _Limit = value; }
		}

		/// <summary>
		/// For model binding purposes from jquery.autocomplete
		/// </summary>
		public string q { get; set; }

		/// <summary>
		/// Filter text from user with partial search criteria they are filtering results on.
		/// </summary>
		public string Filter
		{
			get { return q; }
		}
	}
}