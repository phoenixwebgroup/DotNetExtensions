namespace HtmlTags.UI.ModelBinders
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using BclExtensionMethods;
	using Helpers;

	public class DataTablesFilterModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var valueProvider = bindingContext.ValueProvider;
			var constructorInfo = bindingContext.ModelType.GetConstructor(new Type[] { });
			if (constructorInfo == null)
			{
				return null;
			}
			dynamic model = constructorInfo.Invoke(new object[] { });
			return Bind(valueProvider, model);
		}

		private T Bind<T>(IValueProvider valueProvider, T model) where T : DataTablesFilter
		{
			var dataTablesParam = new DataTablesParameters
			{
				iDisplayStart = GetValue<int>(valueProvider, "start"),
				iDisplayLength = GetValue<int>(valueProvider, "length"),
				sSearch = GetValue<string>(valueProvider, "search[value]"),
				bEscapeRegex = GetValue<bool>(valueProvider, "search[regex]"),
				sEcho = GetValue<int>(valueProvider, "draw")
			};
			var columnIndex = 0;
			while (true)
			{
				var column = string.Format("columns[{0}]", columnIndex);
				var data = GetValue<string>(valueProvider, column + "[data]");
				if (data != null)
				{
					var name = GetValue<string>(valueProvider, column + "[name]");
					dataTablesParam.sColumnNames.Add(name.IsNotNullOrWhiteSpace() ? name : data);
					dataTablesParam.bSortable.Add(GetValue<bool>(valueProvider, column + "[orderable]"));
					dataTablesParam.bSearchable.Add(GetValue<bool>(valueProvider, column + "[searchable]"));
					dataTablesParam.sSearchValues.Add(GetValue<string>(valueProvider, column + "[search][value]"));
					dataTablesParam.bEscapeRegexColumns.Add(GetValue<bool>(valueProvider, column + "[searchable][regex]"));
					++columnIndex;
				}
				else
					break;
			}
			dataTablesParam.iColumns = columnIndex;
			var orderIndex = 0;
			while (true)
			{
				var order = string.Format("order[{0}]", orderIndex);
				var nullable = GetValue<int?>(valueProvider, order + "[column]");
				if (nullable.HasValue)
				{
					dataTablesParam.iSortCol.Add(nullable.Value);
					dataTablesParam.sSortDir.Add(GetValue<string>(valueProvider, order + "[dir]"));
					++orderIndex;
				}
				else
					break;
			}
			for (var filterIndex = 0; filterIndex < dataTablesParam.iColumns; filterIndex++)
			{
				var filter = string.Format("filteringItems[{0}]", filterIndex);
				var filteredItem = GetValue<string>(valueProvider, filter);
				if (filteredItem.IsNotNullOrWhiteSpace())
				{
					dataTablesParam.RangedFilteredItems.Add(dataTablesParam.sColumnNames.ElementAt(filterIndex), filteredItem);
				}
			}
			dataTablesParam.iSortingCols = orderIndex;
			return DataTablesFilter.PopulateFrom(model, dataTablesParam);
		}

		private static T GetValue<T>(IValueProvider valueProvider, string key)
		{
			ValueProviderResult valueProviderResult = valueProvider.GetValue(key);
			return valueProviderResult == null ? default(T) : (T)valueProviderResult.ConvertTo(typeof(T));
		}
	}
}