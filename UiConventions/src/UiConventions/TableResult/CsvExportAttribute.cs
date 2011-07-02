namespace HtmlTags.UI.TableResult
{
	using System;

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class CsvExportAttribute : Attribute
	{
		public string Header { get; set; }
		public bool Exclude { get; set; }
	}
}