using System;
using System.Runtime.Serialization;

namespace NDifference.Exceptions
{
	public class PluginLoadException : Exception
	{
		public PluginLoadException()
		{
		}

		public PluginLoadException(string message)
			: base(message)
		{
		}

		public PluginLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
