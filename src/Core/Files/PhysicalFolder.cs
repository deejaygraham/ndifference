using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	public class PhysicalFolder : IFolder
	{
		public PhysicalFolder(string fullPath)
		{
			this.FullPath = fullPath;
		}

		public PhysicalFolder(DirectoryInfo info)
			: this (info.FullName)
		{
		}

		public string FullPath { get; private set; }

		public string TrailingSlashPath
		{
			get
			{
				string TrailingSlash = new string(Path.DirectorySeparatorChar, 1);

				if (this.FullPath.EndsWith(TrailingSlash))
					return this.FullPath;

				return this.FullPath + TrailingSlash;
			}
		}
	}
}
