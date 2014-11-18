using System;
using System.Runtime.Serialization;

namespace NDifference.Exceptions
{
	[Serializable]
	public class InvalidProjectFileFormatException : Exception
	{
		public InvalidProjectFileFormatException()
		{
		}

		public InvalidProjectFileFormatException(string message)
			: base(message)
		{
		}

		protected InvalidProjectFileFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public InvalidProjectFileFormatException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

