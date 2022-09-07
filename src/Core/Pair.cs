namespace NDifference
{
	/// <summary>
	/// Represents a pair of objects of the same type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Pair<T>
	{
		public Pair(T first, T second)
		{
			this.First = first;
			this.Second = second;
		}

		public T First { get; set; }

		public T Second { get; set; }

		public static Pair<T> MakePair(T one, T two)
		{
			return new Pair<T>(one, two);
		}
	}
}
