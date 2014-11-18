using System;
using System.Runtime.Serialization;

namespace NDifference.Exceptions
{
	[Serializable]
	public class PluginLoadException : Exception
	{
		public PluginLoadException()
		{
		}

		public PluginLoadException(string message)
			: base(message)
		{
		}

		protected PluginLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public PluginLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
