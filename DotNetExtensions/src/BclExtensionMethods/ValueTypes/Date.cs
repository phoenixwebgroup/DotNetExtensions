namespace BclExtensionMethods.ValueTypes
{
	using System;

	/// <summary>
	/// 	A date only class to avoid nuianscens with DateTime's time portion
	/// </summary>
	public struct Date
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
	}
}