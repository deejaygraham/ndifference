using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	[Serializable]
	public class MemberEvent : IMemberInfo, ISourceCodeProvider, IMatchExactly<MemberEvent>, IMatchFuzzily<MemberEvent>
	{
		public string Name { get; set; }

		public FullyQualifiedName EventType { get; set; }

		public MemberAccessibility Accessibility { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(this.EventType.ToCode());
			code.Add(new IdentifierTag(this.Name));

			return code;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.AppendFormat("event {0} **{1}** ", this.EventType.Type, this.Name);

			return builder.ToString();
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public bool ExactlyMatches(MemberEvent other)
		{
			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.Ordinal) == 0;
		}

		public bool FuzzyMatches(MemberEvent other)
		{
			// matches if the names are the same
			return string.Compare(
				this.Name,
				other.Name,
				StringComparison.Ordinal) == 0;
		}
	}
}
