using System.Globalization;
using System.IO;
using System.Text;

namespace NDifference
{
	public static class FileInfoExtensions
	{
		public static string CalculateChecksum(this FileSystemInfo info)
		{
			const string HexFormat = "X2";
			var builder = new StringBuilder();

			using (FileStream fs = new FileStream(info.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
				{
					byte[] hash = md5.ComputeHash(fs);

					for (int i = 0; i < hash.Length; ++i)
					{
						builder.Append(hash[i].ToString(HexFormat, CultureInfo.InvariantCulture));
					}
				}
			}

			return builder.ToString();
		}
	}
}
