namespace MvcActionFilers.Filters
{
	using Model;

	public class NestedExceptionHandler : ExceptionHandler<NestedException>
	{
		public override int GetOrder()
		{
			return 1;
		}
	}
}