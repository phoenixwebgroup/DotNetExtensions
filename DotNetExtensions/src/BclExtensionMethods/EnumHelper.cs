namespace BclExtensionMethods
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	// todo need some explanation

	public static class EnumHelper
	{
		public static IEnumerable<T> GetValuesFor<T>()
		{
			return new List<T>((T[])Enum.GetValues(typeof(T)));
		}

		public static IEnumerable<T> GetValuesFromInts<T>(IEnumerable<int> intValues)
		{
			return intValues
				.Where(i => Enum.IsDefined(typeof(T), i))
				.Select(i => (T)Enum.Parse(typeof(T), i.ToString()));
		}

		public static FieldInfo[] GetOptions<T>()
		{
			return GetOptions(typeof (T));
		}

		public static FieldInfo[] GetOptions(Type enumType)
		{
			return enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
		}
	}
}