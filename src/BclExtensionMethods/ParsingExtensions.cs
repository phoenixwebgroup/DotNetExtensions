namespace BclExtensionMethods
{
	using System;
	using System.Globalization;

	public static class ParsingExtensions
	{
		/// <summary>
		/// 	This method converts an object to the specified non-nullable type if it is not null, otherwise it returns null
		/// 	if the conversion fails or the object is null to begin with.
		/// 	Acceptable types include (byte, short, int, long, double, decimal, and DateTime).
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "value"></param>
		/// <returns></returns>
		public static T? Parse<T>(this object value) where T : struct, IConvertible
		{
			return ParseValue<T>(value);
		}

		/// <summary>
		/// 	This method converts an object to the specified non-nullable type if it is not null, otherwise it returns null
		/// 	if the conversion fails or the object is null to begin with.
		/// 	Acceptable types include (byte, short, int, long, double, decimal, and DateTime).
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "value"></param>
		/// <returns></returns>
		public static T? ParseValue<T>(object value) where T : struct, IConvertible
		{
			return ParseValue<T>(value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// 	Parse a value from the object type to the specified output type.
		/// 	Acceptable types include (byte, short, int, long, double, decimal, and DateTime).
		/// </summary>
		/// <typeparam name = "T"></typeparam>
		/// <param name = "value"></param>
		/// <param name = "culture"></param>
		/// <returns></returns>
		public static T? ParseValue<T>(object value, CultureInfo culture) where T : struct, IConvertible
		{
			if (value == null)
			{
				return null;
			}

			object tempResult = null;
			var result = default(T);
			if (result is byte)
			{
				tempResult = value.ParseByte();
			}
			else if (result is short)
			{
				tempResult = value.ParseShort();
			}
			else if (result is int)
			{
				tempResult = value.ParseInt();
			}
			else if (result is long)
			{
				tempResult = value.ParseLong();
			}
			else if (result is decimal)
			{
				tempResult = value.ParseDecimal();
			}
			else if (result is double)
			{
				tempResult = value.ParseDouble();
			}
			else if (result is DateTime)
			{
				tempResult = value.ParseDateTime();
			}

			if (tempResult != null)
			{
				return (T) Convert.ChangeType(tempResult, typeof (T), culture);
			}

			return null;
		}

		public static byte? ParseByte(this object input)
		{
			return input.ParseByte(NumberStyles.Any, CultureInfo.CurrentUICulture.NumberFormat);
		}

		public static byte? ParseByte(this object input, NumberStyles style, IFormatProvider provider)
		{
			if (input != null)
			{
				byte temp;
				if (input is byte)
				{
					return (byte?) input;
				}
				if (byte.TryParse(input.ToString(), style, provider, out temp))
				{
					return temp;
				}
			}
			return null;
		}

		public static short? ParseShort(this object input)
		{
			return input.ParseShort(NumberStyles.Any, CultureInfo.CurrentUICulture.NumberFormat);
		}

		public static short? ParseShort(this object input, NumberStyles style, IFormatProvider provider)
		{
			if (input != null)
			{
				short temp;
				if (input is short)
				{
					return (short?) input;
				}
				if (short.TryParse(input.ToString(), style, provider, out temp))
				{
					return temp;
				}
			}
			return null;
		}

		public static int? ParseInt(this object input)
		{
			return input.ParseInt(NumberStyles.Any, CultureInfo.CurrentUICulture.NumberFormat);
		}

		public static int? ParseInt(this object input, NumberStyles style, IFormatProvider provider)
		{
			if (input != null)
			{
				int temp;
				if (input is int)
				{
					return (int?) input;
				}
				if (int.TryParse(input.ToString(), style, provider, out temp))
				{
					return temp;
				}
			}
			return null;
		}

		public static long? ParseLong(this object input)
		{
			return input.ParseLong(NumberStyles.Any, CultureInfo.CurrentUICulture.NumberFormat);
		}

		public static long? ParseLong(this object input, NumberStyles style, IFormatProvider provider)
		{
			if (input != null)
			{
				long temp;
				if (input is long)
				{
					return (long?) input;
				}
				if (long.TryParse(input.ToString(), style, provider, out temp))
				{
					return temp;
				}
			}
			return null;
		}

		public static double? ParseDouble(this object input)
		{
			return input.ParseDouble(NumberStyles.Any, CultureInfo.CurrentUICulture.NumberFormat);
		}

		public static double? ParseDouble(this object input, NumberStyles style, IFormatProvider provider)
		{
			if (input != null)
			{
				double temp;
				if (input is double)
				{
					return (double?) input;
				}
				if (double.TryParse(input.ToString(), style, provider, out temp))
				{
					return temp;
				}
			}
			return null;
		}

		public static decimal? ParseDecimal(this object input)
		{
			return input.ParseDecimal(NumberStyles.Any, CultureInfo.CurrentUICulture.NumberFormat);
		}

		public static decimal? ParseDecimal(this object input, NumberStyles style, IFormatProvider provider)
		{
			if (input != null)
			{
				decimal temp;
				if (input is decimal)
				{
					return (decimal?) input;
				}
				if (decimal.TryParse(input.ToString(), style, provider, out temp))
				{
					return temp;
				}
			}
			return null;
		}

		public static DateTime? ParseDateTime(this object input)
		{
			return input.ParseDateTime(CultureInfo.CurrentUICulture.DateTimeFormat, DateTimeStyles.None);
		}

		public static DateTime? ParseDateTime(this object date, IFormatProvider provider, DateTimeStyles styles)
		{
			if (date != null)
			{
				DateTime temp;
				if (date is DateTime)
				{
					return (DateTime?) date;
				}
				if (DateTime.TryParse(date.ToString(), provider, styles, out temp))
				{
					return temp;
				}
			}
			return null;
		}

		/// <summary>
		/// "0" is treated as false
		/// "1" is treated as true
		/// </summary>
		public static bool? ParseBoolean(this object value)
		{
			if (value != null)
			{
				if (value is bool)
				{
					return (bool?) value;
				}

				bool temp;
				var valueString = value.ToString();
				if (bool.TryParse(valueString, out temp))
				{
					return temp;
				}
				if (valueString == "0")
				{
					return false;
				}
				if (valueString == "1")
				{
					return true;
				}
			}
			return null;
		}

		/// <summary>
		///		Convert a string to a character or null if it's not a charater.
		/// </summary>
		public static char? ParseChar(this string input)
		{
			char newChar;
			if (input != null && char.TryParse(input, out newChar))
			{
				return newChar;
			}
			return null;
		}

		/// <summary>
		/// 	null safe ToString()
		/// </summary>
		public static string ParseString(this object value)
		{
			if (value == null)
			{
				return null;
			}

			return value.ToString();
		}

		public static Guid? ParseGuid(this object value)
		{
			if (value != null)
			{
				var text = value.ToString();
				if (!String.IsNullOrEmpty(text))
				{
					Guid parsed;
					if (Guid.TryParse(text, out parsed))
					{
						return parsed;
					}
				}
			}
			return null;
		}
	}
}