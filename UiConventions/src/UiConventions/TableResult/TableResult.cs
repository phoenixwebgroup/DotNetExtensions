namespace HtmlTags.UI.TableResult
{
	using System.Web.Mvc;

	public class TableResult : ActionResult
	{
		public int Total { get; set; }
		public int Records { get; set; }
		public int Page { get; set; }
		public object[] Rows { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.Write(ToJqGridJson());
		}

		public object ToJqGridJson()
		{
			return new
			       	{
			       		total = Total,
			       		page = Page,
			       		records = Records,
			       		rows = Rows
			       	};
		}

		public object ToFlexigridJson()
		{
			return new
			       	{
			       		total = Total,
			       		page = Page,
			       		rows = Rows
			       	};
		}

		public object ToDataTablesJson()
		{
			return new
			{
				data = Rows,
				iTotalRecords = Records,
				iTotalDisplayRecords = Records
			};
		}
	}
}