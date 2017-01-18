namespace HtmlTags.UI.Attributes
{
	using System;

	public class DataTablesRangedFilterAttribute : Attribute
	{
		public string Name { get; set; }

		public DataTablesRangedFilterAttribute(string name)
		{
			Name = name;
		}
	}
}