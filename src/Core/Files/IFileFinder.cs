using System.Collections.Generic;

namespace NDifference
{
	public interface IFileFinder
	{
		string Filter { get; set; }

		IEnumerable<string> Find();
	}
}
