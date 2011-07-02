namespace HtmlTags.UI.TableResult
{
	using System.Linq;
	using PagedList;

	public static class TableResultExtensions
	{
		public static TableResult ToTableResult<T>(this IPagedList<T> pagedList)
			where T : class
		{
			return new TableResult
			       	{
			       		Total = pagedList.PageCount,
			       		Page = pagedList.PageNumber,
			       		Records = pagedList.TotalItemCount,
			       		Rows = pagedList.ToArray()
			       	};
		}
	}
}