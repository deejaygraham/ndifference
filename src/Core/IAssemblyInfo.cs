using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
