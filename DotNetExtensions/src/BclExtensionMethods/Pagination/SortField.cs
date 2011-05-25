namespace BclExtensionMethods.Pagination
{
	using System;

	[Serializable]
	public class SortField
	{
		public SortField(string expression, SortDirectionType direction)
		{
			SortDirection = direction;
			SortExpression = expression;
		}

		public SortField(string expression, string direction)
		{
			SortDirection = direction.ParseEnumValue<SortDirectionType>() ?? SortDirectionType.ASC;
			SortExpression = expression;
		}

		public SortDirectionType SortDirection { get; set; }

		public string SortExpression { get; set; }

		public enum SortDirectionType
		{
			ASC,
			DESC,
		}

		public string ToSortString()
		{
			return SortExpression + " " + SortDirection;
		}
	}
}