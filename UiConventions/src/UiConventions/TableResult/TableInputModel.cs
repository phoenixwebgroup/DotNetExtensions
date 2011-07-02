namespace HtmlTags.UI.TableResult
{
	using System;
	using BclExtensionMethods.Pagination;

	[Serializable]
	public class TableInputModel
	{
		// todo we could make model binders for this and make it more extensible to support other input sources instead of all hacked into one.

		public static string DefaultSortField = "Id";
		public static string DefaultSortDirection = "Asc";

		public string Sidx { get; set; }
		public string Sord { get; set; }
		public int Page { get; set; }
		public int Rows { get; set; }
		public bool NotPaged { get; set; }
		public TableOutputType OutputType { get; set; }

		public PagingCriteria ToPagingCriteria()
		{
			PagingCriteria criteria;
			if (NotPaged)
			{
				criteria = PagingCriteria.NotPaged();
			}
			else
			{
				var pageIndex = Page;
				var pageSize = Rows;

				criteria = PagingCriteria
					.GetPage(pageIndex)
					.WithPageSize(pageSize);
			}
			criteria.SortFields.Add(new SortField(Sidx ?? DefaultSortField, Sord ?? DefaultSortDirection));
			return criteria;
		}
	}
}