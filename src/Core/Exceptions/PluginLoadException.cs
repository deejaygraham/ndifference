using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

		public PluginLoadException(string pluginPath, string operation, Exception innerException)
			: base(BuildMessage(pluginPath, operation, innerException), innerException)
		{
			this.PluginPath = pluginPath;
			this.Operation = operation;
		}

		public string PluginPath { get; private set; }

		public string Operation { get; private set; }

		private static string BuildMessage(
			string pluginPath,
			string operation,
			Exception exception)
		{
			string message = string.Format(
				"Failed to {0} plugin assembly '{1}'.",
				operation,
				pluginPath);
			string details = string.Join(
				" ",
				GetDiagnosticMessages(exception).Distinct(StringComparer.Ordinal));

			if (!string.IsNullOrWhiteSpace(details))
			{
				message += " " + details;
			}

			return message;
		}

		private static IEnumerable<string> GetDiagnosticMessages(Exception exception)
		{
			if (exception == null)
			{
				yield break;
			}

			var aggregateException = exception as AggregateException;
			if (aggregateException != null)
			{
				foreach (Exception innerException in aggregateException.Flatten().InnerExceptions)
				{
					foreach (string message in GetDiagnosticMessages(innerException))
					{
						yield return message;
					}
				}

				yield break;
			}

			var reflectionException = exception as ReflectionTypeLoadException;
			if (reflectionException != null)
			{
				foreach (Exception loaderException in reflectionException.LoaderExceptions)
				{
					foreach (string message in GetDiagnosticMessages(loaderException))
					{
						yield return message;
					}
				}

				yield break;
			}

			var invocationException = exception as TargetInvocationException;
			if (invocationException != null && invocationException.InnerException != null)
			{
				foreach (string message in GetDiagnosticMessages(invocationException.InnerException))
				{
					yield return message;
				}

				yield break;
			}

			var missingFileException = exception as FileNotFoundException;
			if (missingFileException != null)
			{
				yield return string.IsNullOrWhiteSpace(missingFileException.FileName)
					? "A plugin dependency is missing. " + missingFileException.Message
					: string.Format(
						"Missing plugin dependency or assembly '{0}'.",
						missingFileException.FileName);
			}
			else
			{
				var badImageException = exception as BadImageFormatException;
				if (badImageException != null)
				{
					yield return string.IsNullOrWhiteSpace(badImageException.FileName)
						? "The plugin or one of its dependencies has an incompatible binary format."
						: string.Format(
							"The plugin dependency or assembly '{0}' has an incompatible binary format.",
							badImageException.FileName);
				}
				else
				{
					var fileLoadException = exception as FileLoadException;
					if (fileLoadException != null && !string.IsNullOrWhiteSpace(fileLoadException.FileName))
					{
						yield return string.Format(
							"Plugin dependency or assembly '{0}' could not be loaded. {1}",
							fileLoadException.FileName,
							fileLoadException.Message);
					}
					else if (!string.IsNullOrWhiteSpace(exception.Message))
					{
						yield return exception.Message;
					}
				}
			}

			if (exception.InnerException != null)
			{
				foreach (string message in GetDiagnosticMessages(exception.InnerException))
				{
					yield return message;
				}
			}
		}
	}
}
