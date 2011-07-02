namespace HtmlTags.UI
{
	using System.Web.Mvc;

	public interface IFilterLocator
	{
		FilterInfo FindFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
	}
}