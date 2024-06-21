using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace NDifference
{
	public static class HashAlgorithmExtensions
	{
		public static string GetHash(this object instance, HashAlgorithm cryptoServiceProvider)
		{
			return CalculateHash(instance, cryptoServiceProvider);
		}

		private static string CalculateHash(object instance, HashAlgorithm cryptoServiceProvider)
        {
            byte[] asBytes = ToBytes(instance);
            byte[] hash = cryptoServiceProvider.ComputeHash(asBytes);

            return ToHex(hash);
		}

        private static byte[] ToBytes(object instance)
        {
            using (var memoryStream = new MemoryStream())
            {
                var s = JsonSerializer.Serialize(instance);

                JsonSerializer.Serialize(memoryStream, instance, JsonSerializerOptions.Default);

                return memoryStream.ToArray();
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
