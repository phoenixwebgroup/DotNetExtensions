namespace HtmlTags.UI.Conventions
{
	using System;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public class DottedElementNamingConvention : IElementNamingConvention
	{
		public string GetName(Type modelType, Accessor accessor)
		{
			if (accessor.PropertyNames != null)
			{
				return string.Join(".", accessor.PropertyNames);
			}
			return accessor.Name;
		}
	}
}