
using System;
using System.Text;

namespace NDifference
{
	/// <summary>
	/// A property of a class.
	/// </summary>
	public class PropertyDeclaration
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

		public bool IsReadWrite
		{
			get { return this.HasGetter && this.HasSetter; }
		}

		public bool IsReadOnly
		{
			get { return this.HasGetter && !this.HasSetter; }
		}

		public bool IsWriteOnly
		{
			get { return this.HasSetter && !this.HasGetter; }
		}

		public bool HasGetter { get; set; }

		public bool HasSetter { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.AppendFormat("{0} ", this.Type.Type);
			builder.AppendFormat("{0} {{ ", this.Name);

			if (this.HasGetter)
			{
				builder.Append("get; ");
			}

			if (this.HasSetter)
			{
				builder.Append("set; ");
			}

			builder.Append("} ");

			return builder.ToString();
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}
	}
}