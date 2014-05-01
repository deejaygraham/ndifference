using System;
using System.Text;

namespace NDifference
{
	/// <summary>
	/// A field belonging to a class.
	/// </summary>
	public class FieldDeclaration
	{
		public string Name { get; set; }

		public FullyQualifiedName Type { get; set; }

		public InstanceAccessModifier Access { get; set; }

		public bool IsInstance
		{
			get
			{
				return this.Access == InstanceAccessModifier.Instance;
			}

			set
			{
				this.Access = value ? InstanceAccessModifier.Instance : InstanceAccessModifier.Static;
			}
		}

		public bool IsStatic
		{
			get
			{
				return this.Access == InstanceAccessModifier.Static;
			}

			set
			{
				this.Access = value ? InstanceAccessModifier.Static : InstanceAccessModifier.Instance;
			}
		}

		public bool IsReadOnly { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder();

			if (this.IsStatic)
				builder.Append("static ");

			if (this.IsReadOnly)
				builder.Append("readonly ");

			builder.AppendFormat("{0} {1} ", this.Type.Type, this.Name);

			return builder.ToString();
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}
	}
}
