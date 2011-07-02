namespace HtmlTags.Extensions
{
	using System;
	using Constants;
	using HtmlTags;

	public class CalendarTextBox : HtmlTag
	{
		public CalendarTextBox(string id) : base(HtmlTagConstants.Span)
		{
			AddDateInput(id);
			AddScriptToMakeClientSideDatePicker(id);
		}

		private void AddScriptToMakeClientSideDatePicker(string id)
		{
			const string template = "$(function() {{$('#{0}').datepicker({{ changeMonth:true, changeYear:true}});}})";
			var script = string.Format(template, id);
			Child(Tags.Script(script));
		}

		private void AddDateInput(string id)
		{
			var input = new TextboxTag()
				.Id(id)
				.Attr(HtmlAttributeConstants.Name, id);

			Child(input);
		}

		public CalendarTextBox SetDate(DateTime date)
		{
			var value = date.ToShortDateString();
			Children[0].Value(value);
			return this;
		}
	}
}