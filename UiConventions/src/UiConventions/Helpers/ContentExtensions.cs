namespace HtmlTags.UI.Helpers
{
	using System;
	using System.IO;
	using System.Web;
	using System.Web.Mvc;
	using Constants;

	/// <summary>
	/// Extensions that just work to add include files in html
	/// </summary>
	public static class HtmlContentExtensions
	{
		public static string DefaultScriptLocation = "~/Content";
		public static string DefaultStyleSheetLocation = "~/Content";
		public static string DefaultImageLocation = "~/Content";
		public static string DefaultContentLocation = "~/Content";

		public static HtmlTag Image(this HtmlHelper html, string location)
		{
			var path = GetPath(DefaultImageLocation, location);
			return Tags.Image.Attr(HtmlAttributeConstants.Src, path);
		}

		/// <summary>
		/// Include a style sheet, relative or absolute path, uses DefaultStyleSheetLocation
		/// </summary>
		public static HtmlTag Stylesheet(this HtmlHelper html, string location)
		{
			var path = GetPath(DefaultStyleSheetLocation, location);
			return Tags.CssLink(path);
		}

		/// <summary>
		/// Include a script, relative or absolute path, uses DefaultScriptLocation
		/// </summary>
		public static HtmlTag ScriptInclude(this HtmlHelper html, string location)
		{
			var path = GetPath(DefaultScriptLocation, location);
			return Tags.ScriptInclude(path);
		}

		private static string GetPath(string defaultRoot, string location)
		{
			if (location.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase)
			    || location.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
			{
				return location;
			}
			if (VirtualPathUtility.IsAppRelative(location))
			{
				return VirtualPathUtility.ToAbsolute(location);
			}
			if (VirtualPathUtility.IsAbsolute(location))
			{
				return location;
			}
			return VirtualPathUtility.ToAbsolute(Path.Combine(defaultRoot, location));
		}

		public static string GetPath(this HtmlHelper helper, string location)
		{
			return GetPath(DefaultContentLocation, location);
		}

		public static string GetPath(string location)
		{
			return GetPath("~/", location);
		}
	}
}