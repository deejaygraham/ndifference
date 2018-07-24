using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace NDifference.Projects
{
	[Serializable]
	[XmlRoot("NDifferenceProject")]
	public class PersistableProject : IUniquelyIdentifiable
	{
		public PersistableProject()
		{
			this.Identifier = new Identifier().ToString();
			this.Version = "1.0";
			this.Settings = new PersistableProjectSettings();

			this.SourceAssemblies = new List<string>();
			this.TargetAssemblies = new List<string>();
		}

		[XmlAttribute("ID")]
		public string Identifier { get; set; }

		[XmlAttribute("Version")]
		public string Version { get; set; }

		public string ProductName { get; set; }

		public string SourceName { get; set; }

		public string TargetName { get; set; }

        public string SourceFolder { get; set; }

        [XmlArrayItem("Include")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public List<string> SourceAssemblies { get; set; }

        public string TargetFolder { get; set; }

        [XmlArrayItem("Include")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public List<string> TargetAssemblies { get; set; }

		[XmlElement("Settings")]
		public PersistableProjectSettings Settings { get; set; }
	}
}
