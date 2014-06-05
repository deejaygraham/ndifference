using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class ReportFormatRepository : IReportFormatRepository
	{
		private List<IReportFormat> _formats = new List<IReportFormat>();

		public void SupportedFormat(IReportFormat format)
		{
			this._formats.Add(format);
		}

		public Collection<IReportFormat> SupportedFormats
		{
			get
			{
				return new Collection<IReportFormat>(this._formats);
			}
		}
	}
}
