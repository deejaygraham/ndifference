using System;
using System.Collections.ObjectModel;

namespace NDifference
{
	/// <summary>
	/// Information about internal assembly content.
	/// </summary>
	public interface IAssemblyInfo : IUniquelyIdentifiable
 	{
        string Name { get; }

        Version Version { get; }

        string RuntimeVersion { get; }

        string Architecture { get;  }

		ReadOnlyCollection<AssemblyReference> References { get; }
	}
}
