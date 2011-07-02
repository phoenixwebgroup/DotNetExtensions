namespace HtmlTags.UI.ModelBinders
{
	using System.Web.Mvc;
	using BclExtensionMethods.ValueTypes;

	public class ModelBinderRegistryBase
	{
		public static void DecimalModelBinder()
		{
			ModelBinders.Binders.Add(typeof (decimal), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof (decimal?), new DecimalModelBinder());
		}

		public static void MonthOfYearBinder()
		{
			ModelBinders.Binders.Add(typeof (MonthOfYear), new MonthOfYearModelBinder());
		}
	}
}