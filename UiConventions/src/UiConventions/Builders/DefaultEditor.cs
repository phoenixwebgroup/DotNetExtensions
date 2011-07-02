namespace HtmlTags.UI.Builders
{
	using System;
	using Constants;
	using FubuMVC.UI.Configuration;
	using FubuMVC.UI.Tags;

	public class DefaultEditor : BaseElementBuilder
	{
		public static TagBuilder Default
		{
			get
			{
				var builder = new DefaultEditor();
				return builder.Build;
			}
		}

		protected override bool matches(AccessorDef def)
		{
			throw new NotSupportedException();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			return TagActionExpression.BuildTextbox(request)
				.Attr(HtmlAttributeConstants.Id, request.ElementId);
		}
	}
}