namespace BclExtensionMethods
{
	using System;
	using System.Linq;

	public static class InExtensions
	{
		public static bool In<T>(this T source, params T[] list)
		{
			return source != null 
				&& list.Contains(source);
		}

		public static bool In(this Enum value, params Enum[] values)
		{
			return values.Any(current => current.CompareTo(value) == 0);
		}

		public static bool In(this ValueType value, params ValueType[] values)
		{
			if (value is IComparable)
			{
				return values.OfType<IComparable>().Any(v => v.CompareTo(value) == 0);
			}
			return false;
		}
	}
}