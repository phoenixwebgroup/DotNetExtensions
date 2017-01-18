namespace HtmlTags.UI.Helpers
{
	using System.Linq;
	using Attributes;
	using BclExtensionMethods;
	using BclExtensionMethods.Pagination;

	public class DataTablesFilter
	{
		public static T PopulateFrom<T>(T newFilters, DataTablesParameters parameters) where T : DataTablesFilter
		{
			var propertyInfos = newFilters.GetType().GetProperties();
			foreach (var property in propertyInfos)
			{
				var index = parameters.sColumnNames.IndexOf(property.Name);
				if (index >= 0)
				{
					var newValue = parameters.sSearchValues[index];
					property.SetValue(newFilters, newValue, null);
				}
				var attribute = property.GetCustomAttributes(typeof (DataTablesRangedFilterAttribute), true).FirstOrDefault() as DataTablesRangedFilterAttribute;
				if (attribute != null)
				{
					property.SetValue(newFilters, parameters.RangedFilteredItems.GetValueOrDefault(attribute.Name), null);
				}
			}
			newFilters.PagingCriteria = parameters.ToPagingCriteria();
			
			return newFilters;
		}

		public PagingCriteria PagingCriteria { get; set; }
	}
}