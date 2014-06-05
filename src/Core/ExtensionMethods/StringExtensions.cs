using System.Text;

namespace NDifference
{
	public static class StringExtensions
	{
		public static string SplitLongLines(this string longLine, int breakLineAt)
		{
			var builder = new StringBuilder();

			int charsInThisLine = 0;

			foreach (char thisChar in longLine)
			{
				// break at word boundaries
				if (char.IsWhiteSpace(thisChar) && charsInThisLine >= breakLineAt)
				{
					builder.AppendLine();
					charsInThisLine = 0;
				}

				builder.Append(thisChar);
				++charsInThisLine;
			}

			return builder.ToString();
		}
	}

}
