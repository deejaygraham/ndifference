using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	/// <summary>
	/// Responsible for creating a tangible report artefact. Usually on disk.
	/// </summary>
	public interface IReportOutput
	{
		//string Folder { get; }

		string Path { get; }

		void Execute(string reportContent);
	}
}
