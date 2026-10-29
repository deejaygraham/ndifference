using System;
using System.Collections.Generic;
using NDifference.Inspection;

namespace NDifference.Plugins
{
	/// <summary>
	/// Looks for implementations of a particular type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ObjectInstantiator<T> where T : class
	{
		public List<T> CreateTypesImplementingInterface(IRuntimeTypeProvider provider)
		{
			List<T> objects = new List<T>();

			foreach (Type pluginType in provider.GetRuntimeTypesImplementing(typeof(T)))
			{
				if (!typeof(T).IsAssignableFrom(pluginType))
				{
					throw new InvalidOperationException(string.Format(
						"Type '{0}' does not implement '{1}'.",
						pluginType.FullName,
						typeof(T).FullName));
				}

				try
				{
					objects.Add((T)Activator.CreateInstance(pluginType));
				}
				catch (Exception exception)
				{
					throw new InvalidOperationException(
						string.Format(
							"Could not activate plugin type '{0}'.",
							pluginType.FullName),
						exception);
				}
			}

			return objects;
		}
	}
}
