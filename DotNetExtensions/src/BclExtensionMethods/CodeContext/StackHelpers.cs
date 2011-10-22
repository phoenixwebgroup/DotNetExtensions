namespace BclExtensionMethods.CodeContext
{
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;

	public class StackHelpers
	{
		public static MethodBase GetMethodThatCalledMe()
		{
			return new StackTrace().GetFrames().Skip(2).First().GetMethod();
		}
	}
}