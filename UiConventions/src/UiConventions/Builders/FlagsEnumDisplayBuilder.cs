namespace HtmlTags.UI.Builders
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using BclExtensionMethods;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using Helpers;

	public class FlagsEnumDisplayBuilder : BaseElementBuilder
	{
		protected override bool matches(AccessorDef def)
		{
			return RemoveNullableIfNecessary(def.Accessor.PropertyType).HasAttribute<FlagsAttribute>();
		}

		protected override HtmlTag BuildTag(ElementRequest request)
		{
			var tag = Tags.Span;
			if (request.ValueIsEmpty())
			{
				return tag;
			}

			var text = GetNamesOfFlagsThatAreSet(request)
				.StringJoin(", ");

			return tag
				.Text(text);
		}

		private IEnumerable<string> GetNamesOfFlagsThatAreSet(ElementRequest request)
		{
			var value = request.Value<int>();
			var flagsEnumType = RemoveNullableIfNecessary(request.Accessor.PropertyType);

			return EnumHelper
				.GetOptions(flagsEnumType)
				.Where(o => ValueHasOptionSet(value, o))
				.Select(o => o.Name);
		}

		private static bool ValueHasOptionSet(int value, FieldInfo o)
		{
			return (value & (int) o.GetValue(null)) > 0;
		}
	}
}