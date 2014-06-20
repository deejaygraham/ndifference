using NDifference.SourceFormatting;
using System;
using System.Text;

namespace NDifference.TypeSystem
{
	/// <summary>
	/// A field belonging to a class.
	/// </summary>
	[Serializable]
	public class MemberField : IMemberInfo, IMatchExactly<MemberField>, IMatchFuzzily<MemberField>
	{
		public string Name { get; set; }

		public FullyQualifiedName FieldType { get; set; }

		public MemberAccessibility Accessibility { get; set; }

		public InstanceAccessModifier InstanceAccess { get; set; }

		public Obsolete ObsoleteMarker { get; set; }

		public bool IsInstance
		{
			get
			{
				return this.InstanceAccess == InstanceAccessModifier.Instance;
			}

			set
			{
				this.InstanceAccess = value ? InstanceAccessModifier.Instance : InstanceAccessModifier.Static;
			}
		}

		public bool IsStatic
		{
			get
			{
				return this.InstanceAccess == InstanceAccessModifier.Static;
			}

			set
			{
				this.InstanceAccess = value ? InstanceAccessModifier.Static : InstanceAccessModifier.Instance;
			}
		}

		public bool IsReadOnly { get; set; }

		public ICoded ToCode()
		{
			SourceCode code = new SourceCode();

			code.Add(this.FieldType.ToCode());
			code.Add(new IdentifierTag(this.Name));

			return code;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			if (this.IsStatic)
				builder.Append("static ");

			if (this.IsReadOnly)
				builder.Append("readonly ");

			builder.AppendFormat("{0} {1} ", this.FieldType.Type, this.Name);

			return builder.ToString();
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public bool ExactlyMatches(MemberField other)
		{
			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.Ordinal) == 0;
		}
		
		public bool FuzzyMatches(MemberField other)
		{
			// matches if the names are the same
			return string.Compare(
				this.Name,
				other.Name,
				StringComparison.Ordinal) == 0;
		}
	}
}
