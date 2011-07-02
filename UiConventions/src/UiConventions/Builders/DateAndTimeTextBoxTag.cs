namespace HtmlTags.UI.Builders
{
	using System;
	using Extensions;
	using Constants;

	public class DateAndTimeTextBoxTag : HtmlTag
	{
		public DateAndTimeTextBoxTag(string id) : base(HtmlTagConstants.Span)
		{
			AddDateTimeInput(id);
			AddScriptToMakeClientSideDateTimePicker(id);
		}

		private void AddDateTimeInput(string id)
		{
			var input = Tags.TextBox
				.Id(id)
				.Attr(HtmlAttributeConstants.Name, id);

			Child(input);
		}

		private void AddScriptToMakeClientSideDateTimePicker(string id)
		{
			const string template =
				@"$(function() {{$('#{0}').datetime({{ americanMode: false, changeMonth:true, changeYear:true}});}})";
			var script = string.Format(template, id);
			Child(Tags.Script(script));
		}

		public DateAndTimeTextBoxTag SetDateAndTime(DateTime date)
		{
			var value = date.ToString("MM/dd/yyyy HH:mm:ss");
			Children[0].Value(value);
			return this;
		}
	}
}