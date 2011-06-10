namespace BclExtensionMethodTests.Caches
{
	using System;
	using BclExtensionMethods.Caches;
	using NUnit.Framework;

	[TestFixture]
	public class MoreCacheTests : AssertionHelper
	{
		[Test]
		public void Get_KeyWithNullValue_ReturnsNull()
		{
			var cache = new ExpiringCache<int, object>(k => null, TimeSpan.FromMinutes(100));

			var value = cache[1];

			Expect(value, Is.Null);
		}

		[Test]
		public void Get_KeyWithChangingValue_StaysSameWhileCached()
		{
			var cache = new ExpiringCache<int, object>(k => 1, TimeSpan.FromMinutes(100));
			var value = cache[1];
			Expect(value, Is.EqualTo(1));
			cache.OnMissingOrExpired = k => 2;

			value = cache[1];

			Expect(value, Is.EqualTo(1));
		}

		[Test]
		public void Get_ExpiredValue_LoadsNewValue()
		{
			var cache = new ExpiringCache<int, object>(k => 0, TimeSpan.FromMinutes(-1));
			cache.ItemLifeSpan = TimeSpan.FromMinutes(-1);
			var key = 1;
			var value = cache[key];
			Expect(value, Is.EqualTo(0));
			cache.OnMissingOrExpired = k => 1;

			value = cache[key];

			Expect(value, Is.EqualTo(1));
		}
	}
}