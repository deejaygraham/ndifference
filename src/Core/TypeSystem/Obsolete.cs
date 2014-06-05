
namespace NDifference.TypeSystem
{
	/// <summary>
	/// Identifies a type or member as obsolete/deprecated.
	/// </summary>
	public class Obsolete
	{
		//public static readonly Obsolete NotObsolete = new Obsolete();

		public Obsolete()
		{
			this.Message = string.Empty;
		}

		/// <summary>
		/// Message describing reason for deprecation.
		/// </summary>
		public string Message { get; set; }
	}
}
