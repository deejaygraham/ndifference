using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	/// <summary>
	/// Models a way to write a report on discovered differences.
	/// </summary>
	public interface IReportWriter
	{
		IEnumerable<IReportFormat> SupportedFormats { get; }

		OutputFileMap Map { get; set; }

		//Project Project { get; set; }

		void Write(IdentifiedChangeCollection changes, IReportOutput output, IReportFormat format);
	}
}
