namespace HtmlTags.UI.Builders
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Attributes;
	using Extensions;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public class CheckBoxListBuilder : BaseElementBuilder
	{
		public static string OptionSpanClass = "option";
		public static string OptionHorizontalSpanClass = "horizontalOption";
		public static string CheckBoxesClass = "checkboxes";

		protected override bool matches(AccessorDef definition)
		{
			return definition.Accessor.HasAttribute<CheckBoxListAttribute>();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var attribute = GetCheckBoxListAttribute(request);
			var tag = attribute.Horizontal ? Tags.Span : Tags.Div;

			var optionPairs = GetOptionPairs(request, attribute);
			var checkedOptions = request.Value<IList<int>>().Cast<object>();
			var groupName = request.ElementId;
			foreach (var option in optionPairs)
			{
				var isChecked = checkedOptions.Contains(option.Value);
				var label = Tags.Label.For(groupName).Text(option.Text);
				var checkBox = Tags.Checkbox(isChecked).Name(groupName).Value(option.Value);

				tag.Nest(
					Tags.Div.Nest(
						Tags.Span
							.AddClass(OptionSpanClass)
							.AddClass(attribute.Horizontal ? OptionHorizontalSpanClass : string.Empty)
							.Nest(
								label,
								checkBox
							)));
			}

			return tag
				.AddClass(CheckBoxesClass);
		}

		protected virtual Options GetOptionPairs(ElementRequest request, CheckBoxListAttribute attribute)
		{
			var optionsProperty = request.Accessor.DeclaringType.GetProperties()
				.FirstOrDefault(p => p.Name == attribute.OptionsFrom);
			if (optionsProperty == null)
			{
				var message = string.Format("Could not find options source property '{0}' on type '{1}'",
				                            attribute.OptionsFrom, request.Accessor.DeclaringType.Name);
				throw new Exception(message);
			}
			return optionsProperty.GetGetMethod().Invoke(request.Model, null) as Options;
		}

		protected virtual CheckBoxListAttribute GetCheckBoxListAttribute(ElementRequest request)
		{
			var attribute = request.Accessor.GetAttribute<CheckBoxListAttribute>();
			if (attribute == null)
			{
				var message = string.Format("Expected property '{0}' to have attribute: {1}.", request.ElementId,
				                            typeof (CheckBoxListAttribute));
				throw new Exception(message);
			}
			return attribute;
		}
	}
}