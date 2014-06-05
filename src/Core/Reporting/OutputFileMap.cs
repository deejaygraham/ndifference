using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class OutputFileMap
	{
		private Dictionary<string, string> identToFileMap = new Dictionary<string, string>();

		public string IndexFolder { get; set; }

		public void Add(string key, string value)
		{
			Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
			Debug.Assert(!String.IsNullOrEmpty(value), "Value cannot be blank");

			if (this.identToFileMap.ContainsKey(key))
				return;

			this.identToFileMap.Add(key, value);
		}

		public string Lookup(string key)
		{
			Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
			Debug.Assert(this.identToFileMap.ContainsKey(key), "Nothing stored for " + key);

			string value = this.identToFileMap[key];

			return value;
		}

		public string LookupRelativeTo(string key, string folder)
		{
			Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
			Debug.Assert(!String.IsNullOrEmpty(folder), "Folder cannot be blank");

			Debug.Assert(this.identToFileMap.ContainsKey(key), "Nothing stored for " + key);

			string value = this.identToFileMap[key];

			return folder.MakeRelativePath(value);
		}

		public string LookupRelative(string key)
		{
			Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
			Debug.Assert(this.identToFileMap.ContainsKey(key), "Nothing stored for " + key);
			Debug.Assert(!String.IsNullOrEmpty(this.IndexFolder), "IndexFolder property not set");

			string value = this.identToFileMap[key];

			return this.IndexFolder.MakeRelativePath(value).Replace("\\", "/");
		}
	}
}
