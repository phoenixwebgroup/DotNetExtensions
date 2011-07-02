namespace HtmlTags.UI.ModelBinders
{
	using System.Web.Mvc;
	using BclExtensionMethods.ValueTypes;

	public class MonthOfYearModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueResult != null)
			{
				return MonthOfYear.MonthOfYearFrom(valueResult.AttemptedValue);
			}
			return base.BindModel(controllerContext, bindingContext);
		}
	}
}