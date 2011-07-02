namespace HtmlTags.UI.Builders
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using Attributes;
	using Constants;
	using Conventions;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public class EnumDropDownBuilder : BaseElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			throw new NotImplementedException();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			return BuildSelect(request)
				.Id(request.ElementId)
				.Attr(HtmlAttributeConstants.Name, request.ElementId);
		}

		private SelectTag BuildSelect(ElementRequest request)
		{
			var select = Tags.Select;

			AddOptionalBlankOption(request, select);
			AddOptions(request, select);
			SetSelectedValue(request, select);

			return select;
		}

		private static void SetSelectedValue(ElementRequest request, SelectTag select)
		{
			if (!request.ValueIsEmpty())
			{
				select.SelectByValue(request.Value<int>().ToString());
			}
		}

		private void AddOptions(ElementRequest request, SelectTag select)
		{
			var options = GetOptions(request);
			foreach (var option in options)
			{
				var text = GetText(option);
				var value = option.GetRawConstantValue().ToString();
				select.Option(text, value);
			}
		}

		private static void AddOptionalBlankOption(ElementRequest request, SelectTag select)
		{
			var hasBlankOption = request.Accessor.HasAttribute<WithBlankOption>();
			if (hasBlankOption)
			{
				select.Option(string.Empty, string.Empty);
			}
		}

		protected virtual IEnumerable<FieldInfo> GetOptions(ElementRequest request)
		{
			var propertyType = request.Accessor.PropertyType;
			if (propertyType.IsGenericType)
			{
				propertyType = new NullableConverter(propertyType).UnderlyingType;
			}
			return propertyType.GetFields((BindingFlags.Public | BindingFlags.Static))
				.Where(f => !f.HasAttribute<ExcludeFromSelect>());
		}

		private static string GetText(FieldInfo field)
		{
			var descriptor = field.GetAttribute<DescriptionAttribute>();
			if (descriptor != null)
			{
				return descriptor.Description;
			}
			return LabelingConvention.GetLabelText(field.Name);
		}
	}
}