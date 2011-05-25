namespace MvcActionFilers.Filters
{
	using Model;

	public class ExceptionBaseHandler : ExceptionHandler<ExceptionBase>
	{
		public override int GetOrder()
		{
			return 0;
		}
	}
}