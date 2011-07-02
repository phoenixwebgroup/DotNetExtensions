namespace HtmlTags.UI.Scaffolding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Attributes;
	using Extensions;
	using FubuCore.Reflection;
	using Helpers;

	public class ScaffoldedView : IAmAView
	{
		private readonly object _ViewModel;

		private readonly HtmlTag _Tag;

		public Type ModelType
		{
			get { return _ViewModel.GetType(); }
		}

		public ScaffoldedView(object viewModel)
		{
			_ViewModel = viewModel;
			_Tag = CreateTag();
		}

		public HtmlTag GetTag()
		{
			return _Tag;
		}

		private HtmlTag CreateTag()
		{
			var type = _ViewModel.GetType();

			var properties = GetScaffoldedProperties(type)
				.Select(p => TemplateHelpers.DisplayFieldFor(type, _ViewModel, p));
			
			return Tags.Div
				.Nest(properties.ToArray());
		}

		public static IEnumerable<PropertyInfo> GetScaffoldedProperties(Type type)
		{
			return type.GetProperties()
				.Where(p => !p.HasAttribute<ScaffoldIgnoreAttribute>())
				.Where(p => !p.HasAttribute<HiddenAttribute>());
		}

		public HtmlTag View()
		{
			return GetTag();
		}
	}
}