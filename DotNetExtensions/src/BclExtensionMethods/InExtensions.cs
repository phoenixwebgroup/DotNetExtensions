namespace BclExtensionMethods
{
	using System.Linq;

	public static class InExtensions
	{
		public static bool In<T>(this T value, params T[] values)
		{
			return values.ToList().Any(v => value.Equals(v));
		}
	}
}