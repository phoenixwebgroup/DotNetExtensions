namespace HtmlTags.UI.Conventions
{
	using System.Reflection;

	public interface IPageActionsSecurity
	{
		bool UserCanAccess(MethodInfo action);
	}
}