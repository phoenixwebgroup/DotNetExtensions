namespace HtmlTags.UI.TableResult
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Web;
	using System.Web.Mvc;
	using BclExtensionMethods;

	public class CsvResult : FileResult
	{
		public const string Delimiter = ",";
		private const string SingleSpace = " ";

		public IEnumerable Data { get; set; }

		public String[] InvalidChars
		{
			get { return new[] {Delimiter, Environment.NewLine, "\r", "\n"}; }
		}

		public CsvResult(IEnumerable data, string filename) : base("text/csv")
		{
			if (data == null)
			{
				throw new ArgumentNullException();
			}
			Data = data;
			FileDownloadName = filename + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
		}

		protected override void WriteFile(HttpResponseBase response)
		{
			response.Write(BuildCsvContent());
		}

		public string BuildCsvContent()
		{
			var sb = new StringBuilder();
			var exportProperties = GetPropertiesToExport();
			WriteHeaders(exportProperties, sb);
			WriteData(exportProperties, sb);
			return sb.ToString();
		}

		private void WriteData(Dictionary<string, PropertyInfo> exportProperties, StringBuilder sb)
		{
			Data.Cast<object>().Select(row => exportProperties.Select(prop =>
			                                                          	{
			                                                          		var value = prop.Value.GetValue(row, null);
			                                                          		return RemoveInvalidCharacters(value != null ? value.ToString() : string.Empty);
			                                                          	}))
				.Select(columnValue => String.Join(Delimiter, columnValue.ToArray()))
				.ForEach(rowString => sb.AppendLine(rowString));
		}

		private void WriteHeaders(Dictionary<string, PropertyInfo> exportProperties, StringBuilder sb)
		{
			var headers = exportProperties.Select(prop => prop.Key).ToArray();
			sb.AppendLine(String.Join(Delimiter, headers));
		}

		private Dictionary<string, PropertyInfo> GetPropertiesToExport()
		{
			Type type;
			if (Data.GetType().IsArray)
			{
				type = Data.GetType().GetElementType();
			}
			else if (Data.GetType().GetGenericArguments().Count() > 0)
			{
				type = Data.GetType().GetGenericArguments().First();
			}
			else
			{
				throw new Exception("Unknown type.");
			}

			var exportProperties = new Dictionary<string, PropertyInfo>();

			foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				var attrib = prop.GetCustomAttributes(typeof (CsvExportAttribute), false).FirstOrDefault() as CsvExportAttribute;
				if (attrib == null)
				{
					exportProperties.Add(prop.Name, prop);
					continue;
				}
				if (attrib.Exclude)
				{
					continue;
				}
				var header = attrib.Header ?? prop.Name;
				exportProperties.Add(header, prop);
			}
			return exportProperties;
		}

		private string RemoveInvalidCharacters(string source)
		{
			if (string.IsNullOrEmpty(source))
			{
				return source;
			}
			return InvalidChars.Aggregate(source, (str, invalidchar) => str = str.Replace(invalidchar, SingleSpace));
		}
	}
}