namespace BclExtensionMethods.Reflection
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	public static class ReflectionUtilities
	{
		public static PropertyInfo FindPropertyFromExpression(LambdaExpression lambdaExpression)
		{
			Expression expressionToCheck = lambdaExpression;

			bool done = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:
						expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
						break;
					case ExpressionType.Lambda:
						expressionToCheck = lambdaExpression.Body;
						break;
					case ExpressionType.MemberAccess:
						var propertyInfo = ((MemberExpression)expressionToCheck).Member as PropertyInfo;
						return propertyInfo;
					default:
						done = true;
						break;
				}
			}

			return null;
		}

		public static MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> expression)
		{
			var current = expression.Body;
			return GetMemberExpression(current);
		}

		public static MemberExpression GetMemberExpression(Expression<Func<object>> expression)
		{
			var current = expression.Body;
			return GetMemberExpression(current);
		}

		private static MemberExpression GetMemberExpression(Expression current)
		{
			//Dig through unary expressions and remove them.
			while (current is UnaryExpression)
			{
				current = (current as UnaryExpression).Operand;
			}

			if (!(current is MemberExpression))
				throw new ArgumentException("expression must be a member expression");
			return current as MemberExpression;
		}
	}
}