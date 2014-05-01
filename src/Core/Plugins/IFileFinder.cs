using System.Collections.Generic;

namespace NDifference.Plugins
{
	public interface IFileFinder
	{
		IEnumerable<string> Find();
	}
}
