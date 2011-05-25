namespace JoinedFilter
{
	/// <summary>
	/// A way to reliably ensure a filter execution order.
	/// </summary>
	public interface IFilterPriority
	{
		/// <summary>
		/// The order of this filter.
		/// This is the construct used in FilterAttributes with ASP.Net MVC, so it was continued here.
		/// </summary>
		/// <returns></returns>
		int GetOrder();
	}
}