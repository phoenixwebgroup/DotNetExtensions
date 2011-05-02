namespace BclExtensionMethods.Pagination
{
	using System;
	using System.Linq.Dynamic;

	public class InvalidSortException : Exception
	{
		public InvalidSortException(ParseException exception) : base("Invalid sort", exception)
		{
		}
	}
}