namespace BclExtensionMethods.Pagination
{
	using System;
	using System.Collections.Generic;

	[Serializable]
	public class PagingCriteria
	{
		public PagingCriteria()
		{
			SortFields = new List<SortField>();
		}

		public static PagingCriteria GetPage(int page)
		{
			return new PagingCriteria
			       	{
			       		Page = (page < 1) ? 1 : page
			       	};
		}

		public static PagingCriteria GetPageAtIndex(int index)
		{
			return new PagingCriteria
			       	{
			       		Page = (index < 0) ? 1 : (index + 1)
			       	};
		}

		public static PagingCriteria NotPaged()
		{
			return new PagingCriteria
			       	{
			       		IsNotPaged = true
			       	};
		}
		
		public PagingCriteria WithPageSize(int pageSize)
		{
			PageSize = (pageSize < 1) ? 1 : pageSize;
			return this;
		}

		public bool IsNotPaged { get; set; }

		public int Page { get; set; }

		public int PageIndex
		{
			get { return (Page - 1); }
		}

		public int PageSize { get; set; }

		public IList<SortField> SortFields { get; set; }
	}
}