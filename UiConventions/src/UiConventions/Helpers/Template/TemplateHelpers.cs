namespace HtmlTags.UI.Helpers
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Web.Mvc;
	using Extensions;

	public static class TemplateHelpers
	{
		// todo make these pluggable with static accessor methods, they have worked well but would be nice to have pluggable implementations
		public static string FieldDivClass = "field";
		public static string FieldsDivClass = "fields";
		public static string LabelClass = "label";

		public static HtmlTag EditTemplateFor<T>(this T model, params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			if (!fieldSelectors.Any())
			{
				throw new ArgumentException("Must be at least one field selector");
			}

			var label = model.LabelFor(fieldSelectors[0]).AddClass(LabelClass);

			var fields = fieldSelectors
				.Select(f => Tags.Div
				             	.AddClass(FieldDivClass)
				             	.Nest(model.InputFor(f)));

			return Tags.Div
				.AddClass(FieldsDivClass)
				.Nest(label)
				.Nest(fields.ToArray());
		}

		public static HtmlTag EditTemplateFor<T>(this HtmlHelper<T> helper,
		                                         params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			return EditTemplateFor(helper.ViewData.Model, fieldSelectors);
		}

		public static HtmlTag DisplayTemplateFor<T>(this T model,
		                                            params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			if (!fieldSelectors.Any())
			{
				throw new ArgumentException("Must be at least one field selector");
			}

			var label = model.LabelFor(fieldSelectors[0]).AddClass(LabelClass);

			var fields = fieldSelectors
				.Select(f => Tags.Div
				             	.AddClass(FieldDivClass)
				             	.Nest(model.DisplayFor(f)));

			return Tags.Div
				.AddClass(FieldsDivClass)
				.Nest(label)
				.Nest(fields.ToArray());
		}

		public static HtmlTag DisplayTemplateFor<T>(this HtmlHelper<T> helper,
		                                            params Expression<Func<T, object>>[] fieldSelectors) where T : class
		{
			return DisplayTemplateFor(helper.ViewData.Model, fieldSelectors);
		}

		internal static HtmlTag EditFieldFor(Type modelType, object model, PropertyInfo fieldProperty)
		{
			return Tags.Div
				.AddClass("fields")
				.Nest(
					ViewConventionExtensions
						.LabelFor(modelType, model, fieldProperty)
						.AddClass("label"),
					Tags.Div
						.AddClass("field")
						.Nest(
							ViewConventionExtensions
								.InputFor(modelType, model, fieldProperty)
						)
				);
		}

		internal static HtmlTag DisplayFieldFor(Type modelType, object model, PropertyInfo fieldProperty)
		{
			return Tags.Div
				.AddClass("fields")
				.Nest(
					ViewConventionExtensions
						.LabelFor(modelType, model, fieldProperty)
						.AddClass("label"),
					Tags.Div
						.AddClass("field")
						.Nest(
							ViewConventionExtensions
								.DisplayFor(modelType, model, fieldProperty)
						)
				);
		}
	}
}