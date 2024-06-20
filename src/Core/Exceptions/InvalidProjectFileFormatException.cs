using System;
using System.Runtime.Serialization;

namespace NDifference.Exceptions
{
	public class InvalidProjectFileFormatException : Exception
	{
		public InvalidProjectFileFormatException()
		{
		}

		public InvalidProjectFileFormatException(string message)
			: base(message)
		{
		}

		public InvalidProjectFileFormatException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

