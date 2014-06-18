using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
