namespace HtmlTags.UI.AutoComplete
{
	using Constants;
	using FubuMVC.UI.Configuration;
	using Helpers;
	using Extensions;

	public class AutoCompleteBuilder : ElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			return typeof (AutoCompleteInput).IsAssignableFrom(def.Accessor.PropertyType);
		}

		public override HtmlTag Build(ElementRequest request)
		{
			var name = request.Accessor.Name;
			var pair = request.Value<AutoCompleteInput>();

			var textBoxId = name + "Text";
			var textBox = Tags.Input
				.Attr(HtmlAttributeConstants.Type, "Text")
				.Id(textBoxId)
				.Value(pair.Text)
				.Attr(HtmlAttributeConstants.Name, textBoxId);

			var valueBoxId = name + "Value";
			var valueBox = Tags.Hidden
				.Id(valueBoxId)
				.Value(pair.Value)
				.Attr(HtmlAttributeConstants.Name, valueBoxId);

			var createScript = GetCreateScript(textBoxId, pair.Url);

			var resultScript = GetResultScript(textBoxId, valueBoxId);

			return Tags.Span.Nest(
				textBox,
				valueBox,
				createScript,
				resultScript
				);
		}

		private static HtmlTag GetResultScript(string textBoxId, string valueBoxId)
		{
			const string resultTemplate =
				@"$.autoCompleteExtensions.setupGetResult('{0}','{1}');";
			var script = string.Format(resultTemplate, textBoxId, valueBoxId);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}

		private static HtmlTag GetCreateScript(string textBoxId, string url)
		{
			const string template =
				@"$.autoCompleteExtensions.createAutoComplete('{0}','{1}');";
			var script = string.Format(template, textBoxId, url);
			return JQueryHelpers.WrapWithJQueryReadyAndScriptTag(script);
		}
	}
}