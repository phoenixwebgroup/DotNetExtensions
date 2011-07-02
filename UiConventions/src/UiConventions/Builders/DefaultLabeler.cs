namespace HtmlTags.UI.Builders
{
	using System;
	using Conventions;
	using Extensions;
	using FubuMVC.UI.Configuration;

	public class DefaultLabeler : BaseElementBuilder
	{
		public static TagBuilder Default
		{
			get
			{
				var builder = new DefaultLabeler();
				return builder.Build;
			}
		}

		protected override bool matches(AccessorDef def)
		{
			throw new NotSupportedException();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var label = LabelingConvention.GetLabelText(request.Accessor);

			return Tags.Span
				.Text(label);
		}
	}
}