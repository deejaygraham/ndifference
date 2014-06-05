using System;
using System.Diagnostics;

namespace NDifference.Analysis
{
	/// <summary>
	/// Used to give an ordering to categories in a report.
	/// </summary>
	[DebuggerDisplay("{_value}")]
	public class CategoryPriority : IEquatable<CategoryPriority>
	{
		public static CategoryPriority InvalidValue = new CategoryPriority(-1);

		private int _value;

		public CategoryPriority(int value)
		{
			this._value = value;
		}

		public override string ToString()
		{
			return this._value.ToString();
		}

		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		/// <summary>
		/// Equal based on value.
		/// </summary>
		/// <param name="leftHandSide">left hand side of comparison</param>
		/// <param name="rightHandSide">right hand side of comparison</param>
		/// <returns>true if equal</returns>
		public static bool operator ==(CategoryPriority leftHandSide, CategoryPriority rightHandSide)
		{
			if (object.ReferenceEquals(leftHandSide, rightHandSide))
			{
				return true;
			}

			if (((object)leftHandSide == null) || ((object)rightHandSide == null))
			{
				return false;
			}

			return leftHandSide._value == rightHandSide._value;
		}

		public static bool operator !=(CategoryPriority leftHandSide, CategoryPriority rightHandSide)
		{
			return !(leftHandSide == rightHandSide);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			CategoryPriority actualPriority = obj as CategoryPriority;

			if ((object)actualPriority == null)
			{
				return false;
			}

			return this._value == actualPriority._value;
		}

		public bool Equals(CategoryPriority other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this._value == other._value;
		}
	}
}
