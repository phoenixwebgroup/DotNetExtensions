namespace BclExtensionMethods
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// 	For readability only
		/// </summary>
		public static bool IsNull<T>(this T instance)
			where T : class
		{
			return instance == null;
		}

		/// <summary>
		/// 	For readability only
		/// </summary>
		public static bool IsNotNull<T>(this T instance)
			where T : class
		{
			return instance != null;
		}

		/// <summary>
		/// 	!instance.Equals(other), for readability
		/// </summary>
		public static bool NotEquals(this object instance, object other)
		{
			return !instance.Equals(other);
		}

		/// <summary>
		/// 	!(instance is T), for readability
		/// </summary>
		public static bool IsNot<T>(this object instance)
		{
			return !(instance is T);
		}
	}
}