namespace HtmlTags.UI.Scaffolding
{
	public interface IAmAForm : IAmAView
	{
	}

	public interface IAmAView
	{
		HtmlTag View();
	}
}