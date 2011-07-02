namespace HtmlTags.UI.Exports
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.UI.WebControls;
	using BclExtensionMethods;
	using Constants;
	using HtmlTags;

	/// <summary>
	///     This will map an HtmlTags table to an ExportTable.  This does not support tables in tables.
	/// </summary>
	public class HtmlTagToExportVisitor
	{
		private IList<IExportElement> _Captions;
		private ExportTable _Table;
		

		public IList<IExportElement> GetElements()
		{
			var elements = new List<IExportElement>();
			elements.AddRange(_Captions);
			elements.Add(_Table);
			return elements;
		}

		public HtmlTagToExportVisitor()
		{
			_Table = new ExportTable();
			_Captions = new List<IExportElement>();
		}

		public void Visit(HtmlTag table)
		{
			if (table.TagName().ToLower() != HtmlTagConstants.Table)
			{
				throw new ArgumentException(string.Format("Expected tag to be a <{0}> tag, but was <{1}>", HtmlTagConstants.Table,
				                                          table.TagName()));
			}
			table.Children.ForEach(VisitTableChild);
		}

		public ExportList VisitList(HtmlTag list )
		{
			if(list.TagName().ToLower() != HtmlTagConstants.Ul )
			{
				throw new ArgumentException(string.Format("Expected tag to be a <{0}> tag, but was <{1}>", HtmlTagConstants.Ul,
													  list.TagName()));
			}
			var exportList = new ExportList();
			list.Children.ForEach(f => VisitListItem(f, exportList));
			return exportList;
		}

		public void VisitListItem(HtmlTag list, ExportListItem exportList)
		{
			var item = new ExportListItem();
			if(list.TagName().ToLower() == HtmlTagConstants.Ul)
			{
				item = new ExportList();
			}
			
			list.Children.ForEach(f => VisitListItem(f, item));
			item.Text = ExportFromTable.HtmlTagRegex.Replace(list.Text(), String.Empty);
			
			exportList.AddItem(item);
		}

		public ExportImage VisitImage(HtmlTag imageTag)
		{
			if (imageTag.TagName().ToLower() != HtmlTagConstants.Img)
			{
				throw new ArgumentException(string.Format("Expected tag to be a <{0}> tag, but was <{1}>", HtmlTagConstants.Img,
				                                          imageTag.TagName()));
			}
			var image = (ImageTag) imageTag;
			return new ExportImage {Source = image.Src(), AlternateText = image.Alt()};
		}

		private void VisitTableChild(HtmlTag child)
		{
			var tag = child.TagName().ToLower();
			switch (tag)
			{
				case HtmlTagConstants.Tr:
					VisitTableRow(child);
					break;
				case HtmlTagConstants.Caption:
					VisitCaption(child);
					break;
				default:
					child.Children.ForEach(VisitTableChild);
					break;
			}
		}

		private void VisitCaption(HtmlTag child)
		{
			var exportParagraph = new ExportParagraph();
			child.Children.ForEach(VisitCaptionChild);
			exportParagraph.Text = ExportFromTable.HtmlTagRegex.Replace(child.Text(), String.Empty);
			_Captions.Add(exportParagraph);
		}

		private void VisitCaptionChild(HtmlTag caption)
		{
			var exportParagraph = new ExportParagraph();
			exportParagraph.Text = ExportFromTable.HtmlTagRegex.Replace(caption.Text(), String.Empty);
			_Captions.Add(exportParagraph);
		}

		private void VisitTableRow(HtmlTag row)
		{
			var exportRow = new ExportRow(HorizontalAlign.NotSet);
			_Table.AddRow(exportRow);
			row.Children.ForEach(c => VisitTableCell(c, exportRow));
		}

		private static void VisitTableCell(HtmlTag cell, ExportRow exportRow)
		{
			var tag = cell.TagName().ToLower();
			if (tag != HtmlTagConstants.Td && tag != HtmlTagConstants.Th )
			{
				cell.Children.ForEach(c => VisitTableCell(c, exportRow));
				return;
			}
			var isImage = cell.Children.Any(c => c.TagName().ToLower() == HtmlTagConstants.Img);
			var text = cell.Children.Any() ? cell.ToString() : cell.Text();
			var exportCell = new ExportCell
			                 	{
									Text =  ExportFromTable.HtmlTagRegex.Replace(text ?? string.Empty, string.Empty),
									Markup = isImage ? cell.FirstChild().ToString():string.Empty
								};

			SetColspan(cell, exportCell);
			exportRow.AddCell(exportCell);
		}

		private static void SetColspan(HtmlTag cell, ExportCell exportCell)
		{
			if (!cell.HasAttr(HtmlAttributeConstants.Colspan))
			{
				return;
			}
			int colspan;
			if(int.TryParse(cell.Attr(HtmlAttributeConstants.Colspan), out colspan))
			{
				exportCell.ColumnSpan = colspan;
			}
		}
	}
}