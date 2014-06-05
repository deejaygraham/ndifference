using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NDifference.Projects
{
	[Serializable]
	public class PersistableProjectSettings
	{
		public string OutputFolder { get; set; }

		public string IndexName { get; set; }

		[XmlElement("SubFolder")]
		public string Subfolder { get; set; }

		public bool ConsolidateAssemblyTypes { get; set; }

		[XmlIgnore]
		public string HeadTag { get; set; }

		[XmlElement("HeadTag")]
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public XmlCDataSection HeadTagAsCData
		{
			get
			{
				return ToCData(this.HeadTag);
			}

			set
			{
				this.HeadTag = FromCData(value);
			}
		}

		[XmlIgnore]
		public string StyleTag { get; set; }

		[XmlElement("StyleTag")]
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "StyleTagAsCData")]
		public XmlCDataSection StyleTagAsCData
		{
			get
			{
				return ToCData(this.StyleTag);
			}

			set
			{
				this.StyleTag = FromCData(value);
			}
		}

		public string SummaryTitle { get; set; }

		[XmlIgnore]
		public string HeadingText { get; set; }

		[XmlElement("HeadingText")]
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "HeadingTextAsCData")]
		public XmlCDataSection HeadingTextAsCData
		{
			get
			{
				return ToCData(this.HeadingText);
			}

			set
			{
				this.HeadingText = FromCData(value);
			}
		}

		[XmlIgnore]
		public string FooterText { get; set; }

		[XmlElement("FooterText")]
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public XmlCDataSection FooterTextAsCData
		{
			get
			{
				return ToCData(this.FooterText);
			}

			set
			{
				this.FooterText = FromCData(value);
			}
		}

		public string ReportFormat { get; set; }

		public string ApplicationName { get; set; }

		public string ApplicationLink { get; set; }

		public string ApplicationVersion { get; set; }

		private static XmlCDataSection ToCData(string text)
		{
			var dummy = new XmlDocument();
			return dummy.CreateCDataSection(text);
		}

		private static string FromCData(XmlCDataSection value)
		{
			if (value == null)
			{
				return String.Empty;
			}

			return value.Value;
		}
	}
}
