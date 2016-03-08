namespace BclExtensionMethods.Dynamic
{
	using System;
	using System.Reflection;

	public static class DynamicCastExtensions
	{
		private static T Cast<T>(dynamic o)
		{
			return (T)o;
		}
		///<summary></summary>
		/// <exception cref="System.InvalidCastException"></exception>
		public static dynamic DynamicCast(this Type T, dynamic o)
		{
			return typeof(DynamicCastExtensions).GetMethod("Cast", BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(T).Invoke(null, new object[] { o });
		}
	}
}