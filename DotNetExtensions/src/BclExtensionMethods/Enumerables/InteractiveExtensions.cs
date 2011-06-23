namespace BclExtensionMethods.Enumerables
{
	using System;
	using System.Collections.Generic;

	public static class InteractiveExtensions
	{
		public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
			{
				yield break;
			}
			foreach (var item in source)
			{
				action(item);
				yield return item;
			}
		}

		public static void Run<T>(this IEnumerable<T> source)
		{
			foreach (var item in source)
			{
			}
		}
	}
}