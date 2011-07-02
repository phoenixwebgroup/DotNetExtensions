namespace HtmlTags.UI
{
	using System.IO;
	/// <summary>
	/// Overrides Microsoft's tag writer setting that replaces regular quotes with encoded-quotes inside attributes.
	/// To use this in your project, ensure that you hve added "<httpRuntime encoderType="HtmlAttributeEncodingNot"/>" to web.config under "<system.web>"
	/// </summary>
	public class HtmlAttributeEncodingNot : System.Web.Util.HttpEncoder
	{
		protected override void HtmlAttributeEncode(string value, TextWriter output)
		{
			output.Write(value);
		}
	}
}