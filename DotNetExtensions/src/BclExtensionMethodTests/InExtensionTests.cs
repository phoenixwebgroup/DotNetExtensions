namespace BclExtensionMethodTests
{
	using System;
	using BclExtensionMethods;
	using NUnit.Framework;

	[TestFixture]
	public class InExtensionTests : AssertionHelper
	{
		[Test]
		public void In_ValueTypes()
		{
			Expect(4.In(1, 2, 3), Is.False);
			Expect(3.In(1, 2, 3));
		}

		private enum TestEnum
		{
			A,
			B,
			C
		}

		[Test]
		public void In_Enums()
		{
			Expect(TestEnum.A.In(TestEnum.B, TestEnum.C), Is.False);
			Expect(TestEnum.A.In(TestEnum.A), Is.True);
		}

		[Test]
		public void In_Types()
		{
			Expect(typeof(DateTime).In(typeof(DateTime)));
		}
	}
}