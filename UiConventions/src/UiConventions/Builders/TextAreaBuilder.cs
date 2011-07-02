namespace HtmlTags.UI.Builders
{
	using Attributes;
	using Constants;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public class TextAreaBuilder : BaseElementBuilder
	{
		public static int NumberOfRows = 6;

		protected override bool matches(AccessorDef def)
		{
			return def.Accessor.HasAttribute<MultilineAttribute>();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			return Build(request);
		}

		public static HtmlTag Build(ElementRequest request)
		{
			var numberOfRows = 6;
			int? numberOfColumns = null;
			var attribute = request.Accessor.GetAttribute<MultilineAttribute>();
			if (attribute != null)
			{
				if (attribute.NumberOfRows.HasValue)
				{
					numberOfRows = attribute.NumberOfRows.Value;
				}
				if (attribute.NumberOfColumns.HasValue)
				{
					numberOfColumns = attribute.NumberOfColumns.Value;
				}
			}
			var textArea = new TextAreaTag()
				.Rows(numberOfRows);

			if (numberOfColumns.HasValue)
			{
				textArea.Cols(numberOfColumns.Value);
			}

			return textArea
				.Id(request.ElementId)
				.Attr(HtmlAttributeConstants.Name, request.ElementId)
				.Text(request.StringValue());
		}
	}
}