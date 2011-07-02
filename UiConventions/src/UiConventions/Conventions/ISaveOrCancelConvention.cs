namespace HtmlTags.UI.Conventions
{
	using System;
	using Extensions;

	public interface ISaveOrCancelConvention
	{
		HtmlTag SaveOrCancel();
		HtmlTag SubmitOrCancel(string submitText);
	}

	public class SaveButtonOrCancelLinkConvention : ISaveOrCancelConvention
	{
		public HtmlTag SaveOrCancel()
		{
			return SubmitOrCancel(SaveOrCancelConvention.DefaultSaveText);
		}

		public HtmlTag SubmitOrCancel(string submitText)
		{
			var sumbit = ViewConventionExtensions.SubmitButton(submitText)
				.AddClass("positive");
			var or = Tags.Span.Text(" or ");
			var cancel = Tags.Link
				.OnClick("$.ajaxFormsExtensions.AjaxForm.Cancel();")
				.Text("cancel");

			return Tags.Div.AddClass("saveOrCancel").Nest(sumbit, or, cancel);
		}
	}

	public class SaveOrCancelButtonsConvention : ISaveOrCancelConvention
	{
		public HtmlTag SaveOrCancel()
		{
			return SubmitOrCancel(SaveOrCancelConvention.DefaultSaveText);
		}

		public HtmlTag SubmitOrCancel(string submitText)
		{
			var submit = ViewConventionExtensions.SubmitButton(submitText)
				.AddClass("positive");
			var cancel = ViewConventionExtensions.Button(SaveOrCancelConvention.DefaultCancelText)
				.Attr("onclick", "$.ajaxFormsExtensions.AjaxForm.Cancel();");

			return Tags.Div.AddClass("saveOrCancel").Nest(submit, cancel);
		}
	}

	public static class SaveOrCancelConvention
	{
		public static string DefaultSaveText = "Save";
		public static string DefaultCancelText = "Cancel";

		public static ISaveOrCancelConvention Convention { get; set; }

		public static HtmlTag SaveOrCancel()
		{
			return Convention.SaveOrCancel();
		}

		public static HtmlTag SubmitOrCancel(string submitText)
		{
			return Convention.SaveOrCancel();
		}
	}
}