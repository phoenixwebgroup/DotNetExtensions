namespace HtmlTags.UI.Scaffolding
{
	using System;
	using System.Linq;
	using Conventions;
	using Extensions;
	using Helpers;

	public class ScaffoldedForm : IAmAForm
	{
		private readonly object _FormModel;

		private readonly HtmlTag _Tag;

		public Type ModelType
		{
			get { return _FormModel.GetType(); }
		}

		public ScaffoldedForm(object formModel)
		{
			_FormModel = formModel;
			_Tag = CreateTag();
		}

		public HtmlTag GetTag()
		{
			return _Tag;
		}

		private HtmlTag CreateTag()
		{
			var type = _FormModel.GetType();
			
			var properties = ScaffoldedView.GetScaffoldedProperties(type)
				.Select(p => TemplateHelpers.EditFieldFor(type, _FormModel, p));
			
			return Tags.Form
				.Nest(properties.ToArray())
				.Nest(SaveOrCancelConvention.SaveOrCancel());
		}

		public HtmlTag View()
		{
			return GetTag();
		}
	}
}