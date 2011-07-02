namespace HtmlTags.Extensions
{
	using System.Collections.Generic;
	using System.Linq;
	using Constants;
	using UI.Exports;

	public static class HtmlTagExtensions
	{
		public static HtmlTag Nest(this HtmlTag tag, params HtmlTag[] children)
		{
			return tag.AddChildren(children);
		}

		public static HtmlTag Value(this HtmlTag tag, object value)
		{
			tag.Attr(HtmlAttributeConstants.Value, value);
			return tag;
		}

		/// <summary>
		/// 	Returns all tags, including the current tag and children, depth first recursive search.
		/// </summary>
		public static IEnumerable<HtmlTag> AllTags(this HtmlTag tag)
		{
			yield return tag;
			var nestedTagMatches = tag.Children.SelectMany(child => child.AllTags());
			foreach (var nestedTag in nestedTagMatches)
			{
				yield return nestedTag;
			}
		}

		public static byte[] ConvertToPdf(this HtmlTag tag)
		{
			// todo this method is ok, but how about we also move this code somewhere and make a builder that can be controlled instead of defaults here and then keep this method but plugin to the builder.
			var html = tag.ToString();

			var settings = new ExportEventArgs.ExportSettings
			               	{
			               		PdfPageOrientation = PDFPageOrientation.Portrait,
			               		PdfPageSize = PdfPageSize.Letter
			               	};

			var options = new ExportEventArgs(html, "export")
			              	{
			              		ExportType = ExportType.CustomPdf,
			              		SendByEmail = false,
			              		Settings = settings
			              	};

			return ExportPdfHelper.ToPdfBytes(options);
		}
	}
}