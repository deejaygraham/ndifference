using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace NDifference
{
	public static class HashAlgorithmExtensions
	{
		public static string GetHash<T>(this object instance) where T : HashAlgorithm, new()
		{
			T cryptoServiceProvider = new T();

			return CalculateHash(instance, cryptoServiceProvider);
		}

		private static string CalculateHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm, new()
		{
			using (var memoryStream = new MemoryStream())
			{
				JsonSerializer.Serialize(memoryStream, instance, JsonSerializerOptions.Default);

				cryptoServiceProvider.ComputeHash(memoryStream.ToArray());

				return ToHex(cryptoServiceProvider.Hash);
			}
		}

		private static string ToHex(byte[] bytes)
		{
			var builder = new StringBuilder();

			foreach (byte b in bytes)
			{
				builder.Append(b.ToString("x2"));
			}

			return builder.ToString();
		}
	}

}
