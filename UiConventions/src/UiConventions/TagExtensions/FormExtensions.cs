namespace HtmlTags.Extensions.Mvc
{
	using System;
	using System.Linq.Expressions;
	using System.Web.Mvc;

	public static class FormExtensions
	{
		public static FormTag Form(this HtmlHelper htmlHelper)
		{
			return new FormTag();
		}

		public static FormTag Form<TController>(this UrlHelper helper, Expression<Action<TController>> actionAccessor)
		{
			return new FormTag(helper.GetUrl(actionAccessor));
		}
	}
}