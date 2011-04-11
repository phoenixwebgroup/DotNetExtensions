namespace BclExtensionMethods
{
	using System;

	public static class ObjectExtensions
	{
		public static bool IsNull<T>(this T instance)
			where T : class
		{
			return instance == null;
		}

		public static bool IsNotNull<T>(this T instance)
			where T : class
		{
			return instance != null;
		}

		/// <summary>
		/// 	!instance.Equals(other)
		/// </summary>
		public static bool NotEquals(this object instance, object other)
		{
			return !instance.Equals(other);
		}

		/// <summary>
		/// 	!(instance is T)
		/// </summary>
		public static bool IsNot<T>(this object instance)
		{
			return !(instance is T);
		}
	}

	public static class DisposableExtesions
	{
		/// <summary>
		/// 	null safe dispose
		/// </summary>
		public static void TryDispose(IDisposable disposable)
		{
			if (disposable == null)
			{
				return;
			}

			disposable.Dispose();
		}
	}
}