using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDifference.UnitTests
{
	public static class CodeBuildingListExtensions
	{
		public static void GenerateMemberCode(this IEnumerable<string> items, StringBuilder builder, string comment)
		{
			if (items.Any())
			{
				builder.AppendFormat("\t\t// {0}", comment);
				builder.AppendLine();
				foreach (string i in items)
				{
					builder.AppendLine("\t\t" + i);
					builder.AppendLine();
				}
			}

		}
	}
}
