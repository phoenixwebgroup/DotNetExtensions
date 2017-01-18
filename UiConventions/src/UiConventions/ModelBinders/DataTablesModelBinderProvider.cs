namespace HtmlTags.UI.ModelBinders
{
	using System;
	using System.Web.Mvc;
	using Helpers;

	public class DataTablesModelBinderProvider : IModelBinderProvider
	{
		public IModelBinder GetBinder(Type modelType)
		{
			if (modelType.BaseType == typeof(DataTablesFilter))
			{
				return new DataTablesFilterModelBinder();
			}
			return null;
		}
	}
}