namespace HtmlTags.UI.TableResult
{
	using System;

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class CsvExcludeAttribute : CsvExportAttribute
	{
		public CsvExcludeAttribute()
		{
			Exclude = true;
		}
	}
}