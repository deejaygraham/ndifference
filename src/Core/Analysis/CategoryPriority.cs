using System;
using System.Diagnostics;

namespace NDifference.Analysis
{
	/// <summary>
	/// Used to give an ordering to categories in a report.
	/// </summary>
	[DebuggerDisplay("{Value}")]
	public class CategoryPriority : IEquatable<CategoryPriority>
	{
		public static readonly int Uncategorised = -1;

		public CategoryPriority()
			: this (Uncategorised)
		{
		}

		public CategoryPriority(int value)
		{
			this.Value = value;
		}

		public int Value { get; private set; }

		public bool IsValid { get { return this.Value != Uncategorised; } }

		public override string ToString()
		{
			return this.Value.ToString();
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

			return leftHandSide.Value == rightHandSide.Value;
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

			return this.Value == actualPriority.Value;
		}

		public bool Equals(CategoryPriority other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return this.Value == other.Value;
		}
	}
}
