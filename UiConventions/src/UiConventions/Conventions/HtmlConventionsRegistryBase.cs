namespace HtmlTags.UI
{
	using FubuMVC.UI;
	using Validation;

	public class HtmlConventionsRegistryBase : HtmlConventionRegistry
	{
		public void RegisterValidationModifiers()
		{
			Editors.Modifier<RequiredValidationModifier>();
			Editors.Modifier<StringLengthValidationModifier>();
			Editors.Modifier<RangeValidationModifier>();
			Editors.Modifier<EmailValidationModifier>();
			Editors.Modifier<UrlValidationModifier>();
			Editors.Modifier<NumericValidationModifier>();
			Editors.Modifier<DateValidationModifier>();
		}
	}
}