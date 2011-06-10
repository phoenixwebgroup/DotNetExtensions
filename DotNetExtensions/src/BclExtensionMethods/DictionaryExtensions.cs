namespace BclExtensionMethods
{
	using System.Collections.Generic;

	public static class DictionaryExtensions
	{
		public static bool DoesNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			return !dictionary.ContainsKey(key);
		}

		public static bool DoesNotContainValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value)
		{
			return dictionary.ContainsValue(value);
		}

		public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dictionary, K key)
		{
			if (dictionary.ContainsKey(key))
			{
				return dictionary[key];
			}
			return default(V);
		}
	}
}