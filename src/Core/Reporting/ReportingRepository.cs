using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	/// <summary>
	/// Simple reporting repository.
	/// </summary>
	public class ReportingRepository : IReportingRepository
	{
		private List<IReportWriter> reportWriters = new List<IReportWriter>();

		public ReadOnlyCollection<IReportWriter> ReportWriters
		{
			get
			{
				return new ReadOnlyCollection<IReportWriter>(this.reportWriters);
			}
		}

		public IReportWriter Find(IReportFormat supportedFormat)
		{
			Debug.Assert(supportedFormat != null, "supportedFormat cannot be null");

			return this.reportWriters.FirstOrDefault(w => w.SupportedFormats.FirstOrDefault(f => f.Supports(supportedFormat)) != null);
		}

		public IReportWriter Find(string supportedFormat)
		{
			Debug.Assert(supportedFormat != null, "supportedFormat cannot be null");

			return this.reportWriters.FirstOrDefault(w => w.SupportedFormats.FirstOrDefault(f => f.Supports(supportedFormat)) != null);
		}

		public void Add(IReportWriter writer)
		{
			Debug.Assert(writer != null, "Writer cannot be null");

			this.reportWriters.Add(writer);
		}

		public void AddRange(IEnumerable<IReportWriter> writers)
		{
			Debug.Assert(writers != null, "Writer collection cannot be null");

			foreach (var writer in writers)
			{
				this.reportWriters.Add(writer);
			}
		}

		public IReportFormatRepository ReportFormats
		{
			get
			{
				var repo = new ReportFormatRepository();

				foreach (var writer in reportWriters)
				{
					foreach (var format in writer.SupportedFormats)
					{
						repo.SupportedFormat(format);
					}
				}

				return repo;
			}
		}
	}
}
