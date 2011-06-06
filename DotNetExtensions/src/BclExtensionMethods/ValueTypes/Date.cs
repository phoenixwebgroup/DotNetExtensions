namespace BclExtensionMethods.ValueTypes
{
	using System;

	/// <summary>
	/// 	A date only class to avoid nuianscens with DateTime's time portion
	/// </summary>
	[Serializable]
	public struct Date : IComparable<Date>
	{
		private readonly DateTime _Date;

		public Date(string date)
		{
			_Date = Convert.ToDateTime(date);
		}

		public Date(DateTime date)
		{
			_Date = date.Date;
		}

		public Date(int year, int month, int day)
		{
			_Date = new DateTime(year, month, day);
		}

		public static Date Today
		{
			get { return new Date(DateTime.Today); }
		}

		public static Date Yesterday
		{
			get { return new Date(DateTime.Today.AddDays(-1)); }
		}

		public static implicit operator DateTime(Date date)
		{
			return date._Date;
		}

		public static implicit operator Date(DateTime date)
		{
			return new Date(date);
		}

		public int CompareTo(Date other)
		{
			if (other == this)
			{
				return 0;
			}
			if (other > this)
			{
				return 1;
			}
			return -1;
		}

		public override string ToString()
		{
			return _Date.ToShortDateString();
		}

		public static bool operator ==(Date left, Date right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Date left, Date right)
		{
			return !(left == right);
		}

		public static bool operator >(Date left, Date right)
		{
			return left._Date > right._Date;
		}

		public static bool operator <(Date left, Date right)
		{
			return left._Date < right._Date;
		}

		public static bool operator <=(Date left, Date right)
		{
			return !(left > right);
		}

		public static bool operator >=(Date left, Date right)
		{
			return !(left < right);
		}

		public string ToShortDateString()
		{
			return _Date.ToShortDateString();
		}

		public string ToString(string dateTimeFormat)
		{
			return _Date.ToString(dateTimeFormat);
		}

		public string ToString(string dateTimeFormat, IFormatProvider formatProvider)
		{
			return _Date.ToString(dateTimeFormat, formatProvider);
		}

		public Date AddDays(int days)
		{
			return _Date.AddDays(days);
		}

		public Date AddMonths(int months)
		{
			return _Date.AddMonths(months);
		}

		public Date AddYears(int years)
		{
			return _Date.AddYears(years);
		}

		public bool IsWeekday()
		{
			return _Date.IsWeekday();
		}

		public bool IsWeekend()
		{
			return _Date.IsWeekend();
		}
	}
}