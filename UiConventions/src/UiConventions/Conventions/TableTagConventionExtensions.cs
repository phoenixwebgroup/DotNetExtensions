namespace HtmlTags.UI
{
	using System;
	using System.Linq.Expressions;
	using HtmlTags;
	using Extensions;

	public static class TableTagConventionExtensions
	{
		public static HtmlTag DisplayCell<T>(this TableRowTag row, T model, Expression<Func<T, object>> expression)
			where T : class
		{
			var cellContents = model.DisplayFor(expression);
			var cell = row.Cell();
			cell.Nest(cellContents);
			return cell;
		}

		public static HtmlTag DisplayCell<T>(this TableRowTag row, T model, Expression<Func<T, object>> expression,
		                                         string prepend)
			where T : class
		{
			var cellContents = model.DisplayFor(expression);
			var cell = row.Cell();
			cell.Nest(cellContents).Prepend(prepend);
			return cell;
		}

		public static HtmlTag InputCell<T>(this TableRowTag row, T model, Expression<Func<T, object>> expression)
			where T : class
		{
			var cellContents = model.InputFor(expression);
			var cell = row.Cell();
			cell.Nest(cellContents);
			return cell;
		}

		public static HtmlTag InputCell<T>(this TableRowTag row, T model, Expression<Func<T, object>> expression,
		                                       string prepend)
			where T : class
		{
			var cellContents = model.InputFor(expression);
			var cell = row.Cell(); 
			cell.Nest(cellContents).Prepend(prepend);
			return cell;
		}
	}
}