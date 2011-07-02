namespace HtmlTags.UI.ModelBinders
{
	using System.Web.Mvc;
	using BclExtensionMethods;

	public class DecimalModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueResult != null)
			{
				return valueResult.AttemptedValue.ParseDecimal();
			}
			return base.BindModel(controllerContext, bindingContext);
		}
	}
}