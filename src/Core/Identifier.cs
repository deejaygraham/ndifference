
using System;
using System.Diagnostics;

namespace NDifference
{
	/// <summary>
	/// Models a unique identifier used in object instance reconciliation.
	/// </summary>
	[DebuggerDisplay("Id: {Value}")]
	public sealed class Identifier : IEquatable<Identifier>
	{
		public Identifier()
		{
			this.Value = "ID_" + Guid.NewGuid().ToString();
		}

		public Identifier(string value)
		{
			this.Value = value;
		}

		public string Value { get; private set; }

		public static implicit operator string(Identifier thisInstance)
		{
			return thisInstance.Value;
		}

		public static implicit operator Identifier(string id)
		{
			return new Identifier(id);
		}

		public override string ToString()
		{
			return this.Value;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <summary>
		/// Equal based on value.
		/// </summary>
		/// <param name="leftHandSide">left hand side of comparison</param>
		/// <param name="rightHandSide">right hand side of comparison</param>
		/// <returns>true if equal</returns>
		public static bool operator ==(Identifier leftHandSide, Identifier rightHandSide)
		{
			if (object.ReferenceEquals(leftHandSide, rightHandSide))
			{
				return true;
			}

			if (((object)leftHandSide == null) || ((object)rightHandSide == null))
			{
				return false;
			}

			return leftHandSide.Value == rightHandSide.Value;
		}

		public static bool operator !=(Identifier leftHandSide, Identifier rightHandSide)
		{
			return !(leftHandSide == rightHandSide);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Identifier actualIdentifier = obj as Identifier;

			if ((object)actualIdentifier == null)
			{
				return false;
			}

			return this.Value == actualIdentifier.Value;
		}

		public bool Equals(Identifier other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this.Value == other.Value;
		}
	}
}
