using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public interface IReportFormatRepository
	{
		void SupportedFormat(IReportFormat format);

		Collection<IReportFormat> SupportedFormats { get; }
	}
}
