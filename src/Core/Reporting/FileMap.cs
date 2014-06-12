using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class FileMap
	{
		private Dictionary<string, IFile> identToFileMap = new Dictionary<string, IFile>();

		//public IFolder IndexFolder { get; set; }

		public void Add(string key, IFile value)
		{
			Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
			Debug.Assert(value != null, "Value cannot be null");

			if (this.identToFileMap.ContainsKey(key))
				return;

			this.identToFileMap.Add(key, value);
		}

		public string PathFor(string key)
		{
			Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
			Debug.Assert(this.identToFileMap.ContainsKey(key), "Nothing stored for " + key);

			return this.identToFileMap[key].FullPath;
		}

		public string PathRelativeTo(string key, IFolder folder)
		{
			Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
			Debug.Assert(folder != null, "Folder cannot be null");

			Debug.Assert(this.identToFileMap.ContainsKey(key), "Nothing stored for " + key);

			IFile value = this.identToFileMap[key];

			return folder.TrailingSlashPath.MakeRelativePath(value.FullPath);
		}

		//public string LookupRelative(string key)
		//{
		//	Debug.Assert(!String.IsNullOrEmpty(key), "Key cannot be blank");
		//	Debug.Assert(this.identToFileMap.ContainsKey(key), "Nothing stored for " + key);
		//	Debug.Assert(this.IndexFolder != null, "IndexFolder property not set");

		//	return this.LookupRelativeTo(key, this.IndexFolder);
		//}
	}
}
