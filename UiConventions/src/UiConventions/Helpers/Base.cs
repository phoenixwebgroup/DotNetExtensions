namespace HtmlTags.UI.Helpers
{
	using System.Web;
	using System.Web.Mvc;

	/// <summary>
	/// Used to set the base tag on a page
	/// </summary>
	public static class BaseHelper
	{
		public static BaseTag Base(this HtmlHelper helper)
		{
			return BaseFromRequest(helper.ViewContext.HttpContext.Request);
		}

		public static BaseTag BaseFromRequest(HttpRequestBase request)
		{
			var path = FullApplicationPath(request);
			if (!path.EndsWith("/"))
			{
				path += "/";
			}
			return Tags.Base.Href(path);
		}

		private static string FullApplicationPath(HttpRequestBase request)
		{
			var path = request.Url.AbsoluteUri;
			if (request.Url.AbsolutePath != "/")
			{
				path = path.Replace(request.Url.AbsolutePath, string.Empty);
			}
			if (!string.IsNullOrEmpty(request.Url.Query))
			{
				path = path.Replace(request.Url.Query, string.Empty);
			}
			return path + request.ApplicationPath;
		}
	}
}