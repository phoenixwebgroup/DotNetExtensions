namespace BclExtensionMethods.Streams
{
	using System;
	using System.IO;
	using System.Linq;

	public static class StreamExtensions
	{
		public static byte[] HexStringToByteArray(this string hex)
		{
			return Enumerable.Range(0, hex.Length)
				.Where(x => x%2 == 0)
				.Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
				.ToArray();
		}

		public static Stream HexStringToStream(this string hex)
		{
			return new MemoryStream(HexStringToByteArray(hex));
		}
	}
}