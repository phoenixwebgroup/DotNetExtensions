namespace HtmlTags.Extensions
{
	using System.Collections;
	using System.Linq;
	using Constants;
	using UI;

	public static class SelectTagExtensions
	{
		/// <summary>
		/// Marks the selected options from the values in the selected parameter 
		/// </summary>
		public static SelectTag SelectValues(this SelectTag tag, IEnumerable selected)
		{
			foreach (var value in selected)
			{
				// TODO Is there a better way to do this? Generics? It also impacts the CheckBoxBuilder.
				var stringValue = value.ToString(); // default to just writing string value of object
				ExceptionHelpers.IgnoreExceptions(() => stringValue = ((int) value).ToString()); // trys to cast value as an int.
				var optionTag = tag.Children.FirstOrDefault(x => x.Attr(HtmlAttributeConstants.Value).Equals(stringValue));
				if (optionTag != null)
				{
					optionTag.Attr(HtmlAttributeConstants.Selected, HtmlAttributeConstants.Selected);
				}
			}
			return tag;
		}
	}
}