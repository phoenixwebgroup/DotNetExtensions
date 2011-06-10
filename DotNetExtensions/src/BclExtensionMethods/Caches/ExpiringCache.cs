namespace BclExtensionMethods.Caches
{
	using System;
	using System.Collections.Concurrent;
	using System.Linq;

	/// <summary>
	/// Ideas ripped off of Fubu's Cache plus the idea of an expiring cache, but it uses a concurrent dictionary.
	/// </summary>
	public class ExpiringCache<TKey, TValue>
	{
		public virtual TimeSpan ItemLifeSpan { get; set; }
		public virtual Func<TKey, TValue> OnMissingOrExpired { get; set; }

		private class CachedValue
		{
			public CachedValue(TValue value, DateTime expiresAt)
			{
				Value = value;
				ExpiresAt = expiresAt;
			}

			public TValue Value { get; set; }
			public DateTime ExpiresAt { get; set; }

			public bool IsExpired()
			{
				return ExpiresAt < DateTime.Now;
			}

			#region Equality - so TryUpdate can compare instances of cached values

			private readonly Guid _Identity = Guid.NewGuid();

			public override bool Equals(object obj)
			{
				return _Identity.Equals(obj);
			}

			public override int GetHashCode()
			{
				return _Identity.GetHashCode();
			}

			#endregion
		}

		private readonly ConcurrentDictionary<TKey, CachedValue> _Cache;

		public ExpiringCache()
		{
			_Cache = new ConcurrentDictionary<TKey, CachedValue>();
			ItemLifeSpan = default(TimeSpan);
		}

		public ExpiringCache(Func<TKey, TValue> onMissing, TimeSpan itemLifeSpan)
		{
			_Cache = new ConcurrentDictionary<TKey, CachedValue>();
			ItemLifeSpan = itemLifeSpan;
			OnMissingOrExpired = onMissing;
		}

		private CachedValue CreateCachedValue(TValue value)
		{
			return new CachedValue(value, DateTime.Now.Add(ItemLifeSpan));
		}

		public virtual void Remove(TKey key)
		{
			CachedValue value;
			_Cache.TryRemove(key, out value);
		}

		public virtual void ClearAll()
		{
			_Cache.Clear();
		}

		public virtual TValue this[TKey key]
		{
			get
			{
				var currentValue = GetCurrentValue(key);
				if (!currentValue.IsExpired())
				{
					return currentValue.Value;
				}

				var newValue = CreateCachedValue(OnMissingOrExpired(key));
				_Cache.TryUpdate(key, newValue, currentValue);
				return newValue.Value;
			}
			set { _Cache[key] = CreateCachedValue(value); }
		}

		private CachedValue GetCurrentValue(TKey key)
		{
			CachedValue currentValue;
			if (_Cache.TryGetValue(key, out currentValue))
			{
				return currentValue;
			}
			var newValue = CreateCachedValue(OnMissingOrExpired(key));
			_Cache.TryAdd(key, newValue);
			return newValue;
		}

		public virtual int Count
		{
			get { return _Cache.Count; }
		}

		public virtual bool Has(TKey key)
		{
			return _Cache.ContainsKey(key);
		}

		public virtual TKey[] GetAllKeys()
		{
			return _Cache.Keys.ToArray();
		}

		public virtual TValue[] GetAllValues()
		{
			return _Cache.Values
				.Where(c => !c.IsExpired())
				.Select(c => c.Value)
				.ToArray();
		}
	}
}