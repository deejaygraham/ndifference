using System.Globalization;

namespace NDifference
{
	/// <summary>
	/// Reference to another assembly.
	/// </summary>
	public class AssemblyReference
	{
		public string Name { get; set; }

		public string Version { get; set; }

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentUICulture, "{0} {1}", this.Name, this.Version);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Version.GetHashCode();
		}
	}
}
