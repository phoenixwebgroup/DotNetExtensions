namespace HtmlTags.UI
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Web.Mvc;
	using Conventions;
	using Extensions;
	using FubuCore;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;
	using FubuMVC.UI.Tags;
	using Helpers;
	using Microsoft.Practices.ServiceLocation;

	public static class ViewConventionExtensions
	{
		public static ITagGenerator<T> GetGenerator<T>(T model) where T : class
		{
			var generator = ServiceLocator.Current.GetInstance<ITagGenerator<T>>() as TagGenerator<T>;
			generator.Model = model;
			return generator;
		}

		private static ITagGenerator GetGeneratorFromType(Type modelType)
		{
			var type = typeof (ITagGenerator<>).MakeGenericType(modelType);
			return (ITagGenerator) ServiceLocator.Current.GetInstance(type);
		}

		public static HtmlTag InputFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
		{
			return InputFor(helper.ViewData.Model, expression);
		}

		public static HtmlTag InputFor(Type modelType, object model, PropertyInfo property)
		{
			var generator = GetGeneratorFromType(modelType);
			var request = GetRequest(property, model);
			return generator.InputFor(request);
		}

		private static ElementRequest GetRequest(PropertyInfo property, object model)
		{
			var accessor = new SingleProperty(property);
			var elementRequest = new ElementRequest(model, accessor, ServiceLocator.Current);
			elementRequest.ElementId = property.Name;
			return elementRequest;
		}

		public static HtmlTag InputFor<T>(this T model, Expression<Func<T, object>> expression) where T : class
		{
			var generator = GetGenerator(model);
			return generator.InputFor(expression);
		}

		public static HtmlTag FilterFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
		{
			return Tags.Span
				.Nest(
					InputFor(helper.ViewData.Model, expression),
					helper.JqGridTriggerReload(expression)
				);
		}

		public static HtmlTag ReportFilterFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
		{
			return Tags.Span
				.Nest(
					InputForWithValue( helper.ViewData.Model, expression),
					helper.JqGridTriggerReload(expression)
				);
		}

		public static HtmlTag InputForWithValue<T>(this T model, Expression<Func<T, object>> expression) where T : class
		{
			var generator = GetGenerator(model);
			var request = generator.GetRequest(expression);
			return generator.InputFor(request).Value(request.RawValue);
		}

		public static HtmlTag LabelFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
		{
			return LabelFor(helper.ViewData.Model, expression);
		}

		public static HtmlTag LabelFor<T>(this T model, Expression<Func<T, object>> expression) where T : class
		{
			var generator = GetGenerator(model);
			return generator.LabelFor(expression);
		}

		public static HtmlTag LabelFor(Type modelType, object model, PropertyInfo property)
		{
			var generator = GetGeneratorFromType(modelType);
			var request = GetRequest(property, model);
			return generator.LabelFor(request);
		}

		public static HtmlTag DisplayFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
		{
			return DisplayFor(helper.ViewData.Model, expression);
		}

		public static HtmlTag DisplayFor<T>(this T model, Expression<Func<T, object>> expression) where T : class
		{
			var generator = GetGenerator(model);
			return generator.DisplayFor(expression);
		}

		public static HtmlTag DisplayFor(Type modelType, object model, PropertyInfo property)
		{
			var generator = GetGeneratorFromType(modelType);
			var request = GetRequest(property, model);
			return generator.DisplayFor(request);
		}

		public static string ElementNameFor<T>(Expression<Func<T, object>> expression)
			where T : class
		{
			var convention = ServiceLocator.Current.GetInstance<IElementNamingConvention>();
			return convention.GetName(typeof (T), expression.ToAccessor());
		}

		public static HtmlTag SubmitButton(string name)
		{
			var text = LabelingConvention.GetLabelText(name);
			return Tags.SubmitButton.Value(text).Id(name);
		}

		public static HtmlTag Button(string name)
		{
			var text = LabelingConvention.GetLabelText(name);
			return Tags.InputButton.Value(text).Id(name);
		}

		public static HtmlTag SaveOrCancel()
		{
			return SaveOrCancelConvention.SaveOrCancel();
		}

		public static HtmlTag ResetButton()
		{
			return FiltersConventions.ResetButtonConvention.ResetFilters();
		}

		public static HtmlTag FilterButton(string formSelector)
		{
			return FiltersConventions.FilterButtonConvention.Filter(formSelector);
		}

		public static HtmlTag SubmitOrCancel(string submitText)
		{
			return SaveOrCancelConvention.SubmitOrCancel(submitText);
		}

		public static HtmlTag ButtonsFor<TController>(this HtmlHelper helper,
		                                              params Expression<Action<TController>>[] expressions)
			where TController : IController
		{
			return PageActions.ButtonsFor(helper, expressions);
		}

		public static HtmlTag[] ExportButtons()
		{
			return new[]
			       	{
			       		ExportButton("Excel", OutputType.Csv),
			       		ExportButton("Print", OutputType.Print),
			       		ExportButton("Pdf", OutputType.Pdf),
			       		Tags.Hidden.Name("outputType").Id("outputType")
			       	};
		}

		private static HtmlTag ExportButton(string name, OutputType outputType)
		{
			return Button(name).AddClass("exportButton").Attr("outputType", outputType.ToString());
		}

		public static HtmlTag MenuItemFor<TController>(this UrlHelper helper,
		                                               Expression<Action<TController>> actionAccessor,
		                                               MethodInfo currentAction, object routeValues = null)
		{
			return PageActions.MenuItemFor(helper, actionAccessor, currentAction, routeValues);
		}

		public static HtmlTag MenuItemFor(this UrlHelper helper, string url, string text)
		{
			return PageActions.MenuItemFor(url, text, false);
		}
	}
}