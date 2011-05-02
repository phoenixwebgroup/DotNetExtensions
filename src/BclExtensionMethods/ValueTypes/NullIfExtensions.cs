namespace BclExtensionMethods.ValueTypes
{
	public static class NullIfExtensions
	{
		public static T? NullIf<T>(this T? number, T value)
			where T : struct
		{
			if (number != null && number.Equals(value))
			{
				return null;
			}
			return number;
		}

		public static T? NullIf<T>(this T number, T value)
			where T : struct
		{
			return ((T?) number).NullIf(value);
		}
	}
}