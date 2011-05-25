namespace BclExtensionMethods
{
	using System;

	public static class DisposableExtesions
	{
		/// <summary>
		/// 	null safe dispose
		/// </summary>
		public static void TryDispose(IDisposable disposable)
		{
			if (disposable == null)
			{
				return;
			}

			disposable.Dispose();
		}
	}
}