using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace NDifference
{
	[DebuggerDisplay("{Name}")]
	public class AssemblyInfo : IAssemblyInfo
	{
		private List<AssemblyReference> references = new List<AssemblyReference>();

		public AssemblyInfo(string name, string version)
            : this(name, new Version(version))
        {
        }

		public AssemblyInfo(string name, Version version)
        {
			this.Identifier = new Identifier().ToString();
            this.Name = name;
            this.Version = version;
        }

		public string Identifier { get; set; }

        public string Name { get; private set; }

        public Version Version { get; private set; }

        public string RuntimeVersion { get; set; }

        public string Architecture { get; set; }

		public ReadOnlyCollection<AssemblyReference> References
		{
			get
			{
				return new ReadOnlyCollection<AssemblyReference>(this.references);
			}
		}

		public void Add(AssemblyReference ar)
		{
			this.references.Add(ar);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentUICulture, "{0} Version={1}", this.Name, this.Version);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Version.GetHashCode();
		}
	}
}
