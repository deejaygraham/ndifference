using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

namespace NDifference.Plugins
{
	/// <summary>
	/// Looks for implementations of a particular type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ObjectInstantiator<T> where T : class
	{
		public List<T> CreateTypesImplementingInterface(IAssemblyReflector reflector)
		{
			List<T> objects = new List<T>();

			foreach (var foundType in reflector.GetTypesImplementing(typeof(T)))
			{
				ObjectHandle handle = Activator.CreateInstance(foundType.Assembly, foundType.FullName);
				T unwrapped = (T)handle.Unwrap();
				objects.Add(unwrapped);
			}

			return objects;
		}
	}
}
