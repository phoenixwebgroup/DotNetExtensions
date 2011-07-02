namespace HtmlTags.UI.Exports
{
	using System;
	using System.Collections.Generic;
	using System.Web.UI.WebControls;

	public class ExportDocument : IExportElement
	{
		private List<IExportElement> _Elements;

		public List<IExportElement> Elements
		{
			get
			{
				var list = new List<IExportElement>();
				if (!string.IsNullOrEmpty(Header))
				{
					list.Add(ExportParagraph.Create(Header, HorizontalAlign.Center));
				}
				list.AddRange(_Elements);
				return list;
			}
		}

		public string Header { get; set; }

		public ExportDocument()
		{
			_Elements = new List<IExportElement>();
		}

		public void Add(IExportElement element)
		{
			_Elements.Add(element);
		}

		public void AddRange(IEnumerable<IExportElement> elements)
		{
			_Elements.AddRange(elements);
		}

		public void ExportTo(ExportVisitor visitor)
		{
			Action inner = () => Elements.ForEach(e => e.ExportTo(visitor));
			visitor.Visit(this, inner);
		}
	}
}