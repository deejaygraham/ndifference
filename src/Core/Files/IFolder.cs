using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	public interface IFolder
	{
		string FullPath { get; }

		string TrailingSlashPath { get; }
	}
}
