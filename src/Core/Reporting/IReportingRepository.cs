using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public interface IReportingRepository
	{
		ReadOnlyCollection<IReportWriter> ReportWriters { get; }

		void Add(IReportWriter writer);

		void AddRange(IEnumerable<IReportWriter> writers);

		IReportFormatRepository ReportFormats { get; }

		void Find(IFileFinder finder);

		IReportWriter Find(IReportFormat supportedFormat);

		IReportWriter Find(string supportedFormat);
	}
}
