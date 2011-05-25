namespace BclExtensionMethods
{
	using System;
	using System.ComponentModel;

	public static class EnumExtensions
	{
		/// <summary>
		/// 	Get a description overriding regular ToString with the DescriptionAttribute if present
		/// </summary>
		public static string ToDescription(this Enum enumeration)
		{
			var type = enumeration.GetType();
			var members = type.GetMember(enumeration.ToString());

			if (members.Length > 0)
			{
				var attributes = members[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
				if (attributes.Length > 0)
				{
					return ((DescriptionAttribute) attributes[0]).Description;
				}
			}

			return enumeration.ToString();
		}
	}
}