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

		[Test]
		public void Get_ExpiredValue_SavesNewValueWithCorrectExpirationTime()
		{
			// this test extends just checking a new value is returned, it ensures the new value is stored in the dictionary for subsequent cached requests after initial expiration, this fixes a bug where the cache never stored the value after an initial expiration.
			var itemLifeSpan = TimeSpan.FromMinutes(1);
			var cache = new ExpiringCache<int, object>(k => null, itemLifeSpan);
			var key = default(int);
			var value = cache[key];
			var expiredValue = cache.GetCurrentValue(key);
			expiredValue.ExpiresAt = DateTime.Now.Add(-itemLifeSpan);
			Expect(expiredValue.Equals(expiredValue));
			Expect(expiredValue.IsExpired());

			var beforeLoaded = DateTime.Now;
			value = cache[key];
			var afterLoaded = DateTime.Now;

			var currentValue = cache.GetCurrentValue(key);
			Expect(currentValue != expiredValue);
			var lowerEnd = beforeLoaded.Add(itemLifeSpan);
			var upperEnd = afterLoaded.Add(itemLifeSpan);
			Expect(currentValue.ExpiresAt, Is.InRange(lowerEnd, upperEnd));
		}

		[Test]
		public void Equals_NewCachedItem_EqualsSelf()
		{
			var item = new ExpiringCache<int, int>.CachedValue(1, DateTime.Now);

			Expect(item, Is.EqualTo(item));
		}

		[Test]
		public void Equals_DifferentCachedValues_NotEqual()
		{
			var item = new ExpiringCache<int, int>.CachedValue(1, DateTime.Now);
			var otherItem = new ExpiringCache<int, int>.CachedValue(1, DateTime.Now);

			Expect(item, Is.Not.EqualTo(otherItem));
		}
	}
}