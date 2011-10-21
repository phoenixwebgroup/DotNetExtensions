namespace BclExtensionMethods.Dynamic
{
	using System.Dynamic;
	using System.Linq;
	using System.Reflection;

	// todo I stole this code from some blog, need to find it and attribute it here

	/// <summary>
	/// Wrapper to easily access privates via dynamic types, mostly for testing purposes
	/// </summary>
	public class AccessPrivates : DynamicObject
	{
		private readonly object _Wrapped;

		private static BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		public AccessPrivates(object wrappedObject)
		{
			_Wrapped = wrappedObject;
		}

		/// <summary>
		/// 	Create an instance via the constructor matching the args
		/// </summary>
		public static dynamic FromType(Assembly assembly, string typeName, params object[] args)
		{
			var allTypes = assembly.GetTypes();
			var type = allTypes.First(item => item.Name == typeName);

			var types = from a in args
			            select a.GetType();

			//Gets the constructor matching the specified set of args
			var ctor = type.GetConstructor(flags, null, types.ToArray(), null);

			if (ctor != null)
			{
				var instance = ctor.Invoke(args);
				return new AccessPrivates(instance);
			}

			return null;
		}

		/// <summary>
		/// 	Try invoking a method
		/// </summary>
		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			var types = from a in args
			            select a.GetType();

			var method = _Wrapped.GetType().GetMethod
				(binder.Name, flags, null, types.ToArray(), null);

			if (method != null)
			{
				result = method.Invoke(_Wrapped, args);
				return true;
			}
			return base.TryInvokeMember(binder, args, out result);
		}

		/// <summary>
		/// 	Tries to get a property or field with the given name
		/// </summary>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			var prop = _Wrapped.GetType().GetProperty(binder.Name, flags);
			if (prop != null)
			{
				result = prop.GetValue(_Wrapped, null);
				return true;
			}
			var fld = _Wrapped.GetType().GetField(binder.Name, flags);
			if (fld != null)
			{
				result = fld.GetValue(_Wrapped);
				return true;
			}
			return base.TryGetMember(binder, out result);
		}

		/// <summary>
		/// 	Tries to set a property or field with the given name
		/// </summary>
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			var property = _Wrapped.GetType().GetProperty(binder.Name, flags);
			if (property != null)
			{
				property.SetValue(_Wrapped, value, null);
				return true;
			}
			var field = _Wrapped.GetType().GetField(binder.Name, flags);
			if (field != null)
			{
				field.SetValue(_Wrapped, value);
				return true;
			}
			return base.TrySetMember(binder, value);
		}
	}

	public static class PrivateExtensions
	{
		public static dynamic AccessPrivates(this object thing)
		{
			return new AccessPrivates(thing);
		}
	}
}