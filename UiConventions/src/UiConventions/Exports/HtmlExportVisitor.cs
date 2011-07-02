namespace HtmlTags.UI.Exports
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using BclExtensionMethods;

	public class HtmlExportVisitor : ExportVisitor
	{
		private readonly HtmlTextWriter _Html;
		private readonly StringWriter _Writer;

		public string CssTableClass = "printReport";

		public HtmlExportVisitor()
		{
			_Writer = new StringWriter();
			_Html = new HtmlTextWriter(_Writer);
		}

		public override void Visit(ExportTable exportTable, Action inner)
		{
			SetCssClass();
			SetBorder();
			SetHorizontalAlign(exportTable);
			StartTag(HtmlTextWriterTag.Table);
			inner();
			EndTag();
			BreakLine();
			_Html.Write(ExportPdfHelper.cssPdf);
		}

		private void SetHorizontalAlign(ExportTable exportTable)
		{
			_Html.AddAttribute(HtmlTextWriterAttribute.Align, exportTable.HorizontalAlign.ToString());
		}

		private void SetBorder()
		{
			_Html.AddAttribute(HtmlTextWriterAttribute.Border, "1");
			_Html.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
		}

		private void SetCssClass()
		{
			if (!string.IsNullOrEmpty(CssTableClass))
			{
				_Html.AddAttribute(HtmlTextWriterAttribute.Class, CssTableClass);
			}
		}

		public override void Visit(ExportRow row, Action inner)
		{
			StartTag(HtmlTextWriterTag.Tr);
			inner();
			EndTag();
		}

		public override void Visit(ExportList list, Action inner)
		{
			SetHorizontalAlignment(HorizontalAlign.Left);
			StartTag(HtmlTextWriterTag.Div);
				StartTag(HtmlTextWriterTag.Ul);
				inner();
				EndTag();
			EndTag();
		}

		public override void Visit(ExportListItem item, Action inner)
		{
			if(item is ExportList)
			{
				StartTag(HtmlTextWriterTag.Ul);
			}
			else
			{
				StartTag(HtmlTextWriterTag.Li);
				SetContent(item);
			}
			inner();
			EndTag();
		}

		public override void Visit(ExportCell cell, Action inner)
		{
			SetColumnSpan(cell);
			SetHorizontalAlignment(cell.HorizontalAlignment);
			SetWrap(cell);
			StartTag(HtmlTextWriterTag.Td);
			SetContent(cell);
			inner();
			EndTag();
		}

		public override void Visit(ExportDocument document, Action inner)
		{
			StartTag(HtmlTextWriterTag.Html);
			inner();
			EndTag();
		}

		public override void Visit(ExportParagraph paragraph, Action inner)
		{
			SetHorizontalAlignment(paragraph.HorizontalAlignment);
			StartTag(HtmlTextWriterTag.P);
			WriteLines(paragraph);
			inner();
			EndTag();
		}

		private void WriteLines(ExportParagraph paragraph)
		{
			var lines = paragraph.Text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			lines.OfType<string>().ForEach(l =>
			                               {
			                               	_Html.WriteEncodedText(l);
			                               	BreakLine();
			                               });
		}

		public override void Visit(ExportImage image, Action inner)
		{
			_Html.AddAttribute(HtmlTextWriterAttribute.Src,image.Source,false);
			StartTag(HtmlTextWriterTag.Img);
			inner();
			_Html.EndRender();
		}

		private void BreakLine()
		{
			StartTag(HtmlTextWriterTag.Br);
			EndTag();
		}

		public override string GetResult()
		{
			return _Writer.ToString();
		}

		private void EndTag()
		{
			_Html.RenderEndTag();
		}

		private void StartTag(HtmlTextWriterTag tag)
		{
			_Html.RenderBeginTag(tag);
		}

		private void SetContent(ExportCell cell)
		{
			if (string.IsNullOrEmpty(cell.Markup))
			{
				_Html.WriteEncodedText(cell.Text.Replace("&nbsp;", " "));
			}
			else
			{
				_Html.Write(cell.Markup);
			}
		}

		private void SetContent(ExportListItem item)
		{
			if(!string.IsNullOrEmpty(item.Text))
			{
				_Html.WriteEncodedText(item.Text.Replace("&nbsp;", " "));
			}
		}

		private void SetWrap(ExportCell cell)
		{
			if (!cell.AllowWrapping.HasValue || !cell.AllowWrapping.Value)
			{
				_Html.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
			}
		}

		private void SetHorizontalAlignment(HorizontalAlign align)
		{
			if (align != HorizontalAlign.NotSet)
			{
				_Html.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, align.ToString());
			}
		}

		private void SetColumnSpan(ExportCell cell)
		{
			if (cell.ColumnSpan.HasValue && cell.ColumnSpan.Value > 1)
			{
				_Html.AddAttribute(HtmlTextWriterAttribute.Colspan, cell.ColumnSpan.ToString());
			}
		}
	}
}