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

		public static string HtmlSafeTypeName(this string fullyQualifiedName)
		{
			const string XmlLessThan = "<";
			const string HtmlLessThan = "&lt;";
			const string XmlGreaterThan = ">";
			const string HtmlGreaterThan = "&gt;";

			return fullyQualifiedName
				.Replace(XmlLessThan, HtmlLessThan)
				.Replace(XmlGreaterThan, HtmlGreaterThan);
		}
	}

}
