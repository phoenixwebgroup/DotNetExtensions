namespace BclExtensionMethods.Concurrency
{
	using System.Collections;
	using System.Collections.Concurrent;
	using System.Collections.Generic;

	/// <summary>
	/// 	Concurrent collection built on top of a concurrent dictionary.
	/// </summary>
	public class ConcurrentCollection<T> : IEnumerable<T>
	{
		private readonly ConcurrentDictionary<T, bool> _ConcurrentDictionary;

		public ConcurrentCollection()
		{
			_ConcurrentDictionary = new ConcurrentDictionary<T, bool>();
		}

		public bool TryAdd(T item)
		{
			return _ConcurrentDictionary.TryAdd(item, true);
		}

		public bool TryRemove(T item)
		{
			bool value;
			return _ConcurrentDictionary.TryRemove(item, out value);
		}

		public bool Contains(T item)
		{
			return _ConcurrentDictionary.ContainsKey(item);
		}

		public void Clear()
		{
			_ConcurrentDictionary.Clear();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _ConcurrentDictionary.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}