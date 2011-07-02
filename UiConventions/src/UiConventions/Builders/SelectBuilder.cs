namespace HtmlTags.UI.Builders
{
	using System;
	using System.Linq;
	using Attributes;
	using BclExtensionMethods;
	using Constants;
	using FubuCore;
	using FubuCore.Reflection;
	using FubuMVC.UI.Configuration;

	public class SelectBuilder
	{
		public static SelectTag Build(ElementRequest request)
		{
			return BuildOptions(request)
				.Id(request.ElementId) as SelectTag;
		}

		public static HtmlTag AjaxBuild(ElementRequest request)
		{
			string script;
			if(request.Accessor.PropertyType.In(typeof(Guid?), typeof(Guid), typeof(string)))
			{
				script = "var default{0} = '{1}';".ToFormat(request.ElementId, request.RawValue ?? string.Empty);
			}
			else // hopefully a numeric type
			{
				script = "var default{0} = {1};".ToFormat(request.ElementId, request.RawValue ?? 0);
			}
			
			var select = Tags.Select
				.Id(request.ElementId)
				.Attr(HtmlAttributeConstants.Name, request.ElementId);

			return Tags.Span
				.Child(select)
				.Child(Tags.Script(script));
		}

		private static SelectTag BuildOptions(ElementRequest request)
		{
			var select = Tags.Select;

			AddOptionalBlankOption(request, select);
			AddOptions(request, select);
			SetSelectedValue(request, select);

			return select;
		}

		private static void SetSelectedValue(ElementRequest request, SelectTag select)
		{
			if (request.RawValue != null)
			{
				select.SelectByValue(request.RawValue.ToString());
			}
		}

		private static void AddOptions(ElementRequest request, SelectTag select)
		{
			var from = GetOptionsFromAttribute(request);
			var optionPairs = GetOptionPairs(request, from);
			foreach (var item in optionPairs)
			{
				select.Option(item.Text, item.Value);
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

		private static Options GetOptionPairs(ElementRequest req, OptionsFromAttribute fromAttrib)
		{
			var fromProperty = req.Accessor.DeclaringType.GetProperties()
				.FirstOrDefault(p => p.Name == fromAttrib.PropertyName);
			if (fromProperty == null)
			{
				var message = string.Format("Could not find options source property '{0}' on type '{1}'",
				                            fromAttrib.PropertyName, req.Accessor.DeclaringType.Name);
				throw new Exception(message);
			}
			return fromProperty.GetGetMethod().Invoke(req.Model, null) as Options;
		}

		private static OptionsFromAttribute GetOptionsFromAttribute(ElementRequest req)
		{
			var from = req.Accessor.GetAttribute<OptionsFromAttribute>();
			if (from == null)
			{
				var fromAttribType = typeof (OptionsFromAttribute);
				var message = string.Format("Expected property '{0}' to have decorator: {1}.",
				                            req.ElementId, fromAttribType.Name);
				throw new Exception(message);
			}
			return from;
		}
	}
}