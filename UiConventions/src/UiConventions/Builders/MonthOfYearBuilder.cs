namespace HtmlTags.UI.Builders
{
	namespace UI.Application
	{
		using BclExtensionMethods;
		using BclExtensionMethods.ValueTypes;
		using FubuMVC.UI.Configuration;
		using HtmlTags;
		using Constants;
		using Extensions;
		using Builders;

		public class MonthOfYearEditor : BaseElementBuilder
		{
			protected override bool matches(AccessorDef def)
			{
				return def.Accessor.PropertyType.In(typeof(MonthOfYear), typeof(MonthOfYear?));
			}

			protected override HtmlTag BuildTag(ElementRequest request)
			{
				var id = request.ElementId;

				var value = GetValue(request);

				var picker = GetMonthPickerDiv(id);
				var input = GetHiddenInput(id, value);
				var script = GetScript(id, value);

				return Tags.Span
					.Nest(picker, input, script);
			}

			private HtmlTag GetHiddenInput(string id, MonthOfYear value)
			{
				return new HiddenTag()
					.Id(id)
					.Attr(HtmlAttributeConstants.Name, id)
					.Value(value.ToStringYearDashMonth());
			}

			private HtmlTag GetMonthPickerDiv(string id)
			{
				return Tags.Div
					.Id(GetPickerId(id))
					.AddClass("MonthPicker");
			}

			private MonthOfYear GetValue(ElementRequest request)
			{
				var value = MonthOfYear.Current;
				if (!request.ValueIsEmpty())
				{
					value = request.Value<MonthOfYear>();
				}
				return value;
			}

			private string GetPickerId(string id)
			{
				return id + "picker";
			}

			private ScriptTag GetScript(string id, MonthOfYear value)
			{
				// todo clean this up when we move this to HtmlTags library
				const string template =
					"$(function() {{$('#{1}').monthpicker({{ onChanged: function(data){{ $('#{0}').val(data.year + '-' + data.month); $('#{0}').trigger('change') }}, elements:[{{tpl:\"month\", opt:{{value: {2}}}}},{{tpl:\"year\", opt:{{value: {3}}}}}] }});}})";
				var script = string.Format(template, id, GetPickerId(id), value.Month, value.Year);
				return Tags.Script(script);
			}
		}
	}
}