namespace mongo4log4net
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using MongoDB.Bson.Serialization.Conventions;

	public class LoggingMemberFinderConvention : IMemberFinderConvention
	{
		public Dictionary<Type, bool> TypesToIgnore = new Dictionary<Type, bool>
		                                              	{
		                                              		{typeof (IntPtr), true}
		                                              	};

		public IEnumerable<MemberInfo> FindMembers(Type type)
		{
			foreach (
				var fieldInfo in
					type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				if (fieldInfo.IsInitOnly || fieldInfo.IsLiteral || fieldInfo.Name.EndsWith("BackingField"))
				{
					// we can't write
					continue;
				}
				if (TypesToIgnore.ContainsKey(fieldInfo.FieldType))
				{
					continue;
				}
				yield return fieldInfo;
			}

			foreach (
				var propertyInfo in
					type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
				)
			{
				if (!propertyInfo.CanRead || (!propertyInfo.CanWrite && type.Namespace != null))
				{
					// we can't write or it is anonymous...
					continue;
				}

				// skip indexers
				if (propertyInfo.GetIndexParameters().Length != 0)
				{
					continue;
				}

				// skip overridden properties (they are already included by the base class)
				var getMethodInfo = propertyInfo.GetGetMethod(true);
				if (getMethodInfo.IsVirtual && getMethodInfo.GetBaseDefinition().DeclaringType != type)
				{
					continue;
				}

				if (TypesToIgnore.ContainsKey(propertyInfo.PropertyType))
				{
					continue;
				}

				yield return propertyInfo;
			}
		}
	}
}