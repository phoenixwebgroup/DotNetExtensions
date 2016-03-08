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
			return new List<T>((T[]) Enum.GetValues(typeof (T)));
		}

		public static IEnumerable<T> GetValuesFromInts<T>(IEnumerable<int> intValues)
		{
			return intValues
				.Where(i => Enum.IsDefined(typeof (T), i))
				.Select(i => (T) Enum.Parse(typeof (T), i.ToString()));
		}

		public static FieldInfo[] GetOptions<T>()
		{
			return GetOptions(typeof (T));
		}

		public static FieldInfo[] GetOptions(Type enumType)
		{
			return enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
		}

		public static T? ParseEnumFromDescription<T>(this string description) where T : struct, IConvertible
		{
			try
			{
				var t = typeof (T);
				if (description.IsNullOrEmpty() || (!t.IsEnum))
				{
					return null;
				}
				T? parsedEnum;
				foreach (var item in Enum.GetNames(t))
				{
					parsedEnum = item.ParseEnumValue<T>();
					var enumDescription = (parsedEnum.Value as Enum).ToDescription().ToLowerInvariant();
					if (enumDescription == description.Trim().ToLowerInvariant())
					{
						return parsedEnum;
					}
				}
				parsedEnum = description.ParseEnumValue<T>();
				return parsedEnum;
			}
			catch (ArgumentException ex)
			{
				return new T?();
			}
			catch (OverflowException ex)
			{
				return new T?();
			}
		}
	}
}