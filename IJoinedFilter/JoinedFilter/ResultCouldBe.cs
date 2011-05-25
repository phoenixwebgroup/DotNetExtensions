namespace JoinedFilter
{
	using System;
	using System.Web.Mvc;

	public static class ResultCouldBe
	{
		private const bool JoinToNonReflectedActions = true;

		public static bool ViewResult(ActionDescriptor actionDescriptor)
		{
			if (!(actionDescriptor is ReflectedActionDescriptor))
			{
				return JoinToNonReflectedActions;
			}

			var returnType = (actionDescriptor as ReflectedActionDescriptor).MethodInfo.ReturnType;

			return JoinToViewResultBaseTypes(returnType)
			       || JoinToTypesThatCanHoldViewResultBase(returnType);
		}

		private static bool JoinToTypesThatCanHoldViewResultBase(Type returnType)
		{
			return returnType.IsAssignableFrom(typeof(ViewResultBase));
		}

		private static bool JoinToViewResultBaseTypes(Type returnType)
		{
			return typeof(ViewResultBase).IsAssignableFrom(returnType);
		}
	}
}